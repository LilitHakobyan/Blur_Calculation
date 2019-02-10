using System;
using EdgeDetaction.Core.Repasitorys.Implemantation;
using EdgeDetaction.Core.Repasitorys.Interfaces;

namespace EdgeDetaction.Core.Repasitorys
{
    public class EdgeDetectionCore
    {
        private IImage _image;
        public IImage Image => _image ?? (_image = new Image());

        private ILocalization _localization;
        public ILocalization Localization => _localization ?? (_localization = new Localization());
        private bool _disposed;

        protected virtual void Dispose(bool disposing)

        {

            if (!_disposed)
            {

                if (disposing)
                {
                   
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