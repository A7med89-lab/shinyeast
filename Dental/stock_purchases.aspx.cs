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

public partial class stock_purchases : System.Web.UI.Page
{
    public void fill_grid()
    {
        string select = "select purchases.id,purchases.[date],suppliers.name as supplier_name,suppliers.id as supplier_id, purchases.status, sum(purchases_details.quantity) as sum_qty, users.name as user_name, users.id as user_id from purchases inner join purchases_details on purchases.id = purchases_details.purchase_id inner join suppliers on purchases.supplier_id = suppliers.id left join users on purchases.user_id = users.id Group by purchases.id, purchases.[date], suppliers.name,suppliers.id , purchases.status, users.name, users.id";
        db.select(select, GRD_STOCK_IN);
        ViewState.Add("ds_grid", db.ds1);
    }

    public void fill_grid_details()
    {
        string select = "select purchases.id,purchases.[date],suppliers.name as supplier_name, purchases.status, sum(purchases_details.quantity) as sum_qty, users.name as username from purchases inner join purchases_details on purchases.id = purchases_details.purchase_id inner join suppliers on purchases.supplier_id = suppliers.id left join users on purchases.user_id = users.id Group by purchases.id,purchases.[date],suppliers.name , purchases.status, users.name";
        db.select(select, GRD_STOCK_DETAILS);
        ViewState.Add("ds_grid_details", db.ds1);
    }

    public void txt_id()
    {
        string id = "select max(id)+1 as id from stock_orders";
        db.select_id(id, TXT_ID);
        if (TXT_ID.Text == "")
        {
            TXT_ID.Text = 1.ToString();
        }

    }
    public void start_load()
    {             
        string date = DateTime.Now.ToString("yyyy-MM-dd");
        
        TXT_Date.Text = date;
        TXT_Date.Enabled = false;
        txt_id();   
        fill_grid();
    }
    database db = new database();
    DataSet grid_ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        start_load();
    }

    protected void GRD_STOCK_IN_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }

    protected void GRD_STOCK_IN_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int id = int.Parse(((Label)(GRD_STOCK_IN.Rows[index].Cells[0].FindControl("LBL_PURCHASE_ID_GRD"))).Text);
            string select = "select purchases.id, purchases.date, products.name, purchases_details.quantity as qty from purchases inner join purchases_details on purchases.id = purchases_details.purchase_id inner join products on purchases_details.product_id = products.id where purchase_id = " + id;
            db.select(select, GRD_STOCK_DETAILS);
            ViewState.Add("ds", db.ds1);
        }
        if (e.CommandName == "New")
        {
            int index = Convert.ToInt32(e.CommandArgument);

            int id = int.Parse(((Label)(GRD_STOCK_IN.Rows[index].FindControl("LBL_PURCHASE_ID_GRD"))).Text);
            int sum_qty = int.Parse(((Label)(GRD_STOCK_IN.Rows[index].FindControl("LBL_SUM_QTY_GRD"))).Text);           
            int supplier_id = int.Parse(((Label)(GRD_STOCK_IN.Rows[index].FindControl("LBL_SUPP_ID"))).Text);
            int user_id = int.Parse(((Label)(GRD_STOCK_IN.Rows[index].FindControl("LBL_USER_ID"))).Text);
            int inv_id = int.Parse(((DropDownList)(GRD_STOCK_IN.Rows[index].FindControl("DRP_INV_GRD"))).SelectedValue);
            string time = DateTime.Now.ToString("HH:mm:ss");
            string insert_stock_order = "INSERT INTO stock_orders (id, date, time, total_quantity, inventory_id, supplier_id, purchase_id, user_id, status, in) " +
            " VALUES (" +
            "" + TXT_ID.Text + "," +
            "'" + TXT_Date.Text + "'," +
            "'" + time + "'," +
            "" + sum_qty + " ," +
            "" + inv_id + "," +
            "" + supplier_id + "," +
            "" + id + "," +
            "" + user_id + ", " +
            ""+ 1 + ", " +
            ""+ 1 +")";
            db.insert(insert_stock_order);

            DataSet ds_grid = (DataSet)ViewState["ds_grid"];

        }
    }

    protected void GRD_STOCK_IN_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void GRD_STOCK_IN_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }
}