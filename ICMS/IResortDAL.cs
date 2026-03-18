using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace ICMS
{
    public interface IResortDAL
    {
        void MakeConnection(dynamic prop);
        DataTable ListaResortPorRoteiro();
        DataTable DetalheResort();
    }
}
