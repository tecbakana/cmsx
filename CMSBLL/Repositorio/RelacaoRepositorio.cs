using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ICMS;
using System.Dynamic;

namespace CMSBLL.Repositorio
{
    public class RelacaoRepositorio : BaseRepositorio,IRelacaoRepositorio
    {

        #region IRelacaoRepositorio Members

        private IRelacaoDAL dal;

        public void MakeConnection(dynamic prop)
        {
            dal = container.Resolve<IRelacaoDAL>();
            string bc = prop.banco;
            int parm = prop.parms;
            dal.MakeConnection((ExpandoObject)prop);
        }

        public List<Relacao> ListaRelacao()
        {
            List<Relacao> rellista = Helper(dal.ListaRelacao());
            return rellista;
        }

        public void CriaRelacaoAplicacao()
        {
            dal.CriaRelacaoAplicacao();
        }

        public void CriaRelacaoModulo()
        {
            dal.CriaRelacaoModulo();
        }


        public List<Relacao> Helper(DataTable reldata)
        {
            if (reldata == null) return null;
            List<Relacao> rellista = new List<Relacao>();

            foreach (DataRow dr in reldata.Rows)
            {
                Relacao _rel = Relacao.ObterNovaRelacao();

                _rel.RelacaoId = new System.Guid(dr["RelacaoId"].ToString());
                _rel.PaiId     = new System.Guid(dr["ModuloId"].ToString());
                _rel.FilhoId   = new System.Guid(dr["objId"].ToString());

                rellista.Add(_rel);
            }
            return rellista;
        }

        #endregion
    }
}
