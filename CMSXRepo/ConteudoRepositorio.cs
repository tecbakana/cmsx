using System;
using System.Collections.Generic;
using System.Dynamic;
using CMSXData.Models;
using ICMSX;

namespace CMSXRepo
{
    public class ConteudoRepositorio : BaseRepositorio, IConteudoRepositorio
    {
        private readonly IConteudoDAL _dal;

        public ConteudoRepositorio(CmsxDbContext db, IConteudoDAL dal) : base(db) { _dal = dal; }

        public void MakeConnection(dynamic prop) => _dal.MakeConnection((ExpandoObject)prop);

        public List<Conteudo> ObtemConteudoPorId() => throw new NotImplementedException();
        public void CriaNovoConteudo(Conteudo conteudo) => throw new NotImplementedException();
        public void EditaConteudo(Conteudo conteudo) => throw new NotImplementedException();
        public void CreateContent() => throw new NotImplementedException();
        public void CreateValue() => throw new NotImplementedException();
        public void EditContent() => throw new NotImplementedException();
        public void EditValue() => throw new NotImplementedException();
        public List<Conteudo> ListaConteudoPorAreaId() => throw new NotImplementedException();
        public List<Conteudo> ListaConteudoPorAplicacaoId() => throw new NotImplementedException();
        public List<Conteudo> ListaValor() => throw new NotImplementedException();
        public void InativaConteudo() => throw new NotImplementedException();
    }
}