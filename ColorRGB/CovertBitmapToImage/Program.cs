using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
namespace CovertBitmapToImage
{
    class Program
    {
        static void Main(string[] args)
        {
           
            int[,] colorMatrix = new int[, ] {   { 1, 0, -1, 1, 0, -1 },
                                            { 2, 0, -2, 1, 0, -1 },
                                            { 1, 0, -1, 1, 0, -1 },
                                            { 1, 0, -1, 1, 0, -1 },
                                            { 2, 0, -2, 1, 0, -1 },
                                            { 1, 0, -1, 1, 0, -1 } };
            int[,] SobelV = new int[3, 3] { { 1, 0, -1 },
                                            { 2, 0, -2 },
                                            { 1, 0, -1 }
                                                          };
            for (int i = 0; i <  6 -2; i++)
            {
                for (int j = 0; j < 6-2; j++)

                {
                    colorMatrix[i + 1, j + 1] = colorMatrix[i , j ]* SobelV[0,0] + colorMatrix[i , j + 2]*SobelV[0, 2]
                                           + colorMatrix[i + 1, j ] * SobelV[1, 0] + colorMatrix[i + 1, j + 2]*SobelV[1, 2]
                                           + colorMatrix[i + 2, j ] * SobelV[2, 0] + colorMatrix[i + 2, j + 2]*SobelV[2, 2];
                }
            }
        }
    }
}