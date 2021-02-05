<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="AS_Assignment.WebForm2" ValidateRequest="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://www.google.com/recaptcha/api.js?render=6LfGij8aAAAAAET3NJld6kFxgDPyh5SMWQ46r-z7"></script>
    <script type="text/javascript">
        //password validation
        function validate() {
            var str = document.getElementById('<%=tbPass.ClientID %>').value;
            if (str.length < 8) {
                document.getElementById("errPass").innerHTML = "Password length must be at least 8 characters long."
                document.getElementById("errPass").style.color = "Red";
                return ("too_short");
            }
            else if (str.search(/[0-9]/) == -1) {
                document.getElementById("errPass").innerHTML = "Password requires at least 1 number";
                document.getElementById("errPass").style.color = "Red";
                return ("no_number");
            }
            else if (str.search(/[A-Z]/) == -1) {
                document.getElementById("errPass").innerHTML = "Password requires at least 1 upper case";
                document.getElementById("errPass").style.color = "Red";
                return ("no_uppercase");
            }
            else if (str.search(/[a-z]/) == -1) {
                document.getElementById("errPass").innerHTML = "Password requires at least 1 lower case";
                document.getElementById("errPass").style.color = "Red";
                return ("no_lowercase");
            }
            else if (str.search(/[^A-Za-z0-9]/) == -1) {
                document.getElementById("errPass").innerHTML = "Password requires at least 1 special character";
                document.getElementById("errPass").style.color = "Red";
                return ("no_special");
            }

            document.getElementById("errPass").innerHTML = "Excellent!!"
            document.getElementById("errPass").style.color = "Green";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label7" runat="server" Text="Registration"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Login instead" />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnUserAdmin" runat="server" OnClick="btnUserAdmin_Click" Text="User Admin" />
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="First Name:"></asp:Label>
            <br />
            <asp:TextBox ID="tbFirst" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Last Name:"></asp:Label>
            <br />
            <asp:TextBox ID="tbLast" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Text="Date of Birth:"></asp:Label>
            <br />
            <asp:TextBox ID="tbDob" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label4" runat="server" Text="Email Address:"></asp:Label>
            <br />
            <asp:TextBox ID="tbEmail" runat="server"></asp:TextBox>
            &nbsp;
            <asp:Label ID="errEmail" runat="server" ForeColor="Red"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label5" runat="server" Text="Credit Card Info:"></asp:Label>
            <br />
            <asp:TextBox ID="tbCredit" runat="server"></asp:TextBox>
            &nbsp;
            <asp:Label ID="errCredit" runat="server" ForeColor="Red"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label6" runat="server" Text="Password:"></asp:Label>
            <br />
            <asp:TextBox ID="tbPass" runat="server" onkeyup="javascript:validate()" TextMode="Password"></asp:TextBox>
            &nbsp;
            <asp:Label ID="errPass" runat="server" ForeColor="Red"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            <br />
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="167px" OnClick="btnSubmit_Click" />
            <br />
            <br />
        <asp:Label ID="lbComment" runat="server"></asp:Label>
            <br />
        </div>
    <script>
        grecaptcha.ready(function () {
            grecaptcha.execute('6LfGij8aAAAAAET3NJld6kFxgDPyh5SMWQ46r-z7', { action: 'Login' }).then(function (token) {
                document.getElementById("g-recaptcha-response").value = token;
            });
        });
    </script>
    </form>
    </body>
</html>
