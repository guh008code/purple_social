using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;

public partial class Account_Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }

        //RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
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
            else if (txt_mail.Text.Split('@')[1].Length < 1)
            {
                ErrorMessage.Text = ErrorMessage.Text + "<br>Informe um e-mail válido.";
                valida = false;
            }
        }

        if (Password.Text == "")
        {
            ErrorMessage.Text = ErrorMessage.Text + "<br>Informe uma senha.";
            valida = false;
        }
        if (Password.Text.Length >= 7)
        {
            ErrorMessage.Text = ErrorMessage.Text + "<br>Sua senha deve possuir pelo menos 7 caracteres.";
            valida = false;
        }
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
            oUSR.USR_EMA = txt_mail.Text;
            USR_DAO oUSR_DAO = new USR_DAO();

            var lstCadastro = oUSR_DAO.Listar(oUSR);
            if (lstCadastro.Tables[0].Rows.Count > 0)
            {
                ErrorMessage.Text = ErrorMessage.Text + "<br>Este e-mail já está cadastrado.";
                valida = false;
            }
        }

        if (valida)
        {
            USR oUSR = new USR();
            oUSR.USR_NOM = txt_nom.Text.ToUpper();
            oUSR.USR_DAT_NASC = txt_dataNasc.Text != "" ? Convert.ToDateTime(txt_dataNasc.Text) : (DateTime?)null;
            oUSR.USR_SEX = ddl_sexo.SelectedValue;
            oUSR.USR_EMA = txt_mail.Text;
            oUSR.USR_SNH = Password.Text;
            oUSR.USR_DAT_INC = DateTime.Now;
            oUSR.USR_EML_VLD = 0;
            oUSR.USR_ASS = 0;
            USR_DAO oUSR_DAO = new USR_DAO();

            string id = oUSR_DAO.Salvar(oUSR);

            if (id != "")
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

                    string codigoValidador = Util.Criptografar(Password.Text + "_" + id);

                    sListaDestinatario = txt_mail.Text.Trim();
                    sTitulo = " oPurple social science - confirmar cadastro";

                    sAssunto = "<a href='http://www.opurple.com.br/ChangePassword.aspx?confirm=" + codigoValidador + "'>";
                    sAssunto += "<img src='http://www.opurple.com.br/images/logo/logo_name.png' /></a><br/><br/>\n";
                    sAssunto += "Olá <b>" + txt_nom.Text.ToUpper() + "</b>. <br/>\n";
                    sAssunto += "Segue abaixo o link para confirmação e validação do seu cadastro no <b>oPurple</b>.<br/>\n";
                    sAssunto += "<br/>\n";
                    sAssunto += "E-mail Login: " + txt_mail.Text.Trim() +"<br/>\n";
                    sAssunto += "<a href='http://www.opurple.com.br/ChangePassword.aspx?confirm=" + codigoValidador + "'> Confirmar o seu cadastro </a><br/>\n";
                    sAssunto += "<br/>\n";
                    sAssunto += "<b>Esse e-mail é automático e não necessita ser respondido.</b><br/>\n";
                    Eml oEmailDAO = new Eml();
                    oEmailDAO.EnviarEmail(sRemetente, sListaDestinatario, sListaCopia, sListaCopiaOculta, bFormatoHTML, sTitulo, sAssunto, sListaCaminhoAnexo, sServer, bSSL, iPortaSmtp, sUsuario, sSenha);

                    //Response.Redirect("ChangePasswordSuccess.aspx?param=1");

                    //ErrorMessage.Text = "<br>Enviamos uma confirmação de cadastro para o seu e-mail " + txt_mail.Text.Trim();
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alerta", "alert('Enviamos uma confirmação de cadastro para o seu e-mail. Verifique sua caixa de entrada ou SPAM.');window.location='ChangePasswordSuccess.aspx?param=1';", true);
                }
                catch (Exception ex)
                {
                    string erro = ex.Message;

                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alerta", "alert('" + erro + "');", true);
                }
            }
        }
    }

}
