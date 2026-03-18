using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace CMSBLL.Repositorio
{
    public interface IRelacaoRepositorio
    {
        void MakeConnection(dynamic prop);
        List<Relacao> ListaRelacao();
        void CriaRelacaoAplicacao();
        void CriaRelacaoModulo();
        List<Relacao> Helper(DataTable reldata);
    }
}
