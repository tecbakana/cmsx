using System;
using System.Collections.Generic;
using System.Dynamic;
using CMSXData.Models;
using ICMSX;

namespace CMSXRepo
{
    public class CategoriaRepositorio : BaseRepositorio, ICategoriaRepositorio
    {
        private readonly ICategoriaDAL _dal;

        public CategoriaRepositorio(CmsxDbContext db, ICategoriaDAL dal) : base(db) { _dal = dal; }

        public void MakeConnection(dynamic prop) => _dal.MakeConnection((ExpandoObject)prop);

        public Caterium ObtemCategoriaPorId() => throw new NotImplementedException();
        public void CriaNovaCategoria() => throw new NotImplementedException();
        public List<Caterium> ListaCategoria() => throw new NotImplementedException();
        public List<Caterium> ListaCategoriaFull() => throw new NotImplementedException();
        public List<Caterium> ListaCategoriaPai() => throw new NotImplementedException();
        public List<Caterium> ListaSubCategoria() => throw new NotImplementedException();
        public void InativaCategorias() => throw new NotImplementedException();
    }
}