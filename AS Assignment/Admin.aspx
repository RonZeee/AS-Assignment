<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="AS_Assignment.WebForm5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="User Admin"></asp:Label>
            <br />
            <br />
            <asp:Button ID="btnRegis" runat="server" Text="Register" OnClick="btnRegis_Click" />
&nbsp;
            <asp:Button ID="btnLogin" runat="server" Text="Login" />
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Button" />
            <br />
            <br />
            <asp:GridView ID="gvUser" runat="server" Width="662px" DataKeyNames="email" AutoGenerateColumns="false">
                 <Columns>
                 <asp:BoundField DataField="email" HeaderText="email" />
                 <asp:BoundField DataField="firstname" HeaderText="first name" />
                 <asp:BoundField DataField="lastname" HeaderText="last name" />
                 <asp:BoundField DataField="dob" HeaderText="DOB" />
                 <asp:BoundField DataField="creditcard" HeaderText="creditcard" />
                 <asp:BoundField DataField="password" HeaderText="password" />   
                 <asp:HyperLinkField DataNavigateUrlFields="email"
                 DataNavigateUrlFormatString="viewuser.aspx?email={0}"
                    Text="View User" HeaderText="action" />
                 </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
