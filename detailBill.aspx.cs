using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class detailBill : System.Web.UI.Page
{
    Operation operation = new Operation();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DataSet ds = operation.SelectBill(Request.QueryString["id"].ToString());     //根据短信编号查询短信

            txtPkID.Text = Request.QueryString["id"].ToString();                          //后台系统主键
            txtBillno.Text = ds.Tables[0].Rows[0][1].ToString();                          //订单号
            txtLicenseno.Text = ds.Tables[0].Rows[0][2].ToString();                       //车牌号
            if (ds.Tables[0].Rows[0][3].ToString() == "0")                                //订单状态
            {
                labStatus.Text = "待分配";
            }
            else if (ds.Tables[0].Rows[0][3].ToString() == "1")
            {
                labStatus.Text = "已认领";
            }
            else if (ds.Tables[0].Rows[0][3].ToString() == "2")
            {
                labStatus.Text = "成功";
            }
            else if (ds.Tables[0].Rows[0][3].ToString() == "3")
            {
                labStatus.Text = "失败";
            }
            txtPolicyafterfee.Text = ds.Tables[0].Rows[0][4].ToString();                   //见费出单
            string temptime = ds.Tables[0].Rows[0][5].ToString();                          
            string[] ct = temptime.Split(' ');        //获取收费时间
            txtChargetime.Text = ct[0].Replace('/', '-');
            txtMororaft.Text = ds.Tables[0].Rows[0][6].ToString();
            txtHurryup.Text = ds.Tables[0].Rows[0][7].ToString();                        //催单次数
            txtRemark.Text = ds.Tables[0].Rows[0][8].ToString();
            txtSendMan.Text = ds.Tables[0].Rows[0][11].ToString();
        }
    }

    protected void BtnClose_Click(object sender, EventArgs e)
    {
        WebMessageBox.Show("回到订单管理列表！", "manageBill.aspx");
    }

}