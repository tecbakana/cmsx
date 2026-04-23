using System;
using System.Collections.Generic;
using System.Dynamic;
using CMSXData.Models;
using ICMSX;

namespace CMSXRepo
{
    public class UsuarioRepositorio : BaseRepositorio, IUsuarioRepositorio
    {
        private readonly IUsuarioDAL _dal;

        public UsuarioRepositorio(CMSXData.Models.CmsxDbContext db, IUsuarioDAL dal) : base(db) { _dal = dal; }

        public void MakeConnection(dynamic prop) => _dal.MakeConnection((ExpandoObject)prop);
        public Usuario ObtemUsuarioPorId(Guid id) => throw new NotImplementedException();
        public void CriaUsuario() => throw new NotImplementedException();
        public List<Usuario> ListaUsuarios() => throw new NotImplementedException();
        public List<Usuario> ListaUsuariosPorAppId() => throw new NotImplementedException();
        public void InativaUsuario() => throw new NotImplementedException();
    }
}