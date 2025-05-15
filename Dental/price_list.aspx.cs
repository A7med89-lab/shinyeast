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


public partial class price_list : System.Web.UI.Page
{
    public void fill_product()
    {
        string product = "select id, name from products";
        db.select_combo(product, DRP_PRODUCT);
        DRP_PRODUCT.Items.Insert(0, "Selected product");
        
    }
    public void fill_grid()
    {

        string select = "select price_list.product_id, products.name, price_list.purchase_price, price_list.sales_price from price_list inner join products on price_list.product_id = products.id";
        db.select(select, GridView1);
        ViewState.Add("ds", db.ds1);
    }
    database db = new database();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack==false)
        {
            fill_grid();
            fill_product();
        }
    }

    protected void BTN_Click(object sender, EventArgs e)
    {
        //check if product exist
        string product_id = db.select_value("select product_id from price_list where product_id=" + DRP_PRODUCT.SelectedValue + "", "product_id");
        if (product_id == "")
        {
            if (TXT_PURCHASE_PRICE.Text == "" || TXT_SALES_PRICE.Text == "")
            {
                Response.Write("<script>alert('please enter purchase price and sales price');</script>");
                return;
            }
            //insert
            string insert = "insert into price_list (product_id, purchase_price, sales_price) values (" + DRP_PRODUCT.SelectedValue+","+TXT_PURCHASE_PRICE.Text+ ", "+TXT_SALES_PRICE.Text+")";
            db.insert(insert);
            fill_grid();
            TXT_PURCHASE_PRICE.Text = "";
            TXT_SALES_PRICE.Text = "";
            DRP_PRODUCT.SelectedIndex = 0;
        }

        else
        {
            Response.Write("<script>alert('product already has price');</script>");
        }
         
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string delete_price="delete from price_list where product_id="+ ((Label)(GridView1.Rows[e.RowIndex].Cells[0].FindControl("LBL_PRODUCT_ID_GRD_"))).Text + "";
        db.delete(delete_price);
        fill_grid();
        //db.ds1 = (DataSet)ViewState["ds"];
        //db.deleting_grid(db.ds1, "price_list", GridView1, e.RowIndex);

    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //GridView1.EditIndex = e.RowIndex;

        //get product_id
        //string select_p_id = "select id from products where name='" + ((Label)(GridView1.Rows[e.RowIndex].Cells[0].FindControl("LBL_PRODUCT_ID_GRD"))).Text +"'";
        //string p_id = db.select_value(select_p_id, "id");
        //int product_id = int.Parse(p_id);
        int product_id = int.Parse(((Label)(GridView1.Rows[e.RowIndex].Cells[0].FindControl("LBL_PRODUCT_ID_GRD"))).Text);
        string update ="update price_list set purchase_price = "+ ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].FindControl("TXT_PURCHASE_PRICE_GRD"))).Text + " , sales_price = " + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].FindControl("TXT_SALES_PRICE_GRD"))).Text + "where product_id=" + product_id + "";
        db.update(update);

        GridView1.EditIndex = -1;
        fill_grid();
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fill_grid();
    }

    protected void DRP_PRODUCT_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }

    protected void DRP_PRODUCT_TextChanged(object sender, EventArgs e)
    {
        
    }
}