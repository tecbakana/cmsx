using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace ICMSX
{
    public interface IArquivoDAL
    {
        void MakeConnection(dynamic prop);
        void CriaArquivo();
        DataTable ListaArquivoPorAplicacaoId();
        DataTable ListaArquivoPorAreaId();
        DataTable ListaArquivoPorConteudoId();
        DataTable ObtemArquivoPorNome();
        DataTable ObtemArquivoPorId();
    }
}
