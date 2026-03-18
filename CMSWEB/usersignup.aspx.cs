using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSXBLL;

public partial class usersignup : BasePage
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

       
    }


    //protected void btnusumake_Click(object sender, EventArgs e)
    //{
    //    CriaUsuario();
    //}

    //protected bool CriaUsuario()
    //{
    //    UsuarioBLL _usu = UsuarioBLL.ObterNovoUsuario();


    //    _obj.parms = 6;
    //    Guid usuid = System.Guid.NewGuid();
    //    _obj.userid = usuid;
    //    _obj.nome = txtusunome.Text;
    //    _obj.sobrenome = txtususobrenome.Text;
    //    _obj.apelido = txtusuapelido.Text;
    //    if(txtususenha1.Text!= txtususenha2.Text)
    //    {
    //        lblErro.Text = "As senhas não conferem, favor redigitar";
    //        lblErro.Visible = true;
    //        return false;
    //    }
    //    else
    //    {
    //        _obj.senha = txtususenha1.Text;
    //    }

    //    _usurepo.MakeConnection(_obj);
    //    _usurepo.CriaUsuario();

    //    /*   VINCULANDO USUÁRIO ÀS APLICAÇÕES ESCOLHIDAS */
    //    foreach(ListItem item in lstApp.Items)
    //    {
    //        if (item.Selected==true)
    //        {
    //            _obj.parms = 3;
    //            _obj.relid = System.Guid.NewGuid();
    //            _obj.appid = new System.Guid(item.Value);
    //            _obj.userid = usuid;

    //            _relrepo.MakeConnection(_obj);
    //            _relrepo.CriaRelacaoAplicacao();
    //        }
    //    }

    //    /*   VINCULANDO USUÁRIO AOS MÓDULOS ESCOLHIDOS */
    //    foreach (ListItem item in lstModulos.Items)
    //    {
    //        if (item.Selected==true)
    //        {
    //            _obj.parms = 4;
    //            _obj.tipo = "usuario";
    //            _obj.relid = System.Guid.NewGuid();
    //            _obj.PaiId = new System.Guid(item.Value);
    //            _obj.FilhoId = usuid;

    //            _relrepo.MakeConnection(_obj);
    //            _relrepo.CriaRelacaoModulo();
    //        }
    //    }

    //    ListaUsuarios();
    //    CarregaListas();
    //    pnlformapp.Update();

    //    return true;
    //}

    //protected void ListaUsuarios()
    //{

    //    lstUsu.DataSource = null;
    //    lstUsu.DataBind();
    //    _obj.parms = 1;
    //    _obj.appid = Session["Aplicacaoid"].ToString();
    //    _usurepo.MakeConnection(_obj);
    //    //gdrUsu.DataSource = _usurepo.ListaUsuarios();
    //    //gdrUsu.DataBind();
    //    lstUsu.DataSource = _usurepo.ListaUsuariosPorAppId();
    //    lstUsu.DataBind();
    //}

    //protected void CarregaListas()
    //{
    //    LimpaListas();

    //    _obj.parms = 1;
    //    _obj.admin = Session["USUARIO"].ToString();
    //    _apprepo.MakeConnection(_obj);
        
    //    lstApp.DataSource = _apprepo.ListaAplicacao();
    //    lstApp.DataBind();

    //    _obj.parms = 0;
    //    _modrepo.MakeConnection(_obj);
    //    lstModulos.DataSource = _modrepo.ListaModulos();
    //    lstModulos.DataBind();
    //}

    //protected void LimpaListas()
    //{
    //    lstApp.DataSource = null;
    //    lstApp.DataBind();
    //    lstModulos.DataSource = null;
    //    lstModulos.DataBind();
    //}

    //protected void lstUsu_ItemCommand(object source, RepeaterCommandEventArgs e)
    //{

    //    if ((e.Item.ItemType == ListItemType.Item) ||
    //       (e.Item.ItemType == ListItemType.AlternatingItem))
    //    {
    //        if (e.CommandName == "edtModul")
    //        {
    //            this.mpe.Show();
    //            hdnUser.Value = e.CommandArgument.ToString();
    //            CarregaModulosEdicao(hdnUser.Value);
    //        }
    //    }
    //}

    //protected void CarregaModulosEdicao(string userId)
    //{
    //    lstModulosUsuario.Items.Clear();

    //    _obj.parms = 1;
    //    _obj.userid = userId;
    //    _modrepo.MakeConnection(_obj);
    //    hdnUsuarioID.Value = userId;
    //    //lstModulosUsuario.DataSource = _modrepo.ListaModulosXUser();
    //    //lstModulosUsuario.DataBind();

    //    List<Modulo> lst = _modrepo.ListaModulosXUser();
    //    int count = 0;

    //    //lstModulos.Items.Add(new ListItem("Escolha o item", "-1"));

    //    if (lst.Count >= 1)
    //    {
    //        foreach (Modulo item in lst)
    //        {
    //            lstModulosUsuario.Items.Add(new ListItem(item.Nome, item.ModuloId.ToString()));
    //            if (item.RelacaoUsuario == 1)
    //            {
    //                lstModulosUsuario.Items[count].Selected = true;
    //                lstModulosUsuario.Items[count].Enabled = false;
    //            }
    //            count++;
    //        }
    //    }
    //}

    //protected void btnSalvar_Click(object sender, EventArgs e)
    //{
    //    foreach (ListItem item in lstModulosUsuario.Items)
    //    {
    //        if ((item.Selected == true) && (item.Enabled == true))
    //        {
    //            _obj.parms = 4;
    //            _obj.tipo = "usuario";
    //            _obj.relid = System.Guid.NewGuid();
    //            _obj.PaiId = new System.Guid(item.Value);
    //            _obj.FilhoId = new System.Guid(hdnUser.Value);

    //            _relrepo.MakeConnection(_obj);
    //            _relrepo.CriaRelacaoModulo();
    //        }
    //    }
    //}

    //protected void lstUsu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
    //    {
    //        ListBox lstb = (ListBox)e.Item.FindControl("lstMod");
    //        _obj.parms = 1;

    //        //_obj.userid = (e.Item.DataItem).;
    //        _modrepo.MakeConnection(_obj);
    //        //lstb.DataSource = _modrepo.ListaModulosXUser();
    //    }
    //}

}