<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="listLog.aspx.cs" Inherits="listLog" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="contentType2" style="padding:10px;">

    <div style="width:920px; background-color:#fedcbd; padding:5px;font-family:Microsoft YaHei; font-size:small;">
        <table>
            <tr>
                <td align="right" style="width:100px;">订单号：</td>
                <td align="left" style="width:200px;"><asp:Label ID="labbillno" runat="server"></asp:Label></td>
                <td align="right" style="width:100px;">车牌号：</td>
                <td align="left" style="width:200px;"><asp:Label ID="lablicenseno" runat="server"></asp:Label></td>
            </tr>
        </table>
    </div>
    <div style="height:10px;">

    </div>

    <div id="list1" align="left" style="padding:5px;background-color:#EEEEEE">
        <p style="font-family:Microsoft YaHei; font-size:medium;">日志列表</p>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CellPadding="3" ForeColor="#333333" GridLines="None" Height="1px" OnPageIndexChanging="GridView1_PageIndexChanging"
            OnRowDataBound="GridView1_RowDataBound"
            OnSelectedIndexChanging="GridView1_SelectedIndexChanging" Width="920px" PageSize="10" DataKeyNames="pk_id">
            <PagerSettings FirstPageText="首页" LastPageText="末页" Mode="NextPreviousFirstLast"
                NextPageText="下一页" PreviousPageText="上一页" />
            <EmptyDataRowStyle Font-Size="Smaller" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Font-Size="Small" />
            <Columns>
                <asp:BoundField DataField="pk_id" HeaderText="日志编号">
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="mst_id" HeaderText="订单编号">
                    <ItemStyle  Width="100px" HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="status" HeaderText="订单状态">
                    <HeaderStyle Width="110px" HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left"  Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="operatorcode" HeaderText="操作员代码">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left"   />
                </asp:BoundField>
                <asp:BoundField DataField="operatedate" HeaderText="操作时间">
                    <ItemStyle Width="90px" HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="inserttimeforhis" HeaderText="插入时间">
                    <ItemStyle Width="90px" HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="operatetimeforhis" HeaderText="更新时间">
                    <ItemStyle Width="90px" HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
            </Columns>
            <RowStyle BackColor="#FFFFFF" Font-Size="Smaller" Font-Names="Microsoft YaHei" />
            <EditRowStyle BackColor="#FFFFFF" Font-Size="Smaller" />
            <SelectedRowStyle BackColor="#FFFFFF" Font-Bold="False" ForeColor="#333333" Font-Size="Smaller" />
            <PagerStyle BackColor="#EEEEEE" ForeColor="Black" HorizontalAlign="Right" Font-Size="Smaller" Font-Names="Microsoft YaHei"/>
            <HeaderStyle BackColor="#aaaaaa" Font-Bold="True" ForeColor="White" Font-Size="Smaller" Font-Names="Microsoft YaHei" />
            <AlternatingRowStyle BackColor="White" Font-Size="Smaller" Font-Names="Microsoft YaHei" />
        </asp:GridView>
        <asp:Label ID="lblPageSum" runat="server" Text="当前页为　1 / 10　页" style="font-size:15px;"></asp:Label>
    </div>
    <div style="width:920px;  padding:5px;font-family:Microsoft YaHei; font-size:small; margin-top:10px;">
        <table style="width:920px">
            <tr>
                <td align="center">
                    <dx:ASPxButton ID="BtnClose" runat="server" text="关闭" OnClick="btnClose_Click" Width="100px"></dx:ASPxButton>
                </td>
            </tr>
        </table>       
    </div>

    </div>
</asp:Content>

