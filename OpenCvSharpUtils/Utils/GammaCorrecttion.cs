using OpenCvSharp;
using System;

namespace OpenCvSharpUtils
{
    public class GammaCorrecttion
    {
        /// <summary>
        /// ガンマ補正をします。
        /// </summary>
        public static void Correct(IplImage srcImage, IplImage dstImage, double gamma)
        {
            var lut = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                lut[i] = (byte)(Math.Pow((double)i / 255.0, 1.0 / gamma) * 255.0);
            }
            var lutMat = new CvMat(1, 256, MatrixType.U8C1, lut);
            Cv.LUT(srcImage, dstImage, lutMat);
        }
    }
}
