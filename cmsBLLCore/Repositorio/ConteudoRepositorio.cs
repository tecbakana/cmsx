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
    public class ConteudoRepositorio : BaseRepositorio,IConteudoRepositorio
    {
        private IConteudoDAL dal;
        private dynamic propLocal;

        #region IConteudoRepositorio Members

        public void MakeConnection(dynamic prop)
        {
            dal = container.Resolve<IConteudoDAL>();
            string bc = prop.banco;
            int parm = prop.parms;
            dal.MakeConnection((ExpandoObject)prop);
            propLocal = prop;
        }

        public void CriaNovoConteudo(Conteudo conteudo)
        {
            dal.CriaConteudo(conteudo.AreaId, conteudo.ConteudoId, conteudo.Titulo, conteudo.Texto, conteudo.Autor);
        }

        public void EditaConteudo(Conteudo conteudo)
        {
            dal.EditaConteudo(conteudo.ConteudoId, conteudo.Titulo, conteudo.Texto, conteudo.Autor);
        }

        public void Edita()
        {

            Conteudo cnt = (Conteudo)propLocal.conteudo;
            using (CmsxDbContext dbLoc = new CmsxDbContext())
            {
                string cid = cnt.ConteudoId.ToString();
                ///limpando as imagens previas
                Conteudo ct = (from c in dbLoc.Conteudos
                                where c.Conteudoid == cid
                                select new Conteudo()
                                {
                                    CategoriaId = new Guid(c.Cateriaid),
                                    Titulo = c.Titulo,
                                    Texto = c.Texto,
                                    Autor = c.Autor
                                }).FirstOrDefault();

                if (ct != null)
                {
                    ct.CategoriaId = cnt.CategoriaId;
                    ct.Titulo = cnt.Titulo;
                    ct.Texto = cnt.Texto;
                    ct.Autor = cnt.Autor;

                    dbLoc.Entry(ct).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbLoc.SaveChanges();

                    var img = (from i in dbLoc.Imagems
                                  where i.Imagemid == cnt.UrlImg
                                  select new Imagem()
                                  {
                                      ImagemId = new Guid(i.Imagemid),
                                      ParentId = new Guid(i.Parentid)
                                  }).FirstOrDefault();

                    if (img != null)
                    {
                        Relimagemconteudo ric = new Relimagemconteudo()
                        {
                            Imagemid = img.ImagemId.ToString(),
                            Parentid = ct.ConteudoId.ToString()
                        };

                        dbLoc.Relimagemconteudos.Add(ric);
                        dbLoc.SaveChanges();
                    }
                }

            }
        }

        public void CreateContent()
        {
            Conteudo cnt = (Conteudo)propLocal.conteudo;
            using (CmsxDbContext dbLoc = new CmsxDbContext())
            {
                string cid = cnt.ConteudoId.ToString();

                var ct = new CMSXData.Models.Conteudo()
                {
                    Cateriaid = cnt.CategoriaId.ToString(),
                    Areaid = cnt.AreaId.ToString(),
                    Conteudoid = cnt.ConteudoId.ToString(),
                    Titulo = cnt.Titulo,
                    Texto = cnt.Texto,
                    Autor = cnt.Autor,
                    Datainclusao = DateTime.Now
                };

                dbLoc.Conteudos.Add(ct);
                dbLoc.SaveChanges();

                var img = (from i in dbLoc.Imagems
                              where i.Imagemid == cnt.UrlImg
                              select new Imagem()
                              {
                                  ImagemId = new Guid(i.Imagemid),
                                  ParentId = new Guid(i.Parentid)
                              }).FirstOrDefault();

                if (img != null)
                {
                    Relimagemconteudo ric = new Relimagemconteudo()
                    {
                        Imagemid = img.ImagemId.ToString(),
                        Parentid = cnt.ConteudoId.ToString()
                    };

                    dbLoc.Relimagemconteudos.Add(ric);
                    dbLoc.SaveChanges();
                }
            }
        }

        public void EditContent()
        {
            dal.EditContent();
        }

        public void CreateValue()
        {
            dal.CriaValor();
        }

        public void EditValue()
        {
            dal.EditaValor();
        }

        public List<Conteudo> ListaConteudoPorAreaId()
        {

            return Helper(dal.ListaConteudoPorAreaId());
        }

        public List<Conteudo> ObtemConteudoPorId()
        {
            List<Conteudo> lcont = new List<Conteudo>();
            string contid = propLocal.conteudoId.ToString();
            using (CmsxDbContext dbLoc = new CmsxDbContext())
            {
                var clst = from c in dbLoc.Conteudos
                           join i in dbLoc.Imagems
                              on c.Conteudoid equals i.Conteudoid into imgGroup
                           from im in imgGroup.DefaultIfEmpty()
                           where c.Conteudoid == contid
                           select new
                           {
                               ConteudoId = c.Conteudoid,
                               AreaId = c.Areaid,
                               CategoriaId = c.Cateriaid,
                               Titulo = c.Titulo,
                               Texto = c.Texto,
                               UrlImg = im.Url
                           };
                foreach (var item in clst)
                {
                    lcont.Add(new Conteudo()
                    {
                        ConteudoId = new System.Guid(item.ConteudoId),
                        AreaId = (string.IsNullOrEmpty(item.AreaId) ? Guid.Empty : new System.Guid(item.AreaId)),
                        CategoriaId = (string.IsNullOrEmpty(item.CategoriaId) ? Guid.Empty : new System.Guid(item.CategoriaId)),
                        Titulo = item.Titulo,
                        Texto = item.Texto,
                        UrlImg = item.UrlImg
                    });
                }
            }
            return lcont;
        }

        public List<Conteudo> Helper(System.Data.DataTable appdata)
        {
            if (appdata == null) return null;

            List<Conteudo> applista = new List<Conteudo>();
            foreach (DataRow dr in appdata.Rows)
            {
                Conteudo _app = Conteudo.ObtemNovoConteudo();
                _app.AreaId = (!string.IsNullOrEmpty(dr["AreaId"].ToString())?new System.Guid(dr["AreaId"].ToString()):Guid.Empty);
                _app.CategoriaId = (!string.IsNullOrEmpty(dr["CategoriaId"].ToString()) ? new System.Guid(dr["CategoriaId"].ToString()) : Guid.Empty);
                _app.ConteudoId = new System.Guid(dr["ConteudoId"].ToString());
                _app.Texto = dr["Texto"].ToString();
                _app.Titulo = dr["Titulo"].ToString();
                _app.Autor = dr["Autor"].ToString();
                _app.UrlImg = dr["imagemnome"].ToString();

                if(dr.Table.Columns.Contains("UnidadeId"))
                {
                    _app.UnidadeId = dr["UnidadeId"].ToString();
                    _app.Valor     = dr["Valor"].ToString();
                }

                applista.Add(_app);
            }
            return applista;
        }

        public void InativaConteudo()
        {
            dal.InativaConteudo();
        }

        public List<Conteudo> ListaConteudoPorAplicacaoId()
        {
            string appId = propLocal.appId.ToString();
            List<Conteudo> lstcon = new List<Conteudo>();
            using (CmsxDbContext dbLoc = new CmsxDbContext())
            {
                var clst = from c in dbLoc.Conteudos
                           join i in dbLoc.Imagems
                              on c.Conteudoid equals i.Conteudoid into imgGroup
                           from im in imgGroup.DefaultIfEmpty()
                           where dbLoc.Areas.Any(a => a.Areaid == c.Areaid && a.Aplicacaoid == appId)
                             || dbLoc.Cateria.Any(t => t.Cateriaid == c.Cateriaid && t.Aplicacaoid == appId)
                           select new
                           {
                               ConteudoId = c.Conteudoid,
                               AreaId = c.Areaid,
                               CategoriaId = c.Cateriaid,
                               Titulo = c.Titulo,
                               Texto = c.Texto,
                               UrlImg = im.Url
                           };
                foreach (var item in clst)
                {
                    lstcon.Add(new Conteudo()
                    {
                        ConteudoId = new System.Guid(item.ConteudoId),
                        AreaId = (string.IsNullOrEmpty(item.AreaId)?Guid.Empty:new System.Guid(item.AreaId)),
                        CategoriaId = (string.IsNullOrEmpty(item.CategoriaId)?Guid.Empty:new System.Guid(item.CategoriaId)),
                        Titulo = item.Titulo,
                        Texto = item.Texto,
                        UrlImg = item.UrlImg
                    });
                }
            }

            /*GET IMAGE

            foreach (var item in lstcon)
            {
                var img = (from i in db.imagem
                           where i.ConteudoId == item.ConteudoId.ToString()
                           select i);
                string url = (img.Count() >= 1 ? img.FirstOrDefault().Url : null);

                item.UrlImg = url;
            }*/
            return lstcon;
        }

        public List<Conteudo> ListaConteudoPorCategoria()
        {
            List<Conteudo> lstcon = new List<Conteudo>();
            string appObj = lprop.appid;
            string appid = lprop.appid.ToString();
            var lstViewContent = from c in db.Cateria
                                 join cn in db.Conteudos
                                     on c.Cateriaid equals cn.Cateriaid into catGroup
                                 from cnc in catGroup.DefaultIfEmpty()
                                 where c.Aplicacaoid == appid
                                 select new Conteudo
                                 {
                                     AreaId = new Guid(cnc.Areaid),
                                     ConteudoId = new Guid(cnc.Conteudoid),
                                     CategoriaId = new Guid(cnc.Cateriaid),
                                     Titulo = cnc.Titulo,
                                     Texto= cnc.Texto
                                 };



            return lstcon.ToList();
        }

        public List<Conteudo> ListaValor()
        {
            return null;
        }

        #endregion
    }
}
