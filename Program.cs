

using System;
using System.Threading;

namespace DrowsyDoc
{
    class Program
    {
        
        static void Main(string[] args)
        {
            CameraCapture cp = new CameraCapture();
            Detect_Landmarks dl = new Detect_Landmarks();
            ImageShow ishow = new ImageShow();

            Thread camera = new Thread(cp.CaptureCamera);
            camera.Name = "Take Picture Thread";
            Thread d_land = new Thread(dl.detect_landmark);
            d_land.Name = "LandMark Deetection Thread";
            Thread i_show = new Thread(ishow.view_image);
            i_show.Name = "Console Printing Thread";

            camera.Start();

            
            d_land.Start();

            //i_show.Start();

        }
    }
}
