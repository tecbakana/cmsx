using CMSXData.Models;

namespace ICMSX
{
    public interface IAtributoRepositorio
    {
        void MakeConnection(dynamic prop);
        List<Atributo> ListaAtributo();
        List<Atributo> ListaAtributoXProduto();
        void CriaAtributo(Atributo at);
        void InativaAtributo();
    }
}