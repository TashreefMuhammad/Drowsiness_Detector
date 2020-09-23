using System;
using DlibDotNet;
using DlibDotNet.Extensions;
using Dlib = DlibDotNet.Dlib;


namespace DrowsyDoc
{
    class Detect_Landmarks
    { 
        public void detect_landmark()
        {
            string name = Environment.CurrentDirectory;
            
            int i = 0;
            while (true)
            {
                i %= 100;
                try
                {
                    using (var fd = Dlib.GetFrontalFaceDetector())
                    using (var sp = ShapePredictor.Deserialize(name + @"\shape_predictor_68_face_landmarks.dat"))
                    {
                        // load input image
                        var img = Dlib.LoadImage<RgbPixel>(name + @"\rawImage\raw" + + i + ".png");

                        // find all faces in the image
                        var faces = fd.Operator(img);
                        foreach (var face in faces)
                        {
                            // find the landmark points for this face
                            var shape = sp.Detect(img, face);

                            // draw the landmark points on the image
                            for (var j = 0; j < shape.Parts; j++)
                            {
                                var point = shape.GetPart((uint)j);
                                var rect = new DlibDotNet.Rectangle(point);
                                if (j >= 36 && j <= 41)
                                    Dlib.DrawRectangle(img, rect, color: new RgbPixel(255, 0, 0), thickness: 10);
                                else if (j >= 42 && j <= 47)
                                    Dlib.DrawRectangle(img, rect, color: new RgbPixel(0, 255, 0), thickness: 10);
                                else
                                    Dlib.DrawRectangle(img, rect, color: new RgbPixel(255, 255, 0), thickness: 4);
                            }
                        }
                        // the rest of the code goes here....
                        Dlib.SavePng(img, name + @"\detectedImage\marked" + i + ".png");
                        //Dlib.SaveJpeg(img, "output.jpg");
                    }
                    ++i;
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e);
                    Console.WriteLine("Detect Landmarks");
                }
            }
        }
        
    }
}
