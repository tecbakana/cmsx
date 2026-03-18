using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using CMSXBLL;
using System.Data;

public partial class controles_ucadrotator : BaseControl
{
    public List<Imagem> RotatorConfig;


    protected void Page_Load(object sender, EventArgs e)
    {
        AdRotatorFlex();
    }

    protected void tmrAd_Tick(object sender, EventArgs e)
    {
        pnlAd.Update();
    }

    protected void AdRotatorFlex()
    {
        DataTable refr = new DataTable();
        refr.Columns.Add("ImageUrl");
        refr.Columns.Add("AlternateText");
        refr.Columns.Add("NavigateUrl");
        foreach (Imagem item in RotatorConfig)
        {
            DataRow dr = refr.NewRow();
            dr["ImageUrl"] = item.Url;
            dr["AlternateText"] = "";
            dr["NavigateUrl"] = "";
            refr.Rows.Add(dr);
        }
        refr.AcceptChanges();
        if (refr.Rows.Count >= 1)
        {
            adflex.DataSource = refr;
            adflex.DataBind();
            pnlAd.Visible = true;
        }
        else
        {
            pnlAd.Visible = false;
        }

        if (refr.Rows.Count == 1)
        {
            tmrAd.Enabled = false;
        }
    }
}