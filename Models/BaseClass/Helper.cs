/*using EmployeeManagementSystem.Models;
using System.Collections.Generic;
using System.Data;

public class Helper
{
    /// <summary>
    /// Get List Vale pair For Dropdowns and Radio
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public List<ListValue> GetGenericDropdownList(DataTable dt)
    {
        List<ListValue> lv = new List<ListValue>();
        DataColumnCollection columns = dt.Columns;
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                lv.Add(new ListValue
                {
                    cir_category_code = dr["cir_category_nm"].ToString(),
                    cir_category_nm = dr["cir_category_code"].ToString(),
                    type = dt.Columns.Contains("extraField") ? dr["extraField"].ToString() : ""
                }); ;
            }
        }
        return lv;
    }








    public List<ListValue1> GetGenericDropdownList2(DataTable dt)
    {
        List<ListValue1> lv = new List<ListValue1>();
        DataColumnCollection columns = dt.Columns;
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                lv.Add(new ListValue1
                {
                    order_number = dr["order_number"].ToString(),
                    subject = dr["subject"].ToString(),
                    date = dr["date"].ToString(),
                    category = dr["category"].ToString(),
                    word_to_search = dr["word_to_search"].ToString(),
                    search_category = dr["search_category"].ToString(),
                    search_sub_category = dr["search_sub_category"].ToString(),
                    performance_status = dr["performance_status"].ToString(),
                    circuler_display = dr["circuler_display"].ToString(),
                    is_this_performance_display = dr["is_this_performance_display"].ToString(),

                }) ; ;
            }
        }
        return lv;
    }





    public List<ListValue2> GetGenericDropdownList3(DataTable dt)
    {
        List<ListValue2> lv = new List<ListValue2>();
        DataColumnCollection columns = dt.Columns;
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                lv.Add(new ListValue2
                {
                    circular_order_no = dr["circular_order_no"].ToString(),
                    circular_date = dr["circular_date"].ToString(),
                    circular_subject = dr["circular_subject"].ToString(),
                    state_circular_code = dr["state_circular_code"].ToString(),
                    publish_status = dr["publish_status"].ToString(),
                    circular_end_date = dr["circular_end_date"].ToString(),
                    circular_category_code = dr["circular_category_code"].ToString(),
                    circular_start_date = dr["circular_start_date"].ToString(),
                    permanent_status = dr["permanent_status"].ToString(),
                    entry_datetime = dr["entry_datetime"].ToString(),

                    publish_datetime = dr["publish_datetime"].ToString(),
                    user_ip = dr["user_ip"].ToString(),
                    circular_year = dr["circular_year"].ToString(),
                    user_code = dr["user_code"].ToString(),
                    search_word = dr["search_word"].ToString(),
                    notice_cat = dr["notice_cat"].ToString(),
                    notice_subcat = dr["notice_subcat"].ToString(),
                    apr_stts = dr["apr_stts"].ToString(),
                  

                }); ;
            }
        }
        return lv;
    }
}
*/