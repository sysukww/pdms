<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"  AutoEventWireup="true" CodeFile="listBill.aspx.cs" Inherits="listBill" %>

<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxNavBar" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v12.1, Version=12.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v12.1, Version=12.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxDocking" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v12.1, Version=12.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxMenu" tagprefix="dx" %>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="contentType2" style="padding:10px;">
    <div style="width:920px; background-color:#fedcbd; padding:5px;">
        <asp:Button ID="btnNew" runat="server" Text="新增" OnClick="BtnNew_Click" />
        <asp:FileUpload ID="FileUpload1" runat="server" BackColor="White"/>
        <asp:Button ID="btnImport" runat="server" Text="导入" OnClick="BtnImport_Click" />
        <asp:Button ID="btnToday" runat="server" Text="显示今天待分配订单" OnClick="BtnToday_Click" />
    </div>

    <div style="height:5px;"></div>

    <div style="width:910px; background-color:#fedcbd; padding:10px; font-family:Microsoft YaHei; font-size:small;">
        <table>
            <tr>
                <td align="right">插入时间：</td>
                <td align="left">
                    <dx:ASPxDateEdit ID="txtStarttime" runat="server" style="float:left; height:24px;"></dx:ASPxDateEdit>
                    <dx:ASPxLabel ID="txtMiddle" runat="server" Text=" - " style="float:left ; height:30px;"></dx:ASPxLabel>
                    <dx:ASPxDateEdit ID="txtEndtime" runat="server" style="float:left; height:24px;"></dx:ASPxDateEdit>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="BtnSearch_Click" />
                </td>
                <td></td>
            </tr>
        </table>
        <br />     
    </div>

    <div>
        <p style="font-family:Microsoft YaHei; font-size:small;">未分配订单列表</p>
    </div>
    <div id="list1" align="left" style="padding:5px;background-color:#EEEEEE">
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CellPadding="3" ForeColor="#333333" GridLines="None" Height="1px" OnPageIndexChanging="GridView1_PageIndexChanging"
            OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting"
            OnSelectedIndexChanging="GridView1_SelectedIndexChanging" Width="920px" PageSize="20" DataKeyNames="pk_id">
            <PagerSettings FirstPageText="首页" LastPageText="末页" Mode="NextPreviousFirstLast"
                NextPageText="下一页" PreviousPageText="上一页" />
            <EmptyDataRowStyle Font-Size="Smaller" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Font-Size="Small" Font-Names="微软雅黑" />
            <Columns>
                <asp:BoundField DataField="pk_id" HeaderText="订单编号">
                    <ItemStyle HorizontalAlign="Left" Width="100px"/>
                </asp:BoundField>
                <asp:BoundField DataField="billno" HeaderText="订单号">
                    <ItemStyle  HorizontalAlign="Left" Width="170px"/>
                    <HeaderStyle HorizontalAlign="Left" Width="170px"/>
                </asp:BoundField>
                <asp:BoundField DataField="licenseno" HeaderText="车牌号">
                    <HeaderStyle  HorizontalAlign="Left" Width="95px"/>
                    <ItemStyle HorizontalAlign="Left" Width="95px"/>
                </asp:BoundField>
                <asp:BoundField DataField="status" HeaderText="状态">
                    <HeaderStyle HorizontalAlign="Left" Width="55px"/>
                    <ItemStyle HorizontalAlign="Left"  Width="55px"/>
                </asp:BoundField>
                <asp:BoundField DataField="policyafterfee" HeaderText="见费出单">
                    <HeaderStyle HorizontalAlign="Left" Width="55px"/>
                    <ItemStyle HorizontalAlign="Left"  Width="55px" />
                </asp:BoundField>
                <asp:BoundField DataField="chargedate" HeaderText="收费时间">
                    <ItemStyle  HorizontalAlign="Left" Width="55px"/>
                    <HeaderStyle HorizontalAlign="Left" Width="55px"/>
                </asp:BoundField>
                <asp:BoundField DataField="mororaft" HeaderText="上午下午">
                    <ItemStyle  HorizontalAlign="Left" Width="55px"/>
                    <HeaderStyle HorizontalAlign="Left" Width="55px"/>
                </asp:BoundField>
                <asp:BoundField DataField="inserttimeforhis" HeaderText="插入时间">
                    <ItemStyle HorizontalAlign="Left" Width="55px"/>
                    <HeaderStyle HorizontalAlign="Left" Width="55px"/>
                </asp:BoundField>
                <asp:BoundField DataField="operatetimeforhis" HeaderText="更新时间">
                    <ItemStyle HorizontalAlign="Left" Width="55px"/>
                    <HeaderStyle HorizontalAlign="Left" Width="55px"/>
                </asp:BoundField>
                <asp:CommandField HeaderText="详细" SelectText="详细" ShowSelectButton="True">
                    <ItemStyle HorizontalAlign="Center"/>
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:CommandField>
                <asp:CommandField HeaderText="删除" ShowDeleteButton="True">
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
        <hr style="color:#FFFFFF;"/>
        <asp:Label ID="lblPageSum" runat="server" Text="当前页为　1 / 10　页" style="font-family:Microsoft YaHei; font-size:small;"></asp:Label>
    </div>

    </div>
</asp:Content>

