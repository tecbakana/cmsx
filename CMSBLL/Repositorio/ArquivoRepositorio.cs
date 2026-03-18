using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ICMS;
using System.Dynamic;

namespace CMSBLL.Repositorio
{
    public class ArquivoRepositorio : BaseRepositorio,IArquivoRepositorio
    {

        private IArquivoDAL dal;

        #region IArquivoRepositorio Members

        public void MakeConnection(dynamic prop)
        {
            dal = container.Resolve<IArquivoDAL>();
            string bc = prop.banco;
            int parm = prop.parms;
            dal.MakeConnection((ExpandoObject)prop);
        }

        public void CriaArquivo()
        {
            dal.CriaArquivo();
        }

        public Arquivo ObtemArquivoPorNome()
        {
            return Helper(dal.ObtemArquivoPorNome())[0];
        }

        public Arquivo ObtemArquivoPorId()
        {
            return Helper(dal.ObtemArquivoPorId())[0];
        }

        public List<Arquivo> ListaArquivoPorAreaId()
        {
            return Helper(dal.ListaArquivoPorAreaId());
        }

        public List<Arquivo> ListaArquivoPorConteudoId()
        {
            return Helper(dal.ListaArquivoPorConteudoId());
        }

        #endregion

        #region IArquivoRepositorio Members

        public void MakeConnection()
        {
            throw new NotImplementedException();
        }

        public List<Arquivo> Helper(DataTable arqdata)
        {
            if (arqdata == null) return null;
            List<Arquivo> arqlista = new List<Arquivo>();
            bool splash = arqdata.Columns.Contains("imagemnome");

            foreach (DataRow dr in arqdata.Rows)
            {
                Arquivo _arq = Arquivo.ObtemNovoArquivo();

                _arq.ArquivoId    = new System.Guid(dr["ArquivoId"].ToString());
                _arq.AreaId       = new System.Guid(dr["AreaId"].ToString());
                _arq.ConteudoId   = new System.Guid(dr["ConteudoId"].ToString());
                _arq.TipoId       = new System.Guid(dr["TipoId"].ToString());
                _arq.Nome         = dr["Nome"].ToString();
                
                arqlista.Add(_arq);
            }
            return arqlista;
        }

        #endregion
    }
}
