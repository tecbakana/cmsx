using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSXBLL;

public partial class formulariomake : BasePage
{
    string xmlform = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MontaComboAreas();
            ListaFormulario();
        }

    }

    protected void send_content(object sender,EventArgs e)
    {
        MontaFormulario();
        _obj.parms = 6;
        GravaFormulario();
    }

    protected void MontaComboAreas()
    {
        _obj.parms = 2;
        _obj.appid = Session["AplicacaoId"];
        _obj.tpArea = null;
        _areasrepo.MakeConnection(_obj);
        lstAreas.Items.Clear();

        foreach (Areas _area in _areasrepo.ListaAreasPai())
        {
            ListItem _item = new ListItem(_area.Nome, _area.AreaId.ToString());
            lstAreas.Items.Add(_item);
        }
    }

    public void MontaFormulario()
    {

        xmlform = "<?xml version=\"1.0\"?>";
        xmlform += "<formulario><elementos>";
        
        //email, texto, sugestões, envio de arquivo
        //0,1,2,3
        foreach (ListItem item in ckbItems.Items)
        {
            if (item.Selected == true)
            {
                xmlform += "<elemento id='form_" + item.Value + "' />";
            }
        }

        xmlform += "</elementos></formulario>";
    }

    public void GravaFormulario()
    {
        Formulario _frm = Formulario.ObtemNovoFormulario();

        Guid formularioId = System.Guid.NewGuid();
        _obj.formularioid = formularioId;
        _obj.areaid = new System.Guid(lstAreas.SelectedValue);
        _obj.nome = txtNome.Text;
        _obj.valor = xmlform;
        _obj.data = DateTime.Now.ToString("yyyy-MM-dd 00:00:00.000");
        _formrepo.MakeConnection(_obj);
        _formrepo.CriaFormulario();

    }

    public void ListaFormulario()
    {
        dlformulario.DataSource = null;
        dlformulario.DataBind();
        _obj.parms = 1;
        _obj.aplicacaoid = Session["AplicacaoId"];
        _formrepo.MakeConnection(_obj);
        dlformulario.DataSource = _formrepo.ListaFormulario();
        dlformulario.DataBind();
    }
}