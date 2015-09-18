<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="sendHurry.aspx.cs" Inherits="sendHurry" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1">
    <div></div>
    <div id="contentType1">
        <table style="margin-right:auto; margin-left:auto;">
            <tr>
                <td>
                    <div style="height:20px;"></div>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table class="txt" style="margin-left:auto; margin-right:auto;">
                        <tr>
                            <td></td>
                            <td style="" align="left">
                                <asp:TextBox ID="txtPkID" runat="server" style="width:560px;" Visible="false"></asp:TextBox></td>
                            <td style=""></td>
                            <td></td>
                            <td><asp:TextBox ID="txtHurryup" runat="server" Visible="false"></asp:TextBox></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="height: 24px; width:80px; font-family:Microsoft YaHei; font-size:13px;" align="right">
                                订单号：</td>
                            <td style="height: 24px; width:100px;" align="left">
                                <asp:TextBox ID="txtBillno" runat="server" style="height:20px; width:200px;" Enabled="false"></asp:TextBox></td>  
                            <td style="height: 24px; width:60px;"></td>
                            <td style="height: 24px; width:80px; font-family:Microsoft YaHei; font-size:13px;" align="right">
                                车牌号：</td>
                            <td style=" height:24px; width:100px;" align="left">
                                <asp:TextBox ID="txtLicenseno" runat="server" style="height:20px; width:200px;" Enabled="false"></asp:TextBox></td>  
                            <td style=" height:24px; width:60px;"></td>
                        </tr>
                        <tr>
                            <td style="height: 24px; width:80px; font-family:Microsoft YaHei; font-size:13px;" align="right">
                                订单状态：</td>
                            <td style="height: 24px; width:100px;" align="left">
                                <asp:TextBox ID="labStatus" runat="server" Enabled="false" style="height:20px; width:200px;" ></asp:TextBox></td>  
                            <td style="height: 24px; width:60px;"></td>
                            <td style="height: 24px; width:80px; font-family:Microsoft YaHei; font-size:13px;" align="right">
                                见费出单：</td>
                            <td style="height: 24px; width:100px;" align="left">
                                <asp:TextBox ID="txtPolicyafterfee" runat="server" style="height:20px; width:200px;" Enabled="false"></asp:TextBox></td>  
                            <td style="height: 24px; width:60px;"></td>
                        </tr>
                        <tr>
                            <td style="height: 24px; width:80px; font-family:Microsoft YaHei; font-size:13px;" align="right">
                                上午下午：</td>
                            <td style="height: 24px; width:100px;" align="left">
                                <asp:TextBox ID="txtMororaft" runat="server" style="height:20px; width:200px;" Enabled="false"></asp:TextBox></td>
                            <td style="height: 24px; width:60px;">
                            </td>
                            <td style="height: 24px; width:80px; font-family:Microsoft YaHei; font-size:13px;" align="right">
                                收费日期：
                            </td>
                            <td style="height: 24px; width:100px;" align="left">
                                <asp:TextBox ID="txtChargetime" runat="server" style="height:20px; width:200px;" Enabled="false">
                                </asp:TextBox>
                            </td>
                            <td style="height: 24px; width:60px;">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 24px; width:80px; font-family:Microsoft YaHei; font-size:13px;" align="right">
                                配送员代码：</td>
                            <td style="height: 24px; width:100px;" align="left">
                                <asp:TextBox ID="txtOperator" runat="server" style="height:20px; width:200px;" Enabled="false"></asp:TextBox></td>
                            <td style="height: 24px; width:60px;">
                            </td>
                            <td style="height: 24px; width:80px; font-family:Microsoft YaHei; font-size:13px;" align="right">
                                配送员电话：
                            </td>
                            <td style="height: 24px; width:100px;" align="left">
                                <asp:TextBox ID="txtPhone" runat="server" style="height:20px; width:200px;" Enabled="false">
                                </asp:TextBox>
                            </td>
                            <td style="height: 24px; width:60px;">
                                <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtPhone" Font-Size="Small"
                                    ErrorMessage="* 必填项" ValidationGroup="ValidataGroup1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 24px; width:80px; font-family:Microsoft YaHei; font-size:13px;" align="right"></td>
                            <td style="height: 24px; width:100px;" align="left"></td>
                            <td style="height: 24px; width:60px;"></td>
                            <td style="height: 24px; width:80px; font-family:Microsoft YaHei; font-size:13px;" align="right"></td>
                            <td style="height: 24px; width:100px;" align="left">
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPhone"
                                    ErrorMessage="格式：11位数字" ValidationExpression="\d{11}" ValidationGroup="ValidataGroup1"></asp:RegularExpressionValidator>
                            </td>
                            <td style="height: 24px; width:60px;"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td style="width:80px; font-family:Microsoft YaHei; font-size:13px;" align="right">
                                备注：</td>
                            <td style="width:48px;" align="left">
                                <asp:TextBox ID="txtRemark" runat="server" Height="100px" TextMode="MultiLine" Width="560px" Enabled="false"></asp:TextBox></td>
                            <td style="width: 60px"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
               <td style="height:15px;">
               </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="closeBtn" runat="server" text="关闭" OnClick="BtnClose_Click"/>
                    <asp:Button ID="saveBtn" runat="server" text="催单" OnClick="BtnSave_Click" ValidationGroup="ValidataGroup1"/>
                </td>
            </tr>
            <tr>
               <td style="height:50px;">
               </td>
            </tr>
        </table>
    </div>
</asp:Content>
