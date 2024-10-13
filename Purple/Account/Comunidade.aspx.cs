using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class Comunidades : System.Web.UI.Page
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

            if (Request.QueryString["feed"] != null)
            {
                CMD_PTP oCMD_PTP = new CMD_PTP();
                oCMD_PTP.CMD_PTP_ID = Convert.ToInt32(Request.QueryString["feed"]);
                oCMD_PTP.CMD_PTP_USR_ID = (int)Convert.ToInt64(Session["purple_id"]);
                CMD_PTP_DAO oCMD_PTP_DAO = new CMD_PTP_DAO();
                oCMD_PTP_DAO.Excluir(oCMD_PTP);
            }

            if (Request.QueryString["topico"] != null)
            {
                ARE_ATU oARE_ATU = new ARE_ATU();
                oARE_ATU.ARE_ATU_ID = Convert.ToInt32(Request.QueryString["topico"]);
                ARE_ATU_DAO oARE_ATU_DAO = new ARE_ATU_DAO();
                var lstFotos = oARE_ATU_DAO.Listar(oARE_ATU);
                if (lstFotos.Tables[0].Rows.Count > 0)
                {
                    lnkComunidade.Text = lstFotos.Tables[0].Rows[0]["ARE_ATU_DES"].ToString();
                    lnkComunidade.PostBackUrl = "Comunidades.aspx?topico=" + lstFotos.Tables[0].Rows[0]["ARE_ATU_ID"].ToString();
                }

                if (Request.QueryString["assunto"] != null)
                {
                    if (Request.QueryString["assunto"] != "0")
                    {
                        CMD oCMD = new CMD();
                        oCMD.CMD_ID = Convert.ToInt32(Request.QueryString["assunto"]);
                        CMD_DAO oCMD_DAO = new CMD_DAO();
                        var lsAssunto = oCMD_DAO.Listar(oCMD);
                        if (lsAssunto.Tables[0].Rows.Count > 0)
                        {
                            txt_titulo.Text = lsAssunto.Tables[0].Rows[0]["CMD_TIT"].ToString();
                            txt_descricao.Text = lsAssunto.Tables[0].Rows[0]["CMD_DES"].ToString();
                            btnCriar.Visible = false;
                            btnCancelar.Visible = false;
                            txt_titulo.Enabled = false;
                            txt_descricao.Enabled = false;
                            CarregaPost();
                            btnResponder.Visible = true;
                            fieldPostagens.Visible = true;
                        }
                    }
                }
            }
        }

        txt_descricao.Attributes.Add("maxlength", "200");
        txt_descricao.Attributes.Add("onkeyup", "return ismaxlength(this);");

        CarregaPerfil();
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

    public void CarregaPost()
    {
        CMD_PTP oCMD_PTP = new CMD_PTP();
        oCMD_PTP.CMD_PTP_CMD_ID = Convert.ToInt32(Request.QueryString["assunto"].ToString());
        CMD_PTP_DAO oCMD_PTP_DAO = new CMD_PTP_DAO();

        ltrPostagens.Text = "";
        var lstPosts = oCMD_PTP_DAO.Listar(oCMD_PTP);
        if (lstPosts.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < lstPosts.Tables[0].Rows.Count; i++)
            {
                ltrPostagens.Text = ltrPostagens.Text + "<div style='float:left;border:1px solid #ccc;width:448px;'>";
                ltrPostagens.Text = ltrPostagens.Text + "<p style='float:left;'>";
                ltrPostagens.Text = ltrPostagens.Text + "<a href='Profile.aspx?perfil=" + lstPosts.Tables[0].Rows[i]["USR_ID"].ToString() + "'><img style='margin-top:5px;' src='" + (lstPosts.Tables[0].Rows[i]["USR_FOT"].ToString() != "" ? lstPosts.Tables[0].Rows[i]["USR_FOT"].ToString().Replace("~", "..") : "../images/perfil/perfil.png") + "' width='40px' /></a></p> ";

                if (Session["purple_id"].ToString() == lstPosts.Tables[0].Rows[i]["USR_ID"].ToString())
                {
                    ltrPostagens.Text = ltrPostagens.Text + "<p><b><a href='Profile.aspx?perfil=" + lstPosts.Tables[0].Rows[i]["USR_ID"].ToString() + "'>" + lstPosts.Tables[0].Rows[i]["USR_NOM"].ToString() + "</a> - " + lstPosts.Tables[0].Rows[i]["CMD_PTP_DAT_INC"].ToString() + " <a style='float:right;text-decoration:none;' OnClientClick='javascript:if(!confirm('Deseja realmente excluir?')){return false;}' href='Comunidade.aspx?topico=" + Request.QueryString["topico"].ToString() +"&assunto=" + Request.QueryString["assunto"].ToString() + "&feed=" + lstPosts.Tables[0].Rows[i]["CMD_PTP_ID"].ToString() + "'>(X)</a></b><br>";
                }
                else
                {
                    ltrPostagens.Text = ltrPostagens.Text + "<p><b><a href='Profile.aspx?perfil=" + lstPosts.Tables[0].Rows[i]["USR_ID"].ToString() + "'>" + lstPosts.Tables[0].Rows[i]["USR_NOM"].ToString() + "</a> - " + lstPosts.Tables[0].Rows[i]["CMD_PTP_DAT_INC"].ToString() + "</b><br>";
                }

                ltrPostagens.Text = ltrPostagens.Text + lstPosts.Tables[0].Rows[i]["CMD_PTP_DES"].ToString() + "</p></div><br clear='all'/><br>";

            }
        }
        else
        {
            ltrPostagens.Text = "<p>Não há respostas.</p>";
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

    protected void btnCriar_Click(object sender, EventArgs e)
    {
        CMD oCMD = new CMD();
        oCMD.CMD_USR_ID = Convert.ToInt32(Session["purple_id"]);
        oCMD.CMD_TIT = txt_titulo.Text.ToUpper();
        oCMD.CMD_DES = txt_descricao.Text;
        oCMD.CMD_DAT_INC = DateTime.Now;
        oCMD.CMD_DAT_ALT = DateTime.Now;
        oCMD.ARE_ATU_ID = Convert.ToInt32(Request.QueryString["topico"].ToString());
        CMD_DAO oCMD_DAO = new CMD_DAO();
        string id = oCMD_DAO.Inserir(oCMD);
        Response.Redirect("Comunidade.aspx?topico=" + Request.QueryString["topico"].ToString() + "&assunto=" + id);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Comunidades.aspx?topico=" + Request.QueryString["topico"].ToString());
    }
    protected void btnResponder_Click(object sender, EventArgs e)
    {
        int usr_id = (int)Convert.ToInt64(Session["purple_id"]);

        USR oUSR = new USR();
        oUSR.USR_ID = usr_id;
        USR_DAO oUSR_DAO = new USR_DAO();
        var lstUsr = oUSR_DAO.Listar(oUSR);
        if (lstUsr.Tables[0].Rows.Count > 0)
        {
            if (lstUsr.Tables[0].Rows[0]["USR_ASS"].ToString() == "1")
            {
                fieldPostagens.Visible = false;
                fieldResposta.Visible = true;
                btnResponder.Visible = false;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alerta", "alert('Resposta de tópicos apenas para os membros assinantes.');", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alerta", "alert('Acesso apenas para assinantes.');", true);
        }
    }
    protected void btnSalvarResposta_Click(object sender, EventArgs e)
    {
        CMD_PTP oCMD_PTP = new CMD_PTP();
        oCMD_PTP.CMD_PTP_CMD_ID = Convert.ToInt32(Request.QueryString["assunto"].ToString());
        oCMD_PTP.CMD_PTP_DES = txt_resposta.Text;
        oCMD_PTP.CMD_PTP_DAT_INC = DateTime.Now;
        oCMD_PTP.CMD_PTP_USR_ID = (int)Convert.ToInt64(Session["purple_id"]);
        CMD_PTP_DAO oCMD_PTP_DAO = new CMD_PTP_DAO();

        oCMD_PTP_DAO.Inserir(oCMD_PTP);

        Response.Redirect("Comunidade.aspx?topico=" + Request.QueryString["topico"].ToString() + "&assunto=" + Request.QueryString["assunto"].ToString());

        fieldPostagens.Visible = true;
        fieldResposta.Visible = false;
        btnResponder.Visible = true;
    }
    protected void btnCancelarResposta_Click(object sender, EventArgs e)
    {
        fieldPostagens.Visible = true;
        fieldResposta.Visible = false;
        btnResponder.Visible = true;
    }
}
