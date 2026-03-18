using System;
using System.Collections.Generic;
using System.Data;

namespace CMSXBLL.Repositorio
{
    public interface IUsuarioRepositorio
    {
        UsuarioBLL ObtemUsuarioPorId(Guid id);
        void MakeConnection(dynamic prop);
        void CriaUsuario();
        List<UsuarioBLL> ListaUsuarios();
        List<UsuarioBLL> ListaUsuariosPorAppId();
        List<UsuarioBLL> Helper(DataTable usudata);
        void InativaUsuario();
    }
}
