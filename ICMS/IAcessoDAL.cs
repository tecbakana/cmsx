using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace ICMS
{
    public interface IAcessoDAL
    {
        void MakeConnection(dynamic prop);
        DataTable ValidaAcesso();
    }
}
