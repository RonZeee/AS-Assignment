using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AS_Assignment
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataSet dset = new DataSet();
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ToString());
                using (conn)
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand cmd = new SqlCommand("SELECT email, firstname, lastname, dob, creditcard, Password FROM Login", conn);
                    cmd.CommandType = CommandType.Text;
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dset);
                    gvUser.DataSource = dset;
                    gvUser.DataBind();
                }
            }

        }

        protected void btnRegis_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
    }
}