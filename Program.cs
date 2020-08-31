/* Using OpenCV sharp
 * 
 * Useful Links
 * https://www.pyimagesearch.com/2017/04/10/detect-eyes-nose-lips-jaw-dlib-opencv-python/
 * https://stackoverflow.com/questions/33538527/display-a-image-in-a-console-application
 * https://medium.com/machinelearningadvantage/detect-facial-landmark-points-with-c-and-dlib-in-only-50-lines-of-code-71ab59f8873f
 * https://www.c-sharpcorner.com/UploadFile/shubham0987/creating-first-emgu-cv-project/
 * https://www.codeproject.com/Articles/722569/Video-Capture-using-OpenCV-with-Csharp
 * http://emgu.com/wiki/index.php/Download_And_Installation
 * https://ourcodeworld.com/articles/read/761/how-to-take-snapshots-with-the-web-camera-with-c-sharp-using-the-opencvsharp-library-in-winforms
 */


using System;
using System.Drawing;
using System.Threading;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using DlibDotNet;
using DlibDotNet.Extensions;
using Dlib = DlibDotNet.Dlib;
using System.Runtime.InteropServices;
using System.IO;

namespace DrowsyDoc
{
    class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr CreateFile(
            string lpFileName,
            int dwDesiredAccess,
            int dwShareMode,
            IntPtr lpSecurityAttributes,
            int dwCreationDisposition,
            int dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GetCurrentConsoleFont(
            IntPtr hConsoleOutput,
            bool bMaximumWindow,
            [Out][MarshalAs(UnmanagedType.LPStruct)] ConsoleFontInfo lpConsoleCurrentFont);

        [StructLayout(LayoutKind.Sequential)]
        internal class ConsoleFontInfo
        {
            internal int nFont;
            internal Coord dwFontSize;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct Coord
        {
            [FieldOffset(0)]
            internal short X;
            [FieldOffset(2)]
            internal short Y;
        }

        private const int GENERIC_READ = unchecked((int)0x80000000);
        private const int GENERIC_WRITE = 0x40000000;
        private const int FILE_SHARE_READ = 1;
        private const int FILE_SHARE_WRITE = 2;
        private const int INVALID_HANDLE_VALUE = -1;
        private const int OPEN_EXISTING = 3;

        static VideoCapture capture;
        static Mat frame;
        static Bitmap image;
        private static Thread camera;
        static bool isCameraRunning = true;


        // Declare required methods
        private static void CaptureCamera()
        {
            //camera = new Thread(new ThreadStart(CaptureCameraCallback));
            //camera.Start();
            try
            {
                CaptureCameraCallback();
            }catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void CaptureCameraCallback()
        {
            capture = new VideoCapture(0);
            capture.Open(0);
            frame = new Mat();

            if (capture.IsOpened())
            {
                string name = Environment.CurrentDirectory;
                Console.WriteLine(name);
                string inputFilePath = name + @"\ImageFolder\any.png";
                string outputFilePath = name + @"\ImageFolder\out.png";
                string path = outputFilePath;
                while (isCameraRunning)
                {
                    capture.Read(frame);
                    image = BitmapConverter.ToBitmap(frame);
                    Bitmap snapshot = new Bitmap(image);
                //snapshot.Save(string.Format(@"C:\Users\Tashreef\Desktop\{0}.png", Guid.NewGuid()), ImageFormat.Png);
                    snapshot.Save(inputFilePath);

                    using (var fd = Dlib.GetFrontalFaceDetector())
                    using (var sp = ShapePredictor.Deserialize(name+@"\shape_predictor_68_face_landmarks.dat"))
                    {
                        // load input image
                        var img = Dlib.LoadImage<RgbPixel>(inputFilePath);

                        // find all faces in the image
                        var faces = fd.Operator(img);
                        foreach (var face in faces)
                        {
                            // find the landmark points for this face
                            var shape = sp.Detect(img, face);

                            // draw the landmark points on the image
                            for (var i = 0; i < shape.Parts; i++)
                            {
                                var point = shape.GetPart((uint)i);
                                var rect = new DlibDotNet.Rectangle(point);
                                if (i >= 36 && i <= 41)
                                    Dlib.DrawRectangle(img, rect, color: new RgbPixel(255, 0, 0), thickness: 10);
                                else if (i >= 42 && i <= 47)
                                    Dlib.DrawRectangle(img, rect, color: new RgbPixel(0, 255, 0), thickness: 10);
                                else
                                    Dlib.DrawRectangle(img, rect, color: new RgbPixel(255, 255, 0), thickness: 4);
                            }
                        }
                    // the rest of the code goes here....
                       Dlib.SavePng(img, outputFilePath);
                        //Dlib.SaveJpeg(img, "output.jpg");
                    }
                    System.Drawing.Point location = new System.Drawing.Point(5, 5);
                    System.Drawing.Size imageSize = new System.Drawing.Size(20, 10); // desired image size in characters
                    // draw some placeholders
                    Console.SetCursorPosition(location.X - 1, location.Y);
                    Console.Write(">");
                    Console.SetCursorPosition(location.X + imageSize.Width, location.Y);
                    Console.Write("<");
                    Console.SetCursorPosition(location.X - 1, location.Y + imageSize.Height - 1);
                    Console.Write(">");
                    Console.SetCursorPosition(location.X + imageSize.Width, location.Y + imageSize.Height - 1);
                    Console.WriteLine("<");

                    //string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures), @"Sample Pictures\tulips.jpg");

                    Console.WriteLine(DateTime.Now.ToString("HH:mm:ss tt"));
                    using (Graphics g = Graphics.FromHwnd(GetConsoleWindow()))
                    {
                        using (Image image = Image.FromFile(path))
                        {
                            System.Drawing.Size fontSize = GetConsoleFontSize();

                            // translating the character positions to pixels
                            System.Drawing.Rectangle imageRect = new System.Drawing.Rectangle(
                                location.X * fontSize.Width,
                                location.Y * fontSize.Height,
                                imageSize.Width * fontSize.Width,
                                imageSize.Height * fontSize.Height);
                            g.DrawImage(image, imageRect);
                        }
                    }
                    Console.WriteLine(DateTime.Now.ToString("HH:mm:ss tt"));
                    //System.Threading.Thread.Sleep(3000);
                    Console.WriteLine("Cleared");
                }
            }
        }
        private static System.Drawing.Size GetConsoleFontSize()
        {
            // getting the console out buffer handle
            IntPtr outHandle = CreateFile("CONOUT$", GENERIC_READ | GENERIC_WRITE,
                FILE_SHARE_READ | FILE_SHARE_WRITE,
                IntPtr.Zero,
                OPEN_EXISTING,
                0,
                IntPtr.Zero);
            int errorCode = Marshal.GetLastWin32Error();
            if (outHandle.ToInt32() == INVALID_HANDLE_VALUE)
            {
                throw new IOException("Unable to open CONOUT$", errorCode);
            }

            ConsoleFontInfo cfi = new ConsoleFontInfo();
            if (!GetCurrentConsoleFont(outHandle, false, cfi))
            {
                throw new InvalidOperationException("Unable to get font information.");
            }

            return new System.Drawing.Size(cfi.dwFontSize.X, cfi.dwFontSize.Y);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Taking Picture");
            CaptureCamera();
            Console.WriteLine("Picture Taken");
        }
    }
}
