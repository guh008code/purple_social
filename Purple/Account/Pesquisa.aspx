<%@ Page Title="Purple Social Science - Pesquisa" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Pesquisa.aspx.cs" Inherits="Pesquisa" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
        <div class="accountFoto divFlutuantesEsquerda">
            <fieldset class="register fieldsetPortalPerfil">
                <legend>Perfil</legend>

                <asp:ImageButton ID="imgPerfil" ImageUrl="~/images/perfil/perfil.png" 
                    Width="150px" runat="server" onclick="imgPerfil_Click" />

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

         <div class="accountFoto divFlutuantesEsquerda">
            <fieldset class="register fieldsetPortalPesquisa">
                <legend>Itens encontrado(s) na pesquisa</legend>

                    <fieldset class="register fieldsetPortalResultado">
                        <legend>Pessoas encontrada(s)</legend>
                        <asp:Literal ID="ltrAmigos" runat="server"></asp:Literal>
                    </fieldset>
               
                    <fieldset class="register fieldsetPortalResultado">
                        <legend>Comunidades encontrada(s)</legend>

                        <asp:Literal ID="ltrComunidade" runat="server"></asp:Literal>

<%--                        <p style="width:280px">
                         <a href="#" title="foto"><img src="../images/icones/community-icon-6.png" width="65px" style="margin-left:0px;" /><br /> Comunidade</a>
                        </p>

                         <p>
                         <a href="#" title="foto"><img src="../images/icones/community-icon-6.png" width="65px" style="margin-left:0px;" /><br /> Comunidade</a>
                        </p>--%>
                    </fieldset>
                              
            </fieldset>

        </div>
</asp:Content>