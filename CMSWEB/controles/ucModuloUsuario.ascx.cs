using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSXBLL;
using CMSXBLL.Repositorio;

public partial class controles_ucModuloUsuario : BaseControl
{
    #region Properties
    public string UserId { get; set; }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

      CarregaListas();

      if (!Page.IsPostBack)
      {

      }

    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in lstModulos.Items)
        {
            if ((item.Selected == true)||(item.Enabled==true))
            {
                _obj.parms = 4;
                _obj.tipo = "usuario";
                _obj.relid = System.Guid.NewGuid();
                _obj.PaiId = new System.Guid(item.Value);
                _obj.FilhoId = new System.Guid(UserId);

                _relrepo.MakeConnection(_obj);
                _relrepo.CriaRelacaoModulo();
            }
        }
    }

    protected void CarregaListas()
    {
        LimpaListas();
        _obj.parms = 1;
        _obj.userid = UserId;
        _modrepo.MakeConnection(_obj);

        List<Modulo> lst = _modrepo.ListaModulosXUser();
        int count = 0;

        //lstModulos.Items.Add(new ListItem("Escolha o item", "-1"));

        if (lst.Count >= 1)
        {
            foreach (Modulo item in lst)
            {
                lstModulos.Items.Add(new ListItem(item.Nome, item.ModuloId.ToString()));
                if (item.RelacaoUsuario == 1)
                {
                    lstModulos.Items[count].Selected = true;
                    lstModulos.Items[count].Enabled = false;
                }
                count++;
            }
        }
    }

    protected void LimpaListas()
    {
        lstModulos.DataSource = null;
        lstModulos.DataBind();
    }
}