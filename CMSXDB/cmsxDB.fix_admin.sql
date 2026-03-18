-- --------------------------------------------------
-- CMSX - Garante que o admin está no grupo Administradores
-- Execute no banco cmsxDB
-- --------------------------------------------------

USE [cmsxDB];
GO

-- Garante que o grupo Administradores existe
IF NOT EXISTS (SELECT 1 FROM [dbo].[grupo] WHERE GrupoId = 'GRP-001')
BEGIN
    INSERT INTO [dbo].[grupo] (GrupoId, Nome, Descricao, AcessoTotal)
    VALUES ('GRP-001', 'Administradores', 'Acesso total a todos os tenants e configuracoes', 1);
    PRINT 'Grupo Administradores criado.';
END
ELSE
    PRINT 'Grupo Administradores já existe.';
GO

-- Garante que o grupo Suporte existe
IF NOT EXISTS (SELECT 1 FROM [dbo].[grupo] WHERE GrupoId = 'GRP-002')
BEGIN
    INSERT INTO [dbo].[grupo] (GrupoId, Nome, Descricao, AcessoTotal)
    VALUES ('GRP-002', 'Suporte', 'Visualiza todos os tenants, sem permissao de exclusao', 1);
    PRINT 'Grupo Suporte criado.';
END
GO

-- Garante que o grupo Tenant existe
IF NOT EXISTS (SELECT 1 FROM [dbo].[grupo] WHERE GrupoId = 'GRP-003')
BEGIN
    INSERT INTO [dbo].[grupo] (GrupoId, Nome, Descricao, AcessoTotal)
    VALUES ('GRP-003', 'Tenant', 'Acessa apenas a propria aplicacao', 0);
    PRINT 'Grupo Tenant criado.';
END
GO

-- Vincula USR-001 (admin) ao grupo Administradores (se ainda não estiver)
IF NOT EXISTS (
    SELECT 1 FROM [dbo].[relusuariogrupo]
    WHERE UsuarioId = 'USR-001' AND GrupoId = 'GRP-001'
)
BEGIN
    INSERT INTO [dbo].[relusuariogrupo] (RelacaoId, UsuarioId, GrupoId)
    VALUES ('REL-UG-001', 'USR-001', 'GRP-001');
    PRINT 'Admin vinculado ao grupo Administradores.';
END
ELSE
    PRINT 'Admin já está no grupo Administradores.';
GO

-- Verifica resultado
SELECT u.Nome, u.Apelido, g.Nome AS Grupo, g.AcessoTotal
FROM [dbo].[usuario] u
JOIN [dbo].[relusuariogrupo] rug ON rug.UsuarioId = u.UserId
JOIN [dbo].[grupo] g ON g.GrupoId = rug.GrupoId
WHERE u.UserId = 'USR-001';
GO
