using System;
using System.Collections.Generic;
using System.Dynamic;
using CMSXData.Models;
using ICMSX;

namespace CMSXRepo
{
    public class OpcaoRepositorio : BaseRepositorio, IOpcaoRepositorio
    {
        private readonly IOpcaoDAL _dal;

        public OpcaoRepositorio(CmsxDbContext db, IOpcaoDAL dal) : base(db) { _dal = dal; }

        public void MakeConnection(dynamic prop) => _dal.MakeConnection((ExpandoObject)prop);

        public List<Opcao> ListaOpcao() => throw new NotImplementedException();
        public List<Opcao> ListaOpcaoXAtributo() => throw new NotImplementedException();
        public void CriaOpcao(Opcao op) => throw new NotImplementedException();
        public void InativaOpcao() => throw new NotImplementedException();
    }
}