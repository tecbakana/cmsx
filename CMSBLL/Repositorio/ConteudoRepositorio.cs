using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ICMS;
using System.Linq;
using CMSXEF;
using System.Dynamic;


namespace CMSBLL.Repositorio
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
            using (cmsxDBEntities dbLoc = new cmsxDBEntities())
            {
                string cid = cnt.ConteudoId.ToString();
                ///limpando as imagens previas
                conteudo ct = (from c in dbLoc.conteudo
                                where c.ConteudoId == cid
                                select c).FirstOrDefault();

                if (ct != null)
                {
                    ct.CategoriaId = cnt.CategoriaId.ToString();
                    ct.Titulo = cnt.Titulo;
                    ct.Texto = cnt.Texto;
                    ct.Autor = cnt.Autor;

                    dbLoc.Entry(ct).State = System.Data.Entity.EntityState.Modified;
                    dbLoc.SaveChanges();

                    imagem img = (from i in dbLoc.imagem
                                  where i.ImagemId == cnt.UrlImg
                                  select i).FirstOrDefault();

                    if (img != null)
                    {
                        relimagemconteudo ric = new relimagemconteudo()
                        {
                            imagemid = img.ImagemId,
                            parentid = ct.ConteudoId
                        };

                        dbLoc.relimagemconteudo.Add(ric);
                        dbLoc.SaveChanges();
                    }
                }

            }
        }

        public void CreateContent()
        {
            Conteudo cnt = (Conteudo)propLocal.conteudo;
            using (cmsxDBEntities dbLoc = new cmsxDBEntities())
            {
                string cid = cnt.ConteudoId.ToString();

                conteudo ct = new conteudo()
                {
                    CategoriaId = cnt.CategoriaId.ToString(),
                    AreaId = cnt.AreaId.ToString(),
                    ConteudoId = cnt.ConteudoId.ToString(),
                    Titulo = cnt.Titulo,
                    Texto = cnt.Texto,
                    Autor = cnt.Autor,
                    DataInclusao = DateTime.Now
                };

                dbLoc.conteudo.Add(ct);
                dbLoc.SaveChanges();

                imagem img = (from i in dbLoc.imagem
                              where i.ImagemId == cnt.UrlImg
                              select i).FirstOrDefault();

                if (img != null)
                {
                    relimagemconteudo ric = new relimagemconteudo()
                    {
                        imagemid = img.ImagemId,
                        parentid = cnt.ConteudoId.ToString()
                    };

                    dbLoc.relimagemconteudo.Add(ric);
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
            using (cmsxDBEntities dbLoc = new cmsxDBEntities())
            {
                var clst = from c in dbLoc.conteudo
                           join i in dbLoc.imagem
                              on c.ConteudoId equals i.ConteudoId into imgGroup
                           from im in imgGroup.DefaultIfEmpty()
                           where c.ConteudoId == contid
                           select new
                           {
                               ConteudoId = c.ConteudoId,
                               AreaId = c.AreaId,
                               CategoriaId = c.CategoriaId,
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
            using (cmsxDBEntities dbLoc = new cmsxDBEntities())
            {
                var clst = from c in dbLoc.conteudo
                           join i in dbLoc.imagem
                              on c.ConteudoId equals i.ConteudoId into imgGroup
                           from im in imgGroup.DefaultIfEmpty()
                           where dbLoc.areas.Any(a => a.AreaId == c.AreaId && a.AplicacaoId == appId)
                             || dbLoc.categoria.Any(t => t.CategoriaId == c.CategoriaId && t.AplicacaoId == appId)
                           select new
                           {
                               ConteudoId = c.ConteudoId,
                               AreaId = c.AreaId,
                               CategoriaId = c.CategoriaId,
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
            var lstViewContent = from c in db.categoria
                                 join cn in db.conteudo
                                     on c.CategoriaId equals cn.CategoriaId into catGroup
                                 from cnc in catGroup.DefaultIfEmpty()
                                 where c.AplicacaoId == appid
                                 select new Conteudo
                                 {
                                     AreaId = new Guid(cnc.AreaId),
                                     ConteudoId = new Guid(cnc.ConteudoId),
                                     CategoriaId = new Guid(cnc.CategoriaId),
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
