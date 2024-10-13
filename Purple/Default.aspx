<%@ Page Title="Purple Social Science" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
     <br clear="all" />
    <h2>
        Bem vindo a Purple
    </h2>
    <br clear="all" />
    <div style="float:left;width:450px;" >
    <p style="float:left;">
       Rede social sobre ciência dos dados.
       <br />
        Ciência de dados (em inglês: data science) é uma área interdisciplinar voltada para o estudo e a análise de dados, estruturados ou não, que visa a extração de conhecimento ou insights para possíveis tomadas de decisão, de maneira similar à mineração de dados.
    </p>
    <p>Por isso criamos uma rede social para discutir e aprender mais sobre essa febre que vem aumentando a cada dia que passa.</p>
    </div>
    <div id="dvLogin" style="float:left;margin: -42px -2px -9px 33px;"> 
            <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
            </span>
            <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification" />
            <div class="accountInfo">
                <fieldset class="login">
                    <legend>Entrar</legend>
                    <p>
                        <asp:Label ID="UserNameLabel" runat="server">E-mail:</asp:Label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="textEntry"></asp:TextBox>
                    </p>
                    <p>
                        <asp:Label ID="PasswordLabel" runat="server">Senha:</asp:Label>
                        <asp:TextBox ID="txtPass" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                    </p>
                    <p>
                        <asp:CheckBox ID="RememberMe" runat="server"/>
                        <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Salvar senha</asp:Label>
                    </p>
                    <p style="float:left;">
                        <asp:LinkButton ID="lnkEsqueci"
                            runat="server" onclick="lnkEsqueci_Click">Esqueci a minha senha</asp:LinkButton>
                    </p>
                    <p style="float:left;">
                        <asp:LinkButton ID="lnkNovaConta" PostBackUrl="Register.aspx" runat="server">Criar Conta</asp:LinkButton>
                    </p>
                   <p class="submitButton">
                    <asp:Button ID="LoginButton" runat="server" Text="Entrar" onclick="LoginButton_Click" TabIndex="0"/>
                </p>
                </fieldset>

            </div>
    </div>
</asp:Content>
