using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Dynamic;

public partial class controles_menu : BaseControl
{
    private int _idx;

    protected void Page_Load(object sender, EventArgs e)
    {
        btExit.Click += new EventHandler(btExit_Click);
        if (Session["Objeto"] != null)
        {
            _obj = (ExpandoObject)Session["Objeto"];
            montaMenu();
        }
        else
        {
            Response.Redirect("default.aspx");
        }

    }

    protected void btExit_Click(object sender, EventArgs e)
    {
        Sair();
    }

    protected void Sair()
    {
        Session["USUARIO"] = null;
        Session["Objeto"] = null;
        //pnlMenu.Visible = true;
        lstmenu.DataSource = null;
        lstmenu.DataBind();
        Response.Redirect("Default.aspx");
    }

    protected void montaMenu()
    {
        _menurepo.MakeConnection((ExpandoObject)_obj);
        lstmenu.DataSource = _menurepo.montaMenu();
        lstmenu.DataBind();
        //pnlMenu.Visible = false;
        //pnlMenu.Visible = true;
    }

    protected void lstmenu_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item)
        {
            if(e.Item.ItemIndex == _idx)
            {
                ((DataListItem)e.Item).CssClass="active";
            }
        }
    }
    protected void lstmenu_ItemCommand(object source, DataListCommandEventArgs e)
    {
        
    }
    protected void lstmenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        _idx =  lstmenu.SelectedIndex;
    }
}