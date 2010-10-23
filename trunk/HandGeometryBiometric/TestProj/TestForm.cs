using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using HandGeometry;
using System.Threading;

namespace TestProj
{
    public partial class TestForm : Form
    {
        public Image<Gray, Byte> grayImage;

        public TestForm()
        {
            InitializeComponent();

            PreProcess();
        }

        public void PreProcess()
        {
            Image<Bgr, Byte> originalImage = new Image<Bgr, Byte>(@"D:\Master\hand.jpg");            
            originalImage = PreProcessing.Cropping(originalImage, new Rectangle(0 + 10, 0 + 10, originalImage.Width - 30, originalImage.Height - 30));
            originalImage = originalImage.Resize(0.45, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
            //originalImage = originalImage.Rotate(90.0, new Bgr(255, 255, 255));
            pictureBoxOriginal.Image = originalImage.ToBitmap();

            grayImage = PreProcessing.Convert2Grayscale(originalImage);
            grayImage = PreProcessing.NoiseFilter(grayImage, 9);
            pictureBoxGray.Image = grayImage.ToBitmap();

            

            grayImage = PreProcessing.Binarization(grayImage, new Gray(255.0));
            pictureBoxBinary.Image = grayImage.ToBitmap();

            grayImage = PreProcessing.EdgeDetection(grayImage);


            
            
            pictureBoxContour.Image = grayImage.ToBitmap();
            int count = 0;
            for (int i = 0; i < grayImage.Height; i++)
            {
                for (int j = 0; j < grayImage.Width; j++)
                {
                    if (grayImage[i, j].Equals(new Gray(255)))
                    {
                        count++;
                    }
                }
            }
            MessageBox.Show(count.ToString());
            Thread t = new Thread(new ThreadStart(Run));
            t.Start();
            
        }

        public void Run()
        {
            //Contour<Point> contours = grayImage.FindContours();
            //List<Point> listPoints = contours.ToList();

            List<Point> listPoints = PreProcessing.FindContours(grayImage);

            //foreach (Point i in listPoints)
            //{

            //    grayImage.Draw(new Rectangle(i, new Size(0, 0)), new Gray(0), 1);
            //    pictureBoxContour.Image = grayImage.ToBitmap();
            //    Thread.Sleep(10);

            //}
            //MessageBox.Show(listPoints.Count.ToString());
            Image<Bgr, Byte> contoursGraph = PreProcessing.DrawContoursGraph(PreProcessing.BuildContoursGraph(listPoints));
            //contoursGraph = contoursGraph.Resize(0.45, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
            pictureBoxBinary.Image = contoursGraph.ToBitmap();
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            double d = 1.5;
            MessageBox.Show(Math.Round(d).ToString());
        }
    }
}
