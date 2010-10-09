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
    }
}
