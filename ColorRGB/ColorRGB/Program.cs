using System;
using System.Drawing;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            // string bitmapFilePath = string.Empty;//@"E:\";
            Console.WriteLine("Please input image path");
            string bitmapFilePath = Convert.ToString(Console.ReadLine());
            // bitmapFilePath = bitmapFilePath + bitmapFileName;
            Console.WriteLine("Please input R or G or B");
            char RGB = Convert.ToChar(Console.Read());
            String[] subpaths = bitmapFilePath.Split('.');
            //foreach (var subpath in subpaths)

            string filePath = subpaths[0];

            Bitmap b1 = new Bitmap(bitmapFilePath);

            int height = b1.Height;
            int width = b1.Width;

            Byte[,] colorMatrix = new byte[height, width];
            for (int i = 0; i < width; i++)
            {
                // colorMatrix[i] = new byte[hight];
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
                    // colorMatrix[j, i] = b1.GetPixel(i, j).R;

                }
            }
            //

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    // Console.Write(colorMatrix[i][j]);
                    using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(filePath + RGB + ".txt", true))//+ @"Image"
                    {
                        file.Write(colorMatrix[i, j] + " ");
                    }

                }
                using (System.IO.StreamWriter file =
                   new System.IO.StreamWriter(filePath + RGB + ".txt", true))// + @"Image"
                {
                    file.WriteLine("\n");
                }
                //Console.WriteLine("\n");
            }
            using (System.IO.StreamWriter file =

                   new System.IO.StreamWriter(filePath + RGB + "_dat.txt", true))//+ @"Image" 
            {
                file.WriteLine("{0} {1}", height, width);
            }
            //Console.WriteLine("Image" + RGB + ".txt and Image" + RGB + "_dat.txt are created in "+filePath );
            Console.WriteLine("files are created in  the same location that image");
            Console.ReadKey();
        }
    }
}
