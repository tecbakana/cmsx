using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ICMS;
using System.Dynamic;
using CMSBLL;
using CMSBLL.Repositorio;


namespace CMSBLL.Repositorio
{
    public class FormularioRepositorio : BaseRepositorio,IFormularioRepositorio
    {
        private IFormularioDAL dal;
        #region IFormularioRepositorio Members

        public void MakeConnection(dynamic prop)
        {
            dal = container.Resolve<IFormularioDAL>();
            string bc = prop.banco;
            int parm = prop.parms;
            dal.MakeConnection((ExpandoObject)prop);
        }

        public void CriaFormulario()
        {
            dal.CriaFormulario();
        }

        public List<Formulario> ListaFormulario()
        {
            return Helper(dal.ListaFormulario());
        }

        public List<Formulario> ListaFormularioPorId()
        {
            return Helper(dal.ListaFormularioPorAreaId());
        }

        public List<Formulario> Helper(System.Data.DataTable appdata)
        {
            if (appdata == null) return null;
            List<Formulario> applista = new List<Formulario>();
            foreach (DataRow dr in appdata.Rows)
            {
                Formulario _app = Formulario.ObtemNovoFormulario();
                _app.Formularioid = new System.Guid(dr["Formularioid"].ToString());
                //_app.AreaId = new System.Guid(dr["AreaId"].ToString());
                _app.Nome = dr["Nome"].ToString();
                //_app.Ativo = dr["Area"].ToString();
                _app.Valor = dr["Valor"].ToString();
                //_app.Ativo = bool.Parse(dr["Ativo"].ToString());
                _app.DataInclusao = DateTime.Parse(dr["DataInclusao"].ToString());
                applista.Add(_app);
            }
            return applista;
        }

        #endregion
    }
}
