using System;
using System.Collections.Generic;
using System.Linq;
using EdgeDetaction.Core.Repasitorys.Interfaces;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using EdgeDetaction.Core.Repasitorys.Enums;
using System.Runtime.InteropServices;

namespace EdgeDetaction.Core.Repasitorys.Implemantation
{
    class Image : Base, IImage
    {
        public Image() : base()
        {

        }

        public double[,] ConvertImageToMatrix(string bitmapFilePath, CompTypes component, ref int width, ref int hight)
        {
            Bitmap b1 = new Bitmap(bitmapFilePath);
            hight = b1.Height;
            width = b1.Width;
            double[,] colorMatrix = new double[width, hight];

            switch (component)
            {
                case CompTypes.R:
                    for (int j = 0; j < hight; j++)
                    {
                        for (int i = 0; i < width; i++)
                        {
                            colorMatrix[i, j] = b1.GetPixel(i, j).R;
                        }
                    }
                    break;
                case CompTypes.G:
                    for (int j = 0; j < hight; j++)
                    {
                        for (int i = 0; i < width; i++)
                        {
                            colorMatrix[i, j] = b1.GetPixel(i, j).G;
                        }
                    }
                    break;
                case CompTypes.B:
                    for (int j = 0; j < hight; j++)
                    {
                        for (int i = 0; i < width; i++)
                        {
                            colorMatrix[i, j] = b1.GetPixel(i, j).B;
                        }
                    }
                    break;

                default:
                    for (int j = 0; j < hight; j++)
                    {
                        for (int i = 0; i < width; i++)
                        {
                            colorMatrix[i, j] = b1.GetPixel(i, j).R;
                        }
                    }
                    break;
            }

            return colorMatrix;

        }
     
        public byte[,] ConvertImageToMatrixByte(string bitmapFilePath, CompTypes component, ref int width, ref int hight)
        {
            Bitmap b1 = new Bitmap(bitmapFilePath);
            hight = b1.Height;
            width = b1.Width;
            byte[,] colorMatrix = new byte[width, hight];

            switch (component)
            {
                case CompTypes.R:
                    for (int j = 0; j < hight; j++)
                    {
                        for (int i = 0; i < width; i++)
                        {
                            colorMatrix[i, j] = b1.GetPixel(i, j).R;
                        }
                    }
                    break;
                case CompTypes.G:
                    for (int j = 0; j < hight; j++)
                    {
                        for (int i = 0; i < width; i++)
                        {
                            colorMatrix[i, j] = b1.GetPixel(i, j).G;
                        }
                    }
                    break;
                case CompTypes.B:
                    for (int j = 0; j < hight; j++)
                    {
                        for (int i = 0; i < width; i++)
                        {
                            colorMatrix[i, j] = b1.GetPixel(i, j).B;
                        }
                    }
                    break;

                default:
                    for (int j = 0; j < hight; j++)
                    {
                        for (int i = 0; i < width; i++)
                        {
                            colorMatrix[i, j] = b1.GetPixel(i, j).R;
                        }
                    }
                    break;
            }

            return colorMatrix;

        }
        public byte[] ConvertMatreixToArray(byte[,] matrix)
        {
            byte[] arr = new byte[matrix.GetLength(0) * matrix.GetLength(1)];
            int k = 0;
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    arr[k++] = matrix[i, j];
                }
            }

            return arr;
        }
        public byte[,] ConvertArrayToMatrix(byte[] array, int width, int hight)
        {
            byte[,] matrix = new byte[width, hight];
            int k = 0;
            for (int j = 0; j < hight; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    matrix[i, j] = array[k++];
                }
            }
            return matrix;
        }
        public double[,] ConvertArrayToMatrixDoubles(byte[] array, int width, int hight)
        {
            double[,] matrix = new double[width, hight];
            int k = 0;
            for (int j = 0; j < hight; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    matrix[i, j] = array[k++];
                }
            }
            return matrix;
        }
        public double[,] ConvertArrayToMatrixDouble(double[] array, int width, int hight)
        {
            double[,] matrix = new double[width, hight];
            int k = 0;
            for (int j = 0; j < hight; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    matrix[i, j] = array[k++];
                }
            }
            return matrix;
        }

        public byte[,] SobelHOperation(byte[,] matrix)
        {
            byte[,] SobelHMatrix = new byte[matrix.GetLength(0), matrix.GetLength(1)];
            dynamic elementSobel = 0;
            int[,] SobelH = new int[3, 3] { { -1, -2, -1 },

                { 0, 0, 0 },

                { 1, 2, 1 }

            };

            for (int j = 0; j < matrix.GetLength(1) - 2; j++)
            {
                for (int i = 0; i < matrix.GetLength(0) - 2; i++)
                {
                    elementSobel = matrix[i, j] * SobelH[0, 0] + matrix[i, j + 2] * SobelH[0, 2]

                                   + matrix[i + 1, j] * SobelH[1, 0] + matrix[i + 1, j + 2] * SobelH[1, 2]

                                   + matrix[i + 2, j] * SobelH[2, 0] + matrix[i + 2, j + 2] * SobelH[2, 2];



                    if (elementSobel < 0)

                        elementSobel = 0;

                    if (elementSobel > 255)

                        elementSobel = 255;

                    SobelHMatrix[i + 1, j + 1] = Convert.ToByte(elementSobel);

                }

            }
            return SobelHMatrix;
        }
        public double[,] SobelHOperationD(double[,] matrix)
        {
            var w = matrix.GetLength(0);
            var l = matrix.GetLength(1);
            double[,] sobelHMatrix = new double[w,l];
            double[,] sobelH = new double[3, 3]
            { 
                { -1, -2, -1 },

                { 0, 0, 0 },

                { 1, 2, 1 }

            };

            for (int a = 0; a < l - 2; a++)//ijnox
            {
                for (int i = 0; i < w - 2; i++)//qaylox
                {
                    double[,] subMatrix = GetMatrixPart(matrix, i, a);
                    double element = CalculateMultiple(subMatrix, sobelH);
                    sobelHMatrix[i + 1, a + 1] = element;//sobelHMatrix[a + 1, i + 1]
                }
            }
            return sobelHMatrix;
        }
        public double[,] SobelHOperationDoubleForVisualization(double[,] matrix)
        {
            var w = matrix.GetLength(0);
            var l = matrix.GetLength(1);
            double[,] sobelHMatrix = new double[w, l];
            double[,] sobelH = new double[3, 3]
            {
                { -1, -2, -1 },

                { 0, 0, 0 },

                { 1, 2, 1 }

            };

            for (int a = 0; a < l - 2; a++)//ijnox
            {
                for (int i = 0; i < w - 2; i++)//qaylox
                {
                    double[,] subMatrix = GetMatrixPart(matrix, i, a);
                    double element = CalculateMultiple(subMatrix, sobelH);
                    sobelHMatrix[i + 1, a + 1] = element;
                }
            }
            return sobelHMatrix;
        }
        public byte[,] SobelVOperation(byte[,] matrix)
        {
            byte[,] sobelVMatrix = new byte[matrix.GetLength(0), matrix.GetLength(1)];
            dynamic elementSobel = 0;
            int[,] SobelV = new int[3, 3] { { 1, 0, -1 },

                { 2, 0, -2 },

                { 1, 0, -1 }

            };

            for (int j = 0; j < matrix.GetLength(1) - 2; j++)
            {
                for (int i = 0; i < matrix.GetLength(0) - 2; i++)
                {
                    elementSobel = matrix[i, j] * SobelV[0, 0] + matrix[i, j + 2] * SobelV[0, 2]

                                   + matrix[i + 1, j] * SobelV[1, 0] + matrix[i + 1, j + 2] * SobelV[1, 2]

                                   + matrix[i + 2, j] * SobelV[2, 0] + matrix[i + 2, j + 2] * SobelV[2, 2];



                    if (elementSobel < 0)

                        elementSobel = 0;

                    if (elementSobel > 255)

                        elementSobel = 255;

                    sobelVMatrix[i + 1, j + 1] = Convert.ToByte(elementSobel); ;

                }

            }
            return sobelVMatrix;
        }
        public double[,] SobelVOperationD(double[,] matrix)
        {
            var w = matrix.GetLength(0);
            var l = matrix.GetLength(1);
            double[,] sobelVMatrix = new double[w, l];//[l,w]
            double[,] sobelV = new double[3, 3] {
            { -1, 0, 1 },
            { -2, 0, 2 },
            { -1, 0, 1 }

            };
            for (int a = 0; a < l - 2; a++)//ijnox
            {
                for (int i = 0; i < w - 2; i++)//qaylox
                {
                    double[,] subMatrix = GetMatrixPart(matrix, i, a);
                    double element = CalculateMultiple(subMatrix, sobelV);
                    sobelVMatrix[i+1, a + 1] = element;//sobelVMatrix[a+1, i + 1] 
                }
            }
            return sobelVMatrix;
        }
        public double[,] SobelVOperationDForVisualization(double[,] matrix)
        {
            var w = matrix.GetLength(0);
            var l = matrix.GetLength(1);
            double[,] sobelVMatrix = new double[w, l];
            double[,] sobelV = new double[3, 3] {
                { -1, 0, 1 },
                { -2, 0, 2 },
                { -1, 0, 1 }

            };
            for (int a = 0; a < l - 2; a++)//ijnox
            {
                for (int i = 0; i < w - 2; i++)//qaylox
                {
                    double[,] subMatrix = GetMatrixPart(matrix, i, a);
                    double element = CalculateMultiple(subMatrix, sobelV);
                    sobelVMatrix[i + 1, a + 1] = element;
                }
            }
            return sobelVMatrix;
        }
        private double CalculateMultiple(double[,] subMatrix, double[,] sobelV)
        {
            var sum = 0d;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    sum += subMatrix[i, j] * sobelV[i, j];
                }
            }

            return sum;
        }

        private double[,] GetMatrixPart(double[,] matrix, int currentPosition, int k)
        {
            var findedMatrix = new double[3, 3];
            findedMatrix[0, 0] = matrix[currentPosition, k];
            findedMatrix[0, 1] = matrix[currentPosition, k + 1];
            findedMatrix[0, 2] = matrix[currentPosition, k + 2];
            findedMatrix[1, 0] = matrix[currentPosition + 1, k];
            findedMatrix[1, 1] = matrix[currentPosition + 1, k + 1];
            findedMatrix[1, 2] = matrix[currentPosition + 1, k + 2];
            findedMatrix[2, 0] = matrix[currentPosition + 2, k];
            findedMatrix[2, 1] = matrix[currentPosition + 2, k + 1];
            findedMatrix[2, 2] = matrix[currentPosition + 2, k + 2];
            return findedMatrix;

        }

        public double[,] MakeMatrix2SizeLess(double[,] matrix)
        {
            double [,] matrixResult = new double[matrix.GetLength(0)-2,matrix.GetLength(1)-2];

            for (int j = 1; j < matrix.GetLength(1) - 1; j++)
            {
                for (int i = 1; i < matrix.GetLength(0) - 1; i++)
                {
                    matrixResult[i - 1, j - 1] = matrix[i, j];
                }
            }

            return matrixResult;
        }
        public double[,] MagnitudeFromDefoultMatrix(double[,] matrix)
        {
            double[,] sobelHMatrix = SobelHOperationD(matrix);//MakeMatrix2SizeLess(SobelHOperationD(matrix));

            double[,] SobelVMatrix = SobelVOperationD(matrix);//MakeMatrix2SizeLess(SobelVOperationD(matrix));
           
          
            double elementSobel = 0;
            double[,] CombineSobelMatrix = new double[sobelHMatrix.GetLength(0), sobelHMatrix.GetLength(1)];
            //int k = 0;
            //int m = 0;
            for (int j = 0; j < sobelHMatrix.GetLength(1); j++)
            {
                for (int i = 0; i < sobelHMatrix.GetLength(0); i++)

                {
                    elementSobel = Math.Sqrt(Math.Pow(SobelVMatrix[i, j], 2) +

                                             Math.Pow(sobelHMatrix[i, j], 2));
                    if (Convert.ToInt32(elementSobel) == 0)
                    {
                        CombineSobelMatrix[i, j] = matrix[i, j];
                    }
                    else
                    {
                        CombineSobelMatrix[i, j] = elementSobel;
                    }
                    //m++;
                }

                //m = 0;
                //k++;

            }
            return CombineSobelMatrix;

        }
        public double[,] MagnitudeFromDefoultMatrixForVisualization(double[,] matrix)
        {
            double[,] sobelHMatrix = SobelHOperationDoubleForVisualization(matrix);

            double[,] SobelVMatrix = SobelVOperationDForVisualization(matrix);


            double elementSobel = 0;
            double[,] CombineSobelMatrix = new double[sobelHMatrix.GetLength(0) - 2, sobelHMatrix.GetLength(1) - 2];
            int k = 0;
            int m = 0;
            for (int j = 1; j < sobelHMatrix.GetLength(1) - 1; j++)
            {
                for (int i = 1; i < sobelHMatrix.GetLength(0) - 1; i++)

                {
                    elementSobel = Math.Sqrt(Math.Pow(SobelVMatrix[i, j], 2) +

                                             Math.Pow(sobelHMatrix[i, j], 2));

                    CombineSobelMatrix[m, k] = elementSobel;
                    m++;
                }

                m = 0;
                k++;

            }



            return CombineSobelMatrix;

        }
        public byte[,] MagnitudeNew(byte[,] SobelVMatrix, byte[,] SobelHMatrix)
        {
            byte[,] CombineSobelMatrix = new byte[SobelHMatrix.GetLength(0) - 2, SobelHMatrix.GetLength(1) - 2];
            int k = 0;
            int m = 0;
            for (int j = 1; j < SobelHMatrix.GetLength(1) - 1; j++)
            {
                for (int i = 1; i < SobelHMatrix.GetLength(0) - 1; i++)

                {
                    CombineSobelMatrix[m, k] = Math.Min((byte)Math.Sqrt(Math.Pow(SobelVMatrix[i, j], 2) +

                                                                        Math.Pow(SobelHMatrix[i, j], 2)), (byte)255);

                    m++;
                }

                m = 0;
                k++;

            }
            return CombineSobelMatrix;

        }
        public double[,] MagnitudeDouble(double[,] SobelVMatrix, double[,] SobelHMatrix)
        {
            double[,] CombineSobelMatrix = new Double[SobelHMatrix.GetLength(0) - 2, SobelHMatrix.GetLength(1) - 2];
            int k = 0;
            int m = 0;
            for (int j = 1; j < SobelHMatrix.GetLength(1) - 1; j++)
            {
                for (int i = 1; i < SobelHMatrix.GetLength(0) - 1; i++)

                {
                    CombineSobelMatrix[m, k] = Math.Sqrt(Math.Pow(SobelVMatrix[i, j], 2) +

                                                                        Math.Pow(SobelHMatrix[i, j], 2));

                    m++;
                }

                m = 0;
                k++;

            }
            return CombineSobelMatrix;

        }
        public decimal EstimationDouble(double[,] magnitudeMatrix)
        {
            double myu = 0;
            double sigmaPow2 = 0;
            double sum = 0;
            double sum1 = 0;
            int zeroCount = 0;
            for (int j = 0; j < magnitudeMatrix.GetLength(1); j++)
            {

                for (int i = 0; i < magnitudeMatrix.GetLength(0); i++)

                {
                    if (Convert.ToInt32(magnitudeMatrix[i, j]) == 0)
                    {
                        zeroCount++;
                    }
                    else
                    {
                        sum += magnitudeMatrix[i, j];

                    }

                }

            }
            myu = sum / ((magnitudeMatrix.GetLength(0) * magnitudeMatrix.GetLength(1))-zeroCount);
            var myuPow2 = Math.Pow(myu, 2);
            for (int j = 0; j < magnitudeMatrix.GetLength(1); j++)
            {

                for (int i = 0; i < magnitudeMatrix.GetLength(0); i++)

                {
                    if (Convert.ToInt32(magnitudeMatrix[i, j]) != 0)
                    {
                        sum1 += Math.Pow(magnitudeMatrix[i, j] - myu, 2);

                    }
                }

            }

            sigmaPow2 = sum1 / ((magnitudeMatrix.GetLength(0) * magnitudeMatrix.GetLength(1))-zeroCount);

            try
            {
                return Convert.ToDecimal(Math.Round((sigmaPow2 / myuPow2), 5));

            }
            catch (Exception)
            {
                return 0;
            }
            
        }
        public decimal Estimation(byte[,] magnitudeMatrix)
        {
            double myu = 0;
            double sigmaPow2 = 0;
            double sum = 0;
            double sum1 = 0;
            int zeroCount = 0;
            for (int j = 0; j < magnitudeMatrix.GetLength(1); j++)
            {

                for (int i = 0; i < magnitudeMatrix.GetLength(0); i++)

                {
                    //if (magnitudeMatrix[i, j]==0)
                    //{
                    //    zeroCount++;
                    //}
                    //else
                    {
                        sum += magnitudeMatrix[i, j];

                    }

                }

            }
            myu = sum / ((magnitudeMatrix.GetLength(0) * magnitudeMatrix.GetLength(1)));
            var myuPow2 = Math.Pow(myu, 2);
            for (int j = 0; j < magnitudeMatrix.GetLength(1); j++)
            {

                for (int i = 0; i < magnitudeMatrix.GetLength(0); i++)

                {
                    if (magnitudeMatrix[i, j] != 0)
                    {
                        sum1 += Math.Pow(magnitudeMatrix[i, j] - myu, 2);

                    }
                }

            }

            sigmaPow2 = sum1 / ((magnitudeMatrix.GetLength(0) * magnitudeMatrix.GetLength(1)));

            return Convert.ToDecimal(Math.Round((sigmaPow2 / myuPow2), 5));
        }
        public Bitmap ConvertArrayToImage(double[] imageData, int width, int height)
        {
            Bitmap Bm = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            var b = new Bitmap(width, height, PixelFormat.Format8bppIndexed);

            ColorPalette ncp = b.Palette;

            for (int i = 0; i < 256; i++)

                ncp.Entries[i] = Color.FromArgb(255, i, i, i);

            b.Palette = ncp;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int Value = Convert.ToInt32(imageData[x + (y * width)]);
                    if (Value < 0)
                    {
                        Value = 0;
                    }
                    Color C = ncp.Entries[Value];
                    Bm.SetPixel(x, y, C);

                }
            }

            return PaintOn32Abpp(Bm);
        }
        /// <summary>
        /// Turning int array into BMP
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Bitmap FromTwoDimIntArrayGray(int[,] data)
        {
            // Transform 2-dimensional Int32 array to 1-byte-per-pixel byte array
            int width = data.GetLength(0);
            int height = data.GetLength(1);
            int byteIndex = 0;
            Byte[] dataBytes = new Byte[height * width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // logical AND to be 100% sure the int32 value fits inside
                    // the byte even if it contains more data (like, full ARGB).
                    dataBytes[byteIndex] = (Byte)(((UInt32)data[x, y]) & 0xFF);
                    // More efficient than multiplying
                    byteIndex++;
                }
            }
            // generate palette
            Color[] palette = new Color[256];
            for (int b = 0; b < 256; b++)
                palette[b] = Color.FromArgb(b, b, b);
            // Build image
            return BuildImage(dataBytes, width, height, width, PixelFormat.Format8bppIndexed, palette, null);
        }
        /// <summary>
        /// Creates a bitmap based on data, width, height, stride and pixel format.
        /// </summary>
        /// <param name="sourceData">Byte array of raw source data</param>
        /// <param name="width">Width of the image</param>
        /// <param name="height">Height of the image</param>
        /// <param name="stride">Scanline length inside the data</param>
        /// <param name="pixelFormat">Pixel format</param>
        /// <param name="palette">Color palette</param>
        /// <param name="defaultColor">Default color to fill in on the palette if the given colors don't fully fill it.</param>
        /// <returns>The new image</returns>
        public  Bitmap BuildImage(Byte[] sourceData, Int32 width, Int32 height, Int32 stride, PixelFormat pixelFormat, Color[] palette, Color? defaultColor)
        {
            Bitmap newImage = new Bitmap(width, height, pixelFormat);
            BitmapData targetData = newImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, newImage.PixelFormat);
            Int32 newDataWidth = ((System.Drawing.Image.GetPixelFormatSize(pixelFormat) * width) + 7) / 8;
            // Compensate for possible negative stride on BMP format.
            Boolean isFlipped = stride < 0;
            stride = Math.Abs(stride);
            // Cache these to avoid unnecessary getter calls.
            Int32 targetStride = targetData.Stride;
            Int64 scan0 = targetData.Scan0.ToInt64();
            for (Int32 y = 0; y < height; y++)
                Marshal.Copy(sourceData, y * stride, new IntPtr(scan0 + y * targetStride), newDataWidth);
            newImage.UnlockBits(targetData);
            // Fix negative stride on BMP format.
            if (isFlipped)
                newImage.RotateFlip(RotateFlipType.Rotate180FlipX);
            // For indexed images, set the palette.
            if ((pixelFormat & PixelFormat.Indexed) != 0 && palette != null)
            {
                ColorPalette pal = newImage.Palette;
                for (Int32 i = 0; i < pal.Entries.Length; i++)
                {
                    if (i < palette.Length)
                        pal.Entries[i] = palette[i];
                    else if (defaultColor.HasValue)
                        pal.Entries[i] = defaultColor.Value;
                    else
                        break;
                }
                newImage.Palette = pal;
            }

            return newImage;//PaintOn32Abpp(newImage);
        }
        public Bitmap ConvertArrayToImage(int[] imageData, int width, int height)
        {
            Bitmap Bm = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            var b = new Bitmap(width, height, PixelFormat.Format8bppIndexed);

            ColorPalette ncp = b.Palette;

            for (int i = 0; i < 256; i++) {
                ncp.Entries[i] = Color.FromArgb(255, i, i, i);
            }

            b.Palette = ncp;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int Value = imageData[x + (y * width)];
                    Color C = ncp.Entries[Value];
                    Bm.SetPixel(x, y, C);

                }
            }

            return Bm;// PaintOn32Abpp(Bm);
        }
        public static Bitmap PaintOn32Abpp(Bitmap image)
        {
            Bitmap bp = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);
            using (Graphics gr = Graphics.FromImage(bp))
                gr.DrawImage(image, new Rectangle(0, 0, bp.Width, bp.Height));
            return bp;
        }
        
        public static Bitmap PaintOn32bpp(Bitmap image, Color? transparencyFillColor)
        {
            Bitmap bp = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);
            using (Graphics gr = Graphics.FromImage(bp))
            {
                if (transparencyFillColor.HasValue)
                    using (System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(Color.FromArgb(255, transparencyFillColor.Value)))
                        gr.FillRectangle(myBrush, new Rectangle(0, 0, image.Width, image.Height));
                gr.DrawImage(image, new Rectangle(0, 0, bp.Width, bp.Height));
            }
            return bp;
        }
        public static Bitmap ToGrayscale(Bitmap bmp)
        {
            var result = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format8bppIndexed);

            BitmapData data = result.LockBits(new Rectangle(0, 0, result.Width, result.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            // Copy the bytes from the image into a byte array
            byte[] bytes = new byte[data.Height * data.Stride];
            Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    var c = bmp.GetPixel(x, y);
                    var rgb = (byte)((c.R + c.G + c.B) / 3);

                    bytes[x * data.Stride + y] = rgb;
                }
            }

            // Copy the bytes from the byte array into the image
            Marshal.Copy(bytes, 0, data.Scan0, bytes.Length);

            result.UnlockBits(data);

            return result;
        }
        public Bitmap ConvertArrayToImageForSaveLocal(byte[] imageData, int width, int height)
        {
            Bitmap Bm = new Bitmap(width, height, PixelFormat.Format8bppIndexed);

            var b = new Bitmap(width, height, PixelFormat.Format8bppIndexed);

            ColorPalette ncp = b.Palette;

            for (int i = 0; i < 256; i++)

                ncp.Entries[i] = Color.FromArgb(255, i, i, i);

            b.Palette = ncp;

            var BoundsRect = new Rectangle(0, 0, width, height);

            BitmapData bmpData = b.LockBits(BoundsRect,
                                            ImageLockMode.WriteOnly,
                                            PixelFormat.Format8bppIndexed);

            IntPtr ptr = bmpData.Scan0;
            int bytes = bmpData.Stride * b.Height;
            var rgbValues = new byte[bytes];
            Marshal.Copy(imageData, 0, ptr, bytes);

            b.UnlockBits(bmpData);
            Console.WriteLine(b.GetPixel(3648, 1145).ToString());

            return b;
        }
        public double[] ConvertMatreixToArrayDouble(double[,] matrix)
        {
            double[] arr = new double[matrix.GetLength(0) * matrix.GetLength(1)];
            int k = 0;
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    arr[k++] = matrix[i, j];
                }
            }

            return arr;
        }
        public byte[] ConvertMatreixToArrayint(int[,] matrix)
        {
            byte[] arr = new Byte[matrix.GetLength(0) * matrix.GetLength(1)];
            int k = 0;
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    arr[k++] = Convert.ToByte(matrix[i, j]);
                }
            }

            return arr;
        }
        public int[,] NormalizeMatrix(double[,] matrix)
        {
            int[,] normalizedMatrixBytes = new int[matrix.GetLength(0) , matrix.GetLength(1)];
            double max = matrix[0, 0];
            double min = 1;//matrix[0, 0];

            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    if (matrix[i, j] > max )
                    {
                        max = matrix[i, j];
                    }
                    if (matrix[i, j] < min)
                    {
                        min = matrix[i, j];
                    }
                }
            }

            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    normalizedMatrixBytes[i,j] = Convert.ToInt32(((matrix[i, j] - min) / (max - min)) * 255);
                }
            }

            return normalizedMatrixBytes;
        }
        public byte[,] NormalizeMatrixByte(double[,] matrix)
        {
            byte[,] normalizedMatrixBytes = new byte[matrix.GetLength(0), matrix.GetLength(1)];
            double max = matrix[0, 0];
            double min = matrix[0, 0];

            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    if (matrix[i, j] > max)
                    {
                        max = matrix[i, j];
                    }
                    if (matrix[i, j] < min)
                    {
                        min = matrix[i, j];
                    }
                }
            }

            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    normalizedMatrixBytes[i, j] = Convert.ToByte(((matrix[i, j] - min) / (max - min))*255);
                }
            }

            return normalizedMatrixBytes;
        }

        public byte[,] Normalize(double[,] matrix)
        {
            byte[,] normalizedMatrixBytes = new byte[matrix.GetLength(0), matrix.GetLength(1)];

            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    if (matrix[i,j] < 0)

                        matrix[i, j] = 0;

                    if (matrix[i, j] > 255)

                        matrix[i, j] = 255;

                    normalizedMatrixBytes[i, j] = Convert.ToByte(matrix[i, j]);
                }

            }

            return normalizedMatrixBytes;
        }
        public int[,] NormalizeInt(double[,] matrix)
        {
            int[,] normalizedMatrixBytes = new int[matrix.GetLength(0), matrix.GetLength(1)];

            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    if (matrix[i, j] < 0)

                        matrix[i, j] = 0;

                    if (matrix[i, j] > 255)

                        matrix[i, j] = -255;

                    normalizedMatrixBytes[i, j] =Convert.ToInt32(matrix[i, j]);
                }

            }

            return normalizedMatrixBytes;
        }
        public byte[,] Normalize(byte[,] matrix)
        {
            byte[,] normalizedMatrixBytes = new byte[matrix.GetLength(0), matrix.GetLength(1)];

            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    if (matrix[i, j] < 0)

                        matrix[i, j] = 0;

                    if (matrix[i, j] > 255)

                        matrix[i, j] = 255;

                    normalizedMatrixBytes[i, j] = Convert.ToByte(matrix[i, j]);
                }

            }

            return normalizedMatrixBytes;
        }

        public double[,] CalcBlurMapDouble(double[,] matrix)
        {
            var w = matrix.GetLength(0);
            var l = matrix.GetLength(1);
            double[,] calcBlurM = new double[w, l];//new double[l, w];
            //double[,] sobelH = new double[3, 3]
            //{
            //    { -1, -2, -1 },

            //    { 0, 0, 0 },

            //    { 1, 2, 1 }

            //};
            //double[,] sobelV = new double[3, 3] {
            //    { -1, 0, 1 },
            //    { -2, 0, 2 },
            //    { -1, 0, 1 }

            //};
            for (int a = 0; a < l - 2; a++)//ijnox
            {
                for (int i = 0; i < w - 2; i++)//qaylox
                {
                    double[,] subMatrix = GetMatrixPart(matrix, i, a);
                    //double elementH = CalculateMultiple(subMatrix, sobelH);
                    //double elementV = CalculateMultiple(subMatrix, sobelV);

                    decimal elementEst = EstimationDouble(subMatrix);

                    if (Convert.ToInt32(elementEst) == 0)//|| elementEst < 0
                    {
                        calcBlurM[i + 1, a + 1] = matrix[i+1, a+1]; //  calcBlurM[a + 1, i + 1] =0
                    }
                    else
                    {
                        calcBlurM[i + 1, a + 1] = Convert.ToDouble(1 / elementEst);// or visevere// calcBlurM[a + 1, i + 1] 
                    }
                }
            }

            return MakeMatrix2SizeLess(calcBlurM);


        }
        public double[,] CalcBlurMapDoubleForEach3(double[,] matrix)
        {
            var w = matrix.GetLength(0);
            var l = matrix.GetLength(1);
            double[,] calcBlurM = new double[l, w];
            double[,] sobelH = new double[3, 3]
            {
                { -1, -2, -1 },

                { 0, 0, 0 },

                { 1, 2, 1 }

            };
            double[,] sobelV = new double[3, 3] {
                { -1, 0, 1 },
                { -2, 0, 2 },
                { -1, 0, 1 }

            };
            for (int a = 0; a < l - 2; a++)//ijnox
            {
                for (int i = 0; i < w - 2; i++)//qaylox
                {
                    double[,] subMatrix = GetMatrixPart(matrix, i, a);
                    //double elementH = CalculateMultiple(subMatrix, sobelH);
                    //double elementV = CalculateMultiple(subMatrix, sobelV);

                    decimal elementEst = EstimationDouble(subMatrix);
                    if (elementEst == 0)
                    {
                        calcBlurM[a + 1, i + 1] = 0;
                    }
                    else
                    {
                        calcBlurM[a + 1, i + 1] = Convert.ToDouble(1 / elementEst);
                    }
                    i += 3;
                }
                a += 3;
            }
            return calcBlurM;


        }
        public byte[,] CalcBlurMap(byte[,] matrix)
        {
            throw new NotImplementedException();
        }

        public Bitmap ConvertArrayToImage(byte[] imageData, int width, int height)
        {
            Bitmap Bm = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            var b = new Bitmap(width, height, PixelFormat.Format8bppIndexed);

            ColorPalette ncp = b.Palette;

            for (int i = 0; i < 256; i++)

                ncp.Entries[i] = Color.FromArgb(255, i, i, i);

            b.Palette = ncp;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int Value = imageData[x + (y * width)];
                    Color C = ncp.Entries[Value];
                    Bm.SetPixel(x, y, C);

                }
            }

            return PaintOn32Abpp(Bm);
        }
    }
}
