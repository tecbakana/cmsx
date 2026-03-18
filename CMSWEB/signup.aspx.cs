using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class signup : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string lang = Request.QueryString["lang"];
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(lang, true);
        setLang();
    }

    private void setLang()
    {
        ltOla.Text = new utilities().retornaIdioma("resOla");
        lkMais.Text = new utilities().retornaIdioma("resMais");
    }
}