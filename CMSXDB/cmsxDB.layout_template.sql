-- layout_template: templates de layout do Page Builder
-- Executar no SQL Server após cmsxDB.pagebuilder.sql

CREATE TABLE layout_template (
    templateid    VARCHAR(64)   NOT NULL PRIMARY KEY,
    nome          VARCHAR(100)  NOT NULL,
    descricao     VARCHAR(255)  NULL,
    tipo          VARCHAR(50)   NULL DEFAULT 'home',
    layout        NVARCHAR(MAX) NULL,
    padrao        BIT           NOT NULL DEFAULT 0,
    datainclusao  DATETIME      NOT NULL DEFAULT GETDATE()
);

-- Garante que só existe um template padrão por tipo
CREATE UNIQUE INDEX UQ_layout_template_padrao
    ON layout_template (tipo)
    WHERE padrao = 1;
