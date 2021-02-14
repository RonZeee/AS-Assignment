using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AS_Assignment
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        string str = null;
        SqlCommand com;
        byte up;
        string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBCOnnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            errLogin.Text = HttpUtility.HtmlEncode(tbLEmail.Text);
            if (errLogin != null)
            {
                errLogin.Text = "XSS Prevention occured in Email";
            }

            string password = tbLPass.Text.ToString().Trim();
            string email = tbLEmail.Text.ToString().Trim();
            SHA512Managed hashing = new SHA512Managed();
            string dbHash = getDBHash(email);
            string dbSalt = getDBSalt(email);
            try
            {
                if (dbSalt != null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0)
                {
                    string pwdWithSalt = password + dbSalt;
                    byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                    string userHash = Convert.ToBase64String(hashWithSalt);
                    if (userHash.Equals(dbHash))
                    {
                        //session fixation
                        Session["LoggedIn"] = tbLEmail.Text.Trim();
                        //new GUID and saved into the session
                        string guid = Guid.NewGuid().ToString();
                        Session["AuthToken"] = guid;

                        //create new cookie with this guid value
                        Response.Cookies.Add(new HttpCookie("AuthToken", guid));
                        Response.Redirect("Home.aspx", false);
                    }
                    else
                    {
                        errLogin.Text = "Email or password is not valid. Please try again.";
                        /*
                        str = "select * from Login ";
                        SqlDataReader reader = com.ExecuteReader();
                        try
                        {
                            using (SqlConnection con = new SqlConnection(MYDBConnectionString))
                            {
                                con.Open();
                                str = "update Login set failLoginAttempts = failLoginAttempts + 1 where email='" + tbLEmail.ToString() + "'";
                                com = new SqlCommand(str, con);
                                com.ExecuteNonQuery();
                                var count = "3";
                                while (reader.Read())
                                {
                                    if (count == reader["failLoginAttempts"].ToString())
                                    {
                                        errFail.Text = reader["failLoginAttempts"].ToString();
                                    }
                                }
                                con.Close();
                            }
                        }
                        catch (SqlException ex)
                        {
                            throw new Exception(ex.ToString());
                        }
                        */
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }
        }

        protected string getDBHash(string email)
        {
            string h = null;
            SqlConnection connection = new SqlConnection(MYDBConnectionString);
            string sql = "select passwordHash FROM Login WHERE email=@email";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@email", email);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        if (reader["passwordHash"] != null)
                        {
                            if (reader["passwordHash"] != DBNull.Value)
                            {
                                h = reader["passwordHash"].ToString();
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return h;
        }

        protected string getDBSalt(string email)
        {
            string s = null;
            SqlConnection connection = new SqlConnection(MYDBConnectionString);
            string sql = "select passwordSalt FROM Login WHERE email=@email";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@email", email);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["passwordSalt"] != null)
                        {
                            if (reader["passwordSalt"] != DBNull.Value)
                            {
                                s = reader["passwordSalt"].ToString();
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return s;
        }

        protected void loginFails()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(MYDBConnectionString))
                {

                    con.Open();
                    string sql = "UPDATE Login SET failLoginAttempts = failLoginAttempts + 1 WHERE email=@email";
                    SqlCommand command = new SqlCommand(sql, con);
                    //command.Parameters.AddWithValue("@email", email);
                    con.Close();

                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}