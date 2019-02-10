using System;
using System.Collections.Generic;

namespace EdgeDetaction.DAL.Repasitorys.Interfaces
{
    public interface IImageDal : IBaseDal
    {
        List<MatrixDetection> GetAll();
        void Add(MatrixDetection image);
        MatrixDetection GetByName(string name);
        MatrixDetection GetById(int id);
        MatrixDetection GetSobelV(int id);
        MatrixDetection GetSobelH(int id);
        MatrixDetection GetMagnitude(int id);


    }
}