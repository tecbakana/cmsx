using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ICMSX;
using System.Dynamic;

namespace CMSXBLL.Repositorio
{
    public class UsuarioRepositorio : BaseRepositorio,IUsuarioRepositorio
    {
        private IUsuarioDAL dal;

        #region IUsuarioRepositorio Members

        public void MakeConnection(dynamic prop)
        {
            dal = container.Resolve<IUsuarioDAL>();
            string bc = prop.banco;
            int parm = prop.parms;
            dal.MakeConnection((ExpandoObject)prop);
        }

        public UsuarioBLL ObtemUsuarioPorId(Guid id)
        {
            return null;
        }

        public void CriaUsuario()
        {
            dal.CriaUsuario();
        }

        public List<UsuarioBLL> ListaUsuarios()
        {
            var usuarios = Helper(dal.ListaUsuario());
            return usuarios;
        }

        public List<UsuarioBLL> ListaUsuariosPorAppId()
        {
            var usuarios = Helper(dal.ListaUsuarioPorAplicacaoId());
            return usuarios;
        }

        public List<UsuarioBLL> Helper(DataTable usudata)
        {
            if (usudata == null) return null;
            List<UsuarioBLL> usulista = new List<UsuarioBLL>();

            foreach (DataRow dr in usudata.Rows)
            {
                UsuarioBLL _usu   = UsuarioBLL.ObterNovoUsuario();
                _usu.UserId       = new System.Guid(dr["UserId"].ToString());
                _usu.Apelido      = dr["Login"].ToString();
                _usu.Aplicacao    = dr["Aplicacao"].ToString();
                _usu.DataInclusao = DateTime.Parse(dr["Data Inclusao"].ToString());

                usulista.Add(_usu);
            }
            return usulista;
        }

        public void InativaUsuario()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
