using System;
using System.Collections.Generic;
using System.Data;

namespace CMSBLL.Repositorio
{
    public interface IModuloRepositorio
    {
        void MakeConnection(dynamic prop);
        List<Modulo> Helper(DataTable moddata);
        List<Modulo> ListaModulos();
        void CriaModulo();
    }
}
