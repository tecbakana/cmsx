using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ICMS;

namespace CMSDAL
{
    public class ModuloDAL : IModuloDAL
    {

        #region IModuloDAL Members
        private IDbConnection conn;
        private IDbCommand cmd;
        private IDataParameter[] parms;
        private IDataFactory _factory;
        public int NumParms { get; set; }
        private dynamic _localProps;

        public ModuloDAL(IDataFactory factory)
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

        /// <summary>
        /// CriaModulo metodo para criação de modulos de aplicacao
        /// </summary>
        /// <param name="prop">
        /// Propriedade da classe
        /// prop.moduloid,prop.nome,prop.url
        /// </param>
        public void CriaModulo()
        {
            //moduloid,nome,url
            cmd.CommandType = CommandType.Text;

            string cmds = "INSERT INTO modulo(moduloid,nome,url,posicao)";
            cmds += " VALUES (@moduloid,@nome,@url,77)";

            cmd.CommandText = cmds;
            cmd.Connection = conn;

            parms[0].ParameterName = "@moduloid";
            parms[0].Value = _localProps.moduloid;
            parms[1].ParameterName = "@nome";
            parms[1].Value = _localProps.nome;
            parms[2].ParameterName = "@url";
            parms[2].Value = _localProps.url;

            cmd.Parameters.Add(parms[0]);
            cmd.Parameters.Add(parms[1]);
            cmd.Parameters.Add(parms[2]);


            try
            {
                conn.Open();
                cmd.ExecuteScalar();
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

        public DataTable ListaModulos()
        {
            DataTable dt = new DataTable();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            string cmds = "SELECT moduloid,nome,url FROM modulo where posicao<>999 order by posicao asc";
            cmd.CommandText = cmds;

            dt.Columns.Add("ModuloId");
            dt.Columns.Add("Nome");
            dt.Columns.Add("Url");

            try
            {
                conn.Open();
                IDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    DataRow udr = dt.NewRow();
                    udr["ModuloId"] = dr["moduloid"];
                    udr["Nome"]     = dr["nome"];
                    udr["Url"]      = dr["url"];
                    dt.Rows.Add(udr);
                    dt.AcceptChanges();
                }
            }
            catch (DataException ex)
            {
                throw new DataException("Falha no banco:" + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Falha na aplicacao:" + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }

        public DataTable ListaModulosXUser()
        {
            DataTable dt = new DataTable();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            string cmds =  " select distinct md.moduloid, " +
                           "   md.nome as 'Modulo', " +
                           "   md.url, " +
                           "   ap.nome, " +
                           "   0 as valida " + 
                           "  from modulo md " +
                           "  left join relmodulousuario rmxu " +
                           "    on rmxu.moduloid = md.moduloid " +
                           "  left join relmoduloaplicacao ma " +
                           "    on ma.moduloId = md.moduloid " +
                           "  left join aplicacao ap " +
                           "    on ap.aplicacaoid = ma.aplicacaoid" +
                           "  where md.Url like '%make%' and md.posicao<>999 order by md.posicao asc";

            parms[0].ParameterName = "@UserId";
            parms[0].Value = _localProps.userid;

            cmd.Parameters.Add(parms[0]);

            cmd.CommandText = cmds;
            cmd.Connection = conn;

            dt.Columns.Add("moduloId");
            dt.Columns.Add("Nome");
            dt.Columns.Add("valida");
            dt.Columns.Add("Modulo");
            dt.Columns.Add("Url");

            try
            {
                conn.Open();
                IDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    DataRow udr = dt.NewRow();
                    udr["moduloId"] = dr["ModuloId"];
                    udr["Nome"]          = dr["nome"];
                    udr["valida"] = dr["valida"];
                    udr["Modulo"] = dr["Modulo"];
                    udr["Url"] = dr["Url"];
                    dt.Rows.Add(udr);
                    dt.AcceptChanges();
                }
            }
            catch (DataException ex)
            {
                throw new DataException("Falha no banco:" + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Falha na aplicacao:" + ex.Message);
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
