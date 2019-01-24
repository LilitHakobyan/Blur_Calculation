using System;
using System.Collections.Generic;

namespace EdgeDetaction.Core.Repasitorys.Interfaces
{
    public interface IImageDal:IBaseDal
    {
       List<MatrixDetection> GetAll();
       void Add(MatrixDetection image);
       MatrixDetection GetByName(Guid name);
       MatrixDetection GetById(int id);

    }
}