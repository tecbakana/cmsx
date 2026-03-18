using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using ICMSX;
using CMSXData;
using CMSXData.Models;
using System.Dynamic;

namespace CMSXBLL.Repositorio
{
    public class OpcaoRepositorio : BaseRepositorio, IOpcaoRepositorio
    {
        private IOpcaoDAL dal;

        #region IAplicacaoRepositorio Members

        public void MakeConnection(dynamic prop)
        {
            dal = container.Resolve<IOpcaoDAL>();
            string bc = prop.banco;
            int parm = prop.parms;
            lprop = prop;
            db = new CmsxDbContext();
            dal.MakeConnection((ExpandoObject)prop);
        }

        public List<Opcao> Helper(DataTable appdata)
        {
            if (appdata == null) return null;
            List<Opcao> applista = new List<Opcao>();

            //foreach (DataRow dr in appdata.Rows)
            //{
            //    Opcao _app = Opcao.ObterNovaOpcao();
            //    _app.Nome = dr["Nome"].ToString();
            //    _app.Descricao   = dr["Descricao"].ToString();
            //    _app.OpcaoId = int.Parse(dr["OpcaoId"].ToString());
            //    _app.ProdutoId = new System.Guid(dr["ProdutoId"].ToString());

            //    applista.Add(_app);
            //}
            return applista;
        }

        public List<Opcao> Helper(IEnumerable<Opcao> appdata)
        {
            if (appdata == null) return null;
            List<Opcao> applista = new List<Opcao>();

            foreach (Opcao at in appdata)
            {
                Opcao _app = Opcao.ObterNovaOpcao();
                _app.Nome = at.Nome;
                _app.Descricao = at.Descricao;
                _app.OpcaoId = new System.Guid(at.OpcaoId.ToString());
                _app.AtributoId = at.AtributoId;
                _app.Qtd = int.Parse(at.Qtd.ToString());
                _app.Estoque = at.Estoque;

                applista.Add(_app);
            }
            return applista;
        }

        public void CriaOpcao(Opcao atp)
        {
            using (CmsxDbContext dbLoc = new CmsxDbContext())
            {
                var at = new CMSXData.Models.Opcao();
                at.Nome         = atp.Nome;
                at.Descricao    = atp.Descricao;
                at.Opcaoid      = atp.OpcaoId.ToString();
                at.Atributoid   = atp.AtributoId;
                at.Qtd          = atp.Qtd;
                at.Estoque      = (byte)(atp.Estoque == true ? 1 : 0);
                dbLoc.Opcaos.Add(at);
                dbLoc.SaveChanges();
            }
        }

        public List<Opcao> ListaOpcao()
        {
            string atrId = lprop.atrId.ToString();
            //long lng = long.Parse(atrId);
            Guid id = new Guid(atrId);
            IEnumerable<Opcao> lst = from opt in db.Opcaos
                                     where opt.Atributoid == id
                                        select new Opcao()
                                        {
                                            AtributoId = opt.Atributoid,
                                            Nome = opt.Nome,
                                            Descricao = opt.Descricao,
                                            OpcaoId = new Guid(opt.Opcaoid),
                                            Estoque = opt.Estoque==1?true:false,
                                            Qtd = opt.Qtd
                                        };

            return Helper(lst);
        }

        public void InativaOpcao()
        {
            throw new NotImplementedException();
        }

        public List<Opcao> ListaOpcaoXAtributo()
        {
            string optId = lprop.optId.ToString();
            IEnumerable<Opcao> lst = from opt in db.Opcaos
                                     where opt.Opcaoid == optId
                                     select new Opcao()
                                     {
                                         AtributoId = opt.Atributoid,
                                         Nome = opt.Nome,
                                         Descricao = opt.Descricao,
                                         OpcaoId = new Guid(opt.Opcaoid),
                                         Estoque = opt.Estoque == 1 ? true : false,
                                         Qtd = opt.Qtd
                                     };

            return Helper(lst);
        }

        #endregion
    }
}
