using System;
using System.Collections.Generic;
using System.Dynamic;
using CMSXData.Models;
using ICMSX;

namespace CMSXRepo
{
    public class ArquivoRepositorio : BaseRepositorio, IArquivoRepositorio
    {
        private readonly IArquivoDAL _dal;

        public ArquivoRepositorio(CMSXData.Models.CmsxDbContext db, IArquivoDAL dal) : base(db) { _dal = dal; }

        public void MakeConnection() => throw new NotImplementedException();
        public void CriaArquivo() => throw new NotImplementedException();
        public Arquivo ObtemArquivoPorNome() => throw new NotImplementedException();
        public Arquivo ObtemArquivoPorId() => throw new NotImplementedException();
        public List<Arquivo> ListaArquivoPorAreaId() => throw new NotImplementedException();
        public List<Arquivo> ListaArquivoPorConteudoId() => throw new NotImplementedException();
    }
}