using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdgeDetaction.Core.Repasitorys.Interfaces
{
    public interface ILocalization
    {
        void SaveMatrix(byte[,] matrix , string filename);
        void SaveImage(Bitmap image, string filename);
        byte [,] ReadMatrix(string path);
    }
}
