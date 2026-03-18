using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ICMSX;
using System.Dynamic;
using System.Linq;
using CMSXData;
using CMSXData.Models;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace CMSXBLL.Repositorio
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
            using (CmsxDbContext dbLoc = new CmsxDbContext())
            {
                var applst = (from a in dbLoc.Aplicacaos
                              where a.Aplicacaoid.ToString() == appId
                              select new
                              {
                                  AplicacaoId = a.Aplicacaoid,
                                  Nome = a.Nome,
                                  Url = a.Url,
                                  DataInicio = a.Datainicio,
                                  DataFinal = a.Datafinal,
                                  IdUsuarioInicio = a.Idusuarioinicio,
                                  IdUsuarioFim = a.Idusuariofim,
                                  PagSeguroToken = a.Pagsegurotoken,
                                  LayoutChoose = a.Layoutchoose,
                                  Posicao = a.Posicao,
                                  mailUser = a.Mailuser,
                                  mailPassword = a.Mailpassword,
                                  mailServer = a.Mailserver,
                                  mailPort = a.Mailport,
                                  isSecure = a.Issecure,
                                  pageFacebook = a.Pagefacebook,
                                  pageInstagram = a.Pageinstagram,
                                  pageLinkedin = a.Pagelinkedin,
                                  pageFlicker = a.Pageflicker,
                                  pagePinterest = a.Pagepinterest,
                                  pageTwitter = a.Pagetwitter,
                                  googleAdSense = a.Ogleadsense,
                                  header = a.Header
                              });

                if (applst.Count() >= 1)
                {
                    foreach (var a in applst)
                    {
                        app = (new Aplicacao()
                        {
                            AplicacaoId = new System.Guid(a.AplicacaoId.ToString()),
                            Nome = a.Nome,
                            Url = a.Url,
                            DataFinal = a.DataFinal.ToString(),
                            PagSeguroToken = a.PagSeguroToken,
                            Layout = a.LayoutChoose,
                            Mailuser = a.mailUser,
                            Pagefacebook = a.pageFacebook,
                            Pageinstagram = a.pageInstagram,
                            Pagelinkedin = a.pageLinkedin,
                            Pageflicker = a.pageFlicker,
                            Pagepinterest = a.pagePinterest,
                            Pagetwitter = a.pageTwitter,
                            Ogleadsense = a.googleAdSense
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
            using (CmsxDbContext dbLoc = new CmsxDbContext())
            {
                var applst = from a in dbLoc.Aplicacaos
                             where a.Idusuarioinicio == user
                             select new
                             {
                                 AplicacaoId = a.Aplicacaoid,
                                 Nome = a.Nome,
                                 Url = a.Url,
                                 DataInicio = a.Datainicio,
                                 DataFinal = a.Datafinal,
                                 IdUsuarioInicio = a.Idusuarioinicio,
                                 IdUsuarioFim = a.Idusuariofim,
                                 PagSeguroToken = a.Pagsegurotoken,
                                 LayoutChoose = a.Layoutchoose,
                                 Posicao = a.Posicao,
                                 mailUser = a.Mailuser,
                                 mailPassword = a.Mailpassword,
                                 mailServer = a.Mailserver,
                                 mailPort = a.Mailport,
                                 isSecure = a.Issecure,
                                 header = a.Header
                             };
                if (applst.Count() >= 1)
                {
                    foreach(var a in applst)
                    {
                        aplicacoes.Add(new Aplicacao()
                        {
                            AplicacaoId = a.AplicacaoId,
                            Nome = a.Nome,
                            Url = a.Url,
                            DataFinal = a.DataFinal.ToString() ?? new DateTime().ToString(),
                            PagSeguroToken = a.PagSeguroToken,
                            Mailuser = a.mailUser,
                            header = a.header
                        });
                    }
                }

                foreach (var app in aplicacoes)
                {

                    string appid = app.AplicacaoId.ToString();
                    var img = (from i in dbLoc.Imagems
                               where i.Parentid == appid &&
                               i.Tipoid == "24a57e31-4ffe-11e1-8664-07b98c902e34"
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
                    _app.Mailuser = dr["mailUser"].ToString();
                    _app.PagSeguroToken = dr["PagSeguroToken"].ToString();
                }
                else
                {
                    _app.Mailuser = string.Empty;
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
                using (CmsxDbContext dbLoc = new CmsxDbContext())
                {
                    Aplicacao ap = new Aplicacao();
                    Aplicacao app = (Aplicacao)propLocal.aplicacao;
                    ap.AplicacaoId = app.AplicacaoId;
                    ap.Nome = app.Nome;
                    ap.Url = app.Url;
                    ap.AplicacaoId = app.AplicacaoId;
                    ap.Idusuarioinicio = app.Idusuarioinicio;
                    ap.Layoutchoose = app.Layoutchoose;
                    ap.Mailuser = app.Mailuser;
                    ap.PagSeguroToken = app.PagSeguroToken.ToString();
                    ap.Pagefacebook = app.Pagefacebook;
                    ap.Pageinstagram = app.Pageinstagram;
                    ap.Pagelinkedin = app.Pagelinkedin;
                    ap.Pageflicker = app.Pageflicker;
                    ap.Pagepinterest = app.Pagepinterest;
                    ap.Pagetwitter = app.Pagetwitter;
                    ap.Ogleadsense = app.Ogleadsense;

                    dbLoc.Aplicacaos.Add(ap);
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
            using (CmsxDbContext dbLoc = new CmsxDbContext())
            {
                Guid apid = app.AplicacaoId;
                ///limpando as imagens previas
                var ap = (from a in dbLoc.Aplicacaos
                                where a.Aplicacaoid == apid
                                select a).FirstOrDefault();
                if (ap != null)
                {
                    ap.Nome = app.Nome;
                    ap.Url = app.Url;
                    ap.Aplicacaoid = app.Aplicacaoid;/*
                    ap.IdUsuarioInicio = app.IdUsuarioInicio.ToString();
                    ap.LayoutChoose = app.LayoutChoose.ToString();
                    ap.mailUser = app.mailUser;
                    ap.pageFacebook = app.pageFacebook;
                    ap.pageInstagram = app.pageInstagram;
                    ap.pageLinkedin = app.pageLinkedin;
                    ap.pageFlicker = app.pageFlicker;
                    ap.pagePinterest = app.pagePinterest;
                    ap.pageTwitter = app.pageTwitter;
                    ap.googleAdSense = app.googleAdSense;*/

                    ap.Pagsegurotoken = app.PagSeguroToken.ToString();
                    dbLoc.Entry(ap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbLoc.SaveChanges();
                }
            }
        }

        public List<Aplicacao> Helper(IEnumerable<Aplicacao> appdata)
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
            using (CmsxDbContext dbLoc = new CmsxDbContext())
            {
                Guid apid = app.AplicacaoId;
                ///limpando as imagens previas
                var ap = (from a in dbLoc.Aplicacaos
                                where a.Aplicacaoid == apid
                                select a).FirstOrDefault();
                if (ap != null)
                {
                    ap.Isactive = true;
                    dbLoc.Entry(ap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
            using (CmsxDbContext dbLoc = new CmsxDbContext())
            {
                Guid apid = app.AplicacaoId;
                ///limpando as imagens previas
                var ap = (from a in dbLoc.Aplicacaos
                                where a.Aplicacaoid == apid
                                select a).FirstOrDefault();
                if (ap != null)
                {
                    dbLoc.Entry(ap).State = EntityState.Modified;
                    dbLoc.SaveChanges();
                    url = ap.Url;
                }
            }
        }

    }
}
