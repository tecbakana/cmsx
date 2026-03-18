using System;
using System.Collections.Generic;
using System.Text;
using ICMS;
using System.Dynamic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using CMSXEF;

namespace CMSBLL.Repositorio
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
            db = new cmsxDBEntities();
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

            using (cmsxDBEntities dbLoc = new cmsxDBEntities())
            {
                ///limpando as imagens previas
                imagem imgold = (from im in dbLoc.imagem
                                 where im.AreaId == arId
                                 select im).FirstOrDefault();
                if (imgold != null)
                {
                    imgold.AreaId = null;
                    dbLoc.Entry(imgold).State = System.Data.Entity.EntityState.Modified;
                    dbLoc.SaveChanges();
                }


                ///vinculando nova imagem
                imagem img = (from im in dbLoc.imagem
                              where im.ImagemId == imgId
                              select im).First();
                img.ImagemId = imgId;
                img.AreaId = arId;
                dbLoc.Entry(img).State = System.Data.Entity.EntityState.Modified;
                dbLoc.SaveChanges();
            }
        }

        public List<Imagem> Galeria()
        {
            using (cmsxDBEntities dbloc = new cmsxDBEntities())
            {
                string pId = lprop.areaid.ToString();
                IEnumerable<imagem> lst = from img in db.imagem
                                          where img.AreaId == pId
                                          select img;
                return Helper(lst);
            }
        }

        public List<Imagem> GaleriaConteudo()
        {
            return Helper(dal.ObtemImagemPorConteudoId());
        }

        public List<Imagem> GaleriaParentId()
        {
            using(cmsxDBEntities dbloc = new cmsxDBEntities())
            {
                string pId = lprop.pId.ToString();
                IEnumerable<imagem> lst = from img in db.imagem
                                           where img.ParentId == pId
                                           select img;
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

        public List<Imagem> Helper(IEnumerable<imagem> appdata)
        {
            if (appdata == null) return null;
            List<Imagem> applista = new List<Imagem>();
            foreach (imagem dr in appdata)
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
            using (cmsxDBEntities dbloc = new cmsxDBEntities())
            {
                imagem im = new imagem();
                Imagem obj = (Imagem)lprop.imgObj;
                im.ImagemId = obj.ImagemId.ToString();
                im.ParentId = obj.ParentId.ToString();
                im.Url = obj.Url;
                im.TipoId = obj.TipoId.ToString();
                dbloc.imagem.Add(im);
                dbloc.SaveChanges();
            }
        }

        #endregion
    }
}
