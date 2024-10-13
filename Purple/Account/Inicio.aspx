<%@ Page Title="Purple Social Science - Início" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Inicio.aspx.cs" Inherits="Inicio" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        function ismaxlength(obj) {
            var mlength = obj.getAttribute ? parseInt(obj.getAttribute("maxlength")) : ""
            if (obj.getAttribute && obj.value.length > mlength) {
                obj.value = obj.value.substring(0, mlength)
            }
        }
      </script>
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

         <div class="divFlutuantesEsquerda">
            <fieldset class="register fieldsetPortalPostagem">
                <legend>Postar</legend>
                <p>
                    <asp:TextBox style="float:left;" ID="txt_post" runat="server" 
                        placeholder="Descreva a sua postagem aqui..." TextMode="MultiLine" 
                        CssClass="textEntry" Height="50px" Width="324px"></asp:TextBox>
                    <asp:Button style="float:left;margin-left:2px;" ID="btnPublicar" runat="server" Text="Publicar" Width="92px" Height="55px" 
                        onclick="btnPublicar_Click" />
                </p>
                <br clear="all" />
                <p style="float:right;display:none;"> <asp:Button ID="btnFotos" Enabled="false" runat="server" Text="Fotos" /> <asp:Button ID="btnVideos" Enabled="false" runat="server" Text="Videos" /> </</p>
            </fieldset>

            <fieldset id="dvSugestoesAmizades" runat="server" class="register fieldsetPortalPostagem">
                <legend>Sugestões de amizade</legend>
                <p>Não há novas sugestões de amizade</p>
            </fieldset>

           <fieldset class="register fieldsetPortalPostagem">
                <legend>Postagens</legend>
               <asp:Literal ID="ltrPostagens" runat="server"></asp:Literal>
            </fieldset>
        </div>

            <div id="dvInteresses" runat="server" visible="false" class="divFlutuantesEsquerda">
            <fieldset class="register fieldsetPortalLateral">
                <legend><asp:Literal ID="ltrTitulo" runat="server"></asp:Literal></legend>

                <asp:Literal ID="ltrInteresses" runat="server"></asp:Literal>
                
            </fieldset>
            </div>
</asp:Content>