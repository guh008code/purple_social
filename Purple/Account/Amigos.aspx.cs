using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class Amigos : System.Web.UI.Page
{
    public string nome = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["purple_id"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            CarregaPerfil();
            CarregaAmigoPendentes();
            CarregaAmigos();
            CarregaFotosPerfil();
            CarregaComunidades();
        }
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

    public void CarregaPerfil()
    {
        int usr_id = (int)Convert.ToInt64(Session["purple_id"]);
        if (Request.QueryString["perfil"] != null)
        {
            dvPendentes.Visible = false;
            fdAmizade.Visible = true;
            usr_id = (int)Convert.ToInt64(Request.QueryString["perfil"]);

            //botao de amizade
            AMG oAMG = new AMG();
            oAMG.AMG_USR_ID = (int)Convert.ToInt64(Session["purple_id"]);
            oAMG.AMG_USR_USR_ID = (int)Convert.ToInt64(Request.QueryString["perfil"]);
            AMG_DAO oAMG_DAO = new AMG_DAO();
            var lstAmizade = oAMG_DAO.Listar(oAMG);
            if (lstAmizade.Tables[0].Rows.Count > 0)
            {
                if (lstAmizade.Tables[0].Rows[0]["AMG_OK"].ToString() == "1")
                {
                    btnAmizade.Text = "Desfazer Amizade";
                }
                else
                {
                    btnAmizade.Text = "Cancelar Solicitação";
                }
            }


            oAMG = new AMG();
            oAMG.AMG_USR_USR_ID = (int)Convert.ToInt64(Session["purple_id"]);
            oAMG.AMG_USR_ID = (int)Convert.ToInt64(Request.QueryString["perfil"]);
            oAMG_DAO = new AMG_DAO();
            lstAmizade = oAMG_DAO.Listar(oAMG);
            if (lstAmizade.Tables[0].Rows.Count > 0)
            {
                if (lstAmizade.Tables[0].Rows[0]["AMG_OK"].ToString() == "1")
                {
                    btnAmizade.Text = "Desfazer Amizade";
                }
                else
                {
                    btnAmizade.Text = "Aceitar Solicitação";
                }
            }


            lnkAmigos.PostBackUrl = "Amigos.aspx?perfil=" + Request.QueryString["perfil"].ToString();
            lnkFotos.PostBackUrl = "Fotos.aspx?perfil=" + Request.QueryString["perfil"].ToString();

            if (Request.QueryString["perfil"].ToString() == Session["purple_id"].ToString())
            {
                fdAmizade.Visible = false;
            }
        }
        else
        {
            //verificar se possui amizades pendentes de aprovacao
            //dvPendentes
            dvPendentes.Visible = true;

            fdAmizade.Visible = false;
        }

        USR oUSR = new USR();
        oUSR.USR_ID = usr_id;
        USR_DAO oUSR_DAO = new USR_DAO();

        var lstCadastro = oUSR_DAO.Listar(oUSR);
        if (lstCadastro.Tables[0].Rows.Count > 0)
        {
            //foto
            imgPerfil.ImageUrl = lstCadastro.Tables[0].Rows[0]["USR_FOT"].ToString() != "" ? lstCadastro.Tables[0].Rows[0]["USR_FOT"].ToString() : "~/images/perfil/perfil.png";

            nome = " de " + lstCadastro.Tables[0].Rows[0]["USR_NOM"].ToString();

            //amizade
            lnkAmigos.Text = "(0) Amigos";

            AMG oAMG = new AMG();
            oAMG.AMG_USR_USR_ID = usr_id;
            oAMG.AMG_USR_ID = usr_id;
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
    public int CarregaAmigoPendentes()
    {
        int total = 0;
        dvPendentes.Visible = true;
        int usr_id = (int)Convert.ToInt64(Session["purple_id"]);
        if (Request.QueryString["perfil"] != null)
        {
            dvPendentes.Visible = false;
            usr_id = (int)Convert.ToInt64(Request.QueryString["perfil"]);
        }

        AMG oAMG = new AMG();
        oAMG.AMG_USR_ID = usr_id;
        oAMG.AMG_USR_USR_ID = usr_id;
        oAMG.AMG_SLC = 1;
        oAMG.AMG_OK = 0;
        AMG_DAO oAMG_DAO = new AMG_DAO();

        var lstBusca = oAMG_DAO.ListarAmizades(oAMG);
        if (lstBusca.Tables[0].Rows.Count > 0)
        {     
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < lstBusca.Tables[0].Rows.Count; i++)
            {
                if (usr_id.ToString() != lstBusca.Tables[0].Rows[i]["USR_ID"].ToString())
                {
                    sb.Append("<div Style='Float:left;width:66px;Margin:0px 3px 0px 3px;'>");
                    sb.Append("<a href='Profile.aspx?perfil=" + lstBusca.Tables[0].Rows[i]["USR_ID"].ToString() + "' Style='Margin:0px 10px 0px 10px;font-size: x-small;' title='" + lstBusca.Tables[0].Rows[i]["USR_NOM"].ToString() + "'>");
                    sb.Append("<img src='" + (lstBusca.Tables[0].Rows[i]["USR_FOT"].ToString() != "" ? lstBusca.Tables[0].Rows[i]["USR_FOT"].ToString().Replace("~", "..") : "../images/perfil/perfil.png") + "' width='65px' height='65px' style='margin-left:-12px;' />");
                    sb.Append("<br /> " + lstBusca.Tables[0].Rows[i]["USR_NOM"].ToString().Split(' ')[0].ToString() + "</a>");
                    sb.Append("</div>");
                    total = total + 1;
                }
            }

            if (sb.ToString() != "")
            {
                ltrAmigosPendetes.Text = "<p>" + sb.ToString() + "</p>";
            }
            else
            {
                lstBusca = oAMG_DAO.ListarAmizadesInversa(oAMG);
                if (lstBusca.Tables[0].Rows.Count > 0)
                {
                    sb = new StringBuilder();
                    for (int i = 0; i < lstBusca.Tables[0].Rows.Count; i++)
                    {
                        if (usr_id.ToString() != lstBusca.Tables[0].Rows[i]["USR_ID"].ToString())
                        {
                            sb.Append("<div Style='Float:left;width:66px;Margin:0px 3px 0px 3px;'>");
                            sb.Append("<a href='Profile.aspx?perfil=" + lstBusca.Tables[0].Rows[i]["USR_ID"].ToString() + "' Style='Margin:0px 10px 0px 10px;font-size: x-small;' title='" + lstBusca.Tables[0].Rows[i]["USR_NOM"].ToString() + "'>");
                            sb.Append("<img src='" + (lstBusca.Tables[0].Rows[i]["USR_FOT"].ToString() != "" ? lstBusca.Tables[0].Rows[i]["USR_FOT"].ToString().Replace("~", "..") : "../images/perfil/perfil.png") + "' width='65px' height='65px' style='margin-left:-12px;' />");
                            sb.Append("<br /> " + lstBusca.Tables[0].Rows[i]["USR_NOM"].ToString().Split(' ')[0].ToString() + "</a>");
                            sb.Append("</div>");
                            total = total + 1;
                        }
                    }

                    if (sb.ToString() != "")
                    {
                        ltrAmigosPendetes.Text = "<p>" + sb.ToString() + "</p>";
                    }
                    else
                    {
                        dvPendentes.Visible = false;
                    }
                }
                else
                {
                    dvPendentes.Visible = false;
                }
            }
        }
        else
        {
            lstBusca = oAMG_DAO.ListarAmizadesInversa(oAMG);
            if (lstBusca.Tables[0].Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < lstBusca.Tables[0].Rows.Count; i++)
                {
                    if (usr_id.ToString() != lstBusca.Tables[0].Rows[i]["USR_ID"].ToString())
                    {
                        sb.Append("<div Style='Float:left;width:66px;Margin:0px 3px 0px 3px;'>");
                        sb.Append("<a href='Profile.aspx?perfil=" + lstBusca.Tables[0].Rows[i]["USR_ID"].ToString() + "' Style='Margin:0px 10px 0px 10px;font-size: x-small;' title='" + lstBusca.Tables[0].Rows[i]["USR_NOM"].ToString() + "'>");
                        sb.Append("<img src='" + (lstBusca.Tables[0].Rows[i]["USR_FOT"].ToString() != "" ? lstBusca.Tables[0].Rows[i]["USR_FOT"].ToString().Replace("~", "..") : "../images/perfil/perfil.png") + "' width='65px' height='65px' style='margin-left:-12px;' />");
                        sb.Append("<br /> " + lstBusca.Tables[0].Rows[i]["USR_NOM"].ToString().Split(' ')[0].ToString() + "</a>");
                        sb.Append("</div>");
                        total = total + 1;
                    }
                }

                if (sb.ToString() != "")
                {
                    ltrAmigosPendetes.Text = "<p>" + sb.ToString() + "</p>";
                }
                else
                {
                    dvPendentes.Visible = false;
                }
            }
            else
            {
                dvPendentes.Visible = false;
            }
        }

        return total;
    }

    public void CarregaAmigos()
    {
        int usr_id = (int)Convert.ToInt64(Session["purple_id"]);
        if (Request.QueryString["perfil"] != null)
        {
            usr_id = (int)Convert.ToInt64(Request.QueryString["perfil"]);
        }

        AMG oAMG = new AMG();
        oAMG.AMG_USR_ID = usr_id;
        oAMG.AMG_USR_USR_ID = usr_id;
        oAMG.AMG_OK = 1;
        AMG_DAO oAMG_DAO = new AMG_DAO();

        var lstBusca = oAMG_DAO.ListarAmigos(oAMG);
        if (lstBusca.Tables[0].Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < lstBusca.Tables[0].Rows.Count; i++)
            {
                if (usr_id.ToString() != lstBusca.Tables[0].Rows[i]["USR_ID"].ToString())
                {
                    sb.Append("<div Style='Float:left;width:66px;Margin:0px 3px 0px 3px;'>");
                    sb.Append("<a href='Profile.aspx?perfil=" + lstBusca.Tables[0].Rows[i]["USR_ID"].ToString() + "' Style='Margin:0px 10px 0px 10px;font-size: x-small;' title='" + lstBusca.Tables[0].Rows[i]["USR_NOM"].ToString() + "'>");
                    sb.Append("<img src='" + (lstBusca.Tables[0].Rows[i]["USR_FOT"].ToString() != "" ? lstBusca.Tables[0].Rows[i]["USR_FOT"].ToString().Replace("~", "..") : "../images/perfil/perfil.png") + "' width='65px' height='65px' style='margin-left:-12px;' />");
                    sb.Append("<br /> " + lstBusca.Tables[0].Rows[i]["USR_NOM"].ToString().Split(' ')[0].ToString() + "</a>");
                    sb.Append("</div>");
                }
            }

            if (sb.ToString() != "")
            {
                ltrAmigos.Text = "<p>" + sb.ToString() + "</p>";
            }
            else
            {

                ltrAmigos.Text = "<p>Você não possuí amigos.</p>";
            }
        }
        else
        {
            ltrAmigos.Text = "<p>Você não possuí amigos.</p>";
        }
    }

    protected void btnAmizade_Click(object sender, EventArgs e)
    {
        if (btnAmizade.Text == "Adicionar como amigo")
        {
            AMG oAMG = new AMG();
            oAMG.AMG_USR_ID = (int)Convert.ToInt64(Session["purple_id"]);
            oAMG.AMG_USR_USR_ID = (int)Convert.ToInt64(Request.QueryString["perfil"]);
            oAMG.AMG_SLC = 1;
            oAMG.AMG_OK = 0;
            oAMG.AMG_DAT_INC = DateTime.Now;
            AMG_DAO oAMG_DAO = new AMG_DAO();

            oAMG_DAO.Inserir(oAMG);

            btnAmizade.Text = "Cancelar Solicitação";
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        else if (btnAmizade.Text == "Desfazer Amizade")
        {
            AMG oAMG = new AMG();
            oAMG.AMG_USR_USR_ID = (int)Convert.ToInt64(Session["purple_id"]);
            oAMG.AMG_USR_ID = (int)Convert.ToInt64(Request.QueryString["perfil"]);
            AMG_DAO oAMG_DAO = new AMG_DAO();
            oAMG_DAO.Excluir(oAMG);

            oAMG = new AMG();
            oAMG.AMG_USR_ID = (int)Convert.ToInt64(Session["purple_id"]);
            oAMG.AMG_USR_USR_ID = (int)Convert.ToInt64(Request.QueryString["perfil"]);
            oAMG_DAO = new AMG_DAO();
            oAMG_DAO.Excluir(oAMG);

            btnAmizade.Text = "Adicionar como amigo";
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        else if (btnAmizade.Text == "Cancelar Solicitação")
        {
            AMG oAMG = new AMG();
            oAMG.AMG_USR_USR_ID = (int)Convert.ToInt64(Session["purple_id"]);
            oAMG.AMG_USR_ID = (int)Convert.ToInt64(Request.QueryString["perfil"]);
            AMG_DAO oAMG_DAO = new AMG_DAO();
            oAMG_DAO.Excluir(oAMG);

            oAMG = new AMG();
            oAMG.AMG_USR_ID = (int)Convert.ToInt64(Session["purple_id"]);
            oAMG.AMG_USR_USR_ID = (int)Convert.ToInt64(Request.QueryString["perfil"]);
            oAMG_DAO = new AMG_DAO();
            oAMG_DAO.Excluir(oAMG);

            btnAmizade.Text = "Adicionar como amigo";
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        else if (btnAmizade.Text == "Aceitar Solicitação")
        {
            AMG oAMG = new AMG();
            oAMG.AMG_USR_USR_ID = (int)Convert.ToInt64(Session["purple_id"]);
            oAMG.AMG_USR_ID = (int)Convert.ToInt64(Request.QueryString["perfil"]);
            oAMG.AMG_SLC = 1;
            oAMG.AMG_OK = 1;
            oAMG.AMG_DAT_INC = DateTime.Now;
            AMG_DAO oAMG_DAO = new AMG_DAO();

            oAMG_DAO.AdicionarOuRemoverAmizade(oAMG);

            btnAmizade.Text = "Desfazer Amizade";
            Response.Redirect(Request.Url.AbsoluteUri);
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
