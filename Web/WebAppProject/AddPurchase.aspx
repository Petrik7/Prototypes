<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPurchase.aspx.cs" Inherits="WebAppProject.AddPurchase_Click" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Gas Purchase</title>
     <!-- To scale page on mobile browsers -->
    <meta name="HandheldFriendly" content="true" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0,maximum-scale=4.0;" />
    <style type="text/css">
        body {font-family:Calibri;}
        
        table.AddPurchase tr.light td {background-color: #FAFAD2;}
        table.AddPurchase tr.dark0 td {background-color: #EEE8AA;}
    </style>


</head>
<body>
    <form id="form1" runat="server">
    <div></div>
    <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true" />

        <table align="center">
            <tr>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table cellpadding="1" border="0" align="center">
                        <tr>
                            <td align="left" width="58px"><asp:Label ID="LabelHello" runat="server"/></td>
                            <td align="right"> 
                                <asp:LinkButton ID="GasTracker" Text="Gas Tracker" OnClick="GasTracker_Click" runat="server" Width="88px" CausesValidation="false"/>
                                <asp:LinkButton ID="ButtonHomePage" Text="Home Page" OnClick="HomePage_Click" runat="server" CausesValidation="false"/>
                                <asp:LinkButton ID="ButtonSignOut" Text="Sign Out" OnClick="Signout_Click" runat="server" Width="68px" CausesValidation="false"/>
                            </td>
                        </tr>
                     </table>
                 </ContentTemplate>
                 </asp:UpdatePanel>
             </tr>
             <%-- --%>
             <tr>
                <td style="height: 206px" valign="top">
                    <asp:UpdatePanel ID="InsertGasPurchaseUpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                          <table class="AddPurchase" cellpadding="2" align="center" cellspacing = "0" runat="server" borderwidth="8"
                          style="border-left: 1px solid Tan; border-right: 1px solid Tan; border-top: 1px solid Tan; border-bottom: 1px solid Tan"> 
                            <tr style="background-color:Tan">
                                <td align="left" colspan="4"><asp:Label ID="Label1" text="Enter your gas purchase:" runat="server" /></td>
                            </tr>
                            <tr class="light">
                                <td colspan="4"><asp:Label ID="ErrorLabel" runat="server" ForeColor="Black" /></td>
                            </tr>
                            <tr class="light">
                                <td><asp:Label ID="LabelDate" runat="server" AssociatedControlID="Calendar" 
                                             Text="Date" ForeColor="Black" /></td>
                                <td colspan="2" align="center"><asp:Calendar ID="Calendar" Text="Select Purchase Date" ForeColor="Black" runat="server"/></td>
                                <td></td>
                            </tr>
                            <tr class="light">
                                <td colspan="4" align="center"><asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                                                HeaderText="There were errors on the page:" ForeColor="DarkRed"/></td>
                            </tr>
                            <tr  class="dark0">
                               <td><asp:Label ID="PriceLabel" runat="server" AssociatedControlID="PriceTextBox" 
                                             Text="Price" ForeColor="Black" /></td>
                              <td colspan="2"><asp:TextBox runat="server" ID="PriceTextBox" style="width:98%;height:100%" /></td>
                              <td><asp:RequiredFieldValidator ID="RequiredFieldPriceValidator" runat="server" ControlToValidate="PriceTextBox"
                                        ErrorMessage="Price is required." ForeColor="DarkRed"> *
                                  </asp:RequiredFieldValidator>
                                  <asp:RegularExpressionValidator ID="RegularExpression_PriceValidator" runat="server" ControlToValidate="PriceTextBox"
                                        ErrorMessage="Price must be in format $$$$[.cc]." Text="*" ForeColor="DarkRed"
                                        ValidationExpression="empty"/>                               
                              </td>
                            </tr>
                            <tr  class="light">
                              <td><asp:Label ID="AmountLabel" runat="server" AssociatedControlID="AmountTextBox" 
                                             Text="Amount" ForeColor="Black" /></td>
                              <td><asp:TextBox runat="server" ID="AmountTextBox" style="width:130px;height:100%"/></td>
                              <td><asp:DropDownList ID="LiterGallonDropDownList" runat="server" AutoPostBack="True" DataTextField= "ENTITY_ID"
                                                    DataValueField="ENTITY_ID" style="width:98%;height:100%">
                                        <asp:ListItem>Liters</asp:ListItem>
                                        <asp:ListItem>Gallons</asp:ListItem>           
                                  </asp:DropDownList></td>
                              <td><asp:RequiredFieldValidator ID="RequiredFieldAmountValidator" runat="server" ControlToValidate="AmountTextBox"
                                        ErrorMessage="Amount is required." ForeColor="DarkRed"> *
                                  </asp:RequiredFieldValidator>
                                  <asp:RegularExpressionValidator ID="RegularExpression_AmountValidator" runat="server" ControlToValidate="AmountTextBox"
                                        ErrorMessage="Amount must be in format LLLL[.ll]" Text="*" ForeColor="DarkRed"
                                        ValidationExpression="empty"/>
                              </td>
                            </tr>
                            <tr  class="dark0">
                              <td><asp:Label ID="DistanceLabel" runat="server" AssociatedControlID="DistanceTextBox" 
                                             Text="Distance" ForeColor="Black" /></td>
                              <td><asp:TextBox runat="server" ID="DistanceTextBox" style="width:130px;height:100%"/></td>
                              <td><asp:DropDownList ID="KmMileDropDownList" runat="server" AutoPostBack="True" DataTextField= "ENTITY_ID"
                                                    DataValueField="ENTITY_ID" style="width:98%;height:100%">
                                        <asp:ListItem>Kms</asp:ListItem>
                                        <asp:ListItem>Miles</asp:ListItem>           
                                    </asp:DropDownList></td>
                              <td><asp:RequiredFieldValidator ID="RequiredFieldDistanceValidator" runat="server" ControlToValidate="DistanceTextBox"
                                        ErrorMessage="Distance is required." ForeColor="DarkRed"> *
                                  </asp:RequiredFieldValidator>
                                  <asp:RegularExpressionValidator ID="RegularExpression_DistanceValidator" runat="server" ControlToValidate="DistanceTextBox"
                                        ErrorMessage="Distance must be in format MMMM[.mm]" Text="*" ForeColor="DarkRed"
                                        ValidationExpression="empty" />
                              </td>
                            </tr>
                            <tr class="light">
                                <td><asp:Label ID="Grade" runat="server" AssociatedControlID="DropDownListGrade" 
                                             Text="Grade" ForeColor="Black" /></td>
                                <td colspan="2"><asp:DropDownList ID="DropDownListGrade" runat="server" AutoPostBack="True" DataTextField= "ENTITY_ID"
                                             DataValueField="ENTITY_ID" style="width:99%;height:100%">
                                    </asp:DropDownList></td>
                                    <td></td>
                            </tr>
                            <tr class="dark0">
                               <td><asp:Label ID="Note" runat="server" AssociatedControlID="NoteTextBox" 
                                             Text="Note" ForeColor="Black" /></td>
                               <td colspan="2"><asp:TextBox runat="server" ID="NoteTextBox" style="width:98%;height:100%" /></td>
                               <td>
                                  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="NoteTextBox"
                                        ErrorMessage="Note cannot be longer than 64" Text="*" ForeColor="DarkRed"
                                        ValidationExpression="^[ -~]{0,64}$"/>
                              </td>
                            </tr>
                            <tr class="light">
                              <td></td>
                              <td align="right"> <asp:Button ID="InsertButton" runat="server" Text="Insert" OnClick="InsertButton_Click" /></td>
                              <td align="left"> <asp:Button ID="Cancelbutton" runat="server" Text="Cancel" OnClick="CancelButton_Click"  CausesValidation="false"/></td>
                              <td></td>
                            </tr>
                            <tr class="dark0">
                                <td align="left" colspan="4"><asp:Label runat="server" ID="InputTimeLabel"><%=DateTime.Now %></asp:Label></td>
                            </tr>
                          </table>                          
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="InsertButton" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
           </tr>
        </table>

    </form>
</body>
</html>
