using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ICMS;

namespace CMSDAL
{
    public class GaleriaDAL : BaseDAL, IGaleriaDAL
    {
        #region IGaleriaDAL Members

        public GaleriaDAL(IDataFactory factory)
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

        public void CriaGaleria()
        {
            /*
            ImagemId
            Url
            AreaId
            */

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO imagem (ImagemId,Url,AreaId)" +
                              "VALUES (@imagemid,@areaid,@url)";

            parms[0].ParameterName = "@imagemid";
            parms[0].Value = _localProps.imagemid;
            parms[1].ParameterName = "@areaid";
            parms[1].Value = _localProps.areaid;
            parms[2].ParameterName = "@url";
            parms[2].Value = _localProps.url;
            parms[2].ParameterName = "@descricao";
            parms[2].Value = _localProps.descricao;

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

        public DataTable GaleriaPorAreaId()
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ImagemId,AreaId,Url,Descricao" +
                              "  FROM imagem  " +
                              " WHERE AreaId = @areaid";

            parms[0].ParameterName = "@areaid";
            parms[0].Value = _localProps.areaid;

            cmd.Connection = conn;
            cmd.Parameters.Add(parms[0]);

            IDataReader _dr;
            DataTable _dt = new DataTable();

            _dt.Columns.Add(new DataColumn("ImagemId"));
            _dt.Columns.Add(new DataColumn("AreaId"));
            _dt.Columns.Add(new DataColumn("Url"));
            _dt.Columns.Add(new DataColumn("Descricao"));
            
            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();
                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["ImagemId"] = _dr["ImagemId"].ToString();
                    dr["AreaId"] = _dr["AreaId"].ToString();
                    dr["Url"] = _dr["Url"].ToString();
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


        #endregion
    }
}
