using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ICMSX
{
    public interface IAtributoDAL
    {
        int NumParms { get; set; }
        void CriaAtributo();
        void MakeConnection(dynamic prop);
        DataTable ListaAtributos();
        DataTable ListaAtributosXProduto();
        void InativaAtributo();
    }
}
