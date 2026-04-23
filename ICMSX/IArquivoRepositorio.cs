using CMSXData.Models;

namespace ICMSX
{
    public interface IArquivoRepositorio
    {
        void MakeConnection();
        void CriaArquivo();
        Arquivo ObtemArquivoPorNome();
        Arquivo ObtemArquivoPorId();
        List<Arquivo> ListaArquivoPorAreaId();
        List<Arquivo> ListaArquivoPorConteudoId();
    }
}