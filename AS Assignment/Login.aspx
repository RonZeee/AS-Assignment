<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AS_Assignment.WebForm3"  ValidateRequest="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="User Login"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnRegister" runat="server" OnClick="Button1_Click" Text="Register Now" />
            <br />
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Email:" ></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="tbLEmail" runat="server"></asp:TextBox>
            &nbsp;
            <asp:Label ID="errFail" runat="server" Text="Label" Visible="False"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Text="Password:"></asp:Label>
&nbsp;<asp:TextBox ID="tbLPass" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="errLogin" runat="server" ForeColor="Red"></asp:Label>
            <br />
            <br />
            <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" style="height: 29px" />
        </div>
    </form>
</body>
</html>
