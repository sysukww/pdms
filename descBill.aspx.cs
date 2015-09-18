using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class descBill : System.Web.UI.Page
{
    Operation operation = new Operation();
    DateTime dt = DateTime.Now;



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DataSet ds = operation.SelectBill(Request.QueryString["id"].ToString());     //根据短信编号查询短信

            txtPkID.Text = Request.QueryString["id"].ToString();
            txtBillno.Text = ds.Tables[0].Rows[0][1].ToString();
            txtLicenseno.Text = ds.Tables[0].Rows[0][2].ToString();
            if (ds.Tables[0].Rows[0][3].ToString() == "0" )
            {
                labStatus.Text = "待出单";
            }
            txtPolicyafterfee.Text = ds.Tables[0].Rows[0][4].ToString();
            string temptime = ds.Tables[0].Rows[0][5].ToString();
            string[] ct = temptime.Split(' ');        //获取收费时间
            txtChargetime.Text = ct[0];
            DropDownList1.Text = ds.Tables[0].Rows[0][6].ToString();
            txtRemark.Text = ds.Tables[0].Rows[0][8].ToString();
        }
    }

    /*
     * 修改待认领订单的内容
     * 1、获取页面数据
     * 2、修改数据库相关记录（日志不进行相关改变）
     */
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        DateTime opt = DateTime.Now;                        //数据插入时间以及修改时间
        DataSet ds = new DataSet();

        string pk_id = txtPkID.Text.Trim();                 //获取订单的主键编码，在业务上是不变的
        string billno = txtBillno.Text.Trim();              //获取主键编码，在业务上不变
        string licenseno = txtLicenseno.Text.Trim();        //获取车牌号
        string policyafterfee = txtPolicyafterfee.Text.Trim();         //获取见费出单字段
        string remark = txtRemark.Text.Trim();                         //获取备注
        int status = 0;                                                //订单状态（待认领）
        string mororaft = DropDownList1.Text.Trim();                   //上午或是下午
        DateTime chargedate;
        DateTime.TryParse(txtChargetime.Text.Trim(),out chargedate);        //获取收费时间


        operation.UpdateBill(txtPkID.Text.Trim(), txtBillno.Text.Trim(), txtLicenseno.Text.Trim(), status, txtPolicyafterfee.Text.Trim(), chargedate, DropDownList1.Text.Trim(), 0, txtRemark.Text.Trim(), opt, "");

        saveLbl.Text = "已保存";
    }



    protected void BtnClose_Click(object sender, EventArgs e)
    {
        WebMessageBox.Show("回到待认领订单列表！", "listBill.aspx");
    }

}