using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class manageBill : System.Web.UI.Page
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
        //获取查询数据
        int status = Int32.Parse(DDStatus.SelectedIndex.ToString()) - 1;       //状态
        string licenseno = txtLicenseno.Text.ToString();                       //需要查询的车牌号
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


        GridView1.DataSource = operation.SelectBill(status, licenseno, startdt, enddt, searchtype);                       //显示所有订单
        GridView1.DataBind();
        //显示当前页数
        lblPageSum.Text = "当前页为　" + (GridView1.PageIndex + 1) + " / " + GridView1.PageCount + "　页";

        /*
        GridView1.DataSource = operation.SelectBill();      //按照订单的状态进行查询（0为待分配状态）
        GridView1.DataBind();
        //显示当前页数
       
        lblPageSum.Text = "当前页为　" + (GridView1.PageIndex + 1) + " / " + GridView1.PageCount + "　页";
         * 
         */
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
            e.Row.Cells[5].Text = StringFormat.Out(e.Row.Cells[5].Text, 16);
            e.Row.Cells[0].Text = StringFormat.Out(e.Row.Cells[0].Text, 5);
            //删除指定行数据时，弹出询问对话框
            //((LinkButton)(e.Row.Cells[11].Controls[0])).Attributes.Add("onclick", "return confirm('是否删除当前行数据！')");
        }
    }

    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        string id = GridView1.DataKeys[e.NewSelectedIndex].Value.ToString();
        //string id = GridView1.DataKeys[e.NewSelectedIndex].Value.ToString();
        //WebMessageBox.Show(GridView1.DataKeys[e.NewSelectedIndex].Value.ToString());    //调试使用
        Response.Write("<script>location.href='detailBill.aspx?id=" + id + "'</script>");
        //Response.Write("<script>history.go(-1)</script>");
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridViewBind();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
        Response.Write("<script>location.href='sendHurry.aspx?id=" + id + "'</script>");        
    }

    /**
     * 根据多条件查询订单号
     * 1、获取查询数据：车牌号，查询开始日期，查询结束日期以及状态；
     * 2、根据查询条件选择查询情况；
     * 3、调用operation进行查询。
     */
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        //获取查询数据
        int status = Int32.Parse(DDStatus.SelectedIndex.ToString()) - 1;       //状态
        string licenseno = txtLicenseno.Text.ToString();                       //需要查询的车牌号
        string starttime = txtStarttime.Text.ToString();
        string endtime = txtEndtime.Text.ToString();

        int searchtype = 0;                                                   //查询类型
        DateTime startdt;
        DateTime enddt;
        DateTime.TryParse(starttime, out startdt );
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


        GridView1.DataSource = operation.SelectBill(status, licenseno, startdt, enddt, searchtype);                       //显示所有订单
        GridView1.DataBind();
        //显示当前页数
        lblPageSum.Text = "当前页为　" + (GridView1.PageIndex + 1) + " / " + GridView1.PageCount + "　页";

        
    }


    protected void DDStatus_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //获取相关状态的订单号
        int selectedstatus = Int32.Parse(DDStatus.SelectedIndex.ToString())-1;
        txtEndtime.Text = "";
        txtLicenseno.Text = "";
        txtStarttime.Text = "";
        //选择相关订单
        if (selectedstatus == -1)
        {
            GridView1.DataSource = operation.SelectBill();                      //显示所有订单
            GridView1.DataBind();
        }
        else if (selectedstatus == 1)
        {
            GridView1.DataSource = operation.SelectBill(selectedstatus);         //通过订单状态选择订单
            GridView1.DataBind();
            
        }
        else
        {
            GridView1.DataSource = operation.SelectBill(selectedstatus);         //通过订单状态选择订单
            GridView1.DataBind();
        }

        //显示当前页数
        lblPageSum.Text = "当前页为　" + (GridView1.PageIndex + 1) + " / " + GridView1.PageCount + "　页";
    }

}