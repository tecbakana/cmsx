using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICMS
{
    public interface IAplicacaoDAL
    {
        void MakeConnection(dynamic prop);
        DataTable ListaAplicacao();
        DataTable ListaAplicacaoForAutocomplete();
        DataTable ObtemAplicacaoPorId(Guid id);
        DataTable ListaAplicacaoPorNome();
        Guid ObtemIdAplicacaoPorNome();
        void CriaAplicacao();
        void EditaAplicacao();
        void InativaAplicacao();
        void AtivaAplicacao();
    }
}
