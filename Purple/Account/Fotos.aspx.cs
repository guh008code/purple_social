using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class Fotos : System.Web.UI.Page
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


        }            
        
        CarregaPerfil();
        CarregarFotos();
        CarregaFotosPerfil();
        CarregaComunidades();
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

    public void CarregaPerfil()
    {
        int usr_id = (int)Convert.ToInt64(Session["purple_id"]);
        if (Request.QueryString["perfil"] != null)
        {
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

            nome = " " + lstCadastro.Tables[0].Rows[0]["USR_NOM"].ToString();
            lnkAlbumFotos.PostBackUrl = "Fotos.aspx?perfil=" + usr_id;

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

    public void CarregarFotos()
    {
        int usr_id = (int)Convert.ToInt64(Session["purple_id"]);
        if (Request.QueryString["perfil"] != null)
        {
            usr_id = (int)Convert.ToInt64(Request.QueryString["perfil"]);
        }

        StringBuilder sb = new StringBuilder();
        sb.Append("<p>");

        FOT oFOT = new FOT();
        oFOT.FOT_USR_ID = usr_id;
        FOT_DAO oFOT_DAO = new FOT_DAO();
        var lstFotos = oFOT_DAO.Listar(oFOT);
        if (lstFotos.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < lstFotos.Tables[0].Rows.Count; i++)
            {
                sb.Append("<a href='Foto.aspx?perfil=" + usr_id.ToString() + "&foto=" + lstFotos.Tables[0].Rows[i]["FOT_ID"].ToString() + "'><img src='" + lstFotos.Tables[0].Rows[i]["FOT_PAT"].ToString() + "' title='Clique aqui para visualizar a foto.' width='100px' /></a>");
            }
        }

        int total = 8 - lstFotos.Tables[0].Rows.Count;

        for (int i = 1; i <= total; i++)
        {
            sb.Append("<a href='Foto.aspx?perfil=" + usr_id.ToString() + "&foto=" + (0).ToString() + "'><img src='../images/perfil/perfil.png' title='Clique aqui para visualizar a foto.' width='100px' /></a>");
        }
        sb.Append("</p>");

        ltrFotos.Text = sb.ToString();
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
