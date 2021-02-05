using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AS_Assignment
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBCOnnection"].ConnectionString;
        string str = null;
        SqlCommand com;
        byte up;
        protected void Page_Load(object sender, EventArgs e)
        {
            //HttpNotFound();
            
            if (Session["LoggedIn"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
            {
                if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
                {
                    Response.Redirect("Login.aspx", false);
                }
                else
                {
                    var email = Session["LoggedIn"].ToString();
                    updatePage(email);
                }
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //session fixation
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            Response.Redirect("Login.aspx", false);

            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            }
            if (Request.Cookies["AuthToken"] != null)
            {
                Response.Cookies["AuthToken"].Value = string.Empty;
                Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
            }
        }
        //Error Handling
        private void HttpNotFound()
        {
            Response.Clear();
            Response.StatusCode = 404;
            Response.End();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        protected void btnChangePass_Click(object sender, EventArgs e)
        {
            try
            {
                using(SqlConnection con = new SqlConnection(MYDBConnectionString))
                {
                    con.Open();
                    str = "select * from Login ";
                    com = new SqlCommand(str, con);
                    SqlDataReader reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        if (tbCurrentPass.Text == reader["password"].ToString())
                        {
                            up = 1;
                        }
                    }
                    reader.Close();
                    con.Close();

                    if (up == 1)
                    {
                        con.Open();
                        str = "update Login set password=@password where email='" + Session["LoggedIn"].ToString() + "'";
                        com = new SqlCommand(str, con);
                        com.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar, 50));
                        com.Parameters["@password"].Value = tbNewPass.Text;
                        com.ExecuteNonQuery();
                        con.Close();
                        errHome.Text = "Password changed Successfully";
                    }
                    else
                    {
                        errHome.Text = "Please enter correct Current password";
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        //Update user info
        protected void updatePage(string email)
        {
            SqlConnection connection = new SqlConnection(MYDBConnectionString);
            string sql = "SELECT * FROM Login WHERE Email=@email";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@email", email);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["email"] != DBNull.Value)
                        {
                            lbWelcome.Text = "Welcome User " + reader["firstname"].ToString();
                            lbHEmail.Text = "Email: " + reader["email"].ToString();

                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}