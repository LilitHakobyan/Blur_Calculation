using EdgeDetaction.Core.Repasitorys.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace EdgeDetaction.Core.Repasitorys.Implemantation
{
    class Localization : ILocalization
    {
        private string defaultPath = Path.Combine(Environment.GetFolderPath(
                                     Environment.SpecialFolder.MyDoc‌​uments), "EdgeDetaction");

        public List<string> GetImagePathsFromFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                return new List<string>();
            var files = Directory.GetFiles(folderPath).ToList();
            return files;

        }

        public int[] ReadArrayAndMakeMartix(string name)
        {
            var path = Path.Combine(defaultPath, name);

            StreamReader readerDate = File.OpenText(path + "_dat.txt");//Sample
            string rang = readerDate.ReadLine();
            var rangArr = rang.Split(' ');
            int height = Convert.ToInt32(Convert.ToString(rangArr[0]));
            int width = Convert.ToInt32(Convert.ToString(rangArr[1]));

            int[] colorArr = new int[height*width];
            StreamReader readerMatrix = File.OpenText(path + ".txt");
            string matrix = string.Empty;
            string element = string.Empty;
            int s = 0;
            matrix = readerMatrix.ReadToEnd();
            string[] y = matrix.Split(';');
            for (int j = 0; j < width; j++)
            {
                string[] lineArr = y[j].Split(' ');
                for (int i = 0; i < height; i++)
                {

                    if (lineArr[i].Contains("\n\r\n"))
                    {

                        element = lineArr[i].Replace("\n\r\n", string.Empty);
                    }
                    else
                    {
                        element = lineArr[i];
                    }
                    colorArr[i] = Convert.ToInt32(element);

                    //if (s < height * width)
                    //{
                    //    imageData[s] = Convert.ToByte(element);
                    //    s++;
                    //    //c = Color.FromArgb(0, colorMatrix[i, j], 0, 0);
                    //}
                    element = string.Empty;
                }

            }
            return colorArr;
        }

        public byte[,] ReadMatrix(string name)
        {
            var path = Path.Combine(defaultPath, name);

            StreamReader readerDate = File.OpenText(path + "_dat.txt");//Sample
            string rang = readerDate.ReadLine();
            var rangArr = rang.Split(' ');
            int height = Convert.ToInt32(Convert.ToString(rangArr[0]));
            int width = Convert.ToInt32(Convert.ToString(rangArr[1]));

            byte[,] colorMatrix = new byte[height, width];
            StreamReader readerMatrix = File.OpenText(path + ".txt");
            string matrix = string.Empty;
            string element = string.Empty;

           // byte[] imageData = new byte[height * width];
            //Color c = new Color();
            int s = 0;
            matrix = readerMatrix.ReadToEnd();
            string[] y = matrix.Split(';');
            for (int j = 0; j < width; j++)
            {
                string[] lineArr = y[j].Split(' ');
                for (int i = 0; i < height; i++)
                {

                    if (lineArr[i].Contains("\n\r\n"))
                    {

                        element =lineArr[i].Replace("\n\r\n",string.Empty);
                    }
                    else
                    {
                        element = lineArr[i];
                    }
                    colorMatrix[i, j] = Convert.ToByte(element);

                    //if (s < height * width)
                    //{
                    //    imageData[s] = Convert.ToByte(element);
                    //    s++;
                    //    //c = Color.FromArgb(0, colorMatrix[i, j], 0, 0);
                    //}
                    element = string.Empty;
                }

            }
            return colorMatrix;
        }

        public void SaveImage(Bitmap bm, string filename)
        {
            bm.Save(Path.Combine(defaultPath, filename) + ".bmp");
        }

        public void SaveMatrix(byte[,] matrix, string filename)
        {
            var path = Path.Combine(defaultPath, filename + ".txt");
            var pathdat = Path.Combine(defaultPath, filename + "_dat.txt");
            if (File.Exists(path))
            {
                return;
            }

            StreamWriter file = File.CreateText(path);
            StreamWriter fileRenge = File.CreateText(pathdat);


            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    file.Write(matrix[i, j] + " ");

                }
                file.Write(";");
                file.WriteLine("\n");

            }



            file.Close();
            fileRenge.WriteLine("{0} {1}", matrix.GetLength(0), matrix.GetLength(1));
            fileRenge.Close();
        }
        public void SaveMatrix(double[,] matrix, string filename)
        {
            var path = Path.Combine(defaultPath, filename + ".txt");
            var pathdat = Path.Combine(defaultPath, filename + "_dat.txt");
            if (File.Exists(path))
            {
                return;
            }

            StreamWriter file = File.CreateText(path);
            StreamWriter fileRenge = File.CreateText(pathdat);


            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    file.Write(matrix[i, j] + " ");

                }
                file.Write(";");
                file.WriteLine("\n");

            }



            file.Close();
            fileRenge.WriteLine("{0} {1}", matrix.GetLength(0), matrix.GetLength(1));
            fileRenge.Close();
        }
    }
}
