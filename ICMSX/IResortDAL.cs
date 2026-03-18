using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace ICMSX
{
    public interface IResortDAL
    {
        void MakeConnection(dynamic prop);
        DataTable ListaResortPorRoteiro();
        DataTable DetalheResort();
    }
}
