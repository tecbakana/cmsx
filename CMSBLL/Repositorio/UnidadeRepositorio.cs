using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ICMS;
using System.Linq;
using CMSXEF;
using System.Dynamic;

namespace CMSBLL.Repositorio
{
    public class UnidadeRepositorio : BaseRepositorio, IUnidadeRepositorio
    {
        private ICategoriaDAL dal;

        public void MakeConnection(dynamic prop)
        {
            dal = container.Resolve<ICategoriaDAL>();
            db = new cmsxDBEntities();
            string bc = prop.banco;
            int parm = prop.parms;
            lprop = prop;
            dal.MakeConnection((ExpandoObject)prop);
        }

        public void CriaNovaUnidade()
        {
            throw new NotImplementedException();
        }

        public List<Unidade> ListaUnidade()
        {
            
            string appid = lprop.appid;
            IEnumerable<unidades> lst = from uni in db.unidades
                                        select uni;
            return Helper(lst);
        }

        public List<Unidade> Helper(IEnumerable<unidades> entLista)
        {
            if (entLista == null) return null;
            List<Unidade> oLista = new List<Unidade>();

            foreach (unidades obj in entLista)
            {
                Unidade _un = Unidade.ObterNovaUnidade();
                _un.unidadeId = obj.UnidadeId;
                _un.nome = obj.Nome;
                _un.sigla = obj.Sigla;
                oLista.Add(_un);
            }
            return oLista;
        }
    }
}
