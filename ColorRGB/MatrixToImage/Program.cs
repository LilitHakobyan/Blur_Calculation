using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace MatrixToImage
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader readerDate = File.OpenText("E://Sample_dat.txt");
            string rang = readerDate.ReadLine();
            int height = Convert.ToInt32(Convert.ToString(rang[0]) + Convert.ToString(rang[1]));
            int width =  Convert.ToInt32(Convert.ToString(rang[3]) + Convert.ToString(rang[4])); ;
            int[,] colorMatrix = new int[height, width];
            Console.WriteLine(height);
            Console.WriteLine(width);
            StreamReader readerMatrix = File.OpenText("E://Sample.txt");
            string matrix = string.Empty;
            string element = string.Empty;
            byte[] imageData=new byte[height*width];
            Color c = new Color();
            int s = 0;
            for (int i = 0; i < height; i++)
            {
                matrix = readerMatrix.ReadLine();
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
                    if (s < height*width)
                    {
                        imageData[s] = Convert.ToByte(element);
                        s++;
                        c = Color.FromArgb(0, colorMatrix[i, j], 0, 0);
                    }
                   
                    element = string.Empty;
                }

            }
            //
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)

                {
                    Console.Write(colorMatrix[i, j] + " ");
                }
                Console.WriteLine();
            }

            // Create a Bitmap object from a file.
            //Bitmap myBitmap = new Bitmap(height, width);

            // Set each pixel in myBitmap to black.
           // for (int Xcount = 0; Xcount < myBitmap.Width; Xcount++)
           // {
              //  for (int Ycount = 0; Ycount < myBitmap.Height; Ycount++)
              //  {
              //      myBitmap.SetPixel(Xcount, Ycount, c);
               // }
          //  }
          //  myBitmap.Save(@"E:\newImage.bmp");
            // Draw myBitmap to the screen again.

            //byte[] imageData = File.ReadAllBytes("E://Sample.txt");
            //var ms = new MemoryStream(imageData);
            //
            //  Image image = Image.FromStream(ms);
            //   image.Save(@"E:\newImage1.jpg");

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
            Console.WriteLine(Bm);
            Bm.Save(@"E:\newImage1.bmp");


            //  Console.WriteLine(colorMatrix);
            Console.ReadKey();
        }
    }
}
