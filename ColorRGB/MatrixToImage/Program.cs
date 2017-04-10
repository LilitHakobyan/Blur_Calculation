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
            int width = Convert.ToInt32(Convert.ToString(rang[3]) + Convert.ToString(rang[4])); ;
            int[,] colorMatrix = new int[height, width];
            Console.WriteLine(height);
            Console.WriteLine(width);
            StreamReader readerMatrix = File.OpenText("E://Sample.txt");
            string matrix = string.Empty;
            string element = string.Empty;
            byte[] imageData=new byte[height*width];
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
                    for (int s = 0; s < height*width; s++)
                    {
                        imageData[s] = Convert.ToByte(element);
                    }
                    colorMatrix[i, j] = Convert.ToInt32(element);
                    element = string.Empty;
                }


            }
            //
             //byte[] imageData = File.ReadAllBytes("E://Sample.txt");
                var ms = new MemoryStream(imageData);
            
                Image image = Image.FromStream(ms);
                image.Save(@"E:\newImage1.jpg");
            
            //http://stackoverflow.com/questions/21555394/how-to-create-bitmap-from-byte-array
          

           
            //  Console.WriteLine(colorMatrix);
            Console.ReadKey();
        }
    }
}
