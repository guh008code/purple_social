using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

public class FED_DAO
{
    Banco dbConn = new Banco();
    private string SQL { get; set; }

    public DataSet Listar(FED Usuario)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();
        string sCond = "";
        string sSQL = "";

        sSQL = "SELECT * ";
        sSQL = sSQL + "FROM FED ";

        if (!string.IsNullOrEmpty(Usuario.FED_ID.ToString()))
        {
            sCond = sCond + " FED_ID = @FED_ID AND ";
            DbParameter dbparFED_ID = dbConn.CreateParameter("@FED_ID", Usuario.FED_ID);
            dbParameterList.Add(dbparFED_ID);
        }
        if (!string.IsNullOrEmpty(Usuario.FED_USR_ID.ToString()))
        {
            sCond = sCond + " FED_USR_ID = @FED_USR_ID AND ";
            DbParameter dbparFED_USR_ID = dbConn.CreateParameter("@FED_USR_ID", Usuario.FED_USR_ID);
            dbParameterList.Add(dbparFED_USR_ID);
        }
        if (!string.IsNullOrEmpty(Usuario.FED_ALB_ID.ToString()))
        {
            sCond = sCond + " FED_ALB_ID = @FED_ALB_ID AND ";
            DbParameter dbparFED_ALB_ID = dbConn.CreateParameter("@FED_ALB_ID", Usuario.FED_ALB_ID);
            dbParameterList.Add(dbparFED_ALB_ID);
        }
        if (!string.IsNullOrEmpty(Usuario.FED_FOT_ID.ToString()))
        {
            sCond = sCond + " FED_FOT_ID = @FED_FOT_ID AND ";
            DbParameter dbparFED_FOT_ID = dbConn.CreateParameter("@FED_FOT_ID", Usuario.FED_FOT_ID);
            dbParameterList.Add(dbparFED_FOT_ID);
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

    public DataSet ListarPost(FED Usuario)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();
        string sSQL = "";

        sSQL = "select FED_ID, fed_pot, FED_DAT_INC, usr_id, usr_nom, USR_FOT ";
        sSQL = sSQL + "from FED ";
        sSQL = sSQL + "left join AMG on AMG_USR_ID = FED_USR_ID ";
        sSQL = sSQL + "left join usr on USR_ID = AMG_USR_ID ";
        sSQL = sSQL + "where (AMG_USR_ID = @FED_USR_ID or AMG_USR_USR_ID = @FED_USR_ID) ";
        sSQL = sSQL + "and FED_ID is not null AND AMG_SLC = 1 AND AMG_OK = 1 ";
        sSQL = sSQL + "union ";

        sSQL = sSQL + "select FED_ID, fed_pot, FED_DAT_INC, usr_id, usr_nom, USR_FOT ";
        sSQL = sSQL + "from FED ";
        sSQL = sSQL + "left join AMG on AMG_USR_USR_ID = FED_USR_ID ";
        sSQL = sSQL + "left join usr on USR_ID = AMG_USR_USR_ID ";
        sSQL = sSQL + "where (AMG_USR_ID = @FED_USR_ID or AMG_USR_USR_ID = @FED_USR_ID) ";
        sSQL = sSQL + "and FED_ID is not null AND AMG_SLC = 1 AND AMG_OK = 1 ";
        sSQL = sSQL + "union ";

        sSQL = sSQL + "select FED_ID, fed_pot, FED_DAT_INC, usr_id, usr_nom, USR_FOT ";
        sSQL = sSQL + "from FED ";
        sSQL = sSQL + "left join usr on USR_ID = FED_USR_ID ";
        sSQL = sSQL + "where (FED_USR_ID = @FED_USR_ID) ";

        if (!string.IsNullOrEmpty(Usuario.FED_USR_ID.ToString()))
        {
            DbParameter dbparFED_USR_ID = dbConn.CreateParameter("@FED_USR_ID", Usuario.FED_USR_ID);
            dbParameterList.Add(dbparFED_USR_ID);
        }

        sSQL = sSQL + " group by FED_ID, fed_pot, FED_DAT_INC, usr_id, usr_nom, USR_FOT ";
        sSQL = sSQL + "order by FED_DAT_INC desc ";

        SQL = dbConn.RetParam(dbParameterList);
        SQL += sSQL;

        return dbConn.ExecutarRetorno(sSQL, dbParameterList);
    }

    public string InserirPost(FED Usuario)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();
        string sSQL = "";

        DbParameter dbparFED_USR_ID = dbConn.CreateParameter("@FED_USR_ID", Usuario.FED_USR_ID);
        dbParameterList.Add(dbparFED_USR_ID);

        DbParameter dbparFED_POT = dbConn.CreateParameter("@FED_POT", Usuario.FED_POT);
        dbParameterList.Add(dbparFED_POT);

        DbParameter dbparFED_ALB_ID = dbConn.CreateParameter("@FED_ALB_ID", Usuario.FED_ALB_ID);
        dbParameterList.Add(dbparFED_ALB_ID);

        DbParameter dbparFED_FOT_ID = dbConn.CreateParameter("@FED_FOT_ID", Usuario.FED_FOT_ID);
        dbParameterList.Add(dbparFED_FOT_ID);

        DbParameter dbparFED_DAT_INC = dbConn.CreateParameter("@FED_DAT_INC", Usuario.FED_DAT_INC);
        dbParameterList.Add(dbparFED_DAT_INC);

        //INSERT
        sSQL = "insert into FED (";
        sSQL = sSQL + "FED_USR_ID, ";
        sSQL = sSQL + "FED_POT, ";
        sSQL = sSQL + "FED_ALB_ID, ";
        sSQL = sSQL + "FED_FOT_ID, ";
        sSQL = sSQL + "FED_DAT_INC) ";
        sSQL = sSQL + "VALUES (";
        sSQL = sSQL + "@FED_USR_ID, ";
        sSQL = sSQL + "@FED_POT, ";
        sSQL = sSQL + "@FED_ALB_ID, ";
        sSQL = sSQL + "@FED_FOT_ID, ";
        sSQL = sSQL + "@FED_DAT_INC) ";

        SQL = dbConn.RetParam(dbParameterList);
        SQL += sSQL;

        return dbConn.ExecutarRetornarIndice(sSQL, dbParameterList, "", true);
    }

    public string SalvarPost(FED Usuario)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();

        string sSQL = "";
        sSQL = "UPDATE FED ";
        sSQL = sSQL + "SET ";
        sSQL = sSQL + "FED_POT = @FED_POT, ";
        sSQL = sSQL + "FED_DAT_ALT = getdate() ";
        sSQL = sSQL + " WHERE FED_ID =  @FED_ID ";

        DbParameter dbparFED_POT = dbConn.CreateParameter("@FED_POT", Usuario.FED_POT);
        dbParameterList.Add(dbparFED_POT);

        DbParameter dbparFED_ID = dbConn.CreateParameter("@FED_ID", Usuario.FED_ID);
        dbParameterList.Add(dbparFED_ID);

        SQL = dbConn.RetParam(dbParameterList);
        SQL += sSQL;

        return dbConn.Executar(sSQL, dbParameterList);
    }

    public string ExcluirPost(FED Usuario)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();

        string sSQL = "DELETE FROM FED";
        sSQL = sSQL + " WHERE FED_ID =  @FED_ID ";

        DbParameter dbparFED_ID = dbConn.CreateParameter("@FED_ID", Usuario.FED_ID);
        dbParameterList.Add(dbparFED_ID);

        SQL = dbConn.RetParam(dbParameterList);
        SQL += sSQL;

        return dbConn.Executar(sSQL, dbParameterList);
    }

}
