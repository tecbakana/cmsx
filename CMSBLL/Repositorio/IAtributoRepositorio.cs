using System;
using System.Collections.Generic;
using System.Data;
using CMSXEF;

namespace CMSBLL.Repositorio
{
    public interface IAtributoRepositorio
    {
        void MakeConnection(dynamic prop);
        List<Atributo> Helper(DataTable attdata);
        List<Atributo> Helper(IEnumerable<atributo> lst);
        List<Atributo> ListaAtributo();
        List<Atributo> ListaAtributoXProduto();
        void CriaAtributo(Atributo at);
        void InativaAtributo();
    }
}
