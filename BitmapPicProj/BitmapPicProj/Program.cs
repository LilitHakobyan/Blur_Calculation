using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
namespace Bitmap_project
{
    class Program
    {

        static void Main(string[] args)
        {
            string bitmapFilePath = @"C:\1f48b.png";
            Bitmap b1 = new Bitmap(bitmapFilePath);

            int hight = b1.Height;
            int width = b1.Width;

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
                    Console.Write(colorMatrix[i][j]);
                }
                Console.WriteLine("/n");
            }
            //Console.WriteLine(colorMatrix);
            Console.ReadKey();
        }
    }
}
