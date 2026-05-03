CREATE TABLE aplicacao (
    "AplicacaoId" character varying(64) NOT NULL,
    "Nome" character varying(400),
    "Url" character varying(20),
    "DataInicio" timestamp with time zone,
    "DataFinal" timestamp with time zone,
    "IdUsuarioInicio" character varying(36),
    "IdUsuarioFim" character varying(36),
    "PagSeguroToken" character varying(120),
    "LayoutChoose" character varying(150),
    "Posicao" integer,
    "mailUser" character varying(150),
    "mailPassword" character varying(45),
    "mailServer" character varying(80),
    "mailPort" integer,
    "isSecure" smallint,
    "pageFacebook" character varying(255),
    "pageLinkedin" character varying(255),
    "pageInstagram" character varying(255),
    "pageTwitter" character varying(255),
    "pagePinterest" character varying(255),
    "pageFlicker" character varying(255),
    logotipo bytea,
    "googleAdSense" character varying(500),
    header character varying(245),
    "isActive" boolean,
    "Telefone" text,
    "Endereco" text,
    "Descricao" text,
    "IsDemo" boolean NOT NULL,
    CONSTRAINT "PK_aplicacao" PRIMARY KEY ("AplicacaoId")
);


CREATE TABLE areas (
    "AreaId" character varying(64) NOT NULL,
    "AplicacaoId" character varying(64),
    "Nome" character varying(80),
    "Url" character varying(300),
    "Descricao" character varying(255),
    "AreaIdPai" character varying(64),
    "DataInicial" timestamp with time zone,
    "DataFinal" timestamp with time zone,
    "Imagem" smallint,
    "MenuLateral" smallint,
    "MenuSplash" smallint,
    "MenuCentral" smallint,
    posicao integer,
    "MenuFixo" smallint,
    "ListaSimples" smallint,
    "ListaSplash" smallint,
    "ListaBanner" smallint,
    "TipoArea" integer,
    layout text,
    "PageBuilderVersion" text,
    CONSTRAINT "PK_areas" PRIMARY KEY ("AreaId")
);


CREATE TABLE arquivo (
    "ArquivoId" character varying(64) NOT NULL,
    "AreaId" character varying(64),
    "ConteudoId" character varying(64),
    "Nome" character varying(64),
    "TipoId" character varying(64),
    CONSTRAINT "PK_arquivo" PRIMARY KEY ("ArquivoId")
);


CREATE TABLE atributo (
    "AtributoId" uuid NOT NULL,
    "Nome" character varying(45) NOT NULL,
    "Descricao" character varying(45) NOT NULL,
    "ProdutoId" character varying(64),
    "ParentAtributoId" uuid,
    "Ordem" integer,
    "ValorAdicional" numeric,
    CONSTRAINT "PK_atributo" PRIMARY KEY ("AtributoId"),
    CONSTRAINT "FK_Atributo_Parent" FOREIGN KEY ("ParentAtributoId") REFERENCES atributo ("AtributoId")
);


CREATE TABLE cambio (
    "CambioGroupId" character varying(64) NOT NULL,
    "DataCotacao" timestamp with time zone,
    "MoedasXml" character varying(1000),
    "Tipo" smallint,
    CONSTRAINT "PK_cambio" PRIMARY KEY ("CambioGroupId")
);


CREATE TABLE categoria (
    "CategoriaId" character varying(64) NOT NULL,
    "Nome" character varying(200),
    "Descricao" character varying(1000),
    "TipoCategoria" integer,
    "CategoriaIdPai" character varying(64),
    "AplicacaoId" character varying(64),
    CONSTRAINT "PK_categoria" PRIMARY KEY ("CategoriaId")
);


CREATE TABLE ciaaerea (
    "CiaAereaId" character varying(64) NOT NULL,
    "Descricao" character varying(4000),
    "Logotipo" character varying(300),
    "Descricao_Longa" text,
    "Ativo" smallint,
    "TipoNac" smallint,
    "TipoInt" smallint,
    webticket_str character varying(450),
    CONSTRAINT "PK_ciaaerea" PRIMARY KEY ("CiaAereaId")
);


CREATE TABLE conteudo (
    "ConteudoId" character varying(64) NOT NULL,
    "AreaId" character varying(64),
    "Autor" character varying(80),
    "Titulo" character varying(80),
    "Texto" text,
    "DataInclusao" timestamp with time zone,
    "DataFinal" timestamp with time zone,
    "CategoriaId" character varying(64),
    CONSTRAINT "PK_conteudo" PRIMARY KEY ("ConteudoId")
);


CREATE TABLE conteudovalor (
    "ConteudoId" character varying(64) NOT NULL,
    "UnidadeId" uuid,
    "Valor" numeric(10,2),
    CONSTRAINT "PK_conteudovalor" PRIMARY KEY ("ConteudoId")
);


CREATE TABLE dict_blocos (
    tipobloco character varying(50) NOT NULL,
    nome character varying(100) NOT NULL,
    descricao character varying(255),
    icone character varying(50),
    schema_config text NOT NULL,
    CONSTRAINT "PK_dict_blocos" PRIMARY KEY (tipobloco)
);


CREATE TABLE dict_socialmedia (
    "SocialMediaId" integer NOT NULL,
    "SocialMediaName" character varying(45),
    "SocialMediaUrl" character varying(45),
    CONSTRAINT "PK_dict_socialmedia" PRIMARY KEY ("SocialMediaId")
);


CREATE TABLE dict_templates (
    "idTemplate" character varying(100) NOT NULL,
    "Nome" character varying(255),
    "Descricao" character varying(255),
    "viewRelacionada" character varying(45),
    CONSTRAINT "PK_dict_templates" PRIMARY KEY ("idTemplate")
);


CREATE TABLE dictareas (
    id integer NOT NULL,
    nome character varying(12),
    tipo integer NOT NULL,
    CONSTRAINT "PK_dictareas" PRIMARY KEY (id)
);


CREATE TABLE faq (
    faqid character varying(64) NOT NULL,
    formularioid character varying(64) NOT NULL,
    pergunta character varying(500) NOT NULL,
    resposta text NOT NULL,
    ordem integer NOT NULL,
    ativo boolean NOT NULL,
    datainclusao timestamp with time zone NOT NULL,
    CONSTRAINT "PK_faq" PRIMARY KEY (faqid)
);


CREATE TABLE formulario (
    formularioid character varying(64) NOT NULL,
    nome character varying(255) NOT NULL,
    valor character varying(8000),
    ativo boolean,
    datainclusao timestamp with time zone,
    areaid character varying(64),
    categoriaid character varying(64),
    CONSTRAINT "PK_formulario" PRIMARY KEY (formularioid, nome)
);


CREATE TABLE formularionew (
    "IdForm" integer GENERATED BY DEFAULT AS IDENTITY,
    "Nome" character varying(100),
    "Tipo" integer,
    "Email" character varying(155),
    "Telefone" character varying(15),
    "Texto" character varying(255),
    "Ativo" integer,
    formularioid character varying(64),
    CONSTRAINT "PK_formularionew" PRIMARY KEY ("IdForm")
);


CREATE TABLE grupo (
    "GrupoId" text NOT NULL,
    "Nome" character varying(100) NOT NULL,
    "Descricao" character varying(255),
    "AcessoTotal" boolean NOT NULL,
    CONSTRAINT "PK_grupo" PRIMARY KEY ("GrupoId")
);


CREATE TABLE ia_cache (
    cacheid character varying(36) NOT NULL,
    hash character varying(64) NOT NULL,
    resultado text NOT NULL,
    datainclusao timestamp with time zone NOT NULL,
    datavencimento timestamp with time zone NOT NULL,
    CONSTRAINT "PK_ia_cache" PRIMARY KEY (cacheid)
);


CREATE TABLE ia_config (
    aplicacaoid character varying(50) NOT NULL,
    provedor character varying(20),
    apikey character varying(500),
    modelo character varying(100),
    limite_diario integer,
    CONSTRAINT "PK_ia_config" PRIMARY KEY (aplicacaoid)
);


CREATE TABLE ia_uso (
    usoid character varying(36) NOT NULL,
    aplicacaoid character varying(50) NOT NULL,
    data date NOT NULL,
    contador integer NOT NULL,
    CONSTRAINT "PK_ia_uso" PRIMARY KEY (usoid)
);


CREATE TABLE imagem (
    "ImagemId" character varying(64) NOT NULL,
    "Url" character varying(300),
    "Largura" integer,
    "Altura" integer,
    "AreaId" character varying(64),
    "ConteudoId" character varying(64),
    "Descricao" character varying(255),
    "ParentId" character varying(64) NOT NULL,
    "TipoId" character varying(64) NOT NULL,
    CONSTRAINT "PK_imagem" PRIMARY KEY ("ImagemId")
);


CREATE TABLE infofoto (
    "FotoId" character varying(64) NOT NULL,
    "CategoriaId" character varying(64) NOT NULL,
    "FotoUrl" character varying(300),
    "Descricao" character varying(1000),
    CONSTRAINT "PK_infofoto" PRIMARY KEY ("FotoId", "CategoriaId")
);


CREATE TABLE informativo (
    "InfoId" character varying(64) NOT NULL,
    "Titulo" character varying(300),
    "Data" timestamp with time zone,
    "Texto" text,
    "Foto" character varying(64),
    "Ativo" smallint,
    "TipoEnvio" character varying(64),
    CONSTRAINT "PK_informativo" PRIMARY KEY ("InfoId")
);


CREATE TABLE layout_template (
    templateid character varying(64) NOT NULL,
    nome character varying(100) NOT NULL,
    descricao character varying(255),
    tipo character varying(50),
    layout text,
    padrao boolean NOT NULL,
    datainclusao timestamp with time zone NOT NULL,
    CONSTRAINT "PK_layout_template" PRIMARY KEY (templateid)
);


CREATE TABLE modelocomposto (
    "ModeloCompostoId" uuid NOT NULL DEFAULT (NEWID()),
    "Aplicacaoid" character varying(64) NOT NULL,
    "Produtoid" character varying(64) NOT NULL,
    "Nome" character varying(500) NOT NULL,
    "ValorUnitario" numeric(18,2) NOT NULL,
    "ConfiguracaoHash" character varying(64) NOT NULL,
    "Usos" integer NOT NULL DEFAULT 1,
    "Datacriacao" timestamp with time zone NOT NULL DEFAULT (NOW()),
    CONSTRAINT "PK_modelocomposto" PRIMARY KEY ("ModeloCompostoId")
);


CREATE TABLE modulo (
    "ModuloId" character varying(64) NOT NULL,
    "Nome" character varying(400),
    "Url" character varying(400),
    "Posicao" integer,
    CONSTRAINT "PK_modulo" PRIMARY KEY ("ModuloId")
);


CREATE TABLE moduloconf (
    "ModuloConfId" character varying(64) NOT NULL,
    "Descricao" character varying(800),
    "Nome" character varying(200),
    CONSTRAINT "PK_moduloconf" PRIMARY KEY ("ModuloConfId")
);


CREATE TABLE moeda (
    "MoedaId" character varying(64) NOT NULL,
    "Nome" character varying(80) NOT NULL,
    "Sigla" character(34),
    CONSTRAINT "PK_moeda" PRIMARY KEY ("MoedaId", "Nome")
);


CREATE TABLE newsletter (
    "NewsId" character varying(64) NOT NULL,
    "Titulo" character varying(3000),
    "Autor" character varying(1000),
    "Data" timestamp with time zone,
    "Frente" smallint,
    "Texto" text,
    "Foto" character varying(4000),
    "CategoriaId" character varying(64),
    "Ativo" smallint,
    CONSTRAINT "PK_newsletter" PRIMARY KEY ("NewsId")
);


CREATE TABLE orcamentocabecalho (
    orcamentoid uuid NOT NULL DEFAULT (NEWID()),
    aplicacaoid character varying(64) NOT NULL,
    nome character varying(200) NOT NULL,
    email character varying(200),
    telefone character varying(50),
    descricaoservico text,
    valorestimado numeric(12,2),
    prazo character varying(200),
    nomevendedor character varying(200),
    aprovado boolean NOT NULL,
    datainclusao timestamp with time zone DEFAULT (NOW()),
    CONSTRAINT "PK_orcamentocabecalho" PRIMARY KEY (orcamentoid)
);


CREATE TABLE pedido (
    pedidoid uuid NOT NULL DEFAULT (NEWID()),
    aplicacaoid character varying(36),
    numeropedido character varying(100),
    clientenome character varying(200),
    clienteemail character varying(200),
    valorpedido numeric(12,2),
    statusatual character varying(50),
    metodopagamento character varying(50),
    datainclusao timestamp with time zone DEFAULT (NOW()),
    CONSTRAINT "pedidoPK" PRIMARY KEY (pedidoid)
);


CREATE TABLE produto (
    "ProdutoId" character varying(64) NOT NULL,
    sku character varying(45) NOT NULL,
    "Nome" character varying(130),
    "Descricao" text,
    "Valor" numeric(18,2),
    "Tipo" integer,
    "Destaque" integer,
    "DataInicio" timestamp with time zone,
    "DataFinal" timestamp with time zone,
    "CategoriaId" character varying(64),
    "AplicacaoId" character varying(64),
    "PagSeguroKey" character varying(800),
    "DetalheTecnico" text,
    "DescricaCurta" character varying(150),
    produtocol character varying(45),
    "SalematicProdutoid" integer,
    "UnidadeVenda" character varying(45),
    CONSTRAINT "PK_produto" PRIMARY KEY ("ProdutoId", sku),
    CONSTRAINT "AK_produto_ProdutoId" UNIQUE ("ProdutoId")
);


CREATE TABLE publictoken (
    publictokenid uuid NOT NULL DEFAULT (NEWID()),
    token character varying(100) NOT NULL,
    aplicacaoid character varying(64) NOT NULL,
    ativo boolean NOT NULL,
    datainclusao timestamp with time zone NOT NULL DEFAULT (NOW()),
    datavencimento timestamp with time zone,
    CONSTRAINT "PK_publictoken" PRIMARY KEY (publictokenid)
);


CREATE TABLE refatributoxopcao (
    relacaoid uuid NOT NULL,
    atributoid uuid NOT NULL,
    opcaoid uuid NOT NULL,
    CONSTRAINT "PK_refatributoxopcao" PRIMARY KEY (relacaoid)
);


CREATE TABLE relatributoxproduto (
    "Relacaoid" uuid NOT NULL,
    "Atributoid" uuid NOT NULL,
    "ProdutoId" uuid NOT NULL,
    CONSTRAINT "PK_relatributoxproduto" PRIMARY KEY ("Relacaoid")
);


CREATE TABLE relimagemconteudo (
    relid integer GENERATED BY DEFAULT AS IDENTITY,
    imagemid character varying(45),
    parentid character varying(45),
    CONSTRAINT "PK_relimagemconteudo" PRIMARY KEY (relid)
);


CREATE TABLE relmoduloaplicacao (
    "RelacaoId" character varying(64) NOT NULL,
    "AplicacaoId" character varying(64) NOT NULL,
    "ModuloId" character varying(64) NOT NULL,
    CONSTRAINT "PK_relmoduloaplicacao" PRIMARY KEY ("RelacaoId", "AplicacaoId", "ModuloId")
);


CREATE TABLE relmoduloconfaplicacao (
    "RelacaoId" character varying(64) NOT NULL,
    "ModuloConfId" character varying(64) NOT NULL,
    "AplicacaoId" character varying(64) NOT NULL,
    "DataInclusao" timestamp with time zone NOT NULL,
    "DataFinalizacao" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_relmoduloconfaplicacao" PRIMARY KEY ("RelacaoId", "ModuloConfId", "AplicacaoId", "DataInclusao", "DataFinalizacao")
);


CREATE TABLE relmodulousuario (
    "RelacaoId" character varying(64) NOT NULL,
    "ModuloId" character varying(64) NOT NULL,
    "UsuarioId" character varying(64) NOT NULL,
    CONSTRAINT "PK_relmodulousuario" PRIMARY KEY ("RelacaoId", "ModuloId", "UsuarioId")
);


CREATE TABLE relusuarioaplicacao (
    "RelacaoId" character varying(64) NOT NULL,
    "AplicacaoId" character varying(64) NOT NULL,
    "UsuarioId" character varying(64) NOT NULL,
    CONSTRAINT "PK_relusuarioaplicacao" PRIMARY KEY ("RelacaoId", "AplicacaoId", "UsuarioId")
);


CREATE TABLE relusuariogrupo (
    "RelacaoId" text NOT NULL,
    "UsuarioId" text NOT NULL,
    "GrupoId" text NOT NULL,
    CONSTRAINT "PK_relusuariogrupo" PRIMARY KEY ("RelacaoId")
);


CREATE TABLE socialmedia (
    "SocialMediaId" character varying(45) NOT NULL,
    "AplicacaoId" character varying(45),
    "SocialMediaTypeId" integer,
    "SocialMediaLink" character varying(255),
    CONSTRAINT "PK_socialmedia" PRIMARY KEY ("SocialMediaId")
);


CREATE TABLE templates (
    id integer GENERATED BY DEFAULT AS IDENTITY,
    "Nome" character varying(155),
    "Descricao" character varying(255),
    "Url" character varying(80),
    "Ativo" boolean,
    CONSTRAINT "PK_templates" PRIMARY KEY (id)
);


CREATE TABLE tipocotacao (
    "TipoCotacaoId" character varying(64) NOT NULL,
    "Nome" character varying(200),
    "Descricao" character varying(200),
    CONSTRAINT "PK_tipocotacao" PRIMARY KEY ("TipoCotacaoId")
);


CREATE TABLE tipoenvio (
    "TipoEnvioId" character varying(64) NOT NULL,
    "TipoEnvioDesc" character varying(300),
    CONSTRAINT "PK_tipoenvio" PRIMARY KEY ("TipoEnvioId")
);


CREATE TABLE unidades (
    "UnidadeId" uuid NOT NULL,
    "Nome" character varying(45),
    "Sigla" character varying(45),
    CONSTRAINT "PK_unidades" PRIMARY KEY ("UnidadeId")
);


CREATE TABLE usuario (
    "UserId" character varying(64) NOT NULL,
    "Nome" character varying(300) NOT NULL,
    "Sobrenome" character varying(300) NOT NULL,
    "Apelido" character varying(6),
    "Senha" character varying(12),
    "Ativo" smallint,
    "DataInclusao" timestamp with time zone,
    CONSTRAINT "PK_usuario" PRIMARY KEY ("UserId")
);


CREATE TABLE opcao (
    "OpcaoId" character varying(64) NOT NULL,
    "AtributoId" uuid NOT NULL,
    "Qtd" integer NOT NULL,
    "Nome" character varying(500),
    "Descricao" character varying(500),
    "Estoque" integer,
    "ValorAdicional" numeric(18,2),
    CONSTRAINT "PK_opcao" PRIMARY KEY ("OpcaoId"),
    CONSTRAINT "FK_Opcao_Atributo" FOREIGN KEY ("AtributoId") REFERENCES atributo ("AtributoId") ON DELETE CASCADE
);


CREATE TABLE modeloselecao (
    "ModeloSelecaoId" uuid NOT NULL DEFAULT (NEWID()),
    "ModeloCompostoId" uuid NOT NULL,
    "Atributoid" uuid NOT NULL,
    "Opcaoid" character varying(64) NOT NULL,
    CONSTRAINT "PK_modeloselecao" PRIMARY KEY ("ModeloSelecaoId"),
    CONSTRAINT "FK_ModeloSelecao_Modelo" FOREIGN KEY ("ModeloCompostoId") REFERENCES modelocomposto ("ModeloCompostoId") ON DELETE CASCADE
);


CREATE TABLE orcamentodetalhe (
    orcamentodetalheid uuid NOT NULL DEFAULT (NEWID()),
    orcamentoid uuid NOT NULL,
    descricao character varying(500) NOT NULL,
    quantidade numeric(10,2) NOT NULL,
    valor numeric(12,2),
    ativo boolean NOT NULL,
    CONSTRAINT "PK_orcamentodetalhe" PRIMARY KEY (orcamentodetalheid),
    CONSTRAINT "FK_orcamentodetalhe_orcamentoid" FOREIGN KEY (orcamentoid) REFERENCES orcamentocabecalho (orcamentoid) ON DELETE CASCADE
);


CREATE TABLE statuspedido (
    statuspedidoid uuid NOT NULL DEFAULT (NEWID()),
    pedidoid uuid NOT NULL,
    status character varying(50),
    descricao character varying(500),
    datahora timestamp with time zone NOT NULL DEFAULT (NOW()),
    CONSTRAINT "statuspedidoPK" PRIMARY KEY (statuspedidoid),
    CONSTRAINT statuspedido_pedidoid_fk FOREIGN KEY (pedidoid) REFERENCES pedido (pedidoid) ON DELETE CASCADE
);


CREATE TABLE orcamentodetalhecomposto (
    "OrcamentoDetalheCompostoId" uuid NOT NULL DEFAULT (NEWID()),
    orcamentoid uuid NOT NULL,
    "Produtoid" character varying(64) NOT NULL,
    "Quantidade" numeric(10,2) NOT NULL,
    "ValorBase" numeric(18,2) NOT NULL,
    "ValorTotal" numeric(18,2) NOT NULL,
    "ConfiguracaoJson" text NOT NULL,
    "Versao" integer NOT NULL DEFAULT 1,
    "Atual" boolean NOT NULL DEFAULT TRUE,
    "Datainclusao" timestamp with time zone NOT NULL DEFAULT (NOW()),
    CONSTRAINT "PK_orcamentodetalhecomposto" PRIMARY KEY ("OrcamentoDetalheCompostoId"),
    CONSTRAINT "FK_OrcDetalheComposto_Cabecalho" FOREIGN KEY (orcamentoid) REFERENCES orcamentocabecalho (orcamentoid) ON DELETE CASCADE,
    CONSTRAINT "FK_OrcDetalheComposto_Produto" FOREIGN KEY ("Produtoid") REFERENCES produto ("ProdutoId") ON DELETE CASCADE
);


CREATE TABLE selecao (
    "SelecaoId" uuid NOT NULL DEFAULT (NEWID()),
    "OrcamentoDetalheCompostoId" uuid NOT NULL,
    "Atributoid" uuid NOT NULL,
    "Opcaoid" character varying(64) NOT NULL,
    "ValorAdicional" numeric(18,2) NOT NULL DEFAULT 0.0,
    CONSTRAINT "PK_selecao" PRIMARY KEY ("SelecaoId"),
    CONSTRAINT "FK_Selecao_Atributo" FOREIGN KEY ("Atributoid") REFERENCES atributo ("AtributoId") ON DELETE CASCADE,
    CONSTRAINT "FK_Selecao_Detalhe" FOREIGN KEY ("OrcamentoDetalheCompostoId") REFERENCES orcamentodetalhecomposto ("OrcamentoDetalheCompostoId") ON DELETE CASCADE,
    CONSTRAINT "FK_Selecao_Opcao" FOREIGN KEY ("Opcaoid") REFERENCES opcao ("OpcaoId") ON DELETE CASCADE
);


CREATE INDEX "IX_atributo_ParentAtributoId" ON atributo ("ParentAtributoId");


CREATE UNIQUE INDEX "IX_modelocomposto_hash" ON modelocomposto ("ConfiguracaoHash", "Aplicacaoid", "Produtoid");


CREATE INDEX "IX_modeloselecao_ModeloCompostoId" ON modeloselecao ("ModeloCompostoId");


CREATE INDEX "IX_opcao_AtributoId" ON opcao ("AtributoId");


CREATE INDEX "IX_orcamentodetalhe_orcamentoid" ON orcamentodetalhe (orcamentoid);


CREATE INDEX "IX_orcamentodetalhecomposto_orcamentoid" ON orcamentodetalhecomposto (orcamentoid);


CREATE INDEX "IX_orcamentodetalhecomposto_Produtoid" ON orcamentodetalhecomposto ("Produtoid");


CREATE INDEX "IX_publictoken_aplicacaoid" ON publictoken (aplicacaoid);


CREATE UNIQUE INDEX "UQ_publictoken_token" ON publictoken (token);


CREATE INDEX "IX_selecao_Atributoid" ON selecao ("Atributoid");


CREATE INDEX "IX_selecao_Opcaoid" ON selecao ("Opcaoid");


CREATE INDEX "IX_selecao_OrcamentoDetalheCompostoId" ON selecao ("OrcamentoDetalheCompostoId");


CREATE INDEX "IX_statuspedido_pedidoid" ON statuspedido (pedidoid);


