using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using ICMS;

namespace CMSDAL
{
    public class AcessoDAL : BaseDAL,IAcessoDAL
    {

        /*private IDbConnection conn;
        private IDbCommand cmd;
        private IDataParameter[] parms;
        private IDataFactory _factory;
        public int NumParms { get; set; }
        private dynamic _localProps;*/

        #region IAcessoDAL Members

        public AcessoDAL(IDataFactory factory)
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

        public DataTable ValidaAcesso()
        {
            //conn.ConnectionString = CONNSTRING;
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "UsuarioAcesso";
            cmd.CommandType = CommandType.Text;
            cmd.CommandText =   "SELECT  us.UserId, rap.AplicacaoId,app.Url "+
                                "  FROM usuario  us"+
                                " INNER JOIN relusuarioaplicacao   rap"+
                                "    ON rap.UsuarioId = us.UserId" +
                                " INNER JOIN aplicacao   app" +
                                "    ON app.AplicacaoId = rap.AplicacaoId" +
                                " WHERE us.Apelido = @apelido"+
                                "   AND us.Senha   = @senha  "+
                                "   AND us.Ativo = 1"+
                                " GROUP BY UserId,rap.AplicacaoId,app.Url";

            parms[0].ParameterName = "@apelido";
            parms[0].Value = _localProps.apelido;
            parms[1].ParameterName = "@senha";
            parms[1].Value = _localProps.senha;

            cmd.Parameters.Add(parms[0]);
            cmd.Parameters.Add(parms[1]);

            cmd.Connection = conn;

            IDataReader _dr;
            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("UserId"));
            _dt.Columns.Add(new DataColumn("AplicacaoId"));
            _dt.Columns.Add(new DataColumn("Url"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();

                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["UserId"] = _dr["UserId"].ToString();
                    dr["AplicacaoId"] = _dr["AplicacaoId"].ToString();
                    dr["Url"] = _dr["Url"].ToString();
                    _dt.Rows.Add(dr);
                }

                _dt.AcceptChanges();
            }
            catch (DataException ex)
            {
                throw new DataException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return _dt;
        }

        #endregion
    }
}
