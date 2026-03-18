using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSXBLL;
using CMSDAL;
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Exception;
using Uol.PagSeguro.Resources;

public partial class controles_ucPagamento : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GeraPagamento();
        }
    }


    #region PAGSEGURO

    /*------------------------------------ INTEGRACAO PAGSEGURO --------------------------------------*/
    private void GeraPagamento()
    {
        //--------------------------------------------INICIALIZA OBJETOS DO PAGSEGURO ---------------------------------*/
        List<Produto> lsp = (List<Produto>)Session["listaProduto"];

        PaymentRequest payreq = new PaymentRequest();

        foreach (Produto p in lsp)
        {
            payreq.Items.Add(new Item(p.Sku.ToString(), p.Nome.ToString(), 1, p.Valor));
        }

        payreq.Currency = Currency.Brl;
        payreq.Shipping = new Shipping();
        payreq.Shipping.ShippingType = ShippingType.Pac;
        ///------------------------------------------ DADOS DO COMPRADOR -----------------------------------------------

        //payreq.Sender = new Sender("nome", "email", new Phone());

        ///------------------------------------------ CREDENCIAIS ------------------------------------------------------

        AccountCredentials credentials = new AccountCredentials(
            System.Configuration.ConfigurationManager.AppSettings["userpg"].ToString(),
            System.Configuration.ConfigurationManager.AppSettings["pgtk"].ToString()
        );

        Uri redirUri = payreq.Register(credentials);
        frmPagSeguro.Attributes.Add("src", redirUri.AbsoluteUri);
        Session["paypath"] = redirUri.AbsoluteUri;
    }


    /*-----------------------------------------------------------------------------------------------*/
    #endregion

}