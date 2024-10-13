using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

public class ARE_ATU_DAO
{
    Banco dbConn = new Banco();
    private string SQL { get; set; }

    public DataSet Listar(ARE_ATU Area)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();
        string sCond = "";
        string sSQL = "";

        sSQL = "SELECT * ";
        sSQL = sSQL + "FROM ARE_ATU ";

        if (!string.IsNullOrEmpty(Area.ARE_ATU_ID.ToString()))
        {
            sCond = sCond + " ARE_ATU_ID = @ARE_ATU_ID AND ";
            DbParameter dbparARE_ATU_ID = dbConn.CreateParameter("@ARE_ATU_ID", Area.ARE_ATU_ID);
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

    public string Inserir(ARE_ATU Area)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();
        string sSQL = "";

        DbParameter dbparARE_ATU_ID = dbConn.CreateParameter("@ARE_ATU_ID", Area.ARE_ATU_ID);
        dbParameterList.Add(dbparARE_ATU_ID);

        DbParameter dbparARE_ATU_DES = dbConn.CreateParameter("@ARE_ATU_DES", Area.ARE_ATU_DES);
        dbParameterList.Add(dbparARE_ATU_DES);


        //INSERT
        sSQL = "insert into ARE_ATU (";
        sSQL = sSQL + "ARE_ATU_ID, ";
        sSQL = sSQL + "ARE_ATU_DES) ";
        sSQL = sSQL + "VALUES (";
        sSQL = sSQL + "@ARE_ATU_ID, ";
        sSQL = sSQL + "@ARE_ATU_DES) ";

        SQL = dbConn.RetParam(dbParameterList);
        SQL += sSQL;

        return dbConn.ExecutarRetornarIndice(sSQL, dbParameterList, "", true);
    }


    public string Excluir(ARE_ATU Area)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();

        string sSQL = "DELETE FROM ARE_ATU";
        sSQL = sSQL + " WHERE ARE_ATU_ID =  @ARE_ATU_ID ";

        DbParameter dbparARE_ATU_ID = dbConn.CreateParameter("@ARE_ATU_ID", Area.ARE_ATU_ID);
        dbParameterList.Add(dbparARE_ATU_ID);

        SQL = dbConn.RetParam(dbParameterList);
        SQL += sSQL;

        return dbConn.Executar(sSQL, dbParameterList);
    }

}
