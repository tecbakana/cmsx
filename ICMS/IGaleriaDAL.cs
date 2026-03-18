using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace ICMS
{
    public interface IGaleriaDAL
    {
        void MakeConnection(dynamic prop);
        void CriaGaleria();
        DataTable GaleriaPorAreaId();
    }
}
