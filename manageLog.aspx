<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="manageLog.aspx.cs" Inherits="manageLog" %>

<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxNavBar" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v12.1, Version=12.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v12.1, Version=12.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxDocking" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v12.1, Version=12.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxMenu" tagprefix="dx" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="contentType2" style="padding:10px;">

    <div style="width:920px; background-color:#fedcbd; padding:5px;font-family:Microsoft YaHei; font-size:small;">
        <table>
            <tr>
                <td align="left">
                    插入时间：
                </td>
                <td align="right">
                    <dx:ASPxDateEdit ID="txtStarttime" runat="server" style="float:left"></dx:ASPxDateEdit>
                    <dx:ASPxLabel ID="txtMiddle" runat="server" Text="-" style="float:left"></dx:ASPxLabel>
                    <dx:ASPxDateEdit ID="txtEndtime" runat="server" style="float:left"></dx:ASPxDateEdit>
                </td>
                <td style="width:10px;"></td>
                <td>
                    <dx:ASPxButton ID="btnSearch" runat="server" OnClick="BtnSearch_Click" Text="查询" style="float:left;"></dx:ASPxButton>
                    <dx:ASPxButton ID="btnExport" runat="server" OnClick="BtnExport_Click" Text="导出列表" style="float:left;"></dx:ASPxButton>
                    <dx:ASPxButton ID="btnExportLog" runat="server" OnClick="BtnExportLog_Click" Text="导出日志" style="float:left;"></dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>

    <div style="height:15px;"></div>



    <div id="list1" align="left" style="padding:5px;background-color:#EEEEEE">
        <p style="font-family:Microsoft YaHei; font-size:medium;">日志管理</p>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CellPadding="3" ForeColor="#333333" GridLines="None" Height="1px" OnPageIndexChanging="GridView1_PageIndexChanging"
            OnRowDataBound="GridView1_RowDataBound"
            OnSelectedIndexChanging="GridView1_SelectedIndexChanging" Width="920px" PageSize="20" DataKeyNames="pk_id">
            <PagerSettings FirstPageText="首页" LastPageText="末页" Mode="NextPreviousFirstLast"
                NextPageText="下一页" PreviousPageText="上一页" />
            <EmptyDataRowStyle Font-Size="Smaller" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Font-Size="Small" />
            <Columns>
                <asp:BoundField DataField="pk_id" HeaderText="订单编号">
                    <ItemStyle HorizontalAlign="Left" Width="100px"/>
                </asp:BoundField>
                <asp:BoundField DataField="billno" HeaderText="订单号">
                    <ItemStyle HorizontalAlign="Left" Width="170px" />
                    <HeaderStyle HorizontalAlign="Left" Width="170px" />
                </asp:BoundField>
                <asp:BoundField DataField="licenseno" HeaderText="车牌号">
                    <HeaderStyle HorizontalAlign="Left" Width="95px" />
                    <ItemStyle HorizontalAlign="Left" Width="95px" />
                </asp:BoundField>
                <asp:BoundField DataField="status" HeaderText="订单状态">
                    <HeaderStyle HorizontalAlign="Left" Width="55px"/>
                    <ItemStyle HorizontalAlign="Left"   Width="55px"/>
                </asp:BoundField>
                <asp:BoundField DataField="mororaft" HeaderText="上午下午">
                    <HeaderStyle HorizontalAlign="Left" Width="55px"/>
                    <ItemStyle HorizontalAlign="Left"  Width="55px"/>
                </asp:BoundField>
                <asp:BoundField DataField="remark" HeaderText="备注">
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="operatorcode" HeaderText="配送员代码">
                    <ItemStyle HorizontalAlign="Left" Width="90px"/>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="inserttimeforhis" HeaderText="插入时间">
                    <ItemStyle HorizontalAlign="Left" Width="55px"/>
                    <HeaderStyle HorizontalAlign="Left" Width="55px"/>
                </asp:BoundField>
                <asp:BoundField DataField="hurryup" HeaderText="催单次数">
                    <ItemStyle HorizontalAlign="Left" Width="30px"/>
                    <HeaderStyle HorizontalAlign="Left" Width="30px"/>
                </asp:BoundField>
                <asp:CommandField HeaderText="日志列表" SelectText="详情" ShowSelectButton="True">
                    <ItemStyle HorizontalAlign="Center"/>
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:CommandField>
            </Columns>
            <RowStyle BackColor="#FFFFFF" Font-Size="Smaller" Font-Names="Microsoft YaHei" />
            <EditRowStyle BackColor="#FFFFFF" Font-Size="Smaller" />
            <SelectedRowStyle BackColor="#FFFFFF" Font-Bold="False" ForeColor="#333333" Font-Size="Smaller" />
            <PagerStyle BackColor="#EEEEEE" ForeColor="Black" HorizontalAlign="Right" Font-Size="Smaller" Font-Names="Microsoft YaHei"/>
            <HeaderStyle BackColor="#aaaaaa" Font-Bold="True" ForeColor="White" Font-Size="Smaller" Font-Names="Microsoft YaHei" />
            <AlternatingRowStyle BackColor="White" Font-Size="Smaller" Font-Names="Microsoft YaHei" />
        </asp:GridView>
    </div>
    <asp:Label ID="lblPageSum" runat="server" Text="当前页为　1 / 10　页" style="font-size:15px;"></asp:Label>


    </div>
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

</asp:Content>
