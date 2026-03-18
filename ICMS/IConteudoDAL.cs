using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ICMS
{
    public interface IConteudoDAL
    {
        void CriaConteudo(Guid areaid, Guid conteudoid, string titulo, string texto, string autor);
        void EditaConteudo(Guid conteudoid, string titulo, string texto, string autor);
        object CreateContent();
        void EditContent();
        void CriaValor();
        void EditaValor();
        DataTable ListaValor();
        DataTable ListaConteudoPorAreaId();
        DataTable ListaConteudoPorCategoriaId();
        DataTable ObtemConteudoPorId();
        void MakeConnection(dynamic prop);
        void InativaConteudo();
    }
}
