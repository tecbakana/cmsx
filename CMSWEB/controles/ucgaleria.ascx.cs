using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using CMSXBLL;
using CMSXDB;

public partial class controles_ucgaleria : BaseControl
{
    public int acao { get; set; }
    private Guid _appid;
    private string cliFolder;

    protected void Page_Load(object sender, EventArgs e)
    {
        /*PROPERTIES*/
        cliFolder = Path.Combine(Directory.GetParent(Server.MapPath("")).FullName, System.Configuration.ConfigurationManager.AppSettings["pathMakeCliFolder"]);

        _appid = (Guid)Session["Aplicacaoid"];
        LoadImages();
    }

    public void LoadImages()
    {

        dlProdutoImagem.DataSource = null;
        dlProdutoImagem.DataBind();

        string b = _appid.ToString();
        cmsxDBEntities dbLoc = new cmsxDBEntities();
        var lsim = from item in dbLoc.imagem
                       where item.ParentId == b
                       select item;


        // Create sample data for the DataList control.
        DataTable dt = new DataTable();
        DataRow dr;

        // Define the columns of the table.
        dt.Columns.Add(new DataColumn("Thumb", typeof(String)));
        dt.Columns.Add(new DataColumn("InnerSlider", typeof(String)));

        // Populate the table with sample values.
        int ctrl = 0;
        foreach (var item in lsim)
        {
            dr = dt.NewRow();


            string urlImg = System.Configuration.ConfigurationManager.AppSettings["pathSite"] + "/" + ((dynamic)Session["objeto"]).url + "/images/_" + item.Url;

            ////<li class="col-md-4 h5"><a id="carousel-selector-0" class="selected"><img src="http://placehold.it/80x60&amp;text=uno" class="img-responsive"></a></li>
            string imagem = "<li class='col-sm-4 h5'>";
            //inserindo o id
            imagem += "<a id='carousel-selector-" + ctrl.ToString() + "' " + (ctrl == 0 ? "class='selected'" : " ") + ">";
            imagem += "<img src='" + urlImg + "' class='img-responsive'></a></li>";


            dr[0] = imagem;

            ////div guide
            ////<div class="active item" data-slide-number="0"><img src="http://placehold.it/1200x980&amp;text=uno" class="img-responsive"></div>

            string div = "<div class='" + (ctrl == 0 ? "active item" : "item") + "'  style='cursor:hand;width:300px;' data-slide-number='" + ctrl.ToString() + "'>";
            div += "<img src='" + urlImg + "' onclick='javascript:setImage(\"" + item.ImagemId + ";~/" + urlImg + "\");' style='cursor:hand;width:300px;' class='img-responsive'></div>";

            dr[1] = div;

            dt.Rows.Add(dr);

            ctrl++;
        }

        DataView dv = new DataView(dt);

        dlProdutoImagem.DataSource = dv;
        dlProdutoImagem.DataBind();

        rptDivCarrousel.DataSource = dv;
        rptDivCarrousel.DataBind();

    }
    
}