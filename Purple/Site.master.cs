using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["purple_id"] != null)
            {
                semLogin.Visible = false;
                comLogin.Visible = true;
                dvMenu.Visible = false;

                lblNome.Text = Session["purple_name"].ToString().ToUpper();

                lnkLogo.PostBackUrl = "~/Account/Inicio.aspx";
                lnkLogo.ToolTip = "Inicio";

                if (Session["purple_id"].ToString() == "5" || Session["purple_id"].ToString() == "17" || Session["purple_id"].ToString() == "18" || Session["purple_id"].ToString() == "19")
                {
                    ltrLink.Visible = true;
                    ltrLink.Text = "[ <a href='../Account/Relatorio.aspx'>Relatórios</a> ]";
                }
            }
            else
            {
                semLogin.Visible = true;
                comLogin.Visible = false;
                dvMenu.Visible = true;

                lnkLogo.PostBackUrl = "~/Default.aspx";
                lnkLogo.ToolTip = "Home";

                ltrLink.Visible = false;
            }
        }
    }
    protected void lnkSair_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session["purple_id"] = null;
        Response.Redirect("~/Login.aspx");
    }
    protected void txt_busca_TextChanged(object sender, EventArgs e)
    {
        Response.Redirect("Pesquisa.aspx?search=" + txt_busca.Text);
    }
}
