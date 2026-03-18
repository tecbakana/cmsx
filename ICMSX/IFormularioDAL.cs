using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace ICMSX
{
    public interface IFormularioDAL
    {
        void MakeConnection(dynamic prop);
        void CriaFormulario();
        DataTable ListaFormulario();
        DataTable ListaFormularioPorAreaId();
    }
}
