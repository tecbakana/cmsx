using System.Data;
using ICMSX;
using CMSXData;
using CMSXData.Models;

namespace CMSXDAO
{
    public class ProdutoDAL : BaseDAL, IProdutoDAL
    {
        public ProdutoDAL(IDataFactory factory) : base(factory) { }

        public void CriaProduto()
        {
            throw new NotImplementedException();
        }

        public void EditaProduto()
        {
            throw new NotImplementedException();
        }

        public void InativaProduto()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Produto> ListaProduto()
        {
            string appid = _localProps.appid;
            CmsxDbContext db = new CmsxDbContext();
            IEnumerable<Produto> lst = from prod in db.Produtos
                                       where prod.Aplicacaoid == appid
                                       select prod;
            return lst;
        }

        public DataTable ListaProdutoPorId()
        { 
            throw new NotImplementedException();
        }

        public DataTable ListaProdutoRelacionado()
        {
            throw new NotImplementedException();
        }
    }
}
