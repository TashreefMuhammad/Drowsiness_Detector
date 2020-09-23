using System;
using System.Drawing;
using System.Threading;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace DrowsyDoc
{
    class CameraCapture
    {
       

        static VideoCapture capture;
        static Mat frame;
        static Bitmap image;
        private static Thread camera;
        static bool isCameraRunning = true;


        // Declare required methods
        public void CaptureCamera()
        {
            //camera = new Thread(new ThreadStart(CaptureCameraCallback));
            //camera.Start();
            try
            {
                CaptureCameraCallback();
            }
            catch (Exception e)
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
                int i = 0;

                while (isCameraRunning)
                {
                    i %= 100;
                    capture.Read(frame);
                    image = BitmapConverter.ToBitmap(frame);
                    Bitmap snapshot = new Bitmap(image);
                    //snapshot.Save(string.Format(@"C:\Users\Tashreef\Desktop\{0}.png", Guid.NewGuid()), ImageFormat.Png);
                    snapshot.Save(name + @"\rawImage\raw" + i + @".png");
                    ++i;
                    Thread t = Thread.CurrentThread;
                    //Console.SetCursorPosition(0, 0);
                    //Console.WriteLine("Thread {0} going to sleep", t.Name);
                    Thread.Sleep(1300);
                    //Console.WriteLine("Thread {0} finished sleep", t.Name);
                }
                
            }
        }
        
    }
}
