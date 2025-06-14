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

public partial class stock_quantity : System.Web.UI.Page
{
    public void fill_grid_stock()
    {
        //string select = "select stock_quantity.product_id, products.name, stock_quantity.total_net_in from stock_quantity inner join products on stock_quantity.product_id = products.id ";
        string select = "select stock_total_quantities.inventory_id, inventories.name as inventory_name, stock_total_quantities.product_id, products.name as product_name , stock_total_quantities.total_in, stock_total_quantities.total_out, stock_total_quantities.total_net_in as net , stock_prices.sales_stock_price as product_price from stock_total_quantities inner join products on stock_total_quantities.product_id = products.id inner join inventories on stock_total_quantities.inventory_id = inventories.id left join stock_prices on stock_total_quantities.product_id = stock_prices.product_id ";
        db.select(select, GRD_STOCK_TOTAL);
        ViewState.Add("ds", db.ds1);
    }

    //public void fill_grid_stock_total()
    //{
    //    string select = "select stock_quantity.product_id, products.name, stock_quantity.total_in, stock_quantity.total_out from stock_quantity inner join products on stock_quantity.product_id = products.id ";
    //    db.select(select, GRD_STOCK_TOTAL);
    //    ViewState.Add("ds", db.ds1);
    //}

    database db = new database();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack==false)
        {
            fill_grid_stock();
            
        }
        
    }
}