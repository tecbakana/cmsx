using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ICMS;

namespace CMSDAL
{
    public class ArquivoDAL : IArquivoDAL
    {
        private IDbConnection conn;
        private IDbCommand cmd;
        private IDataParameter[] parms;
        private IDataFactory _factory;
        public int NumParms { get; set; }
        private dynamic _localProps;

        public ArquivoDAL(IDataFactory factory)
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


        #region IArquivoDAL Members

        /* 
         * ESTRUTURA DA TABELA Arquivo
            ArquivoId,
            AreaId,
            ConteudoId,
            Nome,
            TipoId
         * 
         */

        public void CriaArquivo()
        {
            cmd.CommandType = CommandType.Text;
            string cmds = "INSERT INTO arquivo(ArquivoId,AreaId,ConteudoId,Nome,Tipo)" +
                          "VALUES(@arquivoid,@areaid,@conteudoid,@nome,@tipo)";

            parms[0].ParameterName = "@arquivoid";
            parms[0].Value = _localProps.arquivoid;
            parms[1].ParameterName = "@areaid";
            parms[1].Value = _localProps.areaid;
            parms[2].ParameterName = "@conteudoid";
            parms[2].Value = _localProps.conteudoid;
            parms[3].ParameterName = "@nome";
            parms[3].Value = _localProps.nome;
            parms[4].ParameterName = "@tipo";
            parms[4].Value = _localProps.tipo;

            foreach (IDataParameter parm in parms)
            {
                cmd.Parameters.Add(parm);
            }

            cmd.CommandText = cmds;
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

        public DataTable ListaArquivoPorAplicacaoId()
        {
             throw new NotImplementedException();
        }

        public DataTable ListaArquivoPorAreaId()
        {
           
            DataTable dt = new DataTable();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM arquivo WHERE AreaId = @areaid";
            
            parms[0].ParameterName = "@areaid";
            parms[0].Value = _localProps.areaid;

            cmd.Parameters.Add(parms[0]);
            cmd.Connection = conn;

            dt.Columns.Add("ArquivoId");
            dt.Columns.Add("AreaId");
            dt.Columns.Add("Nome");
            dt.Columns.Add("TipoId");

            try
            {
                conn.Open();
                IDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    DataRow _dr = dt.NewRow();
                    _dr["ArquivoId"] = dr["Arquivoid"];
                    _dr["AreaId"] = dr["AreaId"];
                    _dr["Nome"] = dr["Nome"];
                    _dr["TipoId"] = dr["TipoId"];
                    dt.Rows.Add(_dr);
                }
                dt.AcceptChanges();

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

            return dt;
        }

        public DataTable ListaArquivoPorConteudoId()
        {
            DataTable dt = new DataTable();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM arquivo WHERE ConteudoId = @conteudoid";

            parms[0].ParameterName = "@conteudoid";
            parms[0].Value = _localProps.conteudoid;

            cmd.Parameters.Add(parms[0]);
            cmd.Connection = conn;

            dt.Columns.Add("ArquivoId");
            dt.Columns.Add("ConteudoId");
            dt.Columns.Add("Nome");
            dt.Columns.Add("TipoId");

            try
            {
                conn.Open();
                IDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    DataRow _dr = dt.NewRow();
                    _dr["ArquivoId"] = dr["Arquivoid"];
                    _dr["ConteudoId"] = dr["ConteudoId"];
                    _dr["Nome"] = dr["Nome"];
                    _dr["TipoId"] = dr["TipoId"];
                    dt.Rows.Add(_dr);
                }
                dt.AcceptChanges();

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

            return dt;
        }

        public DataTable ObtemArquivoPorId()
        {
            DataTable dt = new DataTable();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM arquivo WHERE ArquivoId = @arquivoid";

            parms[0].ParameterName = "@arquivoid";
            parms[0].Value = _localProps.arquivoid;

            cmd.Parameters.Add(parms[0]);
            cmd.Connection = conn;

            dt.Columns.Add("ArquivoId");
            dt.Columns.Add("ConteudoId");
            dt.Columns.Add("Nome");
            dt.Columns.Add("TipoId");

            try
            {
                conn.Open();
                IDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    DataRow _dr = dt.NewRow();
                    _dr["ArquivoId"] = dr["Arquivoid"];
                    _dr["ConteudoId"] = dr["ConteudoId"];
                    _dr["Nome"] = dr["Nome"];
                    _dr["TipoId"] = dr["TipoId"];
                    dt.Rows.Add(_dr);
                }
                dt.AcceptChanges();

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

            return dt;
        }

        public DataTable ObtemArquivoPorNome()
        {
            DataTable dt = new DataTable();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM arquivo WHERE Nome = @nome";

            parms[0].ParameterName = "@nome";
            parms[0].Value = _localProps.nome;

            cmd.Parameters.Add(parms[0]);
            cmd.Connection = conn;

            dt.Columns.Add("ArquivoId");
            dt.Columns.Add("ConteudoId");
            dt.Columns.Add("Nome");
            dt.Columns.Add("TipoId");

            try
            {
                conn.Open();
                IDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    DataRow _dr = dt.NewRow();
                    _dr["ArquivoId"] = dr["Arquivoid"];
                    _dr["ConteudoId"] = dr["ConteudoId"];
                    _dr["Nome"] = dr["Nome"];
                    _dr["TipoId"] = dr["TipoId"];
                    dt.Rows.Add(_dr);
                }
                dt.AcceptChanges();

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

            return dt;
        }

        #endregion
    }
}
