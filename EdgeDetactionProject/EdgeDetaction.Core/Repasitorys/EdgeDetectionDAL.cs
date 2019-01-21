using System;
using EdgeDetaction.Core.Repasitorys.Implemantation;
using EdgeDetaction.Core.Repasitorys.Interfaces;

namespace EdgeDetaction.Core.Repasitorys
{
    public class EdgeDetectionDal
    {


        private EdgeDetectionEntities _edgeDetectionEntities;

        private string _connectionString;





        private EdgeDetectionEntities EdgeDetectionContext => _edgeDetectionEntities ?? (_edgeDetectionEntities = new EdgeDetectionEntities());

        public EdgeDetectionDal(EdgeDetectionEntities edgeDetectionEntities)
        {
            _edgeDetectionEntities = edgeDetectionEntities;
        }


        private IImageDal _imageDal;

        public IImageDal ImageDal => _imageDal ?? (_imageDal = new ImageDal(EdgeDetectionContext));



        private bool _disposed;

        protected virtual void Dispose(bool disposing)

        {

            if (!_disposed)
            {

                if (disposing)
                {
                    EdgeDetectionContext?.Dispose();
                }
            }
            _disposed = true;

        }





        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}