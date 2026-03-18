using System;
using System.Collections.Generic;
using System.Text;
using ICMSX;
using System.Dynamic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using CMSXData;
using CMSXData.Models;

namespace CMSXBLL.Repositorio
{
    public class ImagemRepositorio : BaseRepositorio,IImagemRepositorio
    {
        private IImagem dal;

        #region IImagemRepositorio Members

        public Imagem ObtemImagemPorId(Guid id)
        {
            return null;
        }

        public void MakeConnection(dynamic prop)
        {
            //dal = container.Resolve<>();
            db = new CmsxDbContext();
            string bc = prop.banco;
            int parm = prop.parms;
            lprop = prop;
            //dal.MakeConnection((ExpandoObject)prop);
        }

        public void CriaNovaImagem()
        {
            dal.CriaImagem();
        }

        public void AtualizaGaleria()
        {
            //dal.AtualizaGaleria();
            string imgId = lprop.imagemId.ToString();
            string arId = lprop.areaid.ToString();

            using (CmsxDbContext dbLoc = new CmsxDbContext())
            {
                ///limpando as imagens previas
                var imgold = (from im in dbLoc.Imagems
                                 where im.Areaid.ToString() == arId
                                 select im).FirstOrDefault();
                if (imgold != null)
                {
                    imgold.Areaid = arId;
                    dbLoc.Entry(imgold).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbLoc.SaveChanges();
                }


                ///vinculando nova imagem
                var img = (from im in dbLoc.Imagems
                              where im.Imagemid.ToString() == imgId
                              select im).First();
                img.Imagemid = imgId;
                img.Areaid = arId;
                dbLoc.Entry(img).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbLoc.SaveChanges();
            }
        }

        public List<Imagem> Galeria()
        {
            using (CmsxDbContext dbloc = new CmsxDbContext())
            {
                string pId = lprop.areaid.ToString();
                IEnumerable<Imagem> lst = from img in db.Imagems
                                          where img.Areaid == pId
                                          select new Imagem()
                                          {
                                              ImagemId = new Guid(img.Imagemid),
                                              AreaId = new Guid(img.Areaid),
                                              ConteudoId = new Guid(img.Conteudoid),
                                              Altura = img.Altura??0,
                                              Largura = img.Largura??0,
                                              Url = img.Url,
                                              Descricao = img.Descricao,
                                              ParentId = new Guid(img.Parentid),
                                              TipoId = new Guid(img.Tipoid)
                                          };
                return Helper(lst);
            }
        }

        public List<Imagem> GaleriaConteudo()
        {
            return Helper(dal.ObtemImagemPorConteudoId());
        }

        public List<Imagem> GaleriaParentId()
        {
            using(CmsxDbContext dbloc = new CmsxDbContext())
            {
                string pId = lprop.pId.ToString();
                IEnumerable<Imagem> lst = from img in db.Imagems
                                          where img.Parentid == pId
                                          select new Imagem()
                                          {
                                              ImagemId = new Guid(img.Imagemid),
                                              AreaId = new Guid(img.Areaid),
                                              ConteudoId = new Guid(img.Conteudoid),
                                              Altura = img.Altura ?? 0,
                                              Largura = img.Largura ?? 0,
                                              Url = img.Url,
                                              Descricao = img.Descricao,
                                              ParentId = new Guid(img.Parentid),
                                              TipoId = new Guid(img.Tipoid)
                                          };
                return Helper(lst);
            }
        }

        public List<Imagem> Helper(DataTable appdata)
        {
            if (appdata == null) return null;
            List<Imagem> applista = new List<Imagem>();
            foreach (DataRow dr in appdata.Rows)
            {
                Imagem _app = Imagem.ObterImagem();
                _app.Url = dr["Url"].ToString();
                _app.AreaId = string.IsNullOrEmpty(dr["AreaId"].ToString())?new System.Guid():new System.Guid(dr["AreaId"].ToString());
                _app.ImagemId = new System.Guid(dr["ImagemId"].ToString());
                _app.Altura = 0;// int.Parse(dr["Altura"].ToString());
                _app.Largura = 0;// int.Parse(dr["Largura"].ToString());
                _app.Descricao = dr["Descricao"].ToString();
                applista.Add(_app);
            }
            return applista;
        }

        public List<Imagem> Helper(IEnumerable<Imagem> appdata)
        {
            if (appdata == null) return null;
            List<Imagem> applista = new List<Imagem>();
            foreach (Imagem dr in appdata)
            {
                Imagem _app = Imagem.ObterImagem();
                _app.Url = dr.Url;
                _app.AreaId = dr.AreaId==null? new System.Guid() : new System.Guid(dr.AreaId.ToString());
                _app.ParentId = dr.ParentId==null ? new System.Guid() : new System.Guid(dr.ParentId.ToString());
                _app.ImagemId = new System.Guid(dr.ImagemId.ToString());
                _app.Altura = 0;// int.Parse(dr["Altura"].ToString());
                _app.Largura = 0;// int.Parse(dr["Largura"].ToString());
                _app.Descricao = dr.Descricao==null ? string.Empty : dr.Descricao.ToString();
                _app.TipoId = dr.TipoId == null ? new System.Guid() : new System.Guid(dr.TipoId.ToString());
                applista.Add(_app);
            }
            return applista;
        }

        public void InsereImagemGaleria()
        {
            using (CmsxDbContext dbloc = new CmsxDbContext())
            {
                var im = new CMSXData.Models.Imagem();
                Imagem obj = (Imagem)lprop.imgObj;
                im.Imagemid = obj.ImagemId.ToString();
                im.Parentid = obj.ParentId.ToString();
                im.Url = obj.Url;
                im.Tipoid = obj.TipoId.ToString();
                dbloc.Imagems.Add(im);
                dbloc.SaveChanges();
            }
        }

        #endregion
    }
}
