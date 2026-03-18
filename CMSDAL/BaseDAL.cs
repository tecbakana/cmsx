using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using ICMS;

namespace CMSDAL
{
    public class BaseDAL
    {
        public IDbConnection conn;
        public IDbCommand cmd;
        public IDataParameter[] parms;
        public IDataFactory _factory;
        public int NumParms { get; set; }
        public dynamic _localProps;
        public const string CONNSTRING = @"Data Source=MARCIO-PC\SQLEXPRESS;Initial Catalog=flexsolution11;User ID=flexsolution12;Password=flx0904";

    }
}
