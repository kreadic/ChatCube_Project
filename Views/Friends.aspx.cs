using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Views_Friends : System.Web.UI.Page
{
    ConStringProvider Connection;
    protected void Page_Load(object sender, EventArgs e)
    {
            Connection = new ConStringProvider(Server.MapPath("/App_Data/ChatCubeDatabase.mdf"));

            if (Session["LoggedInUserID"] == null)
                Response.Redirect("~/Views/Login.aspx");

            LoadPeople();

    }
    private void LoadPeople()
    {
        System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath("/Content/Images/Hexagon.png"));

        MemoryStream m = new MemoryStream();
        image.Save(m, image.RawFormat);
        byte[] imageArray = m.ToArray();

        using (SqlConnection con = new SqlConnection(Connection.ConnectionString))
        {
            con.Open();
            SqlCommand command = new SqlCommand("select Users_tbl.User_ID,FirstName,Surname,Gender,Friend_ID,Description,(select Firstname from Users_tbl where User_ID = Friend_ID) as 'FriendName',(select Surname from Users_tbl where User_ID = Friend_ID) as 'FriendSurname' from Users_tbl full outer join Friends_tbl" +
                " on Users_tbl.User_ID = Friends_tbl.User_ID full outer join Invitation_tbl on Friends_tbl.Invitation_ID = Invitation_tbl.Invite_ID", con);
            command.ExecuteNonQuery();
            SqlDataReader reader = command.ExecuteReader();
            List<string> friends = new List<string>();
            List<string> sentRequests = new List<string>();
            List<string> receivedRequests = new List<string>();
            List<User> registeredPeople = new List<User>();
            while (reader.Read())
            {
                try//the method will try to read a null Friend_ID and throw an exception
                {

                    if (reader.GetInt32(4) == int.Parse(Session["LoggedInUserID"].ToString()))//if current logged user is a Friend to user in database
                    {
                        try
                        {
                            if (reader.GetString(5) == "Accepted")//Invite accepted
                            {
                                LoadFriends(reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetString(2));
                                friends.Add(reader.GetInt32(0).ToString());
                            }//Invite accepted
                            else if (reader.GetString(5) == "Sent")//Invite sent
                            {
                                ulPending.InnerHtml += "<li class='list-group-item list-group-item-action' style='color: #7c2020'>" +
                                                       "<a href='#' >" +
                                                       "<img ID='NavPhoto" + reader.GetInt32(4) + "' class='border-light rounded rounded-circle text-center' src='data:image/" + image.GetType().ToString() + ";base64," + Convert.ToBase64String(imageArray) + "' style='background-color:white; Width:50px; Height:50px; margin-right:10px' />" +
                                                       reader.GetString(1) + " " + reader.GetString(2) +
                                                       "<span class='float-right fas  fa-check' style='color:green'></span></a>" +
                                                       "</li>";
                                sentRequests.Add(reader.GetInt32(0).ToString());

                            }//Invite sent 
                            //else if (reader.GetString(5) == "Received")//Invite received
                            //{
                            //    LoadReceivedInvites(reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetString(2));

                            //}//Invite received else

                        }
                        catch
                        {
                            try
                            {
                                if (reader.GetInt32(0) != int.Parse(Session["LoggedInUserID"].ToString())
                               && !friends.Contains(reader.GetInt32(0).ToString())
                               && !registeredPeople.Select(p => p.User_ID).Contains(reader.GetInt32(0))
                               && !sentRequests.Contains(reader.GetInt32(0).ToString())
                               && !receivedRequests.Contains(reader.GetInt32(0).ToString()))
                                {

                                    registeredPeople.Add(new User() { User_ID = reader.GetInt32(0), FirstName = reader.GetString(1), Surname = reader.GetString(2) });
                                }
                            }
                            catch
                            {
                            }
                        }

                    }
                    else if (reader.GetInt32(0) == int.Parse(Session["LoggedInUserID"].ToString()))//if current logged user is a Friend to user in database
                    {
                        if (reader.GetString(5) == "Sent")//Invite was sent to logged in user i.e Received
                        {
                            LoadReceivedInvites(reader.GetInt32(4).ToString(), reader.GetString(6), reader.GetString(7));//hence we load it in the received list
                            receivedRequests.Add(reader.GetInt32(4).ToString());
                        }//Invite accepted
                        //else if (reader.GetString(5) == "Sent")//Invite sent
                        //{
                        //    ulPending.InnerHtml += "<li class='list-group-item list-group-item-action' style='color: #7c2020'>" +
                        //                           "<a href='#' >" +
                        //                           "<img ID='NavPhoto" + reader.GetInt32(4) + "' class='border-light rounded rounded-circle text-center' src='data:image/" + image.GetType().ToString() + ";base64," + Convert.ToBase64String(imageArray) + "' style='background-color:white; Width:50px; Height:50px; margin-right:10px' />" +
                        //                           reader.GetString(1) + " " + reader.GetString(2) +
                        //                           "<span class='float-right fas  fa-check' style='color:yellow'></span></a>" +
                        //                           "</li>";

                        //}//Invite sent 
                        else if (reader.GetString(5) == "Accepted")//Invite received
                        {
                            LoadFriends(reader.GetInt32(4).ToString(), reader.GetString(6), reader.GetString(7));
                            friends.Add(reader.GetInt32(4).ToString());
                        }//Invite received else

                    }
                    else
                    {

                        try
                        {
                            if (reader.GetInt32(0) != int.Parse(Session["LoggedInUserID"].ToString())
                               && !friends.Contains(reader.GetInt32(0).ToString())
                               && !registeredPeople.Select(p => p.User_ID).Contains(reader.GetInt32(0))
                               && !sentRequests.Contains(reader.GetInt32(0).ToString())
                               && !receivedRequests.Contains(reader.GetInt32(0).ToString()))
                            {

                                registeredPeople.Add(new User() { User_ID = reader.GetInt32(0), FirstName = reader.GetString(1), Surname = reader.GetString(2) });
                            }
                        }
                        catch
                        {
                        }
                    }


                }//Null Friend_ID try
                catch //skip
                {

                    try
                    {
                        if (reader.GetInt32(0) != int.Parse(Session["LoggedInUserID"].ToString())
                               && !friends.Contains(reader.GetInt32(0).ToString())
                               && !registeredPeople.Select(p => p.User_ID).Contains(reader.GetInt32(0))
                               && !sentRequests.Contains(reader.GetInt32(0).ToString())
                               && !receivedRequests.Contains(reader.GetInt32(0).ToString()))
                        {

                            registeredPeople.Add(new User() { User_ID = reader.GetInt32(0), FirstName = reader.GetString(1), Surname = reader.GetString(2) });
                        }
                    }
                    catch//skip
                    {
                    }

                }


            }//while

            reader.Close();
            con.Close();

            foreach (string userid in registeredPeople.Select(p => p.User_ID.ToString()).Except(friends).Except(sentRequests).Except(receivedRequests).ToList())
            {
                User user = registeredPeople.SingleOrDefault(u => u.User_ID == int.Parse(userid));
                LoadRegisteredPeople(user.User_ID.ToString(), user.FirstName, user.Surname);
            }
        }

    }
    private void LoadFriends(string userid, string firstname, string surname)
    {
        ulFriends.InnerHtml += "<li class='list-group-item list-group-item-action' style='color: #7c2020'>" +
                                "<a href='#' >" +
                                "<img ID='NavPhoto" + userid + "' class='border-light rounded rounded-circle text-center' src='/Content/Images/default_Photo.jpg' style='background-color:white; Width:50px; Height:50px; margin-right:10px' />" +
                                firstname + " " + surname +
                                "<span class='float-right fas  fa-check' style='color:green'></span></a>" +
                                "</li>";
    }

    private void LoadReceivedInvites(string userid, string firstname, string surname)
    {
        HtmlGenericControl li = new HtmlGenericControl("li");
        li.Attributes.Add("class", "list-group-item list-group-item-action");
        li.Attributes.Add("style", "color: #7c2020");
        li.InnerHtml = "<a href='#' >" +
                    "<img ID='NavPhoto" + userid + "' class='border-light rounded rounded-circle text-center' src='/Content/Images/default_Photo.jpg' style='background-color:white; Width:50px; Height:50px; margin-right:10px' />" +
                    firstname + " " + surname +
                    "<span class='float-right fas fa-circle' style='color:green'></span></a>";

        Button Accept = new Button();
        Accept.ID = "A" + userid;
        Accept.CssClass = "btn btn-outline-success float-right m-2 font-weight-bolder";
        Accept.Text = "Accept Invite";
        Accept.Click += new EventHandler(Accept_Click);
        li.Controls.Add(Accept);

        Button Reject = new Button();
        Reject.ID = "R" + userid;
        Reject.CssClass = "btn btn-outline-danger float-right m-2 font-weight-bolder";
        Reject.Text = "Reject Invite";
        Reject.Click += new EventHandler(Reject_Click);
        li.Controls.Add(Reject);

        ulReceivedInvites.Controls.Add(li);

    }

    private void LoadRegisteredPeople(string userid,string firstname,string surname)
    {
        HtmlGenericControl li = new HtmlGenericControl("li");
        li.Attributes.Add("class", "list-group-item list-group-item-action");
        li.Attributes.Add("style", "color: #7c2020");
        li.InnerHtml = "<a href='#' >" +
                    "<img ID='NavPhoto" + userid + "' class='border-light rounded rounded-circle text-center' src='/Content/Images/default_Photo.jpg' style='background-color:white; Width:50px; Height:50px; margin-right:10px' />" +
                    firstname + " " + surname +
                    "<span class='float-right fas fa-circle' style='color:green'></span></a>";

        Button btn = new Button();
        btn.ID = userid;
        btn.CssClass = "btn btn-outline-primary float-right m-2";
        btn.Text = "Send Invite";
        btn.Click += new EventHandler(Invite_Click);

        li.Controls.Add(btn);
        ulPeople.Controls.Add(li);
        //ulPeople.InnerHtml += "<li class='' style='color: #7c2020'>" +
        //            "<a href='#' >" +
        //            "<img ID='NavPhoto" + userid + "' class='border-light rounded rounded-circle text-center' src='/Content/Images/default_Photo.jpg' style='background-color:white; Width:50px; Height:50px; margin-right:10px' />" +
        //            firstname + " " + surname +
        //            "<span class='float-right fas fa-circle' style='color:green'></span></a>" +
        //            "<button id='" + userid + "' type='button' runat='server' class='btn btn-outline-primary float-right m-2' onclick='Invite_Click' ></button>";
    }

    protected void Invite_Click(object sender, EventArgs e)//Sending Invite Invidation_ID = 3
    {
        try
        {
            var btn = (Button)sender;
            int id = int.Parse(btn.ID);//Remove "Invite"

            using (SqlConnection con = new SqlConnection(Connection.ConnectionString))
            {
                string sCommand = "insert into Friends_tbl(User_ID,Friend_ID,Invitation_ID) values(@User_ID,@Friend_ID,@Invitation_ID);";
                using (SqlCommand command = new SqlCommand(sCommand, con))
                {
                    con.Open();
                    command.Parameters.AddWithValue("@User_ID", id);
                    command.Parameters.AddWithValue("@Friend_ID", int.Parse(Session["LoggedInUserID"].ToString()));
                    command.Parameters.AddWithValue("@Invitation_ID", 3);

                    command.ExecuteNonQuery();

                    btn.Text = "Invitation Sent";
                    btn.Attributes.Add("style", "background-color:green");

                    con.Close();

                }
            }
            Response.Redirect("~/Views/Friends.aspx");

        }
        catch (Exception)
        {
            Response.Redirect("/Views/Friends.aspx");

        }
    }

    protected void Accept_Click(object sender, EventArgs e)//Accepting Invites Invidation_ID = 2
    {
        try
        {
            var btn = (Button)sender;
            int id = int.Parse(btn.ID.Remove(0, 1));//Remove "Accept"

            using (SqlConnection con = new SqlConnection(Connection.ConnectionString))
            {
                con.Open();
                string sCommand = "update Friends_tbl set Invitation_ID = @Invitation_ID where User_ID = @User_ID and Friend_ID = @Friend_ID";
                using (SqlCommand command = new SqlCommand(sCommand, con))
                {
                    command.Parameters.AddWithValue("@User_ID", int.Parse(Session["LoggedInUserID"].ToString()));
                    command.Parameters.AddWithValue("@Friend_ID", id);
                    command.Parameters.AddWithValue("@Invitation_ID", 1);

                    command.ExecuteNonQuery();
                }
                con.Close();
            }
            Response.Redirect("~/Views/Friends.aspx");

        }
        catch
        {
            Response.Redirect("/Views/Friends.aspx");

        }
    }

    protected void Reject_Click(object sender, EventArgs e)//Accepting Invites Invidation_ID = 4 or Delete the row
    {
        try
        {
            var btn = (Button)sender;
            int id = int.Parse(btn.ID.Remove(0, 1));//Remove "Invite"

            using (SqlConnection con = new SqlConnection(Connection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Friends_tbl WHERE User_ID = @User_ID and Friend_ID = @Friend_ID", con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@User_ID", int.Parse(Session["LoggedInUserID"].ToString()));
                    cmd.Parameters.AddWithValue("@Friend_ID", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close(); ;
                }
            }
            Response.Redirect("~/Views/Friends.aspx");

        }
        catch (Exception)
        {
            Response.Redirect("/Views/Friends.aspx");

        }
    }
}