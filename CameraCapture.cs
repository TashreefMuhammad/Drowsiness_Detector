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
            
            try
            {
                CaptureCameraCallback();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //Capturing Images Using The Device's Camera
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
                    
                    snapshot.Save(name + @"\rawImage\raw" + i + @".png");
                    ++i;
                    Thread t = Thread.CurrentThread;
                    
                    Thread.Sleep(1500);
                    
                }
                
            }
        }
        
    }
}
