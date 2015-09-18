using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;


public partial class listLog : System.Web.UI.Page
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
        DataSet ds = operation.SelectBill(Request.QueryString["id"].ToString());
        labbillno.Text = ds.Tables[0].Rows[0][1].ToString();
        lablicenseno.Text = ds.Tables[0].Rows[0][2].ToString();

        GridView1.DataSource = operation.SelectLog(Request.QueryString["id"].ToString());
        GridView1.DataBind();
        lblPageSum.Text = "当前页为　" + (GridView1.PageIndex + 1) + " / " + GridView1.PageCount + "　页";
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[2].Text == "0")
            {
                e.Row.Cells[2].Text = "导入系统";
            }
            else if (e.Row.Cells[2].Text == "1") e.Row.Cells[2].Text = "被认领";
            else if (e.Row.Cells[2].Text == "2") e.Row.Cells[2].Text = "下单成功";
            else if (e.Row.Cells[2].Text == "3") e.Row.Cells[2].Text = "下单失败";
            else if (e.Row.Cells[2].Text == "4") e.Row.Cells[2].Text = "改期";
            else if (e.Row.Cells[2].Text == "5") e.Row.Cells[2].Text = "催单";
            //高亮显示指定行
            e.Row.Attributes.Add("onMouseOver", "Color=this.style.backgroundColor;this.style.backgroundColor='#FFF000'");
            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=Color;");
            //多余字　使用...显示
            e.Row.Cells[3].Text = StringFormat.Out(e.Row.Cells[3].Text, 18);
            e.Row.Cells[0].Text = StringFormat.Out(e.Row.Cells[0].Text, 15);
            e.Row.Cells[1].Text = StringFormat.Out(e.Row.Cells[1].Text, 15);

        }
    }

    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        string id = GridView1.DataKeys[e.NewSelectedIndex].Value.ToString();
        Response.Write("<script>location.href='descBill.aspx?id=" + id + "'</script>");
        //Response.Write("<script>history.go(-1)</script>");
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridViewBind();
    }



    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Write("<script>location.href='manageLog.aspx'</script>");
    }
}