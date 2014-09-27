using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCvSharpUtils
{
    class Program
    {
        static void Main(string[] args)
        {
            StartSample();
        }

        /// <summary>
        /// カメラから映像を取得して肌色領域を抽出しモルフォロジー演算をするサンプルコード。
        /// </summary>
        static void StartSample()
        {
            int cameraIndex = 0;
            var capture = new CvCapture(cameraIndex);
            var cameraImage = capture.QueryFrame();
            var windowSize = cameraImage.Size;
            var colorImage = new IplImage(windowSize, BitDepth.U8, 3);
            var morImage = new IplImage(windowSize, BitDepth.U8, 3);

            while (Cv.WaitKey(1) != 'q')
            {
                cameraImage = capture.QueryFrame();

                ColorExtraction.Extract(cameraImage, colorImage, ColorConversion.BgrToHsv,ColorVariation.Skin);

                colorImage.Copy(morImage);
                for (int i = 3; i < 5; i++)
                {
                    // Morphology演算 クロージング及びオープニング
                    Morphology.Operation(morImage, morImage, MorphologyOperation.Open, i, i);
                    Morphology.Operation(morImage, morImage, MorphologyOperation.Close, i, i);
                }

                Cv.ShowImage("CAMERA", cameraImage);
                Cv.ShowImage("EXTRACTION", colorImage);
                Cv.ShowImage("MORPHOLOGY", morImage);
            }
            capture.Dispose();
            cameraImage.Dispose();
            colorImage.Dispose();
            morImage.Dispose();
        }
    }
}