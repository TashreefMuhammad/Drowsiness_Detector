/* Using OpenCV sharp
 * 
 * Useful Links
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

namespace DrowsyDoc
{
    class Program
    {
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
                //while (isCameraRunning)
                //{
                    capture.Read(frame);
                    image = BitmapConverter.ToBitmap(frame);
                    Bitmap snapshot = new Bitmap(image);
                    string inputFilePath = @"C:\Users\Tashreef\Desktop\any.png";
                    string outputFilePath = @"C:\Users\Tashreef\Desktop\out.png";
                //snapshot.Save(string.Format(@"C:\Users\Tashreef\Desktop\{0}.png", Guid.NewGuid()), ImageFormat.Png);
                    snapshot.Save(inputFilePath);

                    using (var fd = Dlib.GetFrontalFaceDetector())
                    using (var sp = ShapePredictor.Deserialize(@"D:\Sem_6_Fa_2019_(3.2)\CSE 3200\DrowsyDoc\DrowsyDoc\shape_predictor_68_face_landmarks.dat"))
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
                                Dlib.DrawRectangle(img, rect, color: new RgbPixel(255, 255, 0), thickness: 4);
                            }
                        }
                    // the rest of the code goes here....
                        Dlib.SavePng(img, outputFilePath);
                        //Dlib.SaveJpeg(img, "output.jpg");
                    }
                //}
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Taking Picture");
            CaptureCamera();
            Console.WriteLine("Picture Taken");
        }
    }
}
