using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using ICMS;
using System.Dynamic;
using CMSXEF;

namespace CMSBLL.Repositorio
{
    public class MenuRepositorio : BaseRepositorio, IMenuRepositorio
    {
        #region IMenuRepositorio Members

        private IMenuDAL dal;

        public void MakeConnection(dynamic prop)
        {
            dal = container.Resolve<IMenuDAL>();
            string bc = prop.banco;
            int parm = prop.parms;
            lprop = prop;
            dal.MakeConnection((ExpandoObject)prop);
        }

        public DataTable montaMenu()
        {
            dynamic _user = new ExpandoObject();
            DataTable dr = dal.Menu();
            return dr;
        }

        #endregion


        public DataTable montaMenu(string id)
        {
            DataTable dt = new DataTable();

            return dt;
        }
    }
}
