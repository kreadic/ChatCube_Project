using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_ChangePassword : System.Web.UI.Page
{

    ConStringProvider Connection;
    protected void Page_Load(object sender, EventArgs e)
    {
        Connection = new ConStringProvider(Server.MapPath("/App_Data/ChatCubeDatabase.mdf"));
        if (Session["LoggedInUserID"] == null)
            Response.Redirect("~/Views/LogIn.aspx");
    }

    private void ValidatePassword(string password)
    {
        lblStrength.Text = "";
        Regex passValidator = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{6,})");
        if (!passValidator.IsMatch(password))
        {
            divError.Attributes.Add("style", "display:normal");
            lblStrength.Text = "Password must have at least 1 uppercase character,\n 1 lowercase character, 1 special character,\n1 number \nand must be at least 6 characters long";
            return;
        }
    }
    protected void btnCloseError_Click(object sender, EventArgs e)
    {
        divError.Attributes.Add("style", "display:none");
    }
    private string UserExistsCommand(int id, string password)
    {
        return "select EmailAddress,Password from Users_tbl " +
               "inner join LogIn_tbl on Users_tbl.User_ID = LogIn_tbl.User_ID " +
               "where Users_tbl.User_ID=" + id + " and Password='" + password + "'";
    }
    private string EncryptedPassword(string password)
    {
        byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
        data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
        string Passwordhash = System.Text.Encoding.ASCII.GetString(data);
        return Passwordhash;
    }
    protected void lbtnReset_Click(object sender, EventArgs e)
    {
        try
        {
            int userid = int.Parse(Session["LoggedInUserID"].ToString());
            ValidatePassword(txtOldPassword.Text);
            ValidatePassword(txtNewPassword.Text);
            ValidatePassword(txtConfirm.Text);

            using (SqlConnection con = new SqlConnection(Connection.ConnectionString))
            {
                con.Open();

                SqlCommand command = new SqlCommand(UserExistsCommand(userid, @EncryptedPassword(txtOldPassword.Text)), con);
                command.ExecuteNonQuery();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.FieldCount == 0)
                {
                    divError.Attributes.Add("style", "display:normal");
                    lblStrength.Text = "Incorrect Old Password combination!";
                    return;
                }
                reader.Close();



                if (txtNewPassword.Text != txtConfirm.Text)
                {
                    divError.Attributes.Add("style", "display:normal");
                    lblStrength.Text = "Passwords do not match!";
                    return;
                }

                if (txtNewPassword.Text == txtOldPassword.Text)
                {
                    divError.Attributes.Add("style", "display:normal");
                    lblStrength.Text = "You must enter a NEW password!";
                    return;
                }

                string sCommand = "update LogIn_tbl set Password = @Password where User_ID=" + userid + ";";
                using (SqlCommand command2 = new SqlCommand(sCommand, con))
                {
                    command2.Parameters.AddWithValue("Password", EncryptedPassword(txtNewPassword.Text));
                    command2.ExecuteNonQuery();
                }

                sCommand = "update PasswordRecovery_tbl set RecoveryQuestion = @RecoveryQuestion, RecoveryAnswer = @RecoveryAnswer where User_ID=" + int.Parse(Session["LoggedInUserID"].ToString()) + ";";
                using (SqlCommand command3 = new SqlCommand(sCommand, con))
                {
                    command3.Parameters.AddWithValue("@RecoveryQuestion", ddlQuestions.SelectedValue.ToString());
                    command3.Parameters.AddWithValue("@RecoveryAnswer", txtAnswer.Text);
                    command3.ExecuteNonQuery();
                }

                con.Close();
            }

            Response.Redirect("~/Views/Profile.aspx?User_ID=" + userid);

        }
        catch 
        {
            Response.Redirect("~/Views/ChangePassword.aspx");
        }
    }
}