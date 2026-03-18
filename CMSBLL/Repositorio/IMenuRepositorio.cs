using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace CMSBLL.Repositorio
{
    public interface IMenuRepositorio
    {
        void MakeConnection(dynamic prop);
        DataTable montaMenu();
        DataTable montaMenu(string id);
    }
}
