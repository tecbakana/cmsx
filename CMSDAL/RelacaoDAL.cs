using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ICMS;

namespace CMSDAL
{
    public class RelacaoDAL : IRelacaoDAL
    {
        
        private IDbConnection conn;
        private IDbCommand cmd;
        private IDataParameter[] parms;
        private IDataFactory _factory;
        public int NumParms { get; set; }
        private dynamic _localProps;

        public RelacaoDAL(IDataFactory factory)
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

        #region IRelacaoDAL Members

        public void CriaRelacaoAplicacao()
        {
            cmd.CommandType = CommandType.Text;
            string cmds = string.Empty;

            cmds = "INSERT INTO RelUsuarioAplicacao(RelacaoId,AplicacaoId,UsuarioId) VALUES(@relacaoid,@appid,@userid)";

            parms[0].ParameterName = "@relacaoid";
            parms[0].Value = _localProps.relid;
            parms[1].ParameterName = "@appid";
            parms[1].Value = _localProps.appid;
            parms[2].ParameterName = "@userid";
            parms[2].Value = _localProps.userid;

            cmd.CommandText = cmds;

            cmd.Parameters.Add(parms[0]);
            cmd.Parameters.Add(parms[1]);
            cmd.Parameters.Add(parms[2]);

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
        
        public void CriaRelacaoModulo()
        {
            cmd.CommandType = CommandType.Text;
            string cmds = string.Empty;

            if (_localProps.tipo == "aplicacao")
            {
                cmds = "INSERT INTO relmoduloaplicacao(RelacaoId,ModuloId,AplicacaoId) VALUES(@relacaoid,@PaiId,@FilhoId)";
            }
            else if (_localProps.tipo == "usuario")
            {
                cmds = "INSERT INTO relmodulousuario(RelacaoId,ModuloId,UsuarioId) VALUES(@relacaoid,@PaiId,@FilhoId)";
            }

            parms[0].ParameterName = "@relacaoid";
            parms[0].Value = _localProps.relid;
            parms[1].ParameterName = "@PaiId";
            parms[1].Value = _localProps.PaiId;
            parms[2].ParameterName = "@FilhoId";
            parms[2].Value = _localProps.FilhoId;

            cmd.CommandText = cmds;

            cmd.Parameters.Add(parms[0]);
            cmd.Parameters.Add(parms[1]);
            cmd.Parameters.Add(parms[2]);

            cmd.Connection = conn;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch(DataException ex)
            {
                throw new DataException(ex.Message);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        public DataTable ListaRelacao()
        {
            DataTable dt = new DataTable();
            cmd.CommandType = CommandType.Text;
            string cmds = string.Empty;

            if (_localProps.tipo == "aplicacao")
            {
                cmds = "SELECT RelacaoId,ModuloId,AplicacaoId FROM relmoduloaplicacao WHERE AplicacaoId = (CASE WHEN @objid IS NULL THEN AplicacaoId ELSE @objid END)";
            }
            else if (_localProps.tipo == "usuario")
            {
                cmds = "SELECT RelacaoId,ModuloId,UsuarioId FROM relmodulousuario WHERE UsuarioId = (CASE WHEN @objid IS NULL THEN UsuarioId ELSE @objid END)";
            }

            parms[0].ParameterName = "@objid";
           
             if (string.IsNullOrEmpty(_localProps.objid))
            {
                parms[0].Value = System.DBNull.Value;
            }
            else
            {
                parms[0].Value = new System.Guid(_localProps.objid);
            }

             cmd.CommandText = cmds;
            cmd.Parameters.Add(parms[0]);

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

            return dt;
        }

        #endregion
    }
}
