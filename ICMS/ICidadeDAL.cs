using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ICMS
{
    public interface ICidadeDAL
    {
        int NumParms { get; set; }
        void CriaCidade();
        void MakeConnection(dynamic prop);
        DataTable ListaCidades();
        DataTable ListaCidadePorId();
        DataTable ListaCidadePorNomeFilha();
        void InativaCidade();
    }
}
