<%@ Page Title="Purple Social Science - Comunidades" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Comunidades.aspx.cs" Inherits="Comunidades" %>

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

         <div id="dvComunidades" runat="server" class="accountFoto divFlutuantesEsquerda">
            <fieldset class="register fieldsetPortalPostagem">
                <legend>Comunidades</legend>

                <asp:Literal ID="ltrFotos" runat="server"></asp:Literal>
            </fieldset>
        </div>

         <div id="dvDiscussao" runat="server" visible="false" class="accountFoto divFlutuantesEsquerda">
            <fieldset class="register fieldsetPortalPostagem">
                <legend> <asp:LinkButton ID="lnkcomunidadesant" runat="server" PostBackUrl="~/Account/Comunidades.aspx"> Comunidades</asp:LinkButton> De <asp:LinkButton ID="lnkComunidade" runat="server"> Comunidades</asp:LinkButton></legend>


               <p> <asp:Button ID="btnCriar" runat="server" Text="Criar novo Tópico?" 
                       onclick="btnCriar_Click" /></p><br clear="all" />

                   <asp:GridView ID="gvTopicos" runat="Server" AutoGenerateColumns="False" HorizontalAlign="Left"
                    DataKeyNames="CMD_ID" EnableModelValidation="True"
                    AllowSorting="false"
                    PageSize="20" AllowPaging="false" CssClass="mGrid" 
                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                    EmptyDataText="Não há Tópicos criados para visualizar" Width="450px" 
                    onrowdatabound="gvTopicos_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Tópicos " SortExpression="CMD_TIT">
                            <ItemTemplate>
                                <a style="cursor:pointer;"><%# Eval("CMD_TIT").ToString()%></a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Data de Criação " SortExpression="CMD_DAT_INC">
                            <ItemTemplate>
                            <a style="cursor:pointer;"> <%# Eval("CMD_DAT_INC").ToString()%></a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </fieldset>
        </div>
</asp:Content>