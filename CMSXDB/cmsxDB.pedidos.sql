-- Tabela de pedidos (multi-tenant, populada via Service Bus)
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'pedido')
BEGIN
    CREATE TABLE pedido (
        pedidoid       UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID() PRIMARY KEY,
        aplicacaoid    NVARCHAR(36),
        numeropedido   NVARCHAR(100),
        clientenome    NVARCHAR(200),
        clienteemail   NVARCHAR(200),
        valorpedido    DECIMAL(12,2),
        statusatual    NVARCHAR(50),
        datainclusao   DATETIME2        NOT NULL DEFAULT GETUTCDATE()
    );
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'pedido_numeropedido_aplicacao_uq')
BEGIN
    CREATE UNIQUE INDEX pedido_numeropedido_aplicacao_uq
        ON pedido (numeropedido, aplicacaoid);
END

-- Tabela de timeline de status do pedido
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'statuspedido')
BEGIN
    CREATE TABLE statuspedido (
        statuspedidoid UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID() PRIMARY KEY,
        pedidoid       UNIQUEIDENTIFIER NOT NULL
            REFERENCES pedido(pedidoid) ON DELETE CASCADE,
        status         NVARCHAR(50),
        descricao      NVARCHAR(500),
        datahora       DATETIME2        NOT NULL DEFAULT GETUTCDATE()
    );
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'statuspedido_pedidoid_idx')
BEGIN
    CREATE INDEX statuspedido_pedidoid_idx ON statuspedido (pedidoid);
END
