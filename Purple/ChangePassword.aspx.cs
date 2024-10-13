using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Account_ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["confirm"] != null)
            {
                string url = Request.QueryString["confirm"].ToString().Replace(" ", "+");
                string decod = Util.Descriptografar(url);

                USR oUSR = new USR();
                oUSR.USR_ID = (int)Convert.ToInt64(decod.Split('_')[1]);
                oUSR.USR_SNH = decod.Split('_')[0];
                USR_DAO oUSR_DAO = new USR_DAO();
                var lstUsr = oUSR_DAO.Listar(oUSR);
                if (lstUsr.Tables[0].Rows.Count > 0)
                {
                    oUSR = new USR();
                    oUSR.USR_ID = (int)Convert.ToInt64(decod.Split('_')[1]);
                    oUSR.USR_EML_VLD = 1;
                    oUSR_DAO = new USR_DAO();
                    oUSR_DAO.ValidarUsuario(oUSR);
                    Response.Redirect("ChangePasswordSuccess.aspx?param=3");
                }
            }

            if (Request.QueryString["request"] == null)
            {
                Response.Redirect("Default.aspx");
            }

            if (Request.QueryString["codValidator"] != null)
            {
                string codValidator = Request.QueryString["codValidator"].ToString().Replace(" ", "+");
                CurrentPassword.Text = codValidator;
            }


        }
    }
    protected void CancelPushButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
    protected void ChangePasswordPushButton_Click(object sender, EventArgs e)
    {
        bool valida = true;
        if (CurrentPassword.Text == "" && valida)
        {
            FailureText.Text = FailureText.Text + "<br>Informe o código que foi enviado Por e-mail:";
            valida = false;
        }
        else if (NewPassword.Text == "" && valida)
        {
            FailureText.Text = FailureText.Text + "<br>Informe uma senha.";
            valida = false;
        }
        else if (ConfirmNewPassword.Text == "" && valida)
        {
            FailureText.Text = FailureText.Text + "<br>Informe a confirmação de senha.";
            ConfirmNewPassword.Focus();
            valida = false;
        }
        else if (NewPassword.Text != ConfirmNewPassword.Text && valida)
        {
            FailureText.Text = FailureText.Text + "<br>A Confirmação de senha não está conferindo com a senha informada.";
            ConfirmNewPassword.Focus();
            valida = false;
        }
        else if (new Util().checkForSQLInjection(CurrentPassword.Text) && valida)
        {
            FailureText.Text = FailureText.Text + "<br>Código validador com informações inválidas.";
            valida = false;
        }
        else if (new Util().checkForSQLInjection(NewPassword.Text) && valida)
        {
            FailureText.Text = FailureText.Text + "<br>Senha com informações inválidas.";
            valida = false;
        }
        else if (new Util().checkForSQLInjection(ConfirmNewPassword.Text) && valida)
        {
            FailureText.Text = FailureText.Text + "<br>Confirmação de senha com informações inválidas.";
            valida = false;
        }

        if (valida)
        {
            string decod = Util.Descriptografar(CurrentPassword.Text);

            USR oUSR = new USR();
            oUSR.USR_ID = (int)Convert.ToInt64(decod.Split('_')[1]);
            oUSR.USR_SNH = decod.Split('_')[0];
            USR_DAO oUSR_DAO = new USR_DAO();
            var lstUsr = oUSR_DAO.Listar(oUSR);
            if (lstUsr.Tables[0].Rows.Count > 0)
            {
                oUSR = new USR();
                oUSR.USR_ID = (int)Convert.ToInt64(decod.Split('_')[1]);
                oUSR.USR_SNH = NewPassword.Text;
                oUSR_DAO = new USR_DAO();
                oUSR_DAO.AlterarSenha(oUSR);

                Response.Redirect("ChangePasswordSuccess.aspx?param=2");

            }
            else
            {
                FailureText.Text = FailureText.Text + "<br>Código de confirmação inválido!.";
            }
        }
    }
}
