using CMSXData.Models;

namespace ICMSX
{
    public interface IRelacaoRepositorio
    {
        void MakeConnection(dynamic prop);
        List<Relmoduloaplicacao> ListaRelacaoModuloAplicacao();
        void CriaRelacaoAplicacao();
        void CriaRelacaoModulo();
    }
}