using System;
using System.Collections.Generic;
using System.Dynamic;
using CMSXData.Models;
using ICMSX;

namespace CMSXRepo
{
    public class AtributoRepositorio : BaseRepositorio, IAtributoRepositorio
    {
        private readonly IAtributoDAL _dal;

        public AtributoRepositorio(CmsxDbContext db, IAtributoDAL dal) : base(db) { _dal = dal; }

        public void MakeConnection(dynamic prop) => _dal.MakeConnection((ExpandoObject)prop);

        public List<Atributo> ListaAtributo() => throw new NotImplementedException();
        public List<Atributo> ListaAtributoXProduto() => throw new NotImplementedException();
        public void CriaAtributo(Atributo at) => throw new NotImplementedException();
        public void InativaAtributo() => throw new NotImplementedException();
    }
}