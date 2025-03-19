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

    protected void Button1_Click(object sender, EventArgs e)
    {
        string user = TXT_USERNAME.Text;
        string pass = TXT_PASSWORD.Text;
        string name = db.select_value ("select username from users where username = '" + user.Trim() + "'","username");
        string password = db.select_value("select password from users where password = '" + pass.Trim() + "'", "password");
        if (name !="")
        {
            if (password != "")
            {
                Response.Redirect("startup.aspx");
            }
            else
            {
                Response.Write("<script>alert('woring password');</script>");
                return;
            }
        }
        else
        {
            Response.Write("<script>alert('wrong username');</script>");
            return;
        }
        
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("registration.aspx");
    }
}