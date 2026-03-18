-- --------------------------------------------------
-- CMSX IA: cache, uso e configuração por tenant
-- --------------------------------------------------

USE [cmsxDB];
GO

-- Configuração de IA por tenant (chave própria, provedor, limite)
IF OBJECT_ID(N'[dbo].[ia_config]', 'U') IS NOT NULL DROP TABLE [dbo].[ia_config];
GO
CREATE TABLE [dbo].[ia_config] (
    [aplicacaoid]   NVARCHAR(50)   NOT NULL PRIMARY KEY,
    [provedor]      NVARCHAR(20)   NULL,          -- anthropic | gemini | null = default sistema
    [apikey]        NVARCHAR(500)  NULL,           -- chave própria do tenant
    [modelo]        NVARCHAR(100)  NULL,           -- modelo override (null = default do provedor)
    [limite_diario] INT            NULL            -- null = usar LimiteDiarioPadrao do appsettings
);
GO

-- Cache de prompts do Page Builder
IF OBJECT_ID(N'[dbo].[ia_cache]', 'U') IS NOT NULL DROP TABLE [dbo].[ia_cache];
GO
CREATE TABLE [dbo].[ia_cache] (
    [cacheid]        NVARCHAR(36)   NOT NULL PRIMARY KEY,
    [hash]           NVARCHAR(64)   NOT NULL UNIQUE,   -- SHA256 do prompt
    [resultado]      NVARCHAR(MAX)  NOT NULL,
    [datainclusao]   DATETIME       NOT NULL DEFAULT GETDATE(),
    [datavencimento] DATETIME       NOT NULL
);
GO
CREATE INDEX IX_ia_cache_hash ON [dbo].[ia_cache] ([hash]);
GO

-- Contador de uso diário por tenant
IF OBJECT_ID(N'[dbo].[ia_uso]', 'U') IS NOT NULL DROP TABLE [dbo].[ia_uso];
GO
CREATE TABLE [dbo].[ia_uso] (
    [usoid]        NVARCHAR(36)  NOT NULL PRIMARY KEY,
    [aplicacaoid]  NVARCHAR(50)  NOT NULL,
    [data]         DATE          NOT NULL,
    [contador]     INT           NOT NULL DEFAULT 0,
    CONSTRAINT UQ_ia_uso UNIQUE ([aplicacaoid], [data])
);
GO
CREATE INDEX IX_ia_uso_appdata ON [dbo].[ia_uso] ([aplicacaoid], [data]);
GO
