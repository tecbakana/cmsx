-- Novos blocos para o Page Builder
-- Executar após cmsxDB.pagebuilder.sql e cmsxDB.menu_navegacao.sql

USE [cmsxDB];
GO

-- Remove se já existir (idempotente)
DELETE FROM [dbo].[dict_blocos] WHERE tipobloco IN ('rodape','prova-social','video','contador','hero-cta');
GO

INSERT INTO [dbo].[dict_blocos] ([tipobloco], [nome], [descricao], [icone], [schema_config]) VALUES
(
    'rodape',
    'Rodapé',
    'Rodapé do site com endereço, links rápidos, redes sociais e copyright.',
    'bi-layout-wtf',
    '{
        "empresa": {"type":"string","label":"Nome da empresa","default":""},
        "endereco": {"type":"string","label":"Endereço","default":""},
        "telefone": {"type":"string","label":"Telefone","default":""},
        "email": {"type":"string","label":"E-mail","default":""},
        "facebook": {"type":"string","label":"URL Facebook","default":""},
        "instagram": {"type":"string","label":"URL Instagram","default":""},
        "linkedin": {"type":"string","label":"URL LinkedIn","default":""},
        "youtube": {"type":"string","label":"URL YouTube","default":""},
        "copyright": {"type":"string","label":"Texto de copyright","default":""},
        "cor_fundo": {"type":"string","label":"Cor de fundo","default":"#1a1a2e"},
        "cor_texto": {"type":"string","label":"Cor do texto","default":"#ffffff"}
    }'
),
(
    'prova-social',
    'Prova Social / Depoimentos',
    'Carrossel ou grade de depoimentos e avaliações de clientes.',
    'bi-chat-quote',
    '{
        "titulo": {"type":"string","label":"Título da seção","default":"O que dizem sobre nós"},
        "itens": {"type":"array","label":"Depoimentos","default":[
            {"nome":"Maria Silva","cargo":"CEO","texto":"Excelente serviço!","avatar":"","estrelas":5},
            {"nome":"João Souza","cargo":"Gerente","texto":"Muito satisfeito.","avatar":"","estrelas":5}
        ]}
    }'
),
(
    'video',
    'Vídeo Incorporado',
    'Incorpora um vídeo do YouTube ou Vimeo.',
    'bi-play-circle',
    '{
        "titulo": {"type":"string","label":"Título da seção","default":""},
        "url": {"type":"string","label":"URL do vídeo (YouTube ou Vimeo)","default":""},
        "descricao": {"type":"string","label":"Descrição abaixo do vídeo","default":""},
        "largura": {"type":"string","label":"Largura (full, container)","default":"container"}
    }'
),
(
    'contador',
    'Contador Regressivo',
    'Contador com data alvo para lançamentos, promoções ou eventos.',
    'bi-alarm',
    '{
        "titulo": {"type":"string","label":"Título","default":"Oferta encerra em:"},
        "dataAlvo": {"type":"string","label":"Data/hora alvo (ISO 8601)","default":""},
        "mensagemFim": {"type":"string","label":"Mensagem ao zerar","default":"Oferta encerrada!"},
        "cor_fundo": {"type":"string","label":"Cor de fundo","default":"#0d6efd"},
        "cor_texto": {"type":"string","label":"Cor do texto","default":"#ffffff"}
    }'
),
(
    'hero-cta',
    'Hero com Captura',
    'Hero banner com formulário de captura de leads embutido.',
    'bi-envelope-open',
    '{
        "titulo": {"type":"string","label":"Título principal","default":"Receba nossas novidades"},
        "subtitulo": {"type":"string","label":"Subtítulo","default":"Cadastre-se e fique por dentro"},
        "placeholder": {"type":"string","label":"Placeholder do campo e-mail","default":"Seu melhor e-mail"},
        "textobotao": {"type":"string","label":"Texto do botão","default":"Quero receber"},
        "formularioid": {"type":"string","label":"ID do formulário (para salvar leads)","default":""},
        "cor_fundo": {"type":"string","label":"Cor de fundo","default":"#0d6efd"},
        "imagem_fundo": {"type":"string","label":"URL imagem de fundo","default":""}
    }'
);
GO

SELECT tipobloco, nome FROM [dbo].[dict_blocos] ORDER BY nome;
GO
