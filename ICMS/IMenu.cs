using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace ICMS
{
    public interface IMenuDAL
    {
        void MakeConnection(dynamic prop);
        DataTable Menu();
        void CriaMenu();
    }
}
