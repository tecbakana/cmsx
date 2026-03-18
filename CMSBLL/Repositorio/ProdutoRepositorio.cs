using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ICMS;
using CMSXEF;
using System.Dynamic;

namespace CMSBLL.Repositorio
{
    public class ProdutoRepositorio : BaseRepositorio,IProdutoRepositorio
    {
        private IProdutoDAL dal;

        #region IProdutoRepositorio Members

        public void MakeConnection(dynamic prop)
        {
            dal = container.Resolve<IProdutoDAL>();
            db = new cmsxDBEntities();
            string bc = prop.banco;
            int parm = prop.parms;
            lprop = prop;
            dal.MakeConnection((ExpandoObject)prop);
        }

        public List<Produto> ListaProduto()
        {
            
            string appid = lprop.appid.ToString();
            IEnumerable<produto> lst = from prod in db.produto
                                       where prod.AplicacaoId == appid
                                       select prod;
            return Helper(lst);
        }

        public void EditaProduto(Produto prod)
        {
            throw new NotImplementedException();
        }

        public void CriaProduto(Produto prod)
        {
            using (cmsxDBEntities db = new cmsxDBEntities())
            {
                produto p = new produto();
                p.AplicacaoId = prod.AplicacaoId.ToString();
                p.CategoriaId = prod.CategoriaId.ToString();
                p.ProdutoId = prod.ProdutoId.ToString();
                p.Nome = prod.Nome;
                p.Descricao = prod.Descricao;
                p.DescricaCurta = prod.DescricaoCurta;
                p.DetalheTecnico = prod.DetalheTecnico;
                p.Valor = prod.Valor;
                p.sku = prod.Sku;
                p.PagSeguroKey = prod.PagSeguroBotao;
                p.Destaque = prod.Destaque;
                p.DataInicio = DateTime.Now;
                p.Tipo = prod.Tipo;
                db.produto.Add(p);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Helper - transforma a DataTable recebida da camada de dados em um objeto do tipo List
        /// </summary>
        /// <param name="moddata">DataTable</param>
        /// <returns>List</returns>
        public List<Produto> Helper(System.Data.DataTable moddata)
        {
            if (moddata == null) return null;
            List<Produto> modlista = new List<Produto>();

            //foreach (DataRow dr in moddata.Rows)
            //{
            //    Produto _mod = Produto.ObterNovoProduto();
            //    _mod.ProdutoId = new System.Guid(dr["ProdutoId"].ToString());

            //    if (dr.Table.Columns.Contains("Produto"))
            //    {
            //        _mod.Nome = dr["Produto"].ToString();
            //    }
            //    else
            //    {
            //        _mod.Nome = dr["Nome"].ToString();
            //    }

            //    if (dr.Table.Columns.Contains("valida"))
            //    {
            //        _mod.RelacaoUsuario = int.Parse(dr["valida"].ToString());
            //    }
            //    _mod.Url = dr["Url"].ToString();

            //    modlista.Add(_mod);
            //}
            return modlista;
        }

        /// <summary>
        /// Helper - transforma o retorno do Entity em lista de produto
        /// </summary>
        /// <param name="entLista">IEnumerable&lt;produto&gt;</param>
        /// <returns>List&lt;Produto&gt;</returns>
        public List<Produto> Helper(IEnumerable<produto> entLista)
        {
            if (entLista == null) return null;
            List<Produto> prodLista = new List<Produto>();

            foreach (produto prd in entLista)
            {
                Produto _prod = Produto.ObterNovoProduto();
                _prod.ProdutoId = new System.Guid(prd.ProdutoId);
                _prod.Nome = prd.Nome;
                _prod.Descricao = prd.Descricao;
                _prod.Tipo = prd.Tipo==null?0:(int)prd.Tipo;
                _prod.Destaque = (int)prd.Destaque;
                _prod.DetalheTecnico = prd.DetalheTecnico;
                _prod.Valor = (decimal)prd.Valor;
                _prod.Sku = prd.sku;
                _prod.PagSeguroBotao = prd.PagSeguroKey;

                prodLista.Add(_prod);
            }
            return prodLista;
        }

        public List<Produto> ListaProdutoXCategoria()
        {
            string catid = lprop.ctId.ToString();
            IEnumerable<produto> lst = from prod in db.produto
                                       where prod.CategoriaId == catid
                                       select prod;
            return Helper(lst);
        }

        public List<Produto> ListaProdutoXId()
        {
            
            string prodId = lprop.prodId.ToString();
            IEnumerable<produto> lst = from prod in db.produto
                                       where prod.ProdutoId == prodId
                                       select prod;
            return Helper(lst);
        }


        public List<Produto> ListaProdutoXTipo()
        {
            throw new NotImplementedException();
        }

        #endregion



        public void InativaProduto(Produto prod)
        {
            using (cmsxDBEntities dbLoc = new cmsxDBEntities())
            {
                string prodid = prod.ProdutoId.ToString();
                ///limpando as imagens previas
                produto pr = (from p in dbLoc.produto
                                where p.ProdutoId == prodid
                                select p).FirstOrDefault();
                if (pr != null)
                {
                    pr.DataFinal = DateTime.Now;
                    dbLoc.Entry(pr).State = System.Data.Entity.EntityState.Modified;
                    dbLoc.SaveChanges();
                }
            }
        }
    }
}
