using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

public class USR_DAO
{
    Banco dbConn = new Banco();
    private string SQL { get; set; }

    public DataSet Listar(USR Usuario)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();
        string sCond = "";
        string sSQL = "";

        sSQL = "SELECT * ";
        sSQL = sSQL + "FROM USR ";
        sSQL = sSQL + "LEFT JOIN ARE_ATU ON ARE_ATU_ID = USR_ARE_ATU ";
        sSQL = sSQL + "LEFT JOIN NVL_CNH ON NVL_CNH_ID = USR_NVL_CNH ";

        if (!string.IsNullOrEmpty(Usuario.USR_ID.ToString()))
        {
            sCond = sCond + " USR_ID = @USR_ID AND ";
            DbParameter dbparUSR_ID = dbConn.CreateParameter("@USR_ID", Usuario.USR_ID);
            dbParameterList.Add(dbparUSR_ID);
        }
        if (!string.IsNullOrEmpty(Usuario.USR_NOM))
        {
            sCond = sCond + " USR_NOM like @USR_NOM AND ";
            DbParameter dbparUSR_NOM = dbConn.CreateParameter("@USR_NOM", '%' + Usuario.USR_NOM + '%');
            dbParameterList.Add(dbparUSR_NOM);
        }
        if (!string.IsNullOrEmpty(Usuario.USR_SNH))
        {
            sCond = sCond + " USR_SNH = @USR_SNH AND ";
            DbParameter dbparUSR_SNH = dbConn.CreateParameter("@USR_SNH", Usuario.USR_SNH);
            dbParameterList.Add(dbparUSR_SNH);
        }
        if (!string.IsNullOrEmpty(Usuario.USR_EMA))
        {
            sCond = sCond + " USR_EMA = @USR_EMA AND ";
            DbParameter dbparUSR_EMA = dbConn.CreateParameter("@USR_EMA", Usuario.USR_EMA);
            dbParameterList.Add(dbparUSR_EMA);
        }
        if (!string.IsNullOrEmpty(Usuario.USR_EML_VLD.ToString()))
        {
            sCond = sCond + " USR_EML_VLD = @USR_EML_VLD AND ";
            DbParameter dbparUSR_EML_VLD = dbConn.CreateParameter("@USR_EML_VLD", Usuario.USR_EML_VLD);
            dbParameterList.Add(dbparUSR_EML_VLD);
        }

        if (sCond != "")
        {
            sCond = sCond.Substring(1, sCond.Length - 5);
            sSQL = sSQL + " WHERE " + sCond;
        }

        SQL = dbConn.RetParam(dbParameterList);
        SQL += sSQL;

        return dbConn.ExecutarRetorno(sSQL, dbParameterList);
    }

    public DataSet ListarBusca(USR Usuario)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();
        string sCond = "";
        string sSQL = "";

        sSQL = "SELECT * ";
        sSQL = sSQL + "FROM USR ";

        if (!string.IsNullOrEmpty(Usuario.USR_ID.ToString()))
        {
            sCond = sCond + " USR_ID = @USR_ID AND ";
            DbParameter dbparUSR_ID = dbConn.CreateParameter("@USR_ID", Usuario.USR_ID);
            dbParameterList.Add(dbparUSR_ID);
        }

        if (!string.IsNullOrEmpty(Usuario.USR_NOM) && !string.IsNullOrEmpty(Usuario.USR_EMA))
        {
            sCond = sCond + " (USR_NOM like @USR_NOM OR ";
            DbParameter dbparUSR_NOM = dbConn.CreateParameter("@USR_NOM", '%' + Usuario.USR_NOM + '%');
            dbParameterList.Add(dbparUSR_NOM);

            sCond = sCond + " USR_EMA like @USR_EMA) AND ";
            DbParameter dbparUSR_EMA = dbConn.CreateParameter("@USR_EMA", '%' + Usuario.USR_EMA + '%');
            dbParameterList.Add(dbparUSR_EMA);
        }
        else
        {
            if (!string.IsNullOrEmpty(Usuario.USR_NOM))
            {
                sCond = sCond + " USR_NOM like @USR_NOM AND ";
                DbParameter dbparUSR_NOM = dbConn.CreateParameter("@USR_NOM", '%' + Usuario.USR_NOM + '%');
                dbParameterList.Add(dbparUSR_NOM);
            }
            if (!string.IsNullOrEmpty(Usuario.USR_EMA))
            {
                sCond = sCond + " USR_EMA like @USR_EMA AND ";
                DbParameter dbparUSR_EMA = dbConn.CreateParameter("@USR_EMA", '%' + Usuario.USR_EMA + '%');
                dbParameterList.Add(dbparUSR_EMA);
            }
        }
        if (!string.IsNullOrEmpty(Usuario.USR_SNH))
        {
            sCond = sCond + " USR_SNH = @USR_SNH AND ";
            DbParameter dbparUSR_SNH = dbConn.CreateParameter("@USR_SNH", Usuario.USR_SNH);
            dbParameterList.Add(dbparUSR_SNH);
        }

        if (!string.IsNullOrEmpty(Usuario.USR_EML_VLD.ToString()))
        {
            sCond = sCond + " USR_EML_VLD = @USR_EML_VLD AND ";
            DbParameter dbparUSR_EML_VLD = dbConn.CreateParameter("@USR_EML_VLD", Usuario.USR_EML_VLD);
            dbParameterList.Add(dbparUSR_EML_VLD);
        }

        if (sCond != "")
        {
            sCond = sCond.Substring(1, sCond.Length - 5);
            sSQL = sSQL + " WHERE " + sCond;
        }

        SQL = dbConn.RetParam(dbParameterList);
        SQL += sSQL;

        return dbConn.ExecutarRetorno(sSQL, dbParameterList);
    }

    public DataSet ListarProfile(USR Usuario)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();
        string sCond = "";
        string sSQL = "";

        sSQL = "SELECT * ";
        sSQL = sSQL + " FROM USR ";
        sSQL = sSQL + " LEFT JOIN ARE_ATU ON ARE_ATU_ID = USR_ARE_ATU";
        sSQL = sSQL + " LEFT JOIN NVL_CNH ON NVL_CNH_ID = USR_NVL_CNH";

        if (!string.IsNullOrEmpty(Usuario.USR_ID.ToString()))
        {
            sCond = sCond + " USR_ID = @USR_ID AND ";
            DbParameter dbparUSR_ID = dbConn.CreateParameter("@USR_ID", Usuario.USR_ID);
            dbParameterList.Add(dbparUSR_ID);
        }
        if (!string.IsNullOrEmpty(Usuario.USR_NOM))
        {
            sCond = sCond + " USR_NOM like @USR_NOM AND ";
            DbParameter dbparUSR_NOM = dbConn.CreateParameter("@USR_NOM", '%' + Usuario.USR_NOM + '%');
            dbParameterList.Add(dbparUSR_NOM);
        }
        if (!string.IsNullOrEmpty(Usuario.USR_SNH))
        {
            sCond = sCond + " USR_SNH = @USR_SNH AND ";
            DbParameter dbparUSR_SNH = dbConn.CreateParameter("@USR_SNH", Usuario.USR_SNH);
            dbParameterList.Add(dbparUSR_SNH);
        }
        if (!string.IsNullOrEmpty(Usuario.USR_EMA))
        {
            sCond = sCond + " USR_EMA = @USR_EMA AND ";
            DbParameter dbparUSR_EMA = dbConn.CreateParameter("@USR_EMA", Usuario.USR_EMA);
            dbParameterList.Add(dbparUSR_EMA);
        }
        if (!string.IsNullOrEmpty(Usuario.USR_EML_VLD.ToString()))
        {
            sCond = sCond + " USR_EML_VLD = @USR_EML_VLD AND ";
            DbParameter dbparUSR_EML_VLD = dbConn.CreateParameter("@USR_EML_VLD", Usuario.USR_EML_VLD);
            dbParameterList.Add(dbparUSR_EML_VLD);
        }

        if (sCond != "")
        {
            sCond = sCond.Substring(1, sCond.Length - 5);
            sSQL = sSQL + " WHERE " + sCond;
        }

        SQL = dbConn.RetParam(dbParameterList);
        SQL += sSQL;

        return dbConn.ExecutarRetorno(sSQL, dbParameterList);
    }

    public string Salvar(USR Usuario)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();
        string sSQL = "";

        DbParameter dbparUSR_ID = dbConn.CreateParameter("@USR_ID", Usuario.USR_ID);
        dbParameterList.Add(dbparUSR_ID);
        DbParameter dbparUSR_NOM = dbConn.CreateParameter("@USR_NOM", Usuario.USR_NOM);
        dbParameterList.Add(dbparUSR_NOM);
        DbParameter dbparUSR_DAT_NASC = dbConn.CreateParameter("@USR_DAT_NASC", Usuario.USR_DAT_NASC);
        dbParameterList.Add(dbparUSR_DAT_NASC);
        DbParameter dbparUSR_SEX = dbConn.CreateParameter("@USR_SEX", Usuario.USR_SEX);
        dbParameterList.Add(dbparUSR_SEX);
        DbParameter dbparUSR_EMA = dbConn.CreateParameter("@USR_EMA", Usuario.USR_EMA);
        dbParameterList.Add(dbparUSR_EMA);
        DbParameter dbparUSR_SNH = dbConn.CreateParameter("@USR_SNH", Usuario.USR_SNH);
        dbParameterList.Add(dbparUSR_SNH);
        DbParameter dbparUSR_DAT_INC = dbConn.CreateParameter("@USR_DAT_INC", Usuario.USR_DAT_INC);
        dbParameterList.Add(dbparUSR_DAT_INC);
        DbParameter dbparUSR_DAT_EXL = dbConn.CreateParameter("@USR_DAT_EXL", Usuario.USR_DAT_EXL);
        dbParameterList.Add(dbparUSR_DAT_EXL);

        DbParameter dbparUSR_ARE_ATU = dbConn.CreateParameter("@USR_ARE_ATU", Usuario.USR_ARE_ATU);
        dbParameterList.Add(dbparUSR_ARE_ATU);
        DbParameter dbparUSR_NVL_CNH = dbConn.CreateParameter("@USR_NVL_CNH", Usuario.USR_NVL_CNH);
        dbParameterList.Add(dbparUSR_NVL_CNH);
        DbParameter dbparUSR_DAT_ADM = dbConn.CreateParameter("@USR_DAT_ADM", Usuario.USR_DAT_ADM);
        dbParameterList.Add(dbparUSR_DAT_ADM);
        DbParameter dbparUSR_CID_TRA = dbConn.CreateParameter("@USR_CID_TRA", Usuario.USR_CID_TRA);
        dbParameterList.Add(dbparUSR_CID_TRA);
        DbParameter dbparUSR_STA_TRA = dbConn.CreateParameter("@USR_STA_TRA", Usuario.USR_STA_TRA);
        dbParameterList.Add(dbparUSR_STA_TRA);
        DbParameter dbparUSR_SAL = dbConn.CreateParameter("@USR_SAL", Usuario.USR_SAL);
        dbParameterList.Add(dbparUSR_SAL);
        DbParameter dbparUSR_LGA_PPG = dbConn.CreateParameter("@USR_LGA_PPG", Usuario.USR_LGA_PPG);
        dbParameterList.Add(dbparUSR_LGA_PPG);

        DbParameter dbparUSR_ASS = dbConn.CreateParameter("@USR_ASS", Usuario.USR_ASS);
        dbParameterList.Add(dbparUSR_ASS);
        
        if (string.IsNullOrEmpty(Usuario.USR_ID.ToString()))
        {
            //INSERT
            sSQL = "insert into USR (";
            sSQL = sSQL + "USR_NOM, ";
            sSQL = sSQL + "USR_DAT_NASC, ";
            sSQL = sSQL + "USR_SEX, ";
            sSQL = sSQL + "USR_EMA, ";
            sSQL = sSQL + "USR_SNH, ";
            sSQL = sSQL + "USR_LGA_PPG, ";
            sSQL = sSQL + "USR_ASS, ";
            sSQL = sSQL + "USR_DAT_INC) ";
            sSQL = sSQL + "VALUES (";
            sSQL = sSQL + "@USR_NOM, ";
            sSQL = sSQL + "@USR_DAT_NASC, ";
            sSQL = sSQL + "@USR_SEX, ";
            sSQL = sSQL + "@USR_EMA, ";
            sSQL = sSQL + "@USR_SNH, ";
            sSQL = sSQL + "@USR_LGA_PPG, ";
            sSQL = sSQL + "@USR_ASS, ";
            sSQL = sSQL + "@USR_DAT_INC)";

            SQL = dbConn.RetParam(dbParameterList);
            SQL += sSQL;

            return dbConn.ExecutarRetornarIndice(sSQL, dbParameterList, "", true);
        }
        else
        {
            //UPDATE
            sSQL = "UPDATE USR ";
            sSQL = sSQL + "SET ";
            sSQL = sSQL + "USR_NOM = @USR_NOM, ";
            sSQL = sSQL + "USR_DAT_NASC = @USR_DAT_NASC, ";
            sSQL = sSQL + "USR_SEX = @USR_SEX, ";
            sSQL = sSQL + "USR_EMA = @USR_EMA, ";
            sSQL = sSQL + "USR_ASS = @USR_ASS, ";
            sSQL = sSQL + "USR_ARE_ATU = @USR_ARE_ATU, ";
            sSQL = sSQL + "USR_NVL_CNH = @USR_NVL_CNH, ";
            sSQL = sSQL + "USR_DAT_ADM = @USR_DAT_ADM, ";
            sSQL = sSQL + "USR_CID_TRA = @USR_CID_TRA, ";
            sSQL = sSQL + "USR_STA_TRA = @USR_STA_TRA, ";
            sSQL = sSQL + "USR_LGA_PPG = @USR_LGA_PPG, ";
            sSQL = sSQL + "USR_SAL = @USR_SAL, ";

            if (!string.IsNullOrEmpty(Usuario.USR_SNH))
            {
                sSQL = sSQL + "USR_SNH = @USR_SNH, ";
            }

            sSQL = sSQL + "USR_DAT_ALT = getdate() ";
            sSQL = sSQL + " WHERE USR_ID =  @USR_ID";

            SQL = dbConn.RetParam(dbParameterList);
            SQL += sSQL;

            return dbConn.Executar(sSQL, dbParameterList);
        }
    }

    public string SalvarFoto(USR Usuario)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();

        string sSQL = "UPDATE USR SET USR_FOT = @USR_FOT, USR_DAT_ALT = getdate() ";
        sSQL = sSQL + " WHERE USR_ID = @USR_ID ";

        DbParameter dbparUSR_ID = dbConn.CreateParameter("@USR_ID", Usuario.USR_ID);
        dbParameterList.Add(dbparUSR_ID);

        DbParameter dbparUSR_FOT = dbConn.CreateParameter("@USR_FOT", Usuario.USR_FOT);
        dbParameterList.Add(dbparUSR_FOT);

        SQL = dbConn.RetParam(dbParameterList);
        SQL += sSQL;

        return dbConn.Executar(sSQL, dbParameterList);
    }

    public string AlterarSenha(USR Usuario)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();

        string sSQL = "UPDATE USR SET USR_SNH = @USR_SNH, USR_DAT_ALT = getdate() ";
        sSQL = sSQL + " WHERE USR_ID = @USR_ID ";

        DbParameter dbparUSR_ID = dbConn.CreateParameter("@USR_ID", Usuario.USR_ID);
        dbParameterList.Add(dbparUSR_ID);

        DbParameter dbparUSR_SNH = dbConn.CreateParameter("@USR_SNH", Usuario.USR_SNH);
        dbParameterList.Add(dbparUSR_SNH);

        SQL = dbConn.RetParam(dbParameterList);
        SQL += sSQL;

        return dbConn.Executar(sSQL, dbParameterList);
    }

    public string ValidarUsuario(USR Usuario)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();

        string sSQL = "UPDATE USR SET USR_EML_VLD = @USR_EML_VLD, USR_DAT_ALT = getdate() ";
        sSQL = sSQL + " WHERE USR_ID = @USR_ID ";

        DbParameter dbparUSR_ID = dbConn.CreateParameter("@USR_ID", Usuario.USR_ID);
        dbParameterList.Add(dbparUSR_ID);

        DbParameter dbparUSR_EML_VLD = dbConn.CreateParameter("@USR_EML_VLD", Usuario.USR_EML_VLD);
        dbParameterList.Add(dbparUSR_EML_VLD);

        SQL = dbConn.RetParam(dbParameterList);
        SQL += sSQL;

        return dbConn.Executar(sSQL, dbParameterList);
    }

    public string Excluir(USR Usuario)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();

        string sSQL = "DELETE FROM USR";
        sSQL = sSQL + " WHERE USR_ID = @USR_ID ";

        DbParameter dbparUSR_ID = dbConn.CreateParameter("@USR_ID", Usuario.USR_ID);
        dbParameterList.Add(dbparUSR_ID);

        SQL = dbConn.RetParam(dbParameterList);
        SQL += sSQL;

        return dbConn.Executar(sSQL, dbParameterList);
    }

}
