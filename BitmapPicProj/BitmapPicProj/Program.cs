using System;
using System.Drawing;
namespace Bitmap_project
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

            // Color.FromArgb(BackColor.A, BackColor.R, BackColor.G, BackColor.B)
            Color[][] colorMatrix = new Color[width][];
            for (int i = 0; i < width; i++)
            {
                colorMatrix[i] = new Color[hight];
                for (int j = 0; j < hight; j++)
                {
                    colorMatrix[i][j] = b1.GetPixel(i, j);

                }
            }
            // return colorMatrix;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < hight; j++)
                {
                    // Console.Write(colorMatrix[i][j]);
                    using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(@"E:\Marix.txt", true))
                    {
                        file.Write(colorMatrix[i][j]);
                    }

                }
                using (System.IO.StreamWriter file =
                   new System.IO.StreamWriter(@"E:\Marix.txt", true))
                {
                    file.WriteLine("\n");
                }
                //Console.WriteLine("\n");
            }

            // Console.ReadKey();

        }
    }
}
