using CMSXData.Models;

namespace ICMSX
{
    public interface IOpcaoRepositorio
    {
        void MakeConnection(dynamic prop);
        List<Opcao> ListaOpcao();
        List<Opcao> ListaOpcaoXAtributo();
        void CriaOpcao(Opcao op);
        void InativaOpcao();
    }
}