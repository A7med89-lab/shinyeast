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

public partial class purchases_edit_delete : System.Web.UI.Page
{
    database db = new database();
    public void fill_grid()
    {
        string select = "select purchases.id,purchases.[date],suppliers.name as supplier_name,suppliers.id as supplier_id, purchases.status, sum(purchases_details.quantity) as sum_qty, users.name as user_name, users.id as user_id from purchases inner join purchases_details on purchases.id = purchases_details.purchase_id inner join suppliers on purchases.supplier_id = suppliers.id left join users on purchases.user_id = users.id Group by purchases.id, purchases.[date], suppliers.name,suppliers.id , purchases.status, users.name, users.id,  purchases.action having purchases.action = 0";
        db.select(select, GRD_STOCK_IN);
        ViewState.Add("ds_grid", db.ds1);
        int row_count = GRD_STOCK_IN.Rows.Count;
        //if (row_count == 0)
        //{
        //    LBL_PURCHASE_MASSEGE.Visible = true;
        //}
    }

    public void fill_inv()
    {
        string cst = "select id, name from inventories";
        db.select_combo(cst, DRP_INV_FILTER);
        DRP_INV_FILTER.Items.Insert(0, "اختيار المخزن");
    }

    public void fill_supp()
    {
        string cst = "select id, name from suppliers";      
        db.select_combo(cst, DRP_SUPP_FILTER);
        DRP_SUPP_FILTER.Items.Insert(0, "اختيار المورد");
    }
    public void load_start()
    {
        string date = DateTime.Now.ToString("yyyy-MM-dd");
        TXT_DATE_FILTER.Text = date;
        fill_grid();
        fill_inv();
        fill_supp();
    }

    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            load_start();


            GRD_STOCK_DETAILS.Visible = false;
        }
        
    }

    protected void GRD_STOCK_IN_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void GRD_STOCK_IN_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void GRD_STOCK_IN_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }

    protected void GRD_STOCK_IN_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GRD_STOCK_IN_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int id = int.Parse(((Label)(GRD_STOCK_IN.Rows[index].FindControl("LBL_PURCHASE_ID_GRD"))).Text);
            string select = "select purchases.id, purchases.date, products.id as prod_id, products.name,  purchases_details.price ,purchases_details.quantity as qty , purchases_details.total_price, purchases_details.tax, purchases_details.total_after_tax, purchases_details.discount, purchases_details.total_after_discount, purchases_details.profit  from purchases inner join purchases_details on purchases.id = purchases_details.purchase_id inner join products on purchases_details.product_id = products.id where purchase_id = " + id;
            db.select(select, GRD_STOCK_DETAILS);
            if (GRD_STOCK_DETAILS.Visible == false)
                GRD_STOCK_DETAILS.Visible = true;

        }

        if (e.CommandName == "Delete")
        {
           

        }
    }

    protected void GRD_STOCK_IN_RowEditing(object sender, GridViewEditEventArgs e)
    {
        
        db.ds1 = (DataSet)ViewState["ds_grid"];
        GRD_STOCK_IN.EditIndex = e.NewEditIndex; // transfer row to edite mode
        //db.ds1.Tables[0].Rows[e.NewEditIndex][1]= ((TextBox)(GridView1.Rows[e.NewEditIndex].Cells[1].FindControl("cst_name_edite"))).Text;
        GRD_STOCK_IN.DataSource = db.ds1.Tables[0];
        GRD_STOCK_IN.DataBind();
        ViewState["ds_grid"] = db.ds1;
        load_start();
    }

    protected void GRD_STOCK_IN_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        db.ds1 = (DataSet)ViewState["ds_grid"];
        GRD_STOCK_IN.EditIndex = -1;  // transfer row to desgin mode
        GRD_STOCK_IN.DataSource = db.ds1.Tables[0];
        GRD_STOCK_IN.DataBind();
    }
}