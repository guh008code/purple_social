using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class Inicio : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["purple_id"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            if (Request.QueryString["feed"] != null)
            {
                int idFeed = 0 ;
                try
                {
                    idFeed = (int)Convert.ToInt64(Request.QueryString["feed"]);
                    FED oFED = new FED();
                    oFED.FED_ID = idFeed;
                    FED_DAO oFED_DAO = new FED_DAO();
                    oFED_DAO.ExcluirPost(oFED);
                    Response.Redirect("Inicio.aspx");
                }
                catch 
                {
                   
                }
            }

            CarregaPerfil();
            CarregaPost();
            CarregaSugestoesDeAmizade();
            CarregaFotosPerfil();
            CarregaComunidades();
            CarregaInteresses();

            txt_post.Attributes.Add("maxlength", "200");
            txt_post.Attributes.Add("onkeyup", "return ismaxlength(this);");
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

            int total = CarregaAmigoPendentes();
            if (total > 0)
            {
                lnkSolicitacoes.Visible = true;
            }
            else
            {
                lnkSolicitacoes.Visible = false;
            }
            lnkSolicitacoes.Text = (total > 1 ? "(" + total.ToString() + ") Solicitações" : "(" + total.ToString() + ") Solicitação");

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

    public void CarregaPost()
    {
        FED oFED = new FED();
        oFED.FED_USR_ID = (int)Convert.ToInt64(Session["purple_id"]);
        FED_DAO oFED_DAO = new FED_DAO();

        ltrPostagens.Text = "";
        var lstPosts = oFED_DAO.ListarPost(oFED);
        if (lstPosts.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < lstPosts.Tables[0].Rows.Count; i++)
            {
                ltrPostagens.Text = ltrPostagens.Text + "<div style='float:left;border:1px solid #ccc;width:448px;'>";
                ltrPostagens.Text = ltrPostagens.Text + "<p style='float:left;'>";
                ltrPostagens.Text = ltrPostagens.Text + "<a href='Profile.aspx?perfil=" + lstPosts.Tables[0].Rows[i]["USR_ID"].ToString() + "'><img style='margin-top:5px;' src='" + (lstPosts.Tables[0].Rows[i]["USR_FOT"].ToString() != "" ? lstPosts.Tables[0].Rows[i]["USR_FOT"].ToString().Replace("~", "..") : "../images/perfil/perfil.png") + "' width='40px' /></a></p> ";

                if (Session["purple_id"].ToString() == lstPosts.Tables[0].Rows[i]["USR_ID"].ToString())
                {
                    ltrPostagens.Text = ltrPostagens.Text + "<p><b><a href='Profile.aspx?perfil=" + lstPosts.Tables[0].Rows[i]["USR_ID"].ToString() + "'>" + lstPosts.Tables[0].Rows[i]["USR_NOM"].ToString() + "</a> - " + Convert.ToDateTime(lstPosts.Tables[0].Rows[i]["FED_DAT_INC"]).ToShortDateString() + " <a style='float:right;text-decoration:none;' href='Inicio.aspx?feed=" + lstPosts.Tables[0].Rows[i]["FED_ID"].ToString() + "'>(X)</a></b><br>";
                }
                else
                {
                    ltrPostagens.Text = ltrPostagens.Text + "<p><b><a href='Profile.aspx?perfil=" + lstPosts.Tables[0].Rows[i]["USR_ID"].ToString() + "'>" + lstPosts.Tables[0].Rows[i]["USR_NOM"].ToString() + "</a> - " + Convert.ToDateTime(lstPosts.Tables[0].Rows[i]["FED_DAT_INC"]).ToShortDateString() + "</b><br>";
                }

                ltrPostagens.Text = ltrPostagens.Text + lstPosts.Tables[0].Rows[i]["FED_POT"].ToString() + "</p></div><br clear='all'/><br>";

            }
        }
        else
        {
            ltrPostagens.Text = "<p>Não há novas postagens</p>";
        }
    }

    public void CarregaSugestoesDeAmizade()
    {
        dvSugestoesAmizades.Visible = false;
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

    protected void btnPublicar_Click(object sender, EventArgs e)
    {
        if (txt_post.Text.Trim() != "")
        {
            string Post = new Util().checkForSQLInjectionString(txt_post.Text);

            FED oFED = new FED();
            oFED.FED_USR_ID = (int)Convert.ToInt64(Session["purple_id"]);
            oFED.FED_POT = Post;
            oFED.FED_DAT_INC = DateTime.Now;

            FED_DAO oFED_DAO = new FED_DAO();

            string idPost = oFED_DAO.InserirPost(oFED);
            CarregaPost();
            txt_post.Text = "";
        }
    }

    public int CarregaAmigoPendentes()
    {
        int total = 0;
        int usr_id = (int)Convert.ToInt64(Session["purple_id"]);
        if (Request.QueryString["perfil"] != null)
        {
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
                    sb.Append("<a href='Profile.aspx?perfil=" + lstBusca.Tables[0].Rows[i]["USR_ID"].ToString() + "' Style='Float:left;Margin:0px 16px 0px 0px;' title='" + lstBusca.Tables[0].Rows[i]["USR_NOM"].ToString() + "'>");
                    sb.Append("<img src='" + (lstBusca.Tables[0].Rows[i]["USR_FOT"].ToString() != "" ? lstBusca.Tables[0].Rows[i]["USR_FOT"].ToString().Replace("~", "..") : "../images/perfil/perfil.png") + "' width='65px' height='65px' style='margin-left:-10px;' />");
                    sb.Append("<br /> " + lstBusca.Tables[0].Rows[i]["USR_NOM"].ToString().Split(' ')[0].ToString() + "</a>");
                    total = total + 1;
                }
            }

            if (sb.ToString() != "")
            {

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
                            sb.Append("<a href='Profile.aspx?perfil=" + lstBusca.Tables[0].Rows[i]["USR_ID"].ToString() + "' Style='Float:left;Margin:0px 16px 0px 0px;' title='" + lstBusca.Tables[0].Rows[i]["USR_NOM"].ToString() + "'>");
                            sb.Append("<img src='" + (lstBusca.Tables[0].Rows[i]["USR_FOT"].ToString() != "" ? lstBusca.Tables[0].Rows[i]["USR_FOT"].ToString().Replace("~", "..") : "../images/perfil/perfil.png") + "' width='65px' height='65px' style='margin-left:-10px;' />");
                            sb.Append("<br /> " + lstBusca.Tables[0].Rows[i]["USR_NOM"].ToString().Split(' ')[0].ToString() + "</a>");
                            total = total + 1;
                        }
                    }
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
                        sb.Append("<a href='Profile.aspx?perfil=" + lstBusca.Tables[0].Rows[i]["USR_ID"].ToString() + "' Style='Float:left;Margin:0px 16px 0px 0px;' title='" + lstBusca.Tables[0].Rows[i]["USR_NOM"].ToString() + "'>");
                        sb.Append("<img src='" + (lstBusca.Tables[0].Rows[i]["USR_FOT"].ToString() != "" ? lstBusca.Tables[0].Rows[i]["USR_FOT"].ToString().Replace("~", "..") : "../images/perfil/perfil.png") + "' width='65px' height='65px' style='margin-left:-10px;' />");
                        sb.Append("<br /> " + lstBusca.Tables[0].Rows[i]["USR_NOM"].ToString().Split(' ')[0].ToString() + "</a>");
                        total = total + 1;
                    }
                }
            }
        }

        return total;
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
            lnkComunidades.Text = "("+lstCount.Tables[0].Rows[0]["total"].ToString()+") Comunidades";
        }
        else
        {
            lnkComunidades.Text = "(0) Comunidades";
        }
    }

    public void CarregaInteresses()
    {
        USR oUSR = new USR();
        oUSR.USR_ID =  (int)Convert.ToInt64(Session["purple_id"]);
        USR_DAO oUSR_DAO = new USR_DAO();
        var lstInteresse = oUSR_DAO.Listar(oUSR);
        if (lstInteresse.Tables[0].Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();

            int? areAtuacao = lstInteresse.Tables[0].Rows[0]["USR_ARE_ATU"].ToString() != "" ? Convert.ToInt32(lstInteresse.Tables[0].Rows[0]["USR_ARE_ATU"].ToString()) : (int?)null;

            if (areAtuacao == null)
            {
                ltrTitulo.Text = "Sugestões";
            }
            else
            {
                ltrTitulo.Text = "Interesses em comum";
            }

            CMD oCMD = new CMD();
            oCMD.ARE_ATU_ID = areAtuacao;
            CMD_DAO oCMD_DAO = new CMD_DAO();
            var lstComunidades = oCMD_DAO.ListarComunidadesRandomicamente(oCMD);
            if (lstComunidades.Tables[0].Rows.Count > 0)
            {
                dvInteresses.Visible = true;

                for (int i = 0; i < lstComunidades.Tables[0].Rows.Count; i++)
                {
                    sb.Append("<img style='float:left;' src='../images/icones/community-icon-6.png' width='30px' /> ");
                    sb.Append("<p style='float:left;margin-top:0px;width:135px;'> ");
                    sb.Append("<a Style='font-size:xx-small;' href='Comunidade.aspx?topico=" + lstComunidades.Tables[0].Rows[i]["ARE_ATU_ID"].ToString() + "&assunto=" + lstComunidades.Tables[0].Rows[i]["CMD_ID"].ToString() + "'>" + lstComunidades.Tables[0].Rows[i]["CMD_TIT"].ToString() + "</a>");
                    sb.Append("</p><br clear='all' /><br clear='all' />");
                }

                ltrInteresses.Text = sb.ToString();
            }
        }
    }
}
