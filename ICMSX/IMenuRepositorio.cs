using CMSXData.Models;

namespace ICMSX
{
    public interface IMenuRepositorio
    {
        void MakeConnection(dynamic prop);
        List<Area> MontaMenu();
        List<Area> MontaMenu(string id);
    }
}