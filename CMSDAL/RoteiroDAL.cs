using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ICMS;

namespace CMSDAL
{
    public class RoteiroDAL : IRoteiroDAL
    {
        private IDbConnection conn; 
        private IDbCommand cmd;
        private IDataParameter[] parms;
        private IDataFactory _factory;
        public int NumParms { get; set; }
        private dynamic _localProps;

        public RoteiroDAL(IDataFactory factory)
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

        public void CriaArea()
        {
            throw new NotImplementedException();
        }

        #region ListaRoteiros
        public DataTable ListaRoteiros()
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT DISTINCT forn.forn,forn.id_forn,tabela.id_roteiro,tabela.roteiro, tabela.tipo_roteiro,tabela.cidorigem"+
                              "  FROM tabela(NOLOCK)"+
                              " INNER JOIN forn(NOLOCK)"+
                              "    ON forn.id_forn = id_forn_h1"+
                              " INNER JOIN cidade(NOLOCK)"+
                              "    ON cidade.id_cidade = tabela.id_cidade"+
                              "  LEFT JOIN roteiro(NOLOCK)"+
                              "    ON roteiro.id_roteiro = tabela.id_roteiro"+
                              " WHERE forn.id_cliente = @clienteId"+
                              "   AND tabela.id_cidade = (CASE WHEN ISNULL(@cidadeid,0) = 0 THEN forn.id_cidade ELSE @cidadeid END)"+
                              "   AND forn.id_forn = @fornid"+
                              "   AND tabela.fim >= GETDATE()"+
                              "   AND tabela.tipo_roteiro = @tiporoteiro"+
                              " GROUP BY tabela.roteiro,forn.forn,cidade.cidade,tabela.id_tabela,forn.id_forn,tabela.id_roteiro,tabela.tipo_roteiro,tabela.cidorigem"+
                              " HAVING COUNT(roteiro.id_roteiro) >= 1" +
                              " ORDER BY tabela.roteiro";

            parms[0].ParameterName = "@clienteId";
            parms[0].Value = _localProps.clienteId;
            cmd.Parameters.Add(parms[0]);
            parms[1].ParameterName = "@tabelaid";
            parms[1].Value = _localProps.tabelaid;
            cmd.Parameters.Add(parms[1]);
            parms[2].ParameterName = "@cidadeid";
            parms[2].Value = _localProps.cidadeid;
            cmd.Parameters.Add(parms[2]);
            parms[3].ParameterName = "@fornid";
            parms[3].Value = _localProps.fornid;
            cmd.Parameters.Add(parms[3]);
            parms[4].ParameterName = "@tiporoteiro";
            parms[4].Value = _localProps.tiporoteiro;
            cmd.Parameters.Add(parms[4]);


            cmd.Connection = conn;

            IDataReader _dr;
            DataTable _dt = new DataTable();
            //forn.forn,forn.id_forn,tabela.id_roteiro,tabela.roteiro, tabela.tipo_roteiro,tabela.cidorigem

            _dt.Columns.Add(new DataColumn("forn"));
            _dt.Columns.Add(new DataColumn("id_forn"));
            _dt.Columns.Add(new DataColumn("id_roteiro"));
            _dt.Columns.Add(new DataColumn("roteiro"));
            _dt.Columns.Add(new DataColumn("tipo_roteiro"));
            _dt.Columns.Add(new DataColumn("cidorigem"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();
                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["forn"] = _dr["forn"].ToString();
                    dr["id_forn"] = _dr["id_forn"].ToString();
                    dr["id_roteiro"] = _dr["id_roteiro"].ToString();
                    dr["roteiro"] = _dr["roteiro"].ToString();
                    dr["tipo_roteiro"] = _dr["tipo_roteiro"].ToString();
                    dr["cidorigem"] = _dr["cidorigem"].ToString();
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

        #region ListaRoteirosPorCidade
        public DataTable ListaRoteirosPorCidade()
        {
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "select DISTINCT tabela.tipo_roteiro, tabela.cidade,tabela.id_cidade, " +
                    "roteiro.img1a as img1a " +
                    "from tabela " +
                    "LEFT OUTER JOIN roteiro on tabela.id_roteiro = roteiro.id_roteiro " +
                    "where tabela.id_cliente = @clienteId and tabela.nac_inter = 'NACIONAL' and tabela.tipo_roteiro = 'RESORTS' and roteiro.menu = 1" +
                    " and tabela.fim >= GETDATE() order by tabela.cidade";

            parms[0].ParameterName = "@clienteId";
            parms[0].Value = _localProps.clienteId;
            cmd.Parameters.Add(parms[0]);
            parms[1].ParameterName = "@tabelaid";
            parms[1].Value = _localProps.tabelaid;
            cmd.Parameters.Add(parms[1]);

            cmd.Connection = conn;

            IDataReader _dr;
            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("cidade"));
            _dt.Columns.Add(new DataColumn("id_cidade"));
            _dt.Columns.Add(new DataColumn("img1a"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();
                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["cidade"] = _dr["cidade"].ToString();
                    dr["id_cidade"] = _dr["id_cidade"].ToString();
                    dr["img1a"] = _dr["img1a"].ToString();
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

        #region ListaRoteirosPorFornecedor
        public DataTable ListaRoteirosPorFornecedor()
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT DISTINCT forn.forn,forn.id_forn" +
                              "    FROM tabela(NOLOCK)" +
                              "   INNER JOIN forn(NOLOCK)" +
                              "      ON forn.id_forn = id_forn_h1" +
                              "   INNER JOIN cidade(NOLOCK)" +
                              "      ON cidade.id_cidade = tabela.id_cidade" +
                              "    LEFT JOIN roteiro(NOLOCK)" +
                              "      ON roteiro.id_roteiro = tabela.id_roteiro" +
                              "   WHERE forn.id_cliente = @clienteId" +
                              "     AND tabela.id_cidade = @cidadeid" +
                              "     AND forn.id_forn = (CASE WHEN @fornid='' THEN forn.id_forn ELSE @fornid END)" +
                              "     AND tabela.fim >= GETDATE()" +
                              "     AND tabela.tipo_roteiro = @tiporoteiro" +
                              "   GROUP BY forn.forn,forn.id_forn" +
                              "   HAVING COUNT(roteiro.id_roteiro) >= 1";

            parms[0].ParameterName = "@clienteId";
            parms[0].Value = _localProps.clienteId;
            parms[1].ParameterName = "@cidadeid";
            parms[1].Value = _localProps.cidadeId;
            parms[2].ParameterName = "@fornid";
            parms[2].Value = _localProps.fornecedorId;
            parms[3].ParameterName = "@tiporoteiro";
            parms[3].Value = _localProps.tiporoteiro;

            cmd.Connection = conn;
            cmd.Parameters.Add(parms[0]);
            cmd.Parameters.Add(parms[1]);
            cmd.Parameters.Add(parms[2]);
            cmd.Parameters.Add(parms[3]);

            cmd.Connection = conn;

            IDataReader _dr;
            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("fornecedor"));
            _dt.Columns.Add(new DataColumn("idfornecedor"));


            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();
                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["fornecedor"]   = _dr["forn"].ToString();
                    dr["idfornecedor"] = _dr["id_forn"].ToString();
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

        #region ListaDetalheRoteiro
        public DataTable ListaDetalheRoteiro()
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT DISTINCT id_forn_h1,roteiro,id_tabrot,cidorigem,id_cidorig,chave" +
                              "  FROM tabela(NOLOCK)" +
                              " WHERE tipo_roteiro = @tiporoteiro" +
                              "   AND id_cliente = @clienteid"+
                              "   AND id_cidade = @cidadeid" +
                              "   AND id_forn_h1 = @fornid" +
                              " GROUP BY id_forn_h1,roteiro,id_tabrot,cidorigem,id_cidorig,chave" +
                              " ORDER BY id_tabrot";

            parms[0].ParameterName = "@clienteId";
            parms[0].Value = _localProps.clienteId;
            parms[1].ParameterName = "@cidadeid";
            parms[1].Value = _localProps.cidadeId;
            parms[2].ParameterName = "@fornid";
            parms[2].Value = int.Parse(_localProps.fornecedorId);
            parms[3].ParameterName = "@tiporoteiro";
            parms[3].Value = _localProps.tiporoteiro;

            cmd.Connection = conn;
            cmd.Parameters.Add(parms[0]);
            cmd.Parameters.Add(parms[1]);
            cmd.Parameters.Add(parms[2]);
            cmd.Parameters.Add(parms[3]);

            cmd.Connection = conn;

            IDataReader _dr;
            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("idForn"));
            _dt.Columns.Add(new DataColumn("roteiro"));
            _dt.Columns.Add(new DataColumn("id_tabrot"));
            _dt.Columns.Add(new DataColumn("cidorigem"));
            _dt.Columns.Add(new DataColumn("id_cidorig"));
            _dt.Columns.Add(new DataColumn("chave"));


            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();
                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["idForn"] = _dr["id_forn_h1"].ToString();
                    dr["roteiro"] = _dr["roteiro"].ToString();
                    dr["id_tabrot"] = _dr["id_tabrot"].ToString();
                    dr["cidorigem"] = _dr["cidorigem"].ToString();
                    dr["id_cidorig"] = _dr["id_cidorig"].ToString();
                    dr["chave"] = _dr["chave"].ToString();
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

        #region ListaRoteirosPorId
        public DataTable ListaRoteirosPorId()
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Areas.AreaId, Areas.AplicacaoId,Areas.AreaIdPai, Areas.Nome, Areas.Url, Areas.Descricao, Areas.AreaIdPai,PAI.Nome AS 'NomePai',Areas.Imagem,Areas.MenuLateral" +
                            "  FROM Areas " +
                            " LEFT JOIN Areas PAI  " +
                            "   ON PAI.AreaId = Areas.AreaIdPai" +
                            " WHERE Areas.AreaId = @areaid AND Areas.DataFinal IS NULL";

            parms[0].ParameterName = "@areaid";
            parms[0].Value = _localProps.areaid;
            cmd.Parameters.Add(parms[0]);

            cmd.Connection = conn;

            IDataReader _dr;
            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("AplicacaoId"));
            _dt.Columns.Add(new DataColumn("AreaId"));
            _dt.Columns.Add(new DataColumn("AreaIdPai"));
            _dt.Columns.Add(new DataColumn("Nome"));
            _dt.Columns.Add(new DataColumn("Url"));
            _dt.Columns.Add(new DataColumn("Descricao"));
            _dt.Columns.Add(new DataColumn("NomePai"));
            _dt.Columns.Add(new DataColumn("Imagem"));
            _dt.Columns.Add(new DataColumn("MenuLateral"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();
                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["AplicacaoId"] = _dr["AplicacaoId"].ToString();
                    dr["AreaId"] = _dr["AreaId"].ToString();
                    dr["AreaIdPai"] = _dr["AreaIdPai"].ToString();
                    dr["Nome"] = _dr["Nome"].ToString();
                    dr["Url"] = _dr["Url"].ToString();
                    dr["Descricao"] = _dr["Descricao"].ToString();
                    dr["NomePai"] = _dr["NomePai"].ToString();
                    dr["Imagem"] = _dr["Imagem"].ToString();
                    dr["MenuLateral"] = _dr["MenuLateral"].ToString();
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

        /* DETALHES DO ROTEIRO  */


        #region Detalhe do roteiro por chave/id_mktinf
        public DataTable DetalheRoteiro()
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT tabela.id_mkt AS [idMkt], "+
                              "      mktinf.titulo1 AS [titulo],"+
                              "      mktinf.texto1 AS  [texto],"+
                              "  FROM tabela "+
                              "  LEFT OUTER JOIN mkt2 ON tabela.id_mkt = mkt2.id_mkt"+
                              "  LEFT OUTER JOIN mktinf ON mkt2.id_mktinf = mktinf.id_mktinf"+
                              "  WHERE tabela.id_cliente = @clienteid "+
                              "    AND tabela.chave = @chaveid";

            parms[0].ParameterName = "@clienteId";
            parms[0].Value = _localProps.clienteId;
            parms[1].ParameterName = "@chaveid";
            parms[1].Value = _localProps.chaveid;

            cmd.Connection = conn;
            cmd.Parameters.Add(parms[0]);
            cmd.Parameters.Add(parms[1]);

            cmd.Connection = conn;

            IDataReader _dr;
            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("idMkt"));
            _dt.Columns.Add(new DataColumn("titulo"));
            _dt.Columns.Add(new DataColumn("texto"));


            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();
                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["idMkt"] = _dr["idMkt"].ToString();
                    dr["titulo"] = _dr["titulo"].ToString();
                    dr["texto"] = _dr["texto"].ToString();
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
