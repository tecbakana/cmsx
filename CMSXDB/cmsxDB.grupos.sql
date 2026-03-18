-- --------------------------------------------------
-- CMSX - Grupos de Acesso
-- Execute no banco cmsxDB
-- --------------------------------------------------

USE [cmsxDB];
GO

-- Tabela de grupos
CREATE TABLE [dbo].[grupo] (
    [GrupoId]    VARCHAR(36)  NOT NULL PRIMARY KEY,
    [Nome]       VARCHAR(100) NOT NULL,
    [Descricao]  VARCHAR(255) NULL,
    [AcessoTotal] BIT         NOT NULL DEFAULT 0  -- 1 = acessa todos os tenants
);
GO

-- Relação usuário ↔ grupo
CREATE TABLE [dbo].[relusuariogrupo] (
    [RelacaoId]  VARCHAR(36) NOT NULL PRIMARY KEY,
    [UsuarioId]  VARCHAR(64) NOT NULL,
    [GrupoId]    VARCHAR(36) NOT NULL,
    CONSTRAINT FK_relug_usuario FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[usuario]([UserId]),
    CONSTRAINT FK_relug_grupo   FOREIGN KEY ([GrupoId])   REFERENCES [dbo].[grupo]([GrupoId])
);
GO

-- --------------------------------------------------
-- Dados iniciais
-- --------------------------------------------------

-- Grupos padrão
INSERT INTO [dbo].[grupo] (GrupoId, Nome, Descricao, AcessoTotal) VALUES
('GRP-001', 'Administradores', 'Acesso total a todos os tenants e configuracoes', 1),
('GRP-002', 'Suporte',         'Visualiza todos os tenants, sem permissao de exclusao', 1),
('GRP-003', 'Tenant',          'Acessa apenas a propria aplicacao', 0);
GO

-- Vincula admin ao grupo Administradores
INSERT INTO [dbo].[relusuariogrupo] (RelacaoId, UsuarioId, GrupoId)
VALUES ('REL-UG-001', 'USR-001', 'GRP-001');
GO

SELECT 'grupo' AS Tabela, COUNT(*) AS Registros FROM grupo UNION ALL
SELECT 'relusuariogrupo', COUNT(*) FROM relusuariogrupo;
GO
