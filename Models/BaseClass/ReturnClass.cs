using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ReturnClass
/// </summary>
public class ReturnClass
{
    public class ReturnDataTable
    {
        public string type { get; set; }
        public bool status { get; set; }
        public string json { get; set; }
        public string message { get; set; }
        public DataTable table { get; set; }
        //public DataTableCollection tables { get; set; }
        public string file_name { get; set; }
        public string value { get; set; }
        public ReturnDataTable()
        {
            status = false;
            message = "";
            file_name = "";
            json = "[]";
            table = new DataTable();
            value = "";
        }
    }

    public class ReturnDataSet
    {
        public bool status { get; set; }
        public string message { get; set; }
        public DataSet dataset { get; set; }
        public string file_name { get; set; }
        public string json { get; set; }
        public string value { get; set; }
        public ReturnDataSet()
        {
            status = false;
            message = "";
            file_name = "";
            json = "[]";
            dataset = new DataSet();
            value = "";
        }
    }
    public class ReturnBool
    {
        public bool status { get; set; }
        public string message { get; set; }
        public string error { get; set; }
        public ReturnBool()
        {
            status = false;
            message = "";
            error = "";
        }
    }
    public class ReturnString
    {
        public bool status { get; set; }
        public string message { get; set; }
        public string value { get; set; }
        public string msg_id { get; set; }
        public string email_msg { get; set; }
        public ReturnString()
        {
            status = false;
            message = "";
            value = "";
            msg_id = "";
            email_msg = "";
        }
    }
    public class ReturnReportName
    {
        public bool status { get; set; }
        public string message { get; set; }
        public string file_name { get; set; }
        public ReturnReportName()
        {
            status = false;
            message = "";
            file_name = "";
        }
    }

   
}