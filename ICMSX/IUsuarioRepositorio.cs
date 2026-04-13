using CMSXData.Models;

namespace ICMSX
{
    public interface IUsuarioRepositorio
    {
        Usuario ObtemUsuarioPorId(Guid id);
        void MakeConnection(dynamic prop);
        void CriaUsuario();
        List<Usuario> ListaUsuarios();
        List<Usuario> ListaUsuariosPorAppId();
        void InativaUsuario();
    }
}