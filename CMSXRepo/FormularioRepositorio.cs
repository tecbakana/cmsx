using System.Collections.Generic;
using System.Dynamic;
using CMSXData.Models;
using ICMSX;

namespace CMSXRepo
{
    public class FormularioRepositorio : BaseRepositorio, IFormularioRepositorio
    {
        private readonly IFormularioDAL _dal;

        public FormularioRepositorio(CMSXData.Models.CmsxDbContext db, IFormularioDAL dal) : base(db) { _dal = dal; }

        public void MakeConnection(dynamic prop) => _dal.MakeConnection((ExpandoObject)prop);
        public void CriaFormulario() => throw new NotImplementedException();
        public List<Formulario> ListaFormulario() => throw new NotImplementedException();
        public List<Formulario> ListaFormularioPorId() => throw new NotImplementedException();
    }
}