using System;
using System.Collections.Generic;
using System.Data;
using CMSXEF;

namespace CMSBLL.Repositorio
{
    public interface ICategoriaRepositorio
    {
        Categoria ObtemCategoriaPorId();
        void MakeConnection(dynamic prop);
        void CriaNovaCategoria();
        List<Categoria> ListaCategoria();
        List<Categoria> ListaCategoriaFull();
        List<Categoria> ListaCategoriaPai();
        List<Categoria> ListaSubCategoria();
        List<Categoria> Helper(DataTable appdata);
        List<Categoria> Helper(IEnumerable<categoria> appdata);
        void InativaCategorias();
    }
}
