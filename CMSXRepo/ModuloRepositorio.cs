using System.Collections.Generic;
using System.Dynamic;
using CMSXData.Models;
using ICMSX;

namespace CMSXRepo
{
    public class ModuloRepositorio : BaseRepositorio, IModuloRepositorio
    {
        private readonly IModuloDAL _dal;

        public ModuloRepositorio(CMSXData.Models.CmsxDbContext db, IModuloDAL dal) : base(db) { _dal = dal; }

        public void MakeConnection(dynamic prop) => _dal.MakeConnection((ExpandoObject)prop);
        public List<Modulo> ListaModulos() => throw new NotImplementedException();
        public void CriaModulo() => throw new NotImplementedException();
    }
}