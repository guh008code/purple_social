using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["purple_id"] == null)
            {
                //Response.Redirect("~/Login.aspx");
            }
            else
            {
                Response.Redirect("~/Account/Inicio.aspx");
            }
        }
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        FailureText.Text = "";
        bool validado = true;
        if (txtEmail.Text == "" && validado)
        {
            FailureText.Text = "<br>Informe um e-mail.";
            validado = false;
        }
        else if (txtPass.Text == "" && validado)
        {
            FailureText.Text = FailureText.Text + "<br>Informe uma senha.";
            validado = false;
        }
        else if (new Util().checkForSQLInjection(txtEmail.Text) && validado)
        {
            FailureText.Text = "<br>E-mail com informações inválidas.";
            validado = false;
        }
        else if (new Util().checkForSQLInjection(txtPass.Text) && validado)
        {
            FailureText.Text = "<br>Senha com informações inválidas.";
            validado = false;
        }

        if (validado)
        {
            USR oUSR = new USR();
            oUSR.USR_EMA = txtEmail.Text;
            oUSR.USR_SNH = txtPass.Text;
            oUSR.USR_EML_VLD = 1;
            USR_DAO oUSR_DAO = new USR_DAO();

            var lstCadastro = oUSR_DAO.Listar(oUSR);
            if (lstCadastro.Tables[0].Rows.Count > 0)
            {
                ACS oACS = new ACS();
                oACS.ACS_USR_ID = lstCadastro.Tables[0].Rows[0]["USR_ID"].ToString() != "" ? Convert.ToInt32(lstCadastro.Tables[0].Rows[0]["USR_ID"].ToString()) : 0;
                oACS.ACS_DAT_TMS = DateTime.Now;
                ACS_DAO oACS_DAO = new ACS_DAO();
                oACS_DAO.Inserir(oACS);

                Session["purple_id"] = lstCadastro.Tables[0].Rows[0]["USR_ID"].ToString();
                Session["purple_name"] = lstCadastro.Tables[0].Rows[0]["USR_NOM"].ToString();
                Response.Redirect("~/Account/Inicio.aspx");
            }
            else
            {
                FailureText.Text = "<br> E-mail ou senha inválidos.";
            }
        }
    }
    protected void lnkEsqueci_Click(object sender, EventArgs e)
    {
        if (txtEmail.Text != "")
        {
            if (new Util().checkForSQLInjection(txtEmail.Text))
            {
                FailureText.Text = "<br>E-mail com informações inválidas.";
            }
            else
            {
                USR oUsuario = new USR();
                USR_DAO oUsuarioDAO = new USR_DAO();
                oUsuario.USR_EMA = txtEmail.Text;
                var dsLstUsuario = oUsuarioDAO.Listar(oUsuario);
                if (dsLstUsuario.Tables[0].Rows.Count > 0)
                {
                    if (dsLstUsuario.Tables[0].Rows[0]["USR_EML_VLD"].ToString() == "")
                    {
                        FailureText.Text = "<br>Verifique sua caixa de entrada ou (spam) para confirmar o cadastro realizado.";

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

                        string codigoValidador = Util.Criptografar(dsLstUsuario.Tables[0].Rows[0]["USR_SNH"].ToString() + "_" + dsLstUsuario.Tables[0].Rows[0]["USR_ID"].ToString());

                        sListaDestinatario = dsLstUsuario.Tables[0].Rows[0]["USR_EMA"].ToString().Trim();
                        sTitulo = "oPurple social science - confirmação de cadastro";

                        sAssunto = "<a href='http://www.opurple.com.br/'>";
                        sAssunto += "<img src='http://www.opurple.com.br/images/logo/logo_name.png' /></a><br/><br/>\n";
                        sAssunto += "Olá <b>" + dsLstUsuario.Tables[0].Rows[0]["USR_NOM"].ToString() + "</b>. <br/>\n";
                        sAssunto += "Segue abaixo o link para confirmação e validação do seu cadastro no <b>oPurple</b>.<br/>\n";
                        sAssunto += "<br/>\n";
                        sAssunto += "E-mail Login: " + dsLstUsuario.Tables[0].Rows[0]["USR_EMA"].ToString().Trim() + "<br/>\n";
                        sAssunto += "<a href='http://www.opurple.com.br/ChangePassword.aspx?confirm=" + codigoValidador + "'> Confirmar o seu cadastro? </a><br/>\n";
                        sAssunto += "<br/>\n";
                        sAssunto += "<b>Esse e-mail é automático e não necessita ser respondido.</b><br/>\n";
                        Eml oEmailDAO = new Eml();
                        oEmailDAO.EnviarEmail(sRemetente, sListaDestinatario, sListaCopia, sListaCopiaOculta, bFormatoHTML, sTitulo, sAssunto, sListaCaminhoAnexo, sServer, bSSL, iPortaSmtp, sUsuario, sSenha);

                    }
                    else
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

                            string codigoValidador = Util.Criptografar(dsLstUsuario.Tables[0].Rows[0]["USR_SNH"].ToString());

                            sListaDestinatario = dsLstUsuario.Tables[0].Rows[0]["USR_EMA"].ToString().Trim();
                            sTitulo = " oPurple social science - Recuperação de senha";

                            sAssunto = "<a href='http://www.opurple.com.br/'>";
                            sAssunto += "<img src='http://www.opurple.com.br/images/logo/logo_name.png' /></a><br/><br/>\n";
                            sAssunto += "Olá <b>" + dsLstUsuario.Tables[0].Rows[0]["USR_NOM"].ToString() + "</b>. <br/>\n";
                            sAssunto += "Segue abaixo o seu Usuário e <b>código validador</b> para alterar sua nova senha. <br/>\n";
                            sAssunto += "<br/>\n";
                            sAssunto += "E-mail Login: " + dsLstUsuario.Tables[0].Rows[0]["USR_EMA"].ToString() + "<br/>\n";
                            sAssunto += "Código validador: <b>" + codigoValidador + "</b><br/>\n";
                            sAssunto += "<a href='http://www.opurple.com.br/ChangePassword.aspx?codValidator=" + codigoValidador + "&request=" + dsLstUsuario.Tables[0].Rows[0]["USR_ID"].ToString() + "'> Criar uma nova senha? </a><br/>\n";
                            sAssunto += "<br/>\n";
                            sAssunto += "<b>Esse e-mail é automático e não necessita ser respondido.</b><br/>\n";
                            Eml oEmailDAO = new Eml();
                            oEmailDAO.EnviarEmail(sRemetente, sListaDestinatario, sListaCopia, sListaCopiaOculta, bFormatoHTML, sTitulo, sAssunto, sListaCaminhoAnexo, sServer, bSSL, iPortaSmtp, sUsuario, sSenha);

                            FailureText.Text = "<br>Informações com a recuperação de senha foi enviado para o seu e-mail.<br><b>Verifique sua caixa de entrada ou SPAM.";
                            //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alerta", "alert('E-mail com a recuperação de senha foi enviado.');", true);
                        }
                        catch (Exception ex)
                        {
                            string erro = ex.Message;

                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alerta", "alert('" + erro + "');", true);
                        }
                    }
                }
                else
                {
                    FailureText.Text = "<br>E-mail informado é inválido.";
                }
            }
        }
        else
        {
            FailureText.Text = "<br>Informe um e-mail para recuperar a senha.";
        }
    }
}
