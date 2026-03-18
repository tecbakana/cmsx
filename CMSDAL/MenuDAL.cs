using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ICMS;

namespace CMSDAL
{
    public class MenuDAL : BaseDAL,IMenuDAL
    {
        public MenuDAL(IDataFactory factory)
        {
            _factory = factory;
        }

        #region IMenuDAL Members

        public void MakeConnection(dynamic prop)
        {
            var data = _factory.GetConnection(prop.banco, prop.parms);
            conn = data.Key;
            cmd = data.Value.Key;
            parms = data.Value.Value;
            _localProps = prop;
        }

        public void CriaMenu()
        {
            throw new NotImplementedException();
        }

        public DataTable Menu()
        {
            //conn.ConnectionString = CONNSTRING;
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "MenuAplicacao";
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT distinct modx.Url," +
                             "       modx.Nome,modx.posicao" +
                             "  FROM usuario usr" +
                             " INNER JOIN relmodulousuario rma" +
                             "    ON rma.UsuarioId = usr.UserId" +
                             " INNER JOIN modulo modx" +
                             "    ON modx.ModuloId = rma.ModuloId" +
                             " WHERE usr.UserId = @UsuarioId and modx.posicao<>999 order by modx.posicao asc";

            parms[0].ParameterName = "@Usuarioid";
            parms[0].Value = _localProps.userid;

            //throw new Exception("testando falha na passagem de parametros:" + parms[0].Value);

            cmd.Parameters.Add(parms[0]);
            cmd.Connection = conn;

            IDataReader _dr;
            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("Url"));
            _dt.Columns.Add(new DataColumn("Nome"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();

                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["Url"] = _dr["Url"].ToString();
                    dr["Nome"] = _dr["Nome"].ToString();
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
