$base = "T:\Developer\RepositorioTrabalho\tecbakana\cmsx\CMSXRepo"

$files = @{

"AplicacaoRepositorio.cs" = @'
using System;
using System.Collections.Generic;
using System.Dynamic;
using CMSXData.Models;
using ICMSX;

namespace CMSXRepo
{
    public class AplicacaoRepositorio : BaseRepositorio, IAplicacaoRepositorio
    {
        private readonly IAplicacaoDAL _dal;

        public AplicacaoRepositorio(CmsxDbContext db, IAplicacaoDAL dal) : base(db) { _dal = dal; }

        public void MakeConnection(dynamic prop) => _dal.MakeConnection((ExpandoObject)prop);

        public Aplicacao ObtemAplicacaoPorId(Guid id) => throw new NotImplementedException();
        public Aplicacao RegistraAplicacao() => throw new NotImplementedException();
        public bool CriaAplicacao() => throw new NotImplementedException();
        public void ExcluiAplicacao() => throw new NotImplementedException();
        public void Edita() => throw new NotImplementedException();
        public string AtivaAplicacao() => throw new NotImplementedException();
        public List<Aplicacao> ListaAplicacao() => throw new NotImplementedException();
        public List<Aplicacao> ListaAplicacaoForAutocomplete() => throw new NotImplementedException();
        public string[] ListaAplicacaoPorNome() => throw new NotImplementedException();
    }
}
'@

"AreasRepositorio.cs" = @'
using System;
using System.Collections.Generic;
using System.Dynamic;
using CMSXData.Models;
using ICMSX;

namespace CMSXRepo
{
    public class AreasRepositorio : BaseRepositorio, IAreasRepositorio
    {
        private readonly IAreasDAL _dal;

        public AreasRepositorio(CmsxDbContext db, IAreasDAL dal) : base(db) { _dal = dal; }

        public void MakeConnection(dynamic prop) => _dal.MakeConnection((ExpandoObject)prop);

        public Area ObtemAreaPorId() => throw new NotImplementedException();
        public void CriaNovaArea() => throw new NotImplementedException();
        public string AreaRapida() => throw new NotImplementedException();
        public void EditaAreaPosicao() => throw new NotImplementedException();
        public List<Area> ListaAreas() => throw new NotImplementedException();
        public void InativaArea() => throw new NotImplementedException();
    }
}
'@

"AtributoRepositorio.cs" = @'
using System;
using System.Collections.Generic;
using System.Dynamic;
using CMSXData.Models;
using ICMSX;

namespace CMSXRepo
{
    public class AtributoRepositorio : BaseRepositorio, IAtributoRepositorio
    {
        private readonly IAtributoDAL _dal;

        public AtributoRepositorio(CmsxDbContext db, IAtributoDAL dal) : base(db) { _dal = dal; }

        public void MakeConnection(dynamic prop) => _dal.MakeConnection((ExpandoObject)prop);

        public List<Atributo> ListaAtributo() => throw new NotImplementedException();
        public List<Atributo> ListaAtributoXProduto() => throw new NotImplementedException();
        public void CriaAtributo(Atributo at) => throw new NotImplementedException();
        public void InativaAtributo() => throw new NotImplementedException();
    }
}
'@

"CategoriaRepositorio.cs" = @'
using System;
using System.Collections.Generic;
using System.Dynamic;
using CMSXData.Models;
using ICMSX;

namespace CMSXRepo
{
    public class CategoriaRepositorio : BaseRepositorio, ICategoriaRepositorio
    {
        private readonly ICategoriaDAL _dal;

        public CategoriaRepositorio(CmsxDbContext db, ICategoriaDAL dal) : base(db) { _dal = dal; }

        public void MakeConnection(dynamic prop) => _dal.MakeConnection((ExpandoObject)prop);

        public Caterium ObtemCategoriaPorId() => throw new NotImplementedException();
        public void CriaNovaCategoria() => throw new NotImplementedException();
        public List<Caterium> ListaCategoria() => throw new NotImplementedException();
        public List<Caterium> ListaCategoriaFull() => throw new NotImplementedException();
        public List<Caterium> ListaCategoriaPai() => throw new NotImplementedException();
        public List<Caterium> ListaSubCategoria() => throw new NotImplementedException();
        public void InativaCategorias() => throw new NotImplementedException();
    }
}
'@

"ConteudoRepositorio.cs" = @'
using System;
using System.Collections.Generic;
using System.Dynamic;
using CMSXData.Models;
using ICMSX;

namespace CMSXRepo
{
    public class ConteudoRepositorio : BaseRepositorio, IConteudoRepositorio
    {
        private readonly IConteudoDAL _dal;

        public ConteudoRepositorio(CmsxDbContext db, IConteudoDAL dal) : base(db) { _dal = dal; }

        public void MakeConnection(dynamic prop) => _dal.MakeConnection((ExpandoObject)prop);

        public List<Conteudo> ObtemConteudoPorId() => throw new NotImplementedException();
        public void CriaNovoConteudo(Conteudo conteudo) => throw new NotImplementedException();
        public void EditaConteudo(Conteudo conteudo) => throw new NotImplementedException();
        public void CreateContent() => throw new NotImplementedException();
        public void CreateValue() => throw new NotImplementedException();
        public void EditContent() => throw new NotImplementedException();
        public void EditValue() => throw new NotImplementedException();
        public List<Conteudo> ListaConteudoPorAreaId() => throw new NotImplementedException();
        public List<Conteudo> ListaConteudoPorAplicacaoId() => throw new NotImplementedException();
        public List<Conteudo> ListaValor() => throw new NotImplementedException();
        public void InativaConteudo() => throw new NotImplementedException();
    }
}
'@

"OpcaoRepositorio.cs" = @'
using System;
using System.Collections.Generic;
using System.Dynamic;
using CMSXData.Models;
using ICMSX;

namespace CMSXRepo
{
    public class OpcaoRepositorio : BaseRepositorio, IOpcaoRepositorio
    {
        private readonly IOpcaoDAL _dal;

        public OpcaoRepositorio(CmsxDbContext db, IOpcaoDAL dal) : base(db) { _dal = dal; }

        public void MakeConnection(dynamic prop) => _dal.MakeConnection((ExpandoObject)prop);

        public List<Opcao> ListaOpcao() => throw new NotImplementedException();
        public List<Opcao> ListaOpcaoXAtributo() => throw new NotImplementedException();
        public void CriaOpcao(Opcao op) => throw new NotImplementedException();
        public void InativaOpcao() => throw new NotImplementedException();
    }
}
'@

"ProdutoRepositorio.cs" = @'
using System;
using System.Collections.Generic;
using System.Dynamic;
using CMSXData.Models;
using ICMSX;

namespace CMSXRepo
{
    public class ProdutoRepositorio : BaseRepositorio, IProdutoRepositorio
    {
        private readonly IProdutoDAL _dal;

        public ProdutoRepositorio(CmsxDbContext db, IProdutoDAL dal) : base(db) { _dal = dal; }

        public void MakeConnection(dynamic prop) => _dal.MakeConnection((ExpandoObject)prop);

        public List<Produto> ListaProduto() => throw new NotImplementedException();
        public List<Produto> ListaProdutoXId() => throw new NotImplementedException();
        public List<Produto> ListaProdutoXCategoria() => throw new NotImplementedException();
        public List<Produto> ListaProdutoXTipo() => throw new NotImplementedException();
        public void CriaProduto(Produto prod) => throw new NotImplementedException();
        public void EditaProduto(Produto prod) => throw new NotImplementedException();
        public void InativaProduto(Produto prod) => throw new NotImplementedException();
    }
}
'@

}

foreach ($name in $files.Keys) {
    $path = Join-Path $base $name
    Set-Content $path $files[$name] -NoNewline
    Write-Host "Reescrito: $name"
}

Write-Host "Concluido."
