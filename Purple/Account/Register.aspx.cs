using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Text;

public partial class Account_Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            carregaDados();
            CarregaPerfil();
            CarregaFotosPerfil();
            CarregaComunidades();
        }

        StringBuilder scriptDate = new StringBuilder();
        scriptDate.Append("$(document).ready(function(){");
        scriptDate.Append("$('#" + txt_salario.ClientID + "').attr('maxlength','20');");
        scriptDate.Append("$('#" + txt_salario.ClientID + "').maskMoney({allowNegative: true, thousands:'.', decimal:',', affixesStay: false, precision: 2});");

        scriptDate.Append("});");

        ScriptManager.RegisterClientScriptBlock(this, GetType(), "datepik", scriptDate.ToString(), true);
    }


    public void CarregaComunidades()
    {
        int usr_id = (int)Convert.ToInt64(Session["purple_id"]);
        if (Request.QueryString["perfil"] != null)
        {
            usr_id = (int)Convert.ToInt64(Request.QueryString["perfil"]);
        }

        CMD oCMD = new CMD();
        oCMD.CMD_USR_ID = usr_id;
        CMD_DAO oCMD_DAO = new CMD_DAO();

        var lstCount = oCMD_DAO.ListarComunidades(oCMD);
        if (lstCount.Tables[0].Rows.Count > 0)
        {
            lnkComunidades.Text = "(" + lstCount.Tables[0].Rows[0]["total"].ToString() + ") Comunidades";
        }
        else
        {
            lnkComunidades.Text = "(0) Comunidades";
        }
    }

    public void CarregaFotosPerfil()
    {
        int usr_id = (int)Convert.ToInt64(Session["purple_id"]);
        if (Request.QueryString["perfil"] != null)
        {
            usr_id = (int)Convert.ToInt64(Request.QueryString["perfil"]);
        }

        //fotos
        lnkFotos.Text = "(0) Fotos";

        FOT oFOT = new FOT();
        oFOT.FOT_USR_ID = usr_id;
        FOT_DAO oFOT_DAO = new FOT_DAO();
        var lstFotos = oFOT_DAO.Listar(oFOT);
        if (lstFotos.Tables[0].Rows.Count > 0)
        {
            lnkFotos.Text = "(" + lstFotos.Tables[0].Rows.Count + ") Fotos";
        }
    }

    private void carregaDados()
    {
        if (Session["purple_id"] != null)
        {
            USR oUSR = new USR();
            oUSR.USR_ID = (int)Convert.ToInt64(Session["purple_id"]);
            USR_DAO oUSR_DAO = new USR_DAO();
            var lstCadastro = oUSR_DAO.Listar(oUSR);
            if (lstCadastro.Tables[0].Rows.Count > 0)
            {
                txt_nom.Text = lstCadastro.Tables[0].Rows[0]["USR_NOM"].ToString().ToUpper();
                txt_dataNasc.Text = lstCadastro.Tables[0].Rows[0]["USR_DAT_NASC"].ToString() != "" ? Convert.ToDateTime(lstCadastro.Tables[0].Rows[0]["USR_DAT_NASC"]).ToShortDateString() : "";
                ddl_sexo.SelectedValue = lstCadastro.Tables[0].Rows[0]["USR_SEX"].ToString();
                txt_mail.Text = lstCadastro.Tables[0].Rows[0]["USR_EMA"].ToString();
                txt_mail.Enabled = false;

                ddl_trabalho.SelectedValue = lstCadastro.Tables[0].Rows[0]["USR_ARE_ATU"].ToString();
                ddl_conhecimento.SelectedValue = lstCadastro.Tables[0].Rows[0]["USR_NVL_CNH"].ToString();
                txt_dat_adm.Text = lstCadastro.Tables[0].Rows[0]["USR_DAT_ADM"].ToString() != "" ? Convert.ToDateTime(lstCadastro.Tables[0].Rows[0]["USR_DAT_ADM"].ToString()).ToShortDateString() : "";
                txt_salario.Text = lstCadastro.Tables[0].Rows[0]["USR_SAL"].ToString() != "" ? Convert.ToDecimal(lstCadastro.Tables[0].Rows[0]["USR_SAL"].ToString()).ToString("n2") : "0,00";
                txt_cidade.Text = lstCadastro.Tables[0].Rows[0]["USR_CID_TRA"].ToString();
                ddl_estado.SelectedValue = lstCadastro.Tables[0].Rows[0]["USR_STA_TRA"].ToString();
                if (lstCadastro.Tables[0].Rows[0]["USR_ASS"].ToString() == "0")
                {
                    rd_assinatura.Items[0].Selected = true;
                }
                else
                {
                    rd_assinatura.Items[1].Selected = true;
                }

            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void cadastro_Click(object sender, EventArgs e)
    {
        ErrorMessage.Text = "";
        bool valida = true;

        if (txt_nom.Text == "")
        {
            ErrorMessage.Text = "<br>Informe um Nome.";
            valida = false;
        }
        if (!txt_nom.Text.Contains(' '))
        {
            ErrorMessage.Text = ErrorMessage.Text + "<br>Informe um Nome completo.";
            valida = false;
        }
        if (txt_dataNasc.Text == "")
        {
            ErrorMessage.Text = ErrorMessage.Text + "<br>Informe uma data de nascimento.";
            valida = false;
        }
        if (Convert.ToDateTime(txt_dataNasc.Text) < Convert.ToDateTime("01/01/1900"))
        {
            ErrorMessage.Text = ErrorMessage.Text + "<br>Data de nascimento inválida.";
            valida = false;
        }
        if (ddl_sexo.SelectedValue == "")
        {
            ErrorMessage.Text = ErrorMessage.Text + "<br>Informe um sexo.";
            valida = false;
        }
        if (txt_mail.Text == "")
        {
            ErrorMessage.Text = ErrorMessage.Text + "<br>Informe um e-mail.";
            valida = false;
        }
        if (txt_mail.Text != "")
        {
            if (!txt_mail.Text.Contains('@'))
            {
                ErrorMessage.Text = ErrorMessage.Text + "<br>Informe um e-mail válido.";
                valida = false;
            }
            if (!txt_mail.Text.Contains(".com"))
            {
                ErrorMessage.Text = ErrorMessage.Text + "<br>Informe um e-mail válido.";
                valida = false;
            }
        }
        if (Password.Text != "" || ConfirmPassword.Text != "")
        {
            if (ConfirmPassword.Text == "")
            {
                ErrorMessage.Text = ErrorMessage.Text + "<br>Informe a confirmação de senha.";
                ConfirmPassword.Focus();
                valida = false;
            }
            if (Password.Text != ConfirmPassword.Text)
            {
                ErrorMessage.Text = ErrorMessage.Text + "<br>A Confirmação de senha não está conferindo com a senha informada.";
                ConfirmPassword.Focus();
                valida = false;
            }
        }

        if (new Util().checkForSQLInjection(txt_nom.Text) && valida)
        {
            ErrorMessage.Text = ErrorMessage.Text + "<br>Nome com informações inválidas.";
            valida = false;
        }
        else if (new Util().checkForSQLInjection(txt_dataNasc.Text) && valida)
        {
            ErrorMessage.Text = ErrorMessage.Text + "<br>Data com informações inválidas.";
            valida = false;
        }
        else if (new Util().checkForSQLInjection(txt_mail.Text) && valida)
        {
            ErrorMessage.Text = ErrorMessage.Text + "<br>E-mail com informações inválidas.";
            valida = false;
        }
        else if (new Util().checkForSQLInjection(Password.Text) && valida)
        {
            ErrorMessage.Text = ErrorMessage.Text + "<br>Senha com informações inválidas.";
            valida = false;
        }
        else if (new Util().checkForSQLInjection(ConfirmPassword.Text) && valida)
        {
            ErrorMessage.Text = ErrorMessage.Text + "<br>Confirmação de senha com informações inválidas.";
            valida = false;
        }

        if (valida)
        {
            USR oUSR = new USR();
            oUSR.USR_ID = (int)Convert.ToInt64(Session["purple_id"]);
            oUSR.USR_NOM = txt_nom.Text.ToUpper();
            oUSR.USR_DAT_NASC = txt_dataNasc.Text != "" ? Convert.ToDateTime(txt_dataNasc.Text) : (DateTime?)null;
            oUSR.USR_SEX = ddl_sexo.SelectedValue;
            oUSR.USR_EMA = txt_mail.Text;
            oUSR.USR_ARE_ATU = ddl_trabalho.SelectedValue != "" ? Convert.ToInt32(ddl_trabalho.SelectedValue) : (int?)null;
            oUSR.USR_NVL_CNH = ddl_conhecimento.SelectedValue != "" ? Convert.ToInt32(ddl_conhecimento.SelectedValue) : (int?)null;
            oUSR.USR_DAT_ADM = txt_dat_adm.Text != "" ? Convert.ToDateTime(txt_dat_adm.Text) : (DateTime?)null;
            oUSR.USR_SAL = txt_salario.Text != "" ? Convert.ToDecimal(txt_salario.Text) : (decimal?)null;
            oUSR.USR_CID_TRA = txt_cidade.Text;
            oUSR.USR_STA_TRA = ddl_estado.SelectedValue;
            oUSR.USR_LGA_PPG = ddl_linguagens.SelectedValue;
            oUSR.USR_ASS = rd_assinatura.Items[0].Selected ? 0 : 1;

            if (Password.Text != "")
            {
                oUSR.USR_SNH = Password.Text;
            }
            USR_DAO oUSR_DAO = new USR_DAO();

            string id = oUSR_DAO.Salvar(oUSR);

            if (id != "")
            {

                EnviarEmail("Atualização dos seus dados cadastrais foram salvos com sucesso.", (int)Convert.ToInt64(Session["purple_id"]));
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alerta", "alert('Seus Dados foram salvos com sucesso.');", true);
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alerta", "alert('Salvo com sucesso!');window.location='BeneficiarioSimuladorListar.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alerta", "alert('Seus Dados foram salvos com sucesso.');", true);
            }
        }
    }


    public void EnviarEmail(string mensagem, int idUserEnvio)
    {
        USR oUsuario = new USR();
        USR_DAO oUsuarioDAO = new USR_DAO();
        oUsuario.USR_ID = idUserEnvio;
        var dsLstUsuario = oUsuarioDAO.Listar(oUsuario);
        if (dsLstUsuario.Tables[0].Rows.Count > 0)
        {
            try
            {
                string sRemetente = ConfigurationManager.AppSettings["EnderecoSMTP"];
                string sListaDestinatario = "";
                string sListaCopia = "";
                string sListaCopiaOculta = "";
                bool bFormatoHTML = true;
                string sTitulo = "";
                string sAssunto = "";
                string sListaCaminhoAnexo = "";
                string sServer = ConfigurationManager.AppSettings["ServidorSMTP"];
                string sUsuario = ConfigurationManager.AppSettings["UsuarioSMTP"];
                string sSenha = ConfigurationManager.AppSettings["SenhaSMTP"];
                bool bSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["SSLSMTP"]);
                int iPortaSmtp = (int)Convert.ToInt64(ConfigurationManager.AppSettings["PortaSMTP"]);

                sListaDestinatario = dsLstUsuario.Tables[0].Rows[0]["USR_EMA"].ToString().Trim();
                sTitulo = "oPurple social science - alteração cadastral.";

                sAssunto = "<a href='http://www.opurple.com.br/'>";
                sAssunto += "<img src='http://www.opurple.com.br/images/logo/logo_name.png' /></a><br/><br/>\n";
                sAssunto += "Olá <b>" + dsLstUsuario.Tables[0].Rows[0]["USR_NOM"].ToString() + "</b>. <br/>\n";
                sAssunto += mensagem + "<br/>\n";
                sAssunto += "<br/>\n";
                sAssunto += "<br/>\n";
                sAssunto += "<a href='http://www.opurple.com.br/Account/Register.aspx'> visualizar alterações realizadas por você! </a><br/>\n";
                sAssunto += "<br/>\n";
                sAssunto += "<br/>\n";
                sAssunto += "<b>Esse e-mail é automático e não necessita ser respondido.</b><br/>\n";
                Eml oEmailDAO = new Eml();
                oEmailDAO.EnviarEmail(sRemetente, sListaDestinatario, sListaCopia, sListaCopiaOculta, bFormatoHTML, sTitulo, sAssunto, sListaCaminhoAnexo, sServer, bSSL, iPortaSmtp, sUsuario, sSenha);

                //FailureText.Text = "<br>Informações com a recuperação de senha foi enviado para o seu e-mail.<br><b>Verifique sua caixa de entrada ou SPAM.";
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alerta", "alert('E-mail com a recuperação de senha foi enviado.');", true);
            }
            catch (Exception ex)
            {
                string erro = ex.Message;

                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alerta", "alert('" + erro + "');", true);
            }
        }
    }

    public void CarregaPerfil()
    {
        USR oUSR = new USR();
        oUSR.USR_ID = (int)Convert.ToInt64(Session["purple_id"]);
        USR_DAO oUSR_DAO = new USR_DAO();

        var lstCadastro = oUSR_DAO.Listar(oUSR);
        if (lstCadastro.Tables[0].Rows.Count > 0)
        {
            //foto
            imgPerfil.ImageUrl = lstCadastro.Tables[0].Rows[0]["USR_FOT"].ToString() != "" ? lstCadastro.Tables[0].Rows[0]["USR_FOT"].ToString() : "~/images/perfil/perfil.png";

            //amizade
            lnkAmigos.Text = "(0) Amigos";

            AMG oAMG = new AMG();
            oAMG.AMG_USR_USR_ID = (int)Convert.ToInt64(Session["purple_id"]);
            oAMG.AMG_USR_ID = (int)Convert.ToInt64(Session["purple_id"]);
            oAMG.AMG_OK = 1;
            AMG_DAO oAMG_DAO = new AMG_DAO();
            var lstAmizade = oAMG_DAO.ListarAmizades(oAMG);
            if (lstAmizade.Tables[0].Rows.Count > 0)
            {
                string amigo = lstAmizade.Tables[0].Rows.Count > 1 ? "Amigos" : "Amigo";
                lnkAmigos.Text = "(" + lstAmizade.Tables[0].Rows.Count + ") " + amigo;
            }
        }
    }

    protected void imgBtnFoto_Click(object sender, ImageClickEventArgs e)
    {
        if (Request.QueryString["perfil"] != null)
        {
            Response.Redirect("Profile.aspx?perfil=" + Request.QueryString["perfil"].ToString());
        }
        else
        {
            Response.Redirect("Alterarfoto.aspx");
        }
    }

}
