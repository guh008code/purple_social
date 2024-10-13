<%@ Page Title="Purple Social Science - Entrar" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Account_Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<br clear="all" />
    <h2>
        Entrar
    </h2>
    <br clear="all" />
    <div style="float:left;width:350px;">
    <p>
        Informe o seu e-mail e senha para acessar e caso não tenha clique 
        <asp:HyperLink ID="RegisterHyperLink" style="text-decoration:none;" NavigateUrl="Register.aspx" runat="server" EnableViewState="false">AQUI</asp:HyperLink> para criar uma nova conta.
    </p>

        <p>
        Rede social sobre ciência dos dados.
        </p>
    </div>
    <div style="float:left;margin: -43px -2px -9px 33px;">
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
                        <asp:LinkButton ID="lnkNovaConta" PostBackUrl="~/Register.aspx" runat="server">Criar Conta</asp:LinkButton>
                    </p>
                   <p class="submitButton">
                    
                    <asp:Button ID="LoginButton" runat="server" Text="Entrar" onclick="LoginButton_Click" TabIndex="0"/>
                </p>
                </fieldset>

            </div>
            </div>
</asp:Content>