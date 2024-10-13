using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class AlterarFoto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["purple_id"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            //btnSalvar.Attributes.Add("Style", "Display:none");
            //upload_perfil.Attributes.Add("onchange", "__doPostBack('btnSalvar','')");
            CarregaPerfil();
            CarregaFotosPerfil();
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

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        string sCaminho = "";
        try
        {
            if (upload_perfil.PostedFile.FileName == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alerta", "alert('Selecione um arquivo (.jpg, jpeg e .png) para upload.');", true);
                return;
            }

            if (upload_perfil.PostedFile.FileName.ToLower().Trim().EndsWith(".jpg") && upload_perfil.PostedFile.FileName.ToLower().Trim().EndsWith(".jpeg") && upload_perfil.PostedFile.FileName.ToLower().Trim().EndsWith(".png"))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alerta", "alert('Arquivo com extensão e formato inválido. Somente extensão(.jpg, jpeg e .png).');", true);
                return;
            }

            //string tamanhoArq = "2";

            //if (upload_perfil.PostedFile.ContentLength >= ReturnMegaBytes((tamanhoArq != "" ? (int)Convert.ToInt64(tamanhoArq) : 1)))
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alerta", "alert('Arquivo maior que " + tamanhoArq + "MB permitidos.');", true);
            //    return;
            //}

            string caminho = "";
            string path_save = "";
            string linha = string.Empty;

            //caminho = "~/" + "UPL" + "/IMG_PERFIL/" + Session["purple_id"].ToString() + "/";
            caminho = "../" + "UPL" + "/IMG_PERFIL/";
            path_save = Server.MapPath(caminho);

            string file = DateTime.UtcNow.ToFileTimeUtc() + "_" + upload_perfil.FileName.Replace(" ", "_");
            sCaminho = path_save + file;
            string sCaminhoThumb = path_save + "thu_"+ file;
            upload_perfil.SaveAs(@sCaminho);

            USR oUSR = new USR();
            oUSR.USR_ID = (int)Convert.ToInt64(Session["purple_id"]);
            oUSR.USR_FOT = caminho + "thu_" + file;
            USR_DAO oUSR_DAO = new USR_DAO();

            oUSR_DAO.SalvarFoto(oUSR);

            //Util.Resize(@sCaminho, @sCaminho, 150, 150);

            Util.GenerateThumbImage(@sCaminho, @sCaminhoThumb, 150, 150);

            if (imgPerfil.ImageUrl != "../images/perfil/perfil.png" || imgPerfil.ImageUrl != "~/images/perfil/perfil.png")
            {
                File.Delete(Server.MapPath(imgPerfil.ImageUrl));
            }

            imgPerfil.ImageUrl = caminho + "thu_" + file;

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

    }

    public long ReturnMegaBytes(int Byte)
    {
        long valor = Byte * 1024;
        valor = valor * 1024;
        return valor;
    }
}
