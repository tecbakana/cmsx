using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Dynamic;

public partial class conteudoLista : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //_obj.banco = "SqlServer";
        ListaConteudo();
    }

    protected void ListaConteudo()
    {
        _obj.parms = 2;
        _obj.areaid = "565638B9-F0B9-42E9-AFE3-63A3A893E6EC";
        _obj.ativos = 1;
        _conteudorepo.MakeConnection(_obj);
        listaApp.DataSource = _conteudorepo.ListaConteudoPorAreaId();
        listaApp.DataBind();
    }
}