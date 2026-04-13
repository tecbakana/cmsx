using CMSXData.Models;

namespace ICMSX
{
    public interface IModuloRepositorio
    {
        void MakeConnection(dynamic prop);
        List<Modulo> ListaModulos();
        void CriaModulo();
    }
}