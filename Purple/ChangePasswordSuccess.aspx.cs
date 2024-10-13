using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Account_ChangePasswordSuccess : System.Web.UI.Page
{
    public string titulo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["param"] != null)
            {
                if (Request.QueryString["param"].ToString() == "1")
                {
                    titulo = "Enviamos uma confirmação de cadastro para o seu e-mail.";
                }
                if (Request.QueryString["param"].ToString() == "2")
                {
                    titulo = "Sua senha foi alterada com sucesso.";
                }
                if (Request.QueryString["param"].ToString() == "3")
                {
                    titulo = "O Cadastro foi confirmado e validado com sucesso.";
                }
            }
        }
    }
}
