using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ICMSX
{
    public interface IOpcaoDAL
    {
        int NumParms { get; set; }
        void CriaOpcao();
        void MakeConnection(dynamic prop);
        DataTable ListaOpcoes();
        void InativaOpcao();
    }
}
