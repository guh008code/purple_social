<%@ Page Title="Purple Social Science - Alterar Senha" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ChangePassword.aspx.cs" Inherits="Account_ChangePassword" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Alterar Senha
    </h2>
    <p>
       Use os campo abaixo para alterar a sua senha.
    </p>
            <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
            </span>
            <asp:ValidationSummary ID="ChangeUserPasswordValidationSummary" runat="server" CssClass="failureNotification" 
                 ValidationGroup="ChangeUserPasswordValidationGroup"/>
            <div class="accountInfo">
                <fieldset class="changePassword">
                    <legend>Senhas</legend>
                    <p>
                        <asp:Label ID="CurrentPasswordLabel" runat="server">Código enviado Por E-mail:</asp:Label>
                        <asp:TextBox ID="CurrentPassword" runat="server" CssClass="passwordEntry" MaxLength="10"></asp:TextBox>
                    </p>
                    <p>
                        <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword">Nova senha:</asp:Label>
                        <asp:TextBox ID="NewPassword" runat="server" CssClass="passwordEntry" TextMode="Password" MaxLength="10"></asp:TextBox>
                    </p>
                    <p>
                        <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword">Confirmação da nova senha:</asp:Label>
                        <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="passwordEntry" TextMode="Password" MaxLength="10"></asp:TextBox>
                    </p>
                </fieldset>
                <p class="submitButton">
                    <asp:Button ID="CancelPushButton" runat="server" Text="Cancelar" 
                        PostBackUrl="~/Default.aspx" onclick="CancelPushButton_Click"/>
                    <asp:Button ID="ChangePasswordPushButton" runat="server" Text="Alterar senha" 
                        onclick="ChangePasswordPushButton_Click" />
                </p>
            </div>
</asp:Content>