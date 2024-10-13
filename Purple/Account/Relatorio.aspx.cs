using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Configuration;

public partial class Relatorio : System.Web.UI.Page
{
    public string nome = "mim";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["purple_id"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                if (Request.QueryString["perfil"] != null)
                {
                    if (Request.QueryString["perfil"].ToString() == Session["purple_id"].ToString())
                    {
                        Response.Redirect("Profile.aspx");
                    }
                }
            }

            CarregaPerfil();
            CarregaFotosPerfil();
            CarregaRelatorio();
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
    

            nome = lstCadastro.Tables[0].Rows[0]["USR_NOM"].ToString();

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

    public void CarregaRelatorio()
    {
        ACS oACS = new ACS();
        ACS_DAO oACS_DAO = new ACS_DAO();

        gvMaisConectados.DataSource = oACS_DAO.ListarAcessos(oACS);
        gvMaisConectados.DataBind();


        AMG oAMG = new AMG();
        AMG_DAO oAMG_DAO = new AMG_DAO();
        var lstRelatorios = oAMG_DAO.ListarRelatorio(oAMG);

        RelatorioEnt oRelatorioEnt = new RelatorioEnt();
        List<RelatorioEnt> lstRel = new List<RelatorioEnt>();

        string idUser = "0";

        if (lstRelatorios.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < lstRelatorios.Tables[0].Rows.Count; i++)
            {
                if (idUser != lstRelatorios.Tables[0].Rows[i]["USR_ID"].ToString())
                {
                    oRelatorioEnt = new RelatorioEnt();
                    oRelatorioEnt.ID_USUARIO = Convert.ToInt32(lstRelatorios.Tables[0].Rows[i]["USR_ID"].ToString());
                    oRelatorioEnt.USUARIO = lstRelatorios.Tables[0].Rows[i]["USR_NOM"].ToString();
                    oRelatorioEnt.quantidade_amigos = Convert.ToInt32(lstRelatorios.Tables[0].Rows[i]["total"].ToString());
                    oRelatorioEnt.email = lstRelatorios.Tables[0].Rows[i]["USR_EMA"].ToString();
                    lstRel.Add(oRelatorioEnt);
                    idUser = lstRelatorios.Tables[0].Rows[i]["USR_ID"].ToString();
                }
                else
                {
                    lstRel[lstRel.Count - 1].quantidade_amigos = lstRel[lstRel.Count - 1].quantidade_amigos + Convert.ToInt32(lstRelatorios.Tables[0].Rows[i]["total"].ToString());
                }
            }
        }

        gvMaisAmigos.DataSource = lstRel;
        gvMaisAmigos.DataBind();

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

            string nome = ((Label)Master.FindControl("lblNome")).Text;
            EnviarEmail("<b>" + nome + "</b> Realizou uma solicitação de amizade. <br/>\n", (int)Convert.ToInt64(Request.QueryString["perfil"]));
            

            //solicitar amizade

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

            //A pessoa aceitou a sua solicitação
            string nome = ((Label)Master.FindControl("lblNome")).Text;
            EnviarEmail("<b>" + nome + "</b> Aceitou a sua solicitação de amizade. <br/>\n", (int)Convert.ToInt64(Request.QueryString["perfil"]));
            
            btnAmizade.Text = "Desfazer Amizade";
            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }

    public void EnviarEmail(string mensagem, int idUserEnvio)
    {
        USR oUsuario = new USR();
        USR_DAO oUsuarioDAO = new USR_DAO();
        oUsuario.USR_ID = idUserEnvio;
        var dsLstUsuario = oUsuarioDAO.Listar(oUsuario);
        if (dsLstUsuario.Tables[0].Rows.Count > 0)
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

                sListaDestinatario = dsLstUsuario.Tables[0].Rows[0]["USR_EMA"].ToString().Trim();
                sTitulo = "oPurple social science - Nova solicitação de amizade.";

                sAssunto = "<a href='http://www.opurple.com.br/'>";
                sAssunto += "<img src='http://www.opurple.com.br/images/logo/logo_name.png' /></a><br/><br/>\n";
                sAssunto += "Olá <b>" + dsLstUsuario.Tables[0].Rows[0]["USR_NOM"].ToString() + "</b>. <br/>\n";
                sAssunto += mensagem + "<br/>\n";
                sAssunto += "<br/>\n";
                sAssunto += "<br/>\n";
                sAssunto += "<a href='http://www.opurple.com.br/Account/Amigos.aspx'> Visualizar solicitações e amizades? </a><br/>\n";
                sAssunto += "<br/>\n";
                sAssunto += "<br/>\n";
                sAssunto += "<b>Esse e-mail é automático e não necessita ser respondido.</b><br/>\n";
                Eml oEmailDAO = new Eml();
                oEmailDAO.EnviarEmail(sRemetente, sListaDestinatario, sListaCopia, sListaCopiaOculta, bFormatoHTML, sTitulo, sAssunto, sListaCaminhoAnexo, sServer, bSSL, iPortaSmtp, sUsuario, sSenha);

                //FailureText.Text = "<br>Informações com a recuperação de senha foi enviado para o seu e-mail.<br><b>Verifique sua caixa de entrada ou SPAM.";
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alerta", "alert('E-mail com a recuperação de senha foi enviado.');", true);
            }
            catch (Exception ex)
            {
                string erro = ex.Message;

                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alerta", "alert('" + erro + "');", true);
            }
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
