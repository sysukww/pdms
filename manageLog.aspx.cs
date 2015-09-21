using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using DevExpress;


public partial class manageLog : System.Web.UI.Page
{
    Operation operation = new Operation(); //业务类对象

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            GridViewBind();
        }
    }

    /// <summary>
    /// 绑定未发送短信到GridViev控件
    /// </summary>
    private void GridViewBind()
    {

        if (txtEndtime.Text.ToString() == "" || txtStarttime.Text.ToString() == "")
        {
            GridView1.DataSource = operation.SelectBill();
            GridView1.DataBind();
            lblPageSum.Text = "当前页为　" + (GridView1.PageIndex + 1) + " / " + GridView1.PageCount + "　页";
        }
        else
        {
            DateTime starttime;
            DateTime tempendtime;
            DateTime.TryParse(txtStarttime.Text.ToString(), out starttime);
            DateTime.TryParse(txtEndtime.Text.ToString(), out tempendtime);
            DateTime endtime = tempendtime.AddDays(1);


            GridView1.DataSource = operation.SelectLog(starttime, endtime);
            GridView1.DataBind();
            //显示当前页数
            lblPageSum.Text = "当前页为　" + (GridView1.PageIndex + 1) + " / " + GridView1.PageCount + "　页";
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[3].Text == "0")
            {
                e.Row.Cells[3].Text = "待分配";
            }
            else if (e.Row.Cells[3].Text == "1") e.Row.Cells[3].Text = "已认领";
            else if (e.Row.Cells[3].Text == "2") e.Row.Cells[3].Text = "成功";
            else if (e.Row.Cells[3].Text == "3") e.Row.Cells[3].Text = "失败";
            //高亮显示指定行
            e.Row.Attributes.Add("onMouseOver", "Color=this.style.backgroundColor;this.style.backgroundColor='#FFF000'");
            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=Color;");
            //多余字　使用...显示
            e.Row.Cells[5].Text = StringFormat.Out(e.Row.Cells[5].Text, 10);
            e.Row.Cells[0].Text = StringFormat.Out(e.Row.Cells[0].Text, 5);

        }
    }

    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        string id = GridView1.DataKeys[e.NewSelectedIndex].Value.ToString();
        //WebMessageBox.Show(id);     //测试使用
        Response.Write("<script>location.href='listLog.aspx?id=" + id + "'</script>");
        //Response.Write("<script>history.go(-1)</script>");
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        //GridViewBind();

        //获取查询数据
        int status = -1;       //状态
        string licenseno = "";                       //需要查询的车牌号
        string starttime = txtStarttime.Text.ToString();
        string endtime = txtEndtime.Text.ToString();

        int searchtype = 0;                                                   //查询类型
        DateTime startdt;
        DateTime enddt;
        DateTime.TryParse(starttime, out startdt);
        if (DateTime.TryParse(endtime, out enddt))
            enddt = enddt.AddDays(1);


        /* 选择查询的情况（
         * 0：licenseno/starttime/endtime are null，
         * 1：licenseno is not null, starttime and endtime are null,
         * 2：licenseno and starttime are not null, endtime is null,
         * 3：licenseno and endtime are not null, starttime is null,
         * 4：licenseno/starttime/endtime are not null,
         * 5：starttime is not null, licenseno and endtime are null,
         * 6：endtime is not null, licenseno and starttime are null,
         * 7：starttime and endtime are not null, licenseno is null
         * 。）
         * 
         */
        if (licenseno == "" && starttime == "" && endtime == "") searchtype = 0;
        else if (licenseno != "" && starttime == "" && endtime == "") searchtype = 1;
        else if (licenseno != "" && starttime != "" && endtime == "") searchtype = 2;
        else if (licenseno != "" && starttime == "" && endtime != "") searchtype = 3;
        else if (licenseno != "" && starttime != "" && endtime != "") searchtype = 4;
        else if (licenseno == "" && starttime != "" && endtime == "") searchtype = 5;
        else if (licenseno == "" && starttime == "" && endtime != "") searchtype = 6;
        else if (licenseno == "" && starttime != "" && endtime != "") searchtype = 7;


        GridView1.DataSource = operation.SelectBillbyInserttime(status, licenseno, startdt, enddt, searchtype);                       //显示所有订单
        GridView1.DataBind();
        //显示当前页数
        lblPageSum.Text = "当前页为　" + (GridView1.PageIndex + 1) + " / " + GridView1.PageCount + "　页";
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        //获取查询数据
        int status = -1;       //状态
        string licenseno = "";                       //需要查询的车牌号
        string starttime = txtStarttime.Text.ToString();
        string endtime = txtEndtime.Text.ToString();

        int searchtype = 0;                                                   //查询类型
        DateTime startdt;
        DateTime enddt;
        DateTime.TryParse(starttime, out startdt);
        if (DateTime.TryParse(endtime, out enddt))
            enddt = enddt.AddDays(1);


        /* 选择查询的情况（
         * 0：licenseno/starttime/endtime are null，
         * 1：licenseno is not null, starttime and endtime are null,
         * 2：licenseno and starttime are not null, endtime is null,
         * 3：licenseno and endtime are not null, starttime is null,
         * 4：licenseno/starttime/endtime are not null,
         * 5：starttime is not null, licenseno and endtime are null,
         * 6：endtime is not null, licenseno and starttime are null,
         * 7：starttime and endtime are not null, licenseno is null
         * 。）
         * 
         */
        if (licenseno == "" && starttime == "" && endtime == "") searchtype = 0;
        else if (licenseno != "" && starttime == "" && endtime == "") searchtype = 1;
        else if (licenseno != "" && starttime != "" && endtime == "") searchtype = 2;
        else if (licenseno != "" && starttime == "" && endtime != "") searchtype = 3;
        else if (licenseno != "" && starttime != "" && endtime != "") searchtype = 4;
        else if (licenseno == "" && starttime != "" && endtime == "") searchtype = 5;
        else if (licenseno == "" && starttime == "" && endtime != "") searchtype = 6;
        else if (licenseno == "" && starttime != "" && endtime != "") searchtype = 7;


        GridView1.DataSource = operation.SelectBillbyInserttime(status, licenseno, startdt, enddt, searchtype);                       //显示所有订单
        GridView1.DataBind();
        //显示当前页数
        lblPageSum.Text = "当前页为　" + (GridView1.PageIndex + 1) + " / " + GridView1.PageCount + "　页";

    }

    protected void BtnExport_Click(object sender, EventArgs e)
    {
        //获取查询数据
        int status = -1;       //状态
        string licenseno = "";                       //需要查询的车牌号
        string starttime = txtStarttime.Text.ToString();
        string endtime = txtEndtime.Text.ToString();

        int searchtype = 0;                                                   //查询类型
        DateTime startdt;
        DateTime enddt;
        DateTime.TryParse(starttime, out startdt);
        if (DateTime.TryParse(endtime, out enddt))
            enddt = enddt.AddDays(1);


        /* 选择查询的情况（
         * 0：licenseno/starttime/endtime are null，
         * 1：licenseno is not null, starttime and endtime are null,
         * 2：licenseno and starttime are not null, endtime is null,
         * 3：licenseno and endtime are not null, starttime is null,
         * 4：licenseno/starttime/endtime are not null,
         * 5：starttime is not null, licenseno and endtime are null,
         * 6：endtime is not null, licenseno and starttime are null,
         * 7：starttime and endtime are not null, licenseno is null
         * 。）
         * 
         */
        if (licenseno == "" && starttime == "" && endtime == "") searchtype = 0;
        else if (licenseno != "" && starttime == "" && endtime == "") searchtype = 1;
        else if (licenseno != "" && starttime != "" && endtime == "") searchtype = 2;
        else if (licenseno != "" && starttime == "" && endtime != "") searchtype = 3;
        else if (licenseno != "" && starttime != "" && endtime != "") searchtype = 4;
        else if (licenseno == "" && starttime != "" && endtime == "") searchtype = 5;
        else if (licenseno == "" && starttime == "" && endtime != "") searchtype = 6;
        else if (licenseno == "" && starttime != "" && endtime != "") searchtype = 7;

        DataSet ds = new DataSet();
        ds = operation.SelectBillbyInserttime(status, licenseno, startdt, enddt, searchtype); 

        ds.Tables[0].Columns[0].ColumnName = "订单编号";
        ds.Tables[0].Columns[1].ColumnName = "订单号";
        ds.Tables[0].Columns[2].ColumnName = "车牌号";
        ds.Tables[0].Columns[3].ColumnName = "订单状态";
        ds.Tables[0].Columns[4].ColumnName = "见费出单";
        ds.Tables[0].Columns[5].ColumnName = "收费时间";
        ds.Tables[0].Columns[6].ColumnName = "上午下午";
        ds.Tables[0].Columns[7].ColumnName = "催单次数";
        ds.Tables[0].Columns[8].ColumnName = "备注";
        ds.Tables[0].Columns[9].ColumnName = "插入时间";
        ds.Tables[0].Columns[10].ColumnName = "操作时间";
        ds.Tables[0].Columns[11].ColumnName = "操作人";

        DateTime dt = DateTime.Now;
        string dts = dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString() + ".xls";
        ExportResult(ds, dts);
    }

    public void ExportResult(DataSet ds, string excelName)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Charset = "";
        HttpContext.Current.Response.ContentType = "application/vnd.ms-xls";
        StringWriter stringWrite = new StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        DataGrid dg = new DataGrid();
        dg.DataSource = ds;
        dg.DataBind();
        dg.RenderControl(htmlWrite);
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode(excelName));
        HttpContext.Current.Response.Write(stringWrite.ToString());
        HttpContext.Current.Response.End();
    }



    protected void BtnExportLog_Click(object sender, EventArgs e)
    {
        //获取查询数据
        int status = -1;       //状态
        string licenseno = "";                       //需要查询的车牌号
        string starttime = txtStarttime.Text.ToString();
        string endtime = txtEndtime.Text.ToString();

        int searchtype = 0;                                                   //查询类型
        DateTime startdt;
        DateTime enddt;
        DateTime.TryParse(starttime, out startdt);
        if (DateTime.TryParse(endtime, out enddt))
            enddt = enddt.AddDays(1);


        /* 选择查询的情况（
         * 0：licenseno/starttime/endtime are null，
         * 1：licenseno is not null, starttime and endtime are null,
         * 2：licenseno and starttime are not null, endtime is null,
         * 3：licenseno and endtime are not null, starttime is null,
         * 4：licenseno/starttime/endtime are not null,
         * 5：starttime is not null, licenseno and endtime are null,
         * 6：endtime is not null, licenseno and starttime are null,
         * 7：starttime and endtime are not null, licenseno is null
         * 。）
         * 
         */
        if (licenseno == "" && starttime == "" && endtime == "") searchtype = 0;
        else if (licenseno != "" && starttime == "" && endtime == "") searchtype = 1;
        else if (licenseno != "" && starttime != "" && endtime == "") searchtype = 2;
        else if (licenseno != "" && starttime == "" && endtime != "") searchtype = 3;
        else if (licenseno != "" && starttime != "" && endtime != "") searchtype = 4;
        else if (licenseno == "" && starttime != "" && endtime == "") searchtype = 5;
        else if (licenseno == "" && starttime == "" && endtime != "") searchtype = 6;
        else if (licenseno == "" && starttime != "" && endtime != "") searchtype = 7;

        DataSet ds = new DataSet();
        ds = operation.SelectBillbyInserttime(status, licenseno, startdt, enddt, searchtype);

        DataSet dslog = new DataSet();

        //遍历DateSet，并获取每条数据的日志列表
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            DataSet temp_ds = operation.SelectLog(ds.Tables[0].Rows[i]["pk_id"].ToString());

            temp_ds.Tables[0].Columns.Add("订单号", typeof(System.String));
            temp_ds.Tables[0].Columns.Add("动作", typeof(System.String));
            for (int j = 0; j < temp_ds.Tables[0].Rows.Count; j++)
            {
                temp_ds.Tables[0].Rows[j]["订单号"] = ds.Tables[0].Rows[i]["billno"].ToString();
                if (temp_ds.Tables[0].Rows[j]["status"].ToString() == "0" ) temp_ds.Tables[0].Rows[j]["动作"] = "导入系统";
                else if (temp_ds.Tables[0].Rows[j]["status"].ToString() == "1") temp_ds.Tables[0].Rows[j]["动作"] = "被认领";
                else if (temp_ds.Tables[0].Rows[j]["status"].ToString() == "2") temp_ds.Tables[0].Rows[j]["动作"] = "下单成功";
                else if (temp_ds.Tables[0].Rows[j]["status"].ToString() == "3") temp_ds.Tables[0].Rows[j]["动作"] = "下单失败";
                else if (temp_ds.Tables[0].Rows[j]["status"].ToString() == "4") temp_ds.Tables[0].Rows[j]["动作"] = "改期";
                else if (temp_ds.Tables[0].Rows[j]["status"].ToString() == "5") temp_ds.Tables[0].Rows[j]["动作"] = "催单";
            }

            dslog.Merge(temp_ds);           
        }

        dslog.Tables[0].Columns.Remove("status");
        dslog.Tables[0].Columns.Remove("inserttimeforhis");
        dslog.Tables[0].Columns.Remove("operatetimeforhis");

        dslog.Tables[0].Columns[0].ColumnName = "日志编号";
        dslog.Tables[0].Columns[1].ColumnName = "订单编号";
        dslog.Tables[0].Columns[2].ColumnName = "操作人";
        dslog.Tables[0].Columns[3].ColumnName = "操作日期";


        DateTime dt = DateTime.Now;
        string dts = dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString() + ".xls";
        ExportResult(dslog, dts);
    }
}