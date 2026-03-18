using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ICMS;

namespace CMSDAL
{
    public class ResortDAL : IResortDAL
    {
        private IDbConnection conn;
        private IDbCommand cmd;
        private IDataParameter[] parms;
        private IDataFactory _factory;
        public int NumParms { get; set; }
        private dynamic _localProps;

        public const string CONNSTRING = @"Data Source=MARCIO-PC\SQLEXPRESS;Initial Catalog=flexsolution11;User ID=flexsolution12;Password=flx0904";

        public ResortDAL(IDataFactory factory)
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

        public DataTable ListaResortPorRoteiro()
        {
            cmd.CommandType = CommandType.Text;


            cmd.CommandText = "SELECT DISTINCT forn.forn,forn.id_forn,tabela.id_roteiro,tabela.roteiro, tabela.tipo_roteiro,tabela.cidorigem" +
                              "    FROM tabela(NOLOCK)" +
                              "   INNER JOIN forn(NOLOCK)" +
                              "      ON forn.id_forn = id_forn_h1" +
                              "   INNER JOIN cidade(NOLOCK)" +
                              "      ON cidade.id_cidade = tabela.id_cidade" +
                              "    LEFT JOIN roteiro(NOLOCK)" +
                              "      ON roteiro.id_roteiro = tabela.id_roteiro" +
                              "   WHERE forn.id_cliente = @clienteId" +
                              "     AND forn.id_cidade = (CASE WHEN ISNULL(@cidadeid,0) = 0 THEN forn.id_cidade ELSE @cidadeid END)" +
                              //"     AND forn.id_forn = @fornid" +
                              "     AND tabela.fim >= GETDATE()" +
                              "     AND tabela.tipo_roteiro = @tiporoteiro" +
                              "   GROUP BY tabela.roteiro,forn.forn,cidade.cidade,tabela.id_tabela,forn.id_forn,tabela.id_roteiro,tabela.tipo_roteiro,tabela.cidorigem" +
                              "   HAVING COUNT(roteiro.id_roteiro) >= 1" +
                              "   ORDER BY tabela.roteiro";




            parms[0].ParameterName = "@clienteId";
            parms[0].Value = _localProps.clienteId;
            parms[1].ParameterName = "@cidadeid";
            parms[1].Value = _localProps.cidadeId;
            parms[1].ParameterName = "@fornid";
            parms[1].Value = _localProps.fornecedorId;
            parms[1].ParameterName = "@tiporoteiro";
            parms[1].Value = _localProps.tiporoteiro;

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

        public DataTable ListaFornecedor()
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
                              "     AND forn.id_cidade = (CASE WHEN ISNULL(@fornid,'') = '' THEN forn.id_cidade ELSE @fornid END)" +
                              "     AND tabela.fim >= GETDATE()" +
                              "     AND tabela.tipo_roteiro = @tiporoteiro" +
                              "   GROUP BY tabela.roteiro,forn.forn,cidade.cidade,tabela.id_tabela,forn.id_forn,tabela.id_roteiro,tabela.tipo_roteiro,tabela.cidorigem" +
                              "   HAVING COUNT(roteiro.id_roteiro) >= 1" +
                              "   ORDER BY tabela.roteiro";




            parms[0].ParameterName = "@clienteId";
            parms[0].Value = _localProps.clienteId;
            parms[1].ParameterName = "@cidadeid";
            parms[1].Value = _localProps.cidadeId;
            parms[1].ParameterName = "@fornid";
            parms[1].Value = _localProps.fornecedorId;
            parms[1].ParameterName = "@tiporoteiro";
            parms[1].Value = _localProps.tiporoteiro;

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
        /* DETALHES DO ROTEIRO  */


        #region Detalhe do resort por chave/id_mktinf
        public DataTable DetalheResort()
        {
            cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "SELECT tabela.id_mkt AS [idMkt], " +
            //                  "      mktinf.titulo1 AS [titulo]," +
            //                  "      mktinf.texto1 AS  [texto]" +
            //                  "  FROM tabela " +
            //                  "  LEFT OUTER JOIN mkt2 ON tabela.id_mkt = mkt2.id_mkt" +
            //                  "  LEFT OUTER JOIN mktinf ON mkt2.id_mktinf = mktinf.id_mktinf" +
            //                  "  WHERE tabela.id_cliente = @clienteid " +
            //                  "    AND tabela.id_cidorig = @cidorigid" +
            //                  "    AND tabela.id_tabrot = @idtabrot";

            cmd.CommandText = "SELECT tabela.roteiro AS [titulo], " +
                  "     tabela.subtitulo AS [subtitulo]," +
                  "      mktinf.texto1 AS  [texto]," +
                  "      tabela.acomods_h1 AS [acomodacoes]," + 
                  "      tabela.regime_h1 AS [regime]"+
                  "  FROM tabela " +
                  "  LEFT OUTER JOIN mkt2 ON tabela.id_mkt = mkt2.id_mkt" +
                  "  LEFT OUTER JOIN mktinf ON mkt2.id_mktinf = mktinf.id_mktinf" +
                  "  WHERE tabela.id_cliente = @clienteid " +
                  "    AND tabela.id_cidorig = @cidorigid" +
                  "    AND tabela.id_tabrot = @idtabrot" +
                  "    AND tabela.dt_saida BETWEEN @dataini AND @datafim";


            parms[0].ParameterName = "@clienteId";
            parms[0].Value = _localProps.clienteId;
            parms[1].ParameterName = "@cidorigid";
            parms[1].Value = _localProps.cidorig;
            parms[2].ParameterName = "@idtabrot";
            parms[2].Value = _localProps.idtabrot;
            parms[3].ParameterName = "@dataini";
            parms[3].Value = _localProps.dtini;
            parms[3].DbType = DbType.DateTime;
            parms[4].ParameterName = "@datafim";
            parms[4].Value = _localProps.dtfim;
            parms[4].DbType = DbType.DateTime;

            cmd.Connection = conn;
            cmd.Parameters.Add(parms[0]);
            cmd.Parameters.Add(parms[1]);
            cmd.Parameters.Add(parms[2]);
            cmd.Parameters.Add(parms[3]);
            cmd.Parameters.Add(parms[4]);

            cmd.Connection = conn;

            IDataReader _dr;
            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("titulo"));
            _dt.Columns.Add(new DataColumn("subtitulo"));
            _dt.Columns.Add(new DataColumn("texto"));
            _dt.Columns.Add(new DataColumn("acomodacoes"));
            _dt.Columns.Add(new DataColumn("regime"));


            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();
                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["titulo"] = _dr["titulo"].ToString();
                    dr["subtitulo"] = _dr["subtitulo"].ToString();
                    dr["texto"] = _dr["texto"].ToString();
                    dr["acomodacoes"] = _dr["acomodacoes"].ToString();
                    dr["regime"] = _dr["regime"].ToString();
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
