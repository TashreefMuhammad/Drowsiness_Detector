/* Using OpenCV sharp
 * 
 * Useful Links
 * https://www.c-sharpcorner.com/UploadFile/shubham0987/creating-first-emgu-cv-project/
 * https://www.codeproject.com/Articles/722569/Video-Capture-using-OpenCV-with-Csharp
 * http://emgu.com/wiki/index.php/Download_And_Installation
 * https://ourcodeworld.com/articles/read/761/how-to-take-snapshots-with-the-web-camera-with-c-sharp-using-the-opencvsharp-library-in-winforms
 */


using System;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using OpenCvSharp;
using OpenCvSharp.Extensions;

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
                while (isCameraRunning)
                {
                    capture.Read(frame);
                    image = BitmapConverter.ToBitmap(frame);
                    Bitmap snapshot = new Bitmap(image);
                    snapshot.Save(string.Format(@"C:\Users\Tashreef\Desktop\{0}.png", Guid.NewGuid()), ImageFormat.Png);
                }
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
