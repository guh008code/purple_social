using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

public class CMD_DAO
{
    Banco dbConn = new Banco();
    private string SQL { get; set; }

    public DataSet ListarComunidadesRandomicamente(CMD Comunidade)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();
        string sCond = "";
        string sSQL = "";

        sSQL = "SELECT TOP 5 ARE_ATU_ID, CMD_ID, CMD_TIT  ";
        sSQL = sSQL + "FROM CMD ";

        if (!string.IsNullOrEmpty(Comunidade.CMD_ID.ToString()))
        {
            sCond = sCond + " CMD_ID = @CMD_ID AND ";
            DbParameter dbparCMD_ID = dbConn.CreateParameter("@CMD_ID", Comunidade.CMD_ID);
            dbParameterList.Add(dbparCMD_ID);
        }
        if (!string.IsNullOrEmpty(Comunidade.CMD_USR_ID.ToString()))
        {
            sCond = sCond + " CMD_USR_ID = @CMD_USR_ID AND ";
            DbParameter dbparCMD_USR_ID = dbConn.CreateParameter("@CMD_USR_ID", Comunidade.CMD_USR_ID);
            dbParameterList.Add(dbparCMD_USR_ID);
        }
        if (!string.IsNullOrEmpty(Comunidade.CMD_TIT))
        {
            sCond = sCond + " CMD_TIT like @CMD_TIT AND ";
            DbParameter dbparCMD_TIT = dbConn.CreateParameter("@CMD_TIT", '%' + Comunidade.CMD_TIT + '%');
            dbParameterList.Add(dbparCMD_TIT);
        }
        if (!string.IsNullOrEmpty(Comunidade.ARE_ATU_ID.ToString()))
        {
            sCond = sCond + " ARE_ATU_ID = @ARE_ATU_ID AND ";
            DbParameter dbparARE_ATU_ID = dbConn.CreateParameter("@ARE_ATU_ID", Comunidade.ARE_ATU_ID);
            dbParameterList.Add(dbparARE_ATU_ID);
        }

        if (sCond != "")
        {
            sCond = sCond.Substring(1, sCond.Length - 5);
            sSQL = sSQL + " WHERE " + sCond;
        }

        sSQL = sSQL + " ORDER BY NEWID() ";

        SQL = dbConn.RetParam(dbParameterList);
        SQL += sSQL;

        return dbConn.ExecutarRetorno(sSQL, dbParameterList);
    }

    public DataSet Listar(CMD Comunidade)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();
        string sCond = "";
        string sSQL = "";

        sSQL = "SELECT * ";
        sSQL = sSQL + "FROM CMD ";

        if (!string.IsNullOrEmpty(Comunidade.CMD_ID.ToString()))
        {
            sCond = sCond + " CMD_ID = @CMD_ID AND ";
            DbParameter dbparCMD_ID = dbConn.CreateParameter("@CMD_ID", Comunidade.CMD_ID);
            dbParameterList.Add(dbparCMD_ID);
        }
        if (!string.IsNullOrEmpty(Comunidade.CMD_USR_ID.ToString()))
        {
            sCond = sCond + " CMD_USR_ID = @CMD_USR_ID AND ";
            DbParameter dbparCMD_USR_ID = dbConn.CreateParameter("@CMD_USR_ID", Comunidade.CMD_USR_ID);
            dbParameterList.Add(dbparCMD_USR_ID);
        }
        if (!string.IsNullOrEmpty(Comunidade.CMD_TIT))
        {
            sCond = sCond + " CMD_TIT like @CMD_TIT AND ";
            DbParameter dbparCMD_TIT = dbConn.CreateParameter("@CMD_TIT", '%' + Comunidade.CMD_TIT + '%');
            dbParameterList.Add(dbparCMD_TIT);
        }
        if (!string.IsNullOrEmpty(Comunidade.ARE_ATU_ID.ToString()))
        {
            sCond = sCond + " ARE_ATU_ID = @ARE_ATU_ID AND ";
            DbParameter dbparARE_ATU_ID = dbConn.CreateParameter("@ARE_ATU_ID", Comunidade.ARE_ATU_ID);
            dbParameterList.Add(dbparARE_ATU_ID);
        }

        if (sCond != "")
        {
            sCond = sCond.Substring(1, sCond.Length - 5);
            sSQL = sSQL + " WHERE " + sCond;
        }

        sSQL = sSQL + " ORDER BY CMD_DAT_INC DESC ";

        SQL = dbConn.RetParam(dbParameterList);
        SQL += sSQL;

        return dbConn.ExecutarRetorno(sSQL, dbParameterList);
    }

    public DataSet ListarComunidades(CMD Comunidade)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();
        string sCond = "";
        string sSQL = "";

        sSQL = "SELECT count(*) as total ";
        sSQL = sSQL + "FROM CMD ";

        if (!string.IsNullOrEmpty(Comunidade.CMD_ID.ToString()))
        {
            sCond = sCond + " CMD_ID = @CMD_ID AND ";
            DbParameter dbparCMD_ID = dbConn.CreateParameter("@CMD_ID", Comunidade.CMD_ID);
            dbParameterList.Add(dbparCMD_ID);
        }
        if (!string.IsNullOrEmpty(Comunidade.CMD_USR_ID.ToString()))
        {
            sCond = sCond + " CMD_USR_ID = @CMD_USR_ID AND ";
            DbParameter dbparCMD_USR_ID = dbConn.CreateParameter("@CMD_USR_ID", Comunidade.CMD_USR_ID);
            dbParameterList.Add(dbparCMD_USR_ID);
        }
        if (!string.IsNullOrEmpty(Comunidade.CMD_TIT))
        {
            sCond = sCond + " CMD_TIT = @CMD_TIT AND ";
            DbParameter dbparCMD_TIT = dbConn.CreateParameter("@CMD_TIT", Comunidade.CMD_TIT);
            dbParameterList.Add(dbparCMD_TIT);
        }
        if (!string.IsNullOrEmpty(Comunidade.ARE_ATU_ID.ToString()))
        {
            sCond = sCond + " ARE_ATU_ID = @ARE_ATU_ID AND ";
            DbParameter dbparARE_ATU_ID = dbConn.CreateParameter("@ARE_ATU_ID", Comunidade.ARE_ATU_ID);
            dbParameterList.Add(dbparARE_ATU_ID);
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

    public string Inserir(CMD Comunidade)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();
        string sSQL = "";

        DbParameter dbparCMD_ID = dbConn.CreateParameter("@CMD_ID", Comunidade.CMD_ID);
        dbParameterList.Add(dbparCMD_ID);

        DbParameter dbparCMD_USR_ID = dbConn.CreateParameter("@CMD_USR_ID", Comunidade.CMD_USR_ID);
        dbParameterList.Add(dbparCMD_USR_ID);

        DbParameter dbparCMD_TIT = dbConn.CreateParameter("@CMD_TIT", Comunidade.CMD_TIT);
        dbParameterList.Add(dbparCMD_TIT);

        DbParameter dbparCMD_DES = dbConn.CreateParameter("@CMD_DES", Comunidade.CMD_DES);
        dbParameterList.Add(dbparCMD_DES);

        DbParameter dbparCMD_DAT_INC = dbConn.CreateParameter("@CMD_DAT_INC", Comunidade.CMD_DAT_INC);
        dbParameterList.Add(dbparCMD_DAT_INC);

        DbParameter dbparCMD_DAT_ALT = dbConn.CreateParameter("@CMD_DAT_ALT", Comunidade.CMD_DAT_ALT);
        dbParameterList.Add(dbparCMD_DAT_ALT);

        DbParameter dbparARE_ATU_ID = dbConn.CreateParameter("@ARE_ATU_ID", Comunidade.ARE_ATU_ID);
        dbParameterList.Add(dbparARE_ATU_ID);


        //INSERT
        sSQL = "insert into CMD (";
        sSQL = sSQL + "CMD_USR_ID, ";
        sSQL = sSQL + "CMD_TIT, ";
        sSQL = sSQL + "CMD_DES, ";
        sSQL = sSQL + "CMD_DAT_INC, ";
        sSQL = sSQL + "CMD_DAT_ALT, ";
        sSQL = sSQL + "ARE_ATU_ID) ";
        sSQL = sSQL + "VALUES (";
        sSQL = sSQL + " @CMD_USR_ID, ";
        sSQL = sSQL + " @CMD_TIT, ";
        sSQL = sSQL + " @CMD_DES, ";
        sSQL = sSQL + " @CMD_DAT_INC, ";
        sSQL = sSQL + " @CMD_DAT_ALT, ";
        sSQL = sSQL + "@ARE_ATU_ID) ";

        SQL = dbConn.RetParam(dbParameterList);
        SQL += sSQL;

        return dbConn.ExecutarRetornarIndice(sSQL, dbParameterList, "", true);
    }

    public string Salvar(CMD Comunidade)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();
        string sSQL = "";

        DbParameter dbparCMD_ID = dbConn.CreateParameter("@CMD_ID", Comunidade.CMD_ID);
        dbParameterList.Add(dbparCMD_ID);

        DbParameter dbparCMD_USR_ID = dbConn.CreateParameter("@CMD_USR_ID", Comunidade.CMD_USR_ID);
        dbParameterList.Add(dbparCMD_USR_ID);

        DbParameter dbparCMD_TIT = dbConn.CreateParameter("@CMD_TIT", Comunidade.CMD_TIT);
        dbParameterList.Add(dbparCMD_TIT);

        DbParameter dbparCMD_DES = dbConn.CreateParameter("@CMD_DES", Comunidade.CMD_DES);
        dbParameterList.Add(dbparCMD_DES);

        DbParameter dbparCMD_DAT_INC = dbConn.CreateParameter("@CMD_DAT_INC", Comunidade.CMD_DAT_INC);
        dbParameterList.Add(dbparCMD_DAT_INC);

        DbParameter dbparCMD_DAT_ALT = dbConn.CreateParameter("@CMD_DAT_ALT", Comunidade.CMD_DAT_ALT);
        dbParameterList.Add(dbparCMD_DAT_ALT);

        DbParameter dbparARE_ATU_ID = dbConn.CreateParameter("@ARE_ATU_ID", Comunidade.ARE_ATU_ID);
        dbParameterList.Add(dbparARE_ATU_ID);

            //UPDATE
            sSQL = "UPDATE CMD ";
            sSQL = sSQL + "SET ";
            sSQL = sSQL + "CMD_TIT = @CMD_TIT, ";
            sSQL = sSQL + "CMD_DES = @CMD_DES, ";
            sSQL = sSQL + "CMD_DAT_ALT = getdate() ";

            sSQL = sSQL + " WHERE CMD_ID =  @CMD_ID and CMD_USR_ID = @CMD_USR_ID";

            SQL = dbConn.RetParam(dbParameterList);
            SQL += sSQL;

            return dbConn.Executar(sSQL, dbParameterList);
        
    }

    public string Excluir(CMD Comunidade)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();

        string sSQL = "DELETE FROM CMD_PTP";
        sSQL = sSQL + " WHERE CMD_PTP_CMD_ID =  @CMD_ID ";

        sSQL =  sSQL = " DELETE FROM CMD";
        sSQL = sSQL + " WHERE CMD_ID =  @CMD_ID ";
        sSQL = sSQL + " AND CMD_USR_ID =  @CMD_USR_ID ";

        DbParameter dbparCMD_ID = dbConn.CreateParameter("@CMD_ID", Comunidade.CMD_ID);
        dbParameterList.Add(dbparCMD_ID);

        DbParameter dbparCMD_USR_ID = dbConn.CreateParameter("@CMD_USR_ID", Comunidade.CMD_USR_ID);
        dbParameterList.Add(dbparCMD_USR_ID);

        SQL = dbConn.RetParam(dbParameterList);
        SQL += sSQL;

        return dbConn.Executar(sSQL, dbParameterList);
    }

}
