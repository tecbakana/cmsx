using CMSXData.Models;

namespace ICMSX
{
    public interface IAreasRepositorio
    {
        Area ObtemAreaPorId();
        void MakeConnection(dynamic prop);
        void CriaNovaArea();
        string AreaRapida();
        void EditaAreaPosicao();
        List<Area> ListaAreas();
        void InativaArea();
    }
}