using System.Collections.Generic;
using System.Dynamic;
using CMSXData.Models;
using ICMSX;

namespace CMSXRepo
{
    public class MenuRepositorio : BaseRepositorio, IMenuRepositorio
    {
        private readonly IMenuDAL _dal;

        public MenuRepositorio(CMSXData.Models.CmsxDbContext db, IMenuDAL dal) : base(db) { _dal = dal; }

        public void MakeConnection(dynamic prop) => _dal.MakeConnection((ExpandoObject)prop);
        public List<Area> MontaMenu() => throw new NotImplementedException();
        public List<Area> MontaMenu(string id) => throw new NotImplementedException();
    }
}