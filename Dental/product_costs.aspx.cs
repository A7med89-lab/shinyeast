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
using System.Reflection;

public partial class product_costs : System.Web.UI.Page
{
    public void fill_grd()
    {
        string select = "select * from product_costs";
        db.select(select, GridView1);
        ViewState.Add("ds", db.ds1);
    }

    public bool validation()
    {
        string name = TXT_NAME.Text;
        string  id_check = db.select_value("select name from product_costs where name = N'" + name.Trim() +"'", "name");
        if (id_check == "")
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void max_id ()
    {
        string query = "select  max (id)+1 as id from product_costs";
        db.select_id(query, TXT_ID);

        if (TXT_ID.Text == "")
        {
            TXT_ID.Text = 1.ToString();
        }
    }

    public void clear()
    {
        TXT_NAME.Text = "";
        TXT_DESC.Text = "";
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

    protected void Button1_Click1(object sender, EventArgs e)
    {
        
        bool vali = validation();
        if (validation() == false)
        {
            string insert = "INSERT INTO product_costs ([id], [name], amount, [desc]) VALUES(" + TXT_ID.Text + ", N'" + TXT_NAME.Text + "', " + TXT_AMOUNT.Text + " , N'" + TXT_DESC.Text + "')";
            db.insert(insert);
        }

        else
        {
            Response.Write("<script>alert('the same name already exist');</script>");
            return;
            
        }

        max_id();
        fill_grd();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        fill_grd();
        db.ds1 = (DataSet)ViewState["ds"];
        db.deleting_grid(db.ds1, "product_costs", GridView1, e.RowIndex);
        fill_grd();
        max_id();
        clear();
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        

        //db.ds1 = (DataSet)ViewState["ds"];
        GridView1.EditIndex = e.NewEditIndex; // transfer row to edite mode        
        fill_grd();
        //GridView1.DataSource = db.ds1.Tables[0];
        //GridView1.DataBind();
        //ViewState["ds"] = db.ds1;
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //db.ds1 = (DataSet)ViewState["ds"];
        GridView1.EditIndex = -1;  // transfer row to desgin mode
        //GridView1.DataSource = db.ds1.Tables[0];
        //GridView1.DataBind();
        fill_grd();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        
        //db.ds1 = (DataSet)ViewState["ds"];
        string update = "update product_costs set name = N'" + ((TextBox)(GridView1.Rows[e.RowIndex].FindControl("TXT_NAME_GRD"))).Text + "' , amount = " + ((TextBox)(GridView1.Rows[e.RowIndex].FindControl("TXT_AMOUNT_GRD"))).Text + ",  [desc] = N'" + ((TextBox)(GridView1.Rows[e.RowIndex].FindControl("TXT_DESC_GRD"))).Text + "' where id = "+ int.Parse(((Label)(GridView1.Rows[e.RowIndex].FindControl("LBL_ID_EDIT_GRD"))).Text) + " ";
        db.update(update);
        GridView1.EditIndex = -1;
        fill_grd();
        //GridView1.EditIndex = e.RowIndex; // transfer row to edite mode        

    }
}