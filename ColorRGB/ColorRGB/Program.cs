using System;
using System.Drawing;
using System.IO;
namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Please input image path");
            string bitmapFilePath = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Please input R or G or B");
            char RGB = Convert.ToChar(Console.Read());
            string[] subpaths = bitmapFilePath.Split('.');

            string filePath = subpaths[0];
           
            try
            {
                Bitmap b1 = new Bitmap(bitmapFilePath);
                int height = b1.Height;
                int width = b1.Width;
                byte[,] colorMatrix = new byte[height, width];
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        switch (RGB)
                        {
                            case 'R':
                                colorMatrix[j, i] = b1.GetPixel(i, j).R;
                                break;
                            case 'G':
                                colorMatrix[j, i] = b1.GetPixel(i, j).G;
                                break;
                            case 'B':
                                colorMatrix[j, i] = b1.GetPixel(i, j).B;
                                break;
                            default:
                                Console.WriteLine("different  word, program will be take R default");
                                colorMatrix[j, i] = b1.GetPixel(i, j).R;
                                break;
                        }

                    }
                }

                int[,] SobelV = new int[3, 3] { { 1, 0, -1 },
                                            { 2, 0, -2 },
                                            { 1, 0, -1 }
                                                          };
                for (int i = 0; i < height - 2; i++)
                {
                    for (int j = 0; j < width - 2; j++)

                    {
                        colorMatrix[i + 1, j + 1] = (byte)(colorMatrix[i, j] * SobelV[0, 0] + colorMatrix[i, j + 2] * SobelV[0, 2]
                                               + colorMatrix[i + 1, j] * SobelV[1, 0] + colorMatrix[i + 1, j + 2] * SobelV[1, 2]
                                               + colorMatrix[i + 2, j] * SobelV[2, 0] + colorMatrix[i + 2, j + 2] * SobelV[2, 2]);
                    }
                }
                StreamWriter file = File.CreateText(@filePath + RGB + ".txt");
                StreamWriter fileRenge = File.CreateText(@filePath + RGB + "_dat.txt");

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        {
                            file.Write(colorMatrix[i, j] + " ");
                        }
                    }
                    file.WriteLine("\n");

                }
                file.Close();
                fileRenge.WriteLine("{0} {1}", height, width);
                fileRenge.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
 
            Console.WriteLine("files are created in  the same location that image");
            Console.ReadKey();
        }
    }
}
