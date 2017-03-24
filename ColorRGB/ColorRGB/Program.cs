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
            String[] subpaths = bitmapFilePath.Split('.');

            string filePath = subpaths[0];
            try
            {
                Bitmap b1 = new Bitmap(bitmapFilePath);
                int height = b1.Height;
                int width = b1.Width;
                Byte[,] colorMatrix = new byte[height, width];
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
                StreamWriter file = File.CreateText(filePath + RGB + ".txt");
                StreamWriter fileRenge = File.CreateText(filePath + RGB + "_dat.txt");

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
