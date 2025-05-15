using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

public partial class sales_draft : System.Web.UI.Page
{
    int new_record;
    int qty_GRD;
    decimal price_GRD;
    int disacc_GRD;
    decimal total_price_GRD;
    decimal profit_GRD;
    decimal percent_GRD;
    decimal sales_price;
    decimal sales_value;
    decimal pur_price;
    decimal pur_value;
    decimal profit;
    decimal profit_percent;
    database db = new database();

    public void fill_grid()
    {
        string select = "select sales.id,sales.[date], products.name as product,customers.name as customer,sales.quantity,sales.price,sales.total_price, sales.profit_percent,sales.profit,sales.inventory_id,inventory.name as inventory_name from sales inner join products on sales.product_id = products.id inner join customers on customers.id = sales.customer_id inner join inventory on sales.inventory_id = inventory.id";
        db.select(select, GridView1);
        ViewState.Add("ds", db.ds1);
    }

    
    public void fill_grid_new()
    {
        string select_new = "select sales.id,sales.[date], products.name as product,customers.name as customer,sales.quantity,sales.price,sales.total_price, sales.profit_percent,sales.profit,sales.inventory_id,inventory.name as inventory_name from sales inner join products on sales.product_id = products.id inner join customers on customers.id = sales.customer_id inner join inventory on sales.inventory_id = inventory.id where sales.customer_id = " + DRP_CUSTOMER.SelectedValue + " and[date] = '" + TXT_Date.Text + "'";
        db.select(select_new, GridView1);
        ViewState.Add("ds_new", db.ds1);
    }

    /*
    public void fill_grid_filter(string criteria)
    {
        string select_new;
        switch (criteria)
        {
            case "supp":
                select_new = "select purchases.id,purchases.[date], products.name as product,suppliers. name as supplier,purchases.quantity,purchases.price,purchases.total_price, purchases.total_price, purchases.profit_percent,purchases.profit, inventory.id as inventory_id, inventory.name as inventory_name from purchases inner join products on products.id=purchases.product_id inner join suppliers on suppliers.id = purchases.supplier_id inner join inventory on purchases.inventory_id = inventory.id where purchases.supplier_id = " + DRP_SUPP_FILTER.SelectedValue + "";
                db.select(select_new, GridView1);
                ViewState.Add("ds_filter", db.ds1);
                break;
            case "inv":
                select_new = "select purchases.id,purchases.[date], products.name as product,suppliers. name as supplier,purchases.quantity,purchases.price,purchases.total_price, purchases.total_price, purchases.profit_percent,purchases.profit, inventory.id as inventory_id, inventory.name as inventory_name from purchases inner join products on products.id=purchases.product_id inner join suppliers on suppliers.id = purchases.supplier_id inner join inventory on purchases.inventory_id = inventory.id where purchases.inventory_id = " + DRP_INV_FILTER.SelectedValue + "";
                db.select(select_new, GridView1);
                ViewState.Add("ds_filter", db.ds1);
                break;
            case "date":
                select_new = "select purchases.id,purchases.[date], products.name as product,suppliers. name as supplier,purchases.quantity,purchases.price,purchases.total_price, purchases.total_price, purchases.profit_percent,purchases.profit,purchases.supplier_id, purchases.product_id from purchases inner join products on products.id=purchases.product_id inner join suppliers on suppliers.id = purchases.supplier_id where [date]='" + TXT_DATE_FILTER.Text + "'";
                db.select(select_new, GridView1);
                ViewState.Add("ds_filter", db.ds1);
                break;
            case "supp_date":
                select_new = "select purchases.id,purchases.[date], products.name as product,suppliers. name as supplier,purchases.quantity,purchases.price,purchases.total_price, purchases.total_price, purchases.profit_percent,purchases.profit, inventory.id as inventory_id, inventory.name as inventory_name from purchases inner join products on products.id=purchases.product_id inner join suppliers on suppliers.id = purchases.supplier_id inner join inventory on purchases.inventory_id = inventory.id where purchases.supplier_id = " + DRP_SUPP_FILTER.SelectedValue + " and [date]='" + TXT_DATE_FILTER.Text + "'";
                db.select(select_new, GridView1);
                ViewState.Add("ds_filter", db.ds1);
                break;
            case "all":
                select_new = "select purchases.id,purchases.[date], products.name as product,suppliers. name as supplier,purchases.quantity,purchases.price,purchases.total_price, purchases.total_price, purchases.profit_percent,purchases.profit, inventory.id as inventory_id, inventory.name as inventory_name from purchases inner join products on products.id=purchases.product_id inner join suppliers on suppliers.id = purchases.supplier_id inner join inventory on purchases.inventory_id = inventory.id where purchases.supplier_id = " + DRP_SUPP_FILTER.SelectedValue + " and [date]='" + TXT_DATE_FILTER.Text + "' and purchases.inventory_id = " + DRP_INV_FILTER.SelectedValue + "";
                db.select(select_new, GridView1);
                ViewState.Add("ds_filter", db.ds1);
                break;
        }

        
    }
    */

    public void fill_cst()
    {
        string cst = "select id, name from customers";
        db.select_combo(cst, DRP_CUSTOMER);
        db.select_combo(cst, DRP_CUSTOMER_FILTER);
        DRP_CUSTOMER.Items.Insert(0, "Selected Customer");
        DRP_CUSTOMER_FILTER.Items.Insert(0, "Selected Customer");
    }



    public void fill_product()
    {
        string prod = "select id, name from products";
        db.select_combo(prod, DRP_PRODUCT);
        DRP_PRODUCT.Items.Insert(0, "Selected Product");
    }


    public void clear_txt(params TextBox[] txts)
    {
        for (int i = 0; i < txts.Length; i++)
        {

            txts[i].Text = string.Empty;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            TXT_Date.Text = date;
            TXT_DATE_FILTER.Text = date;
            fill_cst();
            //fill_product();
            fill_grid();
            DRP_PRODUCT.Enabled = false;
            DRP_CUSTOMER.Enabled = false;
            TXT_Date.Enabled = false;
            TXT_ID.Enabled = false;

        }
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //get customer id
        string cust_id = db.select_value("select id from customers where name = '" + ((Label)(GridView1.Rows[e.RowIndex].Cells[3].FindControl("LBL_CUST_NAME_GRD"))).Text + "'", "id");
        int id_cust = int.Parse(cust_id);

        //get product id
        string product_id = db.select_value("select id from products where name = '" + ((Label)(GridView1.Rows[e.RowIndex].Cells[2].FindControl("LBL_NAME_GRD"))).Text + "'", "id");
        int id_product = int.Parse(product_id);

        string delete_row = "delete from sales where id= " + ((Label)(GridView1.Rows[e.RowIndex].Cells[0].FindControl("LBL_ID_GRD"))).Text + " and [date]='" + ((Label)(GridView1.Rows[e.RowIndex].Cells[1].FindControl("LBL_DATE_GRD"))).Text + "'and customer_id=" + id_cust + " and product_id=" + id_product + "";
        db.delete(delete_row);

        if (DRP_CUSTOMER.SelectedIndex != 0)
        {

            fill_grid_new();
        }

        else
        {

            fill_grid();
        }
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //check Update or Insert

        string chk = "select id from ";

        //update:



        //Insert:        

        //product id

        string select_p_id = "select id from products where name='" + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].FindControl("TXT_NAME_GRD"))).Text + "'";
        string p_id = db.select_value(select_p_id, "id");
        int product_id = int.Parse(p_id);

        
        //max_id
        //string select_id = "select id from purchases where date='"+TXT_Date.Text+"' and supplier_id="+((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].FindControl("TXT_ID_GRD"))).Text+"";
        string select_id = "select id from sales where date='" + TXT_Date.Text + "' and customer_id=" + DRP_CUSTOMER.SelectedValue + "";
        string id = db.select_value(select_id, "id");
        //int id_pur = int.Parse(id);
        int max_id = 0;
        if (id == "")
        {
            // insert sales
            //max_id = 1;
            string max_p_id = "INSERT INTO sales (id, date, customer_id, product_id,inventory_id,quantity,price, total_price, profit_percent, profit) select COALESCE (max(id),0)+1 ,'" + TXT_Date.Text + "'," + DRP_CUSTOMER.SelectedValue + "," + product_id + "," + ((DropDownList)(GridView1.Rows[e.RowIndex].Cells[9].FindControl("DRP_INV_GRD"))).SelectedValue + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("TXT_QTY_GRD"))).Text + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[5].FindControl("TXT_PRICE_GRD"))).Text + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[6].FindControl("TXT_TOTAL_PRICE_GRD"))).Text + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[7].FindControl("TXT_PERCENT_DRG"))).Text + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[8].FindControl("TXT_PROFIT_DRG"))).Text + " from sales";
            db.insert(max_p_id);

            // insert stock in/out:
            //get sales id
            string get_sales_id = "select id from sales where [date]='" + TXT_Date.Text + "' and customer_id=" + DRP_CUSTOMER.SelectedValue + " and product_id=" + product_id + "";
            string sales_id = db.select_value(get_sales_id, "id");
            int id_sales = int.Parse(sales_id);

            // insert stock in/out
            string stock = "INSERT INTO stock (id, date, product_id, quantity, inventory_id,customer_id,sales_id,[out]) select COALESCE (max(id),0)+1 ,'" + TXT_Date.Text + "'," + product_id + " ," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("TXT_QTY_GRD"))).Text + "," + ((DropDownList)(GridView1.Rows[e.RowIndex].Cells[9].FindControl("DRP_INV_GRD"))).SelectedValue + "," + DRP_CUSTOMER.SelectedValue + "," + id_sales + ",'" + 1 + "' from stock";
            db.insert(stock);

            //insert stock qty total_in total_out:
            //get current total_in
            string get_total_out = "select total_out from stock_quantity where product_id = " + product_id + "";
            string total_out = db.select_value(get_total_out, "total_out"); int out_total=0;
            if (total_out == "")
            {
                out_total = 0;
            }
            else
            {
                out_total = int.Parse(total_out);
            }

            //update total_out
            int sum_total = out_total + int.Parse(((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("TXT_QTY_GRD"))).Text);
            string update_total_in = "UPDATE stock_quantity SET total_out = " + sum_total + " where product_id=" + product_id + "";
            db.update(update_total_in);

            //update_stock_qty_net_in:
            //get totla_in_out
            //in
            string in_stock = "select total_in from stock_quantity where product_id = " + product_id + "";
            string net_in = db.select_value(in_stock, "total_in");
            int stock_net_in = int.Parse(net_in);
            //out
            string out_stock = "select total_out from stock_quantity where product_id = " + product_id + "";
            string out_in = db.select_value(out_stock, "total_out");
            int stock_out_in = int.Parse(out_in);

            //get current total_net_in
            string get_total_net_in = "select total_net_in from stock_quantity where product_id = " + product_id + "";
            string total_net_in = db.select_value(get_total_net_in, "total_net_in");
            int net_in_total = int.Parse(total_net_in);

            //int qty_net_in = stock_net_in - stock_out_in;
            int qty_net_in = net_in_total - int.Parse(((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("TXT_QTY_GRD"))).Text);
            string update_net_in = "UPDATE stock_quantity SET total_net_in = " + qty_net_in + " where product_id=" + product_id + "";
            db.update(update_net_in);

        }
        else
        {
            // insert sales
            max_id = int.Parse(id);
            string insert = "INSERT INTO sales (id, date, customer_id, product_id,inventory_id,quantity,price, total_price, profit_percent, profit) VALUES (" + max_id + ",'" + TXT_Date.Text + "'," + DRP_CUSTOMER.SelectedValue + "," + product_id + "," + ((DropDownList)(GridView1.Rows[e.RowIndex].Cells[9].FindControl("DRP_INV_GRD"))).SelectedValue + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("TXT_QTY_GRD"))).Text + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[5].FindControl("TXT_PRICE_GRD"))).Text + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[6].FindControl("TXT_TOTAL_PRICE_GRD"))).Text + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[7].FindControl("TXT_PERCENT_DRG"))).Text + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[8].FindControl("TXT_PROFIT_DRG"))).Text + ")";
            db.insert(insert);

            // insert stock in/out:
            //get sales id
            string get_sales_id = "select id from sales where [date]='" + TXT_Date.Text + "' and customer_id=" + DRP_CUSTOMER.SelectedValue + " and product_id=" + product_id + "";
            string sales_id = db.select_value(get_sales_id, "id");
            int id_sales = int.Parse(sales_id);

            // insert stock in/out
            string stock = "INSERT INTO stock (id, date, product_id, quantity, inventory_id,customer_id,sales_id,[out]) select COALESCE (max(id),0)+1 ,'" + TXT_Date.Text + "'," + product_id + " ," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("TXT_QTY_GRD"))).Text + "," + ((DropDownList)(GridView1.Rows[e.RowIndex].Cells[9].FindControl("DRP_INV_GRD"))).SelectedValue + "," + DRP_CUSTOMER.SelectedValue + "," + id_sales + ",'" + 1 + "' from stock";
            db.insert(stock);

            //insert stock qty total_in total_out:
            //get current total_in
            string get_total_out = "select total_out from stock_quantity where product_id = " + product_id + "";
            string total_out = db.select_value(get_total_out, "total_out"); int out_total = 0;
            if (total_out == "")
            {
                out_total = 0;
            }
            else
            {
                out_total = int.Parse(total_out);
            }

            int sum_total = out_total + int.Parse(((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("TXT_QTY_GRD"))).Text);
            string update_total_in = "UPDATE stock_quantity SET total_out = " + sum_total + " where product_id=" + product_id + "";
            db.update(update_total_in);

            //update_stock_qty_net_in
            //get_total_in_out
            //in
            string in_stock = "select total_in from stock_quantity where product_id = " + product_id + "";
            string net_in = db.select_value(in_stock, "total_in");
            int stock_net_in = int.Parse(net_in);
            //out
            string out_stock = "select total_out from stock_quantity where product_id = " + product_id + "";
            string out_in = db.select_value(out_stock, "total_out");
            int stock_out_in = int.Parse(out_in);

            //get current total_net_in
            string get_total_net_in = "select total_net_in from stock_quantity where product_id = " + product_id + "";
            string total_net_in = db.select_value(get_total_net_in, "total_net_in");
            int net_in_total = int.Parse(total_net_in);

            //int qty_net_in = stock_net_in - stock_out_in;
            int qty_net_in = net_in_total - int.Parse(((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("TXT_QTY_GRD"))).Text);
            string update_net_in = "UPDATE stock_quantity SET total_net_in = " + qty_net_in + " where product_id=" + product_id + "";
            db.update(update_net_in);
        }

        GridView1.EditIndex = -1;

        fill_grid_new();


    }

    

    protected void CHK_INVENTORY_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void BTN_NEW_Click(object sender, EventArgs e)
    {
        new_record = 0;
        ViewState["new_record"] = 0;
        ViewState.Add("new_record", new_record);
        DRP_PRODUCT.Enabled = true;
        DRP_CUSTOMER.Enabled = true;
        TXT_Date.Enabled = true;
        db.ds1 = (DataSet)ViewState["ds"];
        if (GridView1.Rows.Count > 0)
        {
            for (int i = 0; i < db.ds1.Tables[0].Rows.Count; i++)
            {
                //GridView1.DeleteRow(i);
                db.ds1.Tables[0].Rows[i].Delete();
            }
            GridView1.DataSource = db.ds1;
            GridView1.DataBind();

        }
    }

    protected void DRP_PRODUCT_SelectedIndexChanged(object sender, EventArgs e)
    {
        //new_record

        if (GridView1.Rows.Count <= 0)
        {
            ViewState["new_record"] = new_record;
        }
        else
        {
            ViewState["new_record"] = GridView1.Rows.Count;
        }

        
        new_record = (int)ViewState["new_record"];

        GridView1.EditIndex = new_record;
        db.ds1 = (DataSet)ViewState["ds_new"];

        GridView1.DataSource = db.ds1;
        GridView1.DataBind();
        db.ds1 = (DataSet)ViewState["ds_new"];
        
        DataRow dr = db.ds1.Tables[0].NewRow();
        //dr["id"] = TXT_ID.Text;
        dr["date"] = TXT_Date.Text;
        dr["product"] = DRP_PRODUCT.SelectedItem;
        //dr["product_id"] = DRP_PRODUCT.SelectedValue;
        dr["customer"] = DRP_CUSTOMER.SelectedItem;
        //dr["supplier_id"] = DRP_SUPPLIER.SelectedValue;
        dr["quantity"] = 0;
        dr["price"] = 0;
        dr["total_price"] = 0;        
        dr["Profit"] = 0;
        dr["profit_percent"] = 0;

        db.ds1.Tables[0].Rows.Add(dr); 
        GridView1.DataSource = db.ds1;
        GridView1.DataBind();
        new_record += 1;
        ViewState["new_record"] = new_record;
        ViewState["ds_new"] = db.ds1;
        DRP_PRODUCT.SelectedIndex = 0;
        

    }

    protected void DRP_CUSTOMER_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DRP_CUSTOMER.SelectedIndex > 0)
            fill_product();

        fill_grid_new();

    }

    protected void TXT_QTY_GRD_TextChanged(object sender, EventArgs e)
    {
        int rindex = GridView1.Rows.Count - 1;

        //get product_id
        string select_p_id = "select id from products where name='" + ((TextBox)(GridView1.Rows[rindex].Cells[2].FindControl("TXT_NAME_GRD"))).Text + "'";
        string p_id = db.select_value(select_p_id, "id");
        int product_id = int.Parse(p_id);

        //get price from pricelist
        string select_pur_price = "select price from price_list where product_id = " + product_id + "";
        string pur_p = db.select_value(select_pur_price, "price");
        pur_price = int.Parse(pur_p);

        // calculate profit
        pur_value = 0;
        pur_value = pur_price * int.Parse(((TextBox)(GridView1.Rows[rindex].Cells[4].FindControl("TXT_QTY_GRD"))).Text);
        sales_price = int.Parse(((TextBox)(GridView1.Rows[rindex].Cells[5].FindControl("TXT_PRICE_GRD"))).Text);
        sales_value = sales_price * int.Parse(((TextBox)(GridView1.Rows[rindex].Cells[4].FindControl("TXT_QTY_GRD"))).Text);
        profit = sales_value - pur_value;
        profit_percent = (profit / pur_value) * 100;


        //edit in dataset
        db.ds1 = (DataSet)ViewState["ds_new"];

        if (((TextBox)(GridView1.Rows[rindex].Cells[4].FindControl("TXT_QTY_GRD"))).Text == "")
        {
            qty_GRD = 0;
            db.ds1.Tables[0].Rows[rindex][4] = qty_GRD;
            //ViewState["ds_new"] = db.ds1;
        }
        else
        {
            qty_GRD = int.Parse(((TextBox)(GridView1.Rows[rindex].Cells[4].FindControl("TXT_QTY_GRD"))).Text);
            db.ds1.Tables[0].Rows[rindex][4] = qty_GRD;
            //ViewState["ds_new"] = db.ds1;
        }

        if (((TextBox)(GridView1.Rows[rindex].Cells[5].FindControl("TXT_PRICE_GRD"))).Text == "")
        {
            price_GRD = 0;
            db.ds1.Tables[0].Rows[rindex][5] = price_GRD;
            //ViewState["ds_new"] = db.ds1;
        }
        else
        {
            price_GRD = int.Parse(((TextBox)(GridView1.Rows[rindex].Cells[5].FindControl("TXT_PRICE_GRD"))).Text);
            db.ds1.Tables[0].Rows[rindex][5] = price_GRD;
            //ViewState["ds_new"] = db.ds1;
        }

        total_price_GRD = qty_GRD * price_GRD;
        ((TextBox)(GridView1.Rows[rindex].Cells[6].FindControl("TXT_TOTAL_PRICE_GRD"))).Text = total_price_GRD.ToString();
        db.ds1.Tables[0].Rows[rindex][6] = total_price_GRD;
        ViewState["ds_new"] = db.ds1;

        //profit
        profit_GRD = profit;
        ((TextBox)(GridView1.Rows[rindex].Cells[8].FindControl("TXT_PROFIT_DRG"))).Text = profit_GRD.ToString();
        db.ds1.Tables[0].Rows[rindex][8] = profit_GRD;
        ViewState["ds_new"] = db.ds1;

        //profit prece
        percent_GRD = profit_percent;
        ((TextBox)(GridView1.Rows[rindex].Cells[7].FindControl("TXT_PERCENT_DRG"))).Text = percent_GRD.ToString();
        db.ds1.Tables[0].Rows[rindex][7] = percent_GRD;
        ViewState["ds_new"] = db.ds1;
    }

    protected void TXT_PRICE_GRD_TextChanged(object sender, EventArgs e)
    {
        int rindex = GridView1.Rows.Count - 1;

        //get product_id
        string select_p_id = "select id from products where name='" + ((TextBox)(GridView1.Rows[rindex].Cells[2].FindControl("TXT_NAME_GRD"))).Text + "'";
        string p_id = db.select_value(select_p_id, "id");
        int product_id = int.Parse(p_id);

        //get price from pricelist
        string select_pur_price = "select price from price_list where product_id = "+ product_id + "";
        string pur_p = db.select_value(select_pur_price, "price");
        pur_price = int.Parse(pur_p);

        // calculate profit
        pur_value =0 ;
        pur_value = pur_price * int.Parse (((TextBox)(GridView1.Rows[rindex].Cells[4].FindControl("TXT_QTY_GRD"))).Text);
        sales_price = int.Parse (((TextBox)(GridView1.Rows[rindex].Cells[5].FindControl("TXT_PRICE_GRD"))).Text);
        sales_value = sales_price * int.Parse(((TextBox)(GridView1.Rows[rindex].Cells[4].FindControl("TXT_QTY_GRD"))).Text);
        profit = sales_value - pur_value;
        profit_percent = (profit / pur_value) *100;

       
        //edit in dataset
        db.ds1 = (DataSet)ViewState["ds_new"];

        if (((TextBox)(GridView1.Rows[rindex].Cells[4].FindControl("TXT_QTY_GRD"))).Text == "")
        {
            qty_GRD = 0;
            db.ds1.Tables[0].Rows[rindex][4] = qty_GRD;
            //ViewState["ds_new"] = db.ds1;
        }
        else
        {
            qty_GRD = int.Parse(((TextBox)(GridView1.Rows[rindex].Cells[4].FindControl("TXT_QTY_GRD"))).Text);
            db.ds1.Tables[0].Rows[rindex][4] = qty_GRD;
            //ViewState["ds_new"] = db.ds1;
        }

        if (((TextBox)(GridView1.Rows[rindex].Cells[5].FindControl("TXT_PRICE_GRD"))).Text == "")
        {
            price_GRD = 0;
            db.ds1.Tables[0].Rows[rindex][5] = price_GRD;
            //ViewState["ds_new"] = db.ds1;
        }
        else
        { 
            price_GRD = int.Parse(((TextBox)(GridView1.Rows[rindex].Cells[5].FindControl("TXT_PRICE_GRD"))).Text);
            db.ds1.Tables[0].Rows[rindex][5] = price_GRD;
            //ViewState["ds_new"] = db.ds1;
        }

        total_price_GRD = qty_GRD * price_GRD;
        ((TextBox)(GridView1.Rows[rindex].Cells[6].FindControl("TXT_TOTAL_PRICE_GRD"))).Text = total_price_GRD.ToString();
        db.ds1.Tables[0].Rows[rindex][6] = total_price_GRD;
        ViewState["ds_new"] = db.ds1;

        //profit
        profit_GRD = profit;
        ((TextBox)(GridView1.Rows[rindex].Cells[8].FindControl("TXT_PROFIT_DRG"))).Text = profit_GRD.ToString();
        db.ds1.Tables[0].Rows[rindex][8] = profit_GRD;
        ViewState["ds_new"] = db.ds1;

        //profit prece
        percent_GRD = profit_percent;
        ((TextBox)(GridView1.Rows[rindex].Cells[7].FindControl("TXT_PERCENT_DRG"))).Text = percent_GRD.ToString();
        db.ds1.Tables[0].Rows[rindex][7] = percent_GRD;
        ViewState["ds_new"] = db.ds1;

    }
}