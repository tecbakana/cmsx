using System;
using System.Collections.Generic;
using System.Data;
using CMSXData;

namespace CMSXBLL.Repositorio
{
    public interface IOpcaoRepositorio
    {
        void MakeConnection(dynamic prop);
        List<Opcao> Helper(DataTable attdata);
        List<Opcao> Helper(IEnumerable<Opcao> lst);
        List<Opcao> ListaOpcao();
        List<Opcao> ListaOpcaoXAtributo();
        void CriaOpcao(Opcao op);
        void InativaOpcao();
    }
}
