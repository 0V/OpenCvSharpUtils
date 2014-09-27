using OpenCvSharp;

namespace OpenCvSharpUtils
{
    public class FindContours
    {
        public static CvSeq<CvPoint> DetectContours(IplImage srcImage)
        {
            var grayImage = new IplImage(srcImage.Size, BitDepth.U8, 1);
            srcImage.CvtColor(grayImage, ColorConversion.BgraToGray);
            var binaryImage = grayImage.Clone();
            Cv.Threshold(grayImage, binaryImage, 0, 255, ThresholdType.Binary | ThresholdType.Otsu);
            CvSeq<CvPoint> contours;

            using (var storage = new CvMemStorage())
            {
                // 輪郭の検出
                Cv.FindContours(binaryImage, storage, out contours, CvContour.SizeOf, ContourRetrieval.External, ContourChain.ApproxSimple);
                //輪郭の描画
                Cv.DrawContours(srcImage, contours, new CvScalar(0, 0, 255), new CvScalar(0, 255, 0), 3);
            }

            return contours;
        }
    }
}
