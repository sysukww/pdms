using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class sendHurry : System.Web.UI.Page
{
    Operation operation = new Operation();
    DateTime dt = DateTime.Now;

    SMSService.SMsgService sms = new SMSService.SMsgService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DataSet ds = operation.SelectBill(Request.QueryString["id"].ToString());     //根据短信编号查询短信

            txtPkID.Text = Request.QueryString["id"].ToString();
            if (ds.Tables[0].Rows[0][3].ToString() == "0")                                //订单状态
            {
                labStatus.Text = "待分配";
                saveBtn.Visible = false;
            }
            else if (ds.Tables[0].Rows[0][3].ToString() == "1")
            {
                labStatus.Text = "已认领";
                txtPhone.Enabled = true;
                txtRemark.Enabled = true;
            }
            else if (ds.Tables[0].Rows[0][3].ToString() == "2")
            {
                labStatus.Text = "成功";
                saveBtn.Visible = false;
            }
            else if (ds.Tables[0].Rows[0][3].ToString() == "3")
            {
                labStatus.Text = "失败";
                saveBtn.Visible = false;
            }
            txtBillno.Text = ds.Tables[0].Rows[0][1].ToString();
            txtMororaft.Text = ds.Tables[0].Rows[0][6].ToString();
            txtLicenseno.Text = ds.Tables[0].Rows[0][2].ToString();
            txtPolicyafterfee.Text = ds.Tables[0].Rows[0][4].ToString();
            txtChargetime.Text = ds.Tables[0].Rows[0][5].ToString();
            txtHurryup.Text = ds.Tables[0].Rows[0][7].ToString();
            txtRemark.Text = ds.Tables[0].Rows[0][8].ToString();
            txtOperator.Text = ds.Tables[0].Rows[0][11].ToString();
            if (txtOperator.Text.Trim() != "")
            {
                DataSet ds1 = operation.SelectOperator(txtOperator.Text.Trim());
                txtPhone.Text = ds1.Tables["pdmsoperator"].Rows[0][2].ToString();
            }
        }
    }


    protected void BtnSave_Click(object sender, EventArgs e)
    {
        DateTime opt = DateTime.Now;                        //数据插入时间以及修改时间
        if (labStatus.Text.ToString() == "已认领")
        {
            int num = Int32.Parse(txtHurryup.Text) + 1;
            operation.UpdateBill(txtPkID.Text.Trim(), num, txtRemark.Text.Trim(), opt);
            operation.InsertLog(ToolBox.CreatePkID(), txtPkID.Text.Trim(), 5, txtOperator.Text.Trim(), opt, opt, opt);
            string[] phone = new string[1];
            phone[0] = txtPhone.Text.Trim();
            sms.sendSM("isp", "isp", "pIcc4404", phone, txtRemark.Text.ToString(), 0);

            HttpContext.Current.Response.Write("<script language='javascript' type='text/javascript'>location.href='" + "manageBill.aspx" + "'</script>");
            HttpContext.Current.Response.End();
        }


    }


    protected void BtnClose_Click(object sender, EventArgs e)
    {
        WebMessageBox.Show("返回！", "manageBill.aspx");
    }

}