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

public partial class colors : System.Web.UI.Page
{
    public void fill_grd()
    {
        //inner join
        //string select = "select colors.id, colors.name, types.name as [type_name], sizes.name , categories.name as category_name from colors inner join sizes on colors.size_id = sizes.id  inner join [types] on colors.[type_id] = types.id inner join categories on colors.category_id = categories.id";
        //left join
        string select = "select colors.id, colors.name, categories.name as category_name, types.name as [type_name], sizes.name as size_name from colors left join sizes on colors.size_id = sizes.id  left join [types] on colors.[type_id] = types.id inner join categories on colors.category_id = categories.id";

        db.select(select, GridView1);
        ViewState.Add("ds", db.ds1);
    }

    public void fill_category()
    {
        string query = "select * from categories";
        db.select_combo(query, DRP_CATEG);
        DRP_CATEG.Items.Insert(0, "Selected Items");
    }

    public void fill_type()
    {
        

        if (DRP_CATEG.SelectedIndex > 0)
        {
            int t_id = int.Parse(DRP_CATEG.SelectedValue);
            string query_type = "select * from types where category_id=" + t_id + " ";
            db.select_combo(query_type, DRP_TYPE);
            if (DRP_TYPE.Items.Count != 0)  // if for insert "selected_item" only if DRP_size has data
            {
                DRP_TYPE.Items.Clear();
                db.select_combo(query_type, DRP_TYPE);
                DRP_TYPE.Items.Insert(0, "Selected Items");
            }
        }
    }
    public void fill_size()
    {
        
        if (DRP_TYPE.SelectedIndex > 0 && DRP_CATEG.SelectedIndex > 0)
        {
            
            int t_id = int.Parse(DRP_TYPE.SelectedValue);
            int c_id = int.Parse(DRP_CATEG.SelectedValue);
            string query = "select * from sizes where category_id = " + c_id + " and type_id=" + t_id + " ";
            db.select_combo(query, DRP_SIZE);
            if (DRP_SIZE.Items.Count != 0)  // if for insert "selected_item" only if DRP_size has data
            {
                DRP_SIZE.Items.Clear();
                db.select_combo(query, DRP_SIZE);
                DRP_SIZE.Items.Insert(0, "Selected Items");
            }
        }

    }

    public void fill_size_category()
    {
        string query;
        if (DRP_CATEG.SelectedIndex > 0)
        {
            query = "select * from sizes where category_id = " + DRP_CATEG.SelectedValue + " and type_id is null";
            db.select_combo(query, DRP_SIZE);

            if (DRP_SIZE.Items.Count != 0)  //  for insert "selected_item" text only if DRP_size has data
            {
                DRP_SIZE.Items.Clear();
                db.select_combo(query, DRP_SIZE);
                DRP_SIZE.Items.Insert(0, "Selected Items");
            }

        }

    }

    public bool validation()
    {
        string name = TXT_NAME.Text;
        string name_check;
        if (DRP_TYPE.SelectedIndex <=0 && DRP_SIZE.SelectedIndex <= 0)
        {
            name_check = db.select_value("select name from colors where name = N'" + name.Trim() + "' and category_id = " + DRP_CATEG.Text + "", "name");
        }
        else if (DRP_SIZE.SelectedIndex <= 0)
        {
            name_check = db.select_value("select name from colors where name = N'" + name.Trim() + "' and category_id = " + DRP_CATEG.Text + " and type_id = " + DRP_TYPE.Text + "", "name");
        }
        else if (DRP_TYPE.SelectedIndex <= 0)
        {
            name_check = db.select_value("select name from colors where name = N'" + name.Trim() + "' and category_id = " + DRP_CATEG.Text + " and  size_id = " + DRP_SIZE.Text + " ", "name");
        }
        else
        {
            name_check = db.select_value("select name from colors where name = N'" + name.Trim() + "' and category_id = " + DRP_CATEG.Text + " and type_id = " + DRP_TYPE.Text + " and size_id = " + DRP_SIZE.Text + " ", "name");
        } 
        //string categ_check = db.select_value("select category_id from types where name = N'" + name.Trim() + "' and category_id = " + DRP_CATEG.SelectedItem + "", "category_id");
        //string type_check = db.select_value("select type_id from types where name = N'" + name.Trim() + "' and category_id = " + DRP_CATEG.SelectedItem + "", "category_id");
        //string size_check = db.select_value("select size_id from types where name = N'" + name.Trim() + "' and category_id = " + DRP_CATEG.SelectedItem + "", "category_id");
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
        string query = "select  max (id)+1 as id from colors";
        db.select_id(query, TXT_ID);

        if (TXT_ID.Text == "")
        {
            TXT_ID.Text = 1.ToString();
        }
    }

    public void clear()
    {
        TXT_NAME.Text = "";
        fill_category();
        
    }
    database db = new database();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            max_id();
            fill_grd();
            fill_category();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        bool vali = validation();

        if (validation() == true)
        {
            if (DRP_TYPE.SelectedIndex <= 0 && DRP_SIZE.SelectedIndex <= 0)
            {
                string insert = "INSERT INTO colors ([id], [name],category_id) VALUES(" + TXT_ID.Text + ", N'" + TXT_NAME.Text + "'," + DRP_CATEG.SelectedValue + ")";
                db.insert(insert);
            }

            else if (DRP_TYPE.SelectedIndex > 0 && DRP_SIZE.SelectedIndex <= 0)
            {
                string insert = "INSERT INTO colors ([id], [name],category_id,type_id ) VALUES(" + TXT_ID.Text + ", N'" + TXT_NAME.Text + "'," + DRP_CATEG.SelectedValue + ", " + DRP_TYPE.SelectedValue + ")";
                db.insert(insert);


            }

            else if (DRP_TYPE.SelectedIndex <= 0 && DRP_SIZE.SelectedIndex > 0)
            {
                string insert = "INSERT INTO colors ([id], [name],category_id,size_id ) VALUES(" + TXT_ID.Text + ", N'" + TXT_NAME.Text + "'," + DRP_CATEG.SelectedValue + ", " + DRP_SIZE.SelectedValue + ")";
                db.insert(insert);


            }
            else
            {
                string insert = "INSERT INTO colors ([id], [name],category_id, type_id, size_id) VALUES(" + TXT_ID.Text + ", N'" + TXT_NAME.Text + "'," + DRP_CATEG.SelectedValue + "," + DRP_TYPE.SelectedValue + ", " + DRP_SIZE.SelectedValue + ")";
                db.insert(insert);
            }

        }

        else
        {
            Response.Write("<script>alert('please enter name or new name , should chooes Category');</script>");
            return;
        }

        clear();
        max_id();
        fill_grd();
    }

    protected void DRP_CATEG_SelectedIndexChanged(object sender, EventArgs e)
    {
        fill_type();
        fill_size_category(); // fill size by only category "no by type"
        
    }

    protected void DRP_SIZE_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    

    protected void DRP_COLOR_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void DRP_TYPE_SelectedIndexChanged(object sender, EventArgs e)
    {
        fill_size();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        fill_grd();
        db.ds1 = (DataSet)ViewState["ds"];
        db.deleting_grid(db.ds1, "colors", GridView1, e.RowIndex);
        fill_grd();
        max_id();
    }

    protected void TXT_NAME_TextChanged(object sender, EventArgs e)
    {

    }
}