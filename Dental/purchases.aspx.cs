using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;


public partial class purchases : System.Web.UI.Page
{
    int new_record;
    int qty_GRD;
    int price_GRD;
    int disacc_GRD;
    int total_price_GRD;
    int profit_GRD;

    public void fill_grid()
    {
        string select = "select purchases.id,purchases.[date], products.name as product,suppliers. name as supplier,purchases.quantity,purchases.price,purchases.total_price, purchases.disaccount,purchases.profit, inventory.id as inventory_id, inventory.name as inventory_name from purchases inner join products on products.id=purchases.product_id inner join suppliers on suppliers.id = purchases.supplier_id inner join inventory on purchases.inventory_id = inventory.id"; 
        db.select(select, GridView1);
        ViewState.Add("ds", db.ds1);
    }

    public void fill_grid_new()
    {
        //string select = "select * from purchases where id = "+TXT_ID.Text+" ";
        //string select_new = "select purchases.id, products.name from purchases inner join products on products.id=purchases.product_id where purchases.supplier_id = "+DRP_SUPPLIER.SelectedValue+" and [date]="+TXT_ID.Text+"";
        //string select_new = "select purchases.id,purchases.[date], products.name as product,suppliers. name as supplier,purchases.quantity,purchases.price,purchases.total_price, purchases.disaccount,purchases.profit,purchases.supplier_id, purchases.product_id from purchases inner join products on products.id=purchases.product_id inner join suppliers on suppliers.id = purchases.supplier_id where purchases.supplier_id = " + DRP_SUPPLIER.SelectedValue + " and [date]='" + TXT_Date.Text + "'";
        string select_new = "select purchases.id,purchases.[date], products.name as product,suppliers. name as supplier,purchases.quantity,purchases.price,purchases.total_price, purchases.disaccount,purchases.profit, inventory.id as inventory_id, inventory.name as inventory_name from purchases inner join products on products.id=purchases.product_id inner join suppliers on suppliers.id = purchases.supplier_id inner join inventory on purchases.inventory_id = inventory.id where purchases.supplier_id = " + DRP_SUPPLIER.SelectedValue + " and [date]='" + TXT_Date.Text + "'";
        db.select(select_new, GridView1);
        ViewState.Add("ds_new", db.ds1);
    }

    public void fill_grid_filter(string criteria)
    {
        string select_new;
        switch (criteria)
        {
            case "supp":
                select_new = "select purchases.id,purchases.[date], products.name as product,suppliers. name as supplier,purchases.quantity,purchases.price,purchases.total_price, purchases.total_price, purchases.disaccount,purchases.profit, inventory.id as inventory_id, inventory.name as inventory_name from purchases inner join products on products.id=purchases.product_id inner join suppliers on suppliers.id = purchases.supplier_id inner join inventory on purchases.inventory_id = inventory.id where purchases.supplier_id = " + DRP_SUPP_FILTER.SelectedValue + "";
                db.select(select_new, GridView1);
                ViewState.Add("ds_filter", db.ds1);
                break;
            case "inv":
                select_new = "select purchases.id,purchases.[date], products.name as product,suppliers. name as supplier,purchases.quantity,purchases.price,purchases.total_price, purchases.total_price, purchases.disaccount,purchases.profit, inventory.id as inventory_id, inventory.name as inventory_name from purchases inner join products on products.id=purchases.product_id inner join suppliers on suppliers.id = purchases.supplier_id inner join inventory on purchases.inventory_id = inventory.id where purchases.inventory_id = " + DRP_INV_FILTER.SelectedValue + "";
                db.select(select_new, GridView1);
                ViewState.Add("ds_filter", db.ds1);
                break;
            case "date":
                select_new = "select purchases.id,purchases.[date], products.name as product,suppliers. name as supplier,purchases.quantity,purchases.price,purchases.total_price, purchases.total_price, purchases.disaccount,purchases.profit,purchases.supplier_id, purchases.product_id from purchases inner join products on products.id=purchases.product_id inner join suppliers on suppliers.id = purchases.supplier_id where [date]='" + TXT_DATE_FILTER.Text + "'";
                db.select(select_new, GridView1);
                ViewState.Add("ds_filter", db.ds1);
                break;
            case "supp_date":
                select_new = "select purchases.id,purchases.[date], products.name as product,suppliers. name as supplier,purchases.quantity,purchases.price,purchases.total_price, purchases.total_price, purchases.disaccount,purchases.profit, inventory.id as inventory_id, inventory.name as inventory_name from purchases inner join products on products.id=purchases.product_id inner join suppliers on suppliers.id = purchases.supplier_id inner join inventory on purchases.inventory_id = inventory.id where purchases.supplier_id = " + DRP_SUPP_FILTER.SelectedValue + " and [date]='" + TXT_DATE_FILTER.Text + "'";
                db.select(select_new, GridView1);
                ViewState.Add("ds_filter", db.ds1);
                break;
            case "all":
                select_new = "select purchases.id,purchases.[date], products.name as product,suppliers. name as supplier,purchases.quantity,purchases.price,purchases.total_price, purchases.total_price, purchases.disaccount,purchases.profit, inventory.id as inventory_id, inventory.name as inventory_name from purchases inner join products on products.id=purchases.product_id inner join suppliers on suppliers.id = purchases.supplier_id inner join inventory on purchases.inventory_id = inventory.id where purchases.supplier_id = " + DRP_SUPP_FILTER.SelectedValue + " and [date]='" + TXT_DATE_FILTER.Text + "' and purchases.inventory_id = " + DRP_INV_FILTER.SelectedValue + "";
                db.select(select_new, GridView1);
                ViewState.Add("ds_filter", db.ds1);
                break;
        }
        
        //string select = "select * from purchases where id = "+TXT_ID.Text+" ";
        //string select_new = "select purchases.id, products.name from purchases inner join products on products.id=purchases.product_id where purchases.supplier_id = "+DRP_SUPPLIER.SelectedValue+" and [date]="+TXT_ID.Text+"";
        
    }

    
    public void txt_id()
    {
        string id = "select max(id)+1 as id from purcheses";
        db.select_id(id, TXT_ID);
        if (TXT_ID.Text == "")
        {
            TXT_ID.Text = 1.ToString();
        }

    }

    public void fill_supp()
    {
        string cst = "select id, name from suppliers";
        db.select_combo(cst, DRP_SUPPLIER);
        db.select_combo(cst, DRP_SUPP_FILTER);
        DRP_SUPPLIER.Items.Insert(0, "Selected Supplier");
        DRP_SUPP_FILTER.Items.Insert(0, "Selected Supplier");
    }

    public void fill_inv()
    {
        string cst = "select id, name from inventory";
        db.select_combo(cst, DRP_INV_FILTER);
        DRP_INV_FILTER.Items.Insert(0, "Selected Inventory");
    }

    //public void fill_product()
    //{
    //    string prod = "select id, name from products";
    //    db.select_combo(prod, DRP_PRODUCT);
    //    DRP_PRODUCT.Items.Insert(0, "Selected Product");
    //}

    //public void fill_cash()
    //{
    //    string cash = "select id, name from cashes";
    //    db.select_combo(cash, DRP_CASH);
    //    DRP_CASH.Items.Insert(0, "Selected Cash");

    //}
    public void clear_txt(params TextBox[] txts)
    {
        for (int i = 0; i < txts.Length; i++)
        {

            txts[i].Text = string.Empty;
        }
    }

    //bool validation_cash(int total_price)
    //{
    //    // validate deduplicate username
    //    // should enter amount
    //    // can't withdraw IFF amount >= Curr Amount
    //    int amount;
    //    string curr = db.select_value("select curr_amount from cash_trans where id = (select max (id) from cash_trans where cash_id=" + DRP_CASH.SelectedValue + ")", "curr_amount");
    //    if (curr == "")
    //    {
    //        amount = 0;
    //    }
    //    else
    //    {
    //        amount = int.Parse(curr);
    //    }
    //    if ( total_price > amount)
    //    {
    //        return false;
    //    }
    //    else
    //    {
    //        return true;
    //    }
    //}
    database db = new database();




    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            //date= string.Format("{0: dd / MM / yyyy}", date).ToString();
            ////TXT_Date.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ////TXT_Date.Text = string.Format("{0:dd / MM / yyyy}", TXT_Date.Text).ToString();
            TXT_Date.Text = date;
            TXT_DATE_FILTER.Text = date;
            fill_supp();
            fill_inv();
            txt_id();
            fill_grid();
            //fill_cash();
            DRP_SUPPLIER.Enabled = true;
            //DRP_CASH.Enabled = false;
            TXT_Date.Enabled = false;
            TXT_ID.Enabled = false;

            
            //db.ds1 = (DataSet)ViewState["ds"];
            DataRow dr = db.ds1.Tables[0].NewRow();
            db.ds1.Tables[0].Rows.Add(dr);
            //dr["id"] = TXT_ID.Text;
            //dr["date"] = TXT_Date.Text;
            //dr["product"] = DRP_SUPPLIER.SelectedItem;
            //dr["product_id"] = DRP_PRODUCT.SelectedValue;
            //dr["supplier"] = DRP_SUPPLIER.SelectedItem;
            //dr["supplier_id"] = DRP_SUPPLIER.SelectedValue;
            dr["quantity"] = 0;
            dr["price"] = 0;
            dr["total_price"] = 0;
            dr["disaccount"] = 0;
            dr["Profit"] = 0;
            GridView1.EditIndex = 0;
            GridView1.DataSource = db.ds1;
            GridView1.DataBind();
        }
    }

   
    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {

    }

    protected void DRP_SUPPLIER_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DRP_SUPPLIER.SelectedIndex > 0)
        //fill_product();

        fill_grid_new();


    }

    protected void DRP_SUPPLIER_TextChanged(object sender, EventArgs e)
    {
        
    }

    protected void TXT_Date_TextChanged(object sender, EventArgs e)
    {
        /*
        db.ds1 = (DataSet)ViewState["ds"];
        if (GridView1.Rows.Count > 0)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                //GridView1.DeleteRow(i);
                db.ds1.Tables[0].Rows[i].Delete();
            }

            GridView1.DataSource = db.ds1;
            GridView1.DataBind();
        }
        fill_grid_new();
        */

        //DRP_SUPPLIER.SelectedIndex = 0;
        //DRP_PRODUCT.SelectedIndex = 0;

    }

    protected void TXT_Date_Load(object sender, EventArgs e)
    {
        
    }

    protected void DRP_PRODUCT_SelectedIndexChanged(object sender, EventArgs e)
    {
        //update

        



        //new_record

        if (GridView1.Rows.Count <= 0)
        {
            ViewState["new_record"] = new_record;
        }
        else
        {
            ViewState["new_record"] = GridView1.Rows.Count;
        }

        //if (new_record == (int)ViewState["new_record"])
        //{
        //    new_record += 1;
        //}
        //else
        //{
        //    new_record = (int)ViewState["new_record"];
        //}

        new_record = (int)ViewState["new_record"];

        GridView1.EditIndex = new_record;
        db.ds1 = (DataSet) ViewState["ds_new"];

        GridView1.DataSource = db.ds1;
        GridView1.DataBind();
        db.ds1 = (DataSet)ViewState["ds_new"];

        // id



        //new row:
        //get Price if exist
        
        DataRow dr = db.ds1.Tables[0].NewRow();
        //dr["id"] = TXT_ID.Text;
        dr["date"] = TXT_Date.Text;
        //dr["product"] = DRP_PRODUCT.SelectedItem;
        //dr["product_id"] = DRP_PRODUCT.SelectedValue;
        dr["supplier"] = DRP_SUPPLIER.SelectedItem;
        //dr["supplier_id"] = DRP_SUPPLIER.SelectedValue;
        dr["quantity"] = 0;
        dr["price"] = 0;
        dr["total_price"] = 0;
        dr["disaccount"] = 0;
        dr["Profit"] = 0;

        

        

        db.ds1.Tables[0].Rows.Add(dr);
        //get price
        string name = db.ds1.Tables[0].Rows[new_record][2].ToString();
        string price = db.select_value("select price from price_list where product_id = (select id from products where name='" + name + "') ", "price");
        int price_product;
        if (price == "")
        {
            price_product = 0;
        }
        else
        {
            price_product = int.Parse(price);
            
        }
        db.ds1.Tables[0].Rows[new_record][5] = price_product;
        //get price
        GridView1.DataSource = db.ds1;
        GridView1.DataBind();
        new_record += 1;
        ViewState["new_record"] = new_record;
        ViewState["ds_new"] = db.ds1;
        //DRP_PRODUCT.SelectedIndex = 0;
        /*

        // purcheses
        string id = "select * from purcheses where name=" + TXT_ID.Text + "";
        string type_id = db.select_value(id, "id");
        int t_id = int.Parse(id);

        // purches count
        string id_cout= "select COUNT(id)as id from purchases where id = 1";


        //suppliers
        string get_supp_id = "select * from types where name='" + DRP_SUPPLIER.SelectedItem + "'";
        string supp_id = db.select_value(get_supp_id, "id");
        int s_id = int.Parse(type_id);

        //product
        string select_product = "";
        */
    }

    protected void BTN_NEW_Click(object sender, EventArgs e)
    {
        new_record = 0;
        ViewState["new_record"]= 0;
        ViewState.Add("new_record", new_record);
        //DRP_PRODUCT.Enabled = true;
        DRP_SUPPLIER.Enabled = true;
        //DRP_CASH.Enabled = true;
        TXT_Date.Enabled = true;
        db.ds1 = (DataSet) ViewState ["ds"];
        if (GridView1.Rows.Count > 0)
        {
            for (int i = 0; i < db.ds1.Tables[0].Rows.Count; i++)
            {
                //GridView1.DeleteRow(i);
                db.ds1.Tables[0].Rows[i].Delete();
            }

            GridView1.DataSource = db.ds1;
            GridView1.DataBind();

           

            //foreach (GridViewRow row in GridView1.Rows)
            //{
            //    GridView1.DeleteRow(row.RowIndex);
            //}


        }

        
        //fill_grid_new();
       

    }

    


   

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    

   


    protected void TXT_QTY_GRD_Init(object sender, EventArgs e)
    {
       
    }

    protected void TXT_QTY_GRD_Unload(object sender, EventArgs e)
    {
        
    }

   

    protected void TXT_QTY_GRD_Load(object sender, EventArgs e)
    {
        
    }



    protected void TXT_QTY_GRD_TextChanged(object sender, EventArgs e)
    {
        int rindex = GridView1.Rows.Count - 1;

        db.ds1 = (DataSet)ViewState["ds"];
        //new_record = (int) ViewState["new_record"];

        if (((TextBox)(GridView1.Rows[rindex].Cells[4].FindControl("TXT_QTY_GRD"))).Text == "")
        {
            qty_GRD = 0;
            db.ds1.Tables[0].Rows[new_record][4] = qty_GRD;
            
        }
        else
        {
            qty_GRD = int.Parse(((TextBox)(GridView1.Rows[rindex].Cells[2].FindControl("TXT_QTY_GRD"))).Text);
            db.ds1.Tables[0].Rows[rindex][4] = qty_GRD;
            ViewState["ds_new"] = db.ds1;
        }

        if (((TextBox)(GridView1.Rows[rindex].Cells[5].FindControl("TXT_PRICE_GRD"))).Text == "")
        {
            price_GRD = 0;
            db.ds1.Tables[0].Rows[rindex][5] = price_GRD;
            
        }
        else
        {
            price_GRD = int.Parse(((TextBox)(GridView1.Rows[rindex].Cells[3].FindControl("TXT_PRICE_GRD"))).Text);
            db.ds1.Tables[0].Rows[rindex][5] = price_GRD;
            ViewState["ds_new"] = db.ds1;
        }
        total_price_GRD = qty_GRD * price_GRD;
        ((TextBox)(GridView1.Rows[rindex].Cells[6].FindControl("TXT_TOTAL_PRICE_GRD"))).Text = total_price_GRD.ToString();
        db.ds1.Tables[0].Rows[rindex][6] = total_price_GRD;

        ViewState["ds"]= db.ds1;


    }

    protected void TXT_PRICE_GRD_TextChanged(object sender, EventArgs e)
    {
        int rindex = GridView1.Rows.Count - 1;
        
        //edit in dataset
        db.ds1 = (DataSet)ViewState["ds"];

        if (((TextBox)(GridView1.Rows[rindex].Cells[2].FindControl("TXT_QTY_GRD"))).Text == "")
        {
             qty_GRD = 0;
            db.ds1.Tables[0].Rows[rindex][4] = qty_GRD;
            //ViewState["ds_new"] = db.ds1;
        }
        else
        {
            qty_GRD = int.Parse(((TextBox)(GridView1.Rows[rindex].Cells[2].FindControl("TXT_QTY_GRD"))).Text);
            db.ds1.Tables[0].Rows[rindex][4] = qty_GRD;
            //ViewState["ds_new"] = db.ds1;
        }

        if (((TextBox)(GridView1.Rows[rindex].Cells[3].FindControl("TXT_PRICE_GRD"))).Text == "")
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
        ((TextBox)(GridView1.Rows[rindex].Cells[4].FindControl("TXT_TOTAL_PRICE_GRD"))).Text = total_price_GRD.ToString();
        db.ds1.Tables[0].Rows[rindex][6] = total_price_GRD;
        ViewState["ds"] = db.ds1;
    }
    
    protected void TXT_DISACC_DRG_TextChanged(object sender, EventArgs e)
    {
        int rindex = GridView1.Rows.Count - 1;

        db.ds1 = (DataSet)ViewState["ds"];
        if (((TextBox)(GridView1.Rows[rindex].Cells[2].FindControl("TXT_QTY_GRD"))).Text == "")
        {
            qty_GRD = 0;
            db.ds1.Tables[0].Rows[rindex][4] = qty_GRD;
        }
        else
        {
            qty_GRD = int.Parse(((TextBox)(GridView1.Rows[rindex].Cells[2].FindControl("TXT_QTY_GRD"))).Text);
            db.ds1.Tables[0].Rows[rindex][4] = qty_GRD;
        }

        if (((TextBox)(GridView1.Rows[rindex].Cells[3].FindControl("TXT_PRICE_GRD"))).Text == "")
        {
            price_GRD = 0;
            db.ds1.Tables[0].Rows[rindex][5] = price_GRD;
        }
        else
        {
            price_GRD = int.Parse(((TextBox)(GridView1.Rows[rindex].Cells[5].FindControl("TXT_PRICE_GRD"))).Text);
            db.ds1.Tables[0].Rows[rindex][5] = price_GRD;
        }

        if (((TextBox)(GridView1.Rows[rindex].Cells[4].FindControl("TXT_TOTAL_PRICE_GRD"))).Text == "")
        {
            total_price_GRD = 0;
            db.ds1.Tables[0].Rows[rindex][6] = total_price_GRD;
        }
        else
        {
            total_price_GRD = int.Parse(((TextBox)(GridView1.Rows[rindex].Cells[4].FindControl("TXT_TOTAL_PRICE_GRD"))).Text);
            db.ds1.Tables[0].Rows[rindex][6] = total_price_GRD;
        }

        if (((TextBox)(GridView1.Rows[rindex].Cells[5].FindControl("TXT_DISACC_DRG"))).Text == "")
        {
            disacc_GRD = 0;
            db.ds1.Tables[0].Rows[rindex][7] = disacc_GRD;
        }
        else
        {
            disacc_GRD = int.Parse(((TextBox)(GridView1.Rows[rindex].Cells[5].FindControl("TXT_DISACC_DRG"))).Text);
            db.ds1.Tables[0].Rows[rindex][7] = disacc_GRD;
        }
        profit_GRD =  (total_price_GRD * disacc_GRD)/100;
        ((TextBox)(GridView1.Rows[rindex].Cells[5].FindControl("TXT_PROFIT_DRG"))).Text = profit_GRD.ToString();
        db.ds1.Tables[0].Rows[rindex][8] = profit_GRD;
        ViewState["ds"] = db.ds1;
    }

    protected void TXT_ID_TextChanged(object sender, EventArgs e)
    {

    }

    protected void TXT_DATE_FILTER_TextChanged(object sender, EventArgs e)
    {
        
        //db.ds1 = (DataSet)ViewState["ds"];
        //if (GridView1.Rows.Count > 0)
        //{
        //    for (int i = 0; i < GridView1.Rows.Count; i++)
        //    {
        //        //GridView1.DeleteRow(i);
        //        db.ds1.Tables[0].Rows[i].Delete();
        //    }

        //    GridView1.DataSource = db.ds1;
        //    GridView1.DataBind();
        //}
        //fill_grid_filter();

    }

    protected void DRP_SUPP_FILTER_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CHK_Supplier.Checked == true && CHK_Date.Checked == true && CHK_INVENTORY.Checked == true)
        {
            db.ds1 = (DataSet)ViewState["ds"];
            if (GridView1.Rows.Count > 0)
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    //GridView1.DeleteRow(i);
                    db.ds1.Tables[0].Rows[i].Delete();
                }

                GridView1.DataSource = db.ds1;
                GridView1.DataBind();
            }
            fill_grid_filter("all");

        }
        if (CHK_Supplier.Checked == true && CHK_Date.Checked == true)
        {
            db.ds1 = (DataSet)ViewState["ds"];
            if (GridView1.Rows.Count > 0)
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    //GridView1.DeleteRow(i);
                    db.ds1.Tables[0].Rows[i].Delete();
                }

                GridView1.DataSource = db.ds1;
                GridView1.DataBind();
            }
            fill_grid_filter("supp_date");

        }

        if (CHK_Supplier.Checked == true && CHK_Date.Checked == false)
        {
            db.ds1 = (DataSet)ViewState["ds"];
            if (GridView1.Rows.Count > 0)
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    //GridView1.DeleteRow(i);
                    db.ds1.Tables[0].Rows[i].Delete();
                }

                GridView1.DataSource = db.ds1;
                GridView1.DataBind();
            }
            fill_grid_filter("supp");

        }

        if (CHK_Supplier.Checked == false && CHK_Date.Checked == true)
        {
            db.ds1 = (DataSet)ViewState["ds"];
            if (GridView1.Rows.Count > 0)
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    //GridView1.DeleteRow(i);
                    db.ds1.Tables[0].Rows[i].Delete();
                }

                GridView1.DataSource = db.ds1;
                GridView1.DataBind();
            }
            fill_grid_filter("date");

        }
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //    //check Update or Insert

        //    string chk = "select id from ";

        //    //update:



        //    //Insert:            
        //    //product id
        //    string select_p_id = "select id from products where name='"+ ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].FindControl("TXT_NAME_GRD"))).Text + "'";
        //    string p_id = db.select_value(select_p_id, "id");
        //    int product_id = int.Parse(p_id);

        //    //max_id
        //    //string select_id = "select id from purchases where date='"+TXT_Date.Text+"' and supplier_id="+((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].FindControl("TXT_ID_GRD"))).Text+"";
        //    string select_id = "select id from purchases where date='" + TXT_Date.Text + "' and supplier_id=" + DRP_SUPPLIER.SelectedValue + "";
        //    string id = db.select_value(select_id, "id");
        //    //int id_pur = int.Parse(id);
        //    int max_id=0;
        //    if (id=="")
        //    {

        //        //insert cash_transaction:
        //        //validtion amount
        //        int total = int.Parse(((TextBox)(GridView1.Rows[e.RowIndex].Cells[6].FindControl("TXT_TOTAL_PRICE_GRD"))).Text);
        //        bool amount_validate = validation_cash(total);
        //        // insert
        //        int amount;
        //        string curr = db.select_value("select curr_amount from cash_trans where id = (select max (id) from cash_trans where cash_id=" + DRP_CASH.SelectedValue + ")", "curr_amount");
        //        if (curr == "" || curr == "0")
        //        {
        //            amount = 0;
        //        }
        //        else
        //        {
        //            amount = int.Parse(curr);
        //        }
        //        int curr_amount;
        //        string insert;
        //        if (amount_validate == true)
        //        {
        //            // get max id from purchase
        //            string purchase_id = "select max(id) from purchases where date='" + TXT_Date.Text + "' and supplier_id=" + DRP_SUPPLIER.SelectedValue + "";
        //            string purch_id = db.select_value(select_id, "id");
        //            int pu_id;
        //            if (purch_id == "" | purch_id=="0")
        //            {
        //                pu_id = 1;
        //            }
        //            else
        //            {
        //                 pu_id= int.Parse(purch_id);
        //            }

        //            //
        //            curr_amount = amount - int.Parse(((TextBox)(GridView1.Rows[e.RowIndex].Cells[6].FindControl("TXT_TOTAL_PRICE_GRD"))).Text);
        //            insert = "insert into cash_trans (id, cash_id, trans_date ,trans_type ,[in],[out],purchase_id,amount,curr_amount  ) select COALESCE (max(id),0)+1, " + DRP_CASH.SelectedValue + ",'" + TXT_Date.Text + "','" + "Purchase" + "','" + false + "','" + true + "'," + pu_id + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[6].FindControl("TXT_TOTAL_PRICE_GRD"))).Text + "," + curr_amount + " from cash_trans";
        //            db.insert(insert);
        //            insert = "Update cashes set id = " + DRP_CASH.SelectedValue + ", name = '" + DRP_CASH.SelectedItem.Text + "', amount = " + curr_amount + " where id = " + DRP_CASH.SelectedValue + " ";
        //            db.insert(insert);
        //            LBL_CASH_AMOUNT.Text = curr_amount.ToString();
        //        }
        //        else
        //        {
        //            return;
        //        }

        //        // insert purchases
        //        //max_id = 1;
        //        string max_p_id = "INSERT INTO purchases (id, date, supplier_id, product_id,inventory_id,quantity,price, total_price, disaccount, profit) select COALESCE (max(id),0)+1 ,'" + TXT_Date.Text + "'," + DRP_SUPPLIER.SelectedValue + "," + product_id + "," + ((DropDownList)(GridView1.Rows[e.RowIndex].Cells[9].FindControl("DRP_INV_GRD"))).SelectedValue + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("TXT_QTY_GRD"))).Text + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[5].FindControl("TXT_PRICE_GRD"))).Text + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[6].FindControl("TXT_TOTAL_PRICE_GRD"))).Text + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[7].FindControl("TXT_DISACC_DRG"))).Text + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[8].FindControl("TXT_PROFIT_DRG"))).Text + " from purchases";
        //        db.insert(max_p_id);

        //        // insert stock in/out:
        //        //get purchese id

        //        string get_pur_id = "select id from purchases where [date]='" + TXT_Date.Text + "' and supplier_id=" + DRP_SUPPLIER.SelectedValue + " and product_id=" + product_id + "";
        //        string pur_id = db.select_value(get_pur_id, "id");
        //        int id_pur = int.Parse(pur_id);

        //        // insert stock in/out
        //        string stock = "INSERT INTO stock (id, date, product_id, quantity, inventory_id,supplier_id,purchase_id,[in]) select COALESCE (max(id),0)+1 ,'" + TXT_Date.Text + "'," + product_id + " ," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("TXT_QTY_GRD"))).Text + "," + ((DropDownList)(GridView1.Rows[e.RowIndex].Cells[9].FindControl("DRP_INV_GRD"))).SelectedValue + "," + DRP_SUPPLIER.SelectedValue + "," + id_pur + ",'"+1+ "' from stock";
        //        db.insert(stock);

        //        //insert stock qty total_in total_out:
        //        //Check Product_id
        //        string get_prod_id = "select product_id from stock_quantity where product_id= " + product_id + "";
        //        string prod_id = db.select_value(get_prod_id, "product_id");
        //        //int id_prod = int.Parse(prod_id);
        //        if (prod_id =="")
        //        {
        //            //string stock_qty = "INSERT INTO stock_quantity (stock_id, product_id, total_in,total_net_in) values (" + ((DropDownList)(GridView1.Rows[e.RowIndex].Cells[9].FindControl("DRP_INV_GRD"))).SelectedValue + "," + product_id + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("TXT_QTY_GRD"))).Text + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("TXT_QTY_GRD"))).Text + ")";
        //            string stock_qty = "INSERT INTO stock_quantity (product_id, total_in,total_net_in) values (" + product_id + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("TXT_QTY_GRD"))).Text + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("TXT_QTY_GRD"))).Text + ")";
        //            db.insert(stock_qty);
        //        }

        //        else
        //        {
        //            //get current total_net_in
        //            string get_total_net_in = "select total_net_in from stock_quantity where product_id = " + product_id + "";
        //            string total_net_in = db.select_value(get_total_net_in, "total_net_in");
        //            int net_in = int.Parse(total_net_in);
        //            if (total_net_in == "" || net_in == 0)
        //            {
        //                //string stock_qty = "INSERT INTO stock_quantity (product_id, total_net_in) values (" + product_id + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("TXT_QTY_GRD"))).Text + ")";
        //                string stock_qty = "UPDATE stock_quantity SET total_net_in = " + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("TXT_QTY_GRD"))).Text + " where product_id=" + product_id + "";
        //                db.insert(stock_qty);
        //            }

        //            else
        //            {
        //                //get current total_in
        //                string get_total_in = "select total_in from stock_quantity where product_id = " + product_id + "";
        //                string total_in = db.select_value(get_total_in, "total_in");
        //                int in_total = int.Parse(total_in); 
        //                int sum_total = in_total + int.Parse(((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("TXT_QTY_GRD"))).Text);
        //                //calculate sum total_net_in
        //                int sum_net_in = net_in + int.Parse(((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("TXT_QTY_GRD"))).Text);
        //                //update
        //                string update_total_in = "UPDATE stock_quantity SET total_in = " + sum_total + " , total_net_in= "+sum_net_in+"  where product_id=" + product_id + "";
        //                db.update(update_total_in);
        //            }                
        //        }

        //        // insert price list
        //        string price_list = db.select_value("select product_id from price_list where product_id= " + product_id + "", "product_id");
        //        if (price_list=="")
        //        {
        //            string stock_qty = "INSERT INTO price_list (product_id, price) values (" + product_id + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[5].FindControl("TXT_PRICE_GRD"))).Text + ")";
        //            db.insert(stock_qty);
        //        }
        //        else
        //        {
        //            string update_total_in = "UPDATE price_list SET price= " + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[5].FindControl("TXT_PRICE_GRD"))).Text + " where product_id=" + product_id + "";
        //            db.update(update_total_in);
        //        }


        //    }
        //    else
        //    {
        //        string insert;

        //        //insert cash_transaction:
        //        //validtion amount
        //        int total = int.Parse(((TextBox)(GridView1.Rows[e.RowIndex].Cells[6].FindControl("TXT_TOTAL_PRICE_GRD"))).Text);
        //        bool amount_validate = validation_cash(total);
        //        // insert
        //        int amount;
        //        string curr = db.select_value("select curr_amount from cash_trans where id = (select max (id) from cash_trans where cash_id=" + DRP_CASH.SelectedValue + ")", "curr_amount");
        //        if (curr == "" || curr == "0")
        //        {
        //            amount = 0;
        //        }
        //        else
        //        {
        //            amount = int.Parse(curr);
        //        }
        //        int curr_amount;
        //        if (amount_validate == true)
        //        {
        //            // get max id from purchase
        //            string purchase_id = "select max(id) from purchases where date='" + TXT_Date.Text + "' and supplier_id=" + DRP_SUPPLIER.SelectedValue + "";
        //            string purch_id = db.select_value(select_id, "id");
        //            int pu_id = int.Parse(purch_id);
        //            //
        //            curr_amount = amount - int.Parse(((TextBox)(GridView1.Rows[e.RowIndex].Cells[6].FindControl("TXT_TOTAL_PRICE_GRD"))).Text);
        //            insert = "insert into cash_trans (id, cash_id, trans_date ,trans_type ,[in],[out],purchase_id,amount,curr_amount  ) select COALESCE (max(id),0)+1, " + DRP_CASH.SelectedValue + ",'" + TXT_Date.Text + "','" + "Purchase" + "','" + false + "','" + true + "'," + max_id + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[6].FindControl("TXT_TOTAL_PRICE_GRD"))).Text + "," + curr_amount + " from cash_trans";
        //            db.insert(insert);
        //            insert = "Update cashes set id = " + DRP_CASH.SelectedValue + ", name = '" + DRP_CASH.SelectedItem.Text + "', amount = " + curr_amount + " where id = " + DRP_CASH.SelectedValue + " ";
        //            db.insert(insert);
        //            LBL_CASH_AMOUNT.Text = curr_amount.ToString();
        //        }
        //        else
        //        {
        //            return;
        //        }

        //        // insert purchases
        //        max_id = int.Parse(id);
        //        insert = "INSERT INTO purchases (id, date, supplier_id, product_id,inventory_id,quantity,price, total_price, disaccount, profit) VALUES (" + max_id + ",'" + TXT_Date.Text + "'," + DRP_SUPPLIER.SelectedValue + "," + product_id + "," + ((DropDownList)(GridView1.Rows[e.RowIndex].Cells[9].FindControl("DRP_INV_GRD"))).SelectedValue + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("TXT_QTY_GRD"))).Text + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[5].FindControl("TXT_PRICE_GRD"))).Text + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[6].FindControl("TXT_TOTAL_PRICE_GRD"))).Text + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[7].FindControl("TXT_DISACC_DRG"))).Text + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[8].FindControl("TXT_PROFIT_DRG"))).Text + ")";
        //        db.insert(insert);

        //        // insert stock in/out:
        //        //get purchese id

        //        string get_pur_id = "select id from purchases where [date]='" + TXT_Date.Text + "' and supplier_id=" + DRP_SUPPLIER.SelectedValue + " and product_id=" + product_id + "";
        //        string pur_id = db.select_value(get_pur_id, "id");
        //        int id_pur = int.Parse(pur_id);

        //        // insert stock in/out
        //        string stock = "INSERT INTO stock (id, date, product_id, quantity, inventory_id,supplier_id,purchase_id,[in]) select COALESCE (max(id),0)+1 ,'" + TXT_Date.Text + "'," + product_id + " ," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("TXT_QTY_GRD"))).Text + "," + ((DropDownList)(GridView1.Rows[e.RowIndex].Cells[9].FindControl("DRP_INV_GRD"))).SelectedValue + "," + DRP_SUPPLIER.SelectedValue + "," + id_pur + ",'" + 1 + "' from stock";
        //        db.insert(stock);

        //        //insert stock qty total_in total_out:
        //        //Check Product_id
        //        string get_prod_id = "select product_id from stock_quantity where product_id= " + product_id + "";
        //        string prod_id = db.select_value(get_prod_id, "product_id");
        //        //int id_prod = int.Parse(prod_id);
        //        if (prod_id == "")
        //        {
        //            //string stock_qty = "INSERT INTO stock_quantity (stock_id, product_id, total_in,total_net_in) values (" + ((DropDownList)(GridView1.Rows[e.RowIndex].Cells[9].FindControl("DRP_INV_GRD"))).SelectedValue + "," + product_id + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("TXT_QTY_GRD"))).Text + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("TXT_QTY_GRD"))).Text + ")";
        //            string stock_qty = "INSERT INTO stock_quantity (product_id, total_in,total_net_in) values (" + product_id + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("TXT_QTY_GRD"))).Text + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("TXT_QTY_GRD"))).Text + ")";
        //            db.insert(stock_qty);
        //        }

        //        else
        //        {
        //            //get current total_net_in
        //            string get_total_net_in = "select total_net_in from stock_quantity where product_id = " + product_id + "";
        //            string total_net_in = db.select_value(get_total_net_in, "total_net_in");
        //            int net_in = int.Parse(total_net_in);
        //            if (total_net_in == "" || net_in == 0)
        //            {
        //                //string stock_qty = "INSERT INTO stock_quantity (product_id, total_net_in) values (" + product_id + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("TXT_QTY_GRD"))).Text + ")";
        //                string stock_qty = "UPDATE stock_quantity SET total_net_in = " + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("TXT_QTY_GRD"))).Text + " where product_id=" + product_id + "";
        //                db.insert(stock_qty);
        //            }

        //            else
        //            {
        //                //get current total_in
        //                string get_total_in = "select total_in from stock_quantity where product_id = " + product_id + "";
        //                string total_in = db.select_value(get_total_in, "total_in");
        //                int in_total = int.Parse(total_in);
        //                int sum_total = in_total + int.Parse(((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("TXT_QTY_GRD"))).Text);
        //                //calculate sum total_net_in
        //                int sum_net_in = net_in + int.Parse(((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("TXT_QTY_GRD"))).Text);
        //                //update
        //                string update_total_in = "UPDATE stock_quantity SET total_in = " + sum_total + " , total_net_in= " + sum_net_in + "  where product_id=" + product_id + "";
        //                db.update(update_total_in);
        //            }
        //        }

        //        // insert price list
        //        string price_list = db.select_value("select product_id from price_list where product_id= " + product_id + "", "product_id");
        //        if (price_list == "")
        //        {
        //            string stock_qty = "INSERT INTO price_list (product_id, price) values (" + product_id + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[5].FindControl("TXT_PRICE_GRD"))).Text + ")";
        //            db.insert(stock_qty);
        //        }
        //        else
        //        {
        //            string update_total_in = "UPDATE price_list SET price= " + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[5].FindControl("TXT_PRICE_GRD"))).Text + " where product_id=" + product_id + "";
        //            db.update(update_total_in);
        //        }

        //    }

        //    GridView1.EditIndex = -1;

        //    fill_grid_new();

        //    //if (max_id == id_pur)
        //    //{
        //    //    //string product_id = "select ";
        //    //    //string insert = "INSERT INTO purchases (id, date, supplier_id, product_id,inventory_id,quantity,price, total_price, disaccount, profit) VALUES (" + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[0].FindControl("id"))).Text + ",'"+TXT_Date.Text+"',"+DRP_SUPPLIER.SelectedValue+","+product_id+","+ ((DropDownList)(GridView1.Rows[e.RowIndex].Cells[9].FindControl("DRP_INV_GRD"))).SelectedValue + ","+ ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("TXT_QTY_GRD"))).Text + ","+ ((TextBox)(GridView1.Rows[e.RowIndex].Cells[5].FindControl("TXT_PRICE_GRD"))).Text + ","+ ((TextBox)(GridView1.Rows[e.RowIndex].Cells[6].FindControl("TXT_TOTAL_PRICE_GRD"))).Text + ","+ ((TextBox)(GridView1.Rows[e.RowIndex].Cells[7].FindControl("TXT_DISACC_DRG"))).Text + ","+ ((TextBox)(GridView1.Rows[e.RowIndex].Cells[8].FindControl("TXT_PROFIT_DRG"))).Text + ")";
        //    //    string insert = "INSERT INTO purchases (id, date, supplier_id, product_id,inventory_id,quantity,price, total_price, disaccount, profit) VALUES (" +max_id+ ",'" + TXT_Date.Text + "'," + DRP_SUPPLIER.SelectedValue + "," + product_id + "," + ((DropDownList)(GridView1.Rows[e.RowIndex].Cells[9].FindControl("DRP_INV_GRD"))).SelectedValue + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("TXT_QTY_GRD"))).Text + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[5].FindControl("TXT_PRICE_GRD"))).Text + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[6].FindControl("TXT_TOTAL_PRICE_GRD"))).Text + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[7].FindControl("TXT_DISACC_DRG"))).Text + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[8].FindControl("TXT_PROFIT_DRG"))).Text + ")";
        //    //    db.insert(insert);
        //    //}
        //    //else
        //    //{
        //    //    //string max_id = "INSERT INTO purchases (id, date, supplier_id, product_id,inventory_id) select  max(id) +1,'1-1-2022',1,2,1 from purchases";
        //    //    string max_p_id = "INSERT INTO purchases (id, date, supplier_id, product_id,inventory_id,quantity,price, total_price, disaccount, profit) select COALESCE (max(id),0)+1 ,'" + TXT_Date.Text + "'," + DRP_SUPPLIER.SelectedValue + "," + product_id + "," + ((DropDownList)(GridView1.Rows[e.RowIndex].Cells[9].FindControl("DRP_INV_GRD"))).SelectedValue + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("TXT_QTY_GRD"))).Text + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[5].FindControl("TXT_PRICE_GRD"))).Text + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[6].FindControl("TXT_TOTAL_PRICE_GRD"))).Text + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[7].FindControl("TXT_DISACC_DRG"))).Text + "," + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[8].FindControl("TXT_PROFIT_DRG"))).Text +" from purchases";
        //    //    db.insert(max_p_id);
        //    //}

        //    //stock:



    }

    protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
       
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        /*
        db.ds1 = (DataSet) ViewState["ds"];
        int index = e.RowIndex;
        
        db.deleting_grid(db.ds1, "purchases", GridView1, index);
        GridView1.DataSource = db.ds1;
        GridView1.DataBind();
        */
        //get suppiler id
        string supp_id = db.select_value("select id from suppliers where name = '" + ((Label)(GridView1.Rows[e.RowIndex].Cells[3].FindControl("LBL_SUPP_NAME_GRD"))).Text + "'", "id");
        int id_supp = int.Parse(supp_id);

        //get product id
        string product_id = db.select_value("select id from products where name = '" + ((Label)(GridView1.Rows[e.RowIndex].Cells[2].FindControl("LBL_NAME_GRD"))).Text + "'", "id");
        int id_product = int.Parse(product_id);

        string delete_row = "delete from purchases where id= " + ((Label)(GridView1.Rows[e.RowIndex].Cells[0].FindControl("LBL_ID_GRD"))).Text + " and [date]='" + ((Label)(GridView1.Rows[e.RowIndex].Cells[1].FindControl("LBL_DATE_GRD"))).Text + "'and supplier_id=" + id_supp + " and product_id=" + id_product + "";
        db.delete(delete_row);

        if (DRP_SUPPLIER.SelectedIndex != 0)
        {

            fill_grid_new();
        }

        else
        {
            
            fill_grid();
        }


    }

    protected void BTN_REFRESH_Click(object sender, EventArgs e)
    {

    }

    protected void CHK_Supplier_CheckedChanged(object sender, EventArgs e)
    {
        DRP_SUPP_FILTER.SelectedIndex = 0;
        DRP_INV_FILTER.SelectedIndex = 0;
    }

    protected void CHK_Date_CheckedChanged(object sender, EventArgs e)
    {
        DRP_SUPP_FILTER.SelectedIndex = 0;
        DRP_INV_FILTER.SelectedIndex = 0;

    }

    protected void CHK_INVENTORY_CheckedChanged(object sender, EventArgs e)
    {
        DRP_SUPP_FILTER.SelectedIndex = 0;
        DRP_INV_FILTER.SelectedIndex = 0;

    }



    protected void DRP_CASH_SelectedIndexChanged(object sender, EventArgs e)
    {
        //int amount;
        //string curr = db.select_value("select curr_amount from cash_trans where id = (select max (id) from cash_trans where cash_id=" + DRP_CASH.SelectedValue + ")", "curr_amount");
        //if (curr == "")
        //{
        //    amount = 0;
        //}
        //else
        //{
        //    amount = int.Parse(curr);
        //}
        //LBL_CASH_AMOUNT.Text = amount.ToString();
    }

    protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
}