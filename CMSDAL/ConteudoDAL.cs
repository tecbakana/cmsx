using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ICMS;

namespace CMSDAL
{
    public class ConteudoDAL : IConteudoDAL
    {

        private IDbConnection conn;
        private IDbCommand cmd;
        private IDataParameter[] parms;
        private IDataFactory _factory;
        public int NumParms { get; set; }
        private dynamic _localProps;

        public ConteudoDAL(IDataFactory factory)
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

        public void CriaConteudo(Guid areaid, Guid conteudoid, string titulo, string texto, string autor)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO conteudo (ConteudoId,AreaId,Autor,Titulo,Texto,DataInclusao)" +
                              "VALUES (@ConteudoId,@Areaid,@Autor,@Titulo,@Texto,@DataInclusao)";

            parms[0].ParameterName = "@ConteudoId";
            parms[0].Value = conteudoid;
            parms[1].ParameterName = "@AreaId";
            parms[1].Value = areaid;
            parms[2].ParameterName = "@Autor";
            parms[2].Value = autor;
            parms[3].ParameterName = "@Titulo";
            parms[3].Value = titulo;
            parms[4].ParameterName = "@Texto";
            parms[4].Value = texto;
            parms[5].ParameterName = "@DataInclusao";
            parms[5].Value = DateTime.Now.ToString("yyyy-MM-dd 00:00:00.000");

            cmd.Connection = conn;
            cmd.Parameters.Add(parms[0]);
            cmd.Parameters.Add(parms[1]);
            cmd.Parameters.Add(parms[2]);
            cmd.Parameters.Add(parms[3]);
            cmd.Parameters.Add(parms[4]);
            cmd.Parameters.Add(parms[5]);

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

        public void CriaValor()
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO conteudovalor (ConteudoId,UnidadeId,Valor)" +
                              "VALUES (@ConteudoId,@Unidadeid,@Valor)";

            parms[0].ParameterName = "@ConteudoId";
            parms[0].Value = _localProps.conteudoid;
            parms[1].ParameterName = "@UnidadeId";
            parms[1].Value = _localProps.unidadeid;
            parms[2].ParameterName = "@Valor";
            parms[2].Value = _localProps.valor;

            cmd.Connection = conn;
            cmd.Parameters.Add(parms[0]);
            cmd.Parameters.Add(parms[1]);
            cmd.Parameters.Add(parms[2]);

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

        public void EditaConteudo(Guid conteudoid, string titulo, string texto, string autor)
        {
            /*
            ConteudoId
            Autor
            Titulo
            Texto
            */
            //conn.ConnectionString = CONNSTRING;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE conteudo " +
                              "   SET Autor = @Autor," +
                              "       Titulo = @Titulo,"+
                              "       Texto = @Texto " +
                              " WHERE ConteudoId = @ConteudoId";

            parms[0].ParameterName = "@ConteudoId";
            parms[0].Value = conteudoid;
            parms[1].ParameterName = "@Autor";
            parms[1].Value = autor;
            parms[2].ParameterName = "@Titulo";
            parms[2].Value = titulo;
            parms[3].ParameterName = "@Texto";
            parms[3].Value = texto;

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

        public void EditaValor()
        {
            /*
                       ConteudoId
                       UnidadeId
                       Valor
                       */
            //conn.ConnectionString = CONNSTRING;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE conteudo " +
                              "   SET UnidadeId = @UnidadeId," +
                              "       Valor = @Valor" +
                              " WHERE ConteudoId = @ConteudoId";

            parms[0].ParameterName = "@ConteudoId";
            parms[0].Value = _localProps.conteudoid;
            parms[1].ParameterName = "@UnidadeId";
            parms[1].Value = _localProps.unidadeid;
            parms[2].ParameterName = "@Valor";
            parms[2].Value = _localProps.valor;

            cmd.Connection = conn;
            cmd.Parameters.Add(parms[0]);
            cmd.Parameters.Add(parms[1]);
            cmd.Parameters.Add(parms[2]);


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

        public object CreateContent()
        {

            object retval = null;

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO conteudo (ConteudoId,CategoriaId,AreaId,Autor,Titulo,Texto,DataInclusao)" +
                              "VALUES (@ConteudoId,@CategoriaId,@Areaid,@Autor,@Titulo,@Texto,@DataInclusao)";

            parms[0].ParameterName = "@ConteudoId";
            parms[0].Value = _localProps.conteudoid;
            parms[1].ParameterName = "@AreaId";
            parms[1].Value = _localProps.areaid;
            parms[2].ParameterName = "@Autor";
            parms[2].Value = _localProps.autor;
            parms[3].ParameterName = "@Titulo";
            parms[3].Value = _localProps.titulo;
            parms[4].ParameterName = "@Texto";
            parms[4].Value = _localProps.texto;
            parms[5].ParameterName = "@DataInclusao";
            parms[5].Value = DateTime.Now.ToString("yyyy-MM-dd 00:00:00.000");

            parms[6].ParameterName = "@CategoriaId";
            parms[6].Value = _localProps.categoriaId;

            cmd.Connection = conn;
            cmd.Parameters.Add(parms[0]);
            cmd.Parameters.Add(parms[1]);
            cmd.Parameters.Add(parms[2]);
            cmd.Parameters.Add(parms[3]);
            cmd.Parameters.Add(parms[4]);
            cmd.Parameters.Add(parms[5]);
            cmd.Parameters.Add(parms[6]);

            try
            {
                conn.Open();
                //cmd.ExecuteNonQuery();
                retval = cmd.ExecuteScalar();
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
            return retval;
        }

        public void EditContent()
        {
            /*
            ConteudoId
            Autor
            Titulo
            Texto
            */
            //conn.ConnectionString = CONNSTRING;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE conteudo " +
                              "   SET Autor = @Autor," +
                              "       Titulo = @Titulo," +
                              "       Texto = @Texto " +
                              " WHERE ConteudoId = @ConteudoId";

            parms[0].ParameterName = "@ConteudoId";
            parms[0].Value = _localProps.conteudoid;
            parms[1].ParameterName = "@Autor";
            parms[1].Value = _localProps.autor;
            parms[2].ParameterName = "@Titulo";
            parms[2].Value = _localProps.titulo;
            parms[3].ParameterName = "@Texto";
            parms[3].Value = _localProps.texto;

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

        public DataTable ListaConteudoPorAreaId()
        {
            //conn.ConnectionString = CONNSTRING;
            cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "ListaConteudo";
            cmd.CommandText = "SELECT conteudo.ConteudoId,conteudo.AreaId,Autor,Titulo,Texto,conteudo.DataInclusao, 'imagemnome', cv.UnidadeId, cv.Valor " +
                              "  FROM conteudo  " +
                              "  LEFT JOIN conteudovalor cv " +
                              "    ON cv.ConteudoId = conteudo.ConteudoId " +
                              " WHERE conteudo.AreaId = @AreaId AND DataFinal IS NULL";

            parms[0].ParameterName = "@AreaId";
            parms[0].Value = _localProps.areaid;
            parms[1].ParameterName = "@Ativo";
            parms[1].Value = _localProps.ativos;

            cmd.Connection = conn;
            cmd.Parameters.Add(parms[0]);
            cmd.Parameters.Add(parms[1]);

            IDataReader _dr;
            DataTable _dt = new DataTable();
            //ConteudoId,AreaId,Autor,Titulo,Texto,DataInclusao
            _dt.Columns.Add(new DataColumn("ConteudoId"));
            _dt.Columns.Add(new DataColumn("AreaId"));
            _dt.Columns.Add(new DataColumn("Autor"));
            _dt.Columns.Add(new DataColumn("Titulo"));
            _dt.Columns.Add(new DataColumn("Texto"));
            _dt.Columns.Add(new DataColumn("DataInclusao"));
            _dt.Columns.Add(new DataColumn("imagemnome"));
            _dt.Columns.Add(new DataColumn("UnidadeId"));
            _dt.Columns.Add(new DataColumn("Valor"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();
                while (_dr.Read())
                {
                    DataRow dr          = _dt.NewRow();
                    dr["ConteudoId"]    = _dr["ConteudoId"].ToString();
                    dr["AreaId"]        = _dr["AreaId"].ToString();
                    dr["Autor"]         = _dr["Autor"].ToString();
                    dr["Titulo"]        = _dr["Titulo"].ToString();
                    dr["Texto"]         = _dr["Texto"].ToString();
                    dr["DataInclusao"]  = _dr["DataInclusao"].ToString();
                    dr["imagemnome"]    = _dr["imagemnome"].ToString();
                    dr["UnidadeId"]     = _dr["UnidadeId"].ToString();
                    dr["Valor"]         = _dr["Valor"].ToString();
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

        public DataTable ListaConteudoPorCategoriaId()
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT conteudo.ConteudoId,conteudo.AreaId,Autor,Titulo,Texto,conteudo.DataInclusao, 'imagemnome', cv.UnidadeId, cv.Valor " +
                              "  FROM conteudo  " +
                              "  LEFT JOIN conteudovalor cv " +
                              "    ON cv.ConteudoId = conteudo.ConteudoId " +
                              " WHERE conteudo.CategoriaId = @CategoriaId AND DataFinal IS NULL";

            parms[0].ParameterName = "@CategoriaId";
            parms[0].Value = _localProps.categoriaId;
            parms[1].ParameterName = "@Ativo";
            parms[1].Value = _localProps.ativos;

            cmd.Connection = conn;
            cmd.Parameters.Add(parms[0]);
            cmd.Parameters.Add(parms[1]);

            IDataReader _dr;
            DataTable _dt = new DataTable();
            //ConteudoId,AreaId,Autor,Titulo,Texto,DataInclusao
            _dt.Columns.Add(new DataColumn("ConteudoId"));
            _dt.Columns.Add(new DataColumn("AreaId"));
            _dt.Columns.Add(new DataColumn("Autor"));
            _dt.Columns.Add(new DataColumn("Titulo"));
            _dt.Columns.Add(new DataColumn("Texto"));
            _dt.Columns.Add(new DataColumn("DataInclusao"));
            _dt.Columns.Add(new DataColumn("imagemnome"));
            _dt.Columns.Add(new DataColumn("UnidadeId"));
            _dt.Columns.Add(new DataColumn("Valor"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();
                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["ConteudoId"] = _dr["ConteudoId"].ToString();
                    dr["AreaId"] = _dr["AreaId"].ToString();
                    dr["Autor"] = _dr["Autor"].ToString();
                    dr["Titulo"] = _dr["Titulo"].ToString();
                    dr["Texto"] = _dr["Texto"].ToString();
                    dr["DataInclusao"] = _dr["DataInclusao"].ToString();
                    dr["imagemnome"] = _dr["imagemnome"].ToString();
                    dr["UnidadeId"] = _dr["UnidadeId"].ToString();
                    dr["Valor"] = _dr["Valor"].ToString();
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

        public DataTable ObtemConteudoPorId()
        {
            //conn.ConnectionString = CONNSTRING;
            cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "ListaConteudo";
            cmd.CommandText = "SELECT conteudo.ConteudoId,conteudo.AreaId,Autor,Titulo,Texto,conteudo.DataInclusao, 'imagemnome'" +
                              "  FROM conteudo  " +
                              " WHERE conteudo.ConteudoId = @ConteudoId AND DataFinal IS NULL";

            parms[0].ParameterName = "@ConteudoId";
            parms[0].Value = _localProps.conteudoId;
            parms[1].ParameterName = "@Ativo";
            parms[1].Value = _localProps.ativos;

            cmd.Connection = conn;
            cmd.Parameters.Add(parms[0]);
            cmd.Parameters.Add(parms[1]);

            IDataReader _dr;
            DataTable _dt = new DataTable();
            //ConteudoId,AreaId,Autor,Titulo,Texto,DataInclusao
            _dt.Columns.Add(new DataColumn("ConteudoId"));
            _dt.Columns.Add(new DataColumn("AreaId"));
            _dt.Columns.Add(new DataColumn("Autor"));
            _dt.Columns.Add(new DataColumn("Titulo"));
            _dt.Columns.Add(new DataColumn("Texto"));
            _dt.Columns.Add(new DataColumn("DataInclusao"));
            _dt.Columns.Add(new DataColumn("imagemnome"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();
                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["ConteudoId"] = _dr["ConteudoId"].ToString();
                    dr["AreaId"] = _dr["AreaId"].ToString();
                    dr["Autor"] = _dr["Autor"].ToString();
                    dr["Titulo"] = _dr["Titulo"].ToString();
                    dr["Texto"] = _dr["Texto"].ToString();
                    dr["DataInclusao"] = _dr["DataInclusao"].ToString();
                    dr["imagemnome"] = _dr["imagemnome"].ToString();
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

        public DataTable ListaValor()
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT conteudovalor.ConteudoId,conteudovalor.AreaId,conteudovalor.UnidadeId" +
                              "  FROM conteudovalor " +
                              " WHERE conteudovalor.ConteudoId = @ConteudoId";

            parms[0].ParameterName = "@ConteudoId";
            parms[0].Value = _localProps.conteudoid;
            parms[1].ParameterName = "@Ativo";
            parms[1].Value = _localProps.ativos;

            cmd.Connection = conn;
            cmd.Parameters.Add(parms[0]);
            cmd.Parameters.Add(parms[1]);

            IDataReader _dr;
            DataTable _dt = new DataTable();
            //ConteudoId,AreaId,Autor,Titulo,Texto,DataInclusao
            _dt.Columns.Add(new DataColumn("ConteudoId"));
            _dt.Columns.Add(new DataColumn("UnidadeId"));
            _dt.Columns.Add(new DataColumn("Valor"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();
                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["ConteudoId"] = _dr["ConteudoId"].ToString();
                    dr["UnidadeId"] = _dr["UnidadeId"].ToString();
                    dr["Valor"] = _dr["Valor"].ToString();
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

        public void InativaConteudo()
        {
            //conn.ConnectionString = CONNSTRING;
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "UPDATE conteudo SET DataFinal = '" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00.000") + "'" +
                            " WHERE conteudo.ConteudoId = @ConteudoId";

            parms[0].ParameterName = "@ConteudoId";
            parms[0].Value = _localProps.conteudoid;

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
