using System;
using System.Collections.Generic;

namespace EdgeDetaction.Core.Repasitorys.Interfaces
{
    public interface IImageDal:IBaseDal
    {
       List<Image> GetAll();
       void Add(Image image);
       Image GetByName(Guid name);
       Image GetById(int id);

    }
}