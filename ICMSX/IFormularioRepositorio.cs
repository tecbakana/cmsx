using CMSXData.Models;

namespace ICMSX
{
    public interface IFormularioRepositorio
    {
        void MakeConnection(dynamic prop);
        void CriaFormulario();
        List<Formulario> ListaFormulario();
        List<Formulario> ListaFormularioPorId();
    }
}