using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdgeDetaction.Core;
using EdgeDetaction.Core.Repasitorys;

namespace EdgeDetection.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var formattableString = $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\\statics\\bitmap.bmp";
            var dal = new EdgeDetectionDal(new EdgeDetectionEntities());
            var images = dal.ImageDal.GetAll();
            var image = new Image {Name = Guid.NewGuid().ToString()};
            dal.ImageDal.Add(image);
        }
    }
}
