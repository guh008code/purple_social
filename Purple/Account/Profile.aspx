<%@ Page Title="Purple Social Science - Profile" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Profile.aspx.cs" Inherits="Profile_perfil" %>

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
                <asp:ImageButton ID="imgPerfil" ImageUrl="~/images/perfil/perfil.png" Width="150px" runat="server" onclick="imgBtnFoto_Click" />
            </fieldset>

           <fieldset id="fdAmizade" runat="server" visible="false" class="register fieldsetPortal">
                <legend>Nova amizade</legend>
               <asp:Button ID="btnAmizade" runat="server" Text="Adicionar como amigo" 
                    onclick="btnAmizade_Click" />
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
                <legend>Sobre <%=nome%></legend>
                <p>
                   <asp:Literal ID="ltrSobreMim" runat="server"></asp:Literal>
                </p>
            </fieldset>

            <fieldset class="register fieldsetPortalPostagem">
                <legend> <asp:Literal ID="ltrCurtidas" runat="server"></asp:Literal></legend>
                <p><asp:LinkButton ID="lnkCurtir" runat="server" onclick="lnkCurtir_Click" Text="Recomendar"></asp:LinkButton> </p> 
            </fieldset>

            <fieldset class="register fieldsetPortalPostagem">
                <legend>Amizades</legend>
                 <asp:Literal ID="ltrAmigos" runat="server"></asp:Literal>
            </fieldset>

            <fieldset id="fdAmigoComum" runat="server" class="register fieldsetPortalPostagem">
                <legend>Amigo(s) em Comum</legend>
                 <asp:Literal ID="ltrAmigoEmComum" runat="server"></asp:Literal>
            </fieldset>

           <fieldset class="register fieldsetPortalPostagem">
                <legend>Fotos</legend>
                <asp:Literal ID="ltrFotos" runat="server"></asp:Literal>
            </fieldset>

           <fieldset class="register fieldsetPortalPostagem">
                <legend>Minhas Comunidades</legend>
                <asp:Literal ID="ltrMinhasComunidades" runat="server"></asp:Literal>
            </fieldset>

            <fieldset id="dvInteresses" runat="server" visible="false" class="register fieldsetPortalPostagem">
                <legend>Interesses em comum</legend>
                <p><asp:Literal ID="ltrInteresses" runat="server"></asp:Literal></p>
            </fieldset>
        </div>
</asp:Content>