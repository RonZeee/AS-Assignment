<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="AS_Assignment.WebForm4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="Label1" runat="server" Text="User Profile"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnLogout" runat="server" OnClick="Button1_Click" Text="Logout" />
        <br />
        <br />
        <asp:Label ID="lbWelcome" runat="server" ForeColor="Blue"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lbHEmail" runat="server" ForeColor="Blue"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label5" runat="server" Font-Bold="True" Text="CHANGE PASSWORD"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="Enter Current Password:"></asp:Label>
        <br />
        <asp:TextBox ID="tbCurrentPass" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="Enter New Password:"></asp:Label>
        <br />
        <asp:TextBox ID="tbNewPass" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="errHome" runat="server" ForeColor="Red"></asp:Label>
        <br />
        <br />
        <asp:Button ID="btnChangePass" runat="server" OnClick="btnChangePass_Click" Text="Change Password" Width="148px" />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </form>
</body>
</html>
