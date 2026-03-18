<%@ Page Title="" Language="C#" MasterPageFile="~/InnerCliente.master" AutoEventWireup="true" CodeFile="twitterpage.aspx.cs" Inherits="twitterpage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language="javascript" src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script> 
<script language="javascript" src="Scripts/jquery.juitter.js" type="text/javascript"></script> 
<script language="javascript" src="Scripts/system.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphInner" Runat="Server">


<!--[if IE 6]>
<script src="DD_belatedPNG.js"></script>
<script>
  /* EXAMPLE */
  DD_belatedPNG.fix('.img_png');
  
  /* string argument can be any CSS selector */
  /* .png_bg example is unnecessary */
  /* change it to what suits you! */
</script>
<![endif]-->

<div id="containerTwitter">
<div id="Header">
  <!-- LOGOMARCA DO TWITTER -->

	
    <!-- INFORME ABAIXO QUAL SERÁ A HASHTAG UTILIZADA. VOCÊ TAMBÉM PRECISA CONFIGURAR O ARQUIVO "system.js" -->

    
    <!-- ORIENTAÇÃO -->
    
</div> 

	<!-- DIV QUE IRÁ RECEBER OS TWEETS POR JAVASCRIPT -->
	<div id="juitterContainer"></div> 
	
</div> 
</asp:Content>

