using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace ICMSX
{
    public interface IAcessoDAL
    {
        void MakeConnection(dynamic prop);
        DataTable ValidaAcesso();
    }
}
