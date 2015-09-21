using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;


public partial class listBill : System.Web.UI.Page
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
    /// 绑定待分配的订单到GridViev控件
    /// </summary>
    private void GridViewBind()
    {
        GridView1.DataSource = operation.SelectBill(DateTime.Today);      //按照订单的状态进行查询（0为待分配状态）
        GridView1.DataBind();
        txtStarttime.Value = DateTime.Today.ToString("yyyy/MM/dd");
        txtEndtime.Value = DateTime.Today.ToString("yyyy/MM/dd");
        txtEndtime.Text = txtEndtime.Value.ToString();
        txtStarttime.Text = txtStarttime.Value.ToString();
        //显示当前页数
        lblPageSum.Text = "当前页为　" + (GridView1.PageIndex + 1) + " / " + GridView1.PageCount + "　页";
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[3].Text == "0") e.Row.Cells[3].Text = "待分配";
            //高亮显示指定行
            e.Row.Attributes.Add("onMouseOver", "Color=this.style.backgroundColor;this.style.backgroundColor='#dddddd'");
            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=Color;");
            //多余字　使用...显示
            e.Row.Cells[0].Text = StringFormat.Out(e.Row.Cells[0].Text, 5);
            //删除指定行数据时，弹出询问对话框
            ((LinkButton)(e.Row.Cells[10].Controls[0])).Attributes.Add("onclick", "return confirm('是否删除当前行数据！')");
        }
    }

    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        string id = GridView1.DataKeys[e.NewSelectedIndex].Value.ToString();
        //string id = GridView1.DataKeys[e.NewSelectedIndex].Value.ToString();
        //WebMessageBox.Show(GridView1.DataKeys[e.NewSelectedIndex].Value.ToString());    //调试使用
        Response.Write("<script>location.href='descBill.aspx?id=" + id + "'</script>");
        Response.Write("<script>history.go(-1)</script>");
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        //GridViewBind();

        string licenseno = "";
        int status = 0;
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

    /**
     * 删除某一行的订单记录
     * 1、删除该记录的相关日志
     * 2、删除该订单记录
     */
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            //查通过相关订单的主键查找出订单号
            DataSet ds = operation.SelectBill(GridView1.DataKeys[e.RowIndex].Value.ToString());
            string billno = ds.Tables["pdmsbill"].Rows[0]["billno"].ToString();


            //通过订单号主键删除相关订单日志
            operation.DeleteLog(GridView1.DataKeys[e.RowIndex].Value.ToString());
            //删除订单记录
            operation.DeleteBill(GridView1.DataKeys[e.RowIndex].Value.ToString());
        }
        catch
        {
            WebMessageBox.Show("删除出错", "listBill.aspx");
        }
        //WebMessageBox.Show(GridView1.DataKeys[e.RowIndex].Value.ToString());    //调试使用
        GridViewBind();
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Write("<script language='javascript' type='text/javascript'>location.href='" + "createBill.aspx" + "'</script>");
        HttpContext.Current.Response.End();
    }


    /**
     * 点击导入订单表
     * 1、检测是否为xls后缀文件并将文件保存到虚拟的目录；
     * 2、检测文件内容是否符合要求，如有该有的字段，数据库表中是否有相同订单的记录以及时间是否符合时间的格式；
     * 3、检测逐条插入记录并添加日志。
     */
    protected void BtnImport_Click(object sender, EventArgs e)
    {
        /*
         * 导入xls文件，检测是否为xls文件并将文件保存到虚拟的目录
         */
        if (FileUpload1.HasFile == false)//HasFile用来检查FileUpload是否有指定文件
        {
            Response.Write("<script>alert('请您选择Excel文件')</script> ");
            return;//当无文件时,返回
        }
        string IsXls = System.IO.Path.GetExtension(FileUpload1.FileName).ToString().ToLower();//System.IO.Path.GetExtension获得文件的扩展名
        if (IsXls != ".xls")
        {
            Response.Write("<script>alert('只可以选择Excel文件')</script>");
            return;//当选择的不是Excel文件时,返回
        }
        string savePath = Server.MapPath(("upfiles\\") + "ImportData.xls");//Server.MapPath 获得虚拟服务器相对路径
        FileUpload1.SaveAs(savePath);              //SaveAs 将上传的文件内容保存在服务器上


        /*
         * 对保存在虚拟目录的xls文件内容进行格式检测
         */
        DataSet ds = ExcelSqlConnection(savePath, "ImportData");           //调用自定义方法
        DataRow[] dr = ds.Tables[0].Select();            //定义一个DataRow数组
        int rowsnum = ds.Tables[0].Rows.Count;
        if (rowsnum == 0)
        {
            Response.Write("<script>alert('Excel表为空表,无数据!')</script>");   //当Excel表为空时,对用户进行提示
        }
        else
        {
            //定义变量
            string billno;            //订单号
            string licenseno;         //车牌号
            //string status;            //订单状态
            string policyafterfee;    //见费出单
            string chargedate;        //收费时间
            string remark;            //备注
            DateTime dtTemp;
            for (int i = 0; i < dr.Length; i++)
            {
                try
                {   
                    //获取xls里头的值,检测是否有相关字段
                    billno = dr[i]["订单号"].ToString().Trim();
                    licenseno = dr[i]["车牌号"].ToString();
                    //status = dr[i]["订单状态"].ToString();
                    policyafterfee = dr[i]["见费出单"].ToString();
                    chargedate = dr[i]["收费日期"].ToString();
                    remark = dr[i]["备注"].ToString();

                    //检测表中是否已经存在相同订单号
                    DataSet testds = operation.SelectBillbyBillno(billno);     //查询在数据库中是否有过记录
                    int count = testds.Tables["pdmsbill"].Rows.Count;       //通过查询返回的记录数判断是否有过相同主键的订单号记录

                    if (count != 0)
                    {
                        Response.Write("<script>alert('数据库中已存在订单号为：" + billno + " 的记录，请重新核对文件后再重新导入！')</script>");
                        return;   //有重复数据，返回
                    }

                    //检测时间类字段是否符合格式
                    if (DateTime.TryParse(chargedate, out dtTemp) == false)
                    {
                        Response.Write("<script>alert('文件日期格式错误！')</script> ");
                        return;//当文件内容格式错误时,返回
                    }
                                       
                }
                catch
                {
                    Response.Write("<script>alert('文件格式错误！')</script> ");
                    return;//格式错误时,返回
                }
            }

            /*
             * 将通过检测的数据插入数据库
             * （不在上一步骤进行插入数据的原因是防止失败返回时显示列表中有文件中的数据，引起客户误解）
             */
            bool errorflag = false;                         //当同一个文件中存在重复订单号记录时为true
            List<string> errorlist = new List<string>();
            for (int i = 0; i < dr.Length; i++)
            {
                try
                {   //获取xls里头的值
                    billno = dr[i]["订单号"].ToString();
                    licenseno = dr[i]["车牌号"].ToString();
                    //status = dr[i]["订单状态"].ToString();
                    policyafterfee = dr[i]["见费出单"].ToString();
                    chargedate = dr[i]["收费日期"].ToString();
                    remark = dr[i]["备注"].ToString();
                    DateTime.TryParse(chargedate, out dtTemp);

                    DateTime opt = DateTime.Now;                        //数据插入时间以及修改时间

                    try
                    {
                        //逐条插入数据并创建日志
                        string billpk_id = ToolBox.CreatePkID();
                        operation.InsertBill(billpk_id, billno, licenseno, 0, policyafterfee, dtTemp, "上午", 0, remark, opt, opt, "");      //在订单号里添加数据
                        operation.InsertLog(ToolBox.CreatePkID(), billpk_id, 0, "", opt, opt, opt);    //插入日志   
                    }
                    catch
                    {
                        errorflag = true;
                        errorlist.Add(billno);
                        continue;
                    }

                }
                catch    //若插入过程中发生错误，返回消息并中断
                {
                    Response.Write("<script>alert('导入时出错！" + i + "');</script> ");
                    return;//格式错误时,返回
                }

            }
            if (errorflag)
            {
                string showstring = "";
                for (int i = 0; i < errorlist.Count; i++)
                {
                    showstring += errorlist[i] + ";\\n";
                }
                Response.Write("<script>alert('Excle表中有重复，已经导入首次出现订单记录，重复订单号为：\\n" + showstring + "');</script>");
            }
            else
            {
                Response.Write("<script>alert('Excle表导入成功!');</script>");
            }
            GridViewBind();
        }
    }



    #region 连接Excel  读取Excel数据   并返回DataSet数据集合
    /// <summary>
    /// 连接Excel  读取Excel数据   并返回DataSet数据集合
    /// </summary>
    /// <param name="filepath">Excel服务器路径</param>
    /// <param name="tableName">Excel表名称</param>
    /// <returns></returns>
    public static System.Data.DataSet ExcelSqlConnection(string filepath, string tableName)
    {
        string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1'";
        OleDbConnection ExcelConn = new OleDbConnection(strCon);
        try
        {
            string strCom = string.Format("SELECT * FROM [Sheet1$]");
            ExcelConn.Open();
            OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, ExcelConn);
            DataSet ds = new DataSet();
            myCommand.Fill(ds, "[" + tableName + "$]");
            ExcelConn.Close();
            return ds;
        }
        catch
        {
            ExcelConn.Close();
            return null;
        }
    }
    #endregion


    /**
     * 根据多条件查询订单号
     * 1、获取查询数据：车牌号，查询开始日期，查询结束日期以及状态；
     * 2、根据查询条件选择查询情况；
     * 3、调用operation进行查询。
     */
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        string licenseno="";
        int status = 0;
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

    protected void BtnToday_Click(object sender, EventArgs e)
    {
        GridViewBind();
    }

}