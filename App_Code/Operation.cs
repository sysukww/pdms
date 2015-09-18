using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Data.SqlClient;


/// <summary>
/// Operation 电销保单配送系统（封装所有业务方法）
/// 订单表、订单验车照表以及订单日志表的增删查改以及分页显示的操作类
/// </summary>
public class Operation
{
    public Operation()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    DataBase data = new DataBase();


    /**
     * 模块一：订单表管理
     */

    #region  保单订单新增
    /// <summary>
    /// 新增保单订单
    /// 功能：用于支持上层导入功能以及单个保单订单的新增
    /// </summary>
    /// <param name="pk_id">主键</param>
    /// <param name="billno">订单号</param>
    /// <param name="licenseno">车牌号</param>
    /// <param name="status">状态（0：待分配（默认），1：已认领，2：成功，3：失败 ）</param>
    /// <param name="policyafterfee">见费出单（第一次（默认））</param>
    /// <param name="chargedate">收费时间</param>
    /// <param name="mororaft">上午下午</param>
    /// <param name="hurryup">催单次数</param>
    /// <param name="remark">备注</param>
    /// <param name="inserttimeforhis">插入时间</param>
    /// <param name="operatetimeforhis">更新时间</param>
    /// <param name="operatorcode">配送员代码</param>
    public void InsertBill(string pk_id, string billno, string licenseno, int status, string policyafterfee, DateTime chargedate,
        string mororaft, int hurryup, string remark, DateTime inserttimeforhis, DateTime operatetimeforhis, string operatorcode)
    {
        SqlParameter[] parms ={ 
            data.MakeInParam("@pk_id",SqlDbType.VarChar, 50, pk_id),
            data.MakeInParam("@billno",SqlDbType.VarChar, 50, billno),
            data.MakeInParam("@licenseno",SqlDbType.VarChar, 20, licenseno),
            data.MakeInParam("@status",SqlDbType.Int, 0, status),    
            data.MakeInParam("@policyafterfee",SqlDbType.VarChar,20, policyafterfee),
            data.MakeInParam("@chargedate",SqlDbType.VarChar, 0, chargedate),
            data.MakeInParam("@mororaft",SqlDbType.VarChar, 4, mororaft),
            data.MakeInParam("@hurryup",SqlDbType.Int, 0, hurryup),
            data.MakeInParam("@remark", SqlDbType.VarChar, 255, remark),
            data.MakeInParam("@inserttimeforhis", SqlDbType.DateTime, 0, inserttimeforhis),
            data.MakeInParam("@operatetimeforhis", SqlDbType.DateTime, 0, operatetimeforhis),
            data.MakeInParam("@operatorcode", SqlDbType.Char, 10, operatorcode)
        };
        int i = data.RunProc("INSERT INTO pdmsbill " + 
            "(pk_id, billno, licenseno, status, policyafterfee, chargedate, mororaft, hurryup, remark, inserttimeforhis, operatetimeforhis, operatorcode) VALUES " + 
            "(@pk_id, @billno,@licenseno,@status, @policyafterfee, @chargedate, @mororaft, @hurryup, @remark, @inserttimeforhis, @operatetimeforhis, @operatorcode)", parms);
    }
    #endregion

    #region  保单订单修改
    /// <summary>
    /// 修改保单订单
    /// 功能：能够修改对应订单号除主键以外的任何一项
    /// </summary>
    /// <param name="pk_id">主键</param>
    /// <param name="billno">订单号</param>
    /// <param name="licenseno">车牌号</param>
    /// <param name="status">状态（0：待分配（默认），1：已认领，2：成功，3：失败 ）</param>
    /// <param name="policyafterfee">见费出单（第一次（默认））</param>
    /// <param name="chargedate">收费时间</param>
    /// <param name="mororaft">上午下午</param>
    /// <param name="hurryup">催单次数</param>
    /// <param name="remark">备注</param>
    /// <param name="operatetimeforhis">更改时间</param>
    /// <param name="operatorcode">配送员代码</param>
    public void UpdateBill(string pk_id, string billno, string licenseno, int status, string policyafterfee, DateTime chargedate,
        string mororaft, int hurryup, string remark, DateTime operatetimeforhis, string operatorcode)
    {
        int i;

        i = data.RunProc("UPDATE pdmsbill SET billno = '" + billno + "', licenseno = '" + licenseno + "', status=" + status + 
            ", policyafterfee='" + policyafterfee + "', chargedate= '" + chargedate + "', mororaft='" + mororaft +
            "', hurryup = "+ hurryup + ", remark = '" + remark +
            "', operatetimeforhis='" + operatetimeforhis + "', operatorcode='" + operatorcode + "' WHERE ( pk_id = '" + pk_id + "')");
    }
    /// <summary>
    /// 修改保单订单
    /// 功能：能够修改对应订单号的其余项目
    /// </summary>
    /// <param name="pk_id">主键</param>
    /// <param name="billno">订单号</param>
    /// <param name="licenseno">车牌号</param>
    /// <param name="status">状态（0：待分配（默认），1：已认领，2：成功，3：失败 ）</param>
    /// <param name="policyafterfee">见费出单（第一次（默认））</param>
    /// <param name="chargedate">收费时间</param>
    /// <param name="mororaft">上午下午</param>
    /// <param name="hurryup">催单次数</param>
    /// <param name="remark">备注</param>
    /// <param name="operatetimeforhis">更改时间</param>
    /// <param name="operatorcode">配送员代码</param>
    public void UpdateBillbyBillno(string pk_id, string billno, string licenseno, int status, string policyafterfee, DateTime chargedate,
        string mororaft, int hurryup, string remark, DateTime operatetimeforhis, string operatorcode)
    {
        int i;

        i = data.RunProc("UPDATE pdmsbill SET pk_id = '" + pk_id + "', licenseno = '" + licenseno + "', status=" + status +
            ", policyafterfee='" + policyafterfee + "', chargedate= '" + chargedate + "', mororaft='" + mororaft +
            "', hurryup = " + hurryup + ", remark = '" + remark +
            "', operatetimeforhis='" + operatetimeforhis + "', operatorcode='" + operatorcode + "' WHERE ( billno = '" + billno + "')");
    }
    /// <summary>
    /// 修改保单订单
    /// 功能：催单使用的修改
    /// </summary>
    /// <param name="pk_id">主键</param>
    /// <param name="hurryup">催单次数</param>
    /// <param name="remark">备注</param>
    /// <param name="operatetimeforhis">更改时间</param>
    public void UpdateBill(string pk_id,
        int hurryup, string remark, DateTime operatetimeforhis)
    {
        int i;

        i = data.RunProc("UPDATE pdmsbill SET hurryup = " + hurryup + ", remark = '" + remark +
            "', operatetimeforhis='" + operatetimeforhis + "' WHERE ( pk_id = '" + pk_id + "')");
    }
    #endregion

    #region  配送单删除
    /// <summary>
    /// 删除指定主键号的保单订单
    /// </summary>
    /// <param name="pk_id">保单订单主键</param>
    public void DeleteBill(string pk_id)
    {
        int d = data.RunProc("Delete from pdmsbill where pk_id='" + pk_id + "'");
    }

    #endregion

    #region  配送单查询
    /// <summary>
    /// 查询所有配送单订单
    /// </summary>
    /// <returns>返回查询结果DataSet数据集</returns>
    public DataSet SelectBill()
    {
        return data.RunProcReturn("SELECT * FROM pdmsbill ORDER BY operatetimeforhis DESC","pdmsbill");
    }
    /// <summary>
    /// 按配送单的状态进行查询
    /// </summary>
    /// <param name="status">状态（0：待分配（默认），1：已认领，2：成功，3：失败 ）</param>
    /// <returns>返回查询结果DataSet数据集</returns>
    public DataSet SelectBill(int status)
    {
        SqlParameter[] parms ={
            data.MakeInParam("@status", SqlDbType.Int, 0, status )};
        return data.RunProcReturn("SELECT * FROM pdmsbill where status=@status ORDER BY operatetimeforhis DESC", parms, "pdmsbill");
    }
    /// <summary>
    /// 按配送单的插入时间进行查询
    /// </summary>
    /// <param name="time">查询时间</param>
    /// <returns>返回查询结果DataSet数据集</returns>
    public DataSet SelectBill(DateTime time)
    {
        DateTime ntime = time.AddDays(1);
        SqlParameter[] parms ={
            data.MakeInParam("@time", SqlDbType.DateTime, 0, time ),
            data.MakeInParam("@ntime", SqlDbType.DateTime, 0, ntime )             
                              };
        return data.RunProcReturn("SELECT * FROM pdmsbill where inserttimeforhis between @time and @ntime ORDER BY inserttimeforhis DESC", parms, "pdmsbill");
    }
    /// <summary>
    /// 按配送单的状态/查询时间/车牌号进行多条件查询
    /// </summary>
    /// <param name="status">状态（0：待分配（默认），1：已认领，2：成功，3：失败 ）</param>
    /// <param name="licenseno">车牌号</param>
    /// <param name="starttime">查询开始时间</param>
    /// <param name="endtime">查询结束时间</param>
    /// <param name="searchtype">查询类型</param>
    /// <returns>返回查询结果DataSet数据集</returns>
    public DataSet SelectBill(int status, string licenseno, DateTime starttime, DateTime endtime, int searchtype)
    {
        /*
        SqlParameter[] parms ={
            data.MakeInParam("@status", SqlDbType.Int, 0, status ),
            data.MakeInParam("@licenseno", SqlDbType.VarChar, 50, licenseno),
            data.MakeInParam("starttime", SqlDbType.DateTime, 0, starttime),
            data.MakeInParam("endtime", SqlDbType.DateTime, 0, endtime)};    */

        if (status == -1)
        {
            if (searchtype == 0 )
            {
                return data.RunProcReturn("SELECT * FROM pdmsbill ORDER BY operatetimeforhis DESC", "pdmsbill");
            }
            else if (searchtype == 1)
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@licenseno",SqlDbType.VarChar, 50 ,licenseno)};
                return data.RunProcReturn("SELECT * FROM pdmsbill where licenseno = @licenseno ORDER BY operatetimeforhis DESC", parms, "pdmsbill");
            }
            else if (searchtype == 2)
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@licenseno",SqlDbType.VarChar, 50 ,licenseno),
                    data.MakeInParam("@starttime",SqlDbType.DateTime, 0 ,starttime)};
                return data.RunProcReturn("SELECT * FROM pdmsbill where licenseno = @licenseno and operatetimeforhis >= @starttime ORDER BY operatetimeforhis DESC", parms, "pdmsbill");
            }
            else if (searchtype == 3)
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@licenseno",SqlDbType.VarChar, 50 ,licenseno),
                    data.MakeInParam("@endtime",SqlDbType.DateTime, 0 ,endtime)};
                return data.RunProcReturn("SELECT * FROM pdmsbill where licenseno = @licenseno and operatetimeforhis <= @endtime ORDER BY operatetimeforhis DESC", parms, "pdmsbill");
            }
            else if (searchtype == 4)
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@licenseno",SqlDbType.VarChar, 50 ,licenseno),
                    data.MakeInParam("@starttime",SqlDbType.DateTime, 0 ,starttime),
                    data.MakeInParam("@endtime",SqlDbType.DateTime, 0 ,endtime)};
                return data.RunProcReturn("SELECT * FROM pdmsbill where licenseno = @licenseno and operatetimeforhis between @starttime and @endtime ORDER BY operatetimeforhis DESC", parms, "pdmsbill");
            }
            else if (searchtype == 5)
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@starttime",SqlDbType.DateTime, 0 ,starttime)};
                return data.RunProcReturn("SELECT * FROM pdmsbill where operatetimeforhis >= @starttime ORDER BY operatetimeforhis DESC", parms, "pdmsbill");
            }
            else if (searchtype == 6)
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@endtime",SqlDbType.DateTime, 0 ,endtime)};
                return data.RunProcReturn("SELECT * FROM pdmsbill where operatetimeforhis <= @endtime ORDER BY operatetimeforhis DESC", parms, "pdmsbill");
            }
            else if (searchtype == 7)
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@starttime",SqlDbType.DateTime, 0 ,starttime),
                    data.MakeInParam("@endtime",SqlDbType.DateTime, 0 ,endtime)};
                return data.RunProcReturn("SELECT * FROM pdmsbill where operatetimeforhis between @starttime and @endtime ORDER BY operatetimeforhis DESC", parms, "pdmsbill");
            }
            else
            {
                return data.RunProcReturn("SELECT * FROM pdmsbill ORDER BY operatetimeforhis DESC", "pdmsbill");
            }
        }
        else
        {
            if (searchtype == 0)
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@status", SqlDbType.Int, 0, status )};
                return data.RunProcReturn("SELECT * FROM pdmsbill where status = @status ORDER BY operatetimeforhis DESC", parms, "pdmsbill");
            }
            else if (searchtype == 1)
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@status",SqlDbType.Int, 0 ,status ),
                    data.MakeInParam("@licenseno",SqlDbType.VarChar, 50 ,licenseno)};
                return data.RunProcReturn("SELECT * FROM pdmsbill where status = @status and licenseno = @licenseno ORDER BY operatetimeforhis DESC", parms, "pdmsbill");
            }
            else if (searchtype == 2)
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@status",SqlDbType.Int, 0 ,status ),
                    data.MakeInParam("@licenseno",SqlDbType.VarChar, 50 ,licenseno),
                    data.MakeInParam("@starttime",SqlDbType.DateTime, 0 ,starttime)};
                return data.RunProcReturn("SELECT * FROM pdmsbill where status = @status and licenseno = @licenseno and operatetimeforhis >= @starttime ORDER BY operatetimeforhis DESC", parms, "pdmsbill");
            }
            else if (searchtype == 3)
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@status",SqlDbType.Int, 0 ,status ),
                    data.MakeInParam("@licenseno",SqlDbType.VarChar, 50 ,licenseno),
                    data.MakeInParam("@endtime",SqlDbType.DateTime, 0 ,endtime)};
                return data.RunProcReturn("SELECT * FROM pdmsbill where status = @status and licenseno = @licenseno and operatetimeforhis <= @endtime ORDER BY operatetimeforhis DESC", parms, "pdmsbill");
            }
            else if (searchtype == 4)
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@status",SqlDbType.Int, 0 ,status ),
                    data.MakeInParam("@licenseno",SqlDbType.VarChar, 50 ,licenseno),
                    data.MakeInParam("@starttime",SqlDbType.DateTime, 0 ,starttime),
                    data.MakeInParam("@endtime",SqlDbType.DateTime, 0 ,endtime)};
                return data.RunProcReturn("SELECT * FROM pdmsbill where status = @status and licenseno = @licenseno and operatetimeforhis between @starttime and @endtime ORDER BY operatetimeforhis DESC", parms, "pdmsbill");
            }
            else if (searchtype == 5)
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@status",SqlDbType.Int, 0 ,status ),
                    data.MakeInParam("@starttime",SqlDbType.DateTime, 0 ,starttime)};
                return data.RunProcReturn("SELECT * FROM pdmsbill where status = @status and operatetimeforhis >= @starttime ORDER BY operatetimeforhis DESC", parms, "pdmsbill");
            }
            else if (searchtype == 6)
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@status",SqlDbType.Int, 0 ,status ),
                    data.MakeInParam("@endtime",SqlDbType.DateTime, 0 ,endtime)};
                return data.RunProcReturn("SELECT * FROM pdmsbill where status = @status and operatetimeforhis <= @endtime ORDER BY operatetimeforhis DESC", parms, "pdmsbill");
            }
            else if (searchtype == 7)
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@status",SqlDbType.Int, 0 ,status ),
                    data.MakeInParam("@starttime",SqlDbType.DateTime, 0 ,starttime),
                    data.MakeInParam("@endtime",SqlDbType.DateTime, 0 ,endtime)};
                return data.RunProcReturn("SELECT * FROM pdmsbill where status = @status and operatetimeforhis between @starttime and @endtime ORDER BY operatetimeforhis DESC", parms, "pdmsbill");
            }
            else
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@status", SqlDbType.Int, 0, status )};
                return data.RunProcReturn("SELECT * FROM pdmsbill where status = @status ORDER BY operatetimeforhis DESC", parms, "pdmsbill");
            }
        }
    }
    /// <summary>
    /// 按配送单的状态/查询时间/车牌号进行多条件查询
    /// </summary>
    /// <param name="status">状态（0：待分配（默认），1：已认领，2：成功，3：失败 ）</param>
    /// <param name="licenseno">车牌号</param>
    /// <param name="starttime">查询开始时间</param>
    /// <param name="endtime">查询结束时间</param>
    /// <param name="searchtype">查询类型</param>
    /// <returns>返回查询结果DataSet数据集</returns>
    public DataSet SelectBillbyInserttime(int status, string licenseno, DateTime starttime, DateTime endtime, int searchtype)
    {
        /*
        SqlParameter[] parms ={
            data.MakeInParam("@status", SqlDbType.Int, 0, status ),
            data.MakeInParam("@licenseno", SqlDbType.VarChar, 50, licenseno),
            data.MakeInParam("starttime", SqlDbType.DateTime, 0, starttime),
            data.MakeInParam("endtime", SqlDbType.DateTime, 0, endtime)};    */

        if (status == -1)
        {
            if (searchtype == 0)
            {
                return data.RunProcReturn("SELECT * FROM pdmsbill ORDER BY inserttimeforhis DESC", "pdmsbill");
            }
            else if (searchtype == 1)
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@licenseno",SqlDbType.VarChar, 50 ,licenseno)};
                return data.RunProcReturn("SELECT * FROM pdmsbill where licenseno = @licenseno ORDER BY inserttimeforhis DESC", parms, "pdmsbill");
            }
            else if (searchtype == 2)
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@licenseno",SqlDbType.VarChar, 50 ,licenseno),
                    data.MakeInParam("@starttime",SqlDbType.DateTime, 0 ,starttime)};
                return data.RunProcReturn("SELECT * FROM pdmsbill where licenseno = @licenseno and inserttimeforhis >= @starttime ORDER BY inserttimeforhis DESC", parms, "pdmsbill");
            }
            else if (searchtype == 3)
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@licenseno",SqlDbType.VarChar, 50 ,licenseno),
                    data.MakeInParam("@endtime",SqlDbType.DateTime, 0 ,endtime)};
                return data.RunProcReturn("SELECT * FROM pdmsbill where licenseno = @licenseno and inserttimeforhis <= @endtime ORDER BY inserttimeforhis DESC", parms, "pdmsbill");
            }
            else if (searchtype == 4)
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@licenseno",SqlDbType.VarChar, 50 ,licenseno),
                    data.MakeInParam("@starttime",SqlDbType.DateTime, 0 ,starttime),
                    data.MakeInParam("@endtime",SqlDbType.DateTime, 0 ,endtime)};
                return data.RunProcReturn("SELECT * FROM pdmsbill where licenseno = @licenseno and inserttimeforhis between @starttime and @endtime ORDER BY inserttimeforhis DESC", parms, "pdmsbill");
            }
            else if (searchtype == 5)
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@starttime",SqlDbType.DateTime, 0 ,starttime)};
                return data.RunProcReturn("SELECT * FROM pdmsbill where inserttimeforhis >= @starttime ORDER BY inserttimeforhis DESC", parms, "pdmsbill");
            }
            else if (searchtype == 6)
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@endtime",SqlDbType.DateTime, 0 ,endtime)};
                return data.RunProcReturn("SELECT * FROM pdmsbill where inserttimeforhis <= @endtime ORDER BY inserttimeforhis DESC", parms, "pdmsbill");
            }
            else if (searchtype == 7)
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@starttime",SqlDbType.DateTime, 0 ,starttime),
                    data.MakeInParam("@endtime",SqlDbType.DateTime, 0 ,endtime)};
                return data.RunProcReturn("SELECT * FROM pdmsbill where inserttimeforhis between @starttime and @endtime ORDER BY inserttimeforhis DESC", parms, "pdmsbill");
            }
            else
            {
                return data.RunProcReturn("SELECT * FROM pdmsbill ORDER BY inserttimeforhis DESC", "pdmsbill");
            }
        }
        else
        {
            if (searchtype == 0)
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@status", SqlDbType.Int, 0, status )};
                return data.RunProcReturn("SELECT * FROM pdmsbill where status = @status ORDER BY inserttimeforhis DESC", parms, "pdmsbill");
            }
            else if (searchtype == 1)
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@status",SqlDbType.Int, 0 ,status ),
                    data.MakeInParam("@licenseno",SqlDbType.VarChar, 50 ,licenseno)};
                return data.RunProcReturn("SELECT * FROM pdmsbill where status = @status and licenseno = @licenseno ORDER BY inserttimeforhis DESC", parms, "pdmsbill");
            }
            else if (searchtype == 2)
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@status",SqlDbType.Int, 0 ,status ),
                    data.MakeInParam("@licenseno",SqlDbType.VarChar, 50 ,licenseno),
                    data.MakeInParam("@starttime",SqlDbType.DateTime, 0 ,starttime)};
                return data.RunProcReturn("SELECT * FROM pdmsbill where status = @status and licenseno = @licenseno and inserttimeforhis >= @starttime ORDER BY inserttimeforhis DESC", parms, "pdmsbill");
            }
            else if (searchtype == 3)
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@status",SqlDbType.Int, 0 ,status ),
                    data.MakeInParam("@licenseno",SqlDbType.VarChar, 50 ,licenseno),
                    data.MakeInParam("@endtime",SqlDbType.DateTime, 0 ,endtime)};
                return data.RunProcReturn("SELECT * FROM pdmsbill where status = @status and licenseno = @licenseno and inserttimeforhis <= @endtime ORDER BY inserttimeforhis DESC", parms, "pdmsbill");
            }
            else if (searchtype == 4)
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@status",SqlDbType.Int, 0 ,status ),
                    data.MakeInParam("@licenseno",SqlDbType.VarChar, 50 ,licenseno),
                    data.MakeInParam("@starttime",SqlDbType.DateTime, 0 ,starttime),
                    data.MakeInParam("@endtime",SqlDbType.DateTime, 0 ,endtime)};
                return data.RunProcReturn("SELECT * FROM pdmsbill where status = @status and licenseno = @licenseno and inserttimeforhis between @starttime and @endtime ORDER BY inserttimeforhis DESC", parms, "pdmsbill");
            }
            else if (searchtype == 5)
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@status",SqlDbType.Int, 0 ,status ),
                    data.MakeInParam("@starttime",SqlDbType.DateTime, 0 ,starttime)};
                return data.RunProcReturn("SELECT * FROM pdmsbill where status = @status and inserttimeforhis >= @starttime ORDER BY inserttimeforhis DESC", parms, "pdmsbill");
            }
            else if (searchtype == 6)
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@status",SqlDbType.Int, 0 ,status ),
                    data.MakeInParam("@endtime",SqlDbType.DateTime, 0 ,endtime)};
                return data.RunProcReturn("SELECT * FROM pdmsbill where status = @status and inserttimeforhis <= @endtime ORDER BY inserttimeforhis DESC", parms, "pdmsbill");
            }
            else if (searchtype == 7)
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@status",SqlDbType.Int, 0 ,status ),
                    data.MakeInParam("@starttime",SqlDbType.DateTime, 0 ,starttime),
                    data.MakeInParam("@endtime",SqlDbType.DateTime, 0 ,endtime)};
                return data.RunProcReturn("SELECT * FROM pdmsbill where status = @status and inserttimeforhis between @starttime and @endtime ORDER BY inserttimeforhis DESC", parms, "pdmsbill");
            }
            else
            {
                SqlParameter[] parms ={
                    data.MakeInParam("@status", SqlDbType.Int, 0, status )};
                return data.RunProcReturn("SELECT * FROM pdmsbill where status = @status ORDER BY inserttimeforhis DESC", parms, "pdmsbill");
            }
        }
    }
    /// <summary>
    /// 按配送单主键查询订单
    /// </summary>
    /// <param name="pk_id">保单订单主键</param>
    /// <returns>返回查询结果DataSet数据集</returns>
    public DataSet SelectBill(string pk_id)
    {
        SqlParameter[] parms ={ 
            data.MakeInParam("@pk_id", SqlDbType.VarChar, 50, pk_id)
        };
        return data.RunProcReturn("SELECT * FROM pdmsbill where pk_id=@pk_id", parms, "pdmsbill");
    }
    /// <summary>
    /// 按配送单订单号查询订单
    /// </summary>
    /// <param name="billno">保单订单号</param>
    /// <returns>返回查询结果DataSet数据集</returns>
    public DataSet SelectBillbyBillno(string billno)
    {
        SqlParameter[] parms ={ 
            data.MakeInParam("@billno", SqlDbType.VarChar, 50, billno)
        };
        return data.RunProcReturn("SELECT * FROM pdmsbill where billno=@billno", parms, "pdmsbill");
    }
    #endregion

    /**
     * 模块二：订单日志表管理
     */
     
    #region 新增订单日志

    /// <summary>
    /// 新增订单日志
    /// </summary>
    /// <param name="pk_id">主键</param>
    /// <param name="mst_id">订单表ID</param>
    /// <param name="status">订单状态(0：导入系统；1：被认领；2：下单成功；3：下单失败；4：改期； 5：催单)</param>
    /// <param name="operatorcode">操作员代码</param>
    /// <param name="operatedate">操作时间</param>
    /// <param name="inserttimeforhis">插入时间</param>
    /// <param name="operatetimeforhis">更新时间</param>
    public void InsertLog(string pk_id, string mst_id, int status, string operatorcode, DateTime operatedate, DateTime inserttimeforhis, DateTime operatetimeforhis)
    {
        SqlParameter[] parms ={ 
            data.MakeInParam("@pk_id",SqlDbType.VarChar,50,pk_id),
            data.MakeInParam("@mst_id",SqlDbType.VarChar,50,mst_id),
            data.MakeInParam("@status",SqlDbType.Int,0,status),
            data.MakeInParam("@operatorcode",SqlDbType.Char, 10, operatorcode),
            data.MakeInParam("@operatedate",SqlDbType.DateTime, 0, operatedate),
            data.MakeInParam("@inserttimeforhis",SqlDbType.DateTime,0,inserttimeforhis),
            data.MakeInParam("@operatetimeforhis",SqlDbType.DateTime,0,inserttimeforhis)      
        };
        int i = data.RunProc("INSERT INTO pdmsbilllog (pk_id, mst_id, status ,operatorcode, operatedate, inserttimeforhis, operatetimeforhis) VALUES" +
            " (@pk_id, @mst_id,@status,@operatorcode,@operatedate,@inserttimeforhis,@operatetimeforhis)", parms);
    }
    #endregion

    #region  删除订单日志
    /// <summary>
    /// 删除订单日志
    /// </summary>
    /// <param name="mst_id">要删除的日志的主键</param>
    public void DeleteLog(string mst_id)
    {
        int d = data.RunProc("Delete from pdmsbilllog where mst_id='" + mst_id + "'");
    }
    #endregion

    #region  查询订单日志
    /// <summary>
    /// 显示所有的订单日志
    /// </summary>
    /// <returns>返回DataSet结果集</returns>
    public DataSet SelectLog()
    {
        return data.RunProcReturn("Select * from pdmsbilllog order by operatedate desc", "pdmsbilllog");
    }
    /// <summary>
    /// 根据订单日志主键查询日志
    /// </summary>
    /// <param name="mst_id">订单号</param>
    /// <returns>返回DataSet结果集</returns>
    public DataSet SelectLog(string mst_id)
    {
        return data.RunProcReturn("Select * from pdmsbilllog where mst_id='" + mst_id + "' ORDER BY operatedate", "pdmsbilllog");
    }
    /// <summary>
    /// 按照时间段查询订单日志 
    /// </summary>
    /// <param name="starttime">查询的开始时间</param>
    /// <param name="endtime">查询的结束时间</param>
    /// <returns>返回查询结果DataSet数据集</returns>
    public DataSet SelectLog(DateTime starttime, DateTime endtime)
    {
        SqlParameter[] parms ={ 
            data.MakeInParam("@starttime", SqlDbType.DateTime, 0, starttime),
            data.MakeInParam("@endtime", SqlDbType.DateTime, 0, endtime)
        };
        return data.RunProcReturn("SELECT * FROM pdmsbilllog where operatedate between @starttime and @endtime ORDER BY operatedate DESC", parms, "pdmsbilllog");
    }
    /// <summary>
    /// 仅仅按照结束时间或者是开始时间查询短信
    /// </summary>
    /// <param name="searchtime">查询的开始时间</param>
    /// <param name="nullstate">null的状态</param>
    /// <returns>返回查询结果DataSet数据集</returns>
    public DataSet SelectLog(DateTime searchtime, int nullstate)
    {
        SqlParameter[] parms ={ 
            data.MakeInParam("@searchtime", SqlDbType.DateTime, 0, searchtime)
        };
        if (nullstate == 0)
        {
            return data.RunProcReturn("SELECT * FROM pdmsbilllog where operatetime >= @searchtime and ORDER BY operatetime DESC", parms, "pdmsbilllog");
        }
        else
        {
            return data.RunProcReturn("SELECT * FROM pdmsbilllog where operatetime <= @searchtime and ORDER BY operatetime DESC", parms, "pdmsbillog");
        }
    }

    #endregion

    #region  修改订单日志
    /// <summary>
    /// 修改相关主键的订单日志
    /// </summary>
    /// <param name="pk_id">订单日志主键</param>
    /// <param name="status">订单日志状态</param>
    /// <param name="operatetimeforhis">订单日志更新时间</param>
    public void UpdateLog(string pk_id, int status, DateTime operatetimeforhis)
    {
        int i;

        i = data.RunProc("UPDATE lpprpmsmodel SET status = " + status + ", operatetimeforhis='" +operatetimeforhis+ "' WHERE (pk_id = '" + pk_id + "')");
    }
    #endregion

    #region  分页设置绑定
    /// <summary>
    /// 绑定DataList控件，并且设置分页
    /// </summary>
    /// <param name="infoKey">查询的关键字（如果为空，则查询所有）</param>
    /// <param name="currentPage">当前页</param>
    /// <param name="PageSize">每页显示数量</param>
    /// <returns>返回PagedDataSource对象</returns>
    public PagedDataSource PageDataListBind(string infoKey, int currentPage,int PageSize)
    {
        PagedDataSource pds = new PagedDataSource();
        pds.DataSource = SelectLog().Tables[0].DefaultView; //将查询结果绑定到分页数据源上。
        pds.AllowPaging = true;　　　　　　　　　　//允许分页
        pds.PageSize = PageSize;　　　　　　　　 　//设置每页显示的页数
        pds.CurrentPageIndex = currentPage - 1;　  //设置当前页
        return pds;
    }
    #endregion

    #region  获取配送人信息
    /// <summary>
    /// 通过配送员代码获取配送人信息
    /// </summary>
    /// <param name="operatorcode">配送人代码</param>
    /// <returns>返回查询结果DataSet数据集</returns>
    public DataSet SelectOperator(string operatorcode )
    {
        SqlParameter[] parms ={ 
            data.MakeInParam("@operatorcode", SqlDbType.VarChar, 20, operatorcode)
        };
        return data.RunProcReturn("SELECT * FROM pdmsoperator where operatorcode = @operatorcode", parms, "pdmsoperator");
    }
    #endregion


}
