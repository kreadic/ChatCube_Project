using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

public partial class LogIn : System.Web.UI.Page
{
    ConStringProvider Connection;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Connection = new ConStringProvider(Server.MapPath("/App_Data/ChatCubeDatabase.mdf"));
            if (!IsPostBack)
            {
                Session["LoggedInUserID"] = null;
                if (Request.Cookies["Email"] != null && Request.Cookies["Password"] != null)
                {
                    txtEmail.Text = Request.Cookies["Email"].Value;
                    txtPassword.Text = Request.Cookies["Password"].Value;
                    cbxRememberMe.Checked = true;
                }
            }
        }
        catch 
        {
            Session.RemoveAll();
            Response.Redirect("/Views/LogIn.aspx");
        }
        
    }
    private void ShowMessage(string message)
    {
        divError.Attributes.Add("style", "display:normal");
        lblStrength.Text = message;
    }

    private string EmailExistsCommand(string email)
    {
        return "Select EmailAddress from Users_tbl where EmailAddress='"+email+"'";
    }
    private void IsEmailUnique()
    {
        using (SqlConnection con = new SqlConnection(Connection.ConnectionString))
        {
            con.Open();
            SqlCommand command = new SqlCommand(EmailExistsCommand(txtEmail.Text.ToLower()), con);
            command.ExecuteNonQuery();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.FieldCount > 0)
            {
                divError.Attributes.Add("style", "display:normal");
                lblStrength.Text = "This email already exists";
                return;
            }
            reader.Close();
            con.Close();
        }
    }
    
    private void ValidatePassword()
    {
        lblStrength.Text = "";
        Regex passValidator = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{6,})");
        if (!passValidator.IsMatch(txtPassword.Text))
        {
            divError.Attributes.Add("style", "display:normal");
            lblStrength.Text = "Password must have at least 1 uppercase character,\n 1 lowercase character, 1 special character,\n1 number \nand must be at least 6 characters long";
            return;
        }
    }
    private string UserExistsCommand(string email, string password)
    {
        return "select EmailAddress,Password from Users_tbl " +
               "inner join LogIn_tbl on Users_tbl.User_ID = LogIn_tbl.User_ID " +
               "where EmailAddress='"+ email + "' and Password='"+ password + "'";
    }
    private string EncryptedPassword()
    {
        byte[] data = System.Text.Encoding.ASCII.GetBytes(txtPassword.Text);
        data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
        string Passwordhash = System.Text.Encoding.ASCII.GetString(data);
        return Passwordhash;
    }
    private void UserExists()
    {
        using (SqlConnection con = new SqlConnection(Connection.ConnectionString))
        {
            con.Open();

            SqlCommand command = new SqlCommand(UserExistsCommand(txtEmail.Text.ToLower(), @EncryptedPassword()), con);
            command.ExecuteNonQuery();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.FieldCount == 0)
            {
                divError.Attributes.Add("style", "display:normal");
                lblStrength.Text = "Incorrect Email or Password combination!";
                return;
            }
            reader.Close();
            con.Close();
        }
    }
    private void ValidateCredentials()
    {
        IsEmailUnique();//Does the email exist in the database
        ValidatePassword();//Does the password meet minimum criteria
        UserExists();//Is this user registered
    }
    private string GetUserCommand(string email, string password)
    {
        return "select top 1 Users_tbl.User_ID from Users_tbl " +
               "inner join LogIn_tbl on Users_tbl.User_ID = LogIn_tbl.User_ID " +
               "where EmailAddress='" + email + "' and Password='" + password + "'";


    }
    protected void btnLogIn_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValid)
            {
                using (SqlConnection con = new SqlConnection(Connection.ConnectionString))
                {

                    ValidateCredentials();//if something is incorrect the thread will terminate

                    con.Open();
                    SqlCommand command = new SqlCommand(GetUserCommand(txtEmail.Text.ToLower(), @EncryptedPassword()), con);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Session["LoggedInUserID"] = reader.GetSqlInt32(0);

                        if (cbxRememberMe.Checked)
                        {
                            Response.Cookies["Email"].Value = txtEmail.Text.ToLower();
                            Response.Cookies["Password"].Value = txtPassword.Text;
                            Response.Cookies["Email"].Expires = DateTime.Now.AddDays(5);
                            Response.Cookies["Password"].Expires = DateTime.Now.AddDays(5);
                        }
                        else
                        {
                            Response.Cookies["Email"].Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
                        }

                        reader.Close();
                        con.Close();
                        Response.Redirect("~/Views/Profile.aspx?User_ID=" + Session["LoggedInUserID"].ToString());

                    }
                    else
                    {
                        divError.Attributes.Add("style", "display:normal");
                        lblStrength.Text = "Account not found! Sign up below";
                    }
                }
            }

        }
        catch 
        {
            Response.Redirect("~/Views/Profile.aspx?User_ID=" + Session["LoggedInUserID"].ToString());

        }
    }

    protected void btnCloseError_Click(object sender, EventArgs e)
    {
        divError.Attributes.Add("style", "display:none");
    }
}