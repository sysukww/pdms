using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class createBill : System.Web.UI.Page
{
    Operation operation = new Operation();
    DateTime dt = DateTime.Now;



    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            txtPkID.Text = ToolBox.CreatePkID();      //生成主键码
            labStatus.Text = "待分配";
            txtPolicyafterfee.Text = "第一次";

        }
    }

    /**
     * 保存新增订单
     * 1、获取页面数据；
     * 2、检测是否曾经存在相关记录；
     * 3、插入数据到数据表中同时添加日志。
     */
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet(); 
        DateTime opt = DateTime.Now;                        //当前时间

        string pk_id = txtPkID.Text.Trim();                 //获取主键
        string billno = txtBillno.Text.Trim();              //获取订单号
        string licenseno = txtLicenseno.Text.Trim();        //获取车牌号
        int status = 0;                                     //新增状态默认为0（待认领）
        string policyafterfee = txtPolicyafterfee.Text.Trim();     //获取见费出单字段
        string mororaft = DropDownList1.Text.Trim();               //上午还是下午
        string remark = txtRemark.Text.Trim();                     //获取备注
        DateTime chargedate;                                           //收费时间
        DateTime.TryParse(txtChargetime.Text.Trim(), out chargedate);  

        //查询是否有相同订单号的记录
        ds = operation.SelectBillbyBillno(txtBillno.Text.Trim());     //查询在数据库中是否有过记录
        int count = ds.Tables["pdmsbill"].Rows.Count;       //通过查询返回的记录数判断是否有过相同订单号的订单号记录
        if (count != 0)
        {
            Response.Write("<script>alert('数据库中已有相同订单号的记录，请核对订单后再填写！');</script> ");
            return;//有相同订单号,返回
        }


        //保存订单
        operation.InsertBill(pk_id, billno, licenseno, status, policyafterfee, chargedate, mororaft, 0, remark, opt, opt, "");    //保存订单
        operation.InsertLog(ToolBox.CreatePkID(), pk_id, 0, "", opt, opt, opt);                                                  //添加日志     

        WebMessageBox.Show("成功插入订单", "listBill.aspx");

    }


    protected void BtnClose_Click(object sender, EventArgs e)
    {
        WebMessageBox.Show("放弃编辑！", "listBill.aspx");
    }

}