<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1">
    <ul>
        <li><a href="listBill.aspx">配送单列表</a></li>
        <li><a href="createBill.aspx">配送单新增</a></li>
        <li><a href="manageBill.aspx">配送查询</a></li>
        <li><a href="manageLog.aspx">日志管理</a></li>
    </ul>
</asp:Content>
