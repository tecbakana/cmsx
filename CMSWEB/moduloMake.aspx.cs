using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;


public partial class moduloMake : BasePage
{

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!ValidateSession)
        {
            Response.Redirect("Default.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        btnmodmake.Click += new EventHandler(btnmodmake_Click);
        ListaModulo();
        VarreDiretorio();

    }

    protected void btnmodmake_Click(object sender, EventArgs e)
    {
        CriaModulo();
    }

    protected void CriaModulo()
    {
        _obj.parms = 3;
        _obj.moduloid = System.Guid.NewGuid();
        _obj.nome = txtmodnome.Text;
        _obj.url = lstPaginas.SelectedValue;
        _modrepo.MakeConnection(_obj);
        _modrepo.CriaModulo();
        pnlformapp.Update();
    }

    protected void ListaModulo()
    {
        lstMod.DataSource = null;
        lstMod.DataBind();
        _obj.parms = 0;
        _modrepo.MakeConnection(_obj);
        lstMod.DataSource = _modrepo.ListaModulos();
        lstMod.DataBind();
    }

    protected void VarreDiretorio()
    {
        DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/"));
        FileInfo[] rgFiles = di.GetFiles("*ake.aspx");

        lstPaginas.Items.Add(new ListItem("Defina a pagina"));
        foreach (FileInfo fi in rgFiles)
        {
            lstPaginas.Items.Add(new ListItem(fi.Name,fi.Name));
        }
    }
}