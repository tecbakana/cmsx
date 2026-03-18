using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ICMSX
{
    public interface IRoteiroDAL
    {
        int NumParms { get; set; }
        void CriaArea();
        void MakeConnection(dynamic prop);
        DataTable ListaRoteiros();
        DataTable ListaRoteirosPorCidade();
        DataTable ListaRoteirosPorFornecedor();
        DataTable ListaRoteirosPorId();
        DataTable ListaDetalheRoteiro();
        DataTable DetalheRoteiro();
    }
}
