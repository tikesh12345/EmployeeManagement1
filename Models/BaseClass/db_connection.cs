using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;
using static ReturnClass;

/// <summary>
/// Summary description for db_connection
/// </summary>
public class db_connection
{
    public MySqlDataAdapter Adapter;
    public MySqlDataReader reader;
    public MySqlCommand command;
    public MySqlParameter objMySqlParameter;
    private string con_str;
    public db_connection()
    {
        con_str = GetConnectionString(DBConnectionList.TransactionDb);
    }
   
    #region select query without parameter
    /// <summary>
    /// Select query with default connection string without parameter
    /// </summary>
    /// <param name="_query"></param>
    /// <returns></returns>
    public ReturnDataTable executeSelectQuery(String _query)
    {
        ReturnDataTable dt = new ReturnDataTable();
        try
        {
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = _query;
                    using (Adapter = new MySqlDataAdapter())
                    {
                        Adapter.SelectCommand = cmd;
                        Adapter.Fill(dt.table);
                        dt.status = true;
                        dt.value = "";
                    }
                }
            }
        }
        catch (MySqlException ex)
        {

            Gen_Error_Rpt.Write_Error("executeSelectQuery - Query: " + _query + "\n   error - ", ex);

            dt.status = false;
            dt.message = ex.Message;
        }
        return dt;
    }

    /// <summary>
    /// Async Select query with default connection string without parameter
    /// </summary>
    /// <param name="_query"></param>
    /// <returns></returns>
    public async Task<ReturnDataTable> executeSelectQuery_async(String _query)
    {
        ReturnDataTable dt = new ReturnDataTable();
        try
        {
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = _query;
                    using (Adapter = new MySqlDataAdapter())
                    {
                        Adapter.SelectCommand = cmd;
                        await Adapter.FillAsync(dt.table);
                        dt.status = true;
                        dt.value = "";
                    }
                }
            }
        }
        catch (MySqlException ex)
        {

            Gen_Error_Rpt.Write_Error("executeSelectQuery - Query: " + _query + "\n   error - ", ex);

            dt.status = false;
            dt.message = ex.Message;
        }
        return dt;
    }

    /// <summary>
    /// Select query with custom connection string without parameter
    /// </summary>
    /// <param name="_query"></param>
    /// <param name="dbconname"></param>
    /// <returns></returns>
    public ReturnDataTable executeSelectQuery(String _query, DBConnectionList dbconname)
    {
        ReturnDataTable dt = new ReturnDataTable();

        try
        {
            string con_str1 = GetConnectionString(dbconname);
            using (MySqlConnection con = new MySqlConnection(con_str1))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = _query;
                    using (Adapter = new MySqlDataAdapter())
                    {
                        Adapter.SelectCommand = cmd;
                        Adapter.Fill(dt.table);
                        dt.status = true;
                        dt.value = "";
                    }
                }
            }
        }
        catch (MySqlException ex)
        {

            Gen_Error_Rpt.Write_Error("executeSelectQuery - Query: " + _query + "\n   error - ", ex);

            dt.status = false;
            dt.message = ex.Message;
        }
        return dt;
    }

    /// <summary>
    /// Async Select query with custom connection string without parameter
    /// </summary>
    /// <param name="_query"></param>
    /// <param name="dbconname"></param>
    /// <returns></returns>
    public async Task<ReturnDataTable> executeSelectQuery_async(String _query, DBConnectionList dbconname)
    {
        ReturnDataTable dt = new ReturnDataTable();

        try
        {
            string con_str1 = GetConnectionString(dbconname);
            using (MySqlConnection con = new MySqlConnection(con_str1))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = _query;
                    using (Adapter = new MySqlDataAdapter())
                    {
                        Adapter.SelectCommand = cmd;
                        await Adapter.FillAsync(dt.table);
                        dt.status = true;
                        dt.value = "";
                    }
                }
            }
        }
        catch (MySqlException ex)
        {

            Gen_Error_Rpt.Write_Error("executeSelectQuery - Query: " + _query + "\n   error - ", ex);

            dt.status = false;
            dt.message = ex.Message;
        }
        return dt;
    }
    #endregion

    #region select query with parameter 

    /// <summary>
    /// Execute Select Query With Parameters
    /// </summary>
    /// <param name="_query"></param>
    /// <param name="sqlParameter"></param>
    /// <returns></returns>
    public ReturnDataTable executeSelectQuery(String _query, MySqlParameter[] sqlParameter)
    {
        ReturnDataTable dt = new ReturnDataTable();
        try
        {
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = _query;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(sqlParameter);

                    using (Adapter = new MySqlDataAdapter())
                    {
                        Adapter.SelectCommand = cmd;
                        Adapter.Fill(dt.table);
                        dt.status = true;
                    }
                }
            }
        }
        catch (MySqlException ec)
        {
            Gen_Error_Rpt.Write_Error("executeSelectQuery - Query: " + _query + "\n   error - ", ec);
            dt.status = false;
            dt.message = ec.Message;
        }
        return dt;
    }
    /// <summary>
    /// Async select query with parameter
    /// </summary>
    /// <param name="_query"></param>
    /// <param name="sqlParameter"></param>
    /// <returns></returns>
    public async Task<ReturnDataTable> executeSelectQuery_async(String _query, MySqlParameter[] sqlParameter)
    {
        ReturnDataTable dt = new ReturnDataTable();
        try
        {
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = _query;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(sqlParameter);

                    using (Adapter = new MySqlDataAdapter())
                    {
                        Adapter.SelectCommand = cmd;
                        await Adapter.FillAsync(dt.table);
                        dt.status = true;
                    }
                }
            }
        }
        catch (MySqlException ec)
        {
            Gen_Error_Rpt.Write_Error("executeSelectQuery - Query: " + _query + "\n   error - ", ec);
            dt.status = false;
            dt.message = ec.Message;
        }
        catch(Exception e)
        {

        }
        return dt;
    }

    /// <summary>
    /// select query with parameter an custom connection string
    /// </summary>
    /// <param name="_query"></param>
    /// <param name="sqlParameter"></param>
    /// <param name="dbconname"></param>
    /// <returns></returns>
    public ReturnDataTable executeSelectQuery(String _query, MySqlParameter[] sqlParameter, DBConnectionList dbconname)
    {
        ReturnDataTable dt = new ReturnDataTable();
        try
        {
            using (MySqlConnection con = new MySqlConnection(GetConnectionString(dbconname)))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = _query;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(sqlParameter);

                    using (Adapter = new MySqlDataAdapter())
                    {
                        Adapter.SelectCommand = cmd;
                        Adapter.Fill(dt.table);
                        dt.status = true;
                    }
                }
            }
        }
        catch (MySqlException ec)
        {

            Gen_Error_Rpt.Write_Error("executeSelectQuery - Query: " + _query + "\n   error - ", ec);

            dt.status = false;
            dt.message = ec.Message;
        }
        return dt;
    }

    /// <summary>
    /// Async select Query with parameter and custom connection string
    /// </summary>
    /// <param name="_query"></param>
    /// <param name="sqlParameter"></param>
    /// <param name="dbconname"></param>
    /// <returns></returns>
    public async Task<ReturnDataTable> executeSelectQuery_async(String _query, MySqlParameter[] sqlParameter, DBConnectionList dbconname)
    {
        ReturnDataTable dt = new ReturnDataTable();
        try
        {
            using (MySqlConnection con = new MySqlConnection(GetConnectionString(dbconname)))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = _query;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(sqlParameter);

                    using (Adapter = new MySqlDataAdapter())
                    {
                        Adapter.SelectCommand = cmd;
                        await Adapter.FillAsync(dt.table);
                        dt.status = true;
                    }
                }
            }
        }
        catch (MySqlException ec)
        {
            Gen_Error_Rpt.Write_Error("executeSelectQuery_async - Query: " + _query + "\n   error - ", ec);

            dt.status = false;
            dt.message = ec.Message;
        }
        return dt;
    }

    #endregion


    /// <summary>
    /// Execute Insert Query
    /// </summary>
    /// <param name="_query"></param>
    /// <param name="sqlParameter"></param>
    /// <returns></returns>
    public ReturnBool executeInsertQuery(String _query, MySqlParameter[] sqlParameter)
    {
        ReturnBool dt = new ReturnBool();
        try
        {
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = _query;
                    cmd.Parameters.AddRange(sqlParameter);
                    using (Adapter = new MySqlDataAdapter())
                    {
                        Adapter.InsertCommand = cmd;
                        cmd.ExecuteNonQuery();
                        dt.status = true;
                    }
                }
            }
        }
        catch (MySqlException ex2)
        {

            Gen_Error_Rpt.Write_Error("executeInsertQuery - Query: " + _query + "\n   error - ", ex2);

            dt.status = false;
            dt.message = ex2.Message;
        }
        return dt;
    }

    /// <summary>
    /// Execute Insert Query
    /// </summary>
    /// <param name="_query"></param>
    /// <param name="sqlParameter"></param>
    /// <param name="dbconname"></param>
    /// <returns></returns>
    public ReturnBool executeInsertQuery(String _query, MySqlParameter[] sqlParameter, DBConnectionList dbconname)
    {
        ReturnBool dt = new ReturnBool();
        try
        {
            string con_str1 = GetConnectionString(dbl: dbconname);

            using (MySqlConnection con = new MySqlConnection(con_str1))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = _query;
                    cmd.Parameters.AddRange(sqlParameter);
                    using (Adapter = new MySqlDataAdapter())
                    {
                        Adapter.InsertCommand = cmd;
                        cmd.ExecuteNonQuery();
                        dt.status = true;
                    }
                }
            }
        }
        catch (MySqlException ex2)
        {

            Gen_Error_Rpt.Write_Error("executeInsertQuery - Query: " + _query + "\n   error - ", ex2);

            dt.status = false;
            dt.message = ex2.Message;
        }
        return dt;
    }
    /// <summary>
    /// Execute Update Query
    /// </summary>
    /// <param name="_query"></param>
    /// <param name="sqlParameter"></param>
    /// <returns></returns>
    public ReturnBool executeUpdateQuery(String _query, MySqlParameter[] sqlParameter)
    {
        ReturnBool dt = new ReturnBool();
        try
        {
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = _query;
                    cmd.Parameters.AddRange(sqlParameter);
                    using (Adapter = new MySqlDataAdapter())
                    {
                        Adapter.UpdateCommand = cmd;
                        cmd.ExecuteNonQuery();
                        dt.status = true;
                    }
                }
            }
        }
        catch (MySqlException exu)
        {

            Gen_Error_Rpt.Write_Error("executeUpdateQuery - Query: " + _query + "\n   error - ", exu);

            dt.status = false;
            dt.message = exu.Message;
        }
        return dt;
    }

    public ReturnBool executeUpdateQuery(String _query, MySqlParameter[] sqlParameter, DBConnectionList dbconname)
    {
        ReturnBool dt = new ReturnBool();
        try
        {
            string con_str1 = GetConnectionString(dbl: dbconname);
            using (MySqlConnection con = new MySqlConnection(con_str1))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = _query;
                    cmd.Parameters.AddRange(sqlParameter);
                    using (Adapter = new MySqlDataAdapter())
                    {
                        Adapter.UpdateCommand = cmd;
                        cmd.ExecuteNonQuery();
                        dt.status = true;
                    }
                }
            }
        }
        catch (MySqlException exu)
        {

            Gen_Error_Rpt.Write_Error("executeUpdateQuery - Query: " + _query + "\n   error - ", exu);

            dt.status = false;
            dt.message = exu.Message;
        }
        return dt;
    }
    public ReturnBool executeDeleteQuery(String _query, MySqlParameter[] sqlParameter)
    {
        ReturnBool dt = new ReturnBool();
        try
        {           
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = _query;
                    cmd.Parameters.AddRange(sqlParameter);
                    using (Adapter = new MySqlDataAdapter())
                    {
                        Adapter.DeleteCommand = cmd;
                        cmd.ExecuteNonQuery();
                        dt.status = true;
                    }
                }
            }
        }
        catch (MySqlException exp)
        {

            Gen_Error_Rpt.Write_Error("executeUpdateQuery - Query: " + _query + "\n   error - ", exp);

            dt.status = false;
            dt.message = exp.Message;
        }
        return dt;
    }

    public ReturnBool executeDeleteQuery(String _query, MySqlParameter[] sqlParameter, DBConnectionList dbconname)
    {
        ReturnBool dt = new ReturnBool();
        try
        {
            string con_str1 = GetConnectionString(dbl: dbconname);
            using (MySqlConnection con = new MySqlConnection(con_str1))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = _query;
                    cmd.Parameters.AddRange(sqlParameter);
                    using (Adapter = new MySqlDataAdapter())
                    {
                        Adapter.DeleteCommand = cmd;
                        cmd.ExecuteNonQuery();
                        dt.status = true;
                    }
                }
            }
        }
        catch (MySqlException exp)
        {

            Gen_Error_Rpt.Write_Error("executeUpdateQuery - Query: " + _query + "\n   error - ", exp);

            dt.status = false;
            dt.message = exp.Message;
        }
        return dt;
    }
    /// <summary>
    /// Execute Insert Query Async
    /// </summary>
    /// <param name="_query"></param>
    /// <param name="sqlParameter"></param>
    /// <returns></returns>
    public async Task<ReturnBool> executeInsertQuery_async(String _query, MySqlParameter[] sqlParameter)
    {
        ReturnBool dt = new ReturnBool();
        try
        {
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = _query;
                    cmd.Parameters.AddRange(sqlParameter);
                    using (Adapter = new MySqlDataAdapter())
                    {
                        Adapter.InsertCommand = cmd;
                        await cmd.ExecuteNonQueryAsync();
                        dt.status = true;
                    }
                }
            }
        }
        catch (MySqlException ex2)
        {

            Gen_Error_Rpt.Write_Error("executeInsertQuery - Query: " + _query + "\n   error - ", ex2);

            dt.status = false;
            dt.message = ex2.Message;
        }
        return dt;
    }

    /// <summary>
    /// Execute Insert Query Async
    /// </summary>
    /// <param name="_query"></param>
    /// <param name="sqlParameter"></param>
    /// <param name="dbconname"></param>
    /// <returns></returns>
    public async Task<ReturnBool> executeInsertQuery_async(String _query, MySqlParameter[] sqlParameter, DBConnectionList dbconname)
    {
        ReturnBool dt = new ReturnBool();
        try
        {
            string con_str1 = GetConnectionString(dbl: dbconname);

            using (MySqlConnection con = new MySqlConnection(con_str1))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = _query;
                    cmd.Parameters.AddRange(sqlParameter);
                    using (Adapter = new MySqlDataAdapter())
                    {
                        Adapter.InsertCommand = cmd;
                        await cmd.ExecuteNonQueryAsync();
                        dt.status = true;
                    }
                }
            }
        }
        catch (MySqlException ex2)
        {

            Gen_Error_Rpt.Write_Error("executeInsertQuery - Query: " + _query + "\n   error - ", ex2);

            dt.status = false;
            dt.message = ex2.Message;
        }
        return dt;
    }

    public async Task<ReturnBool> executeUpdateQuery_async(String _query, MySqlParameter[] sqlParameter)
    {
        ReturnBool dt = new ReturnBool();
        try
        {
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = _query;
                    cmd.Parameters.AddRange(sqlParameter);
                    using (Adapter = new MySqlDataAdapter())
                    {
                        Adapter.UpdateCommand = cmd;
                        await cmd.ExecuteNonQueryAsync();
                        dt.status = true;
                    }
                }
            }
        }
        catch (MySqlException exu)
        {

            Gen_Error_Rpt.Write_Error("executeUpdateQuery - Query: " + _query + "\n   error - ", exu);

            dt.status = false;
            dt.message = exu.Message;
        }
        return dt;
    }

    public async Task<ReturnBool> executeUpdateQuery_async(String _query, MySqlParameter[] sqlParameter, DBConnectionList dbconname)
    {
        ReturnBool dt = new ReturnBool();
        try
        {
            string con_str1 = GetConnectionString(dbl: dbconname);
            using (MySqlConnection con = new MySqlConnection(con_str1))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = _query;
                    cmd.Parameters.AddRange(sqlParameter);
                    using (Adapter = new MySqlDataAdapter())
                    {
                        Adapter.UpdateCommand = cmd;
                        await cmd.ExecuteNonQueryAsync();
                        dt.status = true;
                    }
                }
            }
        }
        catch (MySqlException exu)
        {

            Gen_Error_Rpt.Write_Error("executeUpdateQuery - Query: " + _query + "\n   error - ", exu);

            dt.status = false;
            dt.message = exu.Message;
        }
        return dt;
    }

    /// <summary>
    /// Execute Insert Query Async and return last inserted id
    /// </summary>
    /// <param name="_query"></param>
    /// <param name="sqlParameter"></param>
    /// <returns></returns>
    public async Task<ReturnString> executeInsertQuery_returnId_async(String _query, MySqlParameter[] sqlParameter)
    {
        ReturnString rs = new ReturnString();
        try
        {
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = _query;
                    cmd.Parameters.AddRange(sqlParameter);
                    using (Adapter = new MySqlDataAdapter())
                    {
                        Adapter.InsertCommand = cmd;
                        await cmd.ExecuteNonQueryAsync();
                        rs.value = cmd.LastInsertedId.ToString();
                        rs.status = true;
                    }
                }
            }
        }
        catch (MySqlException ex2)
        {

            Gen_Error_Rpt.Write_Error("executeInsertQuery - Query: " + _query + "\n   error - ", ex2);

            rs.status = false;
            rs.message = ex2.Message;
        }
        return rs;
    }

    public async Task<ReturnString> executeUpdateQuery_returns_affected_rowCount_async(String _query, MySqlParameter[] sqlParameter)
    {
        ReturnString rs = new ReturnString();
        try
        {
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = _query;
                    cmd.Parameters.AddRange(sqlParameter);
                    using (Adapter = new MySqlDataAdapter())
                    {
                        Adapter.UpdateCommand = cmd;
                        int x = await cmd.ExecuteNonQueryAsync();
                        rs.status = true;
                        rs.value = x.ToString();
                    }
                }
            }
        }
        catch (MySqlException exu)
        {
            Gen_Error_Rpt.Write_Error("executeUpdateQuery - Query: " + _query + "\n   error - ", exu);
            rs.status = false;
            rs.message = exu.Message;
        }
        return rs;
    }

    /// <summary>
    /// Get Connection String
    /// </summary>
    /// <param name="_query"></param>
    /// <returns></returns>
    private string GetConnectionString(DBConnectionList dbl)
    {
        Utilities util = new Utilities();
        string con_str_local = "";
        ReturnBool rb = util.GetAppSettings("Build", "Version");

        if (rb.status)
        {
            string buildType = rb.message.ToLower();
            if (buildType == "production")
            {
                switch (dbl)
                {
                    case DBConnectionList.TransactionDb:
                        con_str_local = " Data Source = 127.0.0.1; Port = 3306; Initial Catalog = gad; User ID = root; Password = tikesh; Charset = utf8mb4; Allow User Variables=true; SSL Mode=None;";
                        break;
                    case DBConnectionList.ReportingDb:
                        con_str_local = " Data Source = 127.0.0.1; Port = 3306; Initial Catalog = gad; User ID = root; Password = tikesh; Charset = utf8mb4; Allow User Variables=true; SSL Mode=None;";
                        break;
                    case DBConnectionList.UploadDb:
                        con_str_local = " Data Source = 127.0.0.1; Port = 3306; Initial Catalog = gad; User ID = root; Password = tikesh; Charset = utf8mb4; Allow User Variables=true; SSL Mode=None;";
                        break;
                    default:
                        con_str_local = con_str;
                        break;
                }
            }
            else if (buildType == "development")
            {
                switch (dbl)
                {
                    case DBConnectionList.TransactionDb:
                        con_str_local = " Data Source = 127.0.0.1; Port = 3306; Initial Catalog = gad; User ID = root; Password = tikesh; Charset = utf8mb4; Allow User Variables=true; SSL Mode=None;";
                        break;
                    case DBConnectionList.ReportingDb:
                        con_str_local = " Data Source = 127.0.0.1; Port = 3306; Initial Catalog = gad; User ID = root; Password = tikesh; Charset = utf8mb4; Allow User Variables=true; SSL Mode=None;";
                        break;
                    case DBConnectionList.UploadDb:
                        con_str_local = " Data Source = 127.0.0.1; Port = 3306; Initial Catalog = gad; User ID = root; Password = tikesh; Charset = utf8mb4; Allow User Variables=true; SSL Mode=None;";
                        break;
                    default:
                        con_str_local = con_str;
                        break;
                }
            }
            else if (buildType == "local")
            {
                switch (dbl)
                {
                    case DBConnectionList.TransactionDb:
                        con_str_local = " Data Source = 127.0.0.1; Port = 3306; Initial Catalog = gad; User ID = root; Password = tikesh; Charset = utf8mb4; Allow User Variables=true; SSL Mode=None;";
                        break;
                    case DBConnectionList.ReportingDb:
                        con_str_local = " Data Source = 127.0.0.1; Port = 3306; Initial Catalog = gad; User ID = root; Password = tikesh; Charset = utf8mb4; Allow User Variables=true; SSL Mode=None;";
                        break;
                    case DBConnectionList.UploadDb:
                        con_str_local = " Data Source = 127.0.0.1; Port = 3306; Initial Catalog = gad; User ID = root; Password = tikesh; Charset = utf8mb4; Allow User Variables=true; SSL Mode=None;";
                        break;
                    default:
                        con_str_local = con_str;
                        break;
                }
            }
        }
        else
        {
            con_str_local = rb.error;
        }
        return con_str_local;
    }
}

public enum DBConnectionList
{
    TransactionDb = 1,
    ReportingDb = 2,
    UploadDb = 3
}