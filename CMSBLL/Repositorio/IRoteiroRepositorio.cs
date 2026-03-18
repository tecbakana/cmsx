using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace CMSBLL.Repositorio
{
    public interface IRoteiroRepositorio
    {
        void MakeConnection(dynamic prop);
        List<RoteiroBLL> Helper(DataTable rotdata);
        List<RoteiroBLL> ListaRoteiroPorCidade();
        List<RoteiroBLL> ListaRoteiroPorFornecedor(int fornecedor);
        List<RoteiroBLL> ListaDetalheRoteiro();
        List<RoteiroBLL> ListaRoteiros(int Aplicacao);
    }
}
