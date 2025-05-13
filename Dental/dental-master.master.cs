using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class dental_master : System.Web.UI.MasterPage
{
    public void ShowMenu()
    {
        // Show the menu
        Menu1.Visible = true;
    }
    public void HideMenu()
    {
        // Hide the menu
        Menu1.Visible = false;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    // Check if the user is logged in
        //    if (Session["UserName"] != null)
        //    {
        //        // User is logged in, show the menu
        //        ShowMenu();
        //    }
        //    else
        //    {
        //        // User is not logged in, hide the menu
        //        HideMenu();
        //    }
        //}
        
    }

    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {

    }
}
