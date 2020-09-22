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
    class Detect_Landmarks
    { 
        public void detect_landmark()
        {
            string name = Environment.CurrentDirectory;
            Console.WriteLine(name);
            string inputFilePath = name + @"\ImageFolder\any.png";
            string outputFilePath = name + @"\ImageFolder\out.png";
            string path = outputFilePath;
            try
            {
                while (true)
                {
                    using (var fd = Dlib.GetFrontalFaceDetector())
                    using (var sp = ShapePredictor.Deserialize(name + @"\shape_predictor_68_face_landmarks.dat"))
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

                }
            }catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
    }
}
