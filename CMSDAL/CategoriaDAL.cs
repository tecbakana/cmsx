using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ICMS;


namespace CMSDAL
{
    public class CategoriaDAL : ICategoriaDAL
    {

        private IDbConnection conn;
        private IDbCommand cmd;
        private IDataParameter[] parms;
        private IDataFactory _factory;
        public int NumParms { get; set; }
        private dynamic _localProps;

        public CategoriaDAL(IDataFactory factory)
        {
            _factory = factory;
        }

        public object CriaCategoria()
        {
            object retval;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO categoria (CategoriaId,CategoriaIdPai,AplicacaoId,Nome,Descricao,TipoCategoria)" +
                              "VALUES (@CategoriaId,@CategoriaIdPai,@AplicacaoId,@Nome,@Descricao,@TipoCategoria)";

            parms[0].ParameterName = "@CategoriaId";
            parms[0].Value = _localProps.categoriaid;
            parms[1].ParameterName = "@Nome";
            parms[1].Value = _localProps.nome;
            parms[2].ParameterName = "@Descricao";
            parms[2].Value = _localProps.descricao;
            parms[3].ParameterName = "@TipoCategoria";
            parms[3].Value = _localProps.tipoCategoria;
            parms[4].ParameterName = "@AplicacaoId";
            parms[4].Value = _localProps.aplicacaoId;
            parms[5].ParameterName = "@CategoriaIdPai";
            parms[5].Value = _localProps.categoriaIdPai;

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

        public void EditCategoria()
        {
            throw new NotImplementedException();
        }

        public DataTable ListaCategoriasPai()
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT categoria.CategoriaId,categoria.Nome, '' as 'NomePai',categoria.Descricao,categoria.TipoCategoria,categoria.AplicacaoId " +
                              "  FROM categoria  " +
                              " WHERE categoria.CategoriaIdPai IS NULL";

            IDataReader _dr;
            DataTable _dt = new DataTable();
            cmd.Connection = conn;

            //ConteudoId,AreaId,Autor,Titulo,Texto,DataInclusao
            _dt.Columns.Add(new DataColumn("CategoriaId"));
            _dt.Columns.Add(new DataColumn("Nome"));
            _dt.Columns.Add(new DataColumn("Descricao"));
            _dt.Columns.Add(new DataColumn("TipoCategoria"));
            _dt.Columns.Add(new DataColumn("AplicacaoId"));
            _dt.Columns.Add(new DataColumn("NomePai"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();
                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["CategoriaId"] = _dr["CategoriaId"].ToString();
                    dr["Nome"] = _dr["Nome"].ToString();
                    dr["Descricao"] = _dr["Descricao"].ToString();
                    dr["TipoCategoria"] = _dr["TipoCategoria"].ToString();
                    dr["AplicacaoId"] = _dr["AplicacaoId"].ToString();
                    dr["NomePai"] = _dr["NomePai"].ToString();
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

        public DataTable ListaSubCategorias()
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT categoria.CategoriaId,categoria.Nome, '' as 'NomePai',categoria.Descricao,categoria.TipoCategoria,categoria.AplicacaoId " +
                              "  FROM categoria  " +
                              " WHERE categoria.CategoriaIdPai = @cpid";

            parms[0].ParameterName = "@cpid";
            parms[0].Value = _localProps.cpid;

            IDataReader _dr;
            DataTable _dt = new DataTable();
            cmd.Connection = conn;
            cmd.Parameters.Add(parms[0]);

            //ConteudoId,AreaId,Autor,Titulo,Texto,DataInclusao
            _dt.Columns.Add(new DataColumn("CategoriaId"));
            _dt.Columns.Add(new DataColumn("Nome"));
            _dt.Columns.Add(new DataColumn("Descricao"));
            _dt.Columns.Add(new DataColumn("TipoCategoria"));
            _dt.Columns.Add(new DataColumn("AplicacaoId"));
            _dt.Columns.Add(new DataColumn("NomePai"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();
                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["CategoriaId"] = _dr["CategoriaId"].ToString();
                    dr["Nome"] = _dr["Nome"].ToString();
                    dr["Descricao"] = _dr["Descricao"].ToString();
                    dr["TipoCategoria"] = _dr["TipoCategoria"].ToString();
                    dr["AplicacaoId"] = _dr["AplicacaoId"].ToString();
                    dr["NomePai"] = _dr["NomePai"].ToString();
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

        public DataTable ListaCategorias()
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT cat.CategoriaId,cat.Nome,cat.Descricao,cp.Nome as 'NomePai',cat.TipoCategoria,cat.AplicacaoID " +
                              "  FROM categoria cat " +
                              "  LEFT JOIN categoria cp " +
                              "    ON cp.CategoriaId = cat.CategoriaIdPai " +
                              " WHERE cat.AplicacaoId = @AplicacaoId OR @AplicacaoId IS NULL";

            cmd.Connection = conn;

            parms[0].ParameterName = "@AplicacaoId";
            parms[0].Value = _localProps.appid;

            cmd.Parameters.Add(parms[0]);

            IDataReader _dr;
            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("CategoriaId"));
            _dt.Columns.Add(new DataColumn("Nome"));
            _dt.Columns.Add(new DataColumn("Descricao"));
            _dt.Columns.Add(new DataColumn("TipoCategoria"));
            _dt.Columns.Add(new DataColumn("AplicacaoId"));
            _dt.Columns.Add(new DataColumn("NomePai"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();
                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["CategoriaId"] = _dr["CategoriaId"].ToString();
                    dr["Nome"] = _dr["Nome"].ToString();
                    dr["Descricao"] = _dr["Descricao"].ToString();
                    dr["TipoCategoria"] = _dr["TipoCategoria"].ToString();
                    dr["AplicacaoId"] = _dr["AplicacaoId"].ToString();
                    dr["NomePai"] = _dr["NomePai"].ToString();
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

        public DataTable ListaCategoriasFull()
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = " SELECT DISTINCT cat.CategoriaId," +
                              "       cat.Nome, " +
                              "       cat.Descricao, " +
                              "       cat.TipoCategoria, " +
                              "       cat.CategoriaIdPai, " +
                              "       sub.CategoriaId    as 'sCategoriaId', " +
                              "       sub.Nome          as 'sNome', " +
                              "       sub.Descricao     as 'sDescricao', " +
                              "       sub.TipoCategoria as 'sTipoCategoria', " +
                              "       sub.CategoriaIdPai as 'sCategoriaIdPai', " +
                              "       cat.AplicacaoId " +
                              "    FROM categoria cat    " +
                              "    LEFT join categoria sub " +
                              "      ON cat.CategoriaId = sub.CategoriaIdPai" +
                              " WHERE cat.AplicacaoId = @AplicacaoId OR @AplicacaoId IS NULL ";

            cmd.Connection = conn;

            parms[0].ParameterName = "@AplicacaoId";
            parms[0].Value = _localProps.appid;

            cmd.Parameters.Add(parms[0]);

            IDataReader _dr;
            DataTable _dt = new DataTable();

            _dt.TableName = "Categoria_full";

            _dt.Columns.Add(new DataColumn("CategoriaId"));
            _dt.Columns.Add(new DataColumn("Nome"));
            _dt.Columns.Add(new DataColumn("Descricao"));
            _dt.Columns.Add(new DataColumn("TipoCategoria"));
            _dt.Columns.Add(new DataColumn("CategoriaIdPai"));

            _dt.Columns.Add(new DataColumn("sCategoriaId"));
            _dt.Columns.Add(new DataColumn("sNome"));
            _dt.Columns.Add(new DataColumn("sDescricao"));
            _dt.Columns.Add(new DataColumn("sTipoCategoria"));
            _dt.Columns.Add(new DataColumn("sCategoriaIdPai"));

            _dt.Columns.Add(new DataColumn("AplicacaoId"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();
                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["CategoriaId"]       = _dr["CategoriaId"].ToString();
                    dr["Nome"]              = _dr["Nome"].ToString();
                    dr["Descricao"]         = _dr["Descricao"].ToString();
                    dr["TipoCategoria"]     = _dr["TipoCategoria"].ToString();
                    dr["CategoriaIdPai"]    = _dr["CategoriaIdPai"].ToString();

                    dr["sCategoriaId"]       = _dr["sCategoriaId"].ToString();
                    dr["sNome"]              = _dr["sNome"].ToString();
                    dr["sDescricao"]         = _dr["sDescricao"].ToString();
                    dr["sTipoCategoria"]     = _dr["sTipoCategoria"].ToString();
                    dr["sCategoriaIdPai"]    = _dr["sCategoriaIdPai"].ToString();

                    dr["AplicacaoId"]       = _dr["AplicacaoId"].ToString();
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

        public DataTable ListaCategoriasPorId()
        {
            //Categoria (CategoriaId,Nome,Descricao,TipoCategoria)
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT cat.CategoriaId,cat.Nome,cp.Nome as 'NomePai',cat.Descricao,cat.TipoCategoria " +
                              "  FROM categoria  " +
                              "  LEFT JOIN categoria cp " +
                              "    ON cp.CategoriaId = cat.CategoriaIdPai " +
                              " WHERE cat.CategoriaId = @CategoriaId OR @CategoriaId IS NULL";

            parms[0].ParameterName = "@CategoriaId";
            parms[0].Value = _localProps.areaid;

            cmd.Connection = conn;
            cmd.Parameters.Add(parms[0]);

            IDataReader _dr;
            DataTable _dt = new DataTable();
            //ConteudoId,AreaId,Autor,Titulo,Texto,DataInclusao
            _dt.Columns.Add(new DataColumn("CategoriaId"));
            _dt.Columns.Add(new DataColumn("Nome"));
            _dt.Columns.Add(new DataColumn("Descricao"));
            _dt.Columns.Add(new DataColumn("TipoCategoria"));
            _dt.Columns.Add(new DataColumn("NomePai"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();
                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["CategoriaId"] = _dr["CategoriaId"].ToString();
                    dr["Nome"] = _dr["Nome"].ToString();
                    dr["Descricao"] = _dr["Descricao"].ToString();
                    dr["TipoCategoria"] = _dr["TipoCategoria"].ToString();
                    dr["NomePai"] = _dr["NomePai"].ToString();
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
