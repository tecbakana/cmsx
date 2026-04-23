using System;
using System.Collections.Generic;
using System.Dynamic;
using CMSXData.Models;
using ICMSX;

namespace CMSXRepo
{
    public class ProdutoRepositorio : BaseRepositorio, IProdutoRepositorio
    {
        private readonly IProdutoDAL _dal;

        public ProdutoRepositorio(CmsxDbContext db, IProdutoDAL dal) : base(db) { _dal = dal; }

        public void MakeConnection(dynamic prop) => _dal.MakeConnection((ExpandoObject)prop);

        public List<Produto> ListaProduto() => throw new NotImplementedException();
        public List<Produto> ListaProdutoXId() => throw new NotImplementedException();
        public List<Produto> ListaProdutoXCategoria() => throw new NotImplementedException();
        public List<Produto> ListaProdutoXTipo() => throw new NotImplementedException();
        public void CriaProduto(Produto prod) => throw new NotImplementedException();
        public void EditaProduto(Produto prod) => throw new NotImplementedException();
        public void InativaProduto(Produto prod) => throw new NotImplementedException();
    }
}