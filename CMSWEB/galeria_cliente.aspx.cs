using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSXBLL;

public partial class galeria_cliente : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MostraConteudo();
    }

    protected void MostraConteudo()
    {
        _obj.parms = 1;
        _obj.areaid = Request.QueryString["AreaId"];
        _imgrepo.MakeConnection(_obj);
        List<Imagem> _imglist = _imgrepo.Galeria();
        gdrimg.DataSource = _imglist;
        gdrimg.DataBind();

        imgpreview.ImageUrl = _imglist[0].Url;
        info.Text = _imglist[0].Descricao;
    }

    protected void gdrimg_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        DataList dl = (DataList)sender;
        Image img = (Image)e.Item.FindControl("imgGal");
        HiddenField hdn = (HiddenField)e.Item.FindControl("hdndesc");
        img.Attributes.Add("onmouseover", "document.getElementById(\"imgpreview\").src=\"" + img.ImageUrl.Replace("~/", "") + "\";document.getElementById(\"info\").innerText ='" + hdn.Value + "'");
        //img.Attributes.Add("onmouseout", "document.getElementById(\"imgpreview\").src=\"\";document.getElementById(\"info\").innerText ='" + hdn.Value + "'");

       // string[] imgattrib = img.ImageUrl.Split('/');
       // img.Attributes.Add("onclick", "javascript:setImage('" + imgattrib[imgattrib.Length - 1] + "','" +  + "');");

    }
}