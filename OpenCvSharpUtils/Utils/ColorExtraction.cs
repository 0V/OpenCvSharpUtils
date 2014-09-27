using OpenCvSharp;
using System;

namespace OpenCvSharpUtils
{
    /// <summary>
    /// 抽出する色
    /// </summary>
    public enum ColorVariation
    {
        Skin,
        Red,
        Green,
    }

    /// <summary>
    /// 色領域の抽出を行います。
    /// 新しい定義済み抽出ルールを追加したいときは ColorVariation に色名を追加し、ColorExtract メソッド内で分岐して ColorTable のコンストラクタに引数を指定してやればOK
    /// 詳しくは実装を参照してください。
    /// </summary>
    public class ColorExtraction
    {
        public struct ColorTable
        {
            public ColorTable(int ch1Lower, int ch1Upper, int ch2Lower, int ch2Upper, int ch3Lower, int ch3Upper)
            {
                this.ch1Low = ch1Lower;
                this.ch1Up = ch1Upper;
                this.ch2Low = ch2Lower;
                this.ch2Up = ch2Upper;
                this.ch3Low = ch3Lower;
                this.ch3Up = ch3Upper;
            }
            public int ch1Low, ch1Up, ch2Low, ch2Up, ch3Low, ch3Up;
        }

        
        public static void Extract(IplImage srcImage, IplImage dstImage, ColorConversion code, ColorVariation color)
        {
            ColorTable table;

            switch (color)
            {
                case ColorVariation.Green:
                    table = new ColorTable(50, 70, 80, 255, 0, 255);
                    break;
                case ColorVariation.Red:
                    table = new ColorTable(170, 10, 80, 255, 0, 255);;
                    break;
                case ColorVariation.Skin:
                    table = new ColorTable(0, 10, 80, 255, 0, 255);;
                    break;
                default:
                    table = new ColorTable(0, 0, 0, 0, 0, 0);
                    break;
            }


            Extract(
                srcImage,
                dstImage,
                code,
                table.ch1Low, table.ch1Up,
                table.ch2Low, table.ch2Up,
                table.ch3Low, table.ch3Up
                );

        }


        //　マスクのかぶせ方をあらかじめ組み込んでおく
        public static void Extract(IplImage srcImage, IplImage dstImage, ColorConversion code, ColorTable table)
        {
            Extract(
                srcImage,
                dstImage,
                code,
                table.ch1Low, table.ch1Up,
                table.ch2Low, table.ch2Up,
                table.ch3Low, table.ch3Up
                );
        }

        /// <summary>
        /// *Angle : 0~360 °
        /// *Per : 0~100 % 
        /// </summary>
        public static void ExtractRegularFormat(IplImage srcImage, IplImage dstImage, ColorConversion code,
        int ch1LowerAngle, int ch1UpperAngle,
        int ch2LowerPer, int ch2UpperPer,
        int ch3LowerPer, int ch3UpperPer)
        {
            Extract(
                srcImage,
                dstImage,
                code,
                ch1LowerAngle / 2,
                ch1UpperAngle / 2,
                ch2LowerPer * 255 / 100,
                ch2UpperPer * 255 / 100,
                ch3LowerPer * 255 / 100,
                ch3UpperPer * 255 / 100
                );
        }

        public static void Extract(IplImage srcImage, IplImage dstImage, ColorConversion code,
        int ch1Lower, int ch1Upper,
        int ch2Lower, int ch2Upper,
        int ch3Lower, int ch3Upper)
        {
            IplImage colorImage;
            IplImage ch1Image, ch2Image, ch3Image;
            IplImage maskImage;

            int i, k;
            int[] lower = new int[3];
            int[] upper = new int[3];
            int[] val = new int[3];

            CvMat lut;

            if (srcImage == null)
                Console.WriteLine("src");
            else if (dstImage == null)
                Console.WriteLine("dst");


            colorImage = Cv.CreateImage(Cv.GetSize(srcImage), srcImage.Depth, srcImage.NChannels);
            Cv.CvtColor(srcImage, colorImage, code);

            lut = Cv.CreateMat(256, 1, MatrixType.U8C3);

            lower[0] = ch1Lower;
            lower[1] = ch2Lower;
            lower[2] = ch3Lower;

            upper[0] = ch1Upper;
            upper[1] = ch2Upper;
            upper[2] = ch3Upper;

            for (i = 0; i < 256; i++)
            {
                for (k = 0; k < 3; k++)
                {
                    if (lower[k] <= upper[k])
                    {
                        if ((lower[k] <= i) && (i <= upper[k]))
                        {
                            val[k] = 255;
                        }
                        else
                        {
                            val[k] = 0;
                        }
                    }
                    else
                    {
                        if ((i <= upper[k]) || (lower[k] <= i))
                        {
                            val[k] = 255;
                        }
                        else
                        {
                            val[k] = 0;
                        }
                    }
                }
                Cv.Set1D(lut, i, new CvScalar(val[0], val[1], val[2]));
            }

            Cv.LUT(colorImage, colorImage, lut);
            Cv.ReleaseMat(lut);

            ch1Image = Cv.CreateImage(Cv.GetSize(colorImage), colorImage.Depth, 1);
            ch2Image = Cv.CreateImage(Cv.GetSize(colorImage), colorImage.Depth, 1);
            ch3Image = Cv.CreateImage(Cv.GetSize(colorImage), colorImage.Depth, 1);

            Cv.Split(colorImage, ch1Image, ch2Image, ch3Image, null);

            maskImage = Cv.CreateImage(Cv.GetSize(colorImage), colorImage.Depth, 1);
            Cv.And(ch1Image, ch2Image, maskImage);
            Cv.And(maskImage, ch3Image, maskImage);

            Cv.Zero(dstImage);
            Cv.Copy(srcImage, dstImage, maskImage);

            Cv.ReleaseImage(colorImage);
            Cv.ReleaseImage(ch1Image);
            Cv.ReleaseImage(ch2Image);
            Cv.ReleaseImage(ch3Image);
            Cv.ReleaseImage(maskImage);
        }
    }
}
