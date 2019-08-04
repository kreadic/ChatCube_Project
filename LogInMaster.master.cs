using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LogInMaster : System.Web.UI.MasterPage
{

    ConStringProvider Connection;
    protected void Page_Load(object sender, EventArgs e)
    {
        Connection = new ConStringProvider(Server.MapPath("/App_Data/ChatCubeDatabase.mdf"));
        if (!IsPostBack)
        {
            if (Session["LoggedInUserID"] != null)
            {
                using (SqlConnection con = new SqlConnection(Connection.ConnectionString))
                {
                    con.Open();

                    int userid = int.Parse(Session["LoggedInUserID"].ToString());
                    SqlCommand command = new SqlCommand("select FirstName,Surname from Users_tbl where User_ID='" + userid + "'", con);
                    command.ExecuteNonQuery();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        btnLoggedInUser.Text = reader.GetString(0) + " " + reader.GetString(1);
                    }
                    reader.Close();
                    con.Close();
                }

            }
            else
            {
                Session.Remove("LoggedInUserID");
                Response.Redirect("~/Views/LogIn.aspx");

            }

        }
    }


    protected void btnLogOut_Click(object sender, EventArgs e)
    {
        Session.Remove("LoggedInUserID");
        Response.Redirect("~/Views/LogIn.aspx");
    }
}
