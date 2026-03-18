using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using CMSXBLL;

public partial class controles_uccarousel : BaseControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["cliente"] != null)
        {
            mountCarousel();
        }
    }

    private void mountCarousel()
    {
        try
        {
            _obj.parms = 1;
            _app = (Aplicacao)Session["cliente"];
            _obj.appid = _app.AplicacaoId;
            _areasrepo.MakeConnection(_obj);

            rpCarousel.DataSource = _areasrepo.ListaAreaMenuSplash();
            rpCarousel.DataBind();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.StackTrace);
        }
    }
}