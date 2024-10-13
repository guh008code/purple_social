<%@ Page Title="Purple Social Science - Fotos" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Foto.aspx.cs" Inherits="Foto" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
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

         <div class="divFlutuantesEsquerda">
            <fieldset class="register fieldsetPortalPostagem">
                <legend>Fotos de <asp:LinkButton ID="lnkFotosAlbum" runat="server"><%=nome %></asp:LinkButton> > Foto</legend>
                <p>
                <asp:Image ID="imgFoto" runat="server" CssClass="FotoAlbum" />
                </p>
                <p>  
                    <asp:Label ID="lblLegenda" runat="server">Legenda:</asp:Label><br />
                    <asp:TextBox ID="txt_legenda" runat="server" CssClass="textEntry" 
                        MaxLength="100" Width="382px"></asp:TextBox>
                </p>
                <p>
                    <asp:FileUpload ID="upload_perfil" runat="server" CssClass="textEntry" Width="400px"  />
                    </p>
                    <p id="dvBotoes" runat="server" style="float:right;">
                    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" Width="100px" Height="30px" 
                            onclick="btnSalvar_Click" />

                    </p>
                     <p style="float:left;">
                        <asp:Button ID="btnExcluir" Visible="false" runat="server" Text="Excluir foto" Width="100px" Height="30px"
                            onclick="btnCancelar_Click" />
                    </p>
                
            </fieldset>
        </div>
</asp:Content>