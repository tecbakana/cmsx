using ICMSX;

namespace CMSXRepo
{
    public class ResortRepositorio : BaseRepositorio, IResortRepositorio
    {
        public ResortRepositorio(CMSXData.Models.CmsxDbContext db) : base(db) { }

        public void MakeConnection(dynamic prop) => throw new NotImplementedException();
    }
}