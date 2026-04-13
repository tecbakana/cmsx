using ICMSX;

namespace CMSXRepo
{
    public class EmaiRepositorio : BaseRepositorio, IEmailRepositorio
    {
        public EmaiRepositorio(CMSXData.Models.CmsxDbContext db) : base(db) { }

        public void MontaEmail() => throw new NotImplementedException();
        public void Enviar() => throw new NotImplementedException();
    }
}