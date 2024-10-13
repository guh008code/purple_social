using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class Foto : System.Web.UI.Page
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
            CarregarFoto();
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

    public void CarregarFoto()
    {
        int usr_id = (int)Convert.ToInt64(Session["purple_id"]);
        if (Request.QueryString["perfil"] != null)
        {
            usr_id = (int)Convert.ToInt64(Request.QueryString["perfil"]);
        }

        int foto = 0;
        if (Request.QueryString["foto"] != null)
        {
            foto = (int)Convert.ToInt64(Request.QueryString["foto"]);
        }

        FOT oFOT = new FOT();
        oFOT.FOT_ID = foto;
        oFOT.FOT_USR_ID = usr_id;
        FOT_DAO oFOT_DAO = new FOT_DAO();
        var lstFotos = oFOT_DAO.Listar(oFOT);
        if (lstFotos.Tables[0].Rows.Count > 0)
        {
            txt_legenda.Text = lstFotos.Tables[0].Rows[0]["FOT_LEG"].ToString();
            imgFoto.ImageUrl = lstFotos.Tables[0].Rows[0]["FOT_PAT"].ToString();
            imgFoto.ToolTip = lstFotos.Tables[0].Rows[0]["FOT_LEG"].ToString();

            btnExcluir.Visible = true;
        }
        else
        {
            txt_legenda.Text = "";
            imgFoto.ImageUrl = "../images/perfil/perfil.png";
            imgFoto.ToolTip = "";

            btnExcluir.Visible = false;
        }

        if (Request.QueryString["perfil"] != null)
        {
            if (Session["purple_id"].ToString() != Request.QueryString["perfil"].ToString())
            {
                txt_legenda.Enabled = false;
                upload_perfil.Visible = false;
                dvBotoes.Visible = false;
                btnExcluir.Visible = false;
            }
            else
            {
                txt_legenda.Enabled = true;
                upload_perfil.Visible = true;
                dvBotoes.Visible = true;
            }
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
            lnkFotosAlbum.PostBackUrl = "Fotos.aspx?perfil=" + usr_id;

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

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["foto"] == null)
        {
            Response.Redirect("Foto.aspx?perfil=" + Session["purple_id"].ToString() + "&foto=0");
        }

        bool validado = true;
        string sCaminho = "";
        try
        {
            if (Request.QueryString["foto"].ToString() == "0")
            {
                if (txt_legenda.Text != "")
                {
                    if (new Util().checkForSQLInjection(txt_legenda.Text) && validado)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alerta", "alert(' Legenda possuí caracteres com informações inválidas.');", true);
                        validado = false;
                    }
                }

                if (upload_perfil.PostedFile.FileName == "" && validado)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alerta", "alert('Selecione um arquivo (.jpg, jpeg e .png) para upload.');", true);
                    validado = false;
                }

                if (upload_perfil.PostedFile.FileName.ToLower().Trim().EndsWith(".jpg") && upload_perfil.PostedFile.FileName.ToLower().Trim().EndsWith(".jpeg") && upload_perfil.PostedFile.FileName.ToLower().Trim().EndsWith(".png") && validado)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alerta", "alert('Arquivo com extensão e formato inválido. Somente extensão(.jpg, jpeg e .png).');", true);
                    validado = false;
                }

                string tamanhoArq = "2";

                if (upload_perfil.PostedFile.ContentLength >= new Util().ReturnMegaBytes((tamanhoArq != "" ? (int)Convert.ToInt64(tamanhoArq) : 1)) && validado)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alerta", "alert('Arquivo maior que " + tamanhoArq + "MB permitidos.');", true);
                    validado = false;
                }

                if (validado)
                {
                    string caminho = "";
                    string path_save = "";
                    string linha = string.Empty;

                    //caminho = "~/" + "UPL" + "/IMG_PERFIL/" + Session["purple_id"].ToString() + "/";
                    caminho = "../" + "UPL" + "/IMG_FOTOS/";
                    path_save = Server.MapPath(caminho);

                    string file = DateTime.UtcNow.ToFileTimeUtc() + "_" + upload_perfil.FileName.Replace(" ", "_");
                    sCaminho = path_save + file;
                    upload_perfil.SaveAs(@sCaminho);

                    FOT oUSR = new FOT();
                    oUSR.FOT_USR_ID = (int)Convert.ToInt64(Session["purple_id"]);
                    oUSR.FOT_PAT = caminho + file;
                    oUSR.FOT_LEG = txt_legenda.Text;
                    oUSR.FOT_DAT_INC = DateTime.Now;
                    FOT_DAO oUSR_DAO = new FOT_DAO();
                    string id = oUSR_DAO.Salvar(oUSR);

                    Response.Redirect("Foto.aspx?perfil=" + Session["purple_id"].ToString() + "&foto=" + id);
                }
            }
            else
            {
                if (upload_perfil.PostedFile.FileName != "")
                {
                    if (txt_legenda.Text != "")
                    {
                        if (new Util().checkForSQLInjection(txt_legenda.Text) && validado)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alerta", "alert(' Legenda possuí caracteres com informações inválidas.');", true);
                            validado = false;
                        }
                    }

                    if (upload_perfil.PostedFile.FileName == "" && validado)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alerta", "alert('Selecione um arquivo (.jpg, jpeg e .png) para upload.');", true);
                        validado = false;
                    }

                    if (upload_perfil.PostedFile.FileName.ToLower().Trim().EndsWith(".jpg") && upload_perfil.PostedFile.FileName.ToLower().Trim().EndsWith(".jpeg") && upload_perfil.PostedFile.FileName.ToLower().Trim().EndsWith(".png") && validado)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alerta", "alert('Arquivo com extensão e formato inválido. Somente extensão(.jpg, jpeg e .png).');", true);
                        validado = false;
                    }

                    string tamanhoArq = "2";

                    if (upload_perfil.PostedFile.ContentLength >= new Util().ReturnMegaBytes((tamanhoArq != "" ? (int)Convert.ToInt64(tamanhoArq) : 1)) && validado)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alerta", "alert('Arquivo maior que " + tamanhoArq + "MB permitidos.');", true);
                        validado = false;
                    }

                    if (validado)
                    {
                        File.Delete(Server.MapPath(imgFoto.ImageUrl));

                        FOT oFOT = new FOT();
                        oFOT.FOT_ID = (int)Convert.ToInt64(Request.QueryString["foto"]);
                        oFOT.FOT_USR_ID = (int)Convert.ToInt64(Session["purple_id"]);
                        FOT_DAO oFOT_DAO = new FOT_DAO();
                        oFOT_DAO.Excluir(oFOT);

                        string caminho = "";
                        string path_save = "";
                        string linha = string.Empty;

                        //caminho = "~/" + "UPL" + "/IMG_PERFIL/" + Session["purple_id"].ToString() + "/";
                        caminho = "../" + "UPL" + "/IMG_FOTOS/";
                        path_save = Server.MapPath(caminho);

                        string file = DateTime.UtcNow.ToFileTimeUtc() + "_" + upload_perfil.FileName.Replace(" ", "_");
                        sCaminho = path_save + file;
                        upload_perfil.SaveAs(@sCaminho);

                        FOT oUSR = new FOT();
                        oUSR.FOT_USR_ID = (int)Convert.ToInt64(Session["purple_id"]);
                        oUSR.FOT_PAT = caminho + file;
                        oUSR.FOT_LEG = txt_legenda.Text;
                        oUSR.FOT_DAT_INC = DateTime.Now;
                        FOT_DAO oUSR_DAO = new FOT_DAO();
                        string id = oUSR_DAO.Salvar(oUSR);

                        Response.Redirect("Foto.aspx?perfil=" + Session["purple_id"].ToString() + "&foto=" + id);
                    }
                }
                else
                {
                    FOT oUSR = new FOT();
                    oUSR.FOT_ID = (int)Convert.ToInt64(Request.QueryString["foto"]);
                    oUSR.FOT_USR_ID = (int)Convert.ToInt64(Session["purple_id"]);
                    oUSR.FOT_LEG = txt_legenda.Text;
                    FOT_DAO oUSR_DAO = new FOT_DAO();
                    oUSR_DAO.Salvar(oUSR);

                    Response.Redirect("Foto.aspx?perfil=" + Session["purple_id"].ToString() + "&foto=" + Request.QueryString["foto"].ToString());
                }
            }

        }
        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alerta", "alert('Erro ao realizar o upload da imagem. '" + ex.Message.ToString() + "'');", true);
            Response.Write(ex.Message.ToString() + "<br>");
            Response.Write(sCaminho + "<br>");
            Response.Flush();
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        int foto = 0;
        if (Request.QueryString["foto"] != null)
        {
            foto = (int)Convert.ToInt64(Request.QueryString["foto"]);
        }

        try
        {
            File.Delete(Server.MapPath(imgFoto.ImageUrl));

            FOT oFOT = new FOT();
            oFOT.FOT_ID = foto;
            oFOT.FOT_USR_ID = (int)Convert.ToInt64(Session["purple_id"]);
            FOT_DAO oFOT_DAO = new FOT_DAO();
            oFOT_DAO.Excluir(oFOT);

            Response.Redirect("Foto.aspx?perfil=" + Session["purple_id"].ToString() + "&foto=0");
        }
        catch 
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alerta", "alert('Ocorreu um problema ao excluir a foto.');", true);
        }
    }
}
