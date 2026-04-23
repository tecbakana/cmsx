using CMSXData.Models;

namespace ICMSX
{
    public interface ICategoriaRepositorio
    {
        Caterium ObtemCategoriaPorId();
        void MakeConnection(dynamic prop);
        void CriaNovaCategoria();
        List<Caterium> ListaCategoria();
        List<Caterium> ListaCategoriaFull();
        List<Caterium> ListaCategoriaPai();
        List<Caterium> ListaSubCategoria();
        void InativaCategorias();
    }
}