using System;
using System.Collections.Generic;

namespace EdgeDetaction.DAL.Repasitorys.Interfaces
{
    public interface IImageDal : IBaseDal
    {
        List<MatrixDetection> GetAll();
        List<MatrixDetection> GetAllRoots();
        void Add(MatrixDetection image);
        MatrixDetection GetByName(string name);
        MatrixDetection GetById(int id);
        MatrixDetection GetSobelV(int id);
        MatrixDetection GetSobelH(int id);
        MatrixDetection GetMagnitude(int id);
        void SetSobelV(int parentid,int sobelVid);
        void SetSobelH(int parentid, int sobelHid);
        void SetMagnitude(int parentid, int magId);
        void SetEstimation(int id, decimal estimaion);


    }
}