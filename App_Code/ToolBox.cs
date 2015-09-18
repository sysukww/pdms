using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// ToolBox 的摘要说明
/// </summary>
public class ToolBox
{
	public ToolBox()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    #region 生成主键
    /// <summary>
    /// 返回主键码
    /// </summary>
    /// <returns ></returns>
    public static string CreatePkID()
    {
        //今天时间
        DateTime dt = System.DateTime.Today;
        return dt.ToString("yyMMdd") +　System.Guid.NewGuid();
    }
    #endregion
}