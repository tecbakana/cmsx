using CMSXData.Models;

namespace ICMSX
{
    public interface IUnidadeRepositorio
    {
        void MakeConnection(dynamic prop);
        void CriaNovaUnidade();
        List<Unidade> ListaUnidade();
    }
}