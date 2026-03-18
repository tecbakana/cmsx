using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Npgsql;
using ICMSX;

namespace CMSXDAO
{
    public class DataFactory : IDataFactory
    {
        private readonly IConfiguration _configuration;

        public DataFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int NumParms { get; set; }

        #region IDataFactory Members

        public KeyValuePair<IDbConnection, KeyValuePair<IDbCommand, IDataParameter[]>> GetConnection(string banco, int nparm)
        {
            NumParms = nparm;

            switch (banco)
            {
                case "MySql":
                    return MySqlCon();
                case "PGSql":
                    return PGSql();
                default:
                    return SqlServerCon();
            }
        }

        public KeyValuePair<IDbConnection, KeyValuePair<IDbCommand, IDataParameter[]>> SqlServerCon()
        {
            var conn = new SqlConnection();
            var cmd = new SqlCommand();
            var parm = new List<SqlParameter>();

            for (int x = 0; x < NumParms; x++)
            {
                parm.Add(new SqlParameter());
            }

            conn.ConnectionString = _configuration.GetConnectionString("SqlServer");

            return new KeyValuePair<IDbConnection, KeyValuePair<IDbCommand, IDataParameter[]>>(
                conn, new KeyValuePair<IDbCommand, IDataParameter[]>(cmd, parm.ToArray()));
        }

        public KeyValuePair<IDbConnection, KeyValuePair<IDbCommand, IDataParameter[]>> MySqlCon()
        {
            var conn = new MySqlConnection();
            var cmd = new MySqlCommand();
            var parm = new List<MySqlParameter>();

            for (int x = 0; x < NumParms; x++)
            {
                parm.Add(new MySqlParameter());
            }

            conn.ConnectionString = _configuration.GetConnectionString("MySql");

            return new KeyValuePair<IDbConnection, KeyValuePair<IDbCommand, IDataParameter[]>>(
                conn, new KeyValuePair<IDbCommand, IDataParameter[]>(cmd, parm.ToArray()));
        }

        public KeyValuePair<IDbConnection, KeyValuePair<IDbCommand, IDataParameter[]>> PGSql()
        {
            var conn = new NpgsqlConnection();
            var cmd = new NpgsqlCommand();
            var parm = new List<NpgsqlParameter>();

            for (int x = 0; x < NumParms; x++)
            {
                parm.Add(new NpgsqlParameter());
            }

            conn.ConnectionString = _configuration.GetConnectionString("NPGSQL");

            return new KeyValuePair<IDbConnection, KeyValuePair<IDbCommand, IDataParameter[]>>(
                conn, new KeyValuePair<IDbCommand, IDataParameter[]>(cmd, parm.ToArray()));
        }

        #endregion
    }
}
