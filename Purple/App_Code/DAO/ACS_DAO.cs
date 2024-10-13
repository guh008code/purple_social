using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

public class ACS_DAO
{
    Banco dbConn = new Banco();
    private string SQL { get; set; }

    public DataSet Listar(ACS Usuario)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();
        string sCond = "";
        string sSQL = "";

        sSQL = "SELECT * ";
        sSQL = sSQL + "FROM ACS ";
        sSQL = sSQL + "LEFT JOIN USR ON USR_ID = ACS_USR_ID ";

        if (!string.IsNullOrEmpty(Usuario.ACS_ID.ToString()))
        {
            sCond = sCond + " ACS_ID = @ACS_ID AND ";
            DbParameter dbparACS_ID = dbConn.CreateParameter("@ACS_ID", Usuario.ACS_ID);
            dbParameterList.Add(dbparACS_ID);
        }
        if (!string.IsNullOrEmpty(Usuario.ACS_USR_ID.ToString()))
        {
            sCond = sCond + " ACS_USR_ID = @ACS_USR_ID AND ";
            DbParameter dbparACS_USR_ID = dbConn.CreateParameter("@ACS_USR_ID", Usuario.ACS_USR_ID);
            dbParameterList.Add(dbparACS_USR_ID);
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

    public DataSet ListarAcessos(ACS Usuario)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();
        string sCond = "";
        string sSQL = "";

        sSQL = "SELECT DISTINCT USR_ID, USR_NOM, USR_EMA, COUNT(*) AS quantidade_acesso ";
        sSQL = sSQL + "FROM ACS ";
        sSQL = sSQL + "LEFT JOIN USR ON USR_ID = ACS_USR_ID ";

        if (!string.IsNullOrEmpty(Usuario.ACS_ID.ToString()))
        {
            sCond = sCond + " ACS_ID = @ACS_ID AND ";
            DbParameter dbparACS_ID = dbConn.CreateParameter("@ACS_ID", Usuario.ACS_ID);
            dbParameterList.Add(dbparACS_ID);
        }
        if (!string.IsNullOrEmpty(Usuario.ACS_USR_ID.ToString()))
        {
            sCond = sCond + " ACS_USR_ID = @ACS_USR_ID AND ";
            DbParameter dbparACS_USR_ID = dbConn.CreateParameter("@ACS_USR_ID", Usuario.ACS_USR_ID);
            dbParameterList.Add(dbparACS_USR_ID);
        }


        if (sCond != "")
        {
            sCond = sCond.Substring(1, sCond.Length - 5);
            sSQL = sSQL + " WHERE " + sCond;
        }

        sSQL = sSQL + " GROUP BY USR_ID, USR_NOM, USR_EMA ";

        SQL = dbConn.RetParam(dbParameterList);
        SQL += sSQL;

        return dbConn.ExecutarRetorno(sSQL, dbParameterList);
    }

    public string Inserir(ACS Usuario)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();
        string sSQL = "";

        DbParameter dbparACS_ID = dbConn.CreateParameter("@ACS_ID", Usuario.ACS_ID);
        dbParameterList.Add(dbparACS_ID);

        DbParameter dbparACS_USR_ID = dbConn.CreateParameter("@ACS_USR_ID", Usuario.ACS_USR_ID);
        dbParameterList.Add(dbparACS_USR_ID);

        //INSERT
        sSQL = "insert into ACS (";
        sSQL = sSQL + "ACS_USR_ID, ";
        sSQL = sSQL + "ACS_DAT_TMS) ";
        sSQL = sSQL + "VALUES (";
        sSQL = sSQL + "@ACS_USR_ID, ";
        sSQL = sSQL + "GETDATE()) ";

        SQL = dbConn.RetParam(dbParameterList);
        SQL += sSQL;

        return dbConn.ExecutarRetornarIndice(sSQL, dbParameterList, "", true);
    }

}
