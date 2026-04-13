$base = "T:\Developer\RepositorioTrabalho\tecbakana\cmsx\CMSXRepo"

$files = @{

"AcessoRepositorio.cs" = @'
using System.Dynamic;
using ICMSX;

namespace CMSXRepo
{
    public class AcessoRepositorio : BaseRepositorio, IAcessoRepositorio
    {
        private readonly IAcessoDAL _dal;

        public AcessoRepositorio(CMSXData.Models.CmsxDbContext db, IAcessoDAL dal) : base(db) { _dal = dal; }

        public void MakeConnection(dynamic prop) => _dal.MakeConnection((ExpandoObject)prop);
        public dynamic ValidaAcesso() => throw new NotImplementedException();
    }
}
'@

"ArquivoRepositorio.cs" = @'
using System;
using System.Collections.Generic;
using System.Dynamic;
using CMSXData.Models;
using ICMSX;

namespace CMSXRepo
{
    public class ArquivoRepositorio : BaseRepositorio, IArquivoRepositorio
    {
        private readonly IArquivoDAL _dal;

        public ArquivoRepositorio(CMSXData.Models.CmsxDbContext db, IArquivoDAL dal) : base(db) { _dal = dal; }

        public void MakeConnection() => throw new NotImplementedException();
        public void CriaArquivo() => throw new NotImplementedException();
        public Arquivo ObtemArquivoPorNome() => throw new NotImplementedException();
        public Arquivo ObtemArquivoPorId() => throw new NotImplementedException();
        public List<Arquivo> ListaArquivoPorAreaId() => throw new NotImplementedException();
        public List<Arquivo> ListaArquivoPorConteudoId() => throw new NotImplementedException();
    }
}
'@

"EmaiRepositorio.cs" = @'
using ICMSX;

namespace CMSXRepo
{
    public class EmaiRepositorio : BaseRepositorio, IEmailRepositorio
    {
        public EmaiRepositorio(CMSXData.Models.CmsxDbContext db) : base(db) { }

        public void MontaEmail() => throw new NotImplementedException();
        public void Enviar() => throw new NotImplementedException();
    }
}
'@

"FormularioRepositorio.cs" = @'
using System.Collections.Generic;
using System.Dynamic;
using CMSXData.Models;
using ICMSX;

namespace CMSXRepo
{
    public class FormularioRepositorio : BaseRepositorio, IFormularioRepositorio
    {
        private readonly IFormularioDAL _dal;

        public FormularioRepositorio(CMSXData.Models.CmsxDbContext db, IFormularioDAL dal) : base(db) { _dal = dal; }

        public void MakeConnection(dynamic prop) => _dal.MakeConnection((ExpandoObject)prop);
        public void CriaFormulario() => throw new NotImplementedException();
        public List<Formulario> ListaFormulario() => throw new NotImplementedException();
        public List<Formulario> ListaFormularioPorId() => throw new NotImplementedException();
    }
}
'@

"ImagemRepositorio.cs" = @'
using System;
using System.Collections.Generic;
using System.Dynamic;
using CMSXData.Models;
using ICMSX;

namespace CMSXRepo
{
    public class ImagemRepositorio : BaseRepositorio, IImagemRepositorio
    {
        public ImagemRepositorio(CMSXData.Models.CmsxDbContext db) : base(db) { }

        public void MakeConnection(dynamic prop) => throw new NotImplementedException();
        public Imagem ObtemImagemPorId(Guid id) => throw new NotImplementedException();
        public void CriaNovaImagem() => throw new NotImplementedException();
        public void InsereImagemGaleria() => throw new NotImplementedException();
        public void AtualizaGaleria() => throw new NotImplementedException();
        public List<Imagem> Galeria() => throw new NotImplementedException();
        public List<Imagem> GaleriaConteudo() => throw new NotImplementedException();
        public List<Imagem> GaleriaParentId() => throw new NotImplementedException();
    }
}
'@

"MenuRepositorio.cs" = @'
using System.Collections.Generic;
using System.Dynamic;
using CMSXData.Models;
using ICMSX;

namespace CMSXRepo
{
    public class MenuRepositorio : BaseRepositorio, IMenuRepositorio
    {
        private readonly IMenuDAL _dal;

        public MenuRepositorio(CMSXData.Models.CmsxDbContext db, IMenuDAL dal) : base(db) { _dal = dal; }

        public void MakeConnection(dynamic prop) => _dal.MakeConnection((ExpandoObject)prop);
        public List<Area> MontaMenu() => throw new NotImplementedException();
        public List<Area> MontaMenu(string id) => throw new NotImplementedException();
    }
}
'@

"ModuloRepositorio.cs" = @'
using System.Collections.Generic;
using System.Dynamic;
using CMSXData.Models;
using ICMSX;

namespace CMSXRepo
{
    public class ModuloRepositorio : BaseRepositorio, IModuloRepositorio
    {
        private readonly IModuloDAL _dal;

        public ModuloRepositorio(CMSXData.Models.CmsxDbContext db, IModuloDAL dal) : base(db) { _dal = dal; }

        public void MakeConnection(dynamic prop) => _dal.MakeConnection((ExpandoObject)prop);
        public List<Modulo> ListaModulos() => throw new NotImplementedException();
        public void CriaModulo() => throw new NotImplementedException();
    }
}
'@

"RelacaoRepositorio.cs" = @'
using System.Collections.Generic;
using System.Dynamic;
using CMSXData.Models;
using ICMSX;

namespace CMSXRepo
{
    public class RelacaoRepositorio : BaseRepositorio, IRelacaoRepositorio
    {
        private readonly IRelacaoDAL _dal;

        public RelacaoRepositorio(CMSXData.Models.CmsxDbContext db, IRelacaoDAL dal) : base(db) { _dal = dal; }

        public void MakeConnection(dynamic prop) => _dal.MakeConnection((ExpandoObject)prop);
        public List<Relmoduloaplicacao> ListaRelacaoModuloAplicacao() => throw new NotImplementedException();
        public void CriaRelacaoAplicacao() => throw new NotImplementedException();
        public void CriaRelacaoModulo() => throw new NotImplementedException();
    }
}
'@

"ResortRepositorio.cs" = @'
using ICMSX;

namespace CMSXRepo
{
    public class ResortRepositorio : BaseRepositorio, IResortRepositorio
    {
        public ResortRepositorio(CMSXData.Models.CmsxDbContext db) : base(db) { }

        public void MakeConnection(dynamic prop) => throw new NotImplementedException();
    }
}
'@

"RoteiroRepositorio.cs" = @'
using ICMSX;

namespace CMSXRepo
{
    public class RoteiroRepositorio : BaseRepositorio, IRoteiroRepositorio
    {
        public RoteiroRepositorio(CMSXData.Models.CmsxDbContext db) : base(db) { }

        public void MakeConnection(dynamic prop) => throw new NotImplementedException();
    }
}
'@

"UnidadeRepositorio.cs" = @'
using System.Collections.Generic;
using CMSXData.Models;
using ICMSX;

namespace CMSXRepo
{
    public class UnidadeRepositorio : BaseRepositorio, IUnidadeRepositorio
    {
        public UnidadeRepositorio(CMSXData.Models.CmsxDbContext db) : base(db) { }

        public void MakeConnection(dynamic prop) => throw new NotImplementedException();
        public void CriaNovaUnidade() => throw new NotImplementedException();
        public List<Unidade> ListaUnidade() => throw new NotImplementedException();
    }
}
'@

"UsuarioRepositorio.cs" = @'
using System;
using System.Collections.Generic;
using System.Dynamic;
using CMSXData.Models;
using ICMSX;

namespace CMSXRepo
{
    public class UsuarioRepositorio : BaseRepositorio, IUsuarioRepositorio
    {
        private readonly IUsuarioDAL _dal;

        public UsuarioRepositorio(CMSXData.Models.CmsxDbContext db, IUsuarioDAL dal) : base(db) { _dal = dal; }

        public void MakeConnection(dynamic prop) => _dal.MakeConnection((ExpandoObject)prop);
        public Usuario ObtemUsuarioPorId(Guid id) => throw new NotImplementedException();
        public void CriaUsuario() => throw new NotImplementedException();
        public List<Usuario> ListaUsuarios() => throw new NotImplementedException();
        public List<Usuario> ListaUsuariosPorAppId() => throw new NotImplementedException();
        public void InativaUsuario() => throw new NotImplementedException();
    }
}
'@

"Class1.cs" = @'
namespace CMSXRepo
{
}
'@

}

foreach ($name in $files.Keys) {
    $path = Join-Path $base $name
    Set-Content $path $files[$name] -NoNewline
    Write-Host "Reescrito: $name"
}

Write-Host "Concluido."
