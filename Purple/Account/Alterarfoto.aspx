<%@ Page Title="Purple Social Science - Início" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Alterarfoto.aspx.cs" Inherits="AlterarFoto" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:HiddenField ID="hidden_foto" runat="server" />
        <div class="accountFoto divFlutuantesEsquerda">
            <fieldset class="register fieldsetPortalPerfil">
                <legend>Perfil</legend>

                <asp:ImageButton ID="imgPerfil" ImageUrl="~/images/perfil/perfil.png" Width="150px" runat="server" />

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
        </div>

         <div class="divFlutuantesEsquerda">
            <fieldset class="register fieldsetPortalPostagem">
                <legend>Alterar foto do perfil</legend>
                <p>
                    <asp:FileUpload ID="upload_perfil" runat="server" CssClass="textEntry" Width="400px"  />
                    </p>
                    <p style="float:right;">
                    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" Width="100px" Height="30px" 
                            onclick="btnSalvar_Click" />
                    <asp:Button ID="btnCancelar" Visible="false" runat="server" Text="Cancelar" Height="20px" 
                            onclick="btnCancelar_Click" />
                    </p>
                
            </fieldset>
        </div>

            <div class="divFlutuantesEsquerda">
            <fieldset class="register fieldsetPortalLateral">
                <legend>Conheça</legend>
                
                    <img style="float:left;" src="../images/icones/community-icon-6.png" width="50px" /> <p style="float:left;margin-top:10px"> Assunto 1</p><br clear="all" /><br clear="all" />

                    <img style="float:left;" src="../images/icones/community-icon-6.png" width="50px" /> <p style="float:left;margin-top:10px"> Assunto 2</p><br clear="all" /><br clear="all" />

                    <img style="float:left;" src="../images/icones/community-icon-6.png" width="50px" /> <p style="float:left;margin-top:10px"> Assunto 3</p><br clear="all" /><br clear="all" />

                    <img style="float:left;" src="../images/icones/community-icon-6.png" width="50px" /> <p style="float:left;margin-top:10px"> Assunto 4</p><br clear="all" /><br clear="all" />

            </fieldset>
            </div>
</asp:Content>