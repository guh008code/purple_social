<%@ Page Title="Purple Social Science - Configurações" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Register.aspx.cs" Inherits="Account_Register" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="../Scripts/jquery.maskMoney.min.js" type="text/javascript"></script>
    <script src="../Scripts/mascaras.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
           <div class="accountFoto divFlutuantesEsquerda">
            <fieldset class="register fieldsetPortalPerfil">
                <legend>Perfil</legend>

                <asp:ImageButton ID="imgPerfil" ImageUrl="~/images/perfil/perfil.png" 
                    Width="150px" runat="server" onclick="imgBtnFoto_Click" />

            </fieldset>

           <fieldset class="register fieldsetPortal">
                <legend>Amizade</legend>
                <p><asp:LinkButton ID="lnkAmigos" PostBackUrl="Amigos.aspx" runat="server">(0) Amigos</asp:LinkButton></p>
                <p><asp:LinkButton ID="lnkSolicitacoes" Visible="false" PostBackUrl="Amigos.aspx" runat="server">(0) Solicitações</asp:LinkButton></p>
            </fieldset>

           <fieldset class="register fieldsetPortal">
                <legend>Fotos</legend>
                <p><asp:LinkButton ID="lnkFotos" PostBackUrl="Fotos.aspx" runat="server">(0) Fotos</asp:LinkButton></p>
            </fieldset>

           <fieldset class="register fieldsetPortal">
                <legend>Comunidades</legend>
                <p><asp:LinkButton ID="lnkComunidades" PostBackUrl="Comunidades.aspx" runat="server">(0) Comunidades</asp:LinkButton></p>
            </fieldset>
        </div>
           
        <div class="accountFoto divFlutuantesEsquerda">
            <fieldset class="register fieldsetPortalPostagem">
                <legend>Configurações</legend>
         <span class="failureNotification">
            <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
        </span>
        <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification" />
                    <p>
                    Preencha o cadastro abaixo para atualizar o seus dados.
                </p>
                <p>
                    <asp:Label ID="UserNameLabel" runat="server">Nome:</asp:Label><br />
                    <asp:TextBox ID="txt_nom" runat="server" CssClass="textEntry"></asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="ldlDataNasc" runat="server">Data Nascimento:</asp:Label><br />
                    <asp:TextBox ID="txt_dataNasc" runat="server" CssClass="textEntry" placeHolder="Dia/Mês/Ano" MaxLength="10" onkeyup="Formatadata(this,event);" onblur="javascript:f_valida_data(this);" onkeypress="javascript:f_filtra_teclas_data();"></asp:TextBox>
                </p>
               <p>
                    <asp:Label ID="lblSex" runat="server">Gênero:</asp:Label><br />
                   <asp:DropDownList ID="ddl_sexo" runat="server" CssClass="dropdown">
                   <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                   <asp:ListItem Text="Feminino" Value="feminino"></asp:ListItem>
                   <asp:ListItem Text="Masculino" Value="masculino"></asp:ListItem>
                   <asp:ListItem Text="Outros" Value="outros"></asp:ListItem>
                   </asp:DropDownList>
                </p>

               <p>
                   <asp:Label ID="Label4" runat="server">Área de atuação em que você trabalha:</asp:Label><br />
                   <asp:DropDownList ID="ddl_trabalho" runat="server" CssClass="dropdown">
                   <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                   <asp:ListItem Text="Administração de Banco de Dados" Value="1"></asp:ListItem>
                   <asp:ListItem Text="Administração de Redes" Value="2"></asp:ListItem>
                   <asp:ListItem Text="Arquitetura da Informação" Value="3"></asp:ListItem>
                   <asp:ListItem Text="E-Commerce" Value="4"></asp:ListItem>
                   <asp:ListItem Text="Processamento de Dados" Value="5"></asp:ListItem>
                   <asp:ListItem Text="Programação" Value="6"></asp:ListItem>
                   <asp:ListItem Text="Qualidade de Software" Value="7"></asp:ListItem>
                   <asp:ListItem Text="Segurança da Informação" Value="8"></asp:ListItem>
                   <asp:ListItem Text="Sistemas" Value="9"></asp:ListItem>
                   <asp:ListItem Text="Suporte Técnico em Informática" Value="10"></asp:ListItem>
                   <asp:ListItem Text="Tecnologia da Informação" Value="11"></asp:ListItem>
                   </asp:DropDownList>
                </p>

               <p>
                    <asp:Label ID="Label5" runat="server">Nível de seu conhecimento:</asp:Label><br />
                   <asp:DropDownList ID="ddl_conhecimento" runat="server" CssClass="dropdown">
                   <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                   <asp:ListItem Text="Estagiário" Value="1"></asp:ListItem>
                   <asp:ListItem Text="Trainee" Value="2"></asp:ListItem>
                   <asp:ListItem Text="Junior" Value="3"></asp:ListItem>
                   <asp:ListItem Text="Pleno" Value="4"></asp:ListItem>
                   <asp:ListItem Text="Senior" Value="5"></asp:ListItem>
                   <asp:ListItem Text="Consultor" Value="6"></asp:ListItem>
                   <asp:ListItem Text="Especialista" Value="7"></asp:ListItem>
                   </asp:DropDownList>
                </p>

                <p>
                    <asp:Label ID="Label8" runat="server">Linguagem de Programação Preferida:</asp:Label><br />
                   <asp:DropDownList ID="ddl_linguagens" runat="server" CssClass="dropdown">
                   <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                   <asp:ListItem Text="JavaScript" Value="JavaScript"></asp:ListItem>
                   <asp:ListItem Text="Java" Value="Java"></asp:ListItem>
                   <asp:ListItem Text="Python" Value="Python"></asp:ListItem>
                   <asp:ListItem Text="C#" Value="C#"></asp:ListItem>
                   <asp:ListItem Text="PHP" Value="PHP"></asp:ListItem>
                   <asp:ListItem Text="C++" Value="C++"></asp:ListItem>
                   <asp:ListItem Text="C" Value="C"></asp:ListItem>
                   <asp:ListItem Text="TypeScript" Value="TypeScript"></asp:ListItem>
                   <asp:ListItem Text="Ruby" Value="Ruby"></asp:ListItem>
                   <asp:ListItem Text="Swift" Value="Swift"></asp:ListItem>
                   </asp:DropDownList>
                </p>

               <p>
                    <asp:Label ID="Label2" runat="server">Data de Admissão do seu Trabalho:</asp:Label><br />
                    <asp:TextBox ID="txt_dat_adm" runat="server" CssClass="textEntry" placeHolder="Dia/Mês/Ano" MaxLength="10" onkeyup="Formatadata(this,event);" onblur="javascript:f_valida_data(this);" onkeypress="javascript:f_filtra_teclas_data();"></asp:TextBox>
                </p>

              <p>
                    <asp:Label ID="Label6" runat="server">Sálario Atual:</asp:Label><br />
                    <asp:TextBox ID="txt_salario" runat="server" CssClass="textEntry" MaxLength="18"></asp:TextBox>
                </p>

               <p>
                    <asp:Label ID="Label3" runat="server">Cidade em que você trabalha:</asp:Label><br />
                    <asp:TextBox ID="txt_cidade" runat="server" CssClass="textEntry" MaxLength="50"></asp:TextBox>
                </p>

                <p>
                    <asp:Label ID="Label1" runat="server">Estado em que você trabalha:</asp:Label><br />
                   <asp:DropDownList ID="ddl_estado" runat="server" CssClass="dropdown">
                   <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                   <asp:ListItem Text="Acre" Value="AC"></asp:ListItem>
                   <asp:ListItem Text="Alagoas" Value="AL"></asp:ListItem> 
                   <asp:ListItem Text="Amapá" Value="AP"></asp:ListItem>
                   <asp:ListItem Text="Amazonas" Value="AM"></asp:ListItem>
                   <asp:ListItem Text="Bahia" Value="BA"></asp:ListItem>
                   <asp:ListItem Text="Ceará" Value="CE"></asp:ListItem>
                    <asp:ListItem Text="Distrito Federal" Value="DF"></asp:ListItem>
                    <asp:ListItem Text="Espírito Santo" Value="ES"></asp:ListItem>
                    <asp:ListItem Text="Goiás" Value="GO"></asp:ListItem>
                    <asp:ListItem Text="Maranhão" Value="MA"></asp:ListItem>
                    <asp:ListItem Text="Mato Grosso" Value="MT"></asp:ListItem>
                    <asp:ListItem Text="Mato Grosso do Sul" Value="MS"></asp:ListItem>
                    <asp:ListItem Text="Minas Gerais" Value="MG"></asp:ListItem>
                    <asp:ListItem Text="Pará" Value="PA"></asp:ListItem>
                    <asp:ListItem Text="Paraíba" Value="PB"></asp:ListItem>
                    <asp:ListItem Text="Paraná" Value="PR"></asp:ListItem>
                    <asp:ListItem Text="Pernambuco" Value="PE"></asp:ListItem>
                    <asp:ListItem Text="Piauí" Value="PI"></asp:ListItem>
                    <asp:ListItem Text="Rio de Janeiro" Value="RJ"></asp:ListItem>
                    <asp:ListItem Text="Rio Grande do Norte" Value="RN"></asp:ListItem>
                    <asp:ListItem Text="Rio Grande do Sul" Value="RS"></asp:ListItem>
                    <asp:ListItem Text="Rondônia" Value="RO"></asp:ListItem>
                    <asp:ListItem Text="Roraima" Value="RR"></asp:ListItem>
                    <asp:ListItem Text="Santa Catarina" Value="SC"></asp:ListItem>
                    <asp:ListItem Text="São Paulo" Value="SP"></asp:ListItem>
                    <asp:ListItem Text="Sergipe" Value="SE"></asp:ListItem>
                    <asp:ListItem Text="Tocantins" Value="TO"></asp:ListItem>
                   </asp:DropDownList>
                </p>


                <p>
                    <asp:Label ID="EmailLabel" runat="server">E-mail:</asp:Label><br />
                    <asp:TextBox ID="txt_mail" runat="server" CssClass="textEntry"></asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="PasswordLabel" runat="server">Senha:</asp:Label><br />
                    <asp:TextBox ID="Password" placeHolder="*******" runat="server" CssClass="passwordEntry" TextMode="Password" MaxLength="10"></asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="ConfirmPasswordLabel" runat="server">Confirmar senha:</asp:Label><br />
                    <asp:TextBox ID="ConfirmPassword" placeHolder="*******" runat="server" CssClass="passwordEntry" TextMode="Password" MaxLength="10"></asp:TextBox>
                </p>

               <p>
                    <asp:Label ID="Label7" runat="server"><b>Opurple assinatura:</b></asp:Label><br />
                   <asp:RadioButtonList ID="rd_assinatura" runat="server" RepeatDirection="Horizontal">
                   <asp:ListItem Text="Gratuita" Value="0" Selected="True"></asp:ListItem>
                   <asp:ListItem Text="Mensalmente" Value="1"></asp:ListItem>
                   </asp:RadioButtonList>
                </p>

                <p class="submitButton">
                <asp:Button ID="CreateUserButton" runat="server" Text="Salvar cadastro" onclick="cadastro_Click"  />
            </p>
            </fieldset>
        </div>
</asp:Content>