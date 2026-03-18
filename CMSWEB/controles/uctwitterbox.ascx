<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uctwitterbox.ascx.cs" Inherits="controles_uctwitterbox" %>
<script language="javascript" src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script> 
<script language="javascript" src="Scripts/jquery.juitter.js" type="text/javascript"></script> 
<%--<script language="javascript" src="Scripts/system.js" type="text/javascript"></script>--%>
<script type="text/javascript" >
    $(document).ready(function () {
        $.Juitter.start({
            searchType: "fromUser", // Obrigatório. Você pode escolher entre "searchWord" (procurar palavra), "fromUser" (de um usuário), "toUser" (para um usuário)
            searchObject: "<%=_hashtag %>", // Obrigatório. Você pode inserir uma palavra ou o nome do usuário. Para buscas múltiplas, separe as palavras por vírgula. Para buscar por hashtagh, utilize o %23 antes da palavra.

            // Configurações.
            lang: "", // Deixe vazio para não fazer nenhuma restrição por idiomas. Utilize "pt" para português.
            live: "live-<%=_updatetime %>", // O número depois de "live-" indica o tempo em secundos entre cada atualização. Não exagere. ;-)
            placeHolder: "juitterContainer", // ID da div que irá receber o conteúdo.
            loadMSG: "Carregando...", // Mensagem exibida enquanto os tweets estão sendo carregados. Para utilizar uma imagem, preencha o campo com "image/gif" e informe abaixo a URL. 
            imgName: "loader.gif", // URL da imagem enquanto os tweets estão sendo carregados. Para funcionar você deve preencher o campo loadMSG com "image/gif".
            total: 3, // Número de tweets que serão exibidos. Máximo 100.
            readMore: "Veja no Twitter", // Mensagem exibida ao final do tweet.
            nameUser: "image", // Preencha "image" para exibir o avatar dos usuários ou "text" para exibir apenas o nome dos usuários.
            openExternalLinks: "newWindow", // Defina como serão abertos os links de sites externos com "newWindow" (nova janela) ou "sameWindow" (mesma janela).
            filter: "dwarfurl.com,a0c2c,sexo"  // Os tweets que contenham qualquer uma dessas palavras não serão exibidos. 
        });

    });
</script>
<div id="juitterContainer"></div> 