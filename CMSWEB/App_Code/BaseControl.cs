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
public class BaseControl : System.Web.UI.UserControl
{
    public dynamic _obj = new ExpandoObject();

    public ConteudoRepositorio _conteudorepo = new ConteudoRepositorio();
    public Conteudo _conteudo = new Conteudo();

    public AreasRepositorio _areasrepo = new AreasRepositorio();
    public Areas _areas = new Areas();

    public CategoriaRepositorio _catrepo = new CategoriaRepositorio();
    public Categoria _categoria = new Categoria();

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

    public RoteiroRepositorio _rotrepo = new RoteiroRepositorio();
    public RoteiroBLL _roteiro = new RoteiroBLL();

    public ResortRepositorio _resrepo = new ResortRepositorio();
    public ResortBLL _resort = new ResortBLL();

    public ProdutoRepositorio _prodrepo = new ProdutoRepositorio();
    public Produto _prod = new Produto();

    public UnidadeRepositorio _undrepo = new UnidadeRepositorio();
    public Unidade _und = new Unidade();

    public AtributoRepositorio _attrepo = new AtributoRepositorio();
    public Atributo _att = new Atributo();

    public OpcaoRepositorio _opcrepo = new OpcaoRepositorio();
    public Opcao _opc = new Opcao();

    public ImagemRepositorio _imgrepo = new ImagemRepositorio();
    public Imagem _img = new Imagem();

	public BaseControl()
    {
        ConfigureContainer();
    }

    public void ConfigureContainer()
    {

        _obj.banco = ConfigurationManager.AppSettings["defaultDB"].ToString();
    }

    protected override void OnError(EventArgs e)
    {
        // At this point we have information about the error
        HttpContext ctx = HttpContext.Current;

        Exception exception = ctx.Server.GetLastError();

        string errorInfo =
           "<br>Offending URL: " + ctx.Request.Url.ToString() +
           "<br>Source: " + exception.Source +
           "<br>Message: " + exception.Message +
           "<br>Stack trace: " + exception.StackTrace;

        ctx.Response.Write(errorInfo);

        // --------------------------------------------------
        // To let the page finish running we clear the error
        // --------------------------------------------------
        ctx.Server.ClearError();

        base.OnError(e);
    }

    protected object[] setParm(string parms)
    {
        /*string[] parmValue = parms.Split(',');*/
        object[] obj = new object[1];//parmValue.Length];
        int cont = 0;
        /* foreach (string parm in parmValue)
         {*/
        obj[cont] = parms as object;
        cont++;
        /*}*/
        return obj;
    }

    /*delegates*/
    public System.Delegate _delGoLocation;
    public Delegate ucGoLocation
    {
        set { _delGoLocation = value; }
    }
}