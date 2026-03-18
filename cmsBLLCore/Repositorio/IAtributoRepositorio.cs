using System;
using System.Collections.Generic;
using System.Data;
using CMSXData;

namespace CMSXBLL.Repositorio
{
    public interface IAtributoRepositorio
    {
        void MakeConnection(dynamic prop);
        List<Atributo> Helper(DataTable attdata);
        List<Atributo> Helper(IEnumerable<Atributo> lst);
        List<Atributo> ListaAtributo();
        List<Atributo> ListaAtributoXProduto();
        void CriaAtributo(Atributo at);
        void InativaAtributo();
    }
}
