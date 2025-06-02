using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;


public partial class purchases : System.Web.UI.Page
{
   
    public void fill_grid()
    {
        string select = "select purchases.id,purchases.[date], products.name as product,suppliers. name as supplier,purchases.quantity,purchases.price,purchases.total_price, purchases.discount,purchases.profit, inventory.id as inventory_id, inventory.name as inventory_name from purchases inner join products on products.id=purchases.product_id inner join suppliers on suppliers.id = purchases.supplier_id inner join inventory on purchases.inventory_id = inventory.id"; 
        db.select(select, GridView1);
        ViewState.Add("ds", db.ds1);
    }

    public void fill_grid_new()
    {
        //string select = "select * from purchases where id = "+TXT_ID.Text+" ";
        //string select_new = "select purchases.id, products.name from purchases inner join products on products.id=purchases.product_id where purchases.supplier_id = "+DRP_SUPPLIER.SelectedValue+" and [date]="+TXT_ID.Text+"";
        //string select_new = "select purchases.id,purchases.[date], products.name as product,suppliers. name as supplier,purchases.quantity,purchases.price,purchases.total_price, purchases.disaccount,purchases.profit,purchases.supplier_id, purchases.product_id from purchases inner join products on products.id=purchases.product_id inner join suppliers on suppliers.id = purchases.supplier_id where purchases.supplier_id = " + DRP_SUPPLIER.SelectedValue + " and [date]='" + TXT_Date.Text + "'";
        string select_new = "select purchases.id,purchases.[date], products.name as product,suppliers. name as supplier,purchases.quantity,purchases.price,purchases.total_price, purchases.discount,purchases.profit, inventory.id as inventory_id, inventory.name as inventory_name from purchases inner join products on products.id=purchases.product_id inner join suppliers on suppliers.id = purchases.supplier_id inner join inventory on purchases.inventory_id = inventory.id where purchases.supplier_id = " + DRP_SUPPLIER.SelectedValue + " and [date]='" + TXT_Date.Text + "'";
        db.select(select_new, GridView1);
        ViewState.Add("ds_new", db.ds1);
    }

    

    
    public void txt_id()
    {
        string id = "select max(id)+1 as id from purchases";
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
       
        DRP_SUPPLIER.Items.Insert(0, "اختيار المورد");
       
    
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

    public void start_load()
    {
        string date = DateTime.Now.ToString("yyyy-MM-dd");
        string time = DateTime.Now.ToString("HH:mm:ss");
        TXT_Date.Text = date;
        fill_supp();
        txt_id();
        DRP_SUPPLIER.Enabled = true;
        TXT_Date.Enabled = false;

        DataRow dr = grid_dt.NewRow();
        grid_dt.Rows.Add(dr);
        grid_dt.Columns.Add("product_name", typeof(string));
        grid_dt.Columns.Add("product_id", typeof(int));
        grid_dt.Columns.Add("price", typeof(decimal));
        grid_dt.Columns.Add("quantity", typeof(int));
        grid_dt.Columns.Add("total_price", typeof(decimal));
        grid_dt.Columns.Add("tax", typeof(decimal));
        grid_dt.Columns.Add("total_after_tax", typeof(decimal));
        grid_dt.Columns.Add("discount", typeof(decimal));
        grid_dt.Columns.Add("total_after_discount", typeof(decimal));       
        grid_dt.Columns.Add("Profit", typeof(decimal));
        grid_ds.Tables.Add(grid_dt);
        GridView1.DataSource = grid_ds;
        GridView1.DataBind();

        //fill drop in data gridview
        string qurey = "select id, name from products";
        DropDownList drp_prod_name_grid = GridView1.Rows[0].FindControl("DRP_NAME_GRD") as DropDownList;
        DataSet drp_prod_name_grd_ds = db.fill_drop_grid(qurey, drp_prod_name_grid, "اختيار المنتج");
        
        //viewstate
        ViewState.Add("drp_prod_name_grd_ds", drp_prod_name_grd_ds);
        ViewState.Add("grid_ds", grid_ds);

        //keep selected index in drp in gridview
        int drp_prod_name_index_grd = drp_prod_name_grid.SelectedIndex;
        ViewState.Add("drp_prod_name_index_grd", drp_prod_name_index_grd);

        //define delete flag
        delete_flag = false;
        ViewState.Add("delete_flag", delete_flag);
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

    public void calculate_grid()
    {
        int rindex = GridView1.Rows.Count - 1;

        //define variables
        grid_ds = (DataSet)ViewState["grid_ds"];
        string qty = ((TextBox)GridView1.Rows[rindex].FindControl("TXT_QTY_GRD")).Text.Trim();
        string price = ((TextBox)GridView1.Rows[rindex].FindControl("TXT_PRICE_GRD")).Text.Trim();
        string disc = ((TextBox)GridView1.Rows[rindex].FindControl("TXT_DISC_GRD")).Text.Trim();
        string tax = ((TextBox)GridView1.Rows[rindex].FindControl("TXT_TAX_GRD")).Text.Trim();
        decimal total_price_grd = 0, total_after_discc = 0, total_after_tax = 0, profit = 0;
        int drp_prod_name_index_grd = (int)ViewState["drp_prod_name_index_grd"];
        DataSet drp_prod_name_grd_ds = ViewState["drp_prod_name_grd_ds"] as DataSet;
        int[] prod_id = new int[GridView1.Rows.Count - 1];
        string[] prod_name = new string[GridView1.Rows.Count - 1];


        //store all product id and name in arrays
        for (int i = 0; i < GridView1.Rows.Count - 1; i++)
        {
            prod_id[i] = int.Parse(((Label)GridView1.Rows[i].FindControl("LBL_PROD_ID_GRD")).Text);
            prod_name[i] = ((Label)GridView1.Rows[i].FindControl("LBL_PROD_NAME_GRD")).Text;

        }

        // assgin price = 0 if price fild in grid = null or empty
        if (price != null && string.IsNullOrWhiteSpace(price.ToString()))
        {
            grid_ds.Tables[0].Rows[grid_ds.Tables[0].Rows.Count - 1][2] = 0;
            price = 0.ToString();
        }
        else
        {
            grid_ds.Tables[0].Rows[grid_ds.Tables[0].Rows.Count - 1][2] = price;
        }

        // assgin all product's values as per qty value or 0 if qty = 0
        if (qty != null && (string.IsNullOrEmpty(qty) || qty == "0"))
        {

            for (int i = 3; i <= 9; i++)
                grid_ds.Tables[0].Rows[rindex][i] = 0;

        }

        else
        {
            //calculate total
            grid_ds.Tables[0].Rows[rindex][3] = int.Parse(((TextBox)(GridView1.Rows[rindex].Cells[3].FindControl("TXT_QTY_GRD"))).Text);
            total_price_grd = int.Parse(price) * int.Parse(qty);
            grid_ds.Tables[0].Rows[rindex][4] = total_price_grd;

            for (int i = 5; i <= 9; i++)
                grid_ds.Tables[0].Rows[rindex][i] = 0;

            //calculate tax & total after tax
            if (!(tax != null && (string.IsNullOrEmpty(tax) || tax == "0")))
            {
                grid_ds.Tables[0].Rows[rindex][5] = tax;
                total_after_tax = (total_price_grd * int.Parse(tax) / 100) + total_price_grd;
                grid_ds.Tables[0].Rows[rindex][6] = total_after_tax;
            }
            else
            {
                for (int i = 5; i <= 6; i++)
                    grid_ds.Tables[0].Rows[rindex][i] = 0;
            }

            //calculate discount & total after discount
            if (!(disc != null && (string.IsNullOrEmpty(disc) || disc == "0")))
            {
                grid_ds.Tables[0].Rows[rindex][7] = disc;
                total_after_discc = total_price_grd - (total_price_grd * int.Parse(disc) / 100);
                grid_ds.Tables[0].Rows[rindex][8] = total_after_discc;
            }
            else
            {
                for (int i = 7; i <= 8; i++)
                    grid_ds.Tables[0].Rows[rindex][i] = 0;
            }

            //calculate profit
            if (!(tax != null && (string.IsNullOrEmpty(tax) || tax == "0")))
            {
                profit = total_price_grd * int.Parse(disc) / 100;
                grid_ds.Tables[0].Rows[rindex][9] = profit;
            }


        }

        // restore product id and name to dataset to bind into grid
        for (int i = 0; i < GridView1.Rows.Count - 1; i++)
        {
            grid_ds.Tables[0].Rows[i][0] = prod_name[i];
            grid_ds.Tables[0].Rows[i][1] = prod_id[i];
        }
        ViewState["grid_ds"] = grid_ds;
        GridView1.DataSource = grid_ds;
        GridView1.DataBind();

        //fill_drp_prod_name_grd;
        DropDownList dr_prod_name_grid = GridView1.Rows[GridView1.Rows.Count - 1].FindControl("DRP_NAME_GRD") as DropDownList;
        dr_prod_name_grid.DataSource = drp_prod_name_grd_ds.Tables[0];
        dr_prod_name_grid.DataTextField = drp_prod_name_grd_ds.Tables[0].Columns["name"].ToString();
        dr_prod_name_grid.DataValueField = drp_prod_name_grd_ds.Tables[0].Columns["id"].ToString();
        dr_prod_name_grid.DataBind();
        dr_prod_name_grid.Items.Insert(0, "اختيار المنتج");

        //store index of selected product in dropdown list
        dr_prod_name_grid.SelectedIndex = drp_prod_name_index_grd;

        //hide dropdown list and show label
        for (int i = 0; i < GridView1.Rows.Count - 1; i++)
        {
            ((DropDownList)GridView1.Rows[i].FindControl("DRP_NAME_GRD")).Visible = false;
            ((Label)GridView1.Rows[i].FindControl("LBL_PROD_ID_GRD")).Visible = false;
            ((Label)GridView1.Rows[i].FindControl("LBL_PROD_NAME_GRD")).Visible = true;
        }

    }


    database db = new database();
    DataSet grid_ds = new DataSet();
    DataTable grid_dt = new DataTable("purch");
    bool delete_flag = false;
    




    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            start_load();            
        }
        
    }

   
    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {

    }

    protected void DRP_SUPPLIER_SelectedIndexChanged(object sender, EventArgs e)

    {       

    }

    protected void DRP_SUPPLIER_TextChanged(object sender, EventArgs e)
    {
        
    }

    protected void TXT_Date_TextChanged(object sender, EventArgs e)
    {
       



    }

    protected void TXT_Date_Load(object sender, EventArgs e)
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
        
        //define variables
        grid_ds = (DataSet)ViewState["grid_ds"];
        string qty = ((TextBox)GridView1.Rows[rindex].FindControl("TXT_QTY_GRD")).Text.Trim();
        string price = ((TextBox)GridView1.Rows[rindex].FindControl("TXT_PRICE_GRD")).Text.Trim();
        string disc = ((TextBox)GridView1.Rows[rindex].FindControl("TXT_DISC_GRD")).Text.Trim();
        string tax = ((TextBox)GridView1.Rows[rindex].FindControl("TXT_TAX_GRD")).Text.Trim();
        decimal total_price_grd = 0, total_after_discc = 0, total_after_tax = 0 , profit = 0;
        int drp_prod_name_index_grd = (int)ViewState["drp_prod_name_index_grd"];
        DataSet drp_prod_name_grd_ds = ViewState["drp_prod_name_grd_ds"] as DataSet;
        int[] prod_id = new int[GridView1.Rows.Count - 1];
        string[] prod_name = new string[GridView1.Rows.Count - 1];

        
        //store all product id and name in arrays
        for (int i = 0; i < GridView1.Rows.Count - 1; i++)
        {
            prod_id[i] = int.Parse(((Label)GridView1.Rows[i].FindControl("LBL_PROD_ID_GRD")).Text);
            prod_name[i] = ((Label)GridView1.Rows[i].FindControl("LBL_PROD_NAME_GRD")).Text;

        }

        // assgin price = 0 if price fild in grid = null or empty
        if (price != null && string.IsNullOrWhiteSpace(price.ToString()))
        {
            grid_ds.Tables[0].Rows[grid_ds.Tables[0].Rows.Count - 1][2] = 0;
            price = 0.ToString();
        }
        else
        {
            grid_ds.Tables[0].Rows[grid_ds.Tables[0].Rows.Count - 1][2] = price;
        }

        // assgin all product's values as per qty value or 0 if qty = 0
        if (qty != null && (string.IsNullOrEmpty(qty) || qty == "0"))
        {
            
            for (int i = 3; i <= 9; i++)
            grid_ds.Tables[0].Rows[rindex][i] = 0;
            
        }

        else
        {
            //calculate total
            grid_ds.Tables[0].Rows[rindex][3] = int.Parse(((TextBox)(GridView1.Rows[rindex].Cells[3].FindControl("TXT_QTY_GRD"))).Text);
            total_price_grd = int.Parse(price) * int.Parse(qty);
            grid_ds.Tables[0].Rows[rindex][4] = total_price_grd;
            
            for (int i = 5; i <= 9; i++)
                grid_ds.Tables[0].Rows[rindex][i] = 0;

            //calculate tax & total after tax
            if (!(tax != null && (string.IsNullOrEmpty(tax) || tax == "0")))
            {
                grid_ds.Tables[0].Rows[rindex][5] = tax;
                total_after_tax = (total_price_grd * int.Parse(tax) / 100) + total_price_grd;
                grid_ds.Tables[0].Rows[rindex][6] = total_after_tax;
            }
            else
            {
                for (int i = 5; i <= 6; i++)
                    grid_ds.Tables[0].Rows[rindex][i] = 0;
            }

            //calculate discount & total after discount
            if (!(disc != null && (string.IsNullOrEmpty(disc) || disc == "0")))
            {
                grid_ds.Tables[0].Rows[rindex][7] = disc;
                total_after_discc = total_price_grd - (total_price_grd * int.Parse(disc) / 100);
                grid_ds.Tables[0].Rows[rindex][8] = total_after_discc;
            }
            else
            {
                for (int i = 7; i <= 8; i++)
                    grid_ds.Tables[0].Rows[rindex][i] = 0;
            }

            //calculate profit
            if (!(tax != null && (string.IsNullOrEmpty(tax) || tax == "0")))
            {
                profit = total_price_grd * int.Parse(disc) / 100;
                grid_ds.Tables[0].Rows[rindex][9] = profit;
            }
            

        }

        // restore product id and name to dataset to bind into grid
        for (int i = 0; i < GridView1.Rows.Count - 1; i++)
        {
            grid_ds.Tables[0].Rows[i][0] = prod_name[i];
            grid_ds.Tables[0].Rows[i][1] = prod_id[i];
        }
        ViewState["grid_ds"] = grid_ds;
        GridView1.DataSource = grid_ds;
        GridView1.DataBind();

        //fill_drp_prod_name_grd;
        DropDownList dr_prod_name_grid = GridView1.Rows[GridView1.Rows.Count - 1].FindControl("DRP_NAME_GRD") as DropDownList;
        dr_prod_name_grid.DataSource = drp_prod_name_grd_ds.Tables[0];
        dr_prod_name_grid.DataTextField = drp_prod_name_grd_ds.Tables[0].Columns["name"].ToString();
        dr_prod_name_grid.DataValueField = drp_prod_name_grd_ds.Tables[0].Columns["id"].ToString();
        dr_prod_name_grid.DataBind();
        dr_prod_name_grid.Items.Insert(0, "اختيار المنتج");

        //store index of selected product in dropdown list
        dr_prod_name_grid.SelectedIndex = drp_prod_name_index_grd;

        //hide dropdown list and show label
        for (int i = 0; i < GridView1.Rows.Count - 1; i++)
        {
            ((DropDownList)GridView1.Rows[i].FindControl("DRP_NAME_GRD")).Visible = false;
            ((Label)GridView1.Rows[i].FindControl("LBL_PROD_ID_GRD")).Visible = false;
            ((Label)GridView1.Rows[i].FindControl("LBL_PROD_NAME_GRD")).Visible = true;
        }

    }

    protected void TXT_PRICE_GRD_TextChanged(object sender, EventArgs e)
    {
        calculate_grid();
    }
    
    protected void TXT_DISC_GRD_TextChanged(object sender, EventArgs e)
    {
        calculate_grid();
    }

    protected void TXT_ID_TextChanged(object sender, EventArgs e)
    {

    }

    protected void TXT_DATE_FILTER_TextChanged(object sender, EventArgs e)
    {
        
        

    }

   

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
               
    }

    protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
       
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (GridView1.Rows.Count == 1)
        {
            Response.Write("<script>alert('لا يوجد اصناف لامر الشراء');</script>");
            return;

        }

        string prod_id_grd = ((Label)GridView1.Rows[e.RowIndex].FindControl("LBL_PROD_ID_GRD")).Text;
        if (string.IsNullOrEmpty(prod_id_grd))
        {
            Response.Write("<script>alert('لا يمكن مسح يجب الحفظ اولا');</script>");
            return;
        }

        DataSet grid_ds = (DataSet)ViewState["grid_ds"];
        DataSet drp_prod_name_grd_ds = (DataSet)ViewState["drp_prod_name_grd_ds"];
        //int[] prod_id = new int[GridView1.Rows.Count - 1];
        //string[] prod_name = new string[GridView1.Rows.Count - 1];


        //for (int i = 0; i < GridView1.Rows.Count - 1; i++)
        //{
        //    prod_id[i] = int.Parse(((Label)GridView1.Rows[i].FindControl("LBL_PROD_ID_GRD")).Text);
        //    prod_name[i] = ((Label)GridView1.Rows[i].FindControl("LBL_PROD_NAME_GRD")).Text;

        //}

        string purchases_details_get = db.select_value("select * from purchases_details where purchase_id = " + TXT_ID.Text + "", "id");
        int purchases_details_id = int.Parse(purchases_details_get);

        if (purchases_details_id != -1)
        {
            string delete_row = "delete from purchases_details where product_id = " + ((Label)(GridView1.Rows[e.RowIndex].Cells[2].FindControl("LBL_PROD_ID_GRD"))).Text + " and purchase_id = " + TXT_ID.Text + "";
            db.delete(delete_row);
            //fill_grid();
        }

        grid_ds.Tables[0].Rows[e.RowIndex].Delete();
        GridView1.DataSource = grid_ds;
        //for (int i = 0; i < GridView1.Rows.Count - 1; i++)
        //{
        //    grid_ds.Tables[0].Rows[i][0] = prod_name[i];
        //    grid_ds.Tables[0].Rows[i][1] = prod_id[i];
        //}
        GridView1.DataBind();
        ViewState.Add("grid_ds", grid_ds);

        DropDownList grid_dr_n = GridView1.Rows[GridView1.Rows.Count - 1].FindControl("DRP_NAME_GRD") as DropDownList;
        grid_dr_n.DataSource = drp_prod_name_grd_ds.Tables[0];
        grid_dr_n.DataBind();
        grid_dr_n.DataTextField = drp_prod_name_grd_ds.Tables[0].Columns["name"].ToString(); // text field name of table dispalyed in dropdown       
        grid_dr_n.DataValueField = drp_prod_name_grd_ds.Tables[0].Columns["id"].ToString();
        grid_dr_n.Items.Insert(0, "اختيار المنتج");

        for (int i = 0; i < GridView1.Rows.Count - 1; i++)
        {
            ((DropDownList)GridView1.Rows[i].FindControl("DRP_NAME_GRD")).Visible = false;
            ((Label)GridView1.Rows[i].FindControl("LBL_PROD_ID_GRD")).Visible = false;
            ((Label)GridView1.Rows[i].FindControl("LBL_PROD_NAME_GRD")).Visible = true;
        }

        delete_flag= true;
        ViewState["delete_flag"] = delete_flag;




    }

    protected void BTN_REFRESH_Click(object sender, EventArgs e)
    {

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





    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        
      

        if (IsPostBack == false)
        {
            Response.Write("<script>alert('should Enter Product Name or Choose Category');</script>");
            //Response.Write("should choose type");
            return;
        }
        //GridView1.EditIndex = -1;
        grid_ds = (DataSet)ViewState["grid_ds"];
        DataRow dr_new = grid_ds.Tables[0].NewRow();
        grid_ds.Tables[0].Rows.Add(dr_new);
        GridView1.DataSource = grid_ds;
        GridView1.DataBind();
        ViewState.Add("grid_ds", grid_ds);


    }
  

    protected void GridView1_DataBound(object sender, EventArgs e)
    {

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        
        
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "New")
        {
            // confirm calculation before save

            // confirm calculation


            //validate at least price and qty inserted

            //validate price & qty


            //get user data from cookies
            HttpCookie users = Request.Cookies["users"];
            int user_id = 0;
            if (users != null) {
                user_id = int.Parse(Request.Cookies["users"]["user_id"].ToString());
            }
            else
            {
                Response.Write("<script>alert('يوجد خطأ فى اسم المستخدم يرجى الخروج و اعادة الدخول مرة اخرى');</script>");
                return;
            }

            int rowindex = Convert.ToInt32(e.CommandArgument);

            if (((DropDownList)GridView1.Rows[rowindex].FindControl("DRP_NAME_GRD")).SelectedIndex == 0)
            {
                Response.Write("<script>alert('يجب اختيار المنتج اولا');</script>");
                return;
            }

            if (DRP_SUPPLIER.SelectedIndex == 0)
            {
                Response.Write("<script>alert('يجب اختيار المورد ');</script>");
                return;
            }

            //validate duplication in purchases_details & purchases
            string select_id_purchase = "select id from purchases where id = " + TXT_ID.Text + "";
            string id_purchase = db.select_value(select_id_purchase, "id");
            if (GridView1.Rows.Count == 1)
            {
                bool delete_flag = (bool)(ViewState["delete_flag"]);
                if (delete_flag == false)
                {
                    
                    if (!string.IsNullOrEmpty(id_purchase))
                    {
                        Response.Write("<script>alert('رقم امر الشراء موجود مسبقا يجب تغييرة');</script>");
                        return;
                    }
                }

                
            }
            string prod_id_grd = ((Label)GridView1.Rows[rowindex].FindControl("LBL_PROD_ID_GRD")).Text;
            if(string.IsNullOrEmpty(prod_id_grd))
            {
                prod_id_grd = ((DropDownList)GridView1.Rows[rowindex].FindControl("DRP_NAME_GRD")).SelectedValue;
            }
            string select_id_purchase_details = "select id from purchases_details where purchase_id = " + TXT_ID.Text + " and product_id = " + int.Parse(prod_id_grd) + "";
            string id_purchase_details = db.select_value(select_id_purchase_details, "id");
            if (!string.IsNullOrEmpty(id_purchase_details))
            {
                Response.Write("<script>alert('هذا المنتج موجود مسبقا');</script>");
                return;
            }

            if (!string.IsNullOrEmpty(id_purchase))
            {
                string select_supplier_id = "select suppliers.id from suppliers inner join purchases on purchases.supplier_id = "+ DRP_SUPPLIER.SelectedValue + " ";
                string supplier_id = db.select_value(select_supplier_id, "id");
                if (supplier_id != DRP_SUPPLIER.SelectedValue)
                {
                    Response.Write("<script>alert('لا يمكن تغيير المرود');</script>");
                    return;
                }
                    
            }


            //define global variable

            DropDownList grid_dr = GridView1.Rows[rowindex].FindControl("DRP_NAME_GRD") as DropDownList;
            string LBL_ID_GRD = grid_dr.SelectedValue; 
            string LBL_NAME_GRD = grid_dr.SelectedItem.ToString();
            DropDownList[] grid_prod_name_dr = new DropDownList[GridView1.Rows.Count - 1];
            DataSet drp_prod_name_grd_ds = (DataSet)ViewState["drp_prod_name_grd_ds"];
            int [] prod_id = new int[GridView1.Rows.Count ];
            string [] prod_name = new string[GridView1.Rows.Count];
            grid_ds = (DataSet)ViewState["grid_ds"];   

        
            Label LBL_ID = GridView1.Rows[GridView1.Rows.Count - 1].FindControl("LBL_PROD_ID_GRD") as Label;
            Label LBL_NAME = GridView1.Rows[GridView1.Rows.Count - 1].FindControl("LBL_PROD_NAME_GRD") as Label;
            LBL_ID.Text = LBL_ID_GRD;
            LBL_NAME.Text = LBL_NAME_GRD;

            //store all product id and name in arrays
            for (int i = 0; i < GridView1.Rows.Count ; i++)
            {
                prod_id[i] = int.Parse(((Label)GridView1.Rows[i].FindControl("LBL_PROD_ID_GRD")).Text);
                prod_name[i] = ((Label)GridView1.Rows[i].FindControl("LBL_PROD_NAME_GRD")).Text;

            }

            DataRow dr_new = grid_ds.Tables[0].NewRow();
            grid_ds.Tables[0].Rows.Add(dr_new);
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                grid_ds.Tables[0].Rows[i][0] = prod_name[i];
                grid_ds.Tables[0].Rows[i][1] = prod_id[i];
            }
            GridView1.DataSource = grid_ds;
            GridView1.DataBind();
            ViewState["grid_ds"] = grid_ds;

            DropDownList grid_dr_n = GridView1.Rows[GridView1.Rows.Count - 1].FindControl("DRP_NAME_GRD") as DropDownList;
            grid_dr_n.DataSource = drp_prod_name_grd_ds.Tables[0];
            grid_dr_n.DataBind();
            grid_dr_n.DataTextField = drp_prod_name_grd_ds.Tables[0].Columns["name"].ToString(); // text field name of table dispalyed in dropdown       
            grid_dr_n.DataValueField = drp_prod_name_grd_ds.Tables[0].Columns["id"].ToString();
            grid_dr_n.Items.Insert(0, "اختيار المنتج");


            for (int i = 0; i < GridView1.Rows.Count - 1; i++)
            {
                ((DropDownList)GridView1.Rows[i].FindControl("DRP_NAME_GRD")).Visible = false;
                ((Label)GridView1.Rows[i].FindControl("LBL_PROD_ID_GRD")).Visible = false;
                ((Label)GridView1.Rows[i].FindControl("LBL_PROD_NAME_GRD")).Visible = true;
            }

            //database section
            string select_id = "select id from purchases where id = " + TXT_ID.Text + "";
            string id = db.select_value(select_id, "id");
            if (string.IsNullOrEmpty(id))
            {
                string insert_purchase = "INSERT INTO purchases (id, date, time, supplier_id, user_id, status) " +
                " VALUES ( " +
                "" + TXT_ID.Text + "," +
                "'" + TXT_Date.Text + "'," +
                "'" + DateTime.Now.ToString("HH:mm:ss") + "'," +
                "" + DRP_SUPPLIER.SelectedValue + " ," +
                "" + user_id + " ," +
                "" + 0
                + ")";
                db.insert(insert_purchase);
            }

            string insert_prod_details = "INSERT INTO purchases_details (purchase_id, product_id, price, quantity, total_price, tax, total_after_tax, discount, total_after_discount, profit)" +
                " VALUES (" +
                "" + TXT_ID.Text + "," +
                "" + LBL_ID_GRD + "," +
                "" + grid_ds.Tables[0].Rows[rowindex][2] + " ," +
                "" + grid_ds.Tables[0].Rows[rowindex][3] + "," +
                "" + grid_ds.Tables[0].Rows[rowindex][4] + "," +
                "" + grid_ds.Tables[0].Rows[rowindex][5] + "," +
                "" + grid_ds.Tables[0].Rows[rowindex][6] + "," +
                "" + grid_ds.Tables[0].Rows[rowindex][7] + "," +
                "" + grid_ds.Tables[0].Rows[rowindex][8] + "," +
                "" + grid_ds.Tables[0].Rows[rowindex][9] + ")";
            db.insert(insert_prod_details);

                //update price list and prices_trans
            string select_price = "select purchase_price from price_list where product_id = " + LBL_ID_GRD + "";            
            int purchase_price = int.Parse(db.select_value(select_price, "purchase_price"));

            if (purchase_price != (decimal)grid_ds.Tables[0].Rows[rowindex][2])
            {
                string update_price = "update price_list set purchase_price = " + grid_ds.Tables[0].Rows[rowindex][2] + " where product_id = " + LBL_ID_GRD + "";
                db.update(update_price);

                string insert_price = "INSERT INTO prices_trans (product_id, purchase_price) " +
                " VALUES (" +
                "" + LBL_ID_GRD + "," +
                "" + grid_ds.Tables[0].Rows[rowindex][2] + ")";
                db.insert(insert_price);
            }           

            int drp_prod_name_index_grd = (int)(ViewState["drp_prod_name_index_grd"]);
            drp_prod_name_index_grd = 0;
            ViewState["drp_prod_name_index_grd"] = drp_prod_name_index_grd; 
        }
    }

    protected void DRP_NAME_GRD_SelectedIndexChanged(object sender, EventArgs e)
    {

        string query = string.Format("select purchase_price from price_list where product_id = '{0}'", ((DropDownList)(GridView1.Rows[GridView1.Rows.Count - 1].FindControl("DRP_NAME_GRD"))).SelectedValue);
        string purchase_price = db.select_value(query, "purchase_price");
        if (string.IsNullOrEmpty(purchase_price))
        {
            ((TextBox)(GridView1.Rows[GridView1.Rows.Count - 1].FindControl("TXT_PRICE_GRD"))).Text = 0.ToString();
        }
        else
        {
            
            ((TextBox)(GridView1.Rows[GridView1.Rows.Count - 1].FindControl("TXT_PRICE_GRD"))).Text = purchase_price.ToString();
            
        }
        int drp_index = ((DropDownList)(GridView1.Rows[GridView1.Rows.Count - 1].FindControl("DRP_NAME_GRD"))).SelectedIndex;


        ViewState.Add("drp_prod_name_index_grd", drp_index);

        calculate_grid();



    }





    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        

    }

    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    protected void BTN_ADD_ORDER_Click(object sender, EventArgs e)
    {
        start_load();   
    }



    protected void TXT_TAX_GRD_TextChanged(object sender, EventArgs e)
    {
        calculate_grid();
    }
}