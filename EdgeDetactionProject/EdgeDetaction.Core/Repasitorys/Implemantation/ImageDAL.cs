using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.ComponentModel;
using EdgeDetaction.DAL.Repasitorys.Interfaces;

namespace EdgeDetaction.DAL.Repasitorys.Implemantation
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
      
        public MatrixDetection GetByName(string name)
        {
            return DbContext.MatrixDetections.SingleOrDefault(image => image.Name == name);
        }

        public MatrixDetection GetById(int id)
        {
            return DbContext.MatrixDetections.Find(id);
        }

        public MatrixDetection GetSobelV(int id)
        {
            return DbContext.MatrixDetections.SingleOrDefault(image => image.Parent1 == id);
        }

        public MatrixDetection GetSobelH(int id)
        {
            return DbContext.MatrixDetections.SingleOrDefault(image => image.Parent2 == id);
        }

        public MatrixDetection GetMagnitude(int id)
        {
            return DbContext.MatrixDetections.SingleOrDefault(image => image.BaseId == id);
        }
    }
}
