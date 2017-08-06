using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CombineSobel
{
    class Program
    {
        static void Main(string[] args)
        {

            StreamReader readerDate = File.OpenText("E://Sample_dat.txt");//Sample
            string rang = readerDate.ReadLine();
            int height = Convert.ToInt32(Convert.ToString(rang[0]) + Convert.ToString(rang[1]));
            int width = Convert.ToInt32(Convert.ToString(rang[3]) + Convert.ToString(rang[4])); ;
            int[,] colorMatrix = new int[height, width];
            Console.WriteLine(height);
            Console.WriteLine(width);
            StreamReader readerMatrix = File.OpenText("E://Sample.txt");//Sample
            string matrix = string.Empty;
            string element = string.Empty;
            dynamic elementSobel = 0;
            byte[] imageData = new byte[height * width];
            Color c = new Color();
            int s = 0;
            for (int i = 0; i < height; i++)
            {
                matrix = readerMatrix.ReadLine();
                //if (i > 0)
                //{
                // matrix = readerMatrix.ReadLine();
                //}
                char[] y = matrix.ToCharArray();
                // Console.WriteLine(matrix);
                int k = 0;
                for (int j = 0; j < width; j++)
                {
                    //k = j;
                    while (y[k] != ' ')
                    {

                        element += Convert.ToString(y[k]);
                        k++;
                    }
                    k++;
                    colorMatrix[i, j] = Convert.ToInt32(element);
                    if (s < height * width)
                    {
                        imageData[s] = Convert.ToByte(element);
                        s++;
                        c = Color.FromArgb(0, colorMatrix[i, j], 0, 0);
                    }

                    element = string.Empty;
                }

            }
            int[,] SobelHMatrix = new int[height, width];

            int[,] SobelH = new int[3, 3] { { -1, -2, -1 },
                { 0, 0, 0 },
                { 1, 2, 1 }
            };
            for (int i = 0; i < height - 2; i++)
            {
                for (int j = 0; j < width - 2; j++)

                {
                    elementSobel = colorMatrix[i, j] * SobelH[0, 0] + colorMatrix[i, j + 2] * SobelH[0, 2]
                                   + colorMatrix[i + 1, j] * SobelH[1, 0] + colorMatrix[i + 1, j + 2] * SobelH[1, 2]
                                   + colorMatrix[i + 2, j] * SobelH[2, 0] + colorMatrix[i + 2, j + 2] * SobelH[2, 2];

                    if (elementSobel < 0)
                        elementSobel = 0;
                    if (elementSobel > 255)
                        elementSobel = 255;
                    SobelHMatrix[i + 1, j + 1] = elementSobel;
                }
            }

            int[,] SobelVMatrix = new int[height, width];

            int[,] SobelV = new int[3, 3] { { 1, 0, -1 },
                { 2, 0, -2 },
                { 1, 0, -1 }
            };
            for (int i = 0; i < height - 2; i++)
            {
                for (int j = 0; j < width - 2; j++)

                {
                    elementSobel = colorMatrix[i, j] * SobelV[0, 0] + colorMatrix[i, j + 2] * SobelV[0, 2]
                                   + colorMatrix[i + 1, j] * SobelV[1, 0] + colorMatrix[i + 1, j + 2] * SobelV[1, 2]
                                   + colorMatrix[i + 2, j] * SobelV[2, 0] + colorMatrix[i + 2, j + 2] * SobelV[2, 2];

                    if (elementSobel < 0)
                        elementSobel = 0;
                    if (elementSobel > 255)
                        elementSobel = 255;
                    SobelVMatrix[i + 1, j + 1] = elementSobel;
                }
            }
            int[,] CombineSobelMatrix = new int[height, width];

            for (int i = 0; i < height; i++)
            {
               for (int j = 0; j < width; j++)
               {
                   CombineSobelMatrix[i, j] = Math.Min((byte)Math.Sqrt(Math.Pow(SobelVMatrix[i, j], 2) +
                                                                        Math.Pow(SobelHMatrix[i, j], 2)), (byte)255);
               }
            }
            // fi Corner arctg(H/V)
            int[,] CornerSobelMatrix = new int[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (SobelVMatrix[i, j] == 0)
                    {
                        CornerSobelMatrix[i, j] = 0;
                    }
                    else
                    {
                        CornerSobelMatrix[i, j] = Math.Min((byte)Math.Atan((double) SobelHMatrix[i, j] / SobelVMatrix[i, j]), (byte)255);

                    }

                }
            }

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)

                {
                    Console.Write(CombineSobelMatrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            StreamWriter file = File.CreateText(@"E:\SobelCorner " + ".txt");
            StreamWriter fileRenge = File.CreateText(@"E:\SobelCorner" + "_dat.txt");

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    {
                        file.Write(CornerSobelMatrix[i, j] + " ");
                    }
                }
                file.WriteLine("\n");

            }
            file.Close();
            fileRenge.WriteLine("{0} {1}", height, width);
            fileRenge.Close();


        }
    } 

}
