using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ICMS;
using CMSXEF;

namespace CMSDAL
{
    public class AreasDAL : IAreasDAL
    {
        private IDbConnection conn;
        private IDbCommand cmd;
        private IDataParameter[] parms;
        private IDataFactory _factory;
        public int NumParms { get; set; }
        private dynamic _localProps;

        public const string CONNSTRING = @"Data Source=MARCIO-PC\SQLEXPRESS;Initial Catalog=flexsolution11;User ID=flexsolution12;Password=flx0904";

        public AreasDAL(IDataFactory factory)
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

        public void CriaArea()
        {
            //conn.ConnectionString = CONNSTRING;
            cmd.CommandType = CommandType.Text;

            string cmds = "INSERT INTO areas(AreaId,AplicacaoId,AreaIdPai,Nome,Url,Descricao,Imagem,MenuLateral,MenuSplash,MenuCentral,TipoArea,Posicao)";
            cmds += "VALUES (@AreaId,@AplicacaoId,@AreaIdPai,@Nome,@Url,@Descricao,@Imagem,@MenuLateral,@MenuSplash,@MenuCentral,@tipoArea,@posicao)";

            cmd.CommandText = cmds;

            parms[0].ParameterName = "@AreaId";
            parms[0].Value = _localProps.areaid;
            parms[1].ParameterName = "@AplicacaoId";
            parms[1].Value = _localProps.appid;
            parms[2].ParameterName = "@AreaIdPai";

            if (string.IsNullOrEmpty(_localProps.areaidpai))
            {
                parms[2].Value = System.DBNull.Value;
            }
            else
            {
                parms[2].Value = new System.Guid(_localProps.areaidpai);
            }

            parms[3].ParameterName = "@Nome";
            parms[3].Value = _localProps.nome;
            parms[4].ParameterName = "@Url";
            parms[4].Value = _localProps.url;
            parms[5].ParameterName = "@Descricao";
            parms[5].Value = _localProps.descricao;
            parms[6].ParameterName = "@Imagem";
            parms[6].Value = _localProps.imagem;
            parms[7].ParameterName = "@MenuLateral";
            parms[7].Value = _localProps.menulateral;
            parms[8].ParameterName = "@MenuSplash";
            parms[8].Value = _localProps.menusplash;
            parms[9].ParameterName = "@MenuCentral";
            parms[9].Value = _localProps.menucentral;
            parms[10].ParameterName = "@tipoArea";
            parms[10].Value = _localProps.tipoArea;
            parms[11].ParameterName = "@posicao";
            parms[11].Value = _localProps.posicao;

            cmd.Parameters.Add(parms[0]);
            cmd.Parameters.Add(parms[1]);
            cmd.Parameters.Add(parms[2]);
            cmd.Parameters.Add(parms[3]);
            cmd.Parameters.Add(parms[4]);
            cmd.Parameters.Add(parms[5]);
            cmd.Parameters.Add(parms[6]);
            cmd.Parameters.Add(parms[7]);
            cmd.Parameters.Add(parms[8]);
            cmd.Parameters.Add(parms[9]);
            cmd.Parameters.Add(parms[10]);
            cmd.Parameters.Add(parms[11]);

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
        
        public void EditaArea()
        {
            //conn.ConnectionString = CONNSTRING;
            cmd.CommandType = CommandType.Text;

            string cmds = "UPDATE areas ";
			cmds += " set AreaIdPai=@AreaIdPai,";
			cmds += " Nome=@Nome,";
			cmds += " Url=@Url,";
            cmds += " Descricao=@Descricao,";
            cmds += " posicao=@posicao,";
			cmds += " Imagem=@Imagem,";
			cmds += " MenuLateral=@MenuLateral,";
			cmds += " MenuSplash=@MenuSplash,";
			cmds += " MenuCentral=@MenuCentral";
            cmds += " WHERE AreaId = @AreaId AND AplicacaoId = @AplicacaoId";
            cmd.CommandText = cmds;

            parms[0].ParameterName = "@AreaId";
            parms[0].Value = _localProps.areaid;
            parms[1].ParameterName = "@AplicacaoId";
            parms[1].Value = _localProps.appid;
            parms[2].ParameterName = "@AreaIdPai";

            if (string.IsNullOrEmpty(_localProps.areaidpai))
            {
                parms[2].Value = System.DBNull.Value;
            }
            else
            {
                parms[2].Value = new System.Guid(_localProps.areaidpai);
            }

            parms[3].ParameterName = "@Nome";
            parms[3].Value = _localProps.nome;
            parms[4].ParameterName = "@Url";
            parms[4].Value = _localProps.url;
            parms[5].ParameterName = "@Descricao";
            parms[5].Value = _localProps.descricao;
            parms[6].ParameterName = "@Imagem";
            parms[6].Value = _localProps.imagem;
            parms[7].ParameterName = "@MenuLateral";
            parms[7].Value = _localProps.menulateral;
            parms[8].ParameterName = "@MenuSplash";
            parms[8].Value = _localProps.menusplash;
            parms[9].ParameterName = "@MenuCentral";
            parms[9].Value = _localProps.menucentral;
            parms[10].ParameterName = "@posicao";
            parms[10].Value = _localProps.posicao;

            cmd.Parameters.Add(parms[0]);
            cmd.Parameters.Add(parms[1]);
            cmd.Parameters.Add(parms[2]);
            cmd.Parameters.Add(parms[3]);
            cmd.Parameters.Add(parms[4]);
            cmd.Parameters.Add(parms[5]);
            cmd.Parameters.Add(parms[6]);
            cmd.Parameters.Add(parms[7]);
            cmd.Parameters.Add(parms[8]);
            cmd.Parameters.Add(parms[9]);
            cmd.Parameters.Add(parms[10]);

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

        public void EditaAreaPosicao()
        {
            //conn.ConnectionString = CONNSTRING;
            cmd.CommandType = CommandType.Text;

            string cmds = "UPDATE Areas ";
            cmds += " set posicao=@posicao";
            cmds += " WHERE AreaId = @AreaId";
            cmd.CommandText = cmds;

            parms[0].ParameterName = "@posicao";
            parms[0].Value = _localProps.posicao;
            parms[1].ParameterName = "@AreaId";
            parms[1].Value = _localProps.id;

            cmd.Parameters.Add(parms[0]);
            cmd.Parameters.Add(parms[1]);

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

        public DataTable ListaDictAreas()
        {
            //conn.ConnectionString = CONNSTRING;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT id,nome,tipo " +
                            "  FROM dictAreas ";
            cmd.Connection = conn;

            IDataReader _dr;
            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("idTipoArea"));
            _dt.Columns.Add(new DataColumn("nomeTipoArea"));
            _dt.Columns.Add(new DataColumn("tipoArea"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();

                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["id"]   = _dr["id"].ToString();
                    dr["nome"] = _dr["nome"].ToString();
                    dr["tipo"] = _dr["tipo"].ToString();
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
        
        public DataTable ListaAreas()
        {
            //conn.ConnectionString = CONNSTRING;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Areas.AreaId, Areas.AplicacaoId, Areas.Nome, Areas.Url, Areas.Descricao, Areas.AreaIdPai,Pai.Nome AS 'NomePai',Areas.Imagem," +
                            "(case when Areas.MenuLateral=1 then 0 else " +
                            "    case when Areas.MenuSplash = 1 then 1 else " +
                            "      case when Areas.MenuCentral = 1 then 2 end " +
	                        "    end " +
                            "  end) as TipoMenu, " +
                            "  Areas.MenuLateral,Areas.MenuSplash,Areas.MenuCentral, Areas.posicao " +
                            "  FROM areas Areas " +
                            " LEFT JOIN areas Pai  " +
                            "   ON Pai.AreaId = Areas.AreaIdPai" +
                            " WHERE Areas.AplicacaoId = @AplicacaoId AND Areas.DataFinal IS NULL " +
                            " and (case when @TipoArea is null then Areas.tipoArea not in (8,9) else Areas.tipoArea = @TipoArea end) " +
                            " GROUP BY Areas.AreaIdPai,Areas.AreaId,Areas.AplicacaoId,Areas.Nome,Areas.Url,Areas.Descricao,Pai.Nome,Areas.Imagem,Areas.MenuLateral,Areas.MenuSplash,Areas.MenuCentral,Areas.posicao" +
                            " ORDER BY Areas.posicao asc";

            //cmd.CommandText = "ListaAreas";
            parms[0].ParameterName = "@AplicacaoId";
            parms[0].Value = _localProps.appid;
            parms[1].ParameterName = "@TipoArea";
            parms[1].Value = _localProps.tpArea;

            if (cmd.Parameters.Count <= 0)
            {
                cmd.Parameters.Add(parms[0]);
                cmd.Parameters.Add(parms[1]);
            }

            cmd.Connection = conn;
            
            IDataReader _dr;
            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("AplicacaoId"));
            _dt.Columns.Add(new DataColumn("AreaId"));
            _dt.Columns.Add(new DataColumn("Nome"));
            _dt.Columns.Add(new DataColumn("Url"));
            _dt.Columns.Add(new DataColumn("Descricao"));
            _dt.Columns.Add(new DataColumn("Posicao"));
            _dt.Columns.Add(new DataColumn("NomePai"));
            _dt.Columns.Add(new DataColumn("Imagem"));
            _dt.Columns.Add(new DataColumn("TipoMenu"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();

                //DataRow drx = _dt.NewRow();
                //drx["AplicacaoId"] = new Guid();
                //drx["AreaId"] = new Guid(); ;
                //drx["Nome"] = "";
                //drx["Url"] = "";
                //drx["Descricao"] = "Escolha um item abaixo:";
                //drx["Posicao"] = "";
                //drx["NomePai"] = "";
                //drx["Imagem"] = "";
                //drx["MenuLateral"] = "";
                //_dt.Rows.Add(drx);

                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["AplicacaoId"] = _dr["AplicacaoId"].ToString();
                    dr["AreaId"] = _dr["AreaId"].ToString();
                    dr["Nome"] = _dr["Nome"].ToString();
                    dr["Url"] = _dr["Url"].ToString();
                    dr["Descricao"] = _dr["Descricao"].ToString();
                    dr["Posicao"] = _dr["posicao"].ToString();
                    dr["NomePai"] = _dr["NomePai"].ToString();
                    dr["Imagem"] = _dr["Imagem"].ToString();
                    dr["TipoMenu"] = _dr["TipoMenu"].ToString();
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

        public DataTable ListaAreasPorTipo()
        {
            //conn.ConnectionString = CONNSTRING;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Areas.AreaId, Areas.AplicacaoId, Areas.Nome, Areas.Url, Areas.Descricao, Areas.AreaIdPai,Pai.Nome AS 'NomePai',Areas.Imagem,Areas.MenuLateral, Areas.posicao " +
                            "  FROM areas Areas " +
                            " LEFT JOIN areas Pai  " +
                            "   ON Pai.AreaId = Areas.AreaIdPai" +
                            " WHERE Areas.AplicacaoId = @AplicacaoId AND Areas.DataFinal IS NULL AND Areas.tipoArea=@tipoArea " +
                            " GROUP BY Areas.AreaIdPai,Areas.AreaId,Areas.AplicacaoId,Areas.Nome,Areas.Url,Areas.Descricao,Pai.Nome,Areas.Imagem,Areas.MenuLateral,Areas.posicao";

            //cmd.CommandText = "ListaAreas";
            parms[0].ParameterName = "@AplicacaoId";
            parms[0].Value = _localProps.appid;
            parms[1].ParameterName = "@tipoArea";
            parms[1].Value = _localProps.tipoArea;

            cmd.Parameters.Add(parms[0]);
            cmd.Parameters.Add(parms[1]);

            cmd.Connection = conn;

            IDataReader _dr;
            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("AplicacaoId"));
            _dt.Columns.Add(new DataColumn("AreaId"));
            _dt.Columns.Add(new DataColumn("Nome"));
            _dt.Columns.Add(new DataColumn("Url"));
            _dt.Columns.Add(new DataColumn("Descricao"));
            _dt.Columns.Add(new DataColumn("Posicao"));
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
                    dr["Nome"] = _dr["Nome"].ToString();
                    dr["Url"] = _dr["Url"].ToString();
                    dr["Descricao"] = _dr["Descricao"].ToString();
                    dr["Posicao"] = _dr["posicao"].ToString();
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

        public DataTable ListaAreasPorId()
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Areas.AreaId, Areas.AplicacaoId,Areas.AreaIdPai, Areas.Nome, Areas.Url, Areas.Descricao, Areas.AreaIdPai,Pai.Nome AS 'NomePai',Areas.Imagem, " +
                            "(case when Areas.MenuLateral=1 then 0 else " +
                            "    case when Areas.MenuSplash = 1 then 1 else " +
                            "      case when Areas.MenuCentral = 1 then 2 end " +
	                        "    end " +
                            "  end) as TipoMenu, " +
                             " Areas.MenuLateral,Areas.posicao " +
                            "  FROM areas Areas " +
                            " LEFT JOIN Areas Pai  " +
                            "   ON Pai.AreaId = Areas.AreaIdPai" +
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
            _dt.Columns.Add(new DataColumn("TipoMenu"));
            _dt.Columns.Add(new DataColumn("Posicao"));

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
                    dr["TipoMenu"] = _dr["TipoMenu"].ToString();
                    dr["Posicao"] = _dr["posicao"].ToString();
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

        public DataTable ListaAreasFilha()
        {
            //conn.ConnectionString = CONNSTRING;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Areas.AreaId, Areas.AplicacaoId, Areas.Nome, Areas.Url, Areas.Descricao, Areas.AreaIdPai, apai.Nome AS 'NomePai',Areas.Imagem,Areas.MenuLateral,Areas.posicao" +
                            "  FROM areas Areas " +
                            " INNER JOIN areas   apai " +
                            "   ON apai.AreaId = Areas.AreaIdPai " +
                            " WHERE Areas.AplicacaoId = @AplicacaoId and Areas.AreaIdPai = @AreaIdPai AND Areas.DataFinal IS NULL";
            //cmd.CommandText = "ListaAreas";
            parms[0].ParameterName = "@AplicacaoId";
            parms[0].Value = _localProps.appid;
            parms[1].ParameterName = "@AreaIdPai";
            parms[1].Value = _localProps.areaIdPai;

            if (cmd.Parameters.Count <= 0)
            {
                cmd.Parameters.Add(parms[0]);
                cmd.Parameters.Add(parms[1]);
            }
            cmd.Connection = conn;

            IDataReader _dr;
            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("AplicacaoId"));
            _dt.Columns.Add(new DataColumn("AreaId"));
            _dt.Columns.Add(new DataColumn("Nome"));
            _dt.Columns.Add(new DataColumn("Url"));
            _dt.Columns.Add(new DataColumn("Descricao"));
            _dt.Columns.Add(new DataColumn("NomePai"));
            _dt.Columns.Add(new DataColumn("Imagem"));
            _dt.Columns.Add(new DataColumn("MenuLateral"));
            _dt.Columns.Add(new DataColumn("Posicao"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();

                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["AplicacaoId"] = _dr["AplicacaoId"].ToString();
                    dr["AreaId"] = _dr["AreaId"].ToString();
                    dr["Nome"] = _dr["Nome"].ToString();
                    dr["Url"] = _dr["Url"].ToString();
                    dr["Descricao"] = _dr["Descricao"].ToString();
                    dr["NomePai"] = _dr["NomePai"].ToString();
                    dr["Imagem"] = _dr["Imagem"].ToString();
                    dr["MenuLateral"] = _dr["MenuLateral"].ToString();
                    dr["Posicao"] = _dr["posicao"].ToString();
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

        public DataTable ListaAreasPai()
        {
            //conn.ConnectionString = CONNSTRING;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT a.AreaId, a.AplicacaoId, a.Nome, a.Url, a.Descricao, a.AreaIdPai, apai.Nome AS 'NomePai',a.Imagem,a.MenuLateral" +
                            "  FROM areas a " +
                            " LEFT JOIN areas apai " +
                            "   ON apai.AreaId = a.AreaIdPai " +
                            " where a.AplicacaoId = @AplicacaoId " +
                            " and a.AreaIdPai is null " +
                            " and a.datafinal is null " +
                            " and (case when @TipoArea is null then a.tipoArea not in (8,9) else a.tipoArea = @TipoArea end) " +
                            " ORDER BY a.posicao asc";

            parms[0].ParameterName = "@AplicacaoId";
            parms[0].Value = _localProps.appid;
            parms[1].ParameterName = "@TipoArea";
            parms[1].Value = (_localProps.tpArea=="null"?null:_localProps.tpArea);
            cmd.Parameters.Add(parms[0]);
            cmd.Parameters.Add(parms[1]);
            cmd.Connection = conn;

            IDataReader _dr;
            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("AplicacaoId"));
            _dt.Columns.Add(new DataColumn("AreaId"));
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

        /// <summary>
        /// ListaAreaMenu recupera a lista de areas por id de aplicacao, para controle do/por cliente [ menu superior ]
        /// </summary>
        /// <param name="prop">
        /// Este parametro na realidade é passado no Metodo MakeConnection
        /// - prop.parms = 1 [indica a quantidade de parametros esperados pelo metodo
        /// - prop.appid
        /// </param>
        /// <returns>
        /// DataTable (Login,Aplicacao,Data Inclusao)
        /// </returns>
        public DataTable ListaAreaMenu()
        {
            //conn.ConnectionString = CONNSTRING;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Areas.AreaId, Areas.AplicacaoId, Areas.Nome, Areas.Url, Areas.Descricao, Areas.AreaIdPai, apai.Nome AS 'NomePai','' AS 'Imagem',Areas.MenuLateral,'' AS 'imagemnome'" +
                            "  FROM areas Areas " +
                            " LEFT JOIN areas   apai " +
                            "   ON apai.AreaId = Areas.AreaIdPai " +
                //" LEFT JOIN Imagem img  " +
                //"   ON img.AreaId = Areas.AreaId" +
                            " WHERE Areas.AplicacaoId = @AplicacaoId and Areas.AreaIdPai IS NULL AND Areas.DataFinal IS NULL AND Areas.MenuLateral = 0 AND Areas.MenuSplash = 0 and Areas.TipoArea not in (8,9)" +
                            " ORDER BY Areas.posicao asc";

    
            parms[0].ParameterName = "@AplicacaoId";
            parms[0].Value = _localProps.appid;
            if (cmd.Parameters.Count <= 0)
            {
                cmd.Parameters.Add(parms[0]);
            }
            cmd.Connection = conn;

            IDataReader _dr;
            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("AplicacaoId"));
            _dt.Columns.Add(new DataColumn("AreaId"));
            _dt.Columns.Add(new DataColumn("Nome"));
            _dt.Columns.Add(new DataColumn("Url"));
            _dt.Columns.Add(new DataColumn("Descricao"));
            _dt.Columns.Add(new DataColumn("NomePai"));
            _dt.Columns.Add(new DataColumn("Imagem"));
            _dt.Columns.Add(new DataColumn("MenuLateral"));
            _dt.Columns.Add(new DataColumn("imagemnome"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();

                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["AplicacaoId"] = _dr["AplicacaoId"].ToString();
                    dr["AreaId"] = _dr["AreaId"].ToString();
                    dr["Nome"] = _dr["Nome"].ToString();
                    dr["Url"] = _dr["Url"].ToString();
                    dr["Descricao"] = _dr["Descricao"].ToString();
                    dr["NomePai"] = _dr["NomePai"].ToString();
                    dr["Imagem"] = _dr["Imagem"].ToString();
                    dr["MenuLateral"] = _dr["MenuLateral"].ToString();
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

        /// <summary>
        /// ListaAreaMenuLateral recupera a lista de areas por id de aplicacao, para controle do/por cliente [ menu lateral ]
        /// </summary>
        /// <param name="prop">
        /// Este parametro na realidade é passado no Metodo MakeConnection
        /// - prop.parms = 1 [indica a quantidade de parametros esperados pelo metodo
        /// - prop.appid
        /// </param>
        /// <returns>
        /// DataTable (Login,Aplicacao,Data Inclusao)
        /// </returns>
        public DataTable ListaAreaMenuLateral()
        {
            //conn.ConnectionString = CONNSTRING;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Areas.AreaId, Areas.AplicacaoId, REPLACE(Areas.Nome,'keyhome_','') AS 'Nome', Areas.Url, Areas.Descricao, Areas.AreaIdPai, apai.Nome AS 'NomePai',Areas.Imagem,Areas.MenuLateral" +
                            "  FROM areas Areas " +
                            " LEFT JOIN areas   apai " +
                            "   ON apai.AreaId = Areas.AreaIdPai " +
                            " WHERE Areas.AplicacaoId = @AplicacaoId and Areas.AreaIdPai IS NULL AND Areas.DataFinal IS NULL AND Areas.MenuLateral = 1 and Areas.TipoArea not in (8,9)";

            parms[0].ParameterName = "@AplicacaoId";
            parms[0].Value = _localProps.appid;
            cmd.Parameters.Add(parms[0]);
            if (parms.Length > 1)
            {
                parms[1].ParameterName = "@keylocation";
                parms[1].Value = _localProps.keylocation;
                cmd.Parameters.Add(parms[1]);
                cmd.CommandText += " AND Areas.Nome LIKE '" + _localProps.keylocation + "%'";
            }
            else
            {
                cmd.CommandText += " AND Areas.Nome NOT LIKE 'key%'";
            }
            cmd.Connection = conn;

            IDataReader _dr;
            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("AplicacaoId"));
            _dt.Columns.Add(new DataColumn("AreaId"));
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
        
        /// <summary>
        /// ListaAreaMenuSplash recupera a lista de areas por id de aplicacao, para controle do/por cliente [ menu splash ]
        /// </summary>
        /// <param name="prop">
        /// Este parametro na realidade é passado no Metodo MakeConnection
        /// - prop.parms = 1 [indica a quantidade de parametros esperados pelo metodo
        /// - prop.appid
        /// </param>
        /// <returns>
        /// DataTable (Login,Aplicacao,Data Inclusao)
        /// </returns>
        public DataTable ListaAreaMenuSplash()
        {
            //conn.ConnectionString = CONNSTRING;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Areas.AreaId, Areas.AplicacaoId, Areas.Nome, Areas.Url, Areas.Descricao, Areas.AreaIdPai, apai.Nome AS 'NomePai',Areas.Imagem,Areas.MenuLateral,img.Url AS 'imagemnome'" +
                              "  FROM areas Areas " +
                              "  LEFT JOIN areas apai " +
                              "    ON apai.AreaId = Areas.AreaIdPai " +
                              "  INNER JOIN Imagem img  " +
                              "     ON img.AreaId = Areas.AreaId" +
                              " WHERE Areas.AplicacaoId = @AplicacaoId AND Areas.DataFinal IS NULL AND Areas.MenuSplash = 1 AND img.Url IS NOT NULL and Areas.TipoArea not in (8,9)";
                              //" WHERE Areas.AplicacaoId = @AplicacaoId and Areas.AreaIdPai IS NULL AND Areas.DataFinal IS NULL AND Areas.MenuSplash = 1 AND img.Url IS NOT NULL";

            parms[0].ParameterName = "@AplicacaoId";
            parms[0].Value = _localProps.appid;
            cmd.Parameters.Add(parms[0]);
            cmd.Connection = conn;

            IDataReader _dr;
            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("AplicacaoId"));
            _dt.Columns.Add(new DataColumn("AreaId"));
            _dt.Columns.Add(new DataColumn("Nome"));
            _dt.Columns.Add(new DataColumn("Url"));
            _dt.Columns.Add(new DataColumn("Descricao"));
            _dt.Columns.Add(new DataColumn("NomePai"));
            _dt.Columns.Add(new DataColumn("Imagem"));
            _dt.Columns.Add(new DataColumn("MenuLateral"));
            _dt.Columns.Add(new DataColumn("imagemnome"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();

                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["AplicacaoId"] = _dr["AplicacaoId"].ToString();
                    dr["AreaId"] = _dr["AreaId"].ToString();
                    dr["Nome"] = _dr["Nome"].ToString();
                    dr["Url"] = _dr["Url"].ToString();
                    dr["Descricao"] = _dr["Descricao"].ToString();
                    dr["NomePai"] = _dr["NomePai"].ToString();
                    dr["Imagem"] = _dr["Imagem"].ToString();
                    dr["MenuLateral"] = _dr["MenuLateral"].ToString();
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

        public DataTable ListaMenuPorTipo()
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Areas.AreaId, Areas.AplicacaoId, Areas.Nome, Areas.Url, Areas.Descricao, Areas.AreaIdPai, apai.Nome AS 'NomePai',Areas.Imagem,Areas.MenuLateral,img.Url AS 'imagemnome'" +
                              "  FROM areas Areas " +
                              "  LEFT JOIN areas   apai " +
                              "    ON apai.AreaId = Areas.AreaIdPai " +
                              "  INNER JOIN Imagem img  " +
                              "     ON img.AreaId = Areas.AreaId" +
                              " WHERE Areas.AplicacaoId = @AplicacaoId " +
                              "   AND Areas.AreaIdPai IS NULL " +
                              "   AND Areas.DataFinal IS NULL " +
                              "   AND Areas.MenuLateral = @menulateral  " +
                              "   AND Areas.MenuSplash  = @menusplash   " +
                              "   AND Areas.MenuCentral = @menucentral  " +
                              "   AND img.Url IS NOT NULL  and Areas.TipoArea not in (8,9) order by DataInicial" +
                            " ORDER BY Areas.posicao asc";

            parms[0].ParameterName = "@AplicacaoId";
            parms[0].Value = _localProps.appid;
            parms[0].ParameterName = "@menulateral";
            parms[0].Value = _localProps.menulateral;
            parms[0].ParameterName = "@menusplash";
            parms[0].Value = _localProps.menusplash;
            parms[0].ParameterName = "@menucentral";
            parms[0].Value = _localProps.menucentral;

            if (cmd.Parameters.Count <= 0)
            {
                cmd.Parameters.Add(parms[0]);
            }
            cmd.Connection = conn;

            IDataReader _dr;
            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("AplicacaoId"));
            _dt.Columns.Add(new DataColumn("AreaId"));
            _dt.Columns.Add(new DataColumn("Nome"));
            _dt.Columns.Add(new DataColumn("Url"));
            _dt.Columns.Add(new DataColumn("Descricao"));
            _dt.Columns.Add(new DataColumn("NomePai"));
            _dt.Columns.Add(new DataColumn("Imagem"));
            _dt.Columns.Add(new DataColumn("MenuLateral"));
            _dt.Columns.Add(new DataColumn("imagemnome"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();

                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["AplicacaoId"] = _dr["AplicacaoId"].ToString();
                    dr["AreaId"] = _dr["AreaId"].ToString();
                    dr["Nome"] = _dr["Nome"].ToString();
                    dr["Url"] = _dr["Url"].ToString();
                    dr["Descricao"] = _dr["Descricao"].ToString();
                    dr["NomePai"] = _dr["NomePai"].ToString();
                    dr["Imagem"] = _dr["Imagem"].ToString();
                    dr["MenuLateral"] = _dr["MenuLateral"].ToString();
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

        public void InativaArea()
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

        public string AreaRapida()
        {
            throw new NotImplementedException();
        }
    }
}
