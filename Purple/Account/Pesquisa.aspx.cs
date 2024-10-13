using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class Pesquisa : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["purple_id"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            CarregaPerfil();
            BuscaDados();
            CarregaFotosPerfil();
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

    public void BuscaDados()
    {
        string amigos = BuscaAmigos();
        if (amigos == "")
        {
            ltrAmigos.Text = "Nenhum amigo foi encontrado.";
        }
        else
        {
            ltrAmigos.Text = amigos;
        }

        string comunidades = BuscaComunidades();
        if (comunidades == "")
        {
            ltrComunidade.Text = "Nenhuma comunidade encontrada.";
        }
        else
        {
            ltrComunidade.Text = comunidades;
        }
    }

    public string BuscaAmigos()
    {
        StringBuilder sb = new StringBuilder();
        if (Request.QueryString["search"] != null)
        {
            USR oUSR = new USR();
            oUSR.USR_EMA = Request.QueryString["search"].ToString();
            oUSR.USR_NOM = Request.QueryString["search"].ToString();
            oUSR.USR_EML_VLD = 1;
            USR_DAO oUSR_DAO = new USR_DAO();

            var lstBusca = oUSR_DAO.ListarBusca(oUSR);
            for (int i = 0; i < lstBusca.Tables[0].Rows.Count; i++)
            {
                sb.Append("<div Style='Float:left;width:66px;Margin:0px 3px 0px 3px;'>");
                sb.Append("<a href='Profile.aspx?perfil=" + lstBusca.Tables[0].Rows[i]["USR_ID"].ToString() + "' Style='Margin:0px 10px 0px 10px;' title='" + lstBusca.Tables[0].Rows[i]["USR_NOM"].ToString() + "'>");
                sb.Append("<img src='" + (lstBusca.Tables[0].Rows[i]["USR_FOT"].ToString() != "" ? lstBusca.Tables[0].Rows[i]["USR_FOT"].ToString().Replace("~", "..") : "../images/perfil/perfil.png") + "' width='65px' height='65px' style='margin-left:-12px;' />");
                sb.Append("<br /> " + lstBusca.Tables[0].Rows[i]["USR_NOM"].ToString() + "</a>");
                sb.Append("</div>");
            }
        }

        return sb.ToString();
    }

    public string BuscaComunidades()
    {
        StringBuilder sb = new StringBuilder();

        if (Request.QueryString["search"] != null)
        {
            CMD oCMD = new CMD();
            oCMD.CMD_TIT = Request.QueryString["search"].ToString();
            CMD_DAO oCMD_DAO = new CMD_DAO();
            var lstComunidades = oCMD_DAO.Listar(oCMD);
            if (lstComunidades.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < lstComunidades.Tables[0].Rows.Count; i++)
                {
                    sb.Append("<img style='float:left;' src='../images/icones/community-icon-6.png' width='50px' /> <p style='float:left;margin-top:10px'> <a Style='font-size:small;' href='Comunidade.aspx?topico=" + lstComunidades.Tables[0].Rows[i]["ARE_ATU_ID"].ToString() + "&assunto=" + lstComunidades.Tables[0].Rows[i]["CMD_ID"].ToString() + "'>" + lstComunidades.Tables[0].Rows[i]["CMD_TIT"].ToString() + "</a></p><br clear='all' /><br clear='all' />");
                }

            }
        }


        return sb.ToString();
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

    protected void imgPerfil_Click(object sender, ImageClickEventArgs e)
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
