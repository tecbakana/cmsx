using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ICMSX;
using CMSXData;
using CMSXData.Models;
using System.Dynamic;

namespace CMSXBLL.Repositorio
{
    public class ProdutoRepositorio : BaseRepositorio,IProdutoRepositorio
    {
        private IProdutoDAL dal;

        #region IProdutoRepositorio Members

        public void MakeConnection(dynamic prop)
        {
            dal = container.Resolve<IProdutoDAL>();
            db = new CmsxDbContext();
            string bc = prop.banco;
            int parm = prop.parms;
            lprop = prop;
            dal.MakeConnection((ExpandoObject)prop);
        }

        public List<Produto> ListaProduto()
        {
            
            string appid = lprop.appid.ToString();
            IEnumerable<Produto> lst = from prod in db.Produtos
                                       where prod.Aplicacaoid == appid
                                       select new Produto()
                                       {
                                          CategoriaId = new Guid(prod.Cateriaid),
                                          Nome = prod.Nome,
                                          ProdutoId = new Guid(prod.Produtoid),
                                          Descricao = prod.Descricao,
                                          Valor = prod.Valor??0M
                                       };
            return Helper(lst);
        }

        public void EditaProduto(Produto prod)
        {
            throw new NotImplementedException();
        }

        public void CriaProduto(Produto prod)
        {
            using (CmsxDbContext db = new CmsxDbContext())
            {
                var p = new CMSXData.Models.Produto();
                p.Aplicacaoid = prod.AplicacaoId.ToString();
                p.Cateriaid = prod.CategoriaId.ToString();
                p.Produtoid = prod.ProdutoId.ToString();
                p.Nome = prod.Nome;
                p.Descricao = prod.Descricao;
                p.Descricacurta = prod.DescricaoCurta;
                p.Detalhetecnico = prod.DetalheTecnico;
                p.Valor = prod.Valor;
                p.Sku = prod.Sku;
                p.Pagsegurokey = prod.PagSeguroBotao;
                p.Destaque = prod.Destaque;
                p.Datainicio = DateTime.Now;
                p.Tipo = prod.Tipo;
                db.Produtos.Add(p);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Helper - transforma a DataTable recebida da camada de dados em um objeto do tipo List
        /// </summary>
        /// <param name="moddata">DataTable</param>
        /// <returns>List</returns>
        public List<Produto> Helper(DataTable moddata)
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
        public List<Produto> Helper(IEnumerable<Produto> entLista)
        {
            if (entLista == null) return null;
            List<Produto> prodLista = new List<Produto>();

            foreach (Produto prd in entLista)
            {
                Produto _prod = Produto.ObterNovoProduto();
                _prod.ProdutoId = prd.ProdutoId;
                _prod.Nome = prd.Nome;
                _prod.Descricao = prd.Descricao;
                _prod.Tipo = prd.Tipo==null?0:(int)prd.Tipo;
                _prod.Destaque = (int)prd.Destaque;
                _prod.DetalheTecnico = prd.DetalheTecnico;
                _prod.Valor = (decimal)prd.Valor;
                _prod.Sku = prd.Sku;
                _prod.PagSeguroBotao = prd.PagSeguroBotao;

                prodLista.Add(_prod);
            }
            return prodLista;
        }

        public List<Produto> ListaProdutoXCategoria()
        {
            string catid = lprop.ctId.ToString();
            IEnumerable<Produto> lst = from prod in db.Produtos
                                       where prod.Cateriaid == catid
                                       select new Produto()
                                       {
                                           CategoriaId = new Guid(prod.Cateriaid),
                                           Nome = prod.Nome,
                                           ProdutoId = new Guid(prod.Produtoid),
                                           Descricao = prod.Descricao,
                                           Valor = prod.Valor ?? 0M
                                       };
            return Helper(lst);
        }

        public List<Produto> ListaProdutoXId()
        {
            
            string prodId = lprop.prodId.ToString();
            IEnumerable<Produto> lst = from prod in db.Produtos
                                       where prod.Produtoid == prodId
                                       select new Produto()
                                       {
                                           CategoriaId = new Guid(prod.Cateriaid),
                                           Nome = prod.Nome,
                                           ProdutoId = new Guid(prod.Produtoid),
                                           Descricao = prod.Descricao,
                                           Valor = prod.Valor ?? 0M
                                       };
            return Helper(lst);
        }


        public List<Produto> ListaProdutoXTipo()
        {
            throw new NotImplementedException();
        }

        #endregion



        public void InativaProduto(Produto prod)
        {
            using (CmsxDbContext dbLoc = new CmsxDbContext())
            {
                string prodid = prod.ProdutoId.ToString();
                ///limpando as imagens previas
                var pr = (from p in dbLoc.Produtos
                                where p.Produtoid == prodid.ToString()
                                select p).FirstOrDefault();
                if (pr != null)
                {
                    pr.Datafinal = DateTime.Now;
                    dbLoc.Entry(pr).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbLoc.SaveChanges();
                }
            }
        }
    }
}
