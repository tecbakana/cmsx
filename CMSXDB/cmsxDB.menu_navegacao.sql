-- Bloco menu-navegacao para o Page Builder
-- Executar após cmsxDB.pagebuilder.sql

INSERT INTO dict_blocos (tipobloco, nome, descricao, icone, schema_config)
VALUES (
  'menu-navegacao',
  'Menu de Navegação',
  'Barra de navegação com links para as áreas do site',
  'bi-menu-button-wide',
  '{"titulo": {"label": "Título/logo no menu"}, "cor_fundo": {"label": "Cor de fundo (ex: #1a1a2e)"}, "cor_texto": {"label": "Cor do texto (ex: #ffffff)"}, "estilo": {"label": "Estilo (horizontal | dropdown)"}}'
);
