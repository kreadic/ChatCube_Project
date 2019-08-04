using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Data.Entity;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Views_Registration : System.Web.UI.Page
{
    ConStringProvider Connection;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Connection = new ConStringProvider(Server.MapPath("/App_Data/ChatCubeDatabase.mdf"));
            if (!IsPostBack)
            {
                if (Session["LoggedInUserID"] == null)
                {
                    EnableSecurityCredentials(true);
                }
                else
                {
                    PopulateInterface();
                }

            }
        }
        catch 
        {
            Response.Redirect("/Views/Registration.aspx");
        }
        
    }
    private void ValidatePassword()
    {
        lblStrength.Text = "";
        Regex passValidator = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!#\$%\^&\*])(?=.{6,})");
        if (!passValidator.IsMatch(txtPassword.Text))
        {
            divError.Attributes.Add("style", "display:normal");
            lblStrength.Text = "Password must have at least 1 uppercase character,\n 1 lowercase character, 1 special character,\n1 number \nand must be at least 6 characters long";
            return;
        }

        if (txtPassword.Text != txtConfirmPassword.Text)
        {
            divError.Attributes.Add("style", "display:normal");
            lblStrength.Text = "Passwords do not match. Please re-enter password";
            txtPassword.Text = txtConfirmPassword.Text = "";
            return;
        }
    }
    private string EncryptedPassword()
    {
        byte[] data = System.Text.Encoding.ASCII.GetBytes(txtPassword.Text);
        data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
        return System.Text.Encoding.ASCII.GetString(data);
    }
    private string GetUserDataCommand()
    {
        return "select * from Users_tbl where User_ID='"+int.Parse(Session["LoggedInUserID"].ToString())+"'";
    }
    private void PopulateInterface()
    {
        using (SqlConnection con = new SqlConnection(Connection.ConnectionString))
        {
            con.Open();
            SqlCommand command = new SqlCommand(GetUserDataCommand(), con);
            command.ExecuteNonQuery();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                EnableSecurityCredentials(false);
                txtFirstname.Text = reader.GetString(1);
                txtMiddleName.Text = reader.GetString(2);
                txtSurname.Text = reader.GetString(3);
                CheckGender(reader.GetBoolean(4) ? "cbxMale" : "cbxFemale");//if user.Gender = true = Male

                txtEmail.Text = reader.GetString(5); ;
                txtEmail.Enabled = false; //The user must cannot edit their email address

                txtCellNumber.Text = reader.GetString(6);
                txtBio.Text = reader.GetString(7);
                //imgProfilePhoto.ImageUrl = reader.GetSqlBytes(8) != null ? "data:image/" + reader.GetString(9) + ";base64," + Convert.ToBase64String(reader.GetSqlBytes(8).Buffer) : Server.MapPath("~/Content/Images/default_Photo.jpg"); ;
            }
            reader.Close();
            con.Close();
        }
        
    }
    private void EnableSecurityCredentials(bool EnabledDisabled)
    {
        rfvPassword.Enabled = rfvConfirmPassword.Enabled =
        rfvQuestions.Enabled = rfvAnswer.Enabled = EnabledDisabled;

        if (EnabledDisabled)
            divSecurity.Attributes.Add("style", "display:normal");
        else
            divSecurity.Attributes.Add("style", "display:none");

    }
    protected void cbxMale_CheckedChanged(object sender, EventArgs e)
    {
        var cbx = (CheckBox)sender;
        CheckGender(cbx.ID);
    }
    private void CheckGender(string ID)
    {
        if (ID == "cbxMale")
        {
            cbxMale.Checked = true;
            cbxFemale.Checked = false;
        }
        else if (ID == "cbxFemale")
        {
            cbxMale.Checked = false;
            cbxFemale.Checked = true;
        }
        else
        {
            cbxMale.Checked = cbxFemale.Checked = false;
        }
    }
    private void ShowMessage(string message)
    {
        divError.Attributes.Add("style", "display:normal");
        lblStrength.Text = message;
    }
    private string EmailExistsCommand(string email)
    {
        return "Select top 1 EmailAddress from Users_tbl where EmailAddress='" + email + "'";
    }
    private void IsEmailUnique()
    {
        using (SqlConnection con = new SqlConnection(Connection.ConnectionString))
        {
            con.Open();
            SqlCommand command = new SqlCommand(EmailExistsCommand(txtEmail.Text.ToLower()), con);
            command.ExecuteNonQuery();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                if (reader.FieldCount > 0)
                {
                    divError.Attributes.Add("style", "display:normal");
                    lblStrength.Text = "This email already exists";
                    lblStrength.Focus();

                return;
                }
                
            }
            reader.Close();
            con.Close();
        }
    }
    private void GenderChosen()
    {
        lblStrength.Text = "";
        if (!cbxFemale.Checked && !cbxMale.Checked)
        {
            divError.Attributes.Add("style", "display:normal");
            lblStrength.Text = "Please Choose a Gender!";
            return;
        }
    }
    //private byte[] GetImage()
    //{
    //    System.Drawing.Image image;
    //    try
    //    {
    //        //image = System.Drawing.Image.FromStream(fuProfilePhoto.FindControl.ha);
    //    }
    //    catch
    //    {
    //        image = System.Drawing.Image.FromFile(Server.MapPath("~/Content/Images/default_Photo.jpg"));
    //    }
    //    MemoryStream m = new MemoryStream();
    //    image.Save(m, image.RawFormat);
    //    byte[] imageArray = m.ToArray();

    //    return imageArray;
    //}
    protected void btnCloseError_Click(object sender, EventArgs e)
    {
        divError.Attributes.Add("style", "display:none");
    }
    protected void lbtnRegister_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValid)
            {
                ValidatePassword();
                GenderChosen();
                IsEmailUnique();

                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    ShowMessage("The Password and confrimation do not match!");
                    return;

                }
                int userid = 0;
                using (SqlConnection con = new SqlConnection(Connection.ConnectionString))
                {

                    con.Open();
                    //try
                    //{
                    string sCommand = "";
                    //if (fuProfilePhoto.)
                    //{
                    //sCommand = "INSERT INTO Users_tbl(FirstName, MiddleName, Surname,Gender,EmailAddress,CellNumber,Bio,Photo,Photo_Type) VALUES(@FirstName, @MiddleName, @Surname, @Gender, @EmailAddress, @CellNumber,@Bio,@Photo,@Photo_Type);";
                    //}
                    //else
                    //{
                    if (Session["LoggedInUserID"] == null)
                    {
                        sCommand = "INSERT INTO Users_tbl(FirstName, MiddleName, Surname,Gender,EmailAddress,CellNumber,Bio) VALUES(@FirstName, @MiddleName, @Surname, @Gender, @EmailAddress, @CellNumber, @Bio); SELECT SCOPE_IDENTITY();";
                    }
                    else
                    {
                        sCommand = "update Users_tbl set FirstName = @FirstName, MiddleName = @MiddleName, Surname = @Surname,Gender = @Gender,EmailAddress = @EmailAddress,CellNumber = @CellNumber,Bio = @Bio where [User_ID] = @User_ID";
                    }
                    //}

                    using (SqlCommand command = new SqlCommand(sCommand, con))
                    {
                        command.Parameters.AddWithValue("@FirstName", txtFirstname.Text);
                        command.Parameters.AddWithValue("@MiddleName", txtMiddleName.Text);
                        command.Parameters.AddWithValue("@Surname", txtSurname.Text);
                        command.Parameters.AddWithValue("@Gender", cbxMale.Checked);
                        command.Parameters.AddWithValue("@EmailAddress", txtEmail.Text.ToLower());
                        command.Parameters.AddWithValue("@CellNumber", txtCellNumber.Text);
                        command.Parameters.AddWithValue("@Bio", txtBio.Text);
                        if (Session["LoggedInUserID"] != null)
                            command.Parameters.AddWithValue("@User_ID", int.Parse(Session["LoggedInUserID"].ToString()));
                        //if (fuProfilePhoto.HasFile)          
                        //{                                    
                        //    command.Parameters.AddWithValue("Photo", GetImage());
                        //    command.Parameters.AddWithValue("Photo_Type", fuProfilePhoto.PostedFile.ContentType);

                        //}                                    

                        //command.ExecuteNonQuery();

                        userid = Convert.ToInt32(command.ExecuteScalar());
                    }

                    if (Session["LoggedInUserID"] == null)
                    {
                        sCommand = "insert into LogIn_tbl(User_ID,Password) values(@User_ID,@Password);";
                        using (SqlCommand command = new SqlCommand(sCommand, con))
                        {
                            command.Parameters.AddWithValue("@User_ID", userid);
                            command.Parameters.AddWithValue("@Password", EncryptedPassword());


                            command.ExecuteNonQuery();
                        }

                        sCommand = "insert into PasswordRecovery_tbl(User_ID,RecoveryQuestion,RecoveryAnswer) values(@User_ID,@RecoveryQuestion,@RecoveryAnswer);";
                        using (SqlCommand command = new SqlCommand(sCommand, con))
                        {
                            command.Parameters.AddWithValue("@User_ID", userid);
                            command.Parameters.AddWithValue("@RecoveryQuestion", ddlQuestions.SelectedValue.ToString());
                            command.Parameters.AddWithValue("@RecoveryAnswer", txtAnswer.Text);

                            command.ExecuteNonQuery();
                        }
                    }
                    //}
                    //catch
                    //{
                    //    Console.WriteLine("Count not insert.");
                    //}
                    con.Close();

                }


                Response.Redirect("~/Views/Profile.aspx");
            }
        }
        catch 
        {
            Response.Redirect("/Views/Profile.aspx");
        }
       
        
    }

    protected void ibntProfilePhoto_Click(object sender, ImageClickEventArgs e)
    {
        FileUpload fileUpload = new FileUpload();
    }

    protected void btnUploadProfilePhoto_Click(object sender, EventArgs e)
    {
        bool FileOK = false;

        //Session["ProfilePhoto"] = fuProfilePhoto.FileName;

        String FileExtension = Path.GetExtension(Session["ProfilePhoto"].ToString()).ToLower();

        String[] allowedExtensions = { ".png", ".jpeg", ".jpg", ".gif" };

        for (int i = 0; i < allowedExtensions.Length; i++)
        {
            if (FileExtension == allowedExtensions[i])
            {
                FileOK = true;
            }
        }


        if (FileOK)
        {
            try
            {
                //System.Drawing.Image image = System.Drawing.Image.FromStream(fuProfilePhoto.FileContent);
                //MemoryStream m = new MemoryStream();
                //image.Save(m, image.RawFormat);
                //byte[] imageArray = m.ToArray();

                //imgProfilePhoto.ImageUrl = "data:image/" + image.GetType().ToString() + ";base64," + Convert.ToBase64String(imageArray);

            }
            catch (Exception ex)
            {
                divError.Attributes.Add("style", "display:normal");
                lblStrength.Text = "File could not be uploaded." + ex.Message.ToString();

            }
        }
        else
        {
            divError.Attributes.Add("style", "display:normal");
            lblStrength.Text = "Cannot accept files of this type.";
        }

    }


    protected void fuProfilePhoto_UploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
    {
        imgProfilePhoto.ImageUrl = "data:"+ e.ContentType + ";base64," + Convert.ToBase64String(e.GetContents());
    }


    protected void txtEmail_TextChanged(object sender, EventArgs e)
    {
        //IsEmailUnique();
    }
}