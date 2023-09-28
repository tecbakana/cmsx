﻿<%@ Page Title="" Language="C#" MasterPageFile="~/bootstrap_client.master" AutoEventWireup="true" CodeFile="usersignup.aspx.cs" Inherits="usersignup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <div class="panel panel-primary">
          <div class="panel-heading info">
              <img src="images/fundomkt.jpg" class="img-responsive" /><br />
              <div class="info">Crie agora mesmo sua conta! &Eacute; gr&aacute;tis!*</div>
              <div class="info">*plano basico I (com propagandas n&atilde;o intrusivas)</div>
          </div>
          <div class="panel-body">
              <div class="col-md-2"></div>
              <div class="col-md-8">
                  <div class="form-group">
                      <label for="inputName">Seu endere&ccedil;o (sujeito a disponibilidade)</label>
                     <input class="form-control" data-bind="htmlValue: userName" /><br />
                      <span id="retMsg"></span>
                  </div>
                  <div class="form-group">
                      <label for="inputName">Seu nome</label>
                      <input name="txtNome" class="form-control" data-bind="value:name" />
                  </div>
                  <div class="form-group">
                      <label for="inputName">Sobrenome</label>
                      <input name="txtNome" class="form-control" data-bind="value: surname" />
                  </div>
                  <div class="form-group">
                    <label for="txtMail">Email</label>
                      <input name="txtMail" class="form-control" data-bind="value: email" />
                  </div>
                  <div class="form-group">
                    <label for="txtSenha">Senha</label>
                      <input name="txtSenha" type="password" class="form-control" data-bind="value: senha" />
                  </div>
                  <div class="form-group">
                    <label for="txtSenha">Temas dispon&iacute;veis</label>
                      <select data-bind="options: availThemes, optionsText: 'tname', optionsValue: 'turl', optionsCaption: 'Escolha o template...'" size="1"></select>
                  </div>
                  <div class="form-group">
                    <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                      <div class="panel panel-default">
                        <div class="panel-heading" role="tab" id="headingOne">
                          <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                              TERMOS E CONDI&Ccedil;&Otilde;ES DE USO
                            </a>
                          </h4>
                        </div>
                        <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                          <div class="panel-body">
                            <pre>

                                  1. Generalidades

                                1.1. O uso do site FlameIT/SeuSite (doravante será denominada FlameIT) e de todas as informações, telas, exibições e todos os serviços oferecidos 
                                pelA FlameIT, estão sujeitos às condições constantes deste termo.
                                1.2. Estas condições poderão ser alteradas sem necessidade de aviso prévio específico. As alterações constarão sempre deste termo, que deverá ser 
                                revisado sempre pelos usuários.
                                1.3. AO USAR OS SERVIÇOS, VOCÊ CONCORDA COM CADA UMA DAS CONDIÇÕES ABAIXO ESTABELECIDAS. SE VOCÊ NÃO CONCORDAR COM QUAISQUER DOS TERMOS OU CONDIÇÕES 
                                ABAIXO ESTABELECIDOS, VOCÊ NÃO ESTARÁ AUTORIZADO A USAR OS SERVIÇOS PARA QUALQUER PROPÓSITO. CASO VOCÊ TENHA QUAISQUER DÚVIDAS REFERENTES A ESTAS 
                                CONDIÇÕES, SINTA-SE À VONTADE PARA CONTATAR-NOS.
                                1.4. A utilização dos serviços da FlameIT sem a observância das condições constantes deste termo acarretará ao usuário toda responsabilidade por 
                                qualquer dano ou prejuízo que possa sofrer ou causar.
                                1.5. A FlameIT possibilita o acesso a este site, bem como aos seus serviços, livre de qualquer encargo. No entanto, 
                                A FlameIT reserva-se o direito de cobrar pelo uso dos serviços, quando convier, com prévio aviso.


                                  2. Terceiros

                                2.1. A FlameIT possui links (canais de ligação, publicidade) com Web sites, páginas de Internet e serviços de terceiros, sobre os quais não tem 
                                qualquer ingerência, não tendo, portanto, qualquer responsabilidade sobre seu conteúdo, bem como sobre os efeitos de seu uso.
                                2.2. O FlameIT disponibiliza canais de comunicação entre alunos e professores, que porventura trarão informações e opiniões de diversas pessoas, 
                                organizações, associações, enfim, entidades, de caráter filantrópico, comercial, ideológico, acadêmico, religioso, ou qualquer outro assunto, como 
                                um canal democrático de internet. Por esta razão, a FlameIT não se responsabiliza pelo conteúdo dessas opiniões e informações.
                                2.3. A FlameIT possui parcerias estratégicas com diversos canais de serviços, vendas e informação, os quais realizam envio de email marketing, 
                                que será enviado ao usuário SOMENTE se ele, usuário, concordar em receber tais mensagens, e poderá alterar este status a qualquer momento, através
                                de sua página pessoal. A FlameIT não tem qualquer ingerência sobre o conteúdo de emails enviados por seus parceiros, não tendo, portanto, qualquer
                                responsabilidade sobre seu conteúdo, bem como sobre os efeitos de seu uso.


                                  3. Cadastro

                                3.1. Para fazer  dos serviços, a FlameIT exige que o usuário faça um cadastro. O usuário não é obrigado a se cadastrar, caso em que deixa de poder 
                                fazer uso dos serviços.



                                  4. Acesso

                                4.1. O usuário efetua acesso aos serviços mediante o login e senha cadastrados, com perfil de Administrador de sua propria area.


                                  5. Obrigações dos Usuários

                                5.1. O usuário deverá seguir as seguintes regras básicas de conduta:
                                   a) O usuário nunca fornecerá dados financeiros a ninguém
                                   b) O usuário não irá assediar, ameaçar ou fraudar outras pessoas enquanto estiver usando a FlameIT;
                                   c) O usuário concorda que é totalmente responsável por quaisquer materiais que venha a disponibilizar pelos serviços da FlameIT.
                                   d) O usuário não introduzirá nenhum conteúdo que seja obsceno ou que possa ser ofensivo aos parâmetros raciais, étnicos ou sexuais, ou ainda que 
                                seja perigoso, difamatório, acusatório, ou que possa invadir a intimidade pessoal ou direitos de outra pessoa; 
                                   e) O usuário não tentará coletar nenhum nome dos endereços eletrônicos para fins comerciais ou quaisquer outros fins;
                                   f) O usuário não coletará ou instalará informações pessoais sobre quaisquer outros indivíduos na FlameIT, ou de outra maneira perseguirá, 
                                   contatará repetidamente ou assediará outros usuários;
                                   g) O usuário não assumirá a identidade de qualquer outra pessoa ou fraudará uma relação de qualquer pessoa ou entidade, incluindo a FlameIT. O 
                                   usuário não adotará identidade falsa com o propósito de iludir ou fraudar alguém;
                                   h) O usuário não usará os serviços para, de qualquer maneira, sujeitar menores de idade a perigos ou para encorajar envolvimentos de natureza 
                                   sexual com menores de idade;
                                   i) O usuário não manipulará os serviços com o intuito de esconder sua identidade ou participação nos serviços ou na FlameIT (por meio do uso da 
                                   identidade de outra pessoa, ou de qualquer maneira modificar qualquer outro identificativo possível);
                                   j) O usuário não divulgará nenhum conteúdo que contenha quaisquer vírus, Cavalos de Tróia, códigos nocivos, ou qualquer programa de computador 
                                   criado para interromper os serviços por nós prestados. O usuário não poderá tentar impedir os usuários de usufruir nossos serviços, tampouco 
                                   tentar danificar o funcionamento adequado de programas, disco rígido ou equipamentos e materiais necessários ao funcionamento dos serviços;
                                   k) O usuário não enviará quaisquer anúncios publicitários, informações promocionais, e-mail ou outros materiais não solicitados (incluindo, mas 
                                   não se limitando a correspondência inútil, "spam", cartas-correntes, ou "pirâmides" de qualquer espécie) para quaisquer pessoas por meio do uso 
                                   dos serviços.
                                   l) O usuário não enviará nenhum conteúdo que viole direitos autorais.
   
                                5.2. Se o usuário violar quaisquer das disposições acima estabelecidas, ou quaisquer outros aspectos destas condições, 
                                a FlameIT se reserva o direito de suspender ou rescindir os direitos do usuário de fazer uso dos serviços, independentemente do envio de qualquer 
                                notificação.



                                  6. Divulgação de dados cadastrais do usuário

                                6.1. A FlameIT se compromete a não divulgar espontaneamente qualquer dado pessoal do usuário que faça parte de seu cadastro. Porém, tal atitude 
                                poderá ocorrer nas seguintes hipóteses:
                                   a) para cumprir disposição legal;
                                   b) para cumprir algum procedimento legal, inclusive para cumprir qualquer ordem judicial ou de qualquer órgão regulatório ou administrativo 
                                   competente; ou
                                   c) para proteger direitos, propriedades, interesses ou manter a segurança da FlameIT.


                                  7. Informações enviadas pelos usuários

                                7.1. A FlameIT somente poderá se utilizar de informações, opiniões ou descrições de fatos enviados pelos usuários aa FlameIT caso seja 
                                autorizado pelos próprios.
                                7.2. A responsabilidade pela correção e veracidade do conteúdo, bem como por todas as conseqüências decorrentes destas informações, opiniões ou 
                                descrição de fatos é exclusivamente do usuário que os enviou aa FlameIT.


                                  8. Responsabilidade

                                8.1. A FlameIT não se responsabiliza por danos ou prejuízos decorrentes da má utilização dos serviços e informações constantes da FlameIT.
                                8.2. A FlameIT não se responsabiliza pelo conteúdo enviado pelos usuários.
                                8.3. A FlameIT não se responsabiliza pelas opiniões emitidas por pessoas, organizações, associações, enfim, entidades, de caráter filantrópico, 
                                comercial, ideológico, acadêmico, religioso, ou qualquer outro assunto, constantes da FlameIT, bem como pelos efeitos do seu uso.
                                8.4. A FlameIT não se responsabiliza pelo conteúdo de Web sites, páginas de Internet e serviços de terceiros para os quais possua links 
                                (apontadores, canais de ligação), bem como pelos efeitos de seu uso.
                                8.5. A FlameIT não se responsabiliza pela utilização contra o usuário de informação, opinião ou fato divulgado, mesmo de ordem pessoal, pelo 
                                usuário enquanto no uso do serviço de fórum, bem como não se responsabiliza por qualquer prejuízo, dano, vexame, sofrimento ou qualquer problema que 
                                venha a ser causado por este fato.
                                8.6. A FlameIT não tem nenhuma responsabilidade pela correção e veracidade do conteúdo, bem como por todas as conseqüências decorrentes das 
                                informações, opiniões ou descrição de fatos enviadas pelos usuários aa FlameIT.
                                8.7. Por serem gratuitos os serviços, a FlameIT se reserva no direito de modificar, suspender ou interromper temporária ou permanentemente o seu 
                                fornecimento, a seu exclusivo critério, sem que implique em qualquer tipo de responsabilidade por isto.
                                8.8. Em razão da gratuidade dos serviços, a FlameIT não garante a inexistência de eventuais erros em sua prestação.
                                8.9. A FlameIT não tem nenhuma responsabilidade pelos materiais disponibilizados pelos usuários.
                                8.9. A FlameIT não tem responsabilidade nas demais hipóteses não previstas neste termos e condições de uso, bem como casos omissos, se reservando 
                                o direito de decidir a seu critério sobre os casos omissos.


                                  9. Contrato e foro

                                9.1. Este contrato será regido e interpretado de acordo com as leis da República Federativa do Brasil. Estas condições e suas alterações periódicas 
                                constituem todo o entendimento celebrado entre os usuários e a FlameIT e devem prevalecer sobre quaisquer entendimentos e contratos contemporâneos 
                                ou anteriores, realizados entre você e a FlameIT, tanto orais quanto escritos, em conexão com os assuntos relacionados a essas condições. Se 
                                quaisquer das provisões destas condições ou a aplicação das mesmas a qualquer usuário ou circunstâncias for considerada, em qualquer extensão, 
                                inválida ou inexeqüível, o restante destas Condições, ou aplicação de tais provisões às pessoas e circunstâncias as quais não forem consideradas 
                                inválidas ou inexeqüíveis deverão permanecer em vigor, e cada provisão será válida e executada dentro da extensão permitida por lei. Nenhuma 
                                renúncia destas condições deverá ser válida, exceto que tal renúncia seja feita por escrito e assinada por um diretor da FlameIT. Se a FlameIT 
                                for impedido de cumprir quaisquer obrigações previstas neste Contrato por motivos de força maior ou caso fortuito, o usuário aceitará tal 
                                descumprimento, renunciando a quaisquer indenizações que digam respeito a esses fatos. Qualquer dúvida será dirimida no foro da comarca de São Paulo.
                            
                            </pre>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <p class="alert alert-info">
                      Ao clicar no bot&atilde;o abaixo voc&ecirc; estar&aacute; concordando com os termos de uso.
                  </p>
                  <button class="btn btn-default" data-bind="click: Salvar">Criar conta</button>
              </div>
              <div class="col-md-2">
              </div>
          </div>
          <div class="panel-footer">
            <span class="copyright">Copyright © flameIt 2014 / powered by CMSx</span>
          </div>
      </div>
    </div><!-- /.container -->
    <!-- Modal -->
    <div class="modal fade" id="instModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h4 class="modal-title" id="myModalLabel">Progresso da instala&ccedil;&atilde;o</h4>
          </div>
          <div class="modal-body">
            <div class="progress">
              <div class="progress-bar progress-bar-striped active" role="progressbar" aria-valuenow="45" aria-valuemin="0" aria-valuemax="100" style="width: 45%">
                <span class="sr-only"></span>
              </div>
            </div>
          </div>
          <div class="modal-footer">
          </div>
        </div>
      </div>
    </div>

    <div id="success" class="alert alert-success  hidden" role="alert">
        Parab&eacute;ns! Seu cadastro foi efetuado com sucesso! <br />
        Agora voc&ecirc; deve verificar o email cadastrado, para onde enviamos a mensagem de valida&ccedil;&atilde;o!
    </div>

    <div id="fail" class="alert alert-danger hidden" role="alert">
        Houve uma falha ao efetuar seu cadastro. Tente novamente mais tarde.
    </div>

<script>

    function getTemplates() {
        var h = $.ajax({
            url: '<%Response.Write(ConfigurationManager.AppSettings["templatesws"].ToString());%>',
                type: "GET",
                dataType:'json',
                success: function (result) {
                    //$("#div1").html(result);
                    return result;
                },
                cache: false,
                async: false
            }).responseText;
            h.replace(/\\n/g, "\\n")
             .replace(/\\'/g, "\\'")
             .replace(/\\"/g, '\\"')
             .replace(/\\&/g, "\\&")
             .replace(/\\r/g, "\\r")
             .replace(/\\t/g, "\\t")
             .replace(/\\b/g, "\\b")
             .replace(/\\f/g, "\\f");
            return $.parseJSON(h);
        }

    function getVal(value) {
        var h = $.ajax({
            url: '<%Response.Write(ConfigurationManager.AppSettings["acomp"].ToString());%>',
            type: "POST",
            data: { prefixText: value, count: 3 },
            // dataType:'json',,

            success: function (result) {
                //$("#div1").html(result);
                return result.count;
            },
            cache: false,
            async: false
        }).responseText;
        h.replace(/\\n/g, "\\n")
         .replace(/\\'/g, "\\'")
         .replace(/\\"/g, '\\"')
         .replace(/\\&/g, "\\&")
         .replace(/\\r/g, "\\r")
         .replace(/\\t/g, "\\t")
         .replace(/\\b/g, "\\b")
         .replace(/\\f/g, "\\f");
        return $.parseJSON(h).count;
    };

    var d = new Date();
    var data = d.getUTCFullYear() + "/" + (d.getMonth() + 1) + "/" + d.getDate();

    ko.bindingHandlers.htmlValue = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            var updateHandler = function () {
                var modelValue = valueAccessor(),
                    elementValue = element.value;
                //update the value on keyup
                modelValue(elementValue);
            };

            ko.utils.registerEventHandler(element, "keyup", updateHandler);
            ko.utils.registerEventHandler(element, "input", updateHandler);
        },
        update: function (element, valueAccessor) {
            var value = ko.utils.unwrapObservable(valueAccessor()) || "",
                //current = element.innerHTML;
                current = getVal(value);

            //if (value !== current) {
            if (value == "") {
                $("#retMsg").html("Digite o nome desejado!");
            }
            else {
                if (current == 0) {
                    $("#retMsg").html("Nome dispon&iacute;vel!");
                    element.value = value;
                }
                else {
                    $("#retMsg").html("Este nome j&aacute; existe!");
                    element.value = value;
                }
            }
        }
    };

    var appId  = "<%Response.Write(System.Guid.NewGuid());%>";
    var userId = "<%Response.Write(System.Guid.NewGuid());%>";

    // Constructor for an object with two properties
    var temas = function (name, url)
    {
        this.tname = name;
        this.turl = url;
    };

    var viewModel = {
        userName: ko.observable(),
        name: ko.observable(),
        email: ko.observable(),
        surname: ko.observable(),
        senha: ko.observable(),       
        availThemes: ko.observableArray(
            <%/*new temas('Basico I','_Layout.cshtml'),
            new temas('Basico II','_LayoutBasic.cshtml')
            new temas('OnePage','_LayoutFlame.cshtml'),
            new temas('Loja','_LayoutLoja.cshtml') */%>
            getTemplates()
        ),
        Salvar: function () {
            //$('#instModal').modal('show');
            var m = $.ajax({
                url: "<%Response.Write(ConfigurationManager.AppSettings["userws"].ToString());%>",
                //async: false,
                type: "POST",
                data: JSON.stringify(
                    {
                        "usuario": {
                            "UserId": userId,
                            "Apelido": this.userName(),
                            "Nome": this.name(),
                            "Sobrenome": this.surname(),
                            "Senha": this.senha(),
                            "Email" : this.email(),
                            "Aplicacao": this.userName(),
                            "Template": this.availThemes()[0].turl,
                            "AplicacaoId": appId,
                            "DataInclusao": data
                        }
                    }
                ),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                error: function (jqXHR, textStatus, errorThrown) {
                    alert(jqXHR + "-" + textStatus + "-" + errorThrown);
                },
                success: function (result) {
                    //$("#div1").html(result);
                    return result.valid;
                },
                cache: false,
                async: false
            }).responseText;
            m.replace(/\\n/g, "\\n")
             .replace(/\\'/g, "\\'")
             .replace(/\\"/g, '\\"')
             .replace(/\\&/g, "\\&")
             .replace(/\\r/g, "\\r")
             .replace(/\\t/g, "\\t")
             .replace(/\\b/g, "\\b")
             .replace(/\\f/g, "\\f");
           gomsg($.parseJSON(m).valid);
        }
    };

    function gomsg(val)
    {
        if(val==true)
        {
            //$("#success").toogleClass("hidden", true).toogleClass("show", true);
        }
        else
        {
           // $("#fail").toogleClass("hidden", true).toogleClass("show", true);
        }
    }

    ko.applyBindings(viewModel);

    </script>
</asp:Content>
