using EdgeDetaction.Core.Repasitorys.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace EdgeDetaction.Core.Repasitorys.Interfaces
{
    public interface IImage:IBase
    {
       byte[,] ConvertImageToMatrix(string bitmapFilePath, CompTypes component, ref int width, ref int hight);
       byte[] ConvertMatreixToArray(byte[,] matrix);
       byte[,] ConvertArrayToMatrix(byte[] array, int width, int hight);
       byte[,] SobelHOperation(byte[,] matrix);
       byte[,] SobelVOperation(byte[,] matrix);
       byte[,] Magnitude(byte[,] SobelVMatrix, byte[,] SobelHMatrix);
       Bitmap ConvertArrayToImage(byte[] imageData, int width, int height);
       decimal Estimation(byte[,] magnitudeMatrix);
    }
}