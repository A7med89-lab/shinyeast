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
    public void fill_grid_stock_net_in()
    {
        string select = "select stock_quantity.product_id, products.name, stock_quantity.total_net_in from stock_quantity inner join products on stock_quantity.product_id = products.id ";
        db.select(select,GRD_STOCK_NET);
        ViewState.Add("ds", db.ds1);
    }

    public void fill_grid_stock_total()
    {
        string select = "select stock_quantity.product_id, products.name, stock_quantity.total_in, stock_quantity.total_out from stock_quantity inner join products on stock_quantity.product_id = products.id ";
        db.select(select, GRD_STOCK_TOTAL);
        ViewState.Add("ds", db.ds1);
    }

    database db = new database();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack==false)
        {
            fill_grid_stock_net_in();
            fill_grid_stock_total();
        }
        
    }
}