using System.Collections.Generic;
using System.Dynamic;
using CMSXData.Models;
using ICMSX;

namespace CMSXRepo
{
    public class RelacaoRepositorio : BaseRepositorio, IRelacaoRepositorio
    {
        private readonly IRelacaoDAL _dal;

        public RelacaoRepositorio(CMSXData.Models.CmsxDbContext db, IRelacaoDAL dal) : base(db) { _dal = dal; }

        public void MakeConnection(dynamic prop) => _dal.MakeConnection((ExpandoObject)prop);
        public List<Relmoduloaplicacao> ListaRelacaoModuloAplicacao() => throw new NotImplementedException();
        public void CriaRelacaoAplicacao() => throw new NotImplementedException();
        public void CriaRelacaoModulo() => throw new NotImplementedException();
    }
}