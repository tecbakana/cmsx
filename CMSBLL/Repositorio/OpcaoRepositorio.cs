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
            db = new cmsxDBEntities();
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

        public List<Opcao> Helper(IEnumerable<opcao> appdata)
        {
            if (appdata == null) return null;
            List<Opcao> applista = new List<Opcao>();

            foreach (opcao at in appdata)
            {
                Opcao _app = Opcao.ObterNovaOpcao();
                _app.Nome = at.Nome;
                _app.Descricao = at.Descricao;
                _app.OpcaoId = new System.Guid(at.OpcaoId.ToString());
                _app.AtributoId = at.AtributoId;
                _app.Qtd = int.Parse(at.Qtd.ToString());
                _app.Estoque = (at.Estoque == 1 ? true : false);

                applista.Add(_app);
            }
            return applista;
        }

        public void CriaOpcao(Opcao atp)
        {
            using (cmsxDBEntities dbLoc = new cmsxDBEntities())
            {
                opcao at = new opcao();
                at.Nome         = atp.Nome;
                at.Descricao    = atp.Descricao;
                at.OpcaoId      = atp.OpcaoId.ToString();
                at.AtributoId   = atp.AtributoId;
                at.Qtd          = atp.Qtd;
                at.Estoque      = (byte)(atp.Estoque == true ? 1 : 0);
                dbLoc.opcao.Add(at);
                dbLoc.SaveChanges();
            }
        }

        public List<Opcao> ListaOpcao()
        {
            string atrId = lprop.atrId.ToString();
            //long lng = long.Parse(atrId);
            Guid id = new Guid(atrId);
            IEnumerable<opcao> lst = from opt in db.opcao
                                     where opt.AtributoId == id
                                        select opt;

            return Helper(lst);
        }

        public void InativaOpcao()
        {
            throw new NotImplementedException();
        }

        public List<Opcao> ListaOpcaoXAtributo()
        {
            string optId = lprop.optId.ToString();
            IEnumerable<opcao> lst = from opt in db.opcao
                                     where opt.OpcaoId == optId
                                     select opt;

            return Helper(lst);
        }

        #endregion
    }
}
