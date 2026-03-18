using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CMSXBLL.Repositorio
{
    interface IImagemRepositorio
    {
        Imagem ObtemImagemPorId(Guid id);
        void MakeConnection(dynamic prop);
        void CriaNovaImagem();
        void InsereImagemGaleria();
        void AtualizaGaleria();
        List<Imagem> Galeria();
        List<Imagem> GaleriaConteudo();
        List<Imagem> GaleriaParentId();
        List<Imagem> Helper(DataTable appdata);
    }
}
