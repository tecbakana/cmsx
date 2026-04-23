using System.Dynamic;
using ICMSX;

namespace CMSXRepo
{
    public class AcessoRepositorio : BaseRepositorio, IAcessoRepositorio
    {
        private readonly IAcessoDAL _dal;

        public AcessoRepositorio(CMSXData.Models.CmsxDbContext db, IAcessoDAL dal) : base(db) { _dal = dal; }

        public void MakeConnection(dynamic prop) => _dal.MakeConnection((ExpandoObject)prop);
        public dynamic ValidaAcesso() => throw new NotImplementedException();
    }
}