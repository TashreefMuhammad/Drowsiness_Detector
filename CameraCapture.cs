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
                string inputFilePath = name + @"\ImageFolder\any.png";
                while (isCameraRunning)
                {
                    capture.Read(frame);
                    image = BitmapConverter.ToBitmap(frame);
                    Bitmap snapshot = new Bitmap(image);
                    //snapshot.Save(string.Format(@"C:\Users\Tashreef\Desktop\{0}.png", Guid.NewGuid()), ImageFormat.Png);
                    snapshot.Save(inputFilePath);
                }
            }
        }
        
    }
}
