using System;
using System.Collections.Generic;
using System.Dynamic;
using CMSXData.Models;
using ICMSX;

namespace CMSXRepo
{
    public class AreasRepositorio : BaseRepositorio, IAreasRepositorio
    {
        private readonly IAreasDAL _dal;

        public AreasRepositorio(CmsxDbContext db, IAreasDAL dal) : base(db) { _dal = dal; }

        public void MakeConnection(dynamic prop) => _dal.MakeConnection((ExpandoObject)prop);

        public Area ObtemAreaPorId() => throw new NotImplementedException();
        public void CriaNovaArea() => throw new NotImplementedException();
        public string AreaRapida() => throw new NotImplementedException();
        public void EditaAreaPosicao() => throw new NotImplementedException();
        public List<Area> ListaAreas() => throw new NotImplementedException();
        public void InativaArea() => throw new NotImplementedException();
    }
}