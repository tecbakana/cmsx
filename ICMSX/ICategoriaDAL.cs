using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ICMSX
{
    public interface ICategoriaDAL
    {
        object CriaCategoria();
        void EditCategoria();
        DataTable ListaCategorias();
        DataTable ListaCategoriasFull();
        DataTable ListaCategoriasPai();
        DataTable ListaSubCategorias();
        DataTable ListaCategoriasPorId();
        void MakeConnection(dynamic prop);
    }
}
