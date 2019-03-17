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
        static void Main(string[] args)
        {
            var formattableString = $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\\statics\\I21.BMP";
            var dal = new EdgeDetaction.DAL.Repasitorys.EdgeDetectionDal(new EdgeDetectionEntities());
            var core = new EdgeDetectionCore();

            var w = 0;
            var h = 0;
            var m = core.Image.ConvertImageToMatrix(formattableString, CompTypes.R, ref w, ref h);
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
            var path= System.IO.Path.Combine(Environment.GetFolderPath(
             Environment.SpecialFolder.MyDoc‌​uments), "EdgeDetaction");
            ////core.Localization.SaveMatrix(m, image.Name);
            ////System.IO.Directory.CreateDirectory(path);
            //// var ss = core.Localization.ReadMatrix("67a1247b-913e-4405-b98a-10fd3c02724e");
             var amSh = core.Image.ConvertMatreixToArray(m);
            Bitmap bm = core.Image.ConvertArrayToImage(amSh, w, h);
            bm.Save(@"C:\img.bmp");

            //// core.Localization.SaveImage(bm, image.Name);
            //var es = core.Image.Estimation(MM);
        }
    }
}
