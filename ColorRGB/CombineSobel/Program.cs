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


            int [] rowArray=new int[height];
            int[] pillarArray = new int[width];
            int sumRow = 0;
            int sumPillar = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    rowArray[i] += CombineSobelMatrix[i, j];
                }
                sumRow += rowArray[i];
                Console.Write(rowArray[i]+" ");
                
            }
            Console.WriteLine();
            Console.WriteLine(sumRow);


            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    pillarArray[i] += CombineSobelMatrix[j, i];
                }
                sumPillar += pillarArray[i];
                Console.Write(pillarArray[i]+" ");
            }

            Console.WriteLine();
            Console.WriteLine(sumPillar);

            double MRow = 0;
            double MPillar = 0;

            double DRow = 0;
            double DPillar = 0;

            for (int i = 0; i < height; i++)
            {
                MRow += i * rowArray[i];
            }
            MRow = MRow / sumRow;

            for (int i = 0; i < height; i++)
            {
                DRow += i*i * rowArray[i];
            }
            DRow = DRow / sumRow-MRow*MRow;

            for (int i = 0; i < width; i++)
            {
                MPillar += i * pillarArray[i];
            }
            MPillar = MPillar / sumPillar;

            for (int i = 0; i < width; i++)
            {
                DPillar += i * i * pillarArray[i];
            }
            DPillar = DPillar / sumPillar - MPillar * MPillar;

            Console.WriteLine(MRow+ "  "+MPillar);
            Console.WriteLine(DRow + "  " + DPillar);
            Console.WriteLine(DRow/(MRow* MRow));//0.15
            Console.WriteLine(DPillar/(MPillar * MPillar));//0.32
            double M ;
            double D ;
            M = MRow + MPillar;
            D = DRow + DPillar;
            Console.WriteLine(D/(M*M)); // 0.12


            Console.ReadKey();
            //// fi Corner arctg(H/V)
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
                        CornerSobelMatrix[i, j] = Math.Min((byte)Math.Atan((double)SobelHMatrix[i, j] / SobelVMatrix[i, j]), (byte)255);

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
            StreamWriter file = File.CreateText(@"E:\CombineSobelMatrix " + ".txt");
            StreamWriter fileRenge = File.CreateText(@"E:\CombineSobelMatrix" + "_dat.txt");

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    {
                        file.Write(CombineSobelMatrix[i, j] + " ");
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
