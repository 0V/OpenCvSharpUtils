using OpenCvSharp;

namespace OpenCvSharpUtils
{
    /// <summary>
    /// ノイズを減らします。
    /// </summary>
    public class NoiseReductor
    {
        public static void Inpaint(IplImage srcImage, IplImage maskImage, IplImage dstImage, double inpaintRange, InpaintMethod method)
        {
            Cv.Inpaint(srcImage,maskImage,dstImage,inpaintRange,method);
            maskImage.Dispose();
        }
    }
}