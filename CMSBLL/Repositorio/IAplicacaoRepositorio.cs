using System;
using System.Collections.Generic;
using System.Data;
using CMSXEF;

namespace CMSBLL.Repositorio
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
        List<Aplicacao> Helper(DataTable appdata);
        List<Aplicacao> Helper(IEnumerable<aplicacao> appdata);
    }
}
