<%@ Page Title="" Language="C#" MasterPageFile="~/InnerCliente.master" AutoEventWireup="true" CodeFile="reservasonline.aspx.cs" Inherits="reservasonline" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link rel="Stylesheet" type="text/css" href="html/css/heritage.css" />
<script type="text/javascript">

    var hosperr = 0;

    function checaHospedePorQuarto() 
    {
        var form = document.forms[0];
        var nhosp = parseInt(form.nadt.options[form.nadt.options.selectedIndex].value) + parseInt(form.nchild.options[form.nchild.options.selectedIndex].value);
        var rooms = parseInt(form.nroom.options[form.nroom.options.selectedIndex].value);

        /* VALIDANDO QTD DE HOSPEDES POR QUARTO, CONSIDERANDO O TIPO */
        //# 0
        //Suite Master 1
        //Suite S&ecirc;nior 2
        //Apartamento Standard 3

        var criterio = form.tpRoom.options.selectedIndex;
        var selectedRoom = form.tpRoom.options[form.tpRoom.options.selectedIndex].value;
        var regra = 0;
        var msg = "";

        switch (criterio)
        {
            case 0:
                alert("Escolha um tipo de apartamento");
                break;
            case 1: //Master Standard
            case 3:
                regra = 3;
                msg = selectedRoom + ": Não pode ser superior a 3 pessoas por quarto, sendo a terceira pessoa num sofá cama";
                break;
            case 2:
                regra = 2;
                msg = selectedRoom + ": Suporta até duas pessoas apenas";
                break;                
        }

        var parametro = (nhosp / rooms);


        if (parametro>=regra) 
        {
            alert(msg);
            form.nadt.focus();
            hosperr = 1;
        }
    }

    function checa_formulario()
    {
        checaHospedePorQuarto();

        var form = document.forms[0];

        var contatoErr = 1;
        var aptoErr = 1;

        for (i = 0; i < form.Tipo_Contato.length; i++) {
            if (form.Tipo_Contato[i].checked) {
                contatoErr = 0;
                break;
            }
        }

        for (i = 0; i < form.Tipo_Apartamento.length; i++) {
            if (form.Tipo_Apartamento[i].checked) {
                aptoErr = 0;
                break;
            }
        }


        if (contatoErr) {
            alert("Informe o Tipo de Contato");
            form.Tipo_Contato[0].focus();
        } else if (form.Nome_solicitante.value == "") {
            alert("Coloque o Nome do Solicitante");
            form.Nome_solicitante.focus();
        } else if (form.email.value == "") {
            alert("Coloque o E-mail");
            form.email.focus();
        } else if (form.Telefone_fixo.value == "") {
            alert("Coloque o Telefone Fixo");
            form.Telefone_fixo.focus();
        } else if (form.Dia_Entrada.value == "#") {
            alert("Coloque o Dia de Entrada");
            form.Dia_Entrada.focus();
        } else if (form.Mes_Entrada.value == "#") {
            alert("Coloque o Mes de Entrada");
            form.Mes_Entrada.focus();
        } else if (form.Dia_Saida.value == "#") {
            alert("Coloque o Dia de Saida");
            form.Dia_Saida.focus();
        } else if (form.Mes_Saida.value == "#") {
            alert("Coloque o Mes de Saida");
            form.Mes_Saida.focus();
        } else if (form.nadt.value == "") {
            alert("Coloque o Numero de Adultos");
            form.nadt.focus();
        } else if (form.tpRoom.value == "") {
            alert("Coloque o Tipo de Quarto");
            form.Tipo_Quarto.focus();
        } else if (form.txtHospedes.value == "") {
            alert("Coloque o nome dos hóspedes");
            form.Tipo_Quarto.focus();
        } else if (aptoErr) {
            alert("Informe o tipo do apartamento");
            form.Tipo_Apartamento[0].focus();
        } else if (hosperr == 1) {
            alert("Erro ao processar os hospedes");
        } else {
            submitform();
        }
    }


    /**
    * Submit the admin form
    */
    function submitform() {
        try {
            document.forms[0].onsubmit();
        }
        catch (e) {

        }
        finally 
        {

        }
        document.forms[0].submit();
    }

    /*
    *  FECHA A MENSAGEM
    */
    debugger;
    function handMsg(p) {
        obj = document.getElementById("pnlMsg");
        obj.style.display = (p == 1 ? "block" : "none");
    }

   
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div id="pnlMsg" style="position:absolute;width:600px;height:1056px; padding-top:100px; display:block;text-align:center;">
        <span style="font: bold 8pt verdana, arial;color:#003366;text-align:center;padding-left:10px">
            <asp:Label ID="retmsg" runat="server" />
        </span>
        <hr style="height:1px;background-color:#333366;border:0px" />
        <p align="center" class="caixa"><strong>Observa&ccedil;&otilde;es:</strong><br />
                  Reservas 
                    efetuadas via formul&aacute;rio:</strong><br>* 
                   <font color="red" style="font-weight:bold"> A Reserva ter&aacute; validade somente se confirmada atrav&eacute;s 
                    do meio de comunica&ccedil;&atilde;o selecionado acima.</font><br>
                    - Hor&aacute;rio para confirma&ccedil;&atilde;o via e-mail/retorno, 
                    de <strong>Segunda &agrave; Sexta-feira, das 07:00hs &agrave;s 
                    16:00hs.</strong></p>
      
        <img src="images/fechar.gif" onclick="javascript:handMsg(2);" style="float:right;cursor:pointer" />
    </div>

    <div align="center">
                <h1>Reserva Online</h1>
                <%--<span style="font: bold 8pt verdana, arial;color:#003366;text-align:center;padding-left:10px">
                   <asp:Label ID="retmsg" runat="server" />
                </span>--%>
                <form method="post" action="reservasonline.aspx" name="cad">
                  <input type="hidden" name="recipient" value="reservas@heritageresidence.com.br">
                  <input type="hidden" name="redirect" value="sucesso2.html">
                  <input type="hidden" name="subject" value="Formulario Preenchido Online">
                  <br>
                  <br>
                  
                  <table width="640" border="0" cellpadding="3" class="formtable">
                    <tr> 
                      <td height="42" colspan="2">Solicite 
                        sua reserva por Telefone 0xx11 - <strong><font color="#003366">3258-4088</strong> 
                        ou pelo formul&aacute;rio abaixo. </td>
                    </tr>
                    <tr> 
                      <td>Tipo 
                        de contato:</td>
                      <td>
                        <input type="radio" name="Tipo_Contato" value="Desejo apenas Informacao">
                        - Pedido de Informa&ccedil;&atilde;o</td>
                    </tr>
                    <tr> 
                      <td>&nbsp;</td>
                      <td>
                        <input type="radio" name="Tipo_Contato" value="Desejo solicitar Reserva">
                        - Solicita&ccedil;&atilde;o de Reserva</td>
                    </tr>
                    <tr> 
                      <td>&nbsp;</td>
                      <td>&nbsp; 
                        </td>
                    </tr>
                    <tr> 
                      <td>*Nome 
                        do solicitante:</td>
                      <td>
                        <input maxlength="100" type="text" name="Nome_solicitante" style="width: 200px" class="form" size="30" title="Marque a caixa ao lado caso você seja o hóspede." />
                        </td>
                    </tr>
                    <tr> 
                      <td>*Confirme 
                        seu e-mail:</td>
                      <td>
                        <input maxlength="255" type="text" name="email" style="width: 200px" class="form" size="30" id="email" />
                        </td>
                    </tr>
                    <tr> 
                      <td width="50%">Cidade</td>
                      <td width="50%">
                        <input maxlength="45" type="text" name="Cidade" style="width: 200px" class="form" size="30" id="Cidade" />
                        </td>
                    </tr>
                    <tr> 
                      <td>Estado:</td>
                      <td>
                        <select name="Estado" class="form" id="Estado">
                          <option value="0">UF</option>
                          <option value="AC">AC</option>
                          <option value="AL">AL</option>
                          <option value="AM">AM</option>
                          <option value="AP">AP</option>
                          <option value="BA">BA</option>
                          <option value="CE">CE</option>
                          <option value="DF">DF</option>
                          <option value="ES">ES</option>
                          <option value="GO">GO</option>
                          <option value="MA">MA</option>
                          <option value="MG">MG</option>
                          <option value="MS">MS</option>
                          <option value="MT">MT</option>
                          <option value="PA">PA</option>
                          <option value="PB">PB</option>
                          <option value="PE">PE</option>
                          <option value="PI">PI</option>
                          <option value="PR">PR</option>
                          <option value="RJ">RJ</option>
                          <option value="RN">RN</option>
                          <option value="RO">RO</option>
                          <option value="RR">RR</option>
                          <option value="RS">RS</option>
                          <option value="SC">SC</option>
                          <option value="SE">SE</option>
                          <option value="SP">SP</option>
                          <option value="TO">TO</option>
                        </select>
                        </td>
                    </tr>
                    <tr> 
                      <td>*Telefone 
                        Fixo:</td>
                      <td>
                        <input maxlength="10" type="text" name="DDD_fixo" style="width: 40px" class="form" size="10" id="DDD_fixo" />
                        <input maxlength="45" type="text" name="Telefone_fixo" style="width: 155px" class="form" size="30" id="Telefone_fixo" />
                        </td>
                    </tr>
                    <tr> 
                      <td>Telefone 
                        Celular:<br>
                        <font size="1">Obs: n&atilde;o enviamos mensagem por celular</td>
                      <td>
                        <input maxlength="10" type="text" name="DDD_celular" style="width: 40px" class="form" size="10" id="DDD_celular" />
                        <input maxlength="45" type="text" name="Telefone_celular" style="width: 155px" class="form" size="30" id="Telefone_celular" />
                        </td>
                    </tr>
                    <tr> 
                      <td>Telefone 
                        Fax:</td>
                      <td>
                        <input maxlength="10" type="text" name="DDD_fax" style="width: 40px" class="form" size="10" id="DDD_fax" />
                        <input maxlength="45" type="text" name="Telefone_fax" style="width: 155px" class="form" size="30" id="Telefone_fax" />
                        </td>
                    </tr>
                    <tr> 
                      <td height="36" colspan="2"><div align="center"><font color="#003366" size="2" face="Verdana, Arial, Helvetica, sans-serif"><strong>Dados 
                          da Hospedagem</strong></div></td>
                    </tr>
                    <tr> 
                      <td>
                        <label for="book_check_out">*Data de Entrada:</label>
                        </td>
                      <td>
                        <select class="form" name="Dia_Entrada" id="Dia_Entrada">
                          <option value="#" selected></option>
                          <option value="1">1</option>
                          <option value="2">2</option>
                          <option value="3">3</option>
                          <option value="4">4</option>
                          <option value="5">5</option>
                          <option value="6">6</option>
                          <option value="7">7</option>
                          <option value="8">8</option>
                          <option value="9">9</option>
                          <option value="10">10</option>
                          <option value="11">11</option>
                          <option value="12">12</option>
                          <option value="13">13</option>
                          <option value="14">14</option>
                          <option value="15">15</option>
                          <option value="16">16</option>
                          <option value="17">17</option>
                          <option value="18">18</option>
                          <option value="19">19</option>
                          <option value="20">20</option>
                          <option value="21">21</option>
                          <option value="22">22</option>
                          <option value="23">23</option>
                          <option value="24">24</option>
                          <option value="25">25</option>
                          <option value="26">26</option>
                          <option value="27">27</option>
                          <option value="28">28</option>
                          <option value="29">29</option>
                          <option value="30">30</option>
                          <option value="31">31</option>
                        </select>
                        <select class="form" name="Mes_Entrada" id="Mes_Entrada">
                          <option value="#" selected></option>
                          <option value="Janeiro">Janeiro</option>
                          <option value="Fevereiro">Fevereiro</option>
                          <option value="Março">Março</option>
                          <option value="Abril">Abril</option>
                          <option value="Maio">Maio</option>
                          <option value="Junho">Junho</option>
                          <option value="Julho">Julho</option>
                          <option value="Agosto">Agosto</option>
                          <option value="Setembro">Setembro</option>
                          <option value="Outubro">Outubro</option>
                          <option value="Novembro">Novembro</option>
                          <option value="Dezembro">Dezembro</option>
                        </select>
                        </td>
                    </tr>
                    <tr> 
                      <td>
                      <label for="book_check_out">*Data de saída:</label>
                        </td>
                      <td><div class="formLeftcol"> 
                          <label for="book_check_out"></label>
                        </div>
                          <select class="form" name="Dia_Saida" id="Dia_Saida">
                            <option value="#" selected></option>
                            <option value="1">1</option>
                            <option value="2">2</option>
                            <option value="3">3</option>
                            <option value="4">4</option>
                            <option value="5">5</option>
                            <option value="6">6</option>
                            <option value="7">7</option>
                            <option value="8">8</option>
                            <option value="9">9</option>
                            <option value="10">10</option>
                            <option value="11">11</option>
                            <option value="12">12</option>
                            <option value="13">13</option>
                            <option value="14">14</option>
                            <option value="15">15</option>
                            <option value="16">16</option>
                            <option value="17">17</option>
                            <option value="18">18</option>
                            <option value="19">19</option>
                            <option value="20">20</option>
                            <option value="21">21</option>
                            <option value="22">22</option>
                            <option value="23">23</option>
                            <option value="24">24</option>
                            <option value="25">25</option>
                            <option value="26">26</option>
                            <option value="27">27</option>
                            <option value="28">28</option>
                            <option value="29">29</option>
                            <option value="30">30</option>
                            <option value="31">31</option>
                          </select>
                          
                          <select class="form" name="Mes_Saida" id="Mes_Saida">
                            <option value="#" selected></option>
                          <option value="Janeiro">Janeiro</option>
                          <option value="Fevereiro">Fevereiro</option>
                          <option value="Março">Março</option>
                          <option value="Abril">Abril</option>
                          <option value="Maio">Maio</option>
                          <option value="Junho">Junho</option>
                          <option value="Julho">Julho</option>
                          <option value="Agosto">Agosto</option>
                          <option value="Setembro">Setembro</option>
                          <option value="Outubro">Outubro</option>
                          <option value="Novembro">Novembro</option>
                          <option value="Dezembro">Dezembro</option>
                          </select>
                         </td>
                    </tr>
                    <tr> 
                      <td>
                        <label for="label5">*Número de adultos:</label>
                        </td>
                      <td>
                        <select name="nadt" class="form" id="nadt">
                          <option value="0">0</option>
                          <option value="1">1</option>
                          <option value="2">2</option>
                          <option value="3">3</option>
                        </select>
                        </td>
                    </tr>
                    <tr> 
                      <td>
                        <label for="label6">Número de crianças (até 5 anos):</label>
                        </td>
                      <td>
                        <select name="nchild" class="form" id="nchild">
                          <option value="0">0</option>
                          <option value="1">1</option>
                          <option value="2">2</option>
                        </select>
                        </td>
                    </tr>
                    <tr> 
                      <td>Número 
                        de quartos:</td>
                      <td>
                        <select name="nroom" class="form" id="nroom">
                          <option value="1">1</option>
                          <option value="2">2</option>
                          <option value="3">3</option>
                          <option value="4">4</option>
                          <option value="5">5</option>
                        </select>
                        </td>
                    </tr>
                    <tr> 
                      <td>
                        <label for="book_check_out">*Tipo de quarto</label>
                        </td>
                      <td>
                        <select name="tpRoom" class="form" id="tpRoom">
                          <option value="#">[Selecione] </option>
                          <option value="Suite Master ">Suite Master </option>
                          <option value="Suite S&ecirc;nior">Suite S&ecirc;nior</option>
                          <option value="Apartamento Standard">Apartamento Standard</option>
                        </select>
                        </td>
                    </tr>
                    <tr> 
                      <td>Que 
                        tipo de apartamento deseja?:</td>
                      <td>
                        <input type="radio" name="Tipo_Apartamento" value="Desejo acomodacao com cama de solteiro">
                        - Camas de Solteiro </td>
                    </tr>
                    <tr> 
                      <td>&nbsp;</td>
                      <td>
                        <input type="radio" name="Tipo_Apartamento" value="Desejo acomodacao com cama de casal">
                        - Cama de Casal</td>
                    </tr>
                    <tr> 
                      <td>Tem Prefer&ecirc;ncia do apartamento 
                        para :</td>
                      <td>
                        <input type="radio" name="Fumante" value="Desejo acomodacao nao fumante">
                        - N&atilde;o Fumante</td>
                    </tr>
                    <tr> 
                      <td>&nbsp;</td>
                      <td>
                        <input type="radio" name="Fumante" value="Desejo acomodacao para fumante">
                        - Fumante</td>
                    </tr>
                    <tr> 
                      <td colspan="2">Observa&ccedil;&atilde;o: 
                        <br />
                        <br />
                        - Apartamento Standard e Su&iacute;te Master s&atilde;o 
                        compostos por cama de casal ou duas camas de solteiro 
                        no quarto e uma bi-cama na sala.<br>
                        - 
                        Su&iacute;te S&ecirc;nior composta apenas por uma cama 
                        de casal. ou 2 (duas) camas de solteiro<br /><br /></td>
                    </tr>
                    <tr> 
                      <td>Nome 
                        completo do(s) h&oacute;spede(s):</td>
                      <td>
                        <textarea rows="4" cols="42" name="txtHospedes" class="form" id="txtHospedes"></textarea>
                        </td>
                    </tr>
                    <tr> 
                      <td>Comentários 
                        / Observações:</td>
                      <td>
                        <textarea rows="7" cols="42" name="Mensagem" class="form" id="Mensagem"></textarea>
                        </td>
                    </tr>
                    <tr> 
                      <td>&nbsp;</td>
                      <td>&nbsp;</td>
                    </tr>
                    <tr> 
                      <td rowspan="2">Deseja 
                        receber a confirma&ccedil;&atilde;o da reserva via :</td>
                      <td>
                        <input type="radio" name="Via_Confirmacao" value="Confirme o recebimento em meu  E-Mail">
                        - E-Mail </td>
                    </tr>
                    <tr> 
                      <td>
                        <input type="radio" name="Via_Confirmacao" value="Confirme o recebimento em meu Fax">
                        - Fax</td>
                    </tr>
                    <tr> 
                      <td>&nbsp;</td>
                      <td>
                        <input type="radio" name="Via_Confirmacao" value="Confirme o recebimento em meu Telefone Fixo">
                        - Telefone Fixo</td>
                    </tr>
                    <tr> 
                      <td>&nbsp;</td>
                      <td>
                        <input type="radio" name="Via_Confirmacao" value="Confirme o recebimento em meu Telefone Celular">
                        - Telefone Celular</td>
                    </tr>
                    <tr> 
                      <td height="18" colspan="2">&nbsp;</td>
                    </tr>
                    <tr> 
                      <td>&nbsp;</td>
                      <td>
                        <input type="button" id="btsend" runat="server" onclick="javascript:checa_formulario();" value="Enviar" />
                        <%--<asp:Button ID="btsend" UseSubmitBehavior="false" runat="server" OnClientClick="javascript:checa_formulario();" Text="Enviar" />--%>
                      </td>
                    </tr>
                  </table>
                  <p align="center" class="caixa"><strong>Observa&ccedil;&otilde;es:</strong><br />
                  Reservas 
                    efetuadas via formul&aacute;rio:</strong><br>* 
                   <font color="red" style="font-weight:bold"> A Reserva ter&aacute; validade somente se confirmada atrav&eacute;s 
                    do meio de comunica&ccedil;&atilde;o selecionado acima.</font><br>
                    - Hor&aacute;rio para confirma&ccedil;&atilde;o via e-mail/retorno, 
                    de <strong>Segunda &agrave; Sexta-feira, das 07:00hs &agrave;s 
                    16:00hs.</strong></p>
                </form>
              </div>

</asp:Content>

