using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ICMS;

namespace CMSDAL
{
    public class ImagemDAL : BaseDAL, IImagem
    {
        public int Altura { get; set; }
        public Guid Imagem { get; set; }
        public int Largura { get; set; }
        public string Url { get; set; }

        #region IImagem Members

        public ImagemDAL(IDataFactory factory)
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

        public void CriaImagem()
        {
            //conn.ConnectionString = CONNSTRING;
            cmd.CommandType = CommandType.Text;

            string imgid = System.Guid.NewGuid().ToString();

            string cmds = "INSERT INTO Imagem(ImagemId,Url,Largura,Altura,AreaId,ConteudoId)";
            cmds += "VALUES ('" + imgid + "',@Url,@Largura,@Altura,@AreaId,@ConteudoId)";

            cmd.CommandText = cmds;

            parms[0].ParameterName = "@Url";
            parms[0].Value = _localProps.url;
            parms[1].ParameterName = "@Largura";
            parms[1].Value = string.IsNullOrEmpty(_localProps.largura) ? 0 : _localProps.largura;
            parms[2].ParameterName = "@Altura";
            parms[2].Value = string.IsNullOrEmpty(_localProps.altura) ? 0 : _localProps.altura;

            //AREA ID
            parms[3].ParameterName = "@AreaId";

            if (_localProps.areaid == null)
            {
                parms[3].Value = System.DBNull.Value;
            }
            else
            {
                parms[3].Value = _localProps.areaid;
            }

            //CONTEUDOID
            parms[4].ParameterName = "@ConteudoId";

            if (_localProps.conteudoid==null)
            {
                parms[4].Value = System.DBNull.Value;
            }
            else
            {
                parms[4].Value = _localProps.conteudoid;
            }

            cmd.Parameters.Add(parms[0]);
            cmd.Parameters.Add(parms[1]);
            cmd.Parameters.Add(parms[2]);
            cmd.Parameters.Add(parms[3]);
            cmd.Parameters.Add(parms[4]);

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

        public void AtualizaGaleria()
        {

            cmd.CommandType = CommandType.Text;

            string imgid = System.Guid.NewGuid().ToString();

            string cmds = "UPDATE imagem SET ConteudoId = @conteudoid WHERE AreaId = @areaid";

            cmd.CommandText = cmds;
            //AREA ID
            parms[0].ParameterName = "@areaid";

            if (_localProps.areaid == null)
            {
                parms[0].Value = System.DBNull.Value;
            }
            else
            {
                parms[0].Value = _localProps.areaid;
            }

            //CONTEUDOID
            parms[1].ParameterName = "@conteudoid";

            if (_localProps.conteudoid == null)
            {
                parms[1].Value = System.DBNull.Value;
            }
            else
            {
                parms[1].Value = _localProps.conteudoid;
            }

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

        public DataTable ListaImagem()
        {
            //conn.ConnectionString = CONNSTRING;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ImagemId,Url,Largura,Altura,AreaId,ConteudoId,Descricao FROM imagem";

            cmd.Connection = conn;

            IDataReader _dr;
            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("ImagemId"));
            _dt.Columns.Add(new DataColumn("Url"));
            _dt.Columns.Add(new DataColumn("Largura"));
            _dt.Columns.Add(new DataColumn("Altura"));
            _dt.Columns.Add(new DataColumn("AreaId"));
            _dt.Columns.Add(new DataColumn("ConteudoId"));
            _dt.Columns.Add(new DataColumn("Descricao"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();

                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["ImagemId"] = _dr["ImagemId"].ToString();
                    dr["Url"] = _dr["Url"].ToString();
                    dr["Largura"] = _dr["Largura"].ToString();
                    dr["Altura"] = _dr["Altura"].ToString();
                    dr["AreaId"] = _dr["AreaId"].ToString();
                    dr["ConteudoId"] = _dr["ConteudoId"].ToString();
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

        public DataTable ObtemImagemPorid()
        {
            //conn.ConnectionString = CONNSTRING;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ImagemId,Url,Largura,Altura,AreaId,ConteudoId,Descricao FROM Imagem WHERE AreaId = @ImagemId";

            parms[0].ParameterName = "@ImagemId";
            parms[0].Value = _localProps.imagemid;

            cmd.Connection = conn;

            IDataReader _dr;
            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("ImagemId"));
            _dt.Columns.Add(new DataColumn("Url"));
            _dt.Columns.Add(new DataColumn("Largura"));
            _dt.Columns.Add(new DataColumn("Altura"));
            _dt.Columns.Add(new DataColumn("AreaId"));
            _dt.Columns.Add(new DataColumn("ConteudoId"));
            _dt.Columns.Add(new DataColumn("Descricao"));

            parms[0].ParameterName = "@Conteudoid";
            parms[0].Value = _localProps.conteudoid;

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();

                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["ImagemId"] = _dr["ImagemId"].ToString();
                    dr["Url"] = _dr["Url"].ToString();
                    dr["Largura"] = _dr["Largura"].ToString();
                    dr["Altura"] = _dr["Altura"].ToString();
                    dr["AreaId"] = _dr["AreaId"].ToString();
                    dr["ConteudoId"] = _dr["ConteudoId"].ToString();
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

        public DataTable ObtemImagemPorAreaId()
        {
            //conn.ConnectionString = CONNSTRING;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ImagemId,Url,Largura,Altura,AreaId,ConteudoId,Descricao FROM imagem WHERE AreaId = @AreaId";

            parms[0].ParameterName = "@AreaId";
            parms[0].Value = _localProps.areaid;

            cmd.Connection = conn;

            IDataReader _dr;
            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("ImagemId"));
            _dt.Columns.Add(new DataColumn("Url"));
            _dt.Columns.Add(new DataColumn("Largura"));
            _dt.Columns.Add(new DataColumn("Altura"));
            _dt.Columns.Add(new DataColumn("AreaId"));
            _dt.Columns.Add(new DataColumn("ConteudoId"));
            _dt.Columns.Add(new DataColumn("Descricao"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();

                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["ImagemId"] = _dr["ImagemId"].ToString();
                    dr["Url"] = _dr["Url"].ToString();
                    dr["Largura"] = _dr["Largura"].ToString();
                    dr["Altura"] = _dr["Altura"].ToString();
                    dr["AreaId"] = _dr["AreaId"].ToString();
                    dr["ConteudoId"] = _dr["ConteudoId"].ToString();
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

        public DataTable ObtemImagemPorConteudoId()
        {
            //conn.ConnectionString = CONNSTRING;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ImagemId,Url,Largura,Altura,AreaId,ConteudoId,Descricao FROM imagem WHERE ConteudoId = @ConteudoId";

            parms[0].ParameterName = "@ConteudoId";
            parms[0].Value = _localProps.conteudoid;

            cmd.Parameters.Add(parms[0]);

            cmd.Connection = conn;

            IDataReader _dr;
            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("ImagemId"));
            _dt.Columns.Add(new DataColumn("Url"));
            _dt.Columns.Add(new DataColumn("Largura"));
            _dt.Columns.Add(new DataColumn("Altura"));
            _dt.Columns.Add(new DataColumn("AreaId"));
            _dt.Columns.Add(new DataColumn("ConteudoId"));
            _dt.Columns.Add(new DataColumn("Descricao"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();

                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["ImagemId"] = _dr["ImagemId"].ToString();
                    dr["Url"] = _dr["Url"].ToString();
                    dr["Largura"] = _dr["Largura"].ToString();
                    dr["Altura"] = _dr["Altura"].ToString();
                    dr["AreaId"] = _dr["AreaId"].ToString();
                    dr["ConteudoId"] = _dr["ConteudoId"].ToString();
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

        public DataTable ListaImagemPorAreaId()
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ImagemId,Url,'0' as 'Altura','0' as 'Largura',AreaId,ConteudoId,Descricao " +
                              "  FROM imagem  " +
                              " WHERE AreaId = @areaid";

            parms[0].ParameterName = "@areaid";
            parms[0].Value = _localProps.areaid;

            cmd.Connection = conn;
            cmd.Parameters.Add(parms[0]);

            IDataReader _dr;

            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("ImagemId"));
            _dt.Columns.Add(new DataColumn("Url"));
            _dt.Columns.Add(new DataColumn("Largura"));
            _dt.Columns.Add(new DataColumn("Altura"));
            _dt.Columns.Add(new DataColumn("AreaId"));
            _dt.Columns.Add(new DataColumn("ConteudoId"));
            _dt.Columns.Add(new DataColumn("Descricao"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();

                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["ImagemId"] = _dr["ImagemId"].ToString();
                    dr["Url"] = _dr["Url"].ToString();
                    dr["Largura"] = _dr["Largura"].ToString();
                    dr["Altura"] = _dr["Altura"].ToString();
                    dr["AreaId"] = _dr["AreaId"].ToString();
                    dr["ConteudoId"] = _dr["ConteudoId"].ToString();
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

        public void CriaGaleria()
        {
            /*
            ImagemId
            Url
            AreaId
            Descricao
            */

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO imagem (ImagemId,Url,AreaId,Descricao)" +
                              "VALUES (@imagemid,@url,@areaid,@descricao)";

            parms[0].ParameterName = "@imagemid";
            parms[0].Value = _localProps.imagemid;
            parms[1].ParameterName = "@areaid";
            parms[1].Value = _localProps.areaid;
            parms[2].ParameterName = "@url";
            parms[2].Value = _localProps.url;
            parms[3].ParameterName = "@descricao";
            parms[3].Value = _localProps.descricao;

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

        #endregion
    }
}
