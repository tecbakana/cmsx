using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace ICMSX
{
    public interface IUsuarioDAL
    {
        void MakeConnection(dynamic prop);
        void CriaUsuario();
        DataTable ListaUsuario();
        DataTable ListaUsuarioPorAplicacaoId();
        void AtualizaUsuario();
    }
}
