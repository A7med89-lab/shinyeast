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
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Ajax.Utilities;

public partial class stock_purchases : System.Web.UI.Page
{
    public void fill_grid()
    {
        string select = "select purchases.id,purchases.[date],suppliers.name as supplier_name,suppliers.id as supplier_id, purchases.status, sum(purchases_details.quantity) as sum_qty, users.name as user_name, users.id as user_id from purchases inner join purchases_details on purchases.id = purchases_details.purchase_id inner join suppliers on purchases.supplier_id = suppliers.id left join users on purchases.user_id = users.id Group by purchases.id, purchases.[date], suppliers.name,suppliers.id , purchases.status, users.name, users.id,  purchases.action having purchases.action = 0";
        db.select(select, GRD_STOCK_IN);
        ViewState.Add("grid_ds", db.ds1);
        int row_count = GRD_STOCK_IN.Rows.Count;
        if (row_count == 0)
        {
            LBL_PURCHASE_MASSEGE.Visible = true;
        }
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
            int purchase_id = int.Parse(db.select(select, GRD_STOCK_DETAILS, "id"));
            ViewState.Add("purchase_id", purchase_id); 
            if (GRD_STOCK_DETAILS.Visible == false)
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

            string update_purchase = "update purchases set status = '" + 1 + "', action = '" + 1 + "' where id = " + id + "";
            db.update(update_purchase);

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
            start_load();
        }

        if (e.CommandName == "Delete")
        {
            string update_purchase = "update purchases set status = '" + 1 + "', action = '" + 1 + "' where id = " + id + "";
            db.update(update_purchase);
            start_load();

        }
                         
    }

    protected void GRD_STOCK_IN_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void GRD_STOCK_IN_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }

    protected void GRD_STOCK_DETAILS_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "New")
        {
            // Create a new DataTable with the specified columns
            // This is just an example, you can modify it as per your requirements
        }
        else if (e.CommandName == "Select")
        {
            //declaration variables 
            int index = Convert.ToInt32(e.CommandArgument);
            Dictionary<string, Type> col = new Dictionary<string, Type>();
            int purchase_id = (int)ViewState["purchase_id"];
            DataSet grid_cost_ds = new DataSet();
            DataTable prod_cost_data_table = new DataTable("prod_cost");

            // fill grd_cost
            //DataTable prod_cost_data_table = db.add_new_datatable("prod_cost");

            

            string get_all_cost = "select product_id as prod_id, products.name as prod_name ,product_cost_id as cost_id, product_cost_name as cost_name, product_cost_amount as amount, [desc], purchase_product_cost.id  from purchase_product_cost inner join products on purchase_product_cost.product_id = products.id where purchase_id = " + purchase_id + " and product_id = " + int.Parse(((Label)(GRD_STOCK_DETAILS.Rows[index].FindControl("LBL_PROD_ID_GRD"))).Text) + "";

            prod_cost_data_table = db.select_values(get_all_cost);
            grid_cost_ds = db.add_datatable_to_dataset(grid_cost_ds, prod_cost_data_table, "prod_cost");           
            prod_cost_data_table = db.add_new_datarow(prod_cost_data_table); 
            
            if (prod_cost_data_table.Rows.Count == 1)
            {
                prod_cost_data_table.Rows[0]["prod_id"] = int.Parse(((Label)(GRD_STOCK_DETAILS.Rows[index].FindControl("LBL_PROD_ID_GRD"))).Text);
                prod_cost_data_table.Rows[0]["prod_name"] = ((Label)(GRD_STOCK_DETAILS.Rows[index].FindControl("LBL_PROD_NAME_GRD"))).Text;
            }
            else
            {
                int count = prod_cost_data_table.Rows.Count;

                foreach (DataRow row in prod_cost_data_table.Rows)
                {
                    if (count == 0)
                    {
                        break;
                    }
                    if (count < prod_cost_data_table.Rows.Count)
                    {
                        row["prod_id"] = DBNull.Value;
                        row["prod_name"] = DBNull.Value;
                       


                    }
                    count--;


                }
            }
            
            GRD_PROD_COST.DataSource = prod_cost_data_table;
            GRD_PROD_COST.DataBind();
            ViewState.Add("grid_cost_ds", grid_cost_ds);

            for (int i = 0; i < GRD_PROD_COST.Rows.Count - 1; i++)
            {
                ((DropDownList)GRD_PROD_COST.Rows[i].FindControl("DRP_COST_NAME_GRD")).Visible = false;
                ((Label)GRD_PROD_COST.Rows[i].FindControl("LBL_COST_ID_GRD")).Visible = false;
                ((Label)GRD_PROD_COST.Rows[i].FindControl("LBL_COST_NAME_GRD")).Visible = true;
            }

            //fill drop in data gridview
            string qurey = "select id, name from product_costs";
            DropDownList drp_prod_cost_grd = GRD_PROD_COST.Rows[GRD_PROD_COST.Rows.Count -1].FindControl("DRP_COST_NAME_GRD") as DropDownList;
            DataSet drp_prod_cost = db.fill_drop_grid(qurey, drp_prod_cost_grd, "اختيار التكلفة");
            ViewState.Add("drp_prod_cost_grd_ds", drp_prod_cost);

        }
        else if (e.CommandName == "Delete")
        {
            // Handle the delete command
        }
        
        


        
        string type = e.CommandName; //select
    }

    protected void DRP_COST_NAME_GRD_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        string query = string.Format("select amount from product_costs where id = '{0}'", ((DropDownList)(GRD_PROD_COST.Rows[GRD_PROD_COST.Rows.Count - 1].FindControl("DRP_COST_NAME_GRD"))).SelectedValue);
        string amount = db.select_value(query, "amount");
        if (string.IsNullOrEmpty(amount))
        {
            ((TextBox)(GRD_PROD_COST.Rows[GRD_PROD_COST.Rows.Count - 1].FindControl("TXT_COST_AMOUNT_GRD"))).Text = 0.ToString();
        }
        else
        {

            ((TextBox)(GRD_PROD_COST.Rows[GRD_PROD_COST.Rows.Count - 1].FindControl("TXT_COST_AMOUNT_GRD"))).Text = amount.ToString();

        }
        int drp_index = ((DropDownList)(GRD_PROD_COST.Rows[GRD_PROD_COST.Rows.Count - 1].FindControl("DRP_COST_NAME_GRD"))).SelectedIndex;


        ViewState.Add("drp_cost_name_index_grd", drp_index);
    }

    protected void GRD_PROD_COST_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            // confirm calculation before save

            // confirm calculation


            //validate at least amount

            //validate amount


            //get user data from cookies
            HttpCookie users = Request.Cookies["users"];
            int user_id = 0;
            if (users != null)
            {
                user_id = int.Parse(Request.Cookies["users"]["user_id"].ToString());
            }
            else
            {
                Response.Write("<script>alert('يوجد خطأ فى اسم المستخدم يرجى الخروج و اعادة الدخول مرة اخرى');</script>");
                return;
            }

            int rowindex = Convert.ToInt32(e.CommandArgument);

            if (((DropDownList)GRD_PROD_COST.Rows[rowindex].FindControl("DRP_COST_NAME_GRD")).SelectedIndex == 0)
            {
                Response.Write("<script>alert('يجب اختيار نوع التكلفة اولا');</script>");
                return;
            }

            if (((TextBox)GRD_PROD_COST.Rows[rowindex].FindControl("TXT_COST_AMOUNT_GRD")).Text=="" || int.Parse(((TextBox)GRD_PROD_COST.Rows[rowindex].FindControl("TXT_COST_AMOUNT_GRD")).Text) == 0)
            {
                Response.Write("<script>alert('يجب اختيار تحديد قيمة التكلفة اولا');</script>");
                return;
            }

            //define global variable

            DropDownList grid_dr = GRD_PROD_COST.Rows[rowindex].FindControl("DRP_COST_NAME_GRD") as DropDownList;
            if (grid_dr.Items.Count <= 0)
            {
                Response.Write("<script>alert('هذة التكلفة تم اضافتها');</script>");
                return;
            }
            string LBL_ID_GRD = grid_dr.SelectedValue;
            string LBL_NAME_GRD = grid_dr.SelectedItem.ToString();                        
            int[] cost_id = new int[GRD_PROD_COST.Rows.Count];
            string[] cost_name = new string[GRD_PROD_COST.Rows.Count];
            DataSet drp_prod_cost_grd_ds = (DataSet)ViewState["drp_prod_cost_grd_ds"];
            DataSet grid_cost_ds = (DataSet)ViewState["grid_cost_ds"];
            int purchase_id = (int)ViewState["purchase_id"];
            int prod_id = int.Parse(((Label)GRD_PROD_COST.Rows[0].FindControl("LBL_COST_PROD_ID_GRD")).Text);
            string prod_name = ((Label)GRD_PROD_COST.Rows[0].FindControl("LBL_COST_PROD_NAME_GRD")).Text;
            Label LBL_ID = GRD_PROD_COST.Rows[rowindex].FindControl("LBL_COST_ID_GRD") as Label;
            Label LBL_NAME = GRD_PROD_COST.Rows[rowindex].FindControl("LBL_COST_NAME_GRD") as Label;
            LBL_ID.Text = LBL_ID_GRD;
            LBL_NAME.Text = LBL_NAME_GRD;

            //store all cost id and name in arrays
            for (int i = 0; i < GRD_PROD_COST.Rows.Count; i++)
            {
                cost_id[i] = int.Parse(((Label)GRD_PROD_COST.Rows[i].FindControl("LBL_COST_ID_GRD")).Text);
                cost_name[i] = ((Label)GRD_PROD_COST.Rows[i].FindControl("LBL_COST_NAME_GRD")).Text;

            }

            //DataRow dr_new = grid_ds.Tables[0].NewRow();
            //grid_ds.Tables[0].Rows.Add(dr_new);
            //dataset section
            db.add_new_datarow(grid_cost_ds.Tables[0]);
            for (int i = 0; i < GRD_PROD_COST.Rows.Count; i++)
            {
                grid_cost_ds.Tables[0].Rows[i][3] = cost_name[i];
                grid_cost_ds.Tables[0].Rows[i][2] = cost_id[i];
            }
            grid_cost_ds.Tables[0].Rows[rowindex][4] = int.Parse(((TextBox)GRD_PROD_COST.Rows[rowindex].FindControl("TXT_COST_AMOUNT_GRD")).Text);
            grid_cost_ds.Tables[0].Rows[rowindex][5] = ((TextBox)GRD_PROD_COST.Rows[rowindex].FindControl("TXT_COST_DESC_GRD")).Text;
            string select_max_id = "select max(id)+1 as id from purchase_product_cost";
            string max_id = db.select_value(select_max_id, "id");
            grid_cost_ds.Tables[0].Rows[rowindex][6] = max_id;
            GRD_PROD_COST.DataSource = grid_cost_ds;
            GRD_PROD_COST.DataBind();
            ViewState["grid_cost_ds"] = grid_cost_ds;

            //fill drp cost_name & cost_id
            DropDownList drp_cost_name_grd_n = GRD_PROD_COST.Rows[GRD_PROD_COST.Rows.Count - 1].FindControl("DRP_COST_NAME_GRD") as DropDownList;
            string select_cost_name = "select * from product_costs";
            db.fill_drop_grid(select_cost_name, drp_cost_name_grd_n, "اختيار التكلفة");
            


            for (int i = 0; i < GRD_PROD_COST.Rows.Count - 1; i++)
            {
                ((DropDownList)GRD_PROD_COST.Rows[i].FindControl("DRP_COST_NAME_GRD")).Visible = false;
                ((Label)GRD_PROD_COST.Rows[i].FindControl("LBL_COST_ID_GRD")).Visible = false;
                ((Label)GRD_PROD_COST.Rows[i].FindControl("LBL_COST_NAME_GRD")).Visible = true;
            }


            //database section
            string select_id = "select id from product_costs where id = " + LBL_ID.Text + "";
            string id = db.select_value(select_id, "id");
            if (!(string.IsNullOrEmpty(id)))
            {
                string update_product_costs = "UPDATE product_costs SET amount = "+ grid_cost_ds.Tables[0].Rows[rowindex][4] + " where id = "+ grid_cost_ds.Tables[0].Rows[rowindex][2] + "";
                db.update(update_product_costs);
            }

            string purchase_product_cost = "INSERT INTO purchase_product_cost (id, purchase_id, product_id, product_cost_id, product_cost_name, [desc], product_cost_amount)" +
                " VALUES (" +
                "" + max_id + "," +
                "" + purchase_id + "," +                
                "" + prod_id + " ," +
                "" + grid_cost_ds.Tables[0].Rows[rowindex][2] + "," +
                "N'" + grid_cost_ds.Tables[0].Rows[rowindex][3] + "'," +
                "N'" + grid_cost_ds.Tables[0].Rows[rowindex][5] + "'," +
                "" + grid_cost_ds.Tables[0].Rows[rowindex][4] + ")";
            db.insert(purchase_product_cost);

            //update price list and prices_trans
            //string select_price = "select purchase_price from price_list where product_id = " + LBL_ID_GRD + "";
            //int purchase_price = int.Parse(db.select_value(select_price, "purchase_price"));

            //if (purchase_price != (decimal)grid_ds.Tables[0].Rows[rowindex][2])
            //{
            //    string update_price = "update price_list set purchase_price = " + grid_ds.Tables[0].Rows[rowindex][2] + " where product_id = " + LBL_ID_GRD + "";
            //    db.update(update_price);

            //    string insert_price = "INSERT INTO prices_trans (product_id, purchase_price) " +
            //    " VALUES (" +
            //    "" + LBL_ID_GRD + "," +
            //    "" + grid_ds.Tables[0].Rows[rowindex][2] + ")";
            //    db.insert(insert_price);
            //}

            int drp_prod_name_index_grd = (int)(ViewState["drp_cost_name_index_grd"]);
            drp_prod_name_index_grd = 0;
            ViewState["drp_cost_name_index_grd"] = drp_prod_name_index_grd;
        }
    }

    protected void GRD_PROD_COST_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (GRD_PROD_COST.Rows.Count == 1)
        {
            Response.Write("<script>alert('لا يوجد تكاليف يمكن مسحها');</script>");
            return;

        }

        string prod_id_grd = ((Label)GRD_PROD_COST.Rows[e.RowIndex].FindControl("LBL_COST_ID_GRD")).Text;
        if (string.IsNullOrEmpty(prod_id_grd))
        {
            Response.Write("<script>alert('لا يمكن مسح يجب الحفظ اولا');</script>");
            return;
        }

        //define global variables
        DataSet grid_cost_ds = (DataSet)ViewState["grid_cost_ds"];
        int drp_prod_name_index_grd = (int)(ViewState["drp_cost_name_index_grd"]);
        int purchase_id = (int)ViewState["purchase_id"];
        int prod_id = int.Parse(((Label)GRD_PROD_COST.Rows[0].FindControl("LBL_COST_PROD_ID_GRD")).Text);
        string prod_name = ((Label)GRD_PROD_COST.Rows[0].FindControl("LBL_COST_PROD_NAME_GRD")).Text;



        DataTable cost_details_td = new DataTable();
        string cost_query = "select * from purchase_product_cost where purchase_id = " + purchase_id + " and product_id = " + ((Label)GRD_PROD_COST.Rows[0].FindControl("LBL_COST_PROD_ID_GRD")).Text + " and product_cost_id = " + ((Label)GRD_PROD_COST.Rows[e.RowIndex].FindControl("LBL_COST_ID_GRD")).Text + "";
        cost_details_td = db.select_values(cost_query);
        //int text = int.Parse(cost_details_td.Rows[e.RowIndex]["product_id"].ToString());

        if (cost_details_td.Rows.Count > 0)
        {
            if (grid_cost_ds.Tables[0].Columns.Contains("id"))
            {
                string delete_row_dt = "id = " + ((Label)GRD_PROD_COST.Rows[e.RowIndex].FindControl("LBL_ID_GRD")).Text + "";
                db.delete_datatable(grid_cost_ds.Tables[0], where_conditions: delete_row_dt);
            }
            
            //string delete_row = "delete from purchase_product_cost where product_id = " + cost_details_td.Rows[e.RowIndex]["product_id"] + " and purchase_id = " + cost_details_td.Rows[e.RowIndex]["purchase_id"] + " and product_cost_id = " + cost_details_td.Rows[e.RowIndex]["product_cost_id"] + "";
            string delete_row = "delete from purchase_product_cost where id = " + ((Label)GRD_PROD_COST.Rows[e.RowIndex].FindControl("LBL_ID_GRD")).Text + "";
            db.delete(delete_row);

            //test later
            /*
            string delete_row = "product_id = " + cost_details_td.Rows[e.RowIndex]["product_id"] + " and purchase_id = " + cost_details_td.Rows[e.RowIndex]["purchase_id"] + " and product_cost_id = " + cost_details_td.Rows[e.RowIndex]["product_cost_id"] + "";

            db.update_dataset_to_database(database_table_name: "purchase_product_cost", where_conditions:delete_row);
            */            
        }

        if (string.IsNullOrWhiteSpace(grid_cost_ds.Tables[0].Rows[0]["prod_id"].ToString()))
        {
            grid_cost_ds.Tables[0].Rows[0]["prod_id"] = prod_id;
            grid_cost_ds.Tables[0].Rows[0]["prod_name"] = prod_name;           
        }

        GRD_PROD_COST.DataSource = grid_cost_ds;
        GRD_PROD_COST.DataBind();        
        ViewState["grid_cost_ds"] = grid_cost_ds;

        DropDownList drp_cost_name_grd_n = GRD_PROD_COST.Rows[GRD_PROD_COST.Rows.Count - 1].FindControl("DRP_COST_NAME_GRD") as DropDownList;
        string select_cost_name = "select * from product_costs";
        db.fill_drop_grid(select_cost_name, drp_cost_name_grd_n, "اختيار التكلفة");

        //if (string.IsNullOrWhiteSpace(((Label)GRD_PROD_COST.Rows[0].FindControl("LBL_COST_PROD_NAME_GRD")).Text))
        //{
        //    ((Label)GRD_PROD_COST.Rows[0].FindControl("LBL_COST_PROD_ID_GRD")).Text = prod_id.ToString();
        //    ((Label)GRD_PROD_COST.Rows[0].FindControl("LBL_COST_PROD_NAME_GRD")).Text = prod_name;
        //}
        

        for (int i = 0; i < GRD_PROD_COST.Rows.Count - 1; i++)
        {
            ((DropDownList)GRD_PROD_COST.Rows[i].FindControl("DRP_COST_NAME_GRD")).Visible = false;
            ((Label)GRD_PROD_COST.Rows[i].FindControl("LBL_COST_ID_GRD")).Visible = false;
            ((Label)GRD_PROD_COST.Rows[i].FindControl("LBL_COST_NAME_GRD")).Visible = true;
        }


    }
}