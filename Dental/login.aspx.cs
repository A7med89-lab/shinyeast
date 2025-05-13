using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;
using System.Configuration;

public partial class login : System.Web.UI.Page
{
    database db = new database();
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void BTN_LOGIN_Click(object sender, EventArgs e)
    {   
        string name = db.select_value("select name from users where username = '" + TXT_USERNAME.Text.Trim() + "' and password = '" + TXT_PASSWORD.Text.Trim() + "'", "name");
        string username = db.select_value("select username from users where username = '" + TXT_USERNAME.Text.Trim() + "'", "username");
        string password = db.select_value("select password from users where password = '" + TXT_PASSWORD.Text.Trim() + "'", "password");
        if (name != "")
        {
            if (password != "")
            {
                string select = "select id from users where username = '" + username + "' and password = '" + password + "' and name = '" + name + "'";
                string user_id = db.select_value(select, "id");

                //string session_name = Session["UserName"] as string;
                // Create a new cookie
                HttpCookie userCookie = new HttpCookie("users");
                //userCookie.Secure = true;          

                userCookie["user_id"] = user_id;
                userCookie["user_name"] = username;
                userCookie["name"] = name;

                // Optionally set expiration time
                userCookie.Expires = DateTime.Now.AddMinutes(10);

                // Add cookie to the response
                Response.Cookies.Add(userCookie);

                //Response.Redirect("registration.aspx");
                Response.Redirect("startup.aspx");
            }
            else
            {
                Response.Write("<script>alert('wrong password');</script>");
                return;
            }
        }
        else
        {
            Response.Write("<script>alert('wrong username');</script>");
            return;
        }
    }
}