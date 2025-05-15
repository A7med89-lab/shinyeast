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
        string select = "select purchases.id,purchases.[date],suppliers.name as supplier_name,suppliers.id as supplier_id, purchases.status, sum(purchases_details.quantity) as sum_qty, users.name as user_name, users.id as user_id from purchases inner join purchases_details on purchases.id = purchases_details.purchase_id inner join suppliers on purchases.supplier_id = suppliers.id left join users on purchases.user_id = users.id Group by purchases.id, purchases.[date], suppliers.name,suppliers.id , purchases.status, users.name, users.id,  purchases.action having purchases.action = 0";
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
        fill_grid();
        GRD_STOCK_DETAILS.Visible = false;

    }
    database db = new database();
    DataSet grid_ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (! IsPostBack)
        {
            start_load();
            txt_id();

        }
        
    }

    protected void GRD_STOCK_IN_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }

    protected void GRD_STOCK_IN_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        int id = int.Parse(((Label)(GRD_STOCK_IN.Rows[index].FindControl("LBL_PURCHASE_ID_GRD"))).Text);
        string select = "select purchases.id, purchases.date, products.id as prod_id, products.name, purchases_details.quantity as qty from purchases inner join purchases_details on purchases.id = purchases_details.purchase_id inner join products on purchases_details.product_id = products.id where purchase_id = " + id;
        //string select_prod_qty = "select purchases.id, purchases.date, products.name, purchases_details.quantity as qty from purchases inner join purchases_details on purchases.id = purchases_details.purchase_id inner join products on purchases_details.product_id = products.id where purchase_id = " + id;

        if (e.CommandName == "Select")
        {
            db.select(select, GRD_STOCK_DETAILS);
            if(GRD_STOCK_DETAILS.Visible == false)
                GRD_STOCK_DETAILS.Visible = true;
            
        }
        if (e.CommandName == "New")
        {                        
            int sum_qty = int.Parse(((Label)(GRD_STOCK_IN.Rows[index].FindControl("LBL_SUM_QTY_GRD"))).Text);           
            int supplier_id = int.Parse(((Label)(GRD_STOCK_IN.Rows[index].FindControl("LBL_SUPP_ID"))).Text);
            int user_id = int.Parse(((Label)(GRD_STOCK_IN.Rows[index].FindControl("LBL_USER_ID"))).Text);
            int inv_id = int.Parse(((DropDownList)(GRD_STOCK_IN.Rows[index].FindControl("DRP_INV_GRD"))).SelectedValue);
            string time = DateTime.Now.ToString("HH:mm:ss");
            string insert_stock_order = "INSERT INTO stock_orders (id, date, time, total_quantity, inventory_id, supplier_id, purchase_id, user_id, status, [in]) " +
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

            string update_stock_order = "update purchases set status = '" + 1 + "', action = '" + 1 + "' where id = " + id + "";
            db.update(update_stock_order);

            DataTable prod_details = db.select_values(select);
            
            int[] prod_qty = new int[prod_details.Rows.Count];
            int[] prod_id = new int[prod_details.Rows.Count];
            for (int i =0; i < prod_details.Rows.Count; i++)
            {
                // insert into stock_order_details
                prod_qty[i] = int.Parse(prod_details.Rows[i]["qty"].ToString());
                prod_id[i] = int.Parse(prod_details.Rows[i]["prod_id"].ToString());

                string insert_stock_details = "INSERT INTO stock_order_details (id, date, time, stock_order_id, product_id, quantity ) " +
                " select  COALESCE (max(id),0)+1" + ", " +
                "'" + TXT_Date.Text + "'," +
                "'" + time + "'," +
                "" + TXT_ID.Text + " ," +
                "" + prod_id[i] + "," +
                "" + prod_qty[i] + " from stock_order_details";
                db.insert(insert_stock_details);

                //update stock_total_quantity
                string check_prod_total_qty = "select * from stock_total_quantity where product_id = "+ prod_id[i] + "";
                string select_prod_id_total_qty = db.select_value(check_prod_total_qty, "product_id");
                string select_prod_qty_total_qty_in = db.select_value(check_prod_total_qty, "total_in");
                if (string.IsNullOrEmpty(select_prod_qty_total_qty_in))
                {
                    select_prod_qty_total_qty_in = "0";
                }
                int total_in = int.Parse(select_prod_qty_total_qty_in) + prod_qty[i];
                string update_stock_total_quantity="";
                if (string.IsNullOrEmpty(select_prod_id_total_qty))
                {
                    string insert_stock_total_quantity = "insert into stock_total_quantity (product_id , total_in, total_net_in) VALUES (" + prod_id[i] + ","+ prod_qty[i] + ", "+ prod_qty[i] +")";
                    db.insert(insert_stock_total_quantity);

                }
                else
                {
                    int select_prod_qty_total_qty_out = int.Parse(db.select_value(check_prod_total_qty, "total_out"));
                    if (select_prod_qty_total_qty_out != 0)
                    {   
                        int total_net_in = int.Parse(select_prod_qty_total_qty_in) - select_prod_qty_total_qty_out;
                        update_stock_total_quantity = "update stock_total_quantity set total_in = " + total_in + ", total_net_in = " + total_net_in + " where product_id = " + prod_id[i] + "";
                        //db.update(update_stock_total_quantity);
                    }
                    else
                    {                                                
                        update_stock_total_quantity = "update stock_total_quantity set total_in = " + total_in + " , total_net_in = " + total_in + " where product_id = " + prod_id[i] + "";                       
                    }

                    db.update(update_stock_total_quantity);

                }

                
            }


            //string ins_add = "INSERT INTO addresses ([id],city_id, regoin_id , street, buid_number) select  COALESCE (max(id),0)+1," + TXT_ID.Text + "," + DRP_CITY.SelectedValue + "," + DRP_REGON.SelectedValue + ", N'" + TXT_STRT.Text + "', N'" + TXT_BULD.Text + "' from addresses";



            //DataSet ds_grid = (DataSet)ViewState["ds_grid"];
            //ds_grid.Tables[0].Rows[index].Delete();
            start_load();

        }
    }

    protected void GRD_STOCK_IN_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void GRD_STOCK_IN_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }
}