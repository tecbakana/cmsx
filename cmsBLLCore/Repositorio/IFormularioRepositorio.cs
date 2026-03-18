using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace CMSXBLL.Repositorio
{
    public interface IFormularioRepositorio
    {
        void MakeConnection(dynamic prop);
        void CriaFormulario();
        List<Formulario> ListaFormulario();
        List<Formulario> ListaFormularioPorId();
        List<Formulario> Helper(DataTable appdata);
    }
}
