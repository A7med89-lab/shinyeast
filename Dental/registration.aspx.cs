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


public partial class registration : System.Web.UI.Page
{

    public void fill_grd()
    {
        
        string select = "select id,name,username from users";
        db.select(select, GridView1);
        ViewState.Add("ds", db.ds1);
    }

    public void max_id()
    {
        string query = "select  max (id)+1 as id from users";
        db.select_id(query, TXT_ID);    
        if (TXT_ID.Text == "")
        {
            TXT_ID.Text = 1.ToString();
        }
    }

    public void validation()
    {
        // validate deduplicate username
        // should username and password is entered
    }

    public void clear()
    {
        TXT_ID.Text = "";
        TXT_NAME.Text = "";
        TXT_PASSWORD.Text = "";
        TXT_USERNAME.Text = "";
    }
    database db = new database();

    protected void Page_Load(object sender, EventArgs e)
    {
        if(IsPostBack == false)
        {
            fill_grd();
            max_id();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string insert = "insert into users (id, name, username, password) values ("+TXT_ID.Text+", '"+TXT_NAME.Text+"', '"+TXT_USERNAME.Text+"', '"+TXT_PASSWORD.Text+"')";
        db.insert(insert);
        clear();
        fill_grd();
        max_id();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("login.aspx");
    }
}