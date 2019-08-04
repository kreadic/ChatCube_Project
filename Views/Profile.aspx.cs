using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TableDependency.SqlClient;
using System.Data.SqlClient;

public partial class Views_Profile : System.Web.UI.Page
{

    ConStringProvider Connection;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Connection = new ConStringProvider(Server.MapPath("/App_Data/ChatCubeDatabase.mdf"));
            if (!IsPostBack)
            {
                try
                {
                    PopulateInterface();
                }
                catch
                {
                    Session.RemoveAll();
                    Response.Redirect("~/Views/LogIn.aspx");
                }
            }
        }
        catch
        {
            Response.Redirect("/Views/Profile.aspx");

        }
    }

    private void PopulateInterface()
    {
        
            using (SqlConnection con = new SqlConnection(Connection.ConnectionString))
            {
                con.Open();

            int userid = int.Parse(Session["LoggedInUserID"].ToString());
                SqlCommand command = new SqlCommand("select * from Users_tbl where User_ID='"+userid+"'", con);
                command.ExecuteNonQuery();
                SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                lblFullName.Text = reader.GetString(1) + " " + reader.GetString(3);
                lblGender.CssClass = reader.GetBoolean(4) ? "fas fa-male" : "fas fa-female";
                lblEmail.Text = reader.GetString(5);
                lblCellNumber.Text = reader.GetString(6);
                lblBio.Text = reader.GetString(7);
            }
                reader.Close();
                con.Close();
            }
    }

    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        int userid = int.Parse(Session["LoggedInUserID"].ToString());
        Response.Redirect("~/Views/ChangePassword.aspx?User_ID=" + userid);
    }
}
