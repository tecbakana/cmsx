using ICMSX;

namespace CMSXRepo
{
    public class RoteiroRepositorio : BaseRepositorio, IRoteiroRepositorio
    {
        public RoteiroRepositorio(CMSXData.Models.CmsxDbContext db) : base(db) { }

        public void MakeConnection(dynamic prop) => throw new NotImplementedException();
    }
}