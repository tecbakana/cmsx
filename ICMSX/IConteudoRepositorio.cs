using CMSXData.Models;

namespace ICMSX
{
    public interface IConteudoRepositorio
    {
        List<Conteudo> ObtemConteudoPorId();
        void MakeConnection(dynamic prop);
        void CriaNovoConteudo(Conteudo conteudo);
        void EditaConteudo(Conteudo conteudo);
        void CreateContent();
        void CreateValue();
        void EditContent();
        void EditValue();
        List<Conteudo> ListaConteudoPorAreaId();
        List<Conteudo> ListaConteudoPorAplicacaoId();
        List<Conteudo> ListaValor();
        void InativaConteudo();
    }
}