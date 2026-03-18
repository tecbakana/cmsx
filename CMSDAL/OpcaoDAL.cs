using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ICMS;

namespace CMSDAL
{
    public class OpcaoDAL : IOpcaoDAL
    {
        private IDbConnection conn;
        private IDbCommand cmd;
        private IDataParameter[] parms;
        private IDataFactory _factory;
        public int NumParms { get; set; }
        private dynamic _localProps;

        public OpcaoDAL(IDataFactory factory)
        {
            _factory = factory;
        }

        public void MakeConnection(dynamic prop)
        {
            var data = _factory.GetConnection(prop.banco,prop.parms);
            conn = data.Key;
            cmd  = data.Value.Key;
            parms = data.Value.Value;
            _localProps = prop;
        }


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
