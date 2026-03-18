using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ICMSX;
using System.Dynamic;

namespace CMSXBLL.Repositorio
{
    public class ModuloRepositorio : BaseRepositorio,IModuloRepositorio
    {
        private IModuloDAL dal;

        #region IModuloRepositorio Members

        public void MakeConnection(dynamic prop)
        {
            dal = container.Resolve<IModuloDAL>();
            string bc = prop.banco;
            int parm = prop.parms;
            dal.MakeConnection((ExpandoObject)prop);
        }

        public List<Modulo> ListaModulos()
        {
            List<Modulo> modlista = Helper(dal.ListaModulos());
            return modlista;
        }

        public List<Modulo> ListaModulosXUser()
        {
            List<Modulo> modlista = Helper(dal.ListaModulosXUser());
            return modlista;
        }

        public void CriaModulo()
        {
            dal.CriaModulo();
        }

        /// <summary>
        /// Helper - transforma a DataTable recebida da camada de dados em um objeto do tipo List
        /// </summary>
        /// <param name="moddata">DataTable</param>
        /// <returns>List</returns>
        public List<Modulo> Helper(System.Data.DataTable moddata)
        {
            if (moddata == null) return null;
            List<Modulo> modlista = new List<Modulo>();

            foreach (DataRow dr in moddata.Rows)
            {
                Modulo _mod = Modulo.ObterNovoModulo();
                _mod.ModuloId = new System.Guid(dr["ModuloId"].ToString());

                if (dr.Table.Columns.Contains("modulo"))
                {
                    _mod.Nome = dr["Modulo"].ToString();
                }
                else
                {
                    _mod.Nome = dr["Nome"].ToString();
                }

                if (dr.Table.Columns.Contains("valida"))
                {
                    _mod.RelacaoUsuario = int.Parse(dr["valida"].ToString());
                }
                _mod.Url = dr["Url"].ToString();

                modlista.Add(_mod);
            }
            return modlista;
        }

        #endregion
    }
}
