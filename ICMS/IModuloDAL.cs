using System;
using System.Collections.Generic;
using System.Data;

namespace ICMS
{
    public interface IModuloDAL
    {
        void MakeConnection(dynamic props);
        void CriaModulo();
        DataTable ListaModulos();
        DataTable ListaModulosXUser();
    }
}
