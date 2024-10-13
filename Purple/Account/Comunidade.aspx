<%@ Page Title="Purple Social Science - Comunidades" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Comunidade.aspx.cs" Inherits="Comunidades" %>

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

                <asp:ImageButton ID="imgPerfil" ImageUrl="~/images/perfil/perfil.png" Width="150px" runat="server" onclick="imgBtnFoto_Click"  />

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
                <legend> <asp:LinkButton ID="lnkcomunidadesant" runat="server" PostBackUrl="~/Account/Comunidades.aspx"> Comunidades</asp:LinkButton> De <asp:LinkButton ID="lnkComunidade" runat="server"> Comunidades</asp:LinkButton></legend>

                <p><b>Título:</b> <asp:TextBox  Style="margin-left:22px;" ID="txt_titulo" placeholder="Título do tópico" runat="server" Width="350px" MaxLength="50"></asp:TextBox></p>
                <p><b>Descrição:</b> <asp:TextBox  ID="txt_descricao" runat="server" 
                        placeholder="Descrição..." TextMode="MultiLine" 
                        CssClass="textEntry" Height="100px" Width="350px"></asp:TextBox></p>
                
                <p style="float:right;"><asp:Button ID="btnCriar" runat="server" Text="Criar Tópico" 
                        onclick="btnCriar_Click" /> <asp:Button ID="btnCancelar" runat="server" 
                        Text="Cancelar" onclick="btnCancelar_Click" /></p>
                
                <p style="float:right;"><asp:Button ID="btnResponder" runat="server" 
                        Text="Responder" onclick="btnResponder_Click" Visible="false"/></p>
            </fieldset>

            <fieldset id="fieldPostagens" runat="server" visible="false" class="register fieldsetPortalPostagem">
              <legend>Respostas</legend>
               <asp:Literal ID="ltrPostagens" runat="server"></asp:Literal>
            </fieldset>

            <fieldset id="fieldResposta" runat="server" visible="false" class="register fieldsetPortalPostagem">
                       <p><asp:TextBox  ID="txt_resposta" runat="server" 
                        placeholder="Descrição..." TextMode="MultiLine" 
                        CssClass="textEntry" Height="50px" Width="400px"></asp:TextBox></p>

                        <p style="float:right;"><asp:Button ID="btnSalvarResposta" runat="server" 
                                Text="Salvar Resposta" onclick="btnSalvarResposta_Click"/> 
                            <asp:Button ID="btnCancelarResposta" runat="server" Text="Cancelar" 
                                onclick="btnCancelarResposta_Click"/></p>
            </fieldset>
        </div>
</asp:Content>