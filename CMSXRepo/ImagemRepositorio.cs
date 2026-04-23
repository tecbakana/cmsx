using System;
using System.Collections.Generic;
using System.Dynamic;
using CMSXData.Models;
using ICMSX;

namespace CMSXRepo
{
    public class ImagemRepositorio : BaseRepositorio, IImagemRepositorio
    {
        public ImagemRepositorio(CMSXData.Models.CmsxDbContext db) : base(db) { }

        public void MakeConnection(dynamic prop) => throw new NotImplementedException();
        public Imagem ObtemImagemPorId(Guid id) => throw new NotImplementedException();
        public void CriaNovaImagem() => throw new NotImplementedException();
        public void InsereImagemGaleria() => throw new NotImplementedException();
        public void AtualizaGaleria() => throw new NotImplementedException();
        public List<Imagem> Galeria() => throw new NotImplementedException();
        public List<Imagem> GaleriaConteudo() => throw new NotImplementedException();
        public List<Imagem> GaleriaParentId() => throw new NotImplementedException();
    }
}