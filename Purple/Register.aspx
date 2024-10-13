<%@ Page Title="Purple Social Science - Registre-se" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Register.aspx.cs" Inherits="Account_Register" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

        <h2>
            Criar nova conta
        </h2>
        <div style="float:left;width:350px;">
        <p>
            Preencha o cadastro abaixo para criar uma nova conta.
        </p>
        <p>
            A Senha deve conter no mínimo <%= Membership.MinRequiredPasswordLength %> caractêres.
        </p>
        </div>
        <div style="float:left;margin: -42px -2px -9px 33px;">
        <span class="failureNotification">
            <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
        </span>
        <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification" />
        <div class="accountInfo">
            <fieldset class="register">
                <legend>Cadastro</legend>
                <p>
                    <asp:Label ID="UserNameLabel" runat="server">Nome Completo:</asp:Label>
                    <asp:TextBox ID="txt_nom" runat="server" CssClass="textEntry"></asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="ldlDataNasc" runat="server">Data Nascimento:</asp:Label>
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
                    <asp:Label ID="EmailLabel" runat="server">E-mail:</asp:Label>
                    <asp:TextBox ID="txt_mail" runat="server" CssClass="textEntry"></asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="PasswordLabel" runat="server">Senha:</asp:Label>
                    <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password" MaxLength="10"></asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="ConfirmPasswordLabel" runat="server">Confirmar senha:</asp:Label>
                    <asp:TextBox ID="ConfirmPassword" runat="server" CssClass="passwordEntry" MaxLength="10" TextMode="Password"></asp:TextBox>
                </p>

                <p class="submitButton">
                <asp:Button ID="CancelPushButton" runat="server" Text="Cancelar" PostBackUrl="~/Default.aspx"/>
                <asp:Button ID="CreateUserButton" runat="server" Text="Salvar cadastro" onclick="cadastro_Click"  />
            </p>
            </fieldset>
        </div>
        </div>
</asp:Content>