using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdgeDetaction.DAL.Repasitorys.Interfaces;

namespace EdgeDetaction.DAL.Repasitorys.Implemantation
{
    class BaseDal:IBaseDal
    {
        protected EdgeDetectionEntities DbContext;

        protected BaseDal(EdgeDetectionEntities dbContext)
        {
            DbContext = dbContext;
        }

    }
}
