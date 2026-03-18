
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/24/2021 16:25:01
-- Generated from EDMX file: D:\Developer\CMSX\CMSX\CMSXEF\cmsxDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [cmsxDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[db_9b3a78_cmsxdb].[aplicacao]', 'U') IS NOT NULL
    DROP TABLE [db_9b3a78_cmsxdb].[aplicacao];
GO
IF OBJECT_ID(N'[db_9b3a78_cmsxdb].[areas]', 'U') IS NOT NULL
    DROP TABLE [db_9b3a78_cmsxdb].[areas];
GO
IF OBJECT_ID(N'[db_9b3a78_cmsxdb].[arquivo]', 'U') IS NOT NULL
    DROP TABLE [db_9b3a78_cmsxdb].[arquivo];
GO
IF OBJECT_ID(N'[db_9b3a78_cmsxdb].[atributo]', 'U') IS NOT NULL
    DROP TABLE [db_9b3a78_cmsxdb].[atributo];
GO
IF OBJECT_ID(N'[db_9b3a78_cmsxdb].[categoria]', 'U') IS NOT NULL
    DROP TABLE [db_9b3a78_cmsxdb].[categoria];
GO
IF OBJECT_ID(N'[db_9b3a78_cmsxdb].[conteudo]', 'U') IS NOT NULL
    DROP TABLE [db_9b3a78_cmsxdb].[conteudo];
GO
IF OBJECT_ID(N'[db_9b3a78_cmsxdb].[conteudovalor]', 'U') IS NOT NULL
    DROP TABLE [db_9b3a78_cmsxdb].[conteudovalor];
GO
IF OBJECT_ID(N'[db_9b3a78_cmsxdb].[dict_socialmedia]', 'U') IS NOT NULL
    DROP TABLE [db_9b3a78_cmsxdb].[dict_socialmedia];
GO
IF OBJECT_ID(N'[db_9b3a78_cmsxdb].[dict_templates]', 'U') IS NOT NULL
    DROP TABLE [db_9b3a78_cmsxdb].[dict_templates];
GO
IF OBJECT_ID(N'[db_9b3a78_cmsxdb].[dictareas]', 'U') IS NOT NULL
    DROP TABLE [db_9b3a78_cmsxdb].[dictareas];
GO
IF OBJECT_ID(N'[db_9b3a78_cmsxdb].[formulario]', 'U') IS NOT NULL
    DROP TABLE [db_9b3a78_cmsxdb].[formulario];
GO
IF OBJECT_ID(N'[db_9b3a78_cmsxdb].[formularionew]', 'U') IS NOT NULL
    DROP TABLE [db_9b3a78_cmsxdb].[formularionew];
GO
IF OBJECT_ID(N'[db_9b3a78_cmsxdb].[imagem]', 'U') IS NOT NULL
    DROP TABLE [db_9b3a78_cmsxdb].[imagem];
GO
IF OBJECT_ID(N'[db_9b3a78_cmsxdb].[modulo]', 'U') IS NOT NULL
    DROP TABLE [db_9b3a78_cmsxdb].[modulo];
GO
IF OBJECT_ID(N'[db_9b3a78_cmsxdb].[opcao]', 'U') IS NOT NULL
    DROP TABLE [db_9b3a78_cmsxdb].[opcao];
GO
IF OBJECT_ID(N'[db_9b3a78_cmsxdb].[produto]', 'U') IS NOT NULL
    DROP TABLE [db_9b3a78_cmsxdb].[produto];
GO
IF OBJECT_ID(N'[db_9b3a78_cmsxdb].[refatributoxopcao]', 'U') IS NOT NULL
    DROP TABLE [db_9b3a78_cmsxdb].[refatributoxopcao];
GO
IF OBJECT_ID(N'[db_9b3a78_cmsxdb].[relatributoxproduto]', 'U') IS NOT NULL
    DROP TABLE [db_9b3a78_cmsxdb].[relatributoxproduto];
GO
IF OBJECT_ID(N'[db_9b3a78_cmsxdb].[relimagemconteudo]', 'U') IS NOT NULL
    DROP TABLE [db_9b3a78_cmsxdb].[relimagemconteudo];
GO
IF OBJECT_ID(N'[db_9b3a78_cmsxdb].[socialmedia]', 'U') IS NOT NULL
    DROP TABLE [db_9b3a78_cmsxdb].[socialmedia];
GO
IF OBJECT_ID(N'[db_9b3a78_cmsxdb].[templates]', 'U') IS NOT NULL
    DROP TABLE [db_9b3a78_cmsxdb].[templates];
GO
IF OBJECT_ID(N'[db_9b3a78_cmsxdb].[unidades]', 'U') IS NOT NULL
    DROP TABLE [db_9b3a78_cmsxdb].[unidades];
GO
IF OBJECT_ID(N'[db_9b3a78_cmsxdb].[usuario]', 'U') IS NOT NULL
    DROP TABLE [db_9b3a78_cmsxdb].[usuario];
GO
IF OBJECT_ID(N'[cmsxDBModelStoreContainer].[cambio]', 'U') IS NOT NULL
    DROP TABLE [cmsxDBModelStoreContainer].[cambio];
GO
IF OBJECT_ID(N'[cmsxDBModelStoreContainer].[ciaaerea]', 'U') IS NOT NULL
    DROP TABLE [cmsxDBModelStoreContainer].[ciaaerea];
GO
IF OBJECT_ID(N'[cmsxDBModelStoreContainer].[infofoto]', 'U') IS NOT NULL
    DROP TABLE [cmsxDBModelStoreContainer].[infofoto];
GO
IF OBJECT_ID(N'[cmsxDBModelStoreContainer].[informativo]', 'U') IS NOT NULL
    DROP TABLE [cmsxDBModelStoreContainer].[informativo];
GO
IF OBJECT_ID(N'[cmsxDBModelStoreContainer].[moduloconf]', 'U') IS NOT NULL
    DROP TABLE [cmsxDBModelStoreContainer].[moduloconf];
GO
IF OBJECT_ID(N'[cmsxDBModelStoreContainer].[moeda]', 'U') IS NOT NULL
    DROP TABLE [cmsxDBModelStoreContainer].[moeda];
GO
IF OBJECT_ID(N'[cmsxDBModelStoreContainer].[newsletter]', 'U') IS NOT NULL
    DROP TABLE [cmsxDBModelStoreContainer].[newsletter];
GO
IF OBJECT_ID(N'[cmsxDBModelStoreContainer].[relmoduloaplicacao]', 'U') IS NOT NULL
    DROP TABLE [cmsxDBModelStoreContainer].[relmoduloaplicacao];
GO
IF OBJECT_ID(N'[cmsxDBModelStoreContainer].[relmoduloconfaplicacao]', 'U') IS NOT NULL
    DROP TABLE [cmsxDBModelStoreContainer].[relmoduloconfaplicacao];
GO
IF OBJECT_ID(N'[cmsxDBModelStoreContainer].[relusuarioaplicacao]', 'U') IS NOT NULL
    DROP TABLE [cmsxDBModelStoreContainer].[relusuarioaplicacao];
GO
IF OBJECT_ID(N'[cmsxDBModelStoreContainer].[tipocotacao]', 'U') IS NOT NULL
    DROP TABLE [cmsxDBModelStoreContainer].[tipocotacao];
GO
IF OBJECT_ID(N'[cmsxDBModelStoreContainer].[tipoenvio]', 'U') IS NOT NULL
    DROP TABLE [cmsxDBModelStoreContainer].[tipoenvio];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'arquivo'
CREATE TABLE [dbo].[arquivo] (
    [ArquivoId] varchar(64)  NOT NULL,
    [AreaId] varchar(64)  NULL,
    [ConteudoId] varchar(64)  NULL,
    [Nome] varchar(64)  NULL,
    [TipoId] varchar(64)  NULL
);
GO

-- Creating table 'atributo'
CREATE TABLE [dbo].[atributo] (
    [AtributoId] uniqueidentifier  NOT NULL,
    [Nome] varchar(45)  NOT NULL,
    [Descricao] varchar(45)  NOT NULL,
    [ProdutoId] varchar(64)  NULL
);
GO

-- Creating table 'conteudo'
CREATE TABLE [dbo].[conteudo] (
    [ConteudoId] varchar(64)  NOT NULL,
    [AreaId] varchar(64)  NULL,
    [Autor] varchar(80)  NULL,
    [Titulo] varchar(80)  NULL,
    [Texto] varchar(max)  NULL,
    [DataInclusao] datetime  NULL,
    [DataFinal] datetime  NULL,
    [CategoriaId] varchar(64)  NULL
);
GO

-- Creating table 'conteudovalor'
CREATE TABLE [dbo].[conteudovalor] (
    [ConteudoId] varchar(64)  NOT NULL,
    [UnidadeId] uniqueidentifier  NULL,
    [Valor] decimal(10,2)  NULL
);
GO

-- Creating table 'dict_templates'
CREATE TABLE [dbo].[dict_templates] (
    [idTemplate] varchar(100)  NOT NULL,
    [Nome] varchar(255)  NULL,
    [Descricao] varchar(255)  NULL,
    [viewRelacionada] varchar(45)  NULL
);
GO

-- Creating table 'dictareas'
CREATE TABLE [dbo].[dictareas] (
    [id] int  NOT NULL,
    [nome] varchar(12)  NULL,
    [tipo] int  NOT NULL
);
GO

-- Creating table 'formulario'
CREATE TABLE [dbo].[formulario] (
    [formularioid] varchar(64)  NOT NULL,
    [nome] varchar(255)  NOT NULL,
    [valor] varchar(8000)  NULL,
    [ativo] bit  NULL,
    [datainclusao] datetime  NULL,
    [areaid] varchar(64)  NULL
);
GO

-- Creating table 'formularionew'
CREATE TABLE [dbo].[formularionew] (
    [IdForm] int IDENTITY(1,1) NOT NULL,
    [Nome] varchar(100)  NULL,
    [Tipo] int  NULL,
    [Email] varchar(155)  NULL,
    [Telefone] varchar(15)  NULL,
    [Texto] varchar(255)  NULL,
    [Ativo] int  NULL
);
GO

-- Creating table 'imagem'
CREATE TABLE [dbo].[imagem] (
    [ImagemId] varchar(64)  NOT NULL,
    [Url] varchar(300)  NULL,
    [Largura] int  NULL,
    [Altura] int  NULL,
    [AreaId] varchar(64)  NULL,
    [ConteudoId] varchar(64)  NULL,
    [Descricao] varchar(255)  NULL,
    [ParentId] varchar(64)  NOT NULL,
    [TipoId] varchar(64)  NOT NULL
);
GO

-- Creating table 'modulo'
CREATE TABLE [dbo].[modulo] (
    [ModuloId] varchar(64)  NOT NULL,
    [Nome] varchar(400)  NULL,
    [Url] varchar(400)  NULL,
    [Posicao] int  NULL
);
GO

-- Creating table 'opcao'
CREATE TABLE [dbo].[opcao] (
    [OpcaoId] varchar(64)  NOT NULL,
    [AtributoId] uniqueidentifier  NOT NULL,
    [Qtd] int  NOT NULL,
    [Nome] varchar(500)  NULL,
    [Descricao] varchar(500)  NULL,
    [Estoque] int  NULL
);
GO

-- Creating table 'produto'
CREATE TABLE [dbo].[produto] (
    [ProdutoId] varchar(64)  NOT NULL,
    [Nome] varchar(130)  NULL,
    [Descricao] varchar(max)  NULL,
    [Valor] decimal(18,2)  NULL,
    [Tipo] int  NULL,
    [Destaque] int  NULL,
    [DataInicio] datetime  NULL,
    [DataFinal] datetime  NULL,
    [CategoriaId] varchar(64)  NULL,
    [AplicacaoId] varchar(64)  NULL,
    [sku] varchar(45)  NOT NULL,
    [PagSeguroKey] varchar(800)  NULL,
    [DetalheTecnico] varchar(max)  NULL,
    [DescricaCurta] varchar(150)  NULL,
    [produtocol] varchar(45)  NULL
);
GO

-- Creating table 'refatributoxopcao'
CREATE TABLE [dbo].[refatributoxopcao] (
    [relacaoid] uniqueidentifier  NOT NULL,
    [atributoid] uniqueidentifier  NOT NULL,
    [opcaoid] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'relatributoxproduto'
CREATE TABLE [dbo].[relatributoxproduto] (
    [Relacaoid] uniqueidentifier  NOT NULL,
    [Atributoid] uniqueidentifier  NOT NULL,
    [ProdutoId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'unidades'
CREATE TABLE [dbo].[unidades] (
    [UnidadeId] uniqueidentifier  NOT NULL,
    [Nome] varchar(45)  NULL,
    [Sigla] varchar(45)  NULL
);
GO

-- Creating table 'usuario'
CREATE TABLE [dbo].[usuario] (
    [UserId] varchar(64)  NOT NULL,
    [Nome] varchar(300)  NOT NULL,
    [Sobrenome] varchar(300)  NOT NULL,
    [Apelido] varchar(6)  NULL,
    [Senha] varchar(12)  NULL,
    [Ativo] tinyint  NULL,
    [DataInclusao] datetime  NULL
);
GO

-- Creating table 'cambio'
CREATE TABLE [dbo].[cambio] (
    [CambioGroupId] varchar(64)  NOT NULL,
    [DataCotacao] datetime  NULL,
    [MoedasXml] varchar(1000)  NULL,
    [Tipo] tinyint  NULL
);
GO

-- Creating table 'categoria'
CREATE TABLE [dbo].[categoria] (
    [CategoriaId] varchar(64)  NOT NULL,
    [Nome] varchar(200)  NULL,
    [Descricao] varchar(1000)  NULL,
    [TipoCategoria] int  NULL,
    [CategoriaIdPai] varchar(64)  NULL,
    [AplicacaoId] varchar(64)  NULL
);
GO

-- Creating table 'ciaaerea'
CREATE TABLE [dbo].[ciaaerea] (
    [CiaAereaId] varchar(64)  NOT NULL,
    [Descricao] varchar(4000)  NULL,
    [Logotipo] varchar(300)  NULL,
    [Descricao_Longa] varchar(max)  NULL,
    [Ativo] tinyint  NULL,
    [TipoNac] tinyint  NULL,
    [TipoInt] tinyint  NULL,
    [webticket_str] varchar(450)  NULL
);
GO

-- Creating table 'infofoto'
CREATE TABLE [dbo].[infofoto] (
    [FotoId] varchar(64)  NOT NULL,
    [FotoUrl] varchar(300)  NULL,
    [Descricao] varchar(1000)  NULL,
    [CategoriaId] varchar(64)  NOT NULL
);
GO

-- Creating table 'informativo'
CREATE TABLE [dbo].[informativo] (
    [InfoId] varchar(64)  NOT NULL,
    [Titulo] varchar(300)  NULL,
    [Data] datetime  NULL,
    [Texto] varchar(max)  NULL,
    [Foto] varchar(64)  NULL,
    [Ativo] tinyint  NULL,
    [TipoEnvio] varchar(64)  NULL
);
GO

-- Creating table 'moduloconf'
CREATE TABLE [dbo].[moduloconf] (
    [ModuloConfId] varchar(64)  NOT NULL,
    [Descricao] varchar(800)  NULL,
    [Nome] varchar(200)  NULL
);
GO

-- Creating table 'moeda'
CREATE TABLE [dbo].[moeda] (
    [MoedaId] varchar(64)  NOT NULL,
    [Nome] varchar(80)  NOT NULL,
    [Sigla] char(34)  NULL
);
GO

-- Creating table 'newsletter'
CREATE TABLE [dbo].[newsletter] (
    [NewsId] varchar(64)  NOT NULL,
    [Titulo] varchar(3000)  NULL,
    [Autor] varchar(1000)  NULL,
    [Data] datetime  NULL,
    [Frente] tinyint  NULL,
    [Texto] varchar(max)  NULL,
    [Foto] varchar(4000)  NULL,
    [CategoriaId] varchar(64)  NULL,
    [Ativo] tinyint  NULL
);
GO

-- Creating table 'relmoduloaplicacao'
CREATE TABLE [dbo].[relmoduloaplicacao] (
    [RelacaoId] varchar(64)  NOT NULL,
    [AplicacaoId] varchar(64)  NOT NULL,
    [ModuloId] varchar(64)  NOT NULL
);
GO

-- Creating table 'relmoduloconfaplicacao'
CREATE TABLE [dbo].[relmoduloconfaplicacao] (
    [RelacaoId] varchar(64)  NOT NULL,
    [ModuloConfId] varchar(64)  NOT NULL,
    [AplicacaoId] varchar(64)  NOT NULL,
    [DataInclusao] datetime  NOT NULL,
    [DataFinalizacao] datetime  NOT NULL
);
GO

-- Creating table 'relusuarioaplicacao'
CREATE TABLE [dbo].[relusuarioaplicacao] (
    [RelacaoId] varchar(64)  NOT NULL,
    [AplicacaoId] varchar(64)  NOT NULL,
    [UsuarioId] varchar(64)  NOT NULL
);
GO

-- Creating table 'tipocotacao'
CREATE TABLE [dbo].[tipocotacao] (
    [TipoCotacaoId] varchar(64)  NOT NULL,
    [Nome] varchar(200)  NULL,
    [Descricao] varchar(200)  NULL
);
GO

-- Creating table 'tipoenvio'
CREATE TABLE [dbo].[tipoenvio] (
    [TipoEnvioId] varchar(64)  NOT NULL,
    [TipoEnvioDesc] varchar(300)  NULL
);
GO

-- Creating table 'socialmedia'
CREATE TABLE [dbo].[socialmedia] (
    [SocialMediaId] varchar(45)  NOT NULL,
    [AplicacaoId] varchar(45)  NULL,
    [SocialMediaTypeId] int  NULL,
    [SocialMediaLink] varchar(255)  NULL
);
GO

-- Creating table 'dict_socialmedia'
CREATE TABLE [dbo].[dict_socialmedia] (
    [SocialMediaId] int  NOT NULL,
    [SocialMediaName] varchar(45)  NULL,
    [SocialMediaUrl] varchar(45)  NULL
);
GO

-- Creating table 'aplicacao'
CREATE TABLE [dbo].[aplicacao] (
    [AplicacaoId] varchar(64)  NOT NULL,
    [Nome] varchar(400)  NULL,
    [Url] varchar(20)  NULL,
    [DataInicio] datetime  NULL,
    [DataFinal] datetime  NULL,
    [IdUsuarioInicio] varchar(36)  NULL,
    [IdUsuarioFim] varchar(36)  NULL,
    [PagSeguroToken] varchar(120)  NULL,
    [LayoutChoose] varchar(150)  NULL,
    [Posicao] int  NULL,
    [mailUser] varchar(150)  NULL,
    [mailPassword] varchar(45)  NULL,
    [mailServer] varchar(80)  NULL,
    [mailPort] int  NULL,
    [isSecure] tinyint  NULL,
    [pageFacebook] varchar(255)  NULL,
    [pageLinkedin] varchar(255)  NULL,
    [pageInstagram] varchar(255)  NULL,
    [pageTwitter] varchar(255)  NULL,
    [pagePinterest] varchar(255)  NULL,
    [pageFlicker] varchar(255)  NULL,
    [logotipo] varbinary(max)  NULL,
    [googleAdSense] varchar(500)  NULL,
    [isActive] bit  NULL,
    [header] varchar(245)  NULL
);
GO

-- Creating table 'areas'
CREATE TABLE [dbo].[areas] (
    [AreaId] varchar(64)  NOT NULL,
    [AplicacaoId] varchar(64)  NULL,
    [Nome] varchar(80)  NULL,
    [Url] varchar(300)  NULL,
    [Descricao] varchar(255)  NULL,
    [AreaIdPai] varchar(64)  NULL,
    [DataInicial] datetime  NULL,
    [DataFinal] datetime  NULL,
    [Imagem] tinyint  NULL,
    [MenuLateral] tinyint  NULL,
    [MenuSplash] tinyint  NULL,
    [MenuCentral] tinyint  NULL,
    [posicao] int  NULL,
    [MenuFixo] tinyint  NULL,
    [ListaSimples] tinyint  NULL,
    [ListaSplash] tinyint  NULL,
    [ListaBanner] tinyint  NULL,
    [TipoArea] int  NULL
);
GO

-- Creating table 'relimagemconteudo'
CREATE TABLE [dbo].[relimagemconteudo] (
    [relid] int IDENTITY(1,1) NOT NULL,
    [imagemid] varchar(45)  NULL,
    [parentid] varchar(45)  NULL
);
GO

-- Creating table 'templates'
CREATE TABLE [dbo].[templates] (
    [id] int IDENTITY(1,1) NOT NULL,
    [Nome] varchar(155)  NULL,
    [Descricao] varchar(255)  NULL,
    [Url] varchar(80)  NULL,
    [Ativo] bit  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ArquivoId] in table 'arquivo'
ALTER TABLE [dbo].[arquivo]
ADD CONSTRAINT [PK_arquivo]
    PRIMARY KEY CLUSTERED ([ArquivoId] ASC);
GO

-- Creating primary key on [AtributoId] in table 'atributo'
ALTER TABLE [dbo].[atributo]
ADD CONSTRAINT [PK_atributo]
    PRIMARY KEY CLUSTERED ([AtributoId] ASC);
GO

-- Creating primary key on [ConteudoId] in table 'conteudo'
ALTER TABLE [dbo].[conteudo]
ADD CONSTRAINT [PK_conteudo]
    PRIMARY KEY CLUSTERED ([ConteudoId] ASC);
GO

-- Creating primary key on [ConteudoId] in table 'conteudovalor'
ALTER TABLE [dbo].[conteudovalor]
ADD CONSTRAINT [PK_conteudovalor]
    PRIMARY KEY CLUSTERED ([ConteudoId] ASC);
GO

-- Creating primary key on [idTemplate] in table 'dict_templates'
ALTER TABLE [dbo].[dict_templates]
ADD CONSTRAINT [PK_dict_templates]
    PRIMARY KEY CLUSTERED ([idTemplate] ASC);
GO

-- Creating primary key on [id] in table 'dictareas'
ALTER TABLE [dbo].[dictareas]
ADD CONSTRAINT [PK_dictareas]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [formularioid], [nome] in table 'formulario'
ALTER TABLE [dbo].[formulario]
ADD CONSTRAINT [PK_formulario]
    PRIMARY KEY CLUSTERED ([formularioid], [nome] ASC);
GO

-- Creating primary key on [IdForm] in table 'formularionew'
ALTER TABLE [dbo].[formularionew]
ADD CONSTRAINT [PK_formularionew]
    PRIMARY KEY CLUSTERED ([IdForm] ASC);
GO

-- Creating primary key on [ImagemId] in table 'imagem'
ALTER TABLE [dbo].[imagem]
ADD CONSTRAINT [PK_imagem]
    PRIMARY KEY CLUSTERED ([ImagemId] ASC);
GO

-- Creating primary key on [ModuloId] in table 'modulo'
ALTER TABLE [dbo].[modulo]
ADD CONSTRAINT [PK_modulo]
    PRIMARY KEY CLUSTERED ([ModuloId] ASC);
GO

-- Creating primary key on [OpcaoId] in table 'opcao'
ALTER TABLE [dbo].[opcao]
ADD CONSTRAINT [PK_opcao]
    PRIMARY KEY CLUSTERED ([OpcaoId] ASC);
GO

-- Creating primary key on [ProdutoId], [sku] in table 'produto'
ALTER TABLE [dbo].[produto]
ADD CONSTRAINT [PK_produto]
    PRIMARY KEY CLUSTERED ([ProdutoId], [sku] ASC);
GO

-- Creating primary key on [relacaoid] in table 'refatributoxopcao'
ALTER TABLE [dbo].[refatributoxopcao]
ADD CONSTRAINT [PK_refatributoxopcao]
    PRIMARY KEY CLUSTERED ([relacaoid] ASC);
GO

-- Creating primary key on [Relacaoid] in table 'relatributoxproduto'
ALTER TABLE [dbo].[relatributoxproduto]
ADD CONSTRAINT [PK_relatributoxproduto]
    PRIMARY KEY CLUSTERED ([Relacaoid] ASC);
GO

-- Creating primary key on [UnidadeId] in table 'unidades'
ALTER TABLE [dbo].[unidades]
ADD CONSTRAINT [PK_unidades]
    PRIMARY KEY CLUSTERED ([UnidadeId] ASC);
GO

-- Creating primary key on [UserId] in table 'usuario'
ALTER TABLE [dbo].[usuario]
ADD CONSTRAINT [PK_usuario]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [CambioGroupId] in table 'cambio'
ALTER TABLE [dbo].[cambio]
ADD CONSTRAINT [PK_cambio]
    PRIMARY KEY CLUSTERED ([CambioGroupId] ASC);
GO

-- Creating primary key on [CategoriaId] in table 'categoria'
ALTER TABLE [dbo].[categoria]
ADD CONSTRAINT [PK_categoria]
    PRIMARY KEY CLUSTERED ([CategoriaId] ASC);
GO

-- Creating primary key on [CiaAereaId] in table 'ciaaerea'
ALTER TABLE [dbo].[ciaaerea]
ADD CONSTRAINT [PK_ciaaerea]
    PRIMARY KEY CLUSTERED ([CiaAereaId] ASC);
GO

-- Creating primary key on [FotoId], [CategoriaId] in table 'infofoto'
ALTER TABLE [dbo].[infofoto]
ADD CONSTRAINT [PK_infofoto]
    PRIMARY KEY CLUSTERED ([FotoId], [CategoriaId] ASC);
GO

-- Creating primary key on [InfoId] in table 'informativo'
ALTER TABLE [dbo].[informativo]
ADD CONSTRAINT [PK_informativo]
    PRIMARY KEY CLUSTERED ([InfoId] ASC);
GO

-- Creating primary key on [ModuloConfId] in table 'moduloconf'
ALTER TABLE [dbo].[moduloconf]
ADD CONSTRAINT [PK_moduloconf]
    PRIMARY KEY CLUSTERED ([ModuloConfId] ASC);
GO

-- Creating primary key on [MoedaId], [Nome] in table 'moeda'
ALTER TABLE [dbo].[moeda]
ADD CONSTRAINT [PK_moeda]
    PRIMARY KEY CLUSTERED ([MoedaId], [Nome] ASC);
GO

-- Creating primary key on [NewsId] in table 'newsletter'
ALTER TABLE [dbo].[newsletter]
ADD CONSTRAINT [PK_newsletter]
    PRIMARY KEY CLUSTERED ([NewsId] ASC);
GO

-- Creating primary key on [RelacaoId], [AplicacaoId], [ModuloId] in table 'relmoduloaplicacao'
ALTER TABLE [dbo].[relmoduloaplicacao]
ADD CONSTRAINT [PK_relmoduloaplicacao]
    PRIMARY KEY CLUSTERED ([RelacaoId], [AplicacaoId], [ModuloId] ASC);
GO

-- Creating primary key on [RelacaoId], [ModuloConfId], [AplicacaoId], [DataInclusao], [DataFinalizacao] in table 'relmoduloconfaplicacao'
ALTER TABLE [dbo].[relmoduloconfaplicacao]
ADD CONSTRAINT [PK_relmoduloconfaplicacao]
    PRIMARY KEY CLUSTERED ([RelacaoId], [ModuloConfId], [AplicacaoId], [DataInclusao], [DataFinalizacao] ASC);
GO

-- Creating primary key on [RelacaoId], [AplicacaoId], [UsuarioId] in table 'relusuarioaplicacao'
ALTER TABLE [dbo].[relusuarioaplicacao]
ADD CONSTRAINT [PK_relusuarioaplicacao]
    PRIMARY KEY CLUSTERED ([RelacaoId], [AplicacaoId], [UsuarioId] ASC);
GO

-- Creating primary key on [TipoCotacaoId] in table 'tipocotacao'
ALTER TABLE [dbo].[tipocotacao]
ADD CONSTRAINT [PK_tipocotacao]
    PRIMARY KEY CLUSTERED ([TipoCotacaoId] ASC);
GO

-- Creating primary key on [TipoEnvioId] in table 'tipoenvio'
ALTER TABLE [dbo].[tipoenvio]
ADD CONSTRAINT [PK_tipoenvio]
    PRIMARY KEY CLUSTERED ([TipoEnvioId] ASC);
GO

-- Creating primary key on [SocialMediaId] in table 'socialmedia'
ALTER TABLE [dbo].[socialmedia]
ADD CONSTRAINT [PK_socialmedia]
    PRIMARY KEY CLUSTERED ([SocialMediaId] ASC);
GO

-- Creating primary key on [SocialMediaId] in table 'dict_socialmedia'
ALTER TABLE [dbo].[dict_socialmedia]
ADD CONSTRAINT [PK_dict_socialmedia]
    PRIMARY KEY CLUSTERED ([SocialMediaId] ASC);
GO

-- Creating primary key on [AplicacaoId] in table 'aplicacao'
ALTER TABLE [dbo].[aplicacao]
ADD CONSTRAINT [PK_aplicacao]
    PRIMARY KEY CLUSTERED ([AplicacaoId] ASC);
GO

-- Creating primary key on [AreaId] in table 'areas'
ALTER TABLE [dbo].[areas]
ADD CONSTRAINT [PK_areas]
    PRIMARY KEY CLUSTERED ([AreaId] ASC);
GO

-- Creating primary key on [relid] in table 'relimagemconteudo'
ALTER TABLE [dbo].[relimagemconteudo]
ADD CONSTRAINT [PK_relimagemconteudo]
    PRIMARY KEY CLUSTERED ([relid] ASC);
GO

-- Creating primary key on [id] in table 'templates'
ALTER TABLE [dbo].[templates]
ADD CONSTRAINT [PK_templates]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------