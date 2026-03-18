using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ICMSX;
using System.Linq;
using CMSXData;
using CMSXData.Models;
using System.Dynamic;

namespace CMSXBLL.Repositorio
{
    public class UnidadeRepositorio : BaseRepositorio, IUnidadeRepositorio
    {
        private ICategoriaDAL dal;

        public void MakeConnection(dynamic prop)
        {
            dal = container.Resolve<ICategoriaDAL>();
            db = new CmsxDbContext();
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
            IEnumerable<Unidade> lst = from uni in db.Unidades
                                        select new Unidade()
                                        {
                                            unidadeId = uni.Unidadeid,
                                            nome = uni.Nome,
                                            sigla = uni.Sigla
                                        };
            return Helper(lst);
        }

        public List<Unidade> Helper(IEnumerable<Unidade> entLista)
        {
            if (entLista == null) return null;
            List<Unidade> oLista = new List<Unidade>();

            foreach (Unidade obj in entLista)
            {
                Unidade _un = Unidade.ObterNovaUnidade();
                _un.unidadeId = obj.unidadeId;
                _un.nome = obj.nome;
                _un.sigla = obj.sigla;
                oLista.Add(_un);
            }
            return oLista;
        }
    }
}
