using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdgeDetaction.Core.Repasitorys.Interfaces;

namespace EdgeDetaction.Core.Repasitorys.Implemantation
{
    class ImageDal : BaseDal, IImageDal
    {
        public ImageDal(EdgeDetectionEntities detectionEntities) : base(detectionEntities)
        {

        }
        public List<Image> GetAll()
        {
            return DbContext.Images.ToList();
        }

        public void Add(Image image)
        {
            DbContext.Images.Add(image);
            DbContext.SaveChanges();
        }

        public Image GetByName(Guid name)
        {
            throw new NotImplementedException();
        }

        public Image GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
