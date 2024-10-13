using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

public class RMD_DAO
{
    Banco dbConn = new Banco();
    private string SQL { get; set; }

    public DataSet Listar(RMD Usuario)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();
        string sCond = "";
        string sSQL = "";

        sSQL = "SELECT * ";
        sSQL = sSQL + "FROM RMD ";

        if (!string.IsNullOrEmpty(Usuario.RMD_ID.ToString()))
        {
            sCond = sCond + " RMD_ID = @RMD_ID AND ";
            DbParameter dbparRMD_ID = dbConn.CreateParameter("@RMD_ID", Usuario.RMD_ID);
            dbParameterList.Add(dbparRMD_ID);
        }
        if (!string.IsNullOrEmpty(Usuario.RMD_USR_ID.ToString()))
        {
            sCond = sCond + " RMD_USR_ID = @RMD_USR_ID AND ";
            DbParameter dbparRMD_USR_ID = dbConn.CreateParameter("@RMD_USR_ID", Usuario.RMD_USR_ID);
            dbParameterList.Add(dbparRMD_USR_ID);
        }
        if (!string.IsNullOrEmpty(Usuario.RMD_USR_USR_ID.ToString()))
        {
            sCond = sCond + " RMD_USR_USR_ID = @RMD_USR_USR_ID AND ";
            DbParameter dbparRMD_USR_USR_ID = dbConn.CreateParameter("@RMD_USR_USR_ID", Usuario.RMD_USR_USR_ID);
            dbParameterList.Add(dbparRMD_USR_USR_ID);
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

    public string Inserir(RMD Usuario)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();
        string sSQL = "";

        DbParameter dbparRMD_ID = dbConn.CreateParameter("@RMD_ID", Usuario.RMD_ID);
        dbParameterList.Add(dbparRMD_ID);

        DbParameter dbparRMD_USR_ID = dbConn.CreateParameter("@RMD_USR_ID", Usuario.RMD_USR_ID);
        dbParameterList.Add(dbparRMD_USR_ID);

        DbParameter dbparRMD_USR_USR_ID = dbConn.CreateParameter("@RMD_USR_USR_ID", Usuario.RMD_USR_USR_ID);
        dbParameterList.Add(dbparRMD_USR_USR_ID);


        DbParameter dbparRMD_DAT_INC = dbConn.CreateParameter("@RMD_DAT_INC", Usuario.RMD_DAT_INC);
        dbParameterList.Add(dbparRMD_DAT_INC);

        //INSERT
        sSQL = "insert into RMD (";
        sSQL = sSQL + "RMD_USR_ID, ";
        sSQL = sSQL + "RMD_USR_USR_ID, ";
        sSQL = sSQL + "RMD_DAT_INC) ";
        sSQL = sSQL + "VALUES (";
        sSQL = sSQL + "@RMD_USR_ID, ";
        sSQL = sSQL + "@RMD_USR_USR_ID, ";
        sSQL = sSQL + "@RMD_DAT_INC) ";

        SQL = dbConn.RetParam(dbParameterList);
        SQL += sSQL;

        return dbConn.ExecutarRetornarIndice(sSQL, dbParameterList, "", true);
    }

    public string Excluir(RMD Usuario)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();

        string sSQL = "DELETE FROM RMD";
        sSQL = sSQL + " WHERE RMD_USR_ID =  @RMD_USR_ID ";
        sSQL = sSQL + " AND RMD_USR_USR_ID =  @RMD_USR_USR_ID ";

        DbParameter dbparRMD_USR_ID = dbConn.CreateParameter("@RMD_USR_ID", Usuario.RMD_USR_ID);
        dbParameterList.Add(dbparRMD_USR_ID);

        DbParameter dbparRMD_USR_USR_ID = dbConn.CreateParameter("@RMD_USR_USR_ID", Usuario.RMD_USR_USR_ID);
        dbParameterList.Add(dbparRMD_USR_USR_ID);

        SQL = dbConn.RetParam(dbParameterList);
        SQL += sSQL;

        return dbConn.Executar(sSQL, dbParameterList);
    }

}
