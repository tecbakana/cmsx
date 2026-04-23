using CMSXData.Models;

namespace ICMSX
{
    public interface IImagemRepositorio
    {
        Imagem ObtemImagemPorId(Guid id);
        void MakeConnection(dynamic prop);
        void CriaNovaImagem();
        void InsereImagemGaleria();
        void AtualizaGaleria();
        List<Imagem> Galeria();
        List<Imagem> GaleriaConteudo();
        List<Imagem> GaleriaParentId();
    }
}