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
        public List<MatrixDetection> GetAll()
        {
            return DbContext.MatrixDetections.ToList();
        }

        public void Add(MatrixDetection MatrixDetection)
        {
            DbContext.MatrixDetections.Add(MatrixDetection);
            DbContext.SaveChanges();
        }

        public MatrixDetection GetByName(Guid name)
        {
            throw new NotImplementedException();
        }

        public MatrixDetection GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
