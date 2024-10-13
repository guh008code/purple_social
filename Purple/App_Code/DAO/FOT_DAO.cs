using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

public class FOT_DAO
{
    Banco dbConn = new Banco();
    private string SQL { get; set; }

    public DataSet Listar(FOT Usuario)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();
        string sCond = "";
        string sSQL = "";

        sSQL = "SELECT * ";
        sSQL = sSQL + "FROM FOT ";

        if (!string.IsNullOrEmpty(Usuario.FOT_ID.ToString()))
        {
            sCond = sCond + " FOT_ID = @FOT_ID AND ";
            DbParameter dbparFOT_ID = dbConn.CreateParameter("@FOT_ID", Usuario.FOT_ID);
            dbParameterList.Add(dbparFOT_ID);
        }

        if (!string.IsNullOrEmpty(Usuario.FOT_USR_ID.ToString()))
        {
            sCond = sCond + " FOT_USR_ID = @FOT_USR_ID AND ";
            DbParameter dbparFOT_USR_ID = dbConn.CreateParameter("@FOT_USR_ID", Usuario.FOT_USR_ID);
            dbParameterList.Add(dbparFOT_USR_ID);
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

    public string Salvar(FOT Usuario)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();
        string sSQL = "";

        DbParameter dbparFOT_ID = dbConn.CreateParameter("@FOT_ID", Usuario.FOT_ID);
        dbParameterList.Add(dbparFOT_ID);

        DbParameter dbparFOT_USR_ID = dbConn.CreateParameter("@FOT_USR_ID", Usuario.FOT_USR_ID);
        dbParameterList.Add(dbparFOT_USR_ID);

        DbParameter dbparFOT_PAT = dbConn.CreateParameter("@FOT_PAT", Usuario.FOT_PAT);
        dbParameterList.Add(dbparFOT_PAT);

        DbParameter dbparFOT_TIT = dbConn.CreateParameter("@FOT_TIT", Usuario.FOT_TIT);
        dbParameterList.Add(dbparFOT_TIT);

        DbParameter dbparFOT_LEG = dbConn.CreateParameter("@FOT_LEG", Usuario.FOT_LEG);
        dbParameterList.Add(dbparFOT_LEG);

        DbParameter dbparFOT_DAT_INC = dbConn.CreateParameter("@FOT_DAT_INC", Usuario.FOT_DAT_INC);
        dbParameterList.Add(dbparFOT_DAT_INC);

        if (string.IsNullOrEmpty(Usuario.FOT_ID.ToString()))
        {
            //INSERT
            sSQL = "insert into FOT (";
            sSQL = sSQL + "FOT_USR_ID, ";
            sSQL = sSQL + "FOT_PAT, ";
            sSQL = sSQL + "FOT_TIT, ";
            sSQL = sSQL + "FOT_LEG, ";
            sSQL = sSQL + "FOT_DAT_INC) ";
            sSQL = sSQL + "VALUES (";
            sSQL = sSQL + "@FOT_USR_ID, ";
            sSQL = sSQL + "@FOT_PAT, ";
            sSQL = sSQL + "@FOT_TIT, ";
            sSQL = sSQL + "@FOT_LEG, ";
            sSQL = sSQL + "@FOT_DAT_INC) ";

            SQL = dbConn.RetParam(dbParameterList);
            SQL += sSQL;

            return dbConn.ExecutarRetornarIndice(sSQL, dbParameterList, "", true);
        }
        else
        {
            //UPDATE
            sSQL = "UPDATE FOT ";
            sSQL = sSQL + "SET ";
            sSQL = sSQL + "FOT_TIT = @FOT_TIT, ";
            sSQL = sSQL + "FOT_LEG = @FOT_LEG ";

            sSQL = sSQL + " WHERE FOT_ID =  @FOT_ID and FOT_USR_ID = @FOT_USR_ID";

            SQL = dbConn.RetParam(dbParameterList);
            SQL += sSQL;

            return dbConn.Executar(sSQL, dbParameterList);
        }
    }

    public string Excluir(FOT Usuario)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();

        string sSQL = "DELETE FROM FOT";
        sSQL = sSQL + " WHERE FOT_ID = @FOT_ID AND FOT_USR_ID = @FOT_USR_ID";

        DbParameter dbparFOT_ID = dbConn.CreateParameter("@FOT_ID", Usuario.FOT_ID);
        dbParameterList.Add(dbparFOT_ID);

        DbParameter dbparFOT_USR_ID = dbConn.CreateParameter("@FOT_USR_ID", Usuario.FOT_USR_ID);
        dbParameterList.Add(dbparFOT_USR_ID);

        SQL = dbConn.RetParam(dbParameterList);
        SQL += sSQL;

        return dbConn.Executar(sSQL, dbParameterList);
    }

}
