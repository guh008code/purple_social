<%@ Page Title="Purple Social Science - Profile" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Relatorio.aspx.cs" Inherits="Relatorio" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
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
                <legend>Relatório de usuário(s) mais conectado(s)</legend>
                <asp:GridView ID="gvMaisConectados" runat="Server" AutoGenerateColumns="False" HorizontalAlign="Left"
                    DataKeyNames="USR_ID" EnableModelValidation="True"
                    AllowSorting="false"
                    PageSize="20" AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                    EmptyDataText="Não há usuário(s) conectado(s).">
                    <Columns>
                        <asp:TemplateField HeaderText="Usuário " SortExpression="USR_NOM">
                            <ItemTemplate>
                             <a href='Profile.aspx?perfil=<%# Eval("USR_ID").ToString()%>'><%# Eval("USR_NOM").ToString()%></a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="E-mail " SortExpression="USR_EMA">
                            <ItemTemplate>
                                <%# Eval("USR_EMA")%>
                            </ItemTemplate>
                            <ItemStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantidade de acessos " SortExpression="quantidade_acesso">
                            <ItemTemplate>
                                <%# Eval("quantidade_acesso")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </fieldset>

           <fieldset class="register fieldsetPortalPostagem">
                <legend>Relatório de usuários com mais amigos</legend>
                    <asp:GridView ID="gvMaisAmigos" runat="Server" AutoGenerateColumns="False" HorizontalAlign="Left"
                    EnableModelValidation="True"
                    AllowSorting="false"
                    PageSize="20" AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                    EmptyDataText="Não há relatórios">
                    <Columns>
                        <asp:TemplateField HeaderText="Usuário " SortExpression="USUARIO">
                            <ItemTemplate>
                             <a href='Profile.aspx?perfil=<%# Eval("ID_USUARIO").ToString()%>'><%# Eval("USUARIO").ToString()%></a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="E-mail " SortExpression="email">
                            <ItemTemplate>
                                <%# Eval("email")%>
                            </ItemTemplate>
                            <ItemStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantidade de Amigos " SortExpression="quantidade_amigos">
                            <ItemTemplate>
                                <%# Eval("quantidade_amigos")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </fieldset>








        </div>
</asp:Content>