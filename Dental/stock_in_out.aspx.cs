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


public partial class stock_in_out : System.Web.UI.Page
{
    public void fill_grid_sales()
    {
        string select = "select stock.id, stock.date, stock.sales_id , products.name as product, customers.name as customer , stock.quantity, inventory.name as inv_name from stock  inner join products on stock.product_id=products.id inner join customers on stock.customer_id = customers.id inner join inventory on stock.inventory_id = inventory.id  where stock.[out]=1";
        db.select(select, sales_grid);
        ViewState.Add("ds", db.ds1);
    }

    public void fill_grid_purchase()
    {
        string select = "select stock.id, stock.date, stock.purchase_id , products.name as product, suppliers.name as supplier , stock.quantity, inventory.name as inv_name from stock  inner join products on stock.product_id=products.id inner join suppliers on stock.supplier_id = suppliers.id inner join inventory on stock.inventory_id = inventory.id  where stock.[in]=1";
        db.select(select, purch_grid);
        ViewState.Add("ds", db.ds1);
    }
    database db = new database();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            fill_grid_purchase();

            fill_grid_sales();
        }
    }
}