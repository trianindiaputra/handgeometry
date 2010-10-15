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
        }

        public void PreProcess()
        {
            Image<Bgr, Byte> originalImage = new Image<Bgr, Byte>(@"D:\Master\hand.jpg");
            originalImage = PreProcessing.Cropping(originalImage, new Rectangle(0 + 100, 0 + 50, originalImage.Width - 120, originalImage.Height - 120));
            originalImage = originalImage.Resize(0.45, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
            pictureBoxOriginal.Image = originalImage.ToBitmap();

            grayImage = PreProcessing.Convert2Grayscale(originalImage);
            grayImage = PreProcessing.NoiseFilter(grayImage, 9);
            pictureBoxGray.Image = grayImage.ToBitmap();

            

            grayImage = PreProcessing.Binarization(grayImage, new Gray(255.0));
            pictureBoxBinary.Image = grayImage.ToBitmap();

            grayImage = PreProcessing.EdgeDetection(grayImage);


            
            
            pictureBoxContour.Image = grayImage.ToBitmap();

            Thread t = new Thread(new ThreadStart(Run));
            t.Start();

            
            
        }

        public void Run()
        {
            Contour<Point> contours = grayImage.FindContours();
            List<Point> listPoints = contours.ToList();

            foreach (Point i in listPoints)
            {

                grayImage.Draw(new Rectangle(i, new Size(2, 2)), new Gray(150), 1);
                pictureBoxContour.Image = grayImage.ToBitmap();
                Thread.Sleep(10);

            }
            MessageBox.Show("done");
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            
            PreProcess();
        }
    }
}
