using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class ErrorPage : System.Web.UI.Page
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
                    titulo = "Cadastro realizado com sucesso.";
                }
                if (Request.QueryString["param"].ToString() == "2")
                {
                    titulo = "Sua senha foi alterada com sucesso.";
                }
            }
        }
    }
}
