using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class startup : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            HttpCookie users_info = Request.Cookies["users"];
            Menu menu = Master.FindControl("Menu1") as Menu;

            if (users_info != null)
            {
                int user_id = int.Parse(Request.Cookies["users"]["user_id"].ToString());
                string user_name = Request.Cookies["users"]["user_name"].ToString();
            }
            
           
            // Check if the user is logged in
            if (menu != null)
            {
                // User is logged in, show the menu
                menu.Visible = true;
            }
            else
            {
                // User is not logged in, hide the menu
                menu.Visible = false;
            }
        }
    }
}