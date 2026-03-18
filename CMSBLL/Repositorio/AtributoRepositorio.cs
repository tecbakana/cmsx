using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using ICMS;
using CMSXEF;
using System.Dynamic;

namespace CMSBLL.Repositorio
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
            db = new cmsxDBEntities();
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

        public List<Atributo> Helper(IEnumerable<atributo> appdata)
        {
            if (appdata == null) return null;
            List<Atributo> applista = new List<Atributo>();

            foreach (atributo at in appdata)
            {
                Atributo _app = Atributo.ObterNovoAtributo();
                _app.Nome = at.Nome;
                _app.Descricao = at.Descricao;
                _app.AtributoId = int.Parse(at.AtributoId.ToString());
                _app.ProdutoId = new System.Guid(at.ProdutoId);

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
            using (cmsxDBEntities dbLoc = new cmsxDBEntities())
            {
                atributo at = new atributo();
                at.Nome = atp.Nome;
                at.Descricao = atp.Descricao;
                at.ProdutoId = atp.ProdutoId.ToString();
                dbLoc.atributo.Add(at);
                dbLoc.SaveChanges();
            }
        }

        public List<Atributo> ListaAtributo()
        {
            string prodId = lprop.prodId.ToString();
            IEnumerable<atributo> lst = from atrib in db.atributo
                                        select atrib;

            return Helper(lst);
        }

        public List<Atributo> ListaAtributoXProduto()
        {
            string prodId = lprop.prodId.ToString();
            IEnumerable<atributo> lst = from atrib in db.atributo
                                        where atrib.ProdutoId == prodId
                                        select atrib;

            return Helper(lst);
        }


        public void InativaAtributo()
        {
            throw new NotImplementedException();
        }
    }
}
