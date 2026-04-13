using CMSXData.Models;

namespace ICMSX
{
    public interface IAplicacaoRepositorio
    {
        Aplicacao ObtemAplicacaoPorId(Guid id);
        Aplicacao RegistraAplicacao();
        bool CriaAplicacao();
        void ExcluiAplicacao();
        void Edita();
        string AtivaAplicacao();
        void MakeConnection(dynamic prop);
        List<Aplicacao> ListaAplicacao();
        List<Aplicacao> ListaAplicacaoForAutocomplete();
        string[] ListaAplicacaoPorNome();
    }
}