using System;
using System.Collections.Generic;
using System.Data;

namespace CMSBLL.Repositorio
{
    public interface IArquivoRepositorio
    {
        void MakeConnection();
        void CriaArquivo();
        Arquivo ObtemArquivoPorNome();
        Arquivo ObtemArquivoPorId();
        List<Arquivo> ListaArquivoPorAreaId();
        List<Arquivo> ListaArquivoPorConteudoId();
        List<Arquivo> Helper(DataTable arqdata);
    }
}
