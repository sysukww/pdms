<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="detailBill.aspx.cs" Inherits="detailBill" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1">
    <div></div>
    <div id="contentType1">
        <table>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td>
                    <table class="txt">
                        <tr>
                            <td></td>
                            <td style="width: 336px" align="left">
                                <asp:Label ID="txtPkID" runat="server" style="width:560px;" Visible="false"></asp:Label></td>
                            <td style="width: 89px"></td>
                        </tr>
                        <tr>
                            <td style="width:100px; height: 24px;" align="right">
                                订单号：</td>
                            <td style="width: 336px; height: 24px;" align="left">
                                <asp:Label ID="txtBillno" runat="server"></asp:Label></td>  
                            <td style="width: 89px; height: 24px;">
                                </td>
                        </tr>
                        <tr>
                            <td style="width:100px; height: 24px;" align="right">
                                车牌号：</td>
                            <td style="width: 336px; height: 24px;" align="left">
                                <asp:Label ID="txtLicenseno" runat="server"></asp:Label></td>  
                            <td style="width: 89px; height: 24px;">
                               </td>
                        </tr>
                        <tr>
                            <td style="width:100px; height: 24px;" align="right">
                                订单状态：</td>
                            <td style="width: 336px; height: 24px;" align="left">
                                <asp:Label ID="labStatus" runat="server"></asp:Label></td>  
                            <td style="width: 89px; height: 24px;"></td>
                        </tr>
                        <tr>
                            <td style="width:100px; height: 24px;" align="right">
                                见费出单：</td>
                            <td style="width: 336px; height: 24px;" align="left">
                                <asp:Label ID="txtPolicyafterfee" runat="server"></asp:Label></td>  
                            <td style="width: 89px; height: 24px;">
                                </td>
                        </tr>
                        <tr>
                            <td style="width: 85px ;"align="right">
                                上午下午：</td>
                            <td style="width:336px;" align="left">
                                <asp:Label ID="txtMororaft" runat="server" Width="300px" AutoPostBack="true">
                                </asp:Label></td>
                            <td style="width: 89px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 85px;"align="right">
                                备注：</td>
                            <td style="width: 336px" align="left">
                                <asp:Label ID="txtRemark" runat="server" Height="300px" TextMode="MultiLine" Width="560px"></asp:Label></td>
                            <td style="width: 89px"></td>
                        </tr>
                        <tr>
                            <td style="width: 85px;" align="right">
                            收费日期：</td>
                            <td style="width: 336px" align="left">
                                <asp:Label ID="txtChargetime" runat="server" Enabled="false"></asp:Label>
                            </td>
                            <td style="width: 89px"></td>
                        </tr>
                        <tr>
                            <td style="width:100px; height: 24px;" align="right">
                                催单次数：</td>
                            <td style="width: 336px; height: 24px;" align="left">
                                <asp:Label ID="txtHurryup" runat="server"></asp:Label></td>  
                            <td style="width: 89px; height: 24px;">
                                </td>
                        </tr>
                        <tr>
                            <td style="width:100px; height: 24px;" align="right">
                                配送员代码：</td>
                            <td style="width: 336px; height: 24px;" align="left">
                                <asp:Label ID="txtSendMan" runat="server"></asp:Label></td>  
                            <td style="width: 89px; height: 24px;">
                                </td>
                        </tr>
                        <tr>
                            <td style="width: 85px; height: 20px">
                            </td>
                            <td style="width: 336px; height: 20px">
                            </td>
                            <td style="width: 89px; height: 20px">
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="closeBtn" runat="server" text="关闭" OnClick="BtnClose_Click" />
                </td>
                </tr>
                <tr>
                   <td height="2">
                   </td>
                </tr>
            </table>
    </div>
</asp:Content>
