using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ICMS;

namespace CMSDAL
{
    public class UsuarioDAL : IUsuarioDAL
    {
        #region IUsuarioDAL Members
        private IDbConnection conn;
        private IDbCommand cmd;
        private IDataParameter[] parms;
        private IDataFactory _factory;
        public int NumParms { get; set; }
        private dynamic _localProps;

        public UsuarioDAL(IDataFactory factory)
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
        /// CriaUsuario metodo para inclusao de usuarios no banco
        /// </summary>
        /// <param name="prop">
        /// Este parametro na realidade é passado no Metodo MakeConnection
        /// -prop.parms = 6 [indica a quantidade de parametros esperados pelo metodo
        /// -prop.userid,prop.nome,prop.sobrenome,prop.apelido,prop.senha,prop.appid
        /// </param>
        /// 
        public void CriaUsuario()
        {

            cmd.CommandType = CommandType.Text;

            string cmds = "INSERT INTO usuario(UserId,Nome,Sobrenome,Apelido,Senha,Ativo,DataInclusao)";
            cmds += " VALUES (@UserId,@Nome,@Sobrenome,@Apelido,@Senha,1,@DataInclusao)";

            cmd.CommandText = cmds;
            cmd.Connection = conn;

            parms[0].ParameterName = "@UserId";
            parms[0].Value = _localProps.userid;
            parms[1].ParameterName = "@Nome";
            parms[1].Value = _localProps.nome;
            parms[2].ParameterName = "@Sobrenome";
            parms[2].Value = _localProps.sobrenome;
            parms[3].ParameterName = "@Apelido";
            parms[3].Value = _localProps.apelido;
            parms[4].ParameterName = "@Senha";
            parms[4].Value = _localProps.senha;
            parms[5].ParameterName = "@DataInclusao";
            parms[5].Value = DateTime.Today;

            cmd.Parameters.Add(parms[0]);
            cmd.Parameters.Add(parms[1]);
            cmd.Parameters.Add(parms[2]);
            cmd.Parameters.Add(parms[3]);
            cmd.Parameters.Add(parms[4]);
            cmd.Parameters.Add(parms[5]);
            try
            {
                conn.Open();
                cmd.ExecuteScalar();
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
            
        }

        public void AtualizaUsuario()
        {

        }

        /// <summary>
        /// ListaUsuario recupera a lista geral dos usuarios
        /// </summary>
        /// <returns>
        /// DataTable (Login,Aplicacao,Data Inclusao)
        /// </returns>
        public DataTable ListaUsuario()
        {
            DataTable dt = new DataTable();
            cmd.CommandType = CommandType.Text;

            string cmds = "SELECT Usu.UserId,App.AplicacaoId,Usu.Apelido,App.Nome,Usu.DataInclusao " + 
                            "FROM usuario Usu " +
                            "INNER JOIN RelUsuarioAplicacao rel ON rel.UsuarioId = Usu.UserId " +
                            "INNER JOIN Aplicacao App ON App.AplicacaoId = rel.AplicacaoId";

            cmd.CommandText = cmds;
            cmd.Connection = conn;

            dt.Columns.Add("Login");
            dt.Columns.Add("Aplicacao");
            dt.Columns.Add("Data Inclusao");
            dt.Columns.Add("UserId");
            dt.Columns.Add("AplicacaoId");

            try
            {
                conn.Open();
                IDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    DataRow _dr = dt.NewRow();
                    _dr["UserId"] = dr["UserId"];
                    _dr["Login"] = dr["Apelido"];
                    _dr["Aplicacao"] = dr["Nome"];
                    _dr["AplicacaoId"] = dr["AplicacaoId"];
                    _dr["Data Inclusao"] = dr["DataInclusao"];
                    dt.Rows.Add(_dr);
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

        /// <summary>
        /// ListaUsuarioPorAplicacaoId recupera a lista geral dos usuarios por id de aplicacao, para controle do/por cliente
        /// </summary>
        /// <param name="prop">
        /// Este parametro na realidade é passado no Metodo MakeConnection
        /// -prop.parms = 1 [indica a quantidade de parametros esperados pelo metodo
        /// -prop.appid
        /// </param>
        /// <returns>
        /// DataTable (Login,Aplicacao,Data Inclusao)
        /// </returns>
        public DataTable ListaUsuarioPorAplicacaoId()
        {
            DataTable dt = new DataTable();
            cmd.CommandType = CommandType.Text;

            string cmds =   "SELECT Usu.UserId,App.AplicacaoId,Usu.Apelido,App.Nome,Usu.DataInclusao FROM usuario Usu " +
           // string cmds =   "SELECT Usu.UserId,Usu.Apelido,App.Nome,Usu.DataInclusao FROM Usuario Usu " +
                            "INNER JOIN relusuarioaplicacao rel ON rel.UsuarioId = Usu.UserId " +
                            "INNER JOIN aplicacao App ON App.AplicacaoId = rel.AplicacaoId " +
                            "WHERE App.AplicacaoId = @AplicacaoId";

            parms[0].ParameterName = "@AplicacaoId";
            parms[0].Value = _localProps.appid;

            cmd.Parameters.Add(parms[0]);

            cmd.CommandText = cmds;
            cmd.Connection = conn;

            dt.Columns.Add("Login");
            dt.Columns.Add("Aplicacao");
            dt.Columns.Add("Data Inclusao");
            dt.Columns.Add("UserId");
            dt.Columns.Add("AplicacaoId");

            try
            {
                conn.Open();
                IDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    DataRow _dr = dt.NewRow();
                    _dr["UserId"] = dr["UserId"];
                    _dr["Login"] = dr["Apelido"];
                    _dr["Aplicacao"] = dr["Nome"];
                    _dr["AplicacaoId"] = dr["AplicacaoId"];
                    _dr["Data Inclusao"] = dr["DataInclusao"];
                    dt.Rows.Add(_dr);
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
