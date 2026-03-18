using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ICMSX
{
    public interface IAreasDAL
    {
        int NumParms { get; set; }
        void CriaArea();
        void MakeConnection(dynamic prop);
        DataTable ListaDictAreas();
        DataTable ListaAreas();
        DataTable ListaAreasPorTipo();
        DataTable ListaAreasPorId();
        DataTable ListaAreasFilha();
        DataTable ListaAreasPai();
        DataTable ListaAreaMenu();
        DataTable ListaMenuPorTipo();
        DataTable ListaAreaMenuLateral();
        DataTable ListaAreaMenuSplash();
        string AreaRapida();
        void EditaArea();
        void EditaAreaPosicao();
        void InativaArea();
    }
}
