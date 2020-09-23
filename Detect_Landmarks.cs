using System;
using System.Globalization;
using System.Threading;
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

                        var img = Dlib.LoadImage<RgbPixel>(name + @"\rawImage\raw" + + i + ".png");


                        var faces = fd.Operator(img);
                        double[,] location = new double[69, 2];

                        foreach (var face in faces)
                        {

                            var shape = sp.Detect(img, face);

                            Console.WriteLine(shape.ToString());


                            Dlib.DrawLine(img, shape.GetPart(36), shape.GetPart(37), new RgbPixel(255, 0, 0));
                            Dlib.DrawLine(img, shape.GetPart(37), shape.GetPart(38), new RgbPixel(255, 0, 0));
                            Dlib.DrawLine(img, shape.GetPart(38), shape.GetPart(39), new RgbPixel(255, 0, 0));
                            Dlib.DrawLine(img, shape.GetPart(39), shape.GetPart(40), new RgbPixel(255, 0, 0));
                            Dlib.DrawLine(img, shape.GetPart(40), shape.GetPart(41), new RgbPixel(255, 0, 0));
                            Dlib.DrawLine(img, shape.GetPart(41), shape.GetPart(36), new RgbPixel(255, 0, 0));

                            Dlib.DrawLine(img, shape.GetPart(42), shape.GetPart(43), new RgbPixel(0, 255, 0));
                            Dlib.DrawLine(img, shape.GetPart(43), shape.GetPart(44), new RgbPixel(0, 255, 0));
                            Dlib.DrawLine(img, shape.GetPart(44), shape.GetPart(45), new RgbPixel(0, 255, 0));
                            Dlib.DrawLine(img, shape.GetPart(45), shape.GetPart(46), new RgbPixel(0, 255, 0));
                            Dlib.DrawLine(img, shape.GetPart(46), shape.GetPart(47), new RgbPixel(0, 255, 0));
                            Dlib.DrawLine(img, shape.GetPart(47), shape.GetPart(42), new RgbPixel(0, 255, 0));
                            for (var j=36; j<=47; j++)
                            {
                                var point = shape.GetPart((uint)j);
                                var rect = new DlibDotNet.Rectangle(point);
                                
                                if (j >= 36 && j <= 41)
                                {
                                    location[j, 0] = shape.GetPart((uint)j).X;
                                    location[j, 1] = shape.GetPart((uint)j).Y;
                                    
                                }
                                else if (j >= 42 && j <= 47)
                                {
                                    location[j, 0] = shape.GetPart((uint)j).X;
                                    location[j, 1] = shape.GetPart((uint)j).Y;
                                    
                                }
                                
                            }
                            if (eye_aspect_ratio(location) < .20)
                            {
                                Notify.Alarm();
                            }
                        }

                        Dlib.SavePng(img, name + @"\detectedImage\marked" + i + ".png");

                    }
                    ++i;
                }
                catch (Exception e)
                {
                    Thread t = Thread.CurrentThread;

                    Thread.Sleep(1300);

                    Console.WriteLine("Detect Landmarks  ");
                }
            }
        }

        private double eye_aspect_ratio(double [,] location)
        {
            var disA = Math.Sqrt((location[36, 0] - location[39, 0]) * (location[36, 0] - location[39, 0]) + (location[36, 1] - location[39, 1]) * (location[36, 1] - location[39, 1]));
            var disB = Math.Sqrt((location[37, 0] - location[41, 0]) * (location[37, 0] - location[41, 0]) + (location[37, 1] - location[41, 1]) * (location[37, 1] - location[41, 1]));
            var disC = Math.Sqrt((location[38, 0] - location[40, 0]) * (location[38, 0] - location[40, 0]) + (location[38, 1] - location[40, 1]) * (location[38, 1] - location[40, 1]));
            var avg_1 = (disB + disC) / (2.00 * disA);

            var disD = Math.Sqrt((location[42, 0] - location[45, 0]) * (location[42, 0] - location[45, 0]) + (location[42, 1] - location[45, 1]) * (location[42, 1] - location[45, 1]));
            var disE = Math.Sqrt((location[43, 0] - location[47, 0]) * (location[43, 0] - location[47, 0]) + (location[43, 1] - location[47, 1]) * (location[43, 1] - location[47, 1]));
            var disF = Math.Sqrt((location[44, 0] - location[46, 0]) * (location[44, 0] - location[46, 0]) + (location[44, 1] - location[46, 1]) * (location[44, 1] - location[46, 1]));
            var avg_2 = (disE + disF) / (2.00 * disD);

            Console.WriteLine(location[36,0] + " " + location[36,1] + " " + location[39,0] + " " + location[39,1]);
            return (avg_1 + avg_2) / 2.00;
        }
        
    }
}
