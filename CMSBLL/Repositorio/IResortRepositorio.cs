using System;
using System.Collections.Generic;
using System.Data;

namespace CMSBLL.Repositorio
{
    public interface IResortRepositorio
    {
        //Resort ObtemResortPorId(int id);
        void MakeConnection(dynamic prop);
        //void CriaResort();
        List<ResortBLL> ListaResorts();
        List<ResortBLL> Helper(DataTable resortdata);
        //void InativaResort();
    }
}
