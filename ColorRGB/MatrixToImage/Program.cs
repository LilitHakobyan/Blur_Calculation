using System;
using System.Drawing;
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
            for (int i = 0; i < height; i++)
            {   matrix = readerMatrix.ReadLine();
                for (int j = 0; j < width; j++)
                {
                    {
                        while (matrix[j]!=' ')
                        {
                            element += Convert.ToString(matrix[j]); 
                        }
                        colorMatrix[i, j] = Convert.ToInt32(element);
                        element = string.Empty;
                    }
                }
              
            }
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write(colorMatrix[i, j]+' ');
                }
                 Console.WriteLine();
            }

             // Bitmap b1 = new Bitmap("E://Sample.txt");
            

            //  Console.WriteLine(colorMatrix);
            Console.ReadKey();
        }
    }
}
