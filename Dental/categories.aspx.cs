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

public partial class categories : System.Web.UI.Page
{
    public void fill_grd()
    {
        string select = "select * from categories";
        db.select(select, GridView1);
        ViewState.Add("ds", db.ds1);
    }

    public bool validation()
    {
        string name = TXT_NAME.Text;
        string id_check = db.select_value("select name from categories where name = N'" + name.Trim() + "'", "name");
        if (id_check == "")
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void max_id()
    {
        string query = "select  max (id)+1 as id from categories";
        db.select_id(query, TXT_ID);

        if (TXT_ID.Text == "")
        {
            TXT_ID.Text = 1.ToString();
        }
    }

    public void clear()
    {
        TXT_NAME.Text = "";
    }
    database db = new database();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            max_id();
            fill_grd();
        }
    }

    

    protected void Button1_Click(object sender, EventArgs e)
    {
        bool vali = validation();
        if (validation() == false)
        {
            string insert = "INSERT INTO categories ([id], [name]) VALUES(" + TXT_ID.Text + ", N'" + TXT_NAME.Text + "')";
            db.insert(insert);
        }

        else
        {
            Response.Write("<script>alert('the same name already exist');</script>");
            return;
        }

        clear();
        max_id();
        fill_grd();
        
    }




    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        fill_grd();
        db.ds1 = (DataSet)ViewState["ds"];
        db.deleting_grid(db.ds1, "categories", GridView1, e.RowIndex);
        fill_grd();
        max_id();
    }
}
