using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ICMS;
using System.Dynamic;
using System.Linq;
using CMSXEF;
using System.Reflection;

namespace CMSBLL.Repositorio
{
    public class AplicacaoRepositorio : BaseRepositorio,IAplicacaoRepositorio
    {


        private IAplicacaoDAL dal;
        private dynamic propLocal;

        #region IAplicacaoRepositorio Members

        public void MakeConnection(dynamic prop)
        {
            dal = container.Resolve<IAplicacaoDAL>();
            string bc = prop.banco;
            int parm = prop.parms;
            dal.MakeConnection((ExpandoObject)prop);

            propLocal = prop;
        }

        public Aplicacao ObtemAplicacaoPorId(Guid id)
        {
            Aplicacao app = new Aplicacao();

            string appId = propLocal.appId.ToString();
            using (cmsxDBEntities dbLoc = new cmsxDBEntities())
            {
                var applst = (from a in dbLoc.aplicacao
                              where a.AplicacaoId == appId
                              select new
                              {
                                  AplicacaoId = a.AplicacaoId,
                                  Nome = a.Nome,
                                  Url = a.Url,
                                  DataInicio = a.DataInicio,
                                  DataFinal = a.DataFinal,
                                  IdUsuarioInicio = a.IdUsuarioInicio,
                                  IdUsuarioFim = a.IdUsuarioFim,
                                  PagSeguroToken = a.PagSeguroToken,
                                  LayoutChoose = a.LayoutChoose,
                                  Posicao = a.Posicao,
                                  mailUser = a.mailUser,
                                  mailPassword = a.mailPassword,
                                  mailServer = a.mailServer,
                                  mailPort = a.mailPort,
                                  isSecure = a.isSecure,
                                  pageFacebook = a.pageFacebook,
                                  pageInstagram = a.pageInstagram,
                                  pageLinkedin = a.pageLinkedin,
                                  pageFlicker = a.pageFlicker,
                                  pagePinterest = a.pagePinterest,
                                  pageTwitter = a.pageTwitter,
                                  googleAdSense = a.googleAdSense,
                                  header = a.header
                              });

                if (applst.Count() >= 1)
                {
                    foreach (var a in applst)
                    {
                        app = (new Aplicacao()
                        {
                            AplicacaoId = new System.Guid(a.AplicacaoId),
                            Nome = a.Nome,
                            Url = a.Url,
                            DataFinal = a.DataFinal.ToString(),
                            PagSeguroToken = a.PagSeguroToken,
                            Layout = a.LayoutChoose,
                            mailUser = a.mailUser,
                            pageFacebook = a.pageFacebook,
                            pageInstagram = a.pageInstagram,
                            pageLinkedin = a.pageLinkedin,
                            pageFlicker = a.pageFlicker,
                            pagePinterest = a.pagePinterest,
                            pageTwitter = a.pageTwitter,
                            googleAdSense = a.googleAdSense
                        });
                    }
                }
            }


            return app;
        }

        public Aplicacao RegistraAplicacao()
        {
            Guid aplicacaoid = dal.ObtemIdAplicacaoPorNome();
            dynamic props = new ExpandoObject();
            props.parms = 1;
            props.appId = aplicacaoid;
            props.banco = propLocal.banco;
            MakeConnection(props);
            return ObtemAplicacaoPorId(aplicacaoid);
        }

        public List<Aplicacao> ListaAplicacao()
        {
            List<Aplicacao> aplicacoes = new List<Aplicacao>();// Helper(dal.ListaAplicacao());
            string user = propLocal.admin;
            using (cmsxDBEntities dbLoc = new cmsxDBEntities())
            {
                var applst = from a in dbLoc.aplicacao
                             where a.IdUsuarioInicio == user
                             select new
                             {
                                 AplicacaoId = a.AplicacaoId,
                                 Nome = a.Nome,
                                 Url = a.Url,
                                 DataInicio = a.DataInicio,
                                 DataFinal = a.DataFinal,
                                 IdUsuarioInicio = a.IdUsuarioInicio,
                                 IdUsuarioFim = a.IdUsuarioFim,
                                 PagSeguroToken = a.PagSeguroToken,
                                 LayoutChoose = a.LayoutChoose,
                                 Posicao = a.Posicao,
                                 mailUser = a.mailUser,
                                 mailPassword = a.mailPassword,
                                 mailServer = a.mailServer,
                                 mailPort = a.mailPort,
                                 isSecure = a.isSecure,
                                 header = a.header
                             };
                if (applst.Count() >= 1)
                {
                    foreach(var a in applst)
                    {
                        aplicacoes.Add(new Aplicacao()
                        {
                            AplicacaoId = new System.Guid(a.AplicacaoId),
                            Nome = a.Nome,
                            Url = a.Url,
                            DataFinal = a.DataFinal.ToString(),
                            PagSeguroToken = a.PagSeguroToken,
                            mailUser = a.mailUser,
                            header = a.header
                        });
                    }
                }

                foreach (var app in aplicacoes)
                {

                    string appid = app.AplicacaoId.ToString();
                    var img = (from i in dbLoc.imagem
                               where i.ParentId == appid &&
                               i.TipoId == "24a57e31-4ffe-11e1-8664-07b98c902e34"
                               select i);

                    app.ImagemUrl = (img.Count()>=1?img.FirstOrDefault().Url:null);
                }

            }

            return aplicacoes;
        }

        public List<Aplicacao> Helper(DataTable appdata)
        {
            if (appdata == null) return null;
            List<Aplicacao> applista = new List<Aplicacao>();
            foreach (DataRow dr in appdata.Rows)
            {
                Aplicacao _app = Aplicacao.ObterAplicacao();
                _app.Nome = dr["Nome"].ToString();
                _app.AplicacaoId = new System.Guid(dr["AplicacaoId"].ToString());
                _app.Url = dr["Url"].ToString();
                _app.DataFinal = dr["DataFinal"].ToString();
                if (dr.Table.Columns.Contains("mailUser"))
                {
                    _app.mailUser = dr["mailUser"].ToString();
                    _app.PagSeguroToken = dr["PagSeguroToken"].ToString();
                }
                else
                {
                    _app.mailUser = string.Empty;
                    _app.PagSeguroToken = string.Empty;
                }
                applista.Add(_app);
            }
            return applista;
        }

        public bool CriaAplicacao()
        {
            //dal.CriaAplicacao();
            bool retval = false;
            try
            {
                using (cmsxDBEntities dbLoc = new cmsxDBEntities())
                {
                    aplicacao ap = new aplicacao();
                    Aplicacao app = (Aplicacao)propLocal.aplicacao;
                    ap.AplicacaoId = app.AplicacaoId.ToString();
                    ap.Nome = app.Nome;
                    ap.Url = app.Url;
                    ap.AplicacaoId = app.AplicacaoId.ToString();
                    ap.IdUsuarioInicio = app.IdUsuarioInicio.ToString();
                    ap.LayoutChoose = app.LayoutChoose.ToString();
                    ap.mailUser = app.mailUser;
                    ap.PagSeguroToken = app.PagSeguroToken.ToString();
                    ap.pageFacebook = app.pageFacebook;
                    ap.pageInstagram = app.pageInstagram;
                    ap.pageLinkedin = app.pageLinkedin;
                    ap.pageFlicker = app.pageFlicker;
                    ap.pagePinterest = app.pagePinterest;
                    ap.pageTwitter = app.pageTwitter;
                    ap.googleAdSense = app.googleAdSense;

                    dbLoc.aplicacao.Add(ap);
                    dbLoc.SaveChanges();
                }
                retval = true;
            }
            catch(Exception ex)
            {
                retval = false;
            }
            return retval;
        }

        public void InativaAplicacao()
        {
            dal.InativaAplicacao();
        }

        public void Edita()
        {
            //dal.EditaAplicacao();
            Aplicacao app = (Aplicacao)propLocal.aplicacao;
            using (cmsxDBEntities dbLoc = new cmsxDBEntities())
            {
                string apid = app.AplicacaoId.ToString();
                ///limpando as imagens previas
                aplicacao ap = (from a in dbLoc.aplicacao
                                where a.AplicacaoId == apid
                                select a).FirstOrDefault();
                if (ap != null)
                {
                    ap.Nome = app.Nome;
                    ap.Url = app.Url;
                    ap.AplicacaoId = apid;
                    ap.IdUsuarioInicio = app.IdUsuarioInicio.ToString();
                    ap.LayoutChoose = app.LayoutChoose.ToString();
                    ap.mailUser = app.mailUser;
                    ap.pageFacebook = app.pageFacebook;
                    ap.pageInstagram = app.pageInstagram;
                    ap.pageLinkedin = app.pageLinkedin;
                    ap.pageFlicker = app.pageFlicker;
                    ap.pagePinterest = app.pagePinterest;
                    ap.pageTwitter = app.pageTwitter;
                    ap.googleAdSense = app.googleAdSense;

                    ap.PagSeguroToken = app.PagSeguroToken.ToString();
                    dbLoc.Entry(ap).State = System.Data.Entity.EntityState.Modified;
                    dbLoc.SaveChanges();
                }
            }
        }

        public List<Aplicacao> Helper(IEnumerable<CMSXEF.aplicacao> appdata)
        {
            throw new NotImplementedException();
        }

        public string[] ListaAplicacaoPorNome()
        {
            var aplicacoes = Helper(dal.ListaAplicacaoPorNome());
            string[] nomes = new string[aplicacoes.Count];
            for(int i=0;i<=aplicacoes.Count-1;i++)
            {
                nomes[i] = aplicacoes[i].Url;
            }
            return nomes;
        }

        public List<Aplicacao> ListaApp()
        {
            return Helper(dal.ListaAplicacaoPorNome());
        }

        public List<Aplicacao> ListaAplicacaoForAutocomplete()
        {
            return Helper(dal.ListaAplicacaoForAutocomplete());
        }
        #endregion



        public string AtivaAplicacao()
        {
            string url = string.Empty;

            Aplicacao app = (Aplicacao)propLocal.aplicacao;
            using (cmsxDBEntities dbLoc = new cmsxDBEntities())
            {
                string apid = app.AplicacaoId.ToString();
                ///limpando as imagens previas
                aplicacao ap = (from a in dbLoc.aplicacao
                                where a.AplicacaoId == apid
                                select a).FirstOrDefault();
                if (ap != null)
                {
                    ap.isActive = true;
                    dbLoc.Entry(ap).State = System.Data.Entity.EntityState.Modified;
                    dbLoc.SaveChanges();
                    url = ap.Url;
                }
            }
            return url;
        }

        public void ExcluiAplicacao()
        {
            string url = string.Empty;

            Aplicacao app = (Aplicacao)propLocal.aplicacao;
            using (cmsxDBEntities dbLoc = new cmsxDBEntities())
            {
                string apid = app.AplicacaoId.ToString();
                ///limpando as imagens previas
                aplicacao ap = (from a in dbLoc.aplicacao
                                where a.AplicacaoId == apid
                                select a).FirstOrDefault();
                if (ap != null)
                {
                    dbLoc.Entry(ap).State = System.Data.Entity.EntityState.Deleted;
                    dbLoc.SaveChanges();
                    url = ap.Url;
                }
            }
        }

    }
}
