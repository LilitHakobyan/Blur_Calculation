using System;
using System.Collections.Generic;
using System.Linq;
using EdgeDetaction.Core.Repasitorys.Interfaces;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.ComponentModel;

namespace EdgeDetaction.Core.Repasitorys.Implemantation
{
    class Image: Base, IImage
    {
        public Image( ) : base()
        {

        }  
       
        public byte[,] ConvertImageToMatrix(string bitmapFilePath, string component,ref int width,ref int hight)
        {
            Bitmap b1 = new Bitmap(bitmapFilePath);
             hight = b1.Height;
             width = b1.Width;
            byte[,] colorMatrix = new byte[width, hight];

            switch (component.ToUpper())
            {
                case "R":
                    for (int j = 0; j < hight; j++)
                    {
                        for (int i = 0; i < width; i++)
                        {
                            colorMatrix[i, j] = b1.GetPixel(i, j).R;
                        }
                    }
                    break;
                case "G":
                    for (int j = 0; j < hight; j++)
                    {
                        for (int i = 0; i < width; i++)
                        {
                            colorMatrix[i, j] = b1.GetPixel(i, j).G;
                        }
                    }
                    break;
                case "B ":
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
        public byte [] ConvertMatreixToArray(byte [,] matrix)
        {
            byte[] arr = new byte[matrix.GetLength(0) * matrix.GetLength(1)];
            int i = 0;
            foreach (var item in matrix)
            {
                arr[i++] = item;
            }
            return arr;
        }
        public byte[,] ConvertArrayToMatrix(byte[] array,int width, int hight)
        {
            byte[,] matrix = new byte[width,hight];
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
            byte[,] SobelHMatrix = new byte[matrix.GetLength(0) , matrix.GetLength(1)];
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

                    SobelHMatrix[i + 1, j + 1] =Convert.ToByte(elementSobel);

                }

            }
            return SobelHMatrix;
        }

        public byte[,] SobelVOperation(byte[,] matrix)
        {
            byte[,] SobelVMatrix = new byte[matrix.GetLength(0), matrix.GetLength(1)];
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

                    SobelVMatrix[i + 1, j + 1] = Convert.ToByte(elementSobel); ;

                }

            }
            return SobelVMatrix;
        }
        public byte[,] Magnitude(byte[,] SobelVMatrix, byte [,] SobelHMatrix)
        {
            byte[,] CombineSobelMatrix = new byte[SobelHMatrix.GetLength(0), SobelHMatrix.GetLength(1)];

            for (int j = 0; j < SobelHMatrix.GetLength(1); j++)
            {

                for (int i = 0; i < SobelHMatrix.GetLength(0); i++)

                {
                    CombineSobelMatrix[i, j] = Math.Min((byte)Math.Sqrt(Math.Pow(SobelVMatrix[i, j], 2) +

                                                                         Math.Pow(SobelHMatrix[i, j], 2)), (byte)255);
                }

            }
            return CombineSobelMatrix;

        }
        public Bitmap ConvertArrayToImage(byte[] imageData, int width ,int height)
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
            return Bm;  
        }
    }
}
