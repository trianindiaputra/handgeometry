using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
using Emgu.CV.CvEnum;

namespace HandGeometry
{
    public class PreProcessing
    {
        /// <summary>
        /// Crop the image with the given region
        /// </summary>
        /// <param name="inputImage">The original input image</param>
        /// <param name="croppedRegion">The cropped region of the input image.</param>
        /// <returns>Cropped image</returns>
        public static Image<Bgr, Byte> Cropping(Image<Bgr, Byte> inputImage, Rectangle croppedRegion)
        {
            return inputImage.Copy(croppedRegion);
        }

        /// <summary>
        /// Convert the color image to grayscale.
        /// </summary>
        /// <param name="inputImage">The original input image</param>
        /// <returns>The grayscale image</returns>
        public static Image<Gray, Byte> Convert2Grayscale(Image<Bgr, Byte> inputImage)
        {
            return inputImage.Convert<Gray, Byte>();
        }

        /// <summary>
        /// Filter out noise in the given image.
        /// </summary>
        /// <param name="inputImage">The original input image</param>
        /// <returns>The noise-free image</returns>
        public static Image<Gray, Byte> NoiseFilter(Image<Gray, Byte> inputImage, int size)
        {
            return inputImage.SmoothMedian(size);
        }

        /// <summary>
        /// Binarize the image
        /// </summary>
        /// <param name="inputImage">The original input image</param>
        /// <param name="maxintensity">The maximum intensity of the binarized image.</param>
        /// <returns>The binarized image</returns>
        public static Image<Gray, Byte> Binarization(Image<Gray, Byte> inputImage, Gray maxintensity)
        {
            IntPtr grayimage = inputImage.Ptr;
            IntPtr processed_image;
            Image<Gray, Byte> imagetemp = inputImage.CopyBlank();
            processed_image = imagetemp.Ptr;
            CvInvoke.cvThreshold(grayimage, processed_image, 160, maxintensity.Intensity, THRESH.CV_THRESH_BINARY | THRESH.CV_THRESH_OTSU);
            imagetemp.Ptr = processed_image;
            return imagetemp;
        }

        /// <summary>
        /// Remove the pegs from the image.
        /// </summary>
        /// <param name="inputImage">The original input image</param>
        /// <param name="PegTemplate">The image of the pegs</param>
        /// <returns>The pegs-free image</returns>
        public static Image<Gray, Byte> PegRemoval(Image<Gray, Byte> inputImage, Image<Gray, Byte> PegTemplate)
        {
            return inputImage.Sub(PegTemplate);
        }

        /// <summary>
        /// Detect edge of the hand shape in this image
        /// </summary>
        /// <param name="inputImage">The original input image</param>
        /// <returns></returns>
        public static Image<Gray, Byte> EdgeDetection(Image<Gray, Byte> inputImage)
        {
            Gray cannyThreshold = new Gray(127);
            Gray cannyThresholdLinking = new Gray(120);

            Image<Gray, Byte> cannyEdges = inputImage.Canny(cannyThreshold, cannyThresholdLinking);
            return cannyEdges;
        }

        public static List<Point> FindContours(Image<Gray, Byte> image)
        {
            List<Point> contours = new List<Point>();

            Point startPoint = new Point();
            for (int i = 0; i < image.Width; i++)
            {
                if (image[image.Height - 1, i].Equals(new Gray(255)))
                {
                    startPoint.X = i;
                    startPoint.Y = image.Height - 1;
                    contours.Add(startPoint);
                    break;
                }
            }

            List<Point> neighbours = FindNeighbours(image, startPoint);
            while (neighbours.Count != 0)
            {
                if (neighbours.Count == 1)
                {
                    if (contours.Contains(neighbours[0]) == false)
                    {
                        break;
                    }
                    else
                    {
                        contours.Add(neighbours[0]);                        
                        startPoint = neighbours[0];
                        neighbours = FindNeighbours(image, startPoint);
                    }
                }
                else
                {
                    List<Point> newNeighbours = new List<Point>();
                    foreach (Point p in neighbours)
                    {
                        if (contours.Contains(p) == false)
                        {
                            newNeighbours.Add(p);
                        }
                    }

                    if (newNeighbours.Count != 0)
                    {
                        if (newNeighbours.Count == 1)
                        {
                            contours.Add(newNeighbours[0]);
                            startPoint = newNeighbours[0];
                            neighbours = FindNeighbours(image, startPoint);
                        }
                        else
                        {
                            Point nearestNeighbour = newNeighbours[0];
                            for (int i = 1; i < newNeighbours.Count; i++)
                            {
                                if (MeasureManhattanDistance(startPoint, newNeighbours[i]) < MeasureManhattanDistance(startPoint, nearestNeighbour))
                                {
                                    nearestNeighbour = newNeighbours[i];
                                }
                            }

                            contours.Add(nearestNeighbour);
                            startPoint = nearestNeighbour;
                            neighbours = FindNeighbours(image, startPoint);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return contours;
        }
        private static List<Point> FindNeighbours(Image<Gray, Byte> image, Point startPoint)
        {
            List<Point> neighbours = new List<Point>();

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    try
                    {
                        if (image[startPoint.Y + i, startPoint.X + j].Equals(new Gray(255)))
                        {
                            neighbours.Add(new Point(startPoint.X + j, startPoint.Y + i));
                        }
                    }
                    catch
                    {
                    }
                }
            }

            return neighbours;
        }

        private static int MeasureManhattanDistance(Point p1, Point p2)
        {
            return Math.Abs(p2.X - p1.X) + Math.Abs(p2.Y - p1.Y);
        }

        private static double MeasureDistance(Point p1, Point p2)
        {
            return Math.Sqrt(1.0 * ((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y)));
        }

        public static List<int> BuildContoursGraph(List<Point> contours)
        {
            List<int> contoursGraph = new List<int>();
            Point startPoint = contours[0];

            foreach (Point p in contours)
            {
                contoursGraph.Add(MeasureManhattanDistance(startPoint, p));
                
            }

            return contoursGraph;
        }

        public static Image<Bgr, Byte> DrawContoursGraph(List<int> contoursGraph)
        {
            Image<Bgr, Byte> image = new Image<Bgr, byte>(contoursGraph.Count - 1, contoursGraph.Max(), new Bgr(255, 255, 255));
            
            for (int i = 0; i < contoursGraph.Count; i++)
            {
                image.Draw(new Rectangle(new Point(i, contoursGraph[i]), new Size(0, 0)), new Bgr(0, 0, 255), 1);
            }

            return image;
        }

        public static List<List<int>> FindExtremes(List<int> contoursGraph)
        {
            List<List<int>> extremes = new List<List<int>>();
            int idFlag = 0;

            for (int i = 1; i < contoursGraph.Count - 1; i++)
            {
                int temp1 = contoursGraph[i] - contoursGraph[i - 1];
                int temp2 = contoursGraph[i + 1] - contoursGraph[i];
                if (temp1 * temp2 < 0)
                {
                    CheckExtreme(i, contoursGraph);
                }
            }

            return extremes;
        }

        private static int CheckExtreme(int candidate, List<int> contoursGraph)
        {
            try
            {
                if (contoursGraph[candidate] > contoursGraph[candidate - 1]
                    && contoursGraph[candidate] < contoursGraph[candidate + 1])
                {
                    return 1;
                }
                else if (contoursGraph[candidate] > contoursGraph[candidate - 1]
                    && contoursGraph[candidate] < contoursGraph[candidate + 1])
                {
                    return -1;
                }
                else
                {
                }
            }
            catch
            {
                if (candidate == 0)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }

            return 0;
        }
    }
}
