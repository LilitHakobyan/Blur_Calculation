using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdgeDetaction.DAL;
using EdgeDetaction.DAL.Repasitorys;
using EdgeDetaction.Core;
using EdgeDetaction.Core.Repasitorys;
using System.Drawing;
using EdgeDetaction.Core.Repasitorys.Enums;

namespace EdgeDetection.Test
{
    class Program
    {
        public static double Sum (double[,] matrix)
        {
            double Sum = 0;
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    Sum += matrix[i, j];
                }
            }
            return Sum;
        }
        public static double SumFromFile()
        {
            double Summmmm = 0;
            StreamReader readerDate = File.OpenText("C://Sample_dat.txt");//Sample
            string rang = readerDate.ReadLine();
            int width = Convert.ToInt32(Convert.ToString(rang[0]) + Convert.ToString(rang[1])+ Convert.ToString(rang[2]));
            int height = Convert.ToInt32(Convert.ToString(rang[4]) + Convert.ToString(rang[5])+Convert.ToString(rang[6])); ;
            double[,] colorMatrix = new double[width,height];
            Console.WriteLine(height);
            Console.WriteLine(width);
            StreamReader readerMatrix = File.OpenText("C://Sample.txt");//Sample
            string matrix = string.Empty;
            string element = string.Empty;
            int s = 0;

            for (int j = 0; j < height; j++)
            {
                matrix = readerMatrix.ReadLine();

                char[] y = matrix.ToCharArray();
                int k = 0;
                for (int i = 0; i < width; i++)
                {
                    //while (y[k] != ' ')
                    //{
                    //    element += Convert.ToString(y[k]);
                    //    k++;
                    //}
                   // Sum +=Convert.ToDouble(element);
                    //k++;
                    //element = string.Empty;
                    while (y[k] != ' ')
                    {
                        element += Convert.ToString(y[k]);

                        k++;
                    }
                    k++;
                    Summmmm += Convert.ToDouble(element);

                    colorMatrix[i, j] = Convert.ToDouble(element);

                    element = string.Empty;

                }
            }
            var core = new EdgeDetectionCore();
            var matrixH = core.Image.SobelHOperationD(colorMatrix);
            var matrixV = core.Image.SobelVOperationD(colorMatrix);
            double sumH = Sum(matrixH);
            double sumV = Sum(matrixV);
            return Summmmm;
        }
        public static double SumFromFileArray()
        {
            double Sum = 0;
            StreamReader readerDate = File.OpenText("C://Sample_dat.txt");//Sample
            string rang = readerDate.ReadLine();
            int width1 = Convert.ToInt32(Convert.ToString(rang[0]) + Convert.ToString(rang[1]) + Convert.ToString(rang[2]))-2;
            int width = 1;
            int height1 = Convert.ToInt32(Convert.ToString(rang[4]) + Convert.ToString(rang[5]) + Convert.ToString(rang[6]))-2;
            int height = width1 * height1;
            StreamReader readerMatrix = File.OpenText("C://VGrad.txt");//Sample
            string matrix = string.Empty;
            string element = string.Empty;
            int s = 0;

            for (int j = 0; j < height; j++)
            {
                element = readerMatrix.ReadLine();

                //char[] y = matrix.ToCharArray();
                int k = 0;
                for (int i = 0; i < width; i++)
                {
                   // while (y[k] != ' ')
                   //{
                      //  element += Convert.ToString(y[k]);
                       // k++;
                   // }
                    Sum += Convert.ToDouble(element);
                    k++;
                    element = string.Empty;
                }

            }
            return Sum;
        }
      
        static void Main(string[] args)
        {
            var formattableString = $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\\statics\\I21.BMP";
            var dal = new EdgeDetaction.DAL.Repasitorys.EdgeDetectionDal(new EdgeDetectionEntities());
            var core = new EdgeDetectionCore();

            var w = 0;
            var h = 0;
            var m = core.Image.ConvertImageToMatrix(formattableString, CompTypes.R, ref w, ref h);


            var matrixH = core.Image.SobelHOperationDoubleForVisualization(m);
            var matrixV = core.Image.SobelVOperationDForVisualization(m);
            var matrixM = core.Image.MagnitudeDouble(matrixV, matrixH);
            //var m1 = core.Image.ConvertImageToMatrixByte(formattableString, CompTypes.R, ref w, ref h);

            var matrixH1 = core.Image.SobelHOperationD(m);
            var matrixV1 = core.Image.SobelVOperationD(m);
            var matrixM1 = core.Image.MagnitudeDouble(matrixV1, matrixH1);

            var normH = core.Image.ConvertMatreixToArray(core.Image.Normalize(matrixH));
            var normV = core.Image.ConvertMatreixToArray(core.Image.Normalize(matrixV));
            var normM = core.Image.ConvertMatreixToArray(core.Image.Normalize(matrixM));
            var estim = core.Image.EstimationDouble(matrixM);//1.2
            var estim1 = core.Image.EstimationDouble(matrixM1);//1.8
            //var amSh = core.Image.ConvertMatreixToArray(matrixH);
            Bitmap bm = core.Image.ConvertArrayToImage(normH, w, h);
            bm.Save(@"C:\normH.bmp");
            Bitmap bm1 = core.Image.ConvertArrayToImage(normV, w, h);
            bm1.Save(@"C:\normV.bmp");
            Bitmap bm2 = core.Image.ConvertArrayToImage(normM, w - 2, h - 2);
            bm2.Save(@"C:\normM.bmp");
            //core.Localization.SaveImage(bm, image.Name);
            //core.Localization.SaveImage(matrixH, "GradH");
            //core.Localization.SaveMatrix(matrixV, "GradV");
            //core.Localization.SaveMatrix(matrixM, "GradM");
            //core.Localization.SaveMatrix(matrixH, "ImgH");
            //core.Localization.SaveMatrix(matrixV, "ImgV");
            //core.Localization.SaveMatrix(matrixM, "ImgM");
            //  double sumH = Sum(matrixH);
            //  double sumV = Sum(matrixV);
            //double sum = Sum(m);
            //double sumfromfile = SumFromFileArray();
            //double sumfrom = SumFromFile();


            // var a = core.Image.ConvertMatreixToArray(m);
            //var image = new MatrixDetection() { Name = Guid.NewGuid().ToString(), Component = "R", Height = h, Type = "Defoult", Width = w, Matrix = a };
            //// dal.ImageDal.Add(image);
            //var m1 = core.Image.ConvertArrayToMatrix(a, w, h);
            //var mSh = core.Image.SobelHOperation(m1);
            //// var amSh = core.Image.ConvertMatreixToArray(mSh);
            //var mSv = core.Image.SobelVOperation(m1);
            //// var amSv = core.Image.ConvertMatreixToArray(m);
            //var MM = core.Image.Magnitude(mSv, mSh);
            // Bitmap bm=  core.Image.ConvertArrayToImage(a, w, h);
            ////var im = dal.ImageDal.GetById(3);
            ////var imname = dal.ImageDal.GetByName("8b32afa1-320e-46b8-8f9e-2f1d63f97859");
            //var path = System.IO.Path.Combine(Environment.GetFolderPath(
            // Environment.SpecialFolder.MyDoc‌​uments), "EdgeDetaction");
          //  core.Localization.SaveMatrix(m, "SampleMY");
            ////System.IO.Directory.CreateDirectory(path);
            //// var ss = core.Localization.ReadMatrix("67a1247b-913e-4405-b98a-10fd3c02724e");
            //var magnitude = core.Image.Magnitude(m);
            // core.Localization.SaveMatrix(magnitude, "magnitudeMatrix");
            // var est = core.Image.Estimation(magnitude);
            //  var amSh = core.Image.ConvertMatreixToArray(magnitude);
            //  Bitmap bm = core.Image.ConvertArrayToImage(amSh, w-2, h-2);
            //  bm.Save(@"C:\img.bmp");
          //  var k = 0;
            //// core.Localization.SaveImage(bm, image.Name);
            //var es = core.Image.Estimation(MM);
        }
    }
}
