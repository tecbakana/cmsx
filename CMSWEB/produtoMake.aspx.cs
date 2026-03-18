using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSXBLL;
using System.Dynamic;
using System.IO;

public partial class produtoMake : BasePage
{
    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        ucListaProduto.editaProduto+=ucListaProduto_editaProduto;
        //ucMakeProduto.AtualizaLista += ucMakeProduto_AtualizaLista;
        ucMakeProduto.editaProduto += ucListaProduto_editaProduto;
    }

    protected void ucListaProduto_editaProduto(string[] parm)
    {
        Editar(parm);
    }

    protected void EditaLinha(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DeleteItem":
                Inativar(new Guid(e.CommandArgument.ToString()));
                break;
            case "AddAtrib":
                //Editar(new Guid(e.CommandArgument.ToString()));
                break;
        }
    }

    #endregion

    #region Metodos

    protected void Editar(string[] parms)
    {

         //Render the function invocation. 
        string funcCall = "callTab('atributos');";
        ScriptManager.RegisterStartupScript(this,this.GetType(), "JSScript", funcCall,true);

        controles_ucAtributos_opcao uc = ucAtributos_opcao;
        uc.LoadAtributos(parms[0]);

        controles_ucGaleriaMake ug = ucGaleriaMake;
        ug.LoadImages(parms[0]);

        Literal lt = (Literal)uc.FindControl("ltProduto");
        lt.Text = parms[1];
        UpdatePanel pn = (UpdatePanel)uc.FindControl("updAtributo");
        pn.Update();

    }

    protected void IncluirOpcao(string[] parms)
    {
        Session["prodId"] = parms[0];

        //Render the function invocation. 
        string funcCall = "callTab('attributes')";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "JSScript", funcCall, true);
        controles_ucAtributos_opcao uc = ucAtributos_opcao;
        controles_ucGaleriaMake ucg = ucGaleriaMake;
        uc.LoadAtributos(string.Empty);
        Literal lt = (Literal)uc.FindControl("ltProduto");
        lt.Text = parms[1];
        UpdatePanel pn = (UpdatePanel)uc.FindControl("updAtributo");
        pn.Update();
        UpdatePanel pnx = (UpdatePanel)ucg.FindControl("pnlformapp");
        pnx.Update();
    }

    protected void Inativar(Guid produtoId)
    {

    }

    #endregion


}