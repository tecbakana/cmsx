using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using ICMSX;

namespace CMSXDAO
{
    public class BaseDAL
    {
        public IDbConnection conn;
        public IDbCommand cmd;
        public IDataParameter[] parms;
        public IDataFactory _factory;
        public int NumParms { get; set; }
        public dynamic _localProps;

        public BaseDAL(IDataFactory factory)
        {
            _factory = factory;
        }

        public void MakeConnection(dynamic prop)
        {
            var data = _factory.GetConnection(prop.banco, prop.parms);
            conn = data.Key;
            cmd = data.Value.Key;
            parms = data.Value.Value;
            _localProps = prop;
        }
    }
}
