using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ICMS;
using System.Dynamic;
using CMSXEF;

namespace CMSBLL.Repositorio
{
    public class AreasRepositorio : BaseRepositorio, IAreasRepositorio
    {
        private IAreasDAL dal;

        #region IAplicacaoRepositorio Members

        public void MakeConnection(dynamic prop)
        {
            dal = container.Resolve<IAreasDAL>();
            string bc = prop.banco;
            int parm = prop.parms;
            lprop = prop;
            dal.MakeConnection((ExpandoObject)prop);
        }

        public Areas ObtemAreaPorId()
        {
            return Helper(dal.ListaAreasPorId())[0];
        }

        public List<Areas> ListaAreas()
        {
            var areas = Helper(dal.ListaAreas());
            return areas;
        }

        public List<Areas> ListaAreasPorTipo()
        {
            var areas = Helper(dal.ListaAreasPorTipo());
            return areas;
        }

        public List<Areas> ListaAreasFilha()
        {
            var areas = Helper(dal.ListaAreasFilha());
            return areas;
        }

        public List<Areas> ListaAreaMenu()
        {
            var areas = Helper(dal.ListaAreaMenu());
            return areas;
        }

        public List<Areas> ListaAreaMenuLateral()
        {
            var areas = Helper(dal.ListaAreaMenuLateral());
            return areas;
        }

        public List<Areas> ListaAreaMenuSplash()
        {
            var areas = Helper(dal.ListaAreaMenuSplash());
            return areas;
        }

        public List<Areas> ListaAreasPai()
        {
            var areas = Helper(dal.ListaAreasPai());
            return areas;
        }

        public List<Areas> Helper(DataTable appdata)
        {
            if (appdata == null) return null;
            List<Areas> applista = new List<Areas>();

            foreach (DataRow dr in appdata.Rows)
            {
                Areas _app = Areas.ObterNovaArea();
                _app.Nome = dr["Nome"].ToString();
                _app.AplicacaoId = new System.Guid(dr["AplicacaoId"].ToString());
                _app.AreaId = new System.Guid(dr["AreaId"].ToString());

                if (dr.Table.Columns.Contains("AreaIdPai"))
                {
                    _app.AreaIdPai = string.IsNullOrWhiteSpace(dr["AreaIdPai"].ToString())?new Guid():new System.Guid(dr["AreaIdPai"].ToString());
                }

                if (dr.Table.Columns.Contains("Descricao"))
                {
                    _app.Descricao  = dr["Descricao"].ToString();
                }

                if (dr.Table.Columns.Contains("Url"))
                {
                    _app.Url = dr["Url"].ToString();
                }

                if (dr.Table.Columns.Contains("NomePai"))
                {
                    _app.NomePai = (string.IsNullOrEmpty(dr["NomePai"].ToString()) ? "" : dr["NomePai"].ToString());
                }

                if (dr.Table.Columns.Contains("Imagem"))
                {
                    _app.Imagem = dr["Imagem"].ToString()=="1"?true:false;
                }

                if (dr.Table.Columns.Contains("TipoMenu"))
                {
                    _app.TipoMenu = dr["TipoMenu"].ToString();
                }

                if (dr.Table.Columns.Contains("Posicao"))
                {
                    _app.Posicao = string.IsNullOrEmpty(dr["Posicao"].ToString())? 0:int.Parse(dr["Posicao"].ToString());
                }

                if (dr.Table.Columns.Contains("imagemnome"))
                {
                    _app.UrlImagem = dr["imagemnome"].ToString();
                }

                if (dr.Table.Columns.Contains("idTipoArea"))
                {
                    _app.IdTipoArea   = int.Parse(dr["idTipoArea"].ToString());
                    _app.NomeTipoArea = dr["nomeTipoArea"].ToString();
                    _app.TipoArea     = int.Parse(dr["tipoArea"].ToString());
                }

                applista.Add(_app);
            }
            return applista;
        }

        public void CriaNovaArea()
        {
            dal.CriaArea();
        }

        public void EditaArea()
        {
            dal.EditaArea();
        }

        public void EditaAreaPosicao()
        {
            dal.EditaAreaPosicao();
        }

        public void InativaArea()
        {
            dal.InativaArea();
        }

        public string AreaRapida()
        {
            var areaObj = lprop.area;
            using (cmsxDBEntities dbLoc = new cmsxDBEntities())
            {
                areas narea = new areas();

                string aId = areaObj.AreaId.ToString();
                string apid = areaObj.AplicacaoId.ToString();

                narea.Nome = areaObj.Nome;
                narea.Descricao = areaObj.Descricao;
                narea.AreaId = aId;
                narea.AplicacaoId = apid;
                narea.Url = areaObj.Url;
                narea.MenuCentral = 1;
                narea.posicao = areaObj.Posicao;
                narea.DataInicial = DateTime.Now;
                narea.TipoArea = areaObj.TipoArea;
                narea.Imagem = 0;

                dbLoc.areas.Add(narea);
                dbLoc.SaveChanges();
            }

                return string.Empty;
        }
        #endregion
    }
}
