using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using ICMSX; 
using System.Dynamic;

namespace CMSXBLL.Repositorio
{
    public class AcessoRepositorio : BaseRepositorio, IAcessoRepositorio
    {
        #region IAcessoRepositorio Members

        private IAcessoDAL dal;

        public void MakeConnection(dynamic prop)
        {
            dal = container.Resolve<IAcessoDAL>();
            string bc = prop.banco;
            int parm = prop.parms;
            dal.MakeConnection((ExpandoObject)prop);
        }

        public dynamic ValidaAcesso()
        {
            dynamic _user = new ExpandoObject();
            DataTable dr = dal.ValidaAcesso();
            try
            {
                if (dr.Rows.Count == 1)
                {
                    _user.parms = 2;
                    _user.userid = new System.Guid(dr.Rows[0]["userid"].ToString());
                    _user.aplicacaoid = new System.Guid(dr.Rows[0]["AplicacaoId"].ToString());
                    _user.url = dr.Rows[0]["Url"].ToString();
                }
            }
            catch (DataException ex)
            {
                throw new Exception("Houve um erro ao tentar recuperar o usuario:" + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Houve um erro ao tentar recuperar o usuario:" + ex.Message);
            }
            finally
            {

            }
            return _user;
        }

        #endregion
    }
}
