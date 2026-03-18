using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CMSXBLL.Repositorio
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
        List<Conteudo> Helper(DataTable appdata);
        void InativaConteudo();
    }
}
