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

public partial class Profile_perfil : System.Web.UI.Page
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
            CarregaAmigos();
            CarregaFotosPerfil();
            CarregarFotos();
            CarregaAmigosEmComum();
            CarregaCurtidas();
            CarregaComunidades();
            CarregaInteressesEmComum();
        }
    }

    public void CarregaInteressesEmComum()
    {
        int usr_id_lg = (int)Convert.ToInt64(Session["purple_id"]);
        if (Request.QueryString["perfil"] != null)
        {
            dvInteresses.Visible = true;
            int usr_id = (int)Convert.ToInt64(Request.QueryString["perfil"]);

            ltrInteresses.Text = "";

            USR oUSR_PERFIL = new USR();
            oUSR_PERFIL.USR_ID = usr_id;
            USR_DAO oUSR_DAO = new USR_DAO();

            var lstPerfil = oUSR_DAO.Listar(oUSR_PERFIL);
            if (lstPerfil.Tables[0].Rows.Count > 0)
            {
                oUSR_PERFIL = new USR();
                oUSR_PERFIL.USR_ID = usr_id_lg;
                oUSR_DAO = new USR_DAO();
                var lstLogado = oUSR_DAO.Listar(oUSR_PERFIL);
                if (lstLogado.Tables[0].Rows.Count > 0)
                {
                    if (lstPerfil.Tables[0].Rows[0]["USR_ARE_ATU"].ToString() == lstLogado.Tables[0].Rows[0]["USR_ARE_ATU"].ToString())
                    {
                        if (lstPerfil.Tables[0].Rows[0]["USR_ARE_ATU"].ToString() != "")
                        {
                            ltrInteresses.Text = ltrInteresses.Text + "<b>Área de atuação: </b>" + lstPerfil.Tables[0].Rows[0]["ARE_ATU_DES"].ToString() + "<br><br>";
                        }
                    }
                    if (lstPerfil.Tables[0].Rows[0]["USR_NVL_CNH"].ToString() == lstLogado.Tables[0].Rows[0]["USR_NVL_CNH"].ToString())
                    {
                        if (lstPerfil.Tables[0].Rows[0]["USR_NVL_CNH"].ToString() != "")
                        {
                            ltrInteresses.Text = ltrInteresses.Text + "<b>Nível de conhecimento: </b>" + lstPerfil.Tables[0].Rows[0]["NVL_CNH_DES"].ToString() + "<br><br>";
                        }
                    }
                    if (lstPerfil.Tables[0].Rows[0]["USR_LGA_PPG"].ToString() == lstLogado.Tables[0].Rows[0]["USR_LGA_PPG"].ToString())
                    {
                        if (lstPerfil.Tables[0].Rows[0]["USR_LGA_PPG"].ToString() != "")
                        {
                            ltrInteresses.Text = ltrInteresses.Text + "<b>Linguagem de Programação preferida: </b>" + lstPerfil.Tables[0].Rows[0]["USR_LGA_PPG"].ToString() + "<br><br>";
                        }
                    }

                    if (ltrInteresses.Text == "")
                    {
                        ltrInteresses.Text = "<p>Não há Interesses em comum.</p>";
                    }
                }
                else
                {
                    ltrInteresses.Text = "<p>Não há Interesses em comum.</p>";
                }
            }
            else
            {
                ltrInteresses.Text = "<p>Não há Interesses em comum.</p>";
            }
        }
        else
        {
            dvInteresses.Visible = false;
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

        var lstCadastro = oUSR_DAO.ListarProfile(oUSR);
        if (lstCadastro.Tables[0].Rows.Count > 0)
        {
            //foto
            imgPerfil.ImageUrl = lstCadastro.Tables[0].Rows[0]["USR_FOT"].ToString() != "" ? lstCadastro.Tables[0].Rows[0]["USR_FOT"].ToString() : "~/images/perfil/perfil.png";
    
            ltrSobreMim.Text = "<b>Nome: </b>" + lstCadastro.Tables[0].Rows[0]["USR_NOM"].ToString()+"<br><br>";
            ltrSobreMim.Text = ltrSobreMim.Text + "<b>Data de Nascimento: </b>" + Convert.ToDateTime(lstCadastro.Tables[0].Rows[0]["USR_DAT_NASC"].ToString()).ToShortDateString() + "<br><br>";
            ltrSobreMim.Text = ltrSobreMim.Text + "<b>Gênero: </b>" + lstCadastro.Tables[0].Rows[0]["USR_SEX"].ToString().ToUpper() + "<br><br>";
            ltrSobreMim.Text = ltrSobreMim.Text + "<b>E-mail: </b>" + lstCadastro.Tables[0].Rows[0]["USR_EMA"].ToString() + "<br><br>";

            ltrSobreMim.Text = ltrSobreMim.Text + "<b>Área de atuação: </b>" + (lstCadastro.Tables[0].Rows[0]["ARE_ATU_DES"].ToString() != "" ? lstCadastro.Tables[0].Rows[0]["ARE_ATU_DES"].ToString() : "Não informado") + "<br><br>";
            ltrSobreMim.Text = ltrSobreMim.Text + "<b>Nível de conhecimento: </b>" + (lstCadastro.Tables[0].Rows[0]["NVL_CNH_DES"].ToString() != "" ?lstCadastro.Tables[0].Rows[0]["NVL_CNH_DES"].ToString() : "Não informado." ) + "<br><br>";
            ltrSobreMim.Text = ltrSobreMim.Text + "<b>Linguagem de Programação preferida: </b>" + (lstCadastro.Tables[0].Rows[0]["USR_LGA_PPG"].ToString()!= "" ? lstCadastro.Tables[0].Rows[0]["USR_LGA_PPG"].ToString() : "Não informado." ) + "<br><br>";
            ltrSobreMim.Text = ltrSobreMim.Text + "<b>Data de Admissão: </b>" + (lstCadastro.Tables[0].Rows[0]["USR_DAT_ADM"].ToString() != "" ? Convert.ToDateTime(lstCadastro.Tables[0].Rows[0]["USR_DAT_ADM"].ToString()).ToShortDateString() : "Não informado.")  + "<br><br>";

            ltrSobreMim.Text = ltrSobreMim.Text + "<b>Data do cadastro: </b>" + Convert.ToDateTime(lstCadastro.Tables[0].Rows[0]["USR_DAT_INC"].ToString()).ToShortDateString() + "<br><br>";


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

    public void CarregaAmigosEmComum()
    {
        int usr_id = 0;
        int usr_id_logado = 0;

        usr_id_logado = (int)Convert.ToInt64(Session["purple_id"]);
        if (Request.QueryString["perfil"] != null)
        {
            usr_id = (int)Convert.ToInt64(Request.QueryString["perfil"]);
        }

        List<USR> lstAmigosPerfil = new List<USR>();
        List<USR> lstAmigosLogado = new List<USR>();
        StringBuilder sb = new StringBuilder();

        AMG oAMG = new AMG();
        oAMG.AMG_USR_ID = usr_id;
        oAMG.AMG_USR_USR_ID = usr_id;
        oAMG.AMG_OK = 1;
        AMG_DAO oAMG_DAO = new AMG_DAO();

        var lstBusca = oAMG_DAO.ListarAmigos(oAMG);
        if (lstBusca.Tables[0].Rows.Count > 0)
        {      
            for (int i = 0; i < lstBusca.Tables[0].Rows.Count; i++)
            {
                if (usr_id.ToString() != lstBusca.Tables[0].Rows[i]["USR_ID"].ToString())
                {
                    USR oUSR = new USR();
                    oUSR.USR_ID = lstBusca.Tables[0].Rows[i]["USR_ID"].ToString() != "" ? Convert.ToInt32(lstBusca.Tables[0].Rows[i]["USR_ID"].ToString()) : (int?)null;
                    oUSR.USR_NOM = lstBusca.Tables[0].Rows[i]["USR_NOM"].ToString();
                    oUSR.USR_FOT = lstBusca.Tables[0].Rows[i]["USR_FOT"].ToString();
                    lstAmigosPerfil.Add(oUSR);
                }
            }
        }

        oAMG = new AMG();
        oAMG.AMG_USR_ID = usr_id_logado;
        oAMG.AMG_USR_USR_ID = usr_id_logado;
        oAMG.AMG_OK = 1;
        oAMG_DAO = new AMG_DAO();

        lstBusca = oAMG_DAO.ListarAmigos(oAMG);
        if (lstBusca.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < lstBusca.Tables[0].Rows.Count; i++)
            {
                if (usr_id_logado.ToString() != lstBusca.Tables[0].Rows[i]["USR_ID"].ToString())
                {
                    USR oUSR = new USR();
                    oUSR.USR_ID = lstBusca.Tables[0].Rows[i]["USR_ID"].ToString() != "" ? Convert.ToInt32(lstBusca.Tables[0].Rows[i]["USR_ID"].ToString()) : (int?)null;
                    oUSR.USR_NOM = lstBusca.Tables[0].Rows[i]["USR_NOM"].ToString();
                    oUSR.USR_FOT = lstBusca.Tables[0].Rows[i]["USR_FOT"].ToString();
                    lstAmigosLogado.Add(oUSR);
                }
            }

            for (int i = 0; i < lstAmigosLogado.Count; i++)
            {
                for (int z = 0; z < lstAmigosPerfil.Count; z++)
                {
                    if (lstAmigosPerfil[z].USR_NOM == lstAmigosLogado[i].USR_NOM && lstAmigosPerfil[z].USR_FOT == lstAmigosLogado[i].USR_FOT)
                    {
                        sb.Append("<div Style='Float:left;width:66px;Margin:0px 3px 0px 3px;'>");
                        sb.Append("<a href='Profile.aspx?perfil=" + lstAmigosPerfil[z].USR_ID + "' Style='Margin:0px 10px 0px 10px;font-size: x-small;' title='" + lstAmigosPerfil[z].USR_NOM + "'>");
                        sb.Append("<img src='" + (lstAmigosPerfil[z].USR_FOT != "" ? lstAmigosPerfil[z].USR_FOT.Replace("~", "..") : "../images/perfil/perfil.png") + "' width='65px' height='65px' style='margin-left:-12px;' />");
                        sb.Append("<br /> " + lstAmigosPerfil[z].USR_NOM.Split(' ')[0].ToString() + "</a>");
                        sb.Append("</div>");
                    }
                }
            }
        }


        if (sb.ToString() != "")
        {
            ltrAmigoEmComum.Text = "<p>" + sb.ToString() + "</p>";
            fdAmigoComum.Visible = true;
        }
        else
        {
            fdAmigoComum.Visible = false;
            ltrAmigoEmComum.Text = "";
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
            if (lstFotos.Tables[0].Rows.Count > 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    sb.Append("<a href='Foto.aspx?perfil=" + usr_id.ToString() + "&foto=" + lstFotos.Tables[0].Rows[i]["FOT_ID"].ToString() + "'><img src='" + lstFotos.Tables[0].Rows[i]["FOT_PAT"].ToString() + "' title='Clique aqui para visualizar a foto.' width='80px' /></a>");
                }
            }
            else
            {
                for (int i = 0; i < lstFotos.Tables[0].Rows.Count; i++)
                {
                    sb.Append("<a href='Foto.aspx?perfil=" + usr_id.ToString() + "&foto=" + lstFotos.Tables[0].Rows[i]["FOT_ID"].ToString() + "'><img src='" + lstFotos.Tables[0].Rows[i]["FOT_PAT"].ToString() + "' title='Clique aqui para visualizar a foto.' width='80px' /></a>");
                }
            }
        }
        else
        {
            sb.Append("Não Há Fotos.");
        }
        sb.Append("</p>");

        ltrFotos.Text = sb.ToString();
    }

    public void CarregaCurtidas()
    {
        int usr_id = (int)Convert.ToInt64(Session["purple_id"]);
        if (Request.QueryString["perfil"] != null)
        {
            usr_id = (int)Convert.ToInt64(Request.QueryString["perfil"]);
            lnkCurtir.Visible = true;
        }
        else
        {
            lnkCurtir.Visible = false;
        }


        RMD oAMG = new RMD();
        //oAMG.RMD_USR_ID = usr_id;
        oAMG.RMD_USR_USR_ID = usr_id;
        RMD_DAO oAMG_DAO = new RMD_DAO();

        int curtidas = 0;

        var lstBusca = oAMG_DAO.Listar(oAMG);
        if (lstBusca.Tables[0].Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < lstBusca.Tables[0].Rows.Count; i++)
            {
                if (usr_id.ToString() != lstBusca.Tables[0].Rows[i]["RMD_USR_ID"].ToString())
                {
                    curtidas = curtidas + 1;
                }

                if (lstBusca.Tables[0].Rows[i]["RMD_USR_ID"].ToString() == Session["purple_id"].ToString())
                {
                    lnkCurtir.Text = "Desrecomendar";
                }
            }

            if (curtidas > 0)
            {
                if (curtidas == 1)
                {
                    ltrCurtidas.Text = "(" + curtidas.ToString() + ") Recomendação";
                }
                else
                {
                    ltrCurtidas.Text = "(" + curtidas.ToString() + ") Recomendações";
                }        
            }
            else
            {

                ltrCurtidas.Text = "(0) Recomendação";
            }
        }
        else
        {
            ltrCurtidas.Text = "(0) Recomendação";
        }
    }

    protected void lnkCurtir_Click(object sender, EventArgs e)
    {
        if (lnkCurtir.Text == "Recomendar")
        {
            //inserir
            RMD oAMG = new RMD();
            oAMG.RMD_USR_ID = (int)Convert.ToInt64(Session["purple_id"]);
            oAMG.RMD_USR_USR_ID = (int)Convert.ToInt64(Request.QueryString["perfil"]);
            oAMG.RMD_DAT_INC = DateTime.Now;
            RMD_DAO oAMG_DAO = new RMD_DAO();
            oAMG_DAO.Inserir(oAMG);

            int valor = Convert.ToInt32(ltrCurtidas.Text.Split(')')[0].Replace("(", "").ToString());

            if ((valor + 1) == 1)
            {
                ltrCurtidas.Text = "(" + (valor + 1).ToString() + ") Recomendação";
            }
            else
            {
                ltrCurtidas.Text = "(" + (valor + 1).ToString() + ") Recomendações";
            }

            lnkCurtir.Text = "Desrecomendar";
        }
        else
        {
            //excluir
            RMD oAMG = new RMD();
            oAMG.RMD_USR_ID = (int)Convert.ToInt64(Session["purple_id"]);
            oAMG.RMD_USR_USR_ID = (int)Convert.ToInt64(Request.QueryString["perfil"]);
            oAMG.RMD_DAT_INC = DateTime.Now;
            RMD_DAO oAMG_DAO = new RMD_DAO();
            oAMG_DAO.Excluir(oAMG);

            int valor = Convert.ToInt32(ltrCurtidas.Text.Split(')')[0].Replace("(", "").ToString());

            if ((valor - 1) == 0)
            {
                ltrCurtidas.Text = "(" + (valor - 1).ToString() + ") Recomendação";
            }
            if ((valor - 1) == 1)
            {
                ltrCurtidas.Text = "(" + (valor - 1).ToString() + ") Recomendação";
            }
            else
            {
                ltrCurtidas.Text = "(" + (0).ToString() + ") Recomendações";
            }

            lnkCurtir.Text = "Recomendar";
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

        StringBuilder sb = new StringBuilder();
        var lstCount = oCMD_DAO.ListarComunidades(oCMD);
        if (lstCount.Tables[0].Rows.Count > 0)
        {
            lnkComunidades.Text = "(" + lstCount.Tables[0].Rows[0]["total"].ToString() + ") Comunidades";

            lstCount = oCMD_DAO.ListarComunidadesRandomicamente(oCMD);
            for (int i = 0; i < lstCount.Tables[0].Rows.Count; i++)
            {
                sb.Append("<div Style='Float:left;width:80px;Margin:0px 3px 0px 3px;'>");
                sb.Append("<a href='Comunidade.aspx?topico=" + lstCount.Tables[0].Rows[i]["ARE_ATU_ID"].ToString() + "&assunto=" + lstCount.Tables[0].Rows[i]["CMD_ID"].ToString() + "' Style='Margin:0px 10px 0px 10px;font-size: x-small;' title='" + lstCount.Tables[0].Rows[i]["CMD_TIT"].ToString() + "'>");
                sb.Append("<img src='../images/ico/simbolo_logo.png' width='65px' height='65px' style='margin-left:-12px;' />");
                sb.Append("<br /> " + lstCount.Tables[0].Rows[i]["CMD_TIT"].ToString() + "</a>");
                sb.Append("</div>");
            }

        }
        else
        {
            lnkComunidades.Text = "(0) Comunidades";
        }


        if (sb.ToString() == "")
        {
            ltrMinhasComunidades.Text = "<p> Não possuí nenhuma comunidade.</p>";
        }
        else
        {
            ltrMinhasComunidades.Text = "<p> " + sb.ToString() + "</p>";
        }
    }
}
