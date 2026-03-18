

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------



DROP TABLE IF EXISTS aplicacao;

DROP TABLE IF EXISTS areas;

DROP TABLE IF EXISTS arquivo;

DROP TABLE IF EXISTS atributo;

DROP TABLE IF EXISTS cateria;

DROP TABLE IF EXISTS conteudo;

DROP TABLE IF EXISTS conteudovalor;

DROP TABLE IF EXISTS dict_socialmedia;

DROP TABLE IF EXISTS dict_templates;

DROP TABLE IF EXISTS dictareas;

DROP TABLE IF EXISTS formulario;

DROP TABLE IF EXISTS formularionew;

DROP TABLE IF EXISTS imagem;

DROP TABLE IF EXISTS modulo;

DROP TABLE IF EXISTS opcao;

DROP TABLE IF EXISTS produto;

DROP TABLE IF EXISTS refatributoxopcao;

DROP TABLE IF EXISTS relatributoxproduto;

DROP TABLE IF EXISTS relimagemconteudo;

DROP TABLE IF EXISTS socialmedia;

DROP TABLE IF EXISTS templates;

DROP TABLE IF EXISTS unidades;

DROP TABLE IF EXISTS usuario;

DROP TABLE IF EXISTS cambio;

DROP TABLE IF EXISTS ciaaerea;

DROP TABLE IF EXISTS infofoto;

DROP TABLE IF EXISTS informativo;

DROP TABLE IF EXISTS moduloconf;

DROP TABLE IF EXISTS moeda;

DROP TABLE IF EXISTS newsletter;

DROP TABLE IF EXISTS relmoduloaplicacao;

DROP TABLE IF EXISTS relmoduloconfaplicacao;

DROP TABLE IF EXISTS relusuarioaplicacao;

DROP TABLE IF EXISTS tipocotacao;

DROP TABLE IF EXISTS tipoenvio;


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'arquivo'
CREATE TABLE arquivo (
    ArquivoId varchar(64)  NOT NULL,
    AreaId varchar(64)  NULL,
    ConteudoId varchar(64)  NULL,
    Nome varchar(64)  NULL,
    TipoId varchar(64)  NULL
);


-- Creating table 'atributo'
CREATE TABLE atributo (
    AtributoId UUID  NOT NULL,
    Nome varchar(45)  NOT NULL,
    Descricao varchar(45)  NOT NULL,
    ProdutoId varchar(64)  NULL
);


-- Creating table 'conteudo'
CREATE TABLE conteudo (
    ConteudoId varchar(64)  NOT NULL,
    AreaId varchar(64)  NULL,
    Autor varchar(80)  NULL,
    Titulo varchar(80)  NULL,
    Texto text  NULL,
    DataInclusao timestamp  NULL,
    DataFinal timestamp  NULL,
    CateriaId varchar(64)  NULL
);


-- Creating table 'conteudovalor'
CREATE TABLE conteudovalor (
    ConteudoId varchar(64)  NOT NULL,
    UnidadeId UUID  NULL,
    Valor decimal(10,2)  NULL
);


-- Creating table 'dict_templates'
CREATE TABLE dict_templates (
    idTemplate varchar(100)  NOT NULL,
    Nome varchar(255)  NULL,
    Descricao varchar(255)  NULL,
    viewRelacionada varchar(45)  NULL
);


-- Creating table 'dictareas'
CREATE TABLE dictareas (
    id int  NOT NULL,
    nome varchar(12)  NULL,
    tipo int  NOT NULL
);


-- Creating table 'formulario'
CREATE TABLE formulario (
    formularioid varchar(64)  NOT NULL,
    nome varchar(255)  NOT NULL,
    valor varchar(8000)  NULL,
    ativo bit  NULL,
    datainclusao timestamp  NULL,
    areaid varchar(64)  NULL
);


-- Creating table 'formularionew'
CREATE TABLE formularionew (
    IdForm serial NOT NULL,
    Nome varchar(100)  NULL,
    Tipo int  NULL,
    Email varchar(155)  NULL,
    Telefone varchar(15)  NULL,
    Texto varchar(255)  NULL,
    Ativo int  NULL
);


-- Creating table 'imagem'
CREATE TABLE imagem (
    ImagemId varchar(64)  NOT NULL,
    Url varchar(300)  NULL,
    Largura int  NULL,
    Altura int  NULL,
    AreaId varchar(64)  NULL,
    ConteudoId varchar(64)  NULL,
    Descricao varchar(255)  NULL,
    ParentId varchar(64)  NOT NULL,
    TipoId varchar(64)  NOT NULL
);


-- Creating table 'modulo'
CREATE TABLE modulo (
    ModuloId varchar(64)  NOT NULL,
    Nome varchar(400)  NULL,
    Url varchar(400)  NULL,
    Posicao int  NULL
);


-- Creating table 'opcao'
CREATE TABLE opcao (
    OpcaoId varchar(64)  NOT NULL,
    AtributoId UUID  NOT NULL,
    Qtd int  NOT NULL,
    Nome varchar(500)  NULL,
    Descricao varchar(500)  NULL,
    Estoque int  NULL
);


-- Creating table 'produto'
CREATE TABLE produto (
    ProdutoId varchar(64)  NOT NULL,
    Nome varchar(130)  NULL,
    Descricao text  NULL,
    Valor decimal(18,2)  NULL,
    Tipo int  NULL,
    Destaque int  NULL,
    DataInicio timestamp  NULL,
    DataFinal timestamp  NULL,
    CateriaId varchar(64)  NULL,
    AplicacaoId varchar(64)  NULL,
    sku varchar(45)  NOT NULL,
    PagSeguroKey varchar(800)  NULL,
    DetalheTecnico text  NULL,
    DescricaCurta varchar(150)  NULL,
    produtocol varchar(45)  NULL
);


-- Creating table 'refatributoxopcao'
CREATE TABLE refatributoxopcao (
    relacaoid UUID  NOT NULL,
    atributoid UUID  NOT NULL,
    opcaoid UUID  NOT NULL
);


-- Creating table 'relatributoxproduto'
CREATE TABLE relatributoxproduto (
    Relacaoid UUID  NOT NULL,
    Atributoid UUID  NOT NULL,
    ProdutoId UUID  NOT NULL
);


-- Creating table 'unidades'
CREATE TABLE unidades (
    UnidadeId UUID  NOT NULL,
    Nome varchar(45)  NULL,
    Sigla varchar(45)  NULL
);


-- Creating table 'usuario'
CREATE TABLE usuario (
    UserId varchar(64)  NOT NULL,
    Nome varchar(300)  NOT NULL,
    Sobrenome varchar(300)  NOT NULL,
    Apelido varchar(6)  NULL,
    Senha varchar(12)  NULL,
    Ativo smallint  NULL,
    DataInclusao timestamp  NULL
);


-- Creating table 'cambio'
CREATE TABLE cambio (
    CambioGroupId varchar(64)  NOT NULL,
    DataCotacao timestamp  NULL,
    MoedasXml varchar(1000)  NULL,
    Tipo smallint  NULL
);


-- Creating table 'cateria'
CREATE TABLE cateria (
    CateriaId varchar(64)  NOT NULL,
    Nome varchar(200)  NULL,
    Descricao varchar(1000)  NULL,
    TipoCateria int  NULL,
    CateriaIdPai varchar(64)  NULL,
    AplicacaoId varchar(64)  NULL
);


-- Creating table 'ciaaerea'
CREATE TABLE ciaaerea (
    CiaAereaId varchar(64)  NOT NULL,
    Descricao varchar(4000)  NULL,
    Lotipo varchar(300)  NULL,
    Descricao_Longa text  NULL,
    Ativo smallint  NULL,
    TipoNac smallint  NULL,
    TipoInt smallint  NULL,
    webticket_str varchar(450)  NULL
);


-- Creating table 'infofoto'
CREATE TABLE infofoto (
    FotoId varchar(64)  NOT NULL,
    FotoUrl varchar(300)  NULL,
    Descricao varchar(1000)  NULL,
    CateriaId varchar(64)  NOT NULL
);


-- Creating table 'informativo'
CREATE TABLE informativo (
    InfoId varchar(64)  NOT NULL,
    Titulo varchar(300)  NULL,
    Data timestamp  NULL,
    Texto text  NULL,
    Foto varchar(64)  NULL,
    Ativo smallint  NULL,
    TipoEnvio varchar(64)  NULL
);


-- Creating table 'moduloconf'
CREATE TABLE moduloconf (
    ModuloConfId varchar(64)  NOT NULL,
    Descricao varchar(800)  NULL,
    Nome varchar(200)  NULL
);


-- Creating table 'moeda'
CREATE TABLE moeda (
    MoedaId varchar(64)  NOT NULL,
    Nome varchar(80)  NOT NULL,
    Sigla char(34)  NULL
);


-- Creating table 'newsletter'
CREATE TABLE newsletter (
    NewsId varchar(64)  NOT NULL,
    Titulo varchar(3000)  NULL,
    Autor varchar(1000)  NULL,
    Data timestamp  NULL,
    Frente smallint  NULL,
    Texto text  NULL,
    Foto varchar(4000)  NULL,
    CateriaId varchar(64)  NULL,
    Ativo smallint  NULL
);


-- Creating table 'relmoduloaplicacao'
CREATE TABLE relmoduloaplicacao (
    RelacaoId varchar(64)  NOT NULL,
    AplicacaoId varchar(64)  NOT NULL,
    ModuloId varchar(64)  NOT NULL
);


-- Creating table 'relmoduloconfaplicacao'
CREATE TABLE relmoduloconfaplicacao (
    RelacaoId varchar(64)  NOT NULL,
    ModuloConfId varchar(64)  NOT NULL,
    AplicacaoId varchar(64)  NOT NULL,
    DataInclusao timestamp  NOT NULL,
    DataFinalizacao timestamp  NOT NULL
);


-- Creating table 'relusuarioaplicacao'
CREATE TABLE relusuarioaplicacao (
    RelacaoId varchar(64)  NOT NULL,
    AplicacaoId varchar(64)  NOT NULL,
    UsuarioId varchar(64)  NOT NULL
);


-- Creating table 'tipocotacao'
CREATE TABLE tipocotacao (
    TipoCotacaoId varchar(64)  NOT NULL,
    Nome varchar(200)  NULL,
    Descricao varchar(200)  NULL
);


-- Creating table 'tipoenvio'
CREATE TABLE tipoenvio (
    TipoEnvioId varchar(64)  NOT NULL,
    TipoEnvioDesc varchar(300)  NULL
);


-- Creating table 'socialmedia'
CREATE TABLE socialmedia (
    SocialMediaId varchar(45)  NOT NULL,
    AplicacaoId varchar(45)  NULL,
    SocialMediaTypeId int  NULL,
    SocialMediaLink varchar(255)  NULL
);


-- Creating table 'dict_socialmedia'
CREATE TABLE dict_socialmedia (
    SocialMediaId int  NOT NULL,
    SocialMediaName varchar(45)  NULL,
    SocialMediaUrl varchar(45)  NULL
);


-- Creating table 'aplicacao'
CREATE TABLE aplicacao (
    AplicacaoId varchar(64)  NOT NULL,
    Nome varchar(400)  NULL,
    Url varchar(20)  NULL,
    DataInicio timestamp  NULL,
    DataFinal timestamp  NULL,
    IdUsuarioInicio varchar(36)  NULL,
    IdUsuarioFim varchar(36)  NULL,
    PagSeguroToken varchar(120)  NULL,
    LayoutChoose varchar(150)  NULL,
    Posicao int  NULL,
    mailUser varchar(150)  NULL,
    mailPassword varchar(45)  NULL,
    mailServer varchar(80)  NULL,
    mailPort int  NULL,
    isSecure smallint  NULL,
    pageFacebook varchar(255)  NULL,
    pageLinkedin varchar(255)  NULL,
    pageInstagram varchar(255)  NULL,
    pageTwitter varchar(255)  NULL,
    pagePinterest varchar(255)  NULL,
    pageFlicker varchar(255)  NULL,
    lotipo BYTEA  NULL,
    ogleAdSense varchar(500)  NULL,
    isActive bit  NULL,
    header varchar(245)  NULL
);


-- Creating table 'areas'
CREATE TABLE areas (
    AreaId varchar(64)  NOT NULL,
    AplicacaoId varchar(64)  NULL,
    Nome varchar(80)  NULL,
    Url varchar(300)  NULL,
    Descricao varchar(255)  NULL,
    AreaIdPai varchar(64)  NULL,
    DataInicial timestamp  NULL,
    DataFinal timestamp  NULL,
    Imagem smallint  NULL,
    MenuLateral smallint  NULL,
    MenuSplash smallint  NULL,
    MenuCentral smallint  NULL,
    posicao int  NULL,
    MenuFixo smallint  NULL,
    ListaSimples smallint  NULL,
    ListaSplash smallint  NULL,
    ListaBanner smallint  NULL,
    TipoArea int  NULL
);


-- Creating table 'relimagemconteudo'
CREATE TABLE relimagemconteudo (
    relid serial NOT NULL,
    imagemid varchar(45)  NULL,
    parentid varchar(45)  NULL
);


-- Creating table 'templates'
CREATE TABLE templates (
    id serial NOT NULL,
    Nome varchar(155)  NULL,
    Descricao varchar(255)  NULL,
    Url varchar(80)  NULL,
    Ativo bit  NULL
);


-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on ArquivoId in table 'arquivo'
ALTER TABLE arquivo
ADD CONSTRAINT PK_arquivo PRIMARY KEY (ArquivoId);
CLUSTER arquivo USING PK_arquivo;


-- Creating primary key on AtributoId in table 'atributo'
ALTER TABLE atributo
ADD CONSTRAINT PK_atributo PRIMARY KEY (AtributoId);
CLUSTER atributo USING PK_atributo;


-- Creating primary key on ConteudoId in table 'conteudo'
ALTER TABLE conteudo
ADD CONSTRAINT PK_conteudo PRIMARY KEY (ConteudoId);
CLUSTER conteudo USING PK_conteudo;


-- Creating primary key on ConteudoId in table 'conteudovalor'
ALTER TABLE conteudovalor
ADD CONSTRAINT PK_conteudovalor PRIMARY KEY (ConteudoId);
CLUSTER conteudovalor USING PK_conteudovalor;


-- Creating primary key on idTemplate in table 'dict_templates'
ALTER TABLE dict_templates
ADD CONSTRAINT PK_dict_templates PRIMARY KEY (idTemplate);
CLUSTER dict_templates USING PK_dict_templates;


-- Creating primary key on id in table 'dictareas'
ALTER TABLE dictareas
ADD CONSTRAINT PK_dictareas PRIMARY KEY (id);
CLUSTER dictareas USING PK_dictareas;


-- Creating primary key on formularioid, nome in table 'formulario'
ALTER TABLE formulario
ADD CONSTRAINT PK_formulario PRIMARY KEY (formularioid, nome);
CLUSTER formulario USING PK_formulario;


-- Creating primary key on IdForm in table 'formularionew'
ALTER TABLE formularionew
ADD CONSTRAINT PK_formularionew PRIMARY KEY (IdForm);
CLUSTER formularionew USING PK_formularionew;


-- Creating primary key on ImagemId in table 'imagem'
ALTER TABLE imagem
ADD CONSTRAINT PK_imagem PRIMARY KEY (ImagemId);
CLUSTER imagem USING PK_imagem;


-- Creating primary key on ModuloId in table 'modulo'
ALTER TABLE modulo
ADD CONSTRAINT PK_modulo PRIMARY KEY (ModuloId);
CLUSTER modulo USING PK_modulo;


-- Creating primary key on OpcaoId in table 'opcao'
ALTER TABLE opcao
ADD CONSTRAINT PK_opcao PRIMARY KEY (OpcaoId);
CLUSTER opcao USING PK_opcao;


-- Creating primary key on ProdutoId, sku in table 'produto'
ALTER TABLE produto
ADD CONSTRAINT PK_produto PRIMARY KEY (ProdutoId, sku);
CLUSTER produto USING PK_produto;


-- Creating primary key on relacaoid in table 'refatributoxopcao'
ALTER TABLE refatributoxopcao
ADD CONSTRAINT PK_refatributoxopcao PRIMARY KEY (relacaoid);
CLUSTER refatributoxopcao USING PK_refatributoxopcao;


-- Creating primary key on Relacaoid in table 'relatributoxproduto'
ALTER TABLE relatributoxproduto
ADD CONSTRAINT PK_relatributoxproduto PRIMARY KEY (Relacaoid);
CLUSTER relatributoxproduto USING PK_relatributoxproduto;


-- Creating primary key on UnidadeId in table 'unidades'
ALTER TABLE unidades
ADD CONSTRAINT PK_unidades PRIMARY KEY (UnidadeId);
CLUSTER unidades USING PK_unidades;


-- Creating primary key on UserId in table 'usuario'
ALTER TABLE usuario
ADD CONSTRAINT PK_usuario PRIMARY KEY (UserId);
CLUSTER usuario USING PK_usuario;


-- Creating primary key on CambioGroupId in table 'cambio'
ALTER TABLE cambio
ADD CONSTRAINT PK_cambio PRIMARY KEY (CambioGroupId);
CLUSTER cambio USING PK_cambio;


-- Creating primary key on CateriaId in table 'cateria'
ALTER TABLE cateria
ADD CONSTRAINT PK_cateria PRIMARY KEY (CateriaId);
CLUSTER cateria USING PK_cateria;


-- Creating primary key on CiaAereaId in table 'ciaaerea'
ALTER TABLE ciaaerea
ADD CONSTRAINT PK_ciaaerea PRIMARY KEY (CiaAereaId);
CLUSTER ciaaerea USING PK_ciaaerea;


-- Creating primary key on FotoId, CateriaId in table 'infofoto'
ALTER TABLE infofoto
ADD CONSTRAINT PK_infofoto PRIMARY KEY (FotoId, CateriaId);
CLUSTER infofoto USING PK_infofoto;


-- Creating primary key on InfoId in table 'informativo'
ALTER TABLE informativo
ADD CONSTRAINT PK_informativo PRIMARY KEY (InfoId);
CLUSTER informativo USING PK_informativo;


-- Creating primary key on ModuloConfId in table 'moduloconf'
ALTER TABLE moduloconf
ADD CONSTRAINT PK_moduloconf PRIMARY KEY (ModuloConfId);
CLUSTER moduloconf USING PK_moduloconf;


-- Creating primary key on MoedaId, Nome in table 'moeda'
ALTER TABLE moeda
ADD CONSTRAINT PK_moeda PRIMARY KEY (MoedaId, Nome);
CLUSTER moeda USING PK_moeda;


-- Creating primary key on NewsId in table 'newsletter'
ALTER TABLE newsletter
ADD CONSTRAINT PK_newsletter PRIMARY KEY (NewsId);
CLUSTER newsletter USING PK_newsletter;


-- Creating primary key on RelacaoId, AplicacaoId, ModuloId in table 'relmoduloaplicacao'
ALTER TABLE relmoduloaplicacao
ADD CONSTRAINT PK_relmoduloaplicacao PRIMARY KEY (RelacaoId, AplicacaoId, ModuloId);
CLUSTER relmoduloaplicacao USING PK_relmoduloaplicacao;


-- Creating primary key on RelacaoId, ModuloConfId, AplicacaoId, DataInclusao, DataFinalizacao in table 'relmoduloconfaplicacao'
ALTER TABLE relmoduloconfaplicacao
ADD CONSTRAINT PK_relmoduloconfaplicacao PRIMARY KEY (RelacaoId, ModuloConfId, AplicacaoId, DataInclusao, DataFinalizacao);
CLUSTER relmoduloconfaplicacao USING PK_relmoduloconfaplicacao;


-- Creating primary key on RelacaoId, AplicacaoId, UsuarioId in table 'relusuarioaplicacao'
ALTER TABLE relusuarioaplicacao
ADD CONSTRAINT PK_relusuarioaplicacao PRIMARY KEY (RelacaoId, AplicacaoId, UsuarioId);
CLUSTER relusuarioaplicacao USING PK_relusuarioaplicacao;


-- Creating primary key on TipoCotacaoId in table 'tipocotacao'
ALTER TABLE tipocotacao
ADD CONSTRAINT PK_tipocotacao PRIMARY KEY (TipoCotacaoId);
CLUSTER tipocotacao USING PK_tipocotacao;


-- Creating primary key on TipoEnvioId in table 'tipoenvio'
ALTER TABLE tipoenvio
ADD CONSTRAINT PK_tipoenvio PRIMARY KEY (TipoEnvioId);
CLUSTER tipoenvio USING PK_tipoenvio;


-- Creating primary key on SocialMediaId in table 'socialmedia'
ALTER TABLE socialmedia
ADD CONSTRAINT PK_socialmedia PRIMARY KEY (SocialMediaId);
CLUSTER socialmedia USING PK_socialmedia;


-- Creating primary key on SocialMediaId in table 'dict_socialmedia'
ALTER TABLE dict_socialmedia
ADD CONSTRAINT PK_dict_socialmedia PRIMARY KEY (SocialMediaId);
CLUSTER dict_socialmedia USING PK_dict_socialmedia;


-- Creating primary key on AplicacaoId in table 'aplicacao'
ALTER TABLE aplicacao
ADD CONSTRAINT PK_aplicacao PRIMARY KEY (AplicacaoId);
CLUSTER aplicacao USING PK_aplicacao;


-- Creating primary key on AreaId in table 'areas'
ALTER TABLE areas
ADD CONSTRAINT PK_areas PRIMARY KEY (AreaId);
CLUSTER areas USING PK_areas;


-- Creating primary key on relid in table 'relimagemconteudo'
ALTER TABLE relimagemconteudo
ADD CONSTRAINT PK_relimagemconteudo PRIMARY KEY (relid);
CLUSTER relimagemconteudo USING PK_relimagemconteudo;


-- Creating primary key on id in table 'templates'
ALTER TABLE templates
ADD CONSTRAINT PK_templates PRIMARY KEY (id);
CLUSTER templates USING PK_templates;


-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------