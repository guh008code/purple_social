using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Data.SqlClient;
using System.Threading;
using System.Globalization;


public class Banco
{
    public DbProviderFactory _dbFactory;

    private DbConnection _dbConn;
    public DbConnection dbConn
    {
        get
        {
            if (_dbConn == null)
            {
                _dbFactory = DbProviderFactories.GetFactory(ConfigurationManager.AppSettings["FWProvider"]);
                _dbConn = _dbFactory.CreateConnection();

                if (ConfigurationManager.AppSettings["FWProvider"].ToString().Contains("SqlClient") && Convert.ToBoolean(ConfigurationManager.AppSettings["LOCAL_DATABASE"].ToString()))
                {
                    SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
                    connectionStringBuilder.IntegratedSecurity = false;
                    string strConnection = ConfigurationManager.AppSettings["FWConnection"].ToString();
                    string[] acesso = strConnection.Split(';');
                    string dataSource = acesso[0].Split('=')[1].ToString();
                    string banco = acesso[1].Split('=')[1].ToString();
                    string user = acesso[2].Split('=')[1].ToString();
                    string senha = acesso[3].Split('=')[1].ToString();

                    connectionStringBuilder.UserID = user;
                    connectionStringBuilder.Password = senha;
                    connectionStringBuilder.InitialCatalog = banco;
                    connectionStringBuilder.DataSource = dataSource;
                    connectionStringBuilder.ConnectTimeout = 999999999;

                    _dbConn.ConnectionString = connectionStringBuilder.ToString();
                }
                else if (ConfigurationManager.AppSettings["FWProvider"].ToString().Contains("SqlClient"))
                {
                    SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
                    connectionStringBuilder.IntegratedSecurity = false;
                    string strConnection = ConfigurationManager.AppSettings["FWConnection"].ToString();
                    string[] acesso = strConnection.Split(';');
                    string dataSource = acesso[0].Split('=')[1].ToString();
                    string banco = acesso[1].Split('=')[1].ToString();

                    connectionStringBuilder.InitialCatalog = banco;
                    connectionStringBuilder.DataSource = dataSource;
                    connectionStringBuilder.ConnectTimeout = 999999999;
                    connectionStringBuilder.IntegratedSecurity = true;

                    _dbConn.ConnectionString = connectionStringBuilder.ToString();
                }
                else
                {
                    _dbConn.ConnectionString = (ConfigurationManager.AppSettings["FWConnection"].ToString() + "Connection Timeout=9999;");

                }
            }
            return _dbConn;
        }
    }

    public void Conecta()
    {
        //GUSTAVO
        try
        {
            if (dbConn.State == 0)
                dbConn.Open();
            else
                dbConn.Close();
        }
        catch (Exception e)
        {
            throw new Exception("Erro ao Conectar com Banco de Dados. Classe: Banco.cs.\nMessage: " + e.Message);
        }
        finally { }
    }

    public void Desconecta()
    {
        try
        {
            dbConn.Close();
        }
        catch (Exception e)
        {
            throw new Exception("Erro ao desconectar. Classe: Banco.cs\nMessage: " + e.Message);
        }
    }

    public string Executar(string sSQL, List<DbParameter> dbParameterList)
    {
        StringBuilder sbParam = new StringBuilder();
        string erro = "";
        try
        {
            Conecta();
            DbCommand dbComm = _dbFactory.CreateCommand();
            dbComm.CommandText = sSQL;
            dbComm.Connection = dbConn;
            foreach (DbParameter dbParameter in dbParameterList)
            {
                if (dbParameter.Value == null)
                    dbParameter.Value = DBNull.Value;

                if (ConfigurationManager.AppSettings["FWProvider"].Contains("OracleClient"))
                    dbParameter.ParameterName = dbParameter.ParameterName.Replace("@", ":");

                dbComm.Parameters.Add(dbParameter);

                //Retorna String para erro
                sbParam.Append(RetParam(dbParameter));
            }
            if (ConfigurationManager.AppSettings["FWProvider"].Contains("OracleClient"))
            {
                dbComm.CommandText = dbComm.CommandText.Replace("@", ":");
                //PJ13082012
                //Para bancos Oracle que tem dblink @ informamos $ para não converter como se fosse parâmetro
                dbComm.CommandText = dbComm.CommandText.Replace("$", "@");
                dbComm.CommandText = dbComm.CommandText.Replace("isnull", "nvl");
                sSQL = dbComm.CommandText;
            }

            dbComm.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            erro = "Erro ao Consultar. Classe: Banco.cs\nMessage: " + e.Message + "\nParametros: " + sbParam + "\nSQL: " + sSQL;
            throw new Exception(erro);
        }
        finally
        {
            Desconecta();
        }
        return erro;
    }

    public string ExecutarRetornarIndice(string sSQL, List<DbParameter> dbParameterList, string sSequence, bool sRetornaIndice)
    {
        string Indice = "";
        StringBuilder sbParam = new StringBuilder();
        try
        {
            Conecta();
            DbCommand dbComm = _dbFactory.CreateCommand();
            dbComm.CommandText = sSQL;
            dbComm.Connection = dbConn;
            foreach (DbParameter dbParameter in dbParameterList)
            {
                if (dbParameter.Value == null)
                    dbParameter.Value = DBNull.Value;

                if (ConfigurationManager.AppSettings["FWProvider"].Contains("OracleClient"))
                    dbParameter.ParameterName = dbParameter.ParameterName.Replace("@", ":");

                dbComm.Parameters.Add(dbParameter);

                //Retorna String para erro
                sbParam.Append(RetParam(dbParameter));
            }

            if (ConfigurationManager.AppSettings["FWProvider"].Contains("OracleClient"))
                dbComm.CommandText = dbComm.CommandText.Replace("@", ":");

            dbComm.ExecuteNonQuery();

            if (sRetornaIndice)
            {
                if (ConfigurationManager.AppSettings["FWProvider"].Contains("SqlClient"))
                    sSQL = "SELECT @@IDENTITY AS INDICE ";
                if (ConfigurationManager.AppSettings["FWProvider"].Contains("OracleClient"))
                {
                    int countBranco = 0;
                    string nomTbl = "";

                    for (int i = 0; i < sSQL.Length; i++)
                    {
                        string caracter = sSQL[i].ToString();
                        if (caracter == " ")
                            countBranco += 1;

                        if (countBranco == 2)
                            nomTbl = nomTbl + sSQL[i].ToString();
                    }
                    if (!string.IsNullOrEmpty(sSequence))
                    {
                        sSQL = "SELECT " + sSequence + ".CURRVAL AS INDICE FROM DUAL ";
                    }
                    else
                    {
                        sSQL = "SELECT " + nomTbl.Trim() + "_SEQ.CURRVAL AS INDICE FROM DUAL ";
                    }
                }
            }
            if (sRetornaIndice)
            {
                DataSet dtSet = new DataSet();
                dbComm = _dbFactory.CreateCommand();
                dbComm.CommandText = sSQL;
                dbComm.Connection = dbConn;
                DbDataAdapter dtAdapter = _dbFactory.CreateDataAdapter();
                dtAdapter.SelectCommand = dbComm;
                dtAdapter.Fill(dtSet);
                DataRow drCurrent = dtSet.Tables[0].Rows[0];
                Indice = drCurrent["INDICE"].ToString();
            }
            else
            {
                Indice = "0";
            }
        }
        catch (Exception e)
        {
            return "Erro ao Inserir. Classe: Banco.cs\nMessage: " + e.Message + "\nParametros: " + sbParam + "\nSQL: " + sSQL;
        }
        finally
        {
            Desconecta();
        }
        return Indice;
    }

    public DataSet ExecutarRetorno(string sSQL, List<DbParameter> dbParameterList)
    {
        StringBuilder sbParam = new StringBuilder();
        try
        {
            Conecta();
            DbCommand dbComm = _dbFactory.CreateCommand();
            dbComm.Connection = dbConn;
            dbComm.CommandText = sSQL;
            dbComm.CommandTimeout = 200;
            foreach (DbParameter dbParameter in dbParameterList)
            {
                if (dbParameter.Value == null)
                    dbParameter.Value = DBNull.Value;

                if (ConfigurationManager.AppSettings["FWProvider"].Contains("OracleClient"))
                    dbParameter.ParameterName = dbParameter.ParameterName.Replace("@", ":");

                sbParam.Append(RetParam(dbParameter));

                dbComm.Parameters.Add(dbParameter);
            }

            if (ConfigurationManager.AppSettings["FWProvider"].Contains("OracleClient"))
            {
                dbComm.CommandText = dbComm.CommandText.Replace("@", ":");
                //PJ13082012
                //Para bancos Oracle que tem dblink @ informamos $ para não converter como se fosse parâmetro
                dbComm.CommandText = dbComm.CommandText.Replace("$", "@");
                dbComm.CommandText = dbComm.CommandText.Replace("isnull", "nvl");
                sSQL = dbComm.CommandText;
            }

            DataSet dtSet = new DataSet();
            DbDataAdapter dtAdapter = _dbFactory.CreateDataAdapter();
            dtAdapter.SelectCommand = dbComm;
            dtAdapter.Fill(dtSet);

            return dtSet;
        }
        catch (Exception e)
        {
            throw new Exception("Erro ao Consultar. Classe: Banco.cs\nMessage: " + e.Message + "\nParametros: " + sbParam + "\nSQL: " + sSQL);
        }
        finally
        {
            Desconecta();
        }
    }

    public DbParameter CreateParameter(string NomParameter, object ValueParameter)
    {
        DbProviderFactory dbFactory = DbProviderFactories.GetFactory(ConfigurationManager.AppSettings["FWProvider"]);
        DbParameter dbParameter = dbFactory.CreateParameter();
        dbParameter.Value = ValueParameter;
        dbParameter.ParameterName = NomParameter;
        return dbParameter;
    }

    public string RetParam(List<DbParameter> list)
    {
        string sParam = string.Empty;

        if (ConfigurationManager.AppSettings["FWProvider"].Contains("OracleClient"))
        {
            foreach (DbParameter item in list)
            {
                item.ParameterName = item.ParameterName.Replace("@", ":");

                if (item.DbType.ToString().ToUpper() == "STRING" || item.DbType.ToString().ToUpper() == "ANSISTRING")
                {
                    sParam += " VARIABLE " + item.ParameterName.Replace(":", "") + " " + RetTipo(item.DbType.ToString(), 0) + "\r\n" +
                    " EXEC " + item.ParameterName + ":= '" + item.Value + "' ; \r\n";
                }
                else
                {
                    sParam += " VARIABLE " + item.ParameterName.Replace(":", "") + " " + RetTipo(item.DbType.ToString(), 0) + " ; \r\n" +
                   " EXEC " + item.ParameterName + ":= " + item.Value + " ; \r\n";
                }
            }
        }
        else
        {
            foreach (DbParameter item in list)
            {
                if (item.DbType.ToString().ToUpper() == "STRING" || item.DbType.ToString().ToUpper() == "DATETIME")
                {
                    sParam += " DECLARE " + item.ParameterName + " " + RetTipo(item.DbType.ToString(), 1) + "  = '" + item.Value + "' \r\n";
                }
                else
                {
                    sParam += " DECLARE " + item.ParameterName + " " + RetTipo(item.DbType.ToString(), 1) + "  = " + item.Value + " \r\n";
                }
            }
        }

        return sParam;
    }

    public string RetParam(DbParameter param)
    {
        string sParam = string.Empty;

        if (ConfigurationManager.AppSettings["FWProvider"].Contains("OracleClient"))
        {
            if (param.DbType.ToString().ToUpper() == "STRING" || param.DbType.ToString().ToUpper() == "ANSISTRING")
            {
                sParam += " VARIABLE " + param.ParameterName.Replace(":", "") + " " + RetTipo(param.DbType.ToString(), 0) + "\r\n" +
                " EXEC " + param.ParameterName + ":= '" + param.Value + "' ; \r\n";
            }
            else
            {
                sParam += " VARIABLE " + param.ParameterName.Replace(":", "") + " " + RetTipo(param.DbType.ToString(), 0) + " ; \r\n" +
               " EXEC " + param.ParameterName + ":= " + param.Value + " ; \r\n";
            }

        }
        else
        {
            if (param.DbType.ToString().ToUpper() == "STRING" || param.DbType.ToString().ToUpper() == "DATETIME")
            {
                sParam += " DECLARE " + param.ParameterName + " " + RetTipo(param.DbType.ToString(), 1) + "  = '" + param.Value + "' \r\n";
            }
            else
            {
                sParam += " DECLARE " + param.ParameterName + " " + RetTipo(param.DbType.ToString(), 1) + "  = " + param.Value + " \r\n";
            }
        }

        return sParam;
    }

    public string RetTipo(string tipo, int banco)
    {
        //SQL SERVER
        if (banco == 1)
        {
            switch (tipo.ToUpper())
            {
                case "STRING":
                    tipo = "VARCHAR(MAX)";
                    break;
                case "INT32":
                    tipo = "INT";
                    break;
                case "INT16":
                    tipo = "INT";
                    break;
                case "DATETIME":
                    tipo = "DATETIME";
                    break;
                case "DECIMAL":
                    tipo = "DECIMAL";
                    break;
                case "DOUBLE":
                    tipo = "DECIMAL";
                    break;
            }
        }
        //ORACLE
        else
        {
            switch (tipo.ToUpper())
            {
                case "ANSISTRING":
                    tipo = "VARCHAR2";
                    break;
                case "STRING":
                    tipo = "VARCHAR2";
                    break;
                case "INT32":
                    tipo = "NUMBER";
                    break;
                case "INT16":
                    tipo = "NUMBER";
                    break;
                case "DATETIME":
                    tipo = "INTERVAL";
                    break;
                case "DECIMAL":
                    tipo = "NUMBER";
                    break;
            }

        }

        return tipo;
    }

    public DataSet ListarTabelasDoBanco()
    {
        List<DbParameter> dbParameterList = new List<DbParameter>();
        string sSQL = "";

        sSQL = " SELECT upper(name) as name FROM sys.Tables order by name ";

        string SQL = RetParam(dbParameterList);
        SQL += sSQL;

        return ExecutarRetorno(sSQL, dbParameterList);
    }
}
