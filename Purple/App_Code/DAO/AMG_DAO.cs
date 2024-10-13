using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

public class AMG_DAO
{
    Banco dbConn = new Banco();
    private string SQL { get; set; }

    public DataSet Listar(AMG Usuario)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();
        string sCond = "";
        string sSQL = "";

        sSQL = "SELECT * ";
        sSQL = sSQL + "FROM AMG ";

        if (!string.IsNullOrEmpty(Usuario.AMG_ID.ToString()))
        {
            sCond = sCond + " AMG_ID = @AMG_ID AND ";
            DbParameter dbparAMG_ID = dbConn.CreateParameter("@AMG_ID", Usuario.AMG_ID);
            dbParameterList.Add(dbparAMG_ID);
        }
        if (!string.IsNullOrEmpty(Usuario.AMG_USR_ID.ToString()))
        {
            sCond = sCond + " AMG_USR_ID = @AMG_USR_ID AND ";
            DbParameter dbparAMG_USR_ID = dbConn.CreateParameter("@AMG_USR_ID", Usuario.AMG_USR_ID);
            dbParameterList.Add(dbparAMG_USR_ID);
        }
        if (!string.IsNullOrEmpty(Usuario.AMG_USR_USR_ID.ToString()))
        {
            sCond = sCond + " AMG_USR_USR_ID = @AMG_USR_USR_ID AND ";
            DbParameter dbparAMG_USR_USR_ID = dbConn.CreateParameter("@AMG_USR_USR_ID", Usuario.AMG_USR_USR_ID);
            dbParameterList.Add(dbparAMG_USR_USR_ID);
        }
        if (!string.IsNullOrEmpty(Usuario.AMG_SLC.ToString()))
        {
            sCond = sCond + " AMG_SLC = @AMG_SLC AND ";
            DbParameter dbparAMG_SLC = dbConn.CreateParameter("@AMG_SLC", Usuario.AMG_SLC);
            dbParameterList.Add(dbparAMG_SLC);
        }
        if (!string.IsNullOrEmpty(Usuario.AMG_OK.ToString()))
        {
            sCond = sCond + " AMG_OK = @AMG_OK AND ";
            DbParameter dbparAMG_OK = dbConn.CreateParameter("@AMG_OK", Usuario.AMG_OK);
            dbParameterList.Add(dbparAMG_OK);
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

    public DataSet ListarAmigos(AMG Usuario)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();
        string sSQL = "";

        sSQL = "SELECT USR_ID, USR_FOT, USR_NOM ";
        sSQL = sSQL + "FROM AMG ";
        sSQL = sSQL + "inner join usr on usr_id = AMG_USR_ID ";
        sSQL = sSQL + "where (AMG_USR_ID = @AMG_USR_ID OR AMG_USR_USR_ID = @AMG_USR_USR_ID) ";

        if (!string.IsNullOrEmpty(Usuario.AMG_SLC.ToString()))
        {
            sSQL = sSQL + "AND AMG_SLC = @AMG_SLC ";
            DbParameter dbparAMG_SLC = dbConn.CreateParameter("@AMG_SLC", Usuario.AMG_SLC);
            dbParameterList.Add(dbparAMG_SLC);
        }

        if (!string.IsNullOrEmpty(Usuario.AMG_OK.ToString()))
        {
            sSQL = sSQL + "AND AMG_OK = @AMG_OK ";
            DbParameter dbparAMG_OK = dbConn.CreateParameter("@AMG_OK", Usuario.AMG_OK);
            dbParameterList.Add(dbparAMG_OK);
        }

        sSQL = sSQL + " UNION ";
        sSQL = sSQL + "SELECT USR_ID, USR_FOT, USR_NOM ";
        sSQL = sSQL + "FROM AMG ";
        sSQL = sSQL + "inner join usr on usr_id = AMG_USR_USR_ID ";
        sSQL = sSQL + "where (AMG_USR_ID = @AMG_USR_ID OR AMG_USR_USR_ID = @AMG_USR_USR_ID) ";

        if (!string.IsNullOrEmpty(Usuario.AMG_SLC.ToString()))
        {
            sSQL = sSQL + "AND AMG_SLC = @AMG_SLC  ";
        }

        if (!string.IsNullOrEmpty(Usuario.AMG_OK.ToString()))
        {
            sSQL = sSQL + "AND AMG_OK = @AMG_OK ";
        }

        if (!string.IsNullOrEmpty(Usuario.AMG_USR_ID.ToString()))
        {
            DbParameter dbparAMG_USR_ID = dbConn.CreateParameter("@AMG_USR_ID", Usuario.AMG_USR_ID);
            dbParameterList.Add(dbparAMG_USR_ID);
            DbParameter dbparAMG_USR_USR_ID = dbConn.CreateParameter("@AMG_USR_USR_ID", Usuario.AMG_USR_USR_ID);
            dbParameterList.Add(dbparAMG_USR_USR_ID);
        }

        sSQL = sSQL + " GROUP BY USR_ID, USR_FOT, USR_NOM ORDER BY USR_ID ASC ";

        SQL = dbConn.RetParam(dbParameterList);
        SQL += sSQL;

        return dbConn.ExecutarRetorno(sSQL, dbParameterList);
    }

    public DataSet ListarAmizades(AMG Usuario)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();
        string sCond = "";
        string sSQL = "";

        sSQL = "SELECT * ";
        sSQL = sSQL + "FROM AMG ";
        sSQL = sSQL + "inner join usr on usr_id = AMG_USR_ID ";

        if (!string.IsNullOrEmpty(Usuario.AMG_ID.ToString()))
        {
            sCond = sCond + " AMG_ID = @AMG_ID AND ";
            DbParameter dbparAMG_ID = dbConn.CreateParameter("@AMG_ID", Usuario.AMG_ID);
            dbParameterList.Add(dbparAMG_ID);
        }
        if (!string.IsNullOrEmpty(Usuario.AMG_USR_ID.ToString()))
        {
            sCond = sCond + " (AMG_USR_ID = @AMG_USR_ID OR AMG_USR_USR_ID = @AMG_USR_USR_ID) AND ";

            DbParameter dbparAMG_USR_ID = dbConn.CreateParameter("@AMG_USR_ID", Usuario.AMG_USR_ID);
            dbParameterList.Add(dbparAMG_USR_ID);
            DbParameter dbparAMG_USR_USR_ID = dbConn.CreateParameter("@AMG_USR_USR_ID", Usuario.AMG_USR_USR_ID);
            dbParameterList.Add(dbparAMG_USR_USR_ID);
        }
        if (!string.IsNullOrEmpty(Usuario.AMG_SLC.ToString()))
        {
            sCond = sCond + " AMG_SLC = @AMG_SLC AND ";
            DbParameter dbparAMG_SLC = dbConn.CreateParameter("@AMG_SLC", Usuario.AMG_SLC);
            dbParameterList.Add(dbparAMG_SLC);
        }
        if (!string.IsNullOrEmpty(Usuario.AMG_OK.ToString()))
        {
            sCond = sCond + " AMG_OK = @AMG_OK AND ";
            DbParameter dbparAMG_OK = dbConn.CreateParameter("@AMG_OK", Usuario.AMG_OK);
            dbParameterList.Add(dbparAMG_OK);
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

    public DataSet ListarAmizadesInversa(AMG Usuario)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();
        string sCond = "";
        string sSQL = "";

        sSQL = "SELECT * ";
        sSQL = sSQL + "FROM AMG ";
        sSQL = sSQL + "inner join usr on usr_id = AMG_USR_USR_ID ";

        if (!string.IsNullOrEmpty(Usuario.AMG_ID.ToString()))
        {
            sCond = sCond + " AMG_ID = @AMG_ID AND ";
            DbParameter dbparAMG_ID = dbConn.CreateParameter("@AMG_ID", Usuario.AMG_ID);
            dbParameterList.Add(dbparAMG_ID);
        }
        if (!string.IsNullOrEmpty(Usuario.AMG_USR_ID.ToString()))
        {
            sCond = sCond + " (AMG_USR_ID = @AMG_USR_ID OR AMG_USR_USR_ID = @AMG_USR_USR_ID) AND ";

            DbParameter dbparAMG_USR_ID = dbConn.CreateParameter("@AMG_USR_ID", Usuario.AMG_USR_ID);
            dbParameterList.Add(dbparAMG_USR_ID);
            DbParameter dbparAMG_USR_USR_ID = dbConn.CreateParameter("@AMG_USR_USR_ID", Usuario.AMG_USR_USR_ID);
            dbParameterList.Add(dbparAMG_USR_USR_ID);
        }
        if (!string.IsNullOrEmpty(Usuario.AMG_SLC.ToString()))
        {
            sCond = sCond + " AMG_SLC = @AMG_SLC AND ";
            DbParameter dbparAMG_SLC = dbConn.CreateParameter("@AMG_SLC", Usuario.AMG_SLC);
            dbParameterList.Add(dbparAMG_SLC);
        }
        if (!string.IsNullOrEmpty(Usuario.AMG_OK.ToString()))
        {
            sCond = sCond + " AMG_OK = @AMG_OK AND ";
            DbParameter dbparAMG_OK = dbConn.CreateParameter("@AMG_OK", Usuario.AMG_OK);
            dbParameterList.Add(dbparAMG_OK);
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

    public DataSet ListarRelatorio(AMG Usuario)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();
        string sSQL = "";

        sSQL = " SELECT USR_ID, USR_NOM , USR_EMA, count(*) as total   ";
        sSQL = sSQL + "FROM AMG  ";
        sSQL = sSQL + "inner join usr on usr_id = AMG_USR_ID  ";
        sSQL = sSQL + "where AMG_OK = 1 ";
        sSQL = sSQL + "GROUP BY USR_ID, USR_NOM, usr_ema ";
        sSQL = sSQL + "UNION  ";
        sSQL = sSQL + "SELECT USR_ID, USR_NOM , USR_EMA, count(*) as total  ";
        sSQL = sSQL + "FROM AMG  ";
        sSQL = sSQL + "inner join usr on usr_id = AMG_USR_USR_ID  ";
        sSQL = sSQL + "where AMG_OK = 1 ";
        sSQL = sSQL + "GROUP BY USR_ID, USR_NOM, usr_ema ";


        SQL = dbConn.RetParam(dbParameterList);
        SQL += sSQL;

        return dbConn.ExecutarRetorno(sSQL, dbParameterList);
    }

    public string Inserir(AMG Usuario)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();
        string sSQL = "";

        DbParameter dbparAMG_ID = dbConn.CreateParameter("@AMG_ID", Usuario.AMG_ID);
        dbParameterList.Add(dbparAMG_ID);

        DbParameter dbparAMG_USR_ID = dbConn.CreateParameter("@AMG_USR_ID", Usuario.AMG_USR_ID);
        dbParameterList.Add(dbparAMG_USR_ID);

        DbParameter dbparAMG_USR_USR_ID = dbConn.CreateParameter("@AMG_USR_USR_ID", Usuario.AMG_USR_USR_ID);
        dbParameterList.Add(dbparAMG_USR_USR_ID);

        DbParameter dbparAMG_SLC = dbConn.CreateParameter("@AMG_SLC", Usuario.AMG_SLC);
        dbParameterList.Add(dbparAMG_SLC);

        DbParameter dbparAMG_OK = dbConn.CreateParameter("@AMG_OK", Usuario.AMG_OK);
        dbParameterList.Add(dbparAMG_OK);

        DbParameter dbparAMG_DAT_INC = dbConn.CreateParameter("@AMG_DAT_INC", Usuario.AMG_DAT_INC);
        dbParameterList.Add(dbparAMG_DAT_INC);

        //INSERT
        sSQL = "insert into AMG (";
        sSQL = sSQL + "AMG_USR_ID, ";
        sSQL = sSQL + "AMG_USR_USR_ID, ";
        sSQL = sSQL + "AMG_SLC, ";
        sSQL = sSQL + "AMG_OK, ";
        sSQL = sSQL + "AMG_DAT_ALT, ";
        sSQL = sSQL + "AMG_DAT_INC) ";
        sSQL = sSQL + "VALUES (";
        sSQL = sSQL + "@AMG_USR_ID, ";
        sSQL = sSQL + "@AMG_USR_USR_ID, ";
        sSQL = sSQL + "@AMG_SLC, ";
        sSQL = sSQL + "@AMG_OK, ";
        sSQL = sSQL + "getdate(), ";
        sSQL = sSQL + "@AMG_DAT_INC) ";

        SQL = dbConn.RetParam(dbParameterList);
        SQL += sSQL;

        return dbConn.ExecutarRetornarIndice(sSQL, dbParameterList, "", true);
    }

    public string AdicionarOuRemoverAmizade(AMG Usuario)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();

        string sSQL = "";
        sSQL = "UPDATE AMG ";
        sSQL = sSQL + "SET ";
        sSQL = sSQL + "AMG_SLC = @AMG_SLC, ";
        sSQL = sSQL + "AMG_OK = @AMG_OK, ";
        sSQL = sSQL + "AMG_DAT_ALT = getdate() ";
        sSQL = sSQL + " WHERE AMG_USR_ID =  @AMG_USR_ID ";
        sSQL = sSQL + " AND AMG_USR_USR_ID =  @AMG_USR_USR_ID ";

        DbParameter dbparAMG_USR_ID = dbConn.CreateParameter("@AMG_USR_ID", Usuario.AMG_USR_ID);
        dbParameterList.Add(dbparAMG_USR_ID);

        DbParameter dbparAMG_USR_USR_ID = dbConn.CreateParameter("@AMG_USR_USR_ID", Usuario.AMG_USR_USR_ID);
        dbParameterList.Add(dbparAMG_USR_USR_ID);

        DbParameter dbparAMG_SLC = dbConn.CreateParameter("@AMG_SLC", Usuario.AMG_SLC);
        dbParameterList.Add(dbparAMG_SLC);

        DbParameter dbparAMG_OK = dbConn.CreateParameter("@AMG_OK", Usuario.AMG_OK);
        dbParameterList.Add(dbparAMG_OK);

        SQL = dbConn.RetParam(dbParameterList);
        SQL += sSQL;

        return dbConn.Executar(sSQL, dbParameterList);
    }

    public string Excluir(AMG Usuario)
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();

        string sSQL = "DELETE FROM AMG";
        sSQL = sSQL + " WHERE AMG_USR_ID =  @AMG_USR_ID ";
        sSQL = sSQL + " AND AMG_USR_USR_ID =  @AMG_USR_USR_ID ";

        DbParameter dbparAMG_USR_ID = dbConn.CreateParameter("@AMG_USR_ID", Usuario.AMG_USR_ID);
        dbParameterList.Add(dbparAMG_USR_ID);

        DbParameter dbparAMG_USR_USR_ID = dbConn.CreateParameter("@AMG_USR_USR_ID", Usuario.AMG_USR_USR_ID);
        dbParameterList.Add(dbparAMG_USR_USR_ID);

        SQL = dbConn.RetParam(dbParameterList);
        SQL += sSQL;

        return dbConn.Executar(sSQL, dbParameterList);
    }

}
