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

    public void fill_status()
    {
        DRP_STATUS_FILTER.Items.Insert(0, "اختيار الحالة");
        DRP_STATUS_FILTER.Items.Insert(1, "قبول");
        DRP_STATUS_FILTER.Items.Insert(2, "رفض");
    }

    public void fill_grid_purchase_filter(string critria)
    {
        string select = "";
        switch (critria)
        {
            case "inv":
            select = "select stock_orders.id, stock_orders.[date], stock_orders.[time] , stock_orders.purchase_id,  inventories.name as inv_name, stock_order_details.quantity, suppliers.name as supplier_name, stock_orders.[status]  from stock_orders inner join  stock_order_details on stock_orders.id = stock_order_details.stock_order_id inner join inventories on stock_orders.inventory_id = inventories.id inner join suppliers on stock_orders.supplier_id = suppliers.id where stock_orders.[in]=1 and inventories.id = " + DRP_INV_FILTER.SelectedValue + "";
            db.select(select, PURCHASE_GRD);
            break;

            case "supplier":
            select = "select stock_orders.id, stock_orders.[date], stock_orders.[time] , stock_orders.purchase_id,  inventories.name as inv_name, stock_order_details.quantity, suppliers.name as supplier_name, stock_orders.[status]  from stock_orders inner join stock_order_details on stock_orders.id = stock_order_details.stock_order_id inner join inventories on stock_orders.inventory_id = inventories.id inner join suppliers on stock_orders.supplier_id = suppliers.id where stock_orders.[in] = 1 and suppliers.id = "+DRP_SUPP_FILTER.SelectedValue+"";
            db.select(select, PURCHASE_GRD);
            break;

            case "permit":
            select = "select stock_orders.id, stock_orders.[date], stock_orders.[time] , stock_orders.purchase_id,  inventories.name as inv_name, stock_order_details.quantity, suppliers.name as supplier_name, stock_orders.[status]  from stock_orders inner join  stock_order_details on stock_orders.id = stock_order_details.stock_order_id inner join inventories on stock_orders.inventory_id = inventories.id inner join suppliers on stock_orders.supplier_id = suppliers.id where stock_orders.[in]=1 and stock_orders.status=1";
            db.select(select, PURCHASE_GRD);
            break;

            case "deny":
            select = "select stock_orders.id, stock_orders.[date], stock_orders.[time] , stock_orders.purchase_id,  inventories.name as inv_name, stock_order_details.quantity, suppliers.name as supplier_name, stock_orders.[status]  from stock_orders inner join  stock_order_details on stock_orders.id = stock_order_details.stock_order_id inner join inventories on stock_orders.inventory_id = inventories.id inner join suppliers on stock_orders.supplier_id = suppliers.id where stock_orders.[in]=1 and stock_orders.status=0";
            db.select(select, PURCHASE_GRD);
            break;

            case "date":
           
            select = "select stock_orders.id, stock_orders.[date], stock_orders.[time] , stock_orders.purchase_id,  inventories.name as inv_name, stock_order_details.quantity, suppliers.name as supplier_name, stock_orders.[status]  from stock_orders inner join  stock_order_details on stock_orders.id = stock_order_details.stock_order_id inner join inventories on stock_orders.inventory_id = inventories.id inner join suppliers on stock_orders.supplier_id = suppliers.id where stock_orders.[in]=1 and stock_orders.[date]='"+TXT_DATE_FILTER.Text+"'";
            db.select(select, PURCHASE_GRD);
            break;


        }

        
    }

    public void fill_grid_sales()
    {
        string select = "select stock.id, stock.date, stock.sales_id , products.name as product, customers.name as customer , stock.quantity, inventory.name as inv_name from stock  inner join products on stock.product_id=products.id inner join customers on stock.customer_id = customers.id inner join inventory on stock.inventory_id = inventory.id  where stock.[out]=1";
        db.select(select, SALES_GRD);
        ViewState.Add("ds", db.ds1);
    }

    public void fill_grid_purchase()
    {
        string select = "select stock_orders.id, stock_orders.[date], stock_orders.[time] , stock_orders.purchase_id,  inventories.name as inv_name, stock_order_details.quantity, suppliers.name as supplier_name, stock_orders.[status]  from stock_orders inner join  stock_order_details on stock_orders.id = stock_order_details.stock_order_id inner join inventories on stock_orders.inventory_id = inventories.id inner join suppliers on stock_orders.supplier_id = suppliers.id where stock_orders.[in]=1";
        db.select(select, PURCHASE_GRD);
        ViewState.Add("ds", db.ds1);
    }
    database db = new database();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            fill_grid_purchase();

            fill_grid_sales();

            fill_supp();
            fill_inv();
            fill_status();
        }
    }

    protected void sales_grid_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void DRP_SUPP_FILTER_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DRP_SUPP_FILTER.SelectedIndex !=0)
        {
            fill_grid_purchase_filter("supplier");
        }
        else
        {
            fill_grid_purchase();
        }
    }

    protected void DRP_INV_FILTER_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DRP_INV_FILTER.SelectedIndex != 0)
        {
            fill_grid_purchase_filter("inv");
        }
        else
        {
            fill_grid_purchase();
        }
    }

    protected void DRP_STATUS_FILTER_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DRP_STATUS_FILTER.SelectedIndex == 1)
        {
            fill_grid_purchase_filter("permit");
        }
        else if(DRP_STATUS_FILTER.SelectedIndex == 2)
        {
            fill_grid_purchase_filter("deny");
        }
        else
        {
            fill_grid_purchase();
        }

    }

    protected void TXT_DATE_FILTER_TextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TXT_DATE_FILTER.Text))
        {
            fill_grid_purchase();
        }
        else
        {
            fill_grid_purchase_filter("date");
        }
        

        
        
    }
}