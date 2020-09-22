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
        
        static void Main(string[] args)
        {
            Console.WriteLine("Taking Picture");
            CameraCapture cp = new CameraCapture();
            Detect_Landmarks dl = new Detect_Landmarks();
            ImageShow ishow = new ImageShow();
            Console.WriteLine("Picture Taken");

            Thread camera = new Thread(cp.CaptureCamera);
            Thread d_land = new Thread(dl.detect_landmark);
            Thread i_show = new Thread(ishow.view_image);

            camera.Start();

            Thread.Sleep(500);
            d_land.Start();
            Thread.Sleep(500);
            i_show.Start();
        }
    }
}
