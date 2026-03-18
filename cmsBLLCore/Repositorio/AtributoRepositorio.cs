using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using ICMSX;
using CMSXData;
using System.Dynamic;
using CMSXData.Models;

namespace CMSXBLL.Repositorio
{
    public class AtributoRepositorio : BaseRepositorio, IAtributoRepositorio
    {
        private IAtributoDAL dal;

        #region IAplicacaoRepositorio Members

        public void MakeConnection(dynamic prop)
        {
            dal = container.Resolve<IAtributoDAL>();
            string bc = prop.banco;
            int parm = prop.parms;
            lprop = prop;
            dal.MakeConnection((ExpandoObject)prop);
        }

        public List<Atributo> Helper(DataTable appdata)
        {
            if (appdata == null) return null;
            List<Atributo> applista = new List<Atributo>();

            foreach (DataRow dr in appdata.Rows)
            {
                Atributo _app = Atributo.ObterNovoAtributo();
                _app.Nome = dr["Nome"].ToString();
                _app.Descricao   = dr["Descricao"].ToString();
                _app.AtributoId = int.Parse(dr["AtributoId"].ToString());
                _app.ProdutoId = new System.Guid(dr["ProdutoId"].ToString());

                applista.Add(_app);
            }
            return applista;
        }

        public List<Atributo> Helper(IEnumerable<Atributo> appdata)
        {
            if (appdata == null) return null;
            List<Atributo> applista = new List<Atributo>();

            foreach (Atributo at in appdata)
            {
                Atributo _app = Atributo.ObterNovoAtributo();
                _app.Nome = at.Nome;
                _app.Descricao = at.Descricao;
                _app.AtributoId = int.Parse(at.AtributoId.ToString());
                _app.ProdutoId = at.ProdutoId;

                applista.Add(_app);
            }
            return applista;
        }

        #endregion

        public Atributo ObtemCategoriaPorId()
        {
            return Helper(dal.ListaAtributos())[0];
        }

        public void CriaAtributo(Atributo atp)
        {
            using (CmsxDbContext dbLoc = new CmsxDbContext())
            {
                var at = new CMSXData.Models.Atributo();
                at.Nome = atp.Nome;
                at.Descricao = atp.Descricao;
                at.Produtoid = atp.ProdutoId.ToString() ;
                dbLoc.Atributos.Add(at);
                dbLoc.SaveChanges();
            }
        }

        public List<Atributo> ListaAtributo()
        {
            string prodId = lprop.prodId.ToString();
            IEnumerable<Atributo> lst = from atrib in db.Atributos
                                        select new Atributo()
                                        {
                                            AtributoId = 0,
                                            Descricao = atrib.Descricao,
                                            Nome = atrib.Nome
                                        };

            return Helper(lst);
        }

        public List<Atributo> ListaAtributoXProduto()
        {
            string prodId = lprop.prodId.ToString();
            IEnumerable<Atributo> lst = from atrib in db.Atributos
                                        where atrib.Produtoid == prodId
                                        select new Atributo()
                                        {
                                            AtributoId = 0,
                                            Descricao = atrib.Descricao,
                                            Nome = atrib.Nome
                                        };

            return Helper(lst);
        }


        public void InativaAtributo()
        {
            throw new NotImplementedException();
        }
    }
}
