using OpenCvSharp;
namespace OpenCvSharpUtils
{
    public class Morphology
    {        
        // srcImage 8bit1ch
        // dstImage 8bit1ch
        public static void Operation(IplImage srcImage, IplImage dstImage, MorphologyOperation operation, int cols, int rows, int anchorX, int anchorY)
        {
            var tmpImage = new IplImage(srcImage.Size, BitDepth.U8, 1);
            var element = Cv.CreateStructuringElementEx(cols, rows, anchorX, anchorY, ElementShape.Rect, null);
            Cv.MorphologyEx(srcImage, dstImage, tmpImage, element, operation, 1);
            Cv.ReleaseImage(tmpImage);
        }
        public static void Operation(IplImage srcImage, IplImage dstImage, MorphologyOperation operation, int cols, int rows)
        {
            var tmpImage = new IplImage(srcImage.Size, BitDepth.U8, 1);
            var element = Cv.CreateStructuringElementEx(cols, rows, 1, 1, ElementShape.Rect, null);
            Cv.MorphologyEx(srcImage, dstImage, tmpImage, element, operation, 1);
            Cv.ReleaseImage(tmpImage);
        }
        public static void Operation(IplImage srcImage, IplImage dstImage, MorphologyOperation operation)
        {
            var tmpImage = new IplImage(srcImage.Size, BitDepth.U8, 1);
            var element = Cv.CreateStructuringElementEx(3, 3, 1, 1, ElementShape.Rect, null);
            Cv.MorphologyEx(srcImage, dstImage, tmpImage, element, operation, 1);
            Cv.ReleaseImage(tmpImage);
        }

    }
}
