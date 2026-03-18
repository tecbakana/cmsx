using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ICMSX;

namespace CMSXDAO
{
    public class OpcaoDAL : BaseDAL, IOpcaoDAL
    {
        public OpcaoDAL(IDataFactory factory) : base(factory) { }

        public void CriaOpcao()
        {
            throw new NotImplementedException();
        }

        public void InativaOpcao()
        {
            throw new NotImplementedException();
        }

        public DataTable ListaOpcoes()
        {
            throw new NotImplementedException();
        }
    }
}
