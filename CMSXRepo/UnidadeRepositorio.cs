using System.Collections.Generic;
using CMSXData.Models;
using ICMSX;

namespace CMSXRepo
{
    public class UnidadeRepositorio : BaseRepositorio, IUnidadeRepositorio
    {
        public UnidadeRepositorio(CMSXData.Models.CmsxDbContext db) : base(db) { }

        public void MakeConnection(dynamic prop) => throw new NotImplementedException();
        public void CriaNovaUnidade() => throw new NotImplementedException();
        public List<Unidade> ListaUnidade() => throw new NotImplementedException();
    }
}