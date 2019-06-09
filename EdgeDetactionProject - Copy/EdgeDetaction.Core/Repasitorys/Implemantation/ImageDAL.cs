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

        public void SetSobelV(int parentid, int sobelVid)
        {
            var matrixParent = GetById(parentid);
            matrixParent.Parent1 = sobelVid;
            DbContext.MatrixDetections.Attach(matrixParent);
            DbContext.Entry(matrixParent).Property(x => x.Parent1).IsModified = true;
            DbContext.SaveChanges();
        }

        public void SetSobelH(int parentid, int sobelHid)
        {
            var matrixParent = GetById(parentid);
            matrixParent.Parent2 = sobelHid;
            DbContext.MatrixDetections.Attach(matrixParent);
            DbContext.Entry(matrixParent).Property(x => x.Parent2).IsModified = true;
            DbContext.SaveChanges();
        }
        public void SetMagnitude(int parentid, int magId)
        {
            var matrixParent = GetById(parentid);
            matrixParent.BaseId = magId;
            DbContext.MatrixDetections.Attach(matrixParent);
            DbContext.Entry(matrixParent).Property(x => x.BaseId).IsModified = true;
            DbContext.SaveChanges();
        }

        public List<MatrixDetection> GetAllRoots()
        {
            return DbContext.MatrixDetections.Where(image => image.Type == "Default").ToList();
        }

        public void SetEstimation(int id, decimal estimaion)
        {
            var matrixParent = GetById(id);
            matrixParent.Estimation = estimaion;
            DbContext.MatrixDetections.Attach(matrixParent);
            DbContext.Entry(matrixParent).Property(x => x.Estimation).IsModified = true;
            DbContext.SaveChanges();
        }
    }
}
