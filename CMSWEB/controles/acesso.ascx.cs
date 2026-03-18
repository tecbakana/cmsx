using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Dynamic;

public partial class controles_acesso : BaseControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btLogar.Click += new EventHandler(btLogar_Click);

        if (Session["USUARIO"]!=null && Session["Objeto"]!=null)
        {
            _obj = (ExpandoObject)Session["Objeto"];
        }
    }

    protected void btLogar_Click(object sender, EventArgs e)
    {
        try
        {
            _obj.parms = 2;
            _obj.apelido = txtLogin.Value;
            _obj.senha = txtPsw.Value;

            _acessorepo.MakeConnection(_obj);
            dynamic db = _acessorepo.ValidaAcesso();

            _obj.parms = db.parms;
            _obj.userid = db.userid;
            _obj.aplicacaoid = db.aplicacaoid;
            _obj.url = db.url;

            /* armazenando usuario em session */
            Session["USUARIO"] = db.userid;
            Session["AplicacaoId"] = db.aplicacaoid;
            Session["Objeto"] = _obj;
        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alerta", "alert('Erro no login/senha, tente novamente!');");
            Response.Redirect("Default.aspx");
        }
        finally
        {
            Response.Redirect("inicial.aspx");
        }
    }
}