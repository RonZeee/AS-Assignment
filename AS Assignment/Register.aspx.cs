using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace AS_Assignment
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        string str = null;
        SqlCommand com;
        byte up;

        string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBCOnnection"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        //password validate
        private int checkPass(string password)
        {
            int score = 0;
            if (password.Length < 8)
            {
                return 1;
            }
            else
            {
                score = 1;
            }
            if (Regex.IsMatch(password, "[a-z]"))
            {
                score++;
            }
            if (Regex.IsMatch(password, "[0-9]"))
            {
                score++;
            }
            if (Regex.IsMatch(password, "[A-Z]"))
            {
                score++;
            }
            if (Regex.IsMatch(password, "[^A-Za-z0-9]"))
            {
                score++;
            }
            return score;

        }

        protected void checkEmail()
        {

            try
            {
                using (SqlConnection con = new SqlConnection(MYDBConnectionString))
                {
                    con.Open();
                    str = "select * from Login ";
                    com = new SqlCommand(str, con);
                    SqlDataReader reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        if (tbEmail.Text == reader["email"].ToString())
                        {
                            up = 1;
                        }
                    }
                    reader.Close();
                    con.Close();

                    if (up == 1)
                    {
                        errEmail.Text = "Email Already being used";
                        errEmail.ForeColor = Color.Red;
                        lbComment.Text = "Account creation failure";
                        lbComment.ForeColor = Color.Red;
                    }
                    else
                    {
                        try
                        {

                            using (SqlCommand cmd = new SqlCommand("INSERT INTO Login VALUES(@email, @firstname, @lastname, @dob, @creditcard, @password, @Key, @IV, @passwordHash, @passwordSalt, @failLoginAttempts)"))
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.Text;
                                    cmd.Parameters.AddWithValue("@email", tbEmail.Text.Trim());
                                    cmd.Parameters.AddWithValue("@firstname", tbFirst.Text.Trim());
                                    cmd.Parameters.AddWithValue("@lastname", tbLast.Text.Trim());
                                    cmd.Parameters.AddWithValue("@dob", DateTime.Now);
                                    cmd.Parameters.AddWithValue("@creditcard", Convert.ToBase64String(encryptData(tbCredit.Text.Trim())));
                                    cmd.Parameters.AddWithValue("@password", tbPass.Text.Trim());
                                    cmd.Parameters.AddWithValue("@Key", Convert.ToBase64String(Key));
                                    cmd.Parameters.AddWithValue("@IV", Convert.ToBase64String(IV));
                                    cmd.Parameters.AddWithValue("@passwordHash", finalHash);
                                    cmd.Parameters.AddWithValue("@passwordSalt", salt);
                                    cmd.Parameters.AddWithValue("@failLoginAttempts", 0);
                                    cmd.Connection = con;
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                        }
                        //try..catch SQL injection protection
                        catch (SqlException ex)
                        {
                            throw new Exception(ex.ToString());
                            //lbComment.Text = "Error Creating Account";
                        }
                        //Response.Redirect("Login.aspx");
                        lbComment.Text = "Account Created";
                        lbComment.ForeColor = Color.Green;
                        errEmail.Text = "Excellent!";
                        errEmail.ForeColor = Color.Green;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //password validate
            int scores = checkPass(tbPass.Text);
            string status = "";
            switch (scores)
            {
                case 1:
                    status = "Very Weak";
                    break;
                case 2:
                    status = "Weak";
                    break;
                case 3:
                    status = "Medium";
                    break;
                case 4:
                    status = "Strong";
                    break;
                case 5:
                    status = "Excellent";
                    break;
                default:
                    break;
            }
            errPass.Text = "Status : " + status;
            if (scores < 4)
            {
                errPass.ForeColor = Color.Red;
                return;
            }
            errPass.ForeColor = Color.Green;
            //password validate
            //lbComment.Text = HttpUtility.HtmlEncode(tbFirst.Text);

            string pwd = tbPass.Text.ToString().Trim();

            //Generate random "salt"
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] saltByte = new byte[8];

            //Fills array of bytes with a cryptographically strong sequence of random values.
            rng.GetBytes(saltByte);
            salt = Convert.ToBase64String(saltByte);
            SHA512Managed hashing = new SHA512Managed();
            string pwdWithSalt = pwd + salt;
            byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwd));
            byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
            finalHash = Convert.ToBase64String(hashWithSalt);

            //Encryption
            RijndaelManaged cipher = new RijndaelManaged();
            cipher.GenerateKey();
            Key = cipher.Key;
            IV = cipher.IV;
            //Encryption
            createAccount();
        }
        //encrypt credit card no.
        protected byte[] encryptData(string data)
        {
            byte[] cipherText = null;
            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;
                ICryptoTransform encryptTransform = cipher.CreateEncryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(data);
                cipherText = encryptTransform.TransformFinalBlock(plainText, 0, plainText.Length);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }
            return cipherText;
        }
        public void createAccount()
        {
            checkEmail();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void btnUserAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx");
        }
        //Error handling
        private void HttpNotFound()
        {
            Response.Clear();
            Response.StatusCode = 404;
            Response.End();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        
    }
}