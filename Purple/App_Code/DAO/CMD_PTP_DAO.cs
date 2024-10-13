using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

public class CMD_PTP_DAO
{
    Banco dbConn = new Banco();
    private string SQL { get; set; }

    public DataSet Listar(CMD_PTP Comunidade)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();
        string sCond = "";
        string sSQL = "";

        sSQL = "SELECT * ";
        sSQL = sSQL + "FROM CMD_PTP ";
        sSQL = sSQL + "inner join USR ON USR_ID = CMD_PTP_USR_ID ";

        if (!string.IsNullOrEmpty(Comunidade.CMD_PTP_ID.ToString()))
        {
            sCond = sCond + " CMD_PTP_ID = @CMD_PTP_ID AND ";
            DbParameter dbparCMD_PTP_ID = dbConn.CreateParameter("@CMD_PTP_ID", Comunidade.CMD_PTP_ID);
            dbParameterList.Add(dbparCMD_PTP_ID);
        }
        if (!string.IsNullOrEmpty(Comunidade.CMD_PTP_CMD_ID.ToString()))
        {
            sCond = sCond + " CMD_PTP_CMD_ID = @CMD_PTP_CMD_ID AND ";
            DbParameter dbparCMD_PTP_CMD_ID = dbConn.CreateParameter("@CMD_PTP_CMD_ID", Comunidade.CMD_PTP_CMD_ID);
            dbParameterList.Add(dbparCMD_PTP_CMD_ID);
        }
        if (!string.IsNullOrEmpty(Comunidade.CMD_PTP_USR_ID.ToString()))
        {
            sCond = sCond + " CMD_PTP_USR_ID = @CMD_PTP_USR_ID AND ";
            DbParameter dbparCMD_PTP_USR_ID = dbConn.CreateParameter("@CMD_PTP_USR_ID", Comunidade.CMD_PTP_USR_ID);
            dbParameterList.Add(dbparCMD_PTP_USR_ID);
        }


        if (sCond != "")
        {
            sCond = sCond.Substring(1, sCond.Length - 5);
            sSQL = sSQL + " WHERE " + sCond;
        }

        sSQL = sSQL + " ORDER BY CMD_PTP_DAT_INC ASC ";

        SQL = dbConn.RetParam(dbParameterList);
        SQL += sSQL;

        return dbConn.ExecutarRetorno(sSQL, dbParameterList);
    }

    public string Inserir(CMD_PTP Comunidade)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();
        string sSQL = "";

        DbParameter dbparCMD_PTP_ID = dbConn.CreateParameter("@CMD_PTP_ID", Comunidade.CMD_PTP_ID);
        dbParameterList.Add(dbparCMD_PTP_ID);

        DbParameter dbparCMD_PTP_USR_ID = dbConn.CreateParameter("@CMD_PTP_USR_ID", Comunidade.CMD_PTP_USR_ID);
        dbParameterList.Add(dbparCMD_PTP_USR_ID);

        DbParameter dbparCMD_PTP_CMD_ID = dbConn.CreateParameter("@CMD_PTP_CMD_ID", Comunidade.CMD_PTP_CMD_ID);
        dbParameterList.Add(dbparCMD_PTP_CMD_ID);

        DbParameter dbparCMD_PTP_DES = dbConn.CreateParameter("@CMD_PTP_DES", Comunidade.CMD_PTP_DES);
        dbParameterList.Add(dbparCMD_PTP_DES);

        DbParameter dbparCMD_PTP_DAT_INC = dbConn.CreateParameter("@CMD_PTP_DAT_INC", Comunidade.CMD_PTP_DAT_INC);
        dbParameterList.Add(dbparCMD_PTP_DAT_INC);

        DbParameter dbparCMD_PTP_DAT_ALT = dbConn.CreateParameter("@CMD_PTP_DAT_ALT", Comunidade.CMD_PTP_DAT_ALT);
        dbParameterList.Add(dbparCMD_PTP_DAT_ALT);


        //INSERT
        sSQL = "insert into CMD_PTP (";
        sSQL = sSQL + "CMD_PTP_USR_ID, ";
        sSQL = sSQL + "CMD_PTP_CMD_ID, ";
        sSQL = sSQL + "CMD_PTP_DES, ";
        sSQL = sSQL + "CMD_PTP_DAT_INC, ";
        sSQL = sSQL + "CMD_PTP_DAT_ALT) ";
        sSQL = sSQL + "VALUES (";
        sSQL = sSQL + " @CMD_PTP_USR_ID, ";
        sSQL = sSQL + " @CMD_PTP_CMD_ID, ";
        sSQL = sSQL + " @CMD_PTP_DES, ";
        sSQL = sSQL + " @CMD_PTP_DAT_INC, ";
        sSQL = sSQL + " @CMD_PTP_DAT_ALT) ";

        SQL = dbConn.RetParam(dbParameterList);
        SQL += sSQL;

        return dbConn.ExecutarRetornarIndice(sSQL, dbParameterList, "", true);
    }

    public string Excluir(CMD_PTP Comunidade)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();

        string sSQL = "";

        sSQL =  sSQL = " DELETE FROM CMD_PTP";
        sSQL = sSQL + " WHERE CMD_PTP_ID =  @CMD_PTP_ID ";
        sSQL = sSQL + " AND CMD_PTP_USR_ID =  @CMD_PTP_USR_ID ";

        DbParameter dbparCMD_PTP_ID = dbConn.CreateParameter("@CMD_PTP_ID", Comunidade.CMD_PTP_ID);
        dbParameterList.Add(dbparCMD_PTP_ID);

        DbParameter dbparCMD_PTP_USR_ID = dbConn.CreateParameter("@CMD_PTP_USR_ID", Comunidade.CMD_PTP_USR_ID);
        dbParameterList.Add(dbparCMD_PTP_USR_ID);

        SQL = dbConn.RetParam(dbParameterList);
        SQL += sSQL;

        return dbConn.Executar(sSQL, dbParameterList);
    }

}
