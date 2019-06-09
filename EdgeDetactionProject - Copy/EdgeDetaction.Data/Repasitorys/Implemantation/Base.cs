using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdgeDetaction.Core.Repasitorys.Interfaces;

namespace EdgeDetaction.Core.Repasitorys.Implemantation
{
    class Base:IBase
    {
        protected Base ()
        {
        }
        public void ShowImage(string path)
        {
            throw new NotImplementedException();
        }
        public void ShowMatrix(string path)
        {
            throw new NotImplementedException();
        }
    }
}
