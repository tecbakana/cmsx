using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Dynamic;
using System.IO;
using CMSXBLL;

public partial class galeriamake : BasePage
{
    private dynamic ambiente;

    protected void Page_Load(object sender, EventArgs e)
    {
        ucGaleriaMake.LoadImages();

    }

}