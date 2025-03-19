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

public partial class sizes : System.Web.UI.Page
{
    public void fill_grd()
    {
        string select = "select sizes.id, sizes.name, categories.name as category, types.name as type from sizes inner join categories on sizes.category_id = categories.id left join types on sizes.type_id = types.id ";
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
    public bool validation()
    {
        string name= TXT_NAME.Text;
        string name_check = db.select_value ("select name from sizes where name = '" + TXT_NAME.Text + "' and category_id =" + DRP_CATEG.SelectedValue + " and type_id is null", "name");
        string category_check = db.select_value("select category_id from sizes where name = '" + TXT_NAME.Text + "' and category_id ="+ DRP_CATEG.SelectedValue +" ", "category_id");
        //string type_check = db.select_value("select type_id from sizes where name = '" + TXT_NAME.Text + "'", "type_id");
        if (TXT_NAME.Text == "" || DRP_CATEG.SelectedIndex <= 0)
        {

            return false;
            
        }
        else
        {
            if (name_check != "")
            {
                if (DRP_TYPE.SelectedIndex > 0)
                {
                    string type_check = db.select_value ("select name from sizes where name = '"+TXT_NAME.Text+ "' and category_id =" + DRP_CATEG.SelectedValue + " and [type_id]= "+DRP_TYPE.SelectedValue+"", "name"); 

                     if (name_check != "" )
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }           
        }
    }

    public void max_id()
    {
        string query = "select  max (id)+1 as id from sizes";
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
            fill_category();
            
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        bool vali = validation();

        if (validation() == true)
        {
            if (DRP_TYPE.SelectedIndex <= 0)
            {
                string insert = "INSERT INTO sizes ([id], [name],category_id) VALUES(" + TXT_ID.Text + ", N'" + TXT_NAME.Text + "'," + DRP_CATEG.SelectedValue + ")";
                db.insert(insert);
            }
            else
            {
                string insert = "INSERT INTO sizes ([id], [name],category_id, type_id) VALUES(" + TXT_ID.Text + ", N'" + TXT_NAME.Text + "'," + DRP_CATEG.SelectedValue + "," + DRP_TYPE.SelectedValue + ")";
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
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        fill_grd();
        db.ds1 = (DataSet)ViewState["ds"];
        db.deleting_grid(db.ds1, "sizes", GridView1, e.RowIndex);
        fill_grd();
        max_id();
    }
}