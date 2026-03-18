using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ICMS
{
    public interface IImagem
    {
        Guid Imagem { get; set; }
        string Url { get; set; }
        int Altura { get; set; }
        int Largura { get; set; }

        void MakeConnection(dynamic prop);
        void CriaImagem();
        void CriaGaleria();
        void AtualizaGaleria();
        DataTable ListaImagem();
        DataTable ListaImagemPorAreaId();//Galeria
        DataTable ObtemImagemPorid();
        DataTable ObtemImagemPorAreaId();
        DataTable ObtemImagemPorConteudoId();
    }
}
