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


public partial class type : System.Web.UI.Page
{
    public void fill_grd()
    {
        string select = "select [types].id, types.name, categories.name as category_name from [types] inner join categories on types.category_id = categories.id";
        db.select(select, GridView1);
        ViewState.Add("ds", db.ds1);
    }

    public void fill_category()
    {
        string query = "select * from categories";
        db.select_combo(query, DRP_CATEG);
        DRP_CATEG.Items.Insert(0, "Selected Items");
    }

    public bool validation()
    {
        string name = TXT_NAME.Text;
        string name_check = db.select_value("select name from types where name = N'" + name.Trim() + "' and category_id = " + DRP_CATEG.Text + "", "name");

        
        string categ_check = db.select_value("select category_id from types where name = N'" + name.Trim() + "' and category_id = " + DRP_CATEG.SelectedItem + "", "category_id");

        
        if (TXT_NAME.Text == "" || DRP_CATEG.SelectedIndex <= 0)
        {
            return false; // Not Good Validation data will duplicated
        }
        else
        {
            if (name_check != "")
            {
                return false;
            }
            else
            {
                return true;
            }
           //return true; // Good Validation there is no duplicated
        }

    }

    public void max_id()
    {
        string query = "select  max (id)+1 as id from types";
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
            fill_category();
            max_id();
            fill_grd();
        }
        
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        bool vali = validation();
        if (validation() == true)
        {
            string insert = "INSERT INTO types ([id], [name],category_id) VALUES(" + TXT_ID.Text + ", N'" + TXT_NAME.Text + "',"+ DRP_CATEG.SelectedValue +")";
            db.insert(insert);
        }

        else
        {
            Response.Write("<script>alert('please enter name or chooes Category');</script>");
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
        db.deleting_grid(db.ds1, "types", GridView1, e.RowIndex);
        fill_grd();
        max_id();
    }
}