<%@ Page Title="Purple Social Science - Senha alterada" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ChangePasswordSuccess.aspx.cs" Inherits="Account_ChangePasswordSuccess" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
 <meta http-equiv="refresh" content="5;URL='Login.aspx'" />  
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
      Salvo com sucesso!
    </h2>
    <p>
        <%=titulo %>
        <br /><br />
        <b>Aguarde! <br /> Estamos atualizando as informações...</b>
    </p>
</asp:Content>
