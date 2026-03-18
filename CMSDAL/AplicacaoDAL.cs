using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ICMS;

namespace CMSDAL
{
    public class AplicacaoDAL : IAplicacaoDAL
    {

        private IDbConnection conn;
        private IDbCommand cmd;
        private IDataParameter[] parms;
        private IDataFactory _factory;
        private dynamic _localProps;

        public AplicacaoDAL(IDataFactory factory)
        {
            _factory = factory;
        }

        public void MakeConnection(dynamic prop)
        {
            var data = _factory.GetConnection(prop.banco, prop.parms);
            conn = data.Key;
            cmd  = data.Value.Key;
            parms = data.Value.Value;
            _localProps = prop;
        }

        public DataTable ListaAplicacao()
        {
            //conn.ConnectionString = CONNSTRING;
            cmd.CommandText = "SELECT * FROM aplicacao";
            cmd.Connection = conn;
            IDataReader _dr;

            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("AplicacaoId"));
            _dt.Columns.Add(new DataColumn("Nome"));
            _dt.Columns.Add(new DataColumn("Url"));
            _dt.Columns.Add(new DataColumn("DataFinal"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();
                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["Nome"] = _dr["Nome"].ToString();
                    dr["AplicacaoId"] = _dr["AplicacaoId"].ToString();
                    dr["Url"] = _dr["Url"].ToString();
                    dr["DataFinal"] = _dr["DataFinal"].ToString();
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

        public DataTable ListaAplicacaoForAutocomplete()
        {
            //conn.ConnectionString = CONNSTRING;
            cmd.CommandText = "SELECT Url FROM aplicacao";
            cmd.Connection = conn;
            IDataReader _dr;

            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("Url"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();
                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
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

        public DataTable ObtemAplicacaoPorId(Guid id)
        {
            //conn.ConnectionString = CONNSTRING;
            cmd.CommandText = "SELECT * FROM aplicacao  WHERE AplicacaoId = @appid";
            parms[0].ParameterName = "@appid";
            parms[0].Value = _localProps.appId;
            cmd.Parameters.Add(parms[0]);
            cmd.Connection = conn;
            IDataReader _dr;


            //throw new Exception("tratativa dentro da aplicacao" + parms[0].Value.ToString());

            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("AplicacaoId"));
            _dt.Columns.Add(new DataColumn("Nome"));
            _dt.Columns.Add(new DataColumn("Url"));
            _dt.Columns.Add(new DataColumn("DataFinal"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();
                while(_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["Nome"] = _dr["Nome"].ToString();
                    dr["AplicacaoId"] = _dr["AplicacaoId"].ToString();
                    dr["Url"] = _dr["Url"].ToString();
                    dr["DataFinal"] = _dr["DataFinal"].ToString();
                    _dt.Rows.Add(dr);
                }

                _dt.AcceptChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return _dt;
        }

        public Guid ObtemIdAplicacaoPorNome()
        {
            //conn.ConnectionString = CONNSTRING;
            cmd.CommandText = "SELECT * FROM aplicacao  WHERE Url = @urlcliente";
            parms[0].ParameterName = "@urlcliente";
            parms[0].Value = _localProps.urlcliente;
            cmd.Parameters.Add(parms[0]);

           // throw new Exception("tratativa dentro da aplicacao" + parms[0].Value);

            cmd.Connection = conn;
            IDataReader _dr;

            Guid _appid = new Guid();

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();
                while (_dr.Read())
                {
                    _appid = new Guid(_dr["AplicacaoId"].ToString());
                }
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
            return _appid;
        }
        
        public DataTable ListaAplicacaoPorNome()
        {
            //conn.ConnectionString = CONNSTRING;
            cmd.CommandText = "SELECT * FROM aplicacao  WHERE Url like @urlcliente";
            parms[0].ParameterName = "@urlcliente";
            parms[0].Value = "%" +  _localProps.urlcliente + "%";
            cmd.Parameters.Add(parms[0]);

            // throw new Exception("tratativa dentro da aplicacao" + parms[0].Value);

            cmd.Connection = conn;
            IDataReader _dr;

            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("AplicacaoId"));
            _dt.Columns.Add(new DataColumn("Nome"));
            _dt.Columns.Add(new DataColumn("Url"));
            _dt.Columns.Add(new DataColumn("DataFinal"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();
                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["Nome"] = _dr["Nome"].ToString();
                    dr["AplicacaoId"] = _dr["AplicacaoId"].ToString();
                    dr["Url"] = _dr["Url"].ToString();
                    dr["DataFinal"] = _dr["DataFinal"].ToString();
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

        public void InativaAplicacao()
        {

            parms[0].ParameterName = "@appId";
            parms[0].Value = _localProps.appId;
            parms[1].ParameterName = "@datafinal";

            int actv = _localProps.act;

            if (_localProps.act == 0)
            {
              parms[1].Value = DateTime.Today;
            }
            else
            {
              parms[1].Value = DBNull.Value;
            }

            cmd.CommandText = "UPDATE aplicacao SET DataFinal = @datafinal, isActive =" + actv.ToString() + " WHERE AplicacaoId = @appId";

            cmd.Parameters.Add(parms[0]);
            cmd.Parameters.Add(parms[1]);

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

        public void EditaAplicacao()
        {

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE aplicacao " +
                              "   SET Nome = @Nome," +
                              "       Url = @Url," +
                              "       LayoutChoose = @Layout " +
                              "       isActive = 1 " +
                              " WHERE AplicacaoId = @AplicacaoId";

            parms[0].ParameterName = "@AplicacaoId";
            parms[0].Value = _localProps.appid;
            parms[1].ParameterName = "@Layout";
            parms[1].Value = _localProps.layout;
            parms[2].ParameterName = "@Url";
            parms[2].Value = _localProps.url;
            parms[3].ParameterName = "@Nome";
            parms[3].Value = _localProps.nome;

            cmd.Connection = conn;
            cmd.Parameters.Add(parms[0]);
            cmd.Parameters.Add(parms[1]);
            cmd.Parameters.Add(parms[2]);
            cmd.Parameters.Add(parms[3]);

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

        public void CriaAplicacao()
        {
            cmd.CommandText = "INSERT INTO aplicacao(AplicacaoId,Nome,Url,IdUsuarioInicio,isActive) VALUES(@appId,@nome,@urlcliente,@idUsuario,1)";

            parms[0].ParameterName = "@appId";
            parms[0].Value = _localProps.appId;
            parms[1].ParameterName = "@nome";
            parms[1].Value = _localProps.appnome;
            parms[2].ParameterName = "@urlcliente";
            parms[2].Value = _localProps.appurl;
            parms[3].ParameterName = "@idUsuario";
            parms[3].Value = _localProps.admin;

            cmd.Parameters.Add(parms[0]);
            cmd.Parameters.Add(parms[1]);
            cmd.Parameters.Add(parms[2]);
            cmd.Parameters.Add(parms[3]);

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

        public void AtivaAplicacao()
        {
            cmd.CommandText = "UPDATE aplicacao SET DataFinal = null, isActive = 1 WHERE AplicacaoId = @appId";

            parms[0].ParameterName = "@appId";
            parms[0].Value = _localProps.appId;
            parms[1].ParameterName = "@datafinal";

            if (_localProps.act == 0)
            {
                parms[1].Value = DateTime.Today;
            }
            else
            {
                parms[1].Value = DBNull.Value;
            }

            cmd.Parameters.Add(parms[0]);
            cmd.Parameters.Add(parms[1]);

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
