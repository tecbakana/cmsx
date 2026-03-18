using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ICMS;

namespace CMSDAL
{
    public class AtributoDAL : IAtributoDAL
    {
        private IDbConnection conn;
        private IDbCommand cmd;
        private IDataParameter[] parms;
        private IDataFactory _factory;
        public int NumParms { get; set; }
        private dynamic _localProps;


        public AtributoDAL(IDataFactory factory)
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

        public void CriaAtributo()
        {
            //conn.ConnectionString = CONNSTRING;
            cmd.CommandType = CommandType.Text;

            string cmds = "INSERT INTO atributo(AtributoId,Nome,Descricao)";
            cmds += "VALUES (@AtributoId,@Nome,@Descricao)";

            cmd.CommandText = cmds;

            parms[0].ParameterName = "@AtributoId";
            parms[0].Value = _localProps.atributoId;
            parms[1].ParameterName = "@Nome";
            parms[1].Value = _localProps.nome;
            parms[2].ParameterName = "@Descricao";
            parms[2].Value = _localProps.descricao;

            cmd.Parameters.Add(parms[0]);
            cmd.Parameters.Add(parms[1]);
            cmd.Parameters.Add(parms[2]);

            cmd.Connection = conn;
            try
            {
                conn.Open();
                cmd.ExecuteScalar();
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
        }
        
        public DataTable ListaAtributos()
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT att.AtributoId,att.Nome,att.Descricao " +
                            "  FROM atributo att " ;
            cmd.Connection = conn;
            IDataReader _dr;
            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("AtributoId"));
            _dt.Columns.Add(new DataColumn("Nome"));
            _dt.Columns.Add(new DataColumn("Descricao"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();

                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["AtributoId"]    = _dr["AtributoId"].ToString();
                    dr["Nome"]          = _dr["Nome"].ToString();
                    dr["Descricao"]     = _dr["Descricao"].ToString();
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

        public DataTable ListaAtributosXProduto()
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT att.AtributoId,att.Nome,att.Descricao " +
                            "  FROM atributo att " +
                            " INNER JOIN relatributoxproduto rel  " +
                            "   ON att.AtributoId = rel.AtributoId" +
                            " WHERE rel.ProdutoId = @ProdutoId ";

            parms[0].ParameterName = "@ProdutoId";
            parms[0].Value = _localProps.prdId;

            cmd.Connection = conn;

            IDataReader _dr;
            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("AtributoId"));
            _dt.Columns.Add(new DataColumn("Nome"));
            _dt.Columns.Add(new DataColumn("Descricao"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();

                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["AtributoId"] = _dr["AtributoId"].ToString();
                    dr["Nome"] = _dr["Nome"].ToString();
                    dr["Descricao"] = _dr["Descricao"].ToString();
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

        public void InativaAtributo()
        {

            cmd.CommandType = CommandType.Text;

            string DataFinal = DateTime.Now.ToString("yyyy-MM-dd 00:00:00.000");
            cmd.CommandText = "UPDATE areas SET DataFinal = '" + DataFinal +"'" +
                            " WHERE areas.AreaId = @AreaId";

            parms[0].ParameterName = "@AreaId";
            parms[0].Value = _localProps.areaid;

            if (cmd.Parameters.Count <= 0)
            {
                cmd.Parameters.Add(parms[0]);
            }
            cmd.Connection = conn;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
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
        }


    }
}
