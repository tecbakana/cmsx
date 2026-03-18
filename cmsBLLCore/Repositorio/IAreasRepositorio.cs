using System;
using System.Collections.Generic;
using System.Data;

namespace CMSXBLL.Repositorio
{
    public interface IAreasRepositorio
    {
        Areas ObtemAreaPorId();
        void MakeConnection(dynamic prop);
        void CriaNovaArea();
        string AreaRapida();
        void EditaAreaPosicao();
        List<Areas> ListaAreas();
        List<Areas> Helper(DataTable appdata);
        void InativaArea();
    }
}
