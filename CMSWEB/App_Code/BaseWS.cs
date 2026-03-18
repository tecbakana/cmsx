using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using CMSXBLL;
using CMSXBLL.Repositorio;
using System.Dynamic;
using System.Configuration;

/// <summary>
/// Summary description for BaseControl
/// </summary>
public class BaseWS : System.Web.Services.WebService
{
    public dynamic _obj = new ExpandoObject();

    public ConteudoRepositorio _conteudorepo = new ConteudoRepositorio();
    public Conteudo _conteudo = new Conteudo();

    public AreasRepositorio _areasrepo = new AreasRepositorio();
    public Areas _areas = new Areas();

    public AplicacaoRepositorio _apprepo = new AplicacaoRepositorio();
    public Aplicacao _app = new Aplicacao();

    public UsuarioRepositorio _usurepo = new UsuarioRepositorio();
    public UsuarioBLL _usu = new UsuarioBLL();

    public ModuloRepositorio _modrepo = new ModuloRepositorio();
    public Modulo _mod = new Modulo();

    public RelacaoRepositorio _relrepo = new RelacaoRepositorio();
    public Relacao _rel = new Relacao();

    public AcessoRepositorio _acessorepo = new AcessoRepositorio();
    public Acesso _acc = new Acesso();

    public MenuRepositorio _menurepo = new MenuRepositorio();
    public MenuBLL _menu = new MenuBLL();

    public FormularioRepositorio _formrepo = new FormularioRepositorio();
    public Formulario _form = new Formulario();

    public EmaiRepositorio _mailrepo = new EmaiRepositorio();
    public Email _mail = new Email();

    public ProdutoRepositorio _prodrepo = new ProdutoRepositorio();
    public Produto _prod = new Produto();

    public UnidadeRepositorio _undrepo = new UnidadeRepositorio();
    public Unidade _und = new Unidade();

    public AtributoRepositorio _attrepo = new AtributoRepositorio();
    public Atributo _att = new Atributo();

    public OpcaoRepositorio _opcrepo = new OpcaoRepositorio();
    public Opcao _opc = new Opcao();

    public BaseWS()
    {
        ConfigureContainer();
    }

    public void ConfigureContainer()
    {

        _obj.banco = ConfigurationManager.AppSettings["defaultDB"].ToString();
    }

}