using OpenCvSharp;

namespace OpenCvSharpUtils
{
    /// <summary>
    /// 円を抽出します。
    /// </summary>
    public class CirclesDetectior
    {
        public static IplImage Detect(IplImage srcImage)
        {
            var grayImage = new IplImage(srcImage.Size, BitDepth.U8, 1);
            var dstImage = srcImage.Clone();
            Cv.CvtColor(srcImage, grayImage, ColorConversion.BgrToGray);
            using (var storage = new CvMemStorage())
            using (var circles = Cv.HoughCircles(grayImage, storage, HoughCirclesMethod.Gradient, 2, 10, 160, 50, 10, 20))
            {
                // 明示的にCvCircleSegmentを指定する必要がある
                foreach (CvCircleSegment circle in circles)
                {
                    dstImage.Circle(circle.Center, (int)circle.Radius, CvColor.Red, 2);
                }
            }
            return dstImage;
        }

        public static IplImage Detect(IplImage srcImage, double dp, double minDist)
        {
            var grayImage = new IplImage(srcImage.Size, BitDepth.U8, 1);
            var dstImage = srcImage.Clone();
            Cv.CvtColor(srcImage, grayImage, ColorConversion.BgrToGray);
            using (var storage = new CvMemStorage())
            using (var circles = Cv.HoughCircles(grayImage, storage, HoughCirclesMethod.Gradient, dp,minDist))
            {
                foreach (CvCircleSegment circle in circles)
                {
                    dstImage.Circle(circle.Center, (int)circle.Radius, CvColor.Red, 2);
                }
            }
            return dstImage;
        }

        public static IplImage Detect(IplImage srcImage, double dp, double minDist, double param1, double param2)
        {
            var grayImage = new IplImage(srcImage.Size, BitDepth.U8, 1);
            var dstImage = srcImage.Clone();
            Cv.CvtColor(srcImage, grayImage, ColorConversion.BgrToGray);
            using (var storage = new CvMemStorage())
            using (var circles = Cv.HoughCircles(grayImage, storage, HoughCirclesMethod.Gradient, dp, minDist, param1, param2))
            {
                foreach (CvCircleSegment circle in circles)
                {
                    dstImage.Circle(circle.Center, (int)circle.Radius, CvColor.Red, 2);
                }
            }
            return dstImage;
        }

        public static IplImage Detect(IplImage srcImage, double dp, double minDist, double param1, double param2, int minRadius)
        {
            var grayImage = new IplImage(srcImage.Size, BitDepth.U8, 1);
            var dstImage = srcImage.Clone();
            Cv.CvtColor(srcImage, grayImage, ColorConversion.BgrToGray);
            using (var storage = new CvMemStorage())
            using (var circles = Cv.HoughCircles(grayImage, storage, HoughCirclesMethod.Gradient, dp, minDist, param1, param2, minRadius))
            {
                foreach (CvCircleSegment circle in circles)
                {
                    dstImage.Circle(circle.Center, (int)circle.Radius, CvColor.Red, 2);
                }
            }
            return dstImage;
        }

        public static IplImage Detect(IplImage srcImage, double dp, double minDist, double param1, double param2, int minRadius, int maxRadius)
        {
            var grayImage = new IplImage(srcImage.Size, BitDepth.U8, 1);
            var dstImage = srcImage.Clone();
            Cv.CvtColor(srcImage, grayImage, ColorConversion.BgrToGray);
            using (var storage = new CvMemStorage())
            using (var circles = Cv.HoughCircles(grayImage, storage, HoughCirclesMethod.Gradient, dp, minDist, param1, param2, minRadius, maxRadius))
            {
                foreach (CvCircleSegment circle in circles)
                {
                    dstImage.Circle(circle.Center, (int)circle.Radius, CvColor.Red, 2);
                }
            }
            return dstImage;
        }
    }
}
