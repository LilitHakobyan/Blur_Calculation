using EdgeDetaction.Core.Repasitorys.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace EdgeDetaction.Core.Repasitorys.Interfaces
{
    public interface IImage : IBase
    {
        double[,] ConvertImageToMatrix(string bitmapFilePath, CompTypes component, ref int width, ref int hight);
        byte[,] ConvertImageToMatrixByte(string bitmapFilePath, CompTypes component, ref int width, ref int hight);
        byte[] ConvertMatreixToArray(byte[,] matrix);
        double[,] ConvertArrayToMatrixDoubles(byte[] array, int width, int hight);
        double[] ConvertMatreixToArrayDouble(double[,] matrix);
        byte[,] ConvertArrayToMatrix(byte[] array, int width, int hight);
        double[,] ConvertArrayToMatrixDouble(double[] array, int width, int hight);
        byte[,] SobelHOperation(byte[,] matrix);
        byte[,] SobelVOperation(byte[,] matrix);
        double[,] SobelHOperationD(double[,] matrix);
        double[,] SobelVOperationD(double[,] matrix);
        double[,] SobelVOperationDForVisualization(double[,] matrix);
        double[,] SobelHOperationDoubleForVisualization(double[,] matrix);
        byte[,] MagnitudeNew(byte[,] SobelVMatrix, byte[,] SobelHMatrix);
        double[,] MagnitudeDouble(double[,] SobelVMatrix, double[,] SobelHMatrix);
        double[,] MagnitudeFromDefoultMatrix(double[,] matrix);
        double[,] MagnitudeFromDefoultMatrixForVisualization(double[,] matrix);
        Bitmap ConvertArrayToImage(byte[] imageData, int width, int height);
        decimal Estimation(byte[,] magnitudeMatrix);
        decimal EstimationDouble(double[,] magnitudeMatrix);
        byte[,] NormalizeMatrix(double[,] matrix);
        byte[,] NormalizeMatrix(byte[,] matrix);

        byte[,] Normalize(double[,] matrix);

        byte[,] Normalize(byte[,] matrix);


    }
}