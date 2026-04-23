using System;
using System.Collections.Generic;
using System.Dynamic;
using CMSXData.Models;
using ICMSX;

namespace CMSXRepo
{
    public class AplicacaoRepositorio : BaseRepositorio, IAplicacaoRepositorio
    {
        private readonly IAplicacaoDAL _dal;

        public AplicacaoRepositorio(CmsxDbContext db, IAplicacaoDAL dal) : base(db) { _dal = dal; }

        public void MakeConnection(dynamic prop) => _dal.MakeConnection((ExpandoObject)prop);

        public Aplicacao ObtemAplicacaoPorId(Guid id) => throw new NotImplementedException();
        public Aplicacao RegistraAplicacao() => throw new NotImplementedException();
        public bool CriaAplicacao() => throw new NotImplementedException();
        public void ExcluiAplicacao() => throw new NotImplementedException();
        public void Edita() => throw new NotImplementedException();
        public string AtivaAplicacao() => throw new NotImplementedException();
        public List<Aplicacao> ListaAplicacao() => throw new NotImplementedException();
        public List<Aplicacao> ListaAplicacaoForAutocomplete() => throw new NotImplementedException();
        public string[] ListaAplicacaoPorNome() => throw new NotImplementedException();
    }
}