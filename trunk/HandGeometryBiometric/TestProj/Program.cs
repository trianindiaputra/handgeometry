using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HandGeometry;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.Drawing;
using System.Windows.Forms;

namespace TestProj
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TestForm());
        }

        //static void Main(string[] args)
        //{
        //    Image<Bgr, Byte> originalImage = new Image<Bgr, Byte>(@"D:\Master\hand.jpg");
        //    Image<Gray, Byte> grayImage = PreProcessing.Convert2Grayscale(originalImage);
        //    grayImage = PreProcessing.NoiseFilter(grayImage, 9);
        //    grayImage = PreProcessing.Binarization(grayImage, new Gray(255.0));
        //    grayImage = PreProcessing.EdgeDetection(grayImage);
        //    Contour<Point> contours = grayImage.FindContours();
        //    List<Point> listPoints = contours.ToList();
        //    Console.WriteLine("Num: " + contours.Count());
        //    Console.WriteLine("Num: " + listPoints.Count());
        //}
    }
}
