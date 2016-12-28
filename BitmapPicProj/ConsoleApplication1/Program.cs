using System;
using System.Drawing;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            string bitmapFilePath = @"E:\";
            string bitmapFileName = Convert.ToString(Console.ReadLine());//@"C:\1f48b.png";
            bitmapFilePath = bitmapFilePath + bitmapFileName;
            Bitmap b1 = new Bitmap(bitmapFilePath);

            int hight = b1.Height;
            int width = b1.Width;
            Color myColor = Color.FromArgb(100, 150, 75);

            Byte[][] colorMatrix = new byte[width][];
            for (int i = 0; i < width; i++)
            {
                colorMatrix[i] = new byte[hight];
                for (int j = 0; j < hight; j++)
                {
                    colorMatrix[i][j] = b1.GetPixel(i, j).R;
                    colorMatrix[i][j] = b1.GetPixel(i, j).R;
                }
            }
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < hight; j++)
                {
                    // Console.Write(colorMatrix[i][j]);
                    using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(@"E:\ImageR.txt", true))
                    {
                        file.Write(colorMatrix[i][j] + " ");
                    }

                }
                using (System.IO.StreamWriter file =
                   new System.IO.StreamWriter(@"E:\ImageR.txt", true))
                {
                    file.WriteLine("\n");
                }
                //Console.WriteLine("\n");
            }
            using (System.IO.StreamWriter file =
                   new System.IO.StreamWriter(@"E:\ImageR_dat.txt", true))
            {
                file.WriteLine("{0},{1}", hight, width);
            }
        }
    }
}
