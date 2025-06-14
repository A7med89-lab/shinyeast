using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class products : System.Web.UI.Page
{
    public void fill_grid()
    {
        //string select_1 = "select products.id, products.name, products.fixed, products.dynamic, types.name as type, sizes.name as size, colors.name as color , products.made_in from products inner join types on products.id = types.product_id left join sizes on products.id = sizes.product_id left join colors on products.id = colors.product_id";
        //string select_2 = "select products.id, products.name, types.name as type, sizes.name as size, colors.name as color , products.fixed, products.dynamic, products.made_in from products inner join types on products.type_id = types.id left join sizes on products.size_id = sizes.id left join colors on products.color_id = colors.id";
        //string select_3 = "select products.id, products.name, types.name as type, sizes.name as size, colors.name as color , products.fixed, products.removable, products.made_in from products inner join types on products.type_id = types.id left join sizes on products.size_id = sizes.id And products.type_id = sizes.type_id left join colors on products.color_id = colors.id and products.size_id = colors.size_id and products.type_id = colors.type_id";
        //string select_3 = "select products.id as ID , products.name as الاسم ,categories.name as التصنيف,types.name as النوع, sizes.name as الحجم, colors.name as اللون , products.fixed as ثابت, products.removable as متحرك, products.made_in as [صنع فى] from products inner join categories on products.category_id = categories.id left join types on products.type_id = types.id left join sizes on products.size_id = sizes.id and products.type_id = sizes.type_id left join colors on products.color_id = colors.id and products.size_id = colors.size_id and products.type_id = colors.type_id";
        string select_3 = "select products.id as id , products.name as name ,categories.name as cat, types.name as prod_type, sizes.name as size, colors.name as color , products.fixed as fix, products.removable as remov, products.made_in as made from products inner join categories on products.category_id = categories.id left join types on products.type_id = types.id left join sizes on products.size_id = sizes.id and products.type_id = sizes.type_id left join colors on products.color_id = colors.id and products.size_id = colors.size_id and products.type_id = colors.type_id";
        db.select(select_3, GridView1);
        ViewState.Add("ds", db.ds1);
    }

    public void fill_grid_qty(int product_id)
    {
        
        string select = "select product_quantities.total_in, product_quantities.total_out, product_quantities.total_net_in, stock_prices.sales_stock_price from product_quantities left join stock_prices on product_quantities.product_id = stock_prices.product_id where product_quantities.product_id = "+ product_id + "";
        db.select(select, GridView2);
        ViewState.Add("ds_prod_qty", db.ds1);
    }

    public void txt_id()
    {
        string id = "select max(id)+1 as id from products";
        db.select_id(id, TXT_ID);
        if (TXT_ID.Text == "")
        {
            TXT_ID.Text = 1.ToString();
        }

    }

    public void txt_category_id()
    {
        string id = "select max(id)+1 as id from categories";
        db.select_id(id, TXT_CATEGORY_ID);
        if (TXT_CATEGORY_ID.Text == "")
        {
            TXT_CATEGORY_ID.Text = 1.ToString();
        }

    }

    public void txt_type_id()
    {
        string id = "select max(id)+1 as id from types";
        db.select_id(id, TXT_TYPE_ID);
        if (TXT_TYPE_ID.Text == "")
        {
            TXT_TYPE_ID.Text = 1.ToString();
        }

    }
    public void txt_size_id()
    {
        string id = "select max(id)+1 as id from sizes";
        db.select_id(id, TXT_SIZE_ID);
        if (TXT_SIZE_ID.Text == "")
        {
            TXT_SIZE_ID.Text = 1.ToString();
        }

    }
    public void txt_color_id()
    {
        string id = "select max(id)+1 as id from colors";
        db.select_id(id, TXT_COLOR_ID);
        if (TXT_COLOR_ID.Text == "")
        {
            TXT_COLOR_ID.Text = 1.ToString();
        }

    }

    public void fill_category()
    {
        string query = "select * from categories";
        db.select_combo(query, DRP_CATEGORY);
        DRP_CATEGORY.Items.Insert(0, "Selected Items");

        if (DRP_CATEGORY.SelectedIndex > 0)
        {
            query = "select * from sizes where category_id = " + DRP_CATEGORY.SelectedValue + " and type_id=" + DRP_TYPE.SelectedValue + " type_id is null ";
            db.select_combo(query, DRP_SIZE);

            if (DRP_SIZE.Items.Count != 0)  //  for insert "selected_item" text only if DRP_size has data
            {
                DRP_SIZE.Items.Clear();
                db.select_combo(query, DRP_SIZE);
                DRP_SIZE.Items.Insert(0, "Selected Items");
            }

        }

    }

    public void fill_category_size()
    {
        string query;
        if (DRP_CATEGORY.SelectedIndex > 0)
        {
            query = "select * from sizes where category_id = " + DRP_CATEGORY.SelectedValue + " and type_id is null ";
            db.select_combo(query, DRP_SIZE);

            if (DRP_SIZE.Items.Count != 0)  //  for insert "selected_item" text only if DRP_size has data
            {
                DRP_SIZE.Items.Clear();
                db.select_combo(query, DRP_SIZE);
                DRP_SIZE.Items.Insert(0, "Selected Items");
            }

        }

    }

    public void fill_category_color()
    {
        string query;
        if (DRP_CATEGORY.SelectedIndex > 0)
        {
            query = "select * from colors where category_id = " + DRP_CATEGORY.SelectedValue + " and type_id is null and size_id is null ";
            db.select_combo(query, DRP_COLOR);

            if (DRP_COLOR.Items.Count != 0)  //  for insert "selected_item" text only if DRP_size has data
            {
                DRP_COLOR.Items.Clear();
                db.select_combo(query, DRP_COLOR);
                DRP_COLOR.Items.Insert(0, "Selected Items");
            }

        }

    }

    public void fill_type_color()
    {
        string query;
        if (DRP_CATEGORY.SelectedIndex > 0)
        {
            query = "select * from colors where category_id = " + DRP_CATEGORY.SelectedValue + " and type_id = " + DRP_TYPE.SelectedValue+ " and size_id is null";
            db.select_combo(query, DRP_COLOR);

            if (DRP_COLOR.Items.Count != 0)  //  for insert "selected_item" text only if DRP_size has data
            {
                DRP_COLOR.Items.Clear();
                db.select_combo(query, DRP_COLOR);
                DRP_COLOR.Items.Insert(0, "Selected Items");
            }

        }

    }

    public void fill_size_color()
    {
        string query;
        if (DRP_CATEGORY.SelectedIndex > 0)
        {
            query = "select * from colors where category_id = " + DRP_CATEGORY.SelectedValue + " and size_id = " + DRP_SIZE.SelectedValue + " and type_id is null";
            db.select_combo(query, DRP_COLOR);

            if (DRP_COLOR.Items.Count != 0)  //  for insert "selected_item" text only if DRP_size has data
            {
                DRP_COLOR.Items.Clear();
                db.select_combo(query, DRP_COLOR);
                DRP_COLOR.Items.Insert(0, "Selected Items");
            }

        }

    }

    public void fill_type()
    {
        //string query = "select * from types";
        //db.select_combo(query, DRP_TYPE);
        //DRP_TYPE.Items.Insert(0, "Selected Items");

        if (DRP_CATEGORY.SelectedIndex > 0)
        {
            //get category_id
            //string get_type_id = "select * from categories where name='" + DRP_CATEGORY.SelectedItem + "'";
            //string type_id = db.select_value(get_type_id, "id");
            //int t_id = int.Parse(type_id);
            int t_id = int.Parse(DRP_CATEGORY.SelectedValue);
            string query_type = "select * from types where category_id=" + t_id + " ";
            db.select_combo(query_type, DRP_TYPE);
            if (DRP_TYPE.Items.Count != 0)  // for insert "selected_item" in DRP_type only if DRP_type has data
            {
                DRP_TYPE.Items.Clear();
                db.select_combo(query_type, DRP_TYPE);
                DRP_TYPE.Items.Insert(0, "Selected Items");
            }
        }
    }
    public void fill_size()
    {
        //DRP_SIZE.Items.Insert(0, "Selected Items");
        
        if (DRP_TYPE.SelectedIndex > 0 && DRP_CATEGORY.SelectedIndex > 0 )
        {
            //get type_id & category_id

            //string get_type_id = "select * from types where name='" + DRP_TYPE.SelectedItem + "'";
            //string type_id = db.select_value(get_type_id, "id");
            //int t_id = int.Parse(type_id);

            int t_id = int.Parse(DRP_TYPE.SelectedValue);
            int c_id = int.Parse(DRP_CATEGORY.SelectedValue);
            string query = "select * from sizes where category_id = "+ c_id +" and type_id=" + t_id + " ";
            db.select_combo(query, DRP_SIZE);

            if (DRP_SIZE.Items.Count != 0)  //  for insert "selected_item" text only if DRP_size has data
            {
                DRP_SIZE.Items.Clear();
                db.select_combo(query, DRP_SIZE);
                DRP_SIZE.Items.Insert(0, "Selected Items");
            }
        }

    }
    public void fill_color()
    {
        //DRP_COLOR.Items.Clear();
        if (DRP_SIZE.Items.Count > 0 && DRP_TYPE.Items.Count > 0)
        {
            //string get_type_id = "select * from types where name='" + DRP_TYPE.SelectedItem + "'";
            //string type_id = db.select_value(get_type_id, "id");
            //int t_id = int.Parse(type_id);

            //string get_size_id = "select * from sizes where name='" + DRP_SIZE.SelectedItem + "'";
            //string size_id = db.select_value(get_size_id, "id");
            //int s_id = int.Parse(size_id);

            string query = "select * from colors where type_id=" + DRP_TYPE.SelectedValue + " and size_id=" + DRP_SIZE.SelectedValue + " ";
            db.select_combo(query, DRP_COLOR);

            //if (DRP_TYPE.SelectedIndex != 0 && DRP_SIZE.SelectedIndex != 0)
            //{

            //}

            if (DRP_COLOR.Items.Count != 0)
            {
                DRP_COLOR.Items.Clear();
                db.select_combo(query, DRP_COLOR);
                DRP_COLOR.Items.Insert(0, "Selected Items");
            }
        }

    }

    public bool valid_category()
    {
        if (TXT_CATEGORY_Name.Text == "")
        {
            return false;
        }
        else
        {

            string check_name = db.select_value("select name from categories where name = '" + TXT_CATEGORY_Name.Text + "'", "name");
            if (check_name != "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
    }

    public bool valid_type()
    {

        if (TXT_TYPE_Name.Text == "")
        {
            return false;
        }
        else
        {
            if(DRP_CATEGORY.SelectedIndex <=0)
            {
                return false;
            }
            else
            {
                string check_name = db.select_value("select name from types where name = '" + TXT_TYPE_Name.Text + "' and category_id = " + DRP_CATEGORY.SelectedValue + "", "name");
                if (check_name != "")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            
        }

    }

    public bool valid_size()
    {
        string check_name;
        if (TXT_SIZE_NAME.Text == "" || DRP_CATEGORY.SelectedIndex <= 0)
        {
            return false;
        }
        else
        {
            if (DRP_TYPE.SelectedIndex > 0)
            {
                check_name = db.select_value("select name from sizes where name = '" + TXT_NAME.Text + "' and category_id =" + DRP_CATEGORY.SelectedValue + " and [type_id]= " + DRP_TYPE.SelectedValue + "", "name");

                if (check_name != "")
                {
                    return false;
                }
                else
                {
                    return true;

                }
            }
            else
            {
                check_name = db.select_value("select name from sizes where name = '" + TXT_SIZE_NAME.Text + "' and category_id =" + DRP_CATEGORY.SelectedValue + " and type_id is null", "name");
                if (check_name != "")
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }

        }
    }
    
    public bool valid_color()
    {
        string check_name;
        if (TXT_COLOR_NAME.Text == "" || DRP_CATEGORY.SelectedIndex <= 0)
        {
            return false;
        }
        else
        {
            if (DRP_TYPE.SelectedIndex > 0)
            {
                check_name = db.select_value("select name from colors where name = '" + TXT_NAME.Text + "' and category_id =" + DRP_CATEGORY.SelectedValue + " and [type_id]= " + DRP_TYPE.SelectedValue + "", "name");
                if (check_name != "")
                {
                    if (DRP_SIZE.SelectedIndex > 0)
                    {
                        check_name = db.select_value("select name from colors where name = '" + TXT_NAME.Text + "' and category_id =" + DRP_CATEGORY.SelectedValue + " and [type_id]= " + DRP_TYPE.SelectedValue + " and size_id = " + DRP_SIZE.SelectedValue + "", "name");
                        if (check_name!="")
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;

                }
            }
            else
            {
                check_name = db.select_value("select name from colors where name = '" + TXT_COLOR_NAME.Text + "' and size_id =" + DRP_SIZE.SelectedValue + " and category_id =" + DRP_CATEGORY.SelectedValue + " and type_id is null", "name");
                if (check_name != "")
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }

        }


    }

    bool fixd()
    {
        if (CHK_FXD.Checked==true)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    bool removable()
    {
        if (CHK_DYNMIC.Checked == true)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
    public void clear_txt(params TextBox[] txts)
    {
        for (int i = 0; i < txts.Length; i++)
        {

            txts[i].Text = string.Empty;
        }
    }
    

    public void clear_drp(params DropDownList[] drps)
    {
        for (int i = 0; i < drps.Length; i++)
        {
            if (drps[i].Items.Count > 0)
                drps[i].Items.Clear();
        }
    }

    public void hide_product_cost_grid(bool hide = true)
    {
        if (hide == false)
        {
            GridView2.Visible = true;
            LBL_PROD_NAME.Visible = true;
        }
        else
        {
            GridView2.Visible = false;
            LBL_PROD_NAME.Visible = false;
        }

    }

    database db = new database();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            txt_id();
            fill_category();
            fill_type();
            fill_size();
            fill_color();
            fill_grid();
            hide_product_cost_grid(true);

        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //string get_type_id = "select * from types where name='" + DRP_TYPE.SelectedItem + "'";
        //string type_id = db.select_value(get_type_id, "id");
        if (DRP_CATEGORY.Text == "Selected Items" || DRP_CATEGORY.Text == "" || TXT_NAME.Text == "")
        {
            
            Response.Write("<script>alert('should Enter Product Name or Choose Category');</script>");
            //Response.Write("should choose type");
            return;
        }
        //int t_id = int.Parse(type_id);

        //string get_color_id = "select * from colors where name='" + DRP_COLOR.SelectedItem + "'";
        //string color_id = db.select_value(get_color_id, "id");
        //int c_id = int.Parse(color_id);

        //string get_size_id = "select * from sizes where name='" + DRP_SIZE.SelectedItem + "'";
        //string size_id = db.select_value(get_size_id, "id");
        //int s_id = int.Parse(size_id);

        //string name = "N" + TXT_NAME.Text.Trim();

            
        string ins_products = "INSERT INTO products ([id],name,category_id,fixed,removable,made_in, date, time) values (" + TXT_ID.Text + ",N'" + TXT_NAME.Text + "', " + DRP_CATEGORY.SelectedValue + ",'" + fixd() + "','" + removable() + "','" + TXT_MADE_IN.Text + "', '" + DateTime.Now.ToString("yyyy-MM-dd") + "', '" + DateTime.Now.ToString("HH:mm:ss") + "')";
        db.insert(ins_products);

        string ins_price_list = "INSERT INTO price_list (product_id, date, time) values (" + TXT_ID.Text + ", '" + DateTime.Now.ToString("yyyy-MM-dd") + "', '" + DateTime.Now.ToString("HH:mm:ss") + "')";
        db.insert(ins_price_list);

        //string get_type_id = "select * from types where name='" + DRP_TYPE.SelectedItem + "'";
        //string type_id = db.select_value(get_type_id, "id");
        //int t_id = int.Parse(type_id);

        //string update_type = "UPDATE types SET product_id = " + TXT_ID.Text + " WHERE id = " + t_id + "";
        //db.update(update_type);

        if (DRP_TYPE.Items.Count > 0 && DRP_TYPE.SelectedIndex > 0 && DRP_TYPE.Text != "Selected Items")
        {
            string update_type = "UPDATE products SET type_id = " + DRP_TYPE.SelectedValue + " WHERE id = " + TXT_ID.Text + "";
            db.update(update_type);

            string type_name = TXT_NAME.Text + "_" + DRP_TYPE.SelectedItem;
            string update_name = "UPDATE products SET  name= N'" + type_name + "' WHERE id = " + TXT_ID.Text + "";
            db.update(update_name);
        }

        if (DRP_SIZE.Items.Count > 0 && DRP_SIZE.SelectedIndex > 0)
        {
            string get_size_id = "select * from sizes where name='" + DRP_SIZE.SelectedItem + "'";
            string size_id = db.select_value(get_size_id, "id");
            int s_id = int.Parse(size_id);

            string update_size = "UPDATE products SET size_id = " + size_id + " WHERE id = " + TXT_ID.Text + "";
            db.update(update_size);

            // Concatenate Name + Size 
            if (DRP_TYPE.Text != "Selected Items" && DRP_SIZE.Text != "Selected Items")
            {
                string size_name = TXT_NAME.Text + "_" + DRP_TYPE.SelectedItem + "_" + DRP_SIZE.SelectedItem;
                string update_name = "UPDATE products SET  name= N'" + size_name + "' WHERE id = " + TXT_ID.Text + "";
                db.update(update_name);
            }
            else
            {
                string size_name = TXT_NAME.Text + "_"+ DRP_SIZE.SelectedItem;
                string update_name = "UPDATE products SET  name= N'" + size_name + "' WHERE id = " + TXT_ID.Text + "";
                db.update(update_name);
            }
            
        }

        if (DRP_COLOR.Items.Count > 0 && DRP_COLOR.SelectedIndex > 0 )
        {
            string get_color_id = "select * from colors where name='" + DRP_COLOR.SelectedItem + "'";
            string color_id = db.select_value(get_color_id, "id");
            int c_id = int.Parse(color_id);

            string update_color = "UPDATE products SET color_id = " + c_id + " WHERE id = " + TXT_ID.Text + "";
            db.update(update_color);
            string color_size_name;
            if (DRP_TYPE.Text != "Selected Items" && DRP_SIZE.Text != "Selected Items")
            {
                color_size_name = TXT_NAME.Text + "_" + DRP_TYPE.SelectedItem + "_" + DRP_SIZE.SelectedItem + "_" + DRP_COLOR.SelectedItem;
            }

            else if (DRP_TYPE.Text != "Selected Items" && DRP_SIZE.Items.Count <= 0)
            {
                color_size_name = TXT_NAME.Text + "_" + DRP_TYPE.SelectedItem + "_" + DRP_COLOR.SelectedItem;
            }
            else if (DRP_TYPE.Items.Count <=0  && DRP_SIZE.Text != "Selected Items")
            {
                color_size_name = TXT_NAME.Text + "_" + DRP_SIZE.SelectedItem + "_" + DRP_COLOR.SelectedItem;
            }
            else
            {
                color_size_name = TXT_NAME.Text + "_" + DRP_COLOR.SelectedItem;
            }
            string update_name = "UPDATE products SET  name= N'" + color_size_name + "' WHERE id = " + TXT_ID.Text + "";
            db.update(update_name);
        }
        //string ins_phone = "INSERT INTO phones ([id],supplier_id,phone) select  COALESCE (max(id),0)+1," + TXT_ID.Text + ",'" + TXT_PHONE.Text + "' from phones";
        //db.insert(ins_phone);



        clear_txt(TXT_ID, TXT_NAME);
        clear_drp(DRP_TYPE, DRP_SIZE, DRP_COLOR);
        txt_id();
        fill_type();
        fill_grid();
    }

    protected void Button4_Click(object sender, EventArgs e)
    {

    }



    protected void LINK_TYPE_CANCEL_Click(object sender, EventArgs e)
    {
        //TXT_TYPE_ID.Visible = false;
        TXT_TYPE_Name.Visible = false;
        LINK_TYPE_SUBMIT.Visible = false;
        LINK_TYPE_CANCEL.Visible = false;


    }


    protected void LINK_TYPE_NEW_Click(object sender, EventArgs e)
    {
        //TXT_TYPE_ID.Visible = true;
        TXT_TYPE_Name.Visible = true;
        LINK_TYPE_SUBMIT.Visible = true;
        LINK_TYPE_CANCEL.Visible = true;
        LINK_TYPE_EDITE_DELETE.Visible = true;
        txt_type_id();

    }

    protected void LINK_TYPE_SUBMIT_Click(object sender, EventArgs e)
    {

        bool check = valid_type();

        if (check == true)
        {            
            string ins_type = "INSERT INTO types ([id],name,category_id) values (" + TXT_TYPE_ID.Text + ",N'" + TXT_TYPE_Name.Text + "'," + DRP_CATEGORY.SelectedValue + ")";
            db.insert(ins_type);
            DRP_TYPE.Items.Clear();
            fill_type();

            clear_txt(TXT_TYPE_ID, TXT_TYPE_Name);
            txt_type_id();
        }

        else
        {
            Response.Write("<script>alert('please enter name, new name or choose category');</script>");
            return;
        }





        
    }

    protected void TXT_SIZE_ID_TextChanged(object sender, EventArgs e)
    {

    }

    protected void TXT_SIZE_NAME_TextChanged(object sender, EventArgs e)
    {

    }

    protected void TXT_COLOR_ID_TextChanged(object sender, EventArgs e)
    {

    }

    protected void TXT_COLOR_NAME_TextChanged(object sender, EventArgs e)
    {

    }

    protected void LINK_COLOR_NEW_Click(object sender, EventArgs e)
    {
        //TXT_COLOR_ID.Visible = true;
        TXT_COLOR_NAME.Visible = true;
        LINK_COLOR_SUBMIT.Visible = true;
        LINK_COLOR_CANCEL.Visible = true;
        txt_color_id();
    }

    protected void LINK_SIZE_NEW_Click(object sender, EventArgs e)
    {
        //TXT_SIZE_ID.Visible = true;
        TXT_SIZE_NAME.Visible = true;
        LINK_SIZE_SUBMIT.Visible = true;
        LINK_SIZE_CANCEL.Visible = true;
        txt_size_id();
    }

    protected void LINK_SIZE_CANCEL_Click(object sender, EventArgs e)
    {
        //TXT_SIZE_ID.Visible = false;
        TXT_SIZE_NAME.Visible = false;
        LINK_SIZE_SUBMIT.Visible = false;
        LINK_SIZE_CANCEL.Visible = false;
    }

    protected void LINK_COLOR_CANCEL_Click(object sender, EventArgs e)
    {
        TXT_COLOR_ID.Visible = false;
        TXT_COLOR_NAME.Visible = false;
        LINK_COLOR_SUBMIT.Visible = false;
        LINK_COLOR_CANCEL.Visible = false;
    }

    protected void TXT_TYPE_ID_TextChanged(object sender, EventArgs e)
    {

    }

    protected void LINK_SIZE_SUBMIT_Click(object sender, EventArgs e)
    {
        bool valid = valid_size();

        /*
            string get_type_id = "select * from types where name='" + DRP_TYPE.SelectedItem + "'";
            string type_id = db.select_value(get_type_id, "id");
            int t_id = int.Parse(type_id);
            string get_size_id = "select * from sizes where name='" + (TXT_SIZE_NAME.Text).Trim() + "'";
            string size_id = db.select_value(get_size_id, "id");
            //int s_id = 0;
            if (size_id != "")
            {
                int s_id = int.Parse(size_id);
                //txt_size_id();
                string ins_size = "INSERT INTO sizes ([id],name,type_id) values (" + s_id + ",N'" + TXT_SIZE_NAME.Text + "', " + type_id + ")";
                db.insert(ins_size);

            }

            else
            {
                txt_size_id();
                string ins_size = "INSERT INTO sizes ([id],name,type_id) values (" + TXT_SIZE_ID.Text + ", N'" + TXT_SIZE_NAME.Text + "', " + type_id + ")";
                db.insert(ins_size);
            }
        //else
        //{
        //    if (get_size_id != "")
        //    {
        //        //txt_size_id();
        //        string ins_size = "INSERT INTO sizes ([id],name,type_id) values (" + TXT_SIZE_ID.Text + ",'" + TXT_SIZE_NAME.Text + "', " + type_id + ")";
        //        db.insert(ins_size);
        //    }
        //    else
        //    {
        //        string ins_size = "INSERT INTO sizes ([id],name,type_id) values (" + s_id + ",'" + TXT_SIZE_NAME.Text + "', " + type_id + ")";
        //        db.insert(ins_size);
        //    }
        //}

        DRP_SIZE.Items.Clear();
        fill_size();
        clear_txt(TXT_SIZE_ID, TXT_SIZE_NAME);
        //txt_size_id();
    */


        if (valid == true)
        {
            if (DRP_TYPE.SelectedIndex <= 0)
            {
                txt_size_id();
                string insert = "INSERT INTO sizes ([id], [name],category_id) VALUES(" + TXT_SIZE_ID.Text + ", N'" + TXT_SIZE_NAME.Text + "'," + DRP_CATEGORY.SelectedValue + ")";
                //string insert = "";
                db.insert(insert);
                DRP_SIZE.Items.Clear();
                fill_size();
                fill_category_size();
                clear_txt(TXT_SIZE_ID, TXT_SIZE_NAME);
                txt_size_id();
            }
            else
            {
                txt_size_id();
                string insert = "INSERT INTO sizes ([id], [name],category_id, type_id) VALUES(" + TXT_SIZE_ID.Text + ", N'" + TXT_SIZE_NAME.Text + "'," + DRP_CATEGORY.SelectedValue + "," + DRP_TYPE.SelectedValue + ")";
                db.insert(insert);
                DRP_SIZE.Items.Clear();
                fill_size();
                clear_txt(TXT_SIZE_ID, TXT_SIZE_NAME);
                txt_size_id();
            }

        }

        else
        {
            Response.Write("<script>alert('please enter name or new name , should chooes Category');</script>");
            return;
        }
        
    }

    protected void DRP_TYPE_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DRP_TYPE.Text != "Selected Items")
        {

            //string get_type_id = "select * from types where name='" + DRP_TYPE.SelectedItem+ "'";
            //string type_id = db.select_value(get_type_id, "id");
            //int t_id = int.Parse(type_id);
            //string select_size = "select id, name from sizes where type_id= " + t_id + "";
            //db.select_combo(select_size, DRP_SIZE);

            fill_size();
            if (DRP_COLOR.Items.Count > 0)
            {
                DRP_COLOR.Items.Clear();
                
            }
            fill_type_color();
            //fill_type();
            //DRP_COLOR.Items.Clear();
            //fill_color();
        }
        else
        {
            if (DRP_SIZE.Items.Count > 0)
                DRP_SIZE.Items.Clear();
            if (DRP_COLOR.Items.Count > 0)
                DRP_COLOR.Items.Clear();

        }
    }



    protected void LINK_COLOR_SUBMIT_Click(object sender, EventArgs e)
    {
        /*
        string get_type_id = "select * from types where name='" + DRP_TYPE.SelectedItem + "'";
        string type_id = db.select_value(get_type_id, "id");
        int t_id = int.Parse(type_id);

        string get_size_id = "select * from sizes where name='" + DRP_SIZE.SelectedItem + "'";
        string size_id = db.select_value(get_size_id, "id");
        int s_id = int.Parse(size_id);

        string get_color_id = "select * from colors where name='" + (TXT_COLOR_NAME.Text).Trim() + "'";
        string color_id = db.select_value(get_color_id, "id");
        //int s_id = 0;
        if (color_id != "")
        {
            int c_id = int.Parse(color_id);
            string ins_color = "INSERT INTO colors ([id],name,type_id,size_id) values (" + c_id + ",N'" + TXT_COLOR_NAME.Text + "', " + type_id + "," + size_id + ")";
            db.insert(ins_color);

        }

        else
        {
            txt_color_id();
            string ins_color = "INSERT INTO colors ([id],name,type_id,size_id) values (" + TXT_COLOR_ID.Text + ", N'" + TXT_COLOR_NAME.Text + "', " + type_id + "," + size_id + ")";
            db.insert(ins_color);
        }



        DRP_COLOR.Items.Clear();
        fill_color();
        clear_txt(TXT_COLOR_ID, TXT_COLOR_NAME);
        //txt_color_id();
        */


        bool valid = valid_color();

        if (valid == true)
        {
            if (DRP_TYPE.SelectedIndex <= 0 && DRP_SIZE.SelectedIndex <= 0)
            {
                string insert = "INSERT INTO colors ([id], [name],category_id) VALUES(" + TXT_COLOR_ID.Text + ", N'" + TXT_COLOR_NAME.Text + "'," + DRP_CATEGORY.SelectedValue + ")";
                db.insert(insert);
                DRP_COLOR.Items.Clear();
                fill_category_color();
                clear_txt(TXT_COLOR_ID, TXT_COLOR_NAME);
                txt_color_id();
            }

            else if (DRP_TYPE.SelectedIndex > 0 && DRP_SIZE.SelectedIndex <= 0)
            {
                string insert = "INSERT INTO colors ([id], [name],category_id, type_id) VALUES(" + TXT_COLOR_ID.Text + ", N'" + TXT_COLOR_NAME.Text + "'," + DRP_CATEGORY.SelectedValue + "," + DRP_TYPE.SelectedValue + ")";
                db.insert(insert);
                DRP_COLOR.Items.Clear();
                fill_type_color();
                clear_txt(TXT_COLOR_ID, TXT_COLOR_NAME);
                txt_color_id();
            }

            else if (DRP_TYPE.SelectedIndex <= 0 && DRP_SIZE.SelectedIndex > 0)
            {
                string insert = "INSERT INTO colors ([id], [name],category_id, size_id) VALUES(" + TXT_COLOR_ID.Text + ", N'" + TXT_COLOR_NAME.Text + "'," + DRP_CATEGORY.SelectedValue + "," + DRP_SIZE.SelectedValue + ")";
                db.insert(insert);
                DRP_COLOR.Items.Clear();
                //fill_size();
                fill_size_color();
                clear_txt(TXT_COLOR_ID, TXT_COLOR_NAME);
                txt_color_id();
            }

            else
            {
                string insert = "INSERT INTO colors ([id], [name],category_id, type_id,size_id) VALUES(" + TXT_COLOR_ID.Text + ", N'" + TXT_COLOR_NAME.Text + "'," + DRP_CATEGORY.SelectedValue + "," + DRP_TYPE.SelectedValue + "," + DRP_SIZE.SelectedValue + ")";
                db.insert(insert);
                DRP_COLOR.Items.Clear();
                //fill_size();
                fill_color();
                //fill_category_size();
                clear_txt(TXT_COLOR_ID, TXT_COLOR_NAME);
                txt_color_id();
            }

        }

        else
        {
            Response.Write("<script>alert('please enter name or new name , should chooes Category');</script>");
            return;
        }
    }

    protected void DRP_SIZE_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DRP_CATEGORY.SelectedIndex !=0 && DRP_SIZE.SelectedIndex != 0 && DRP_TYPE.SelectedIndex <=0)
        {
            fill_size_color();
        }

        else if (DRP_TYPE.SelectedIndex != 0 && DRP_SIZE.SelectedIndex != 0)
        {

            //string get_type_id = "select * from types where name='" + DRP_TYPE.SelectedItem+ "'";
            //string type_id = db.select_value(get_type_id, "id");
            //int t_id = int.Parse(type_id);
            //string select_size = "select id, name from sizes where type_id= " + t_id + "";
            //db.select_combo(select_size, DRP_SIZE);
            fill_color();
            //fill_type();
            //DRP_COLOR.Items.Clear();
            //fill_color();
        }

        else
        {
            if (DRP_SIZE.Items.Count > 0 && DRP_TYPE.Items.Count > 0)
                DRP_COLOR.Items.Clear();
          
        }
    }

    protected void DRP_TYPE_TextChanged(object sender, EventArgs e)
    {

    }

    protected void DRP_TYPE_Load(object sender, EventArgs e)
    {

    }

    protected void DRP_SIZE_Load(object sender, EventArgs e)
    {
        //DRP_COLOR.Items.Clear();
        //fill_color();
    }

    protected void DRP_SIZE_TextChanged(object sender, EventArgs e)
    {
        //DRP_COLOR.Items.Clear();
        //fill_color();
    }

    protected void DRP_SIZE_Init(object sender, EventArgs e)
    {
        //DRP_COLOR.Items.Clear();
        //fill_color();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //fill_grid();
        db.ds1 = (DataSet)ViewState["ds"];
        int index = e.RowIndex;
        db.deleting_grid(db.ds1, "products", GridView1, index);

        fill_grid();
        txt_id();
    }

    protected void LINK_TYPE_EDITE_DELETE_Click(object sender, EventArgs e)
    {
        Response.Redirect("types.aspx");
    }

    protected void LINK_COLOR_EDITE_DETELTE_Click(object sender, EventArgs e)
    {
        Response.Redirect("colors.aspx");
    }

    protected void CHK_DYNMIC_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void DRP_CATEGORY_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DRP_CATEGORY.Text != "Selected Items")
        {
            fill_type();
            if (DRP_SIZE.Items.Count > 0)
                DRP_SIZE.Items.Clear();
            if (DRP_COLOR.Items.Count > 0)
                DRP_COLOR.Items.Clear();
            fill_category_size();
            fill_category_color();
        }

        else
        {
            if (DRP_TYPE.Items.Count > 0)
                DRP_TYPE.Items.Clear();
            if (DRP_SIZE.Items.Count > 0)
                DRP_SIZE.Items.Clear();
            if (DRP_COLOR.Items.Count > 0)
                DRP_COLOR.Items.Clear();
        }
            
    }

    protected void LINK_CATEGORY_NEW_Click(object sender, EventArgs e)
    {
        TXT_CATEGORY_Name.Visible = true;
        LINK_CATEGORY_SUBMIT.Visible = true;
        LINK_CATEGORY_CANCEL.Visible = true;
        LINK_CATEGORY_EDITE_DELETE.Visible = true;
        txt_category_id();
    }

    protected void TXT_CATEGORY_ID_TextChanged(object sender, EventArgs e)
    {

    }

    protected void TXT_CATEGORY_Name_TextChanged(object sender, EventArgs e)
    {

    }
    
    protected void LINK_CATEGORY_SUBMIT_Click(object sender, EventArgs e)
    {
        bool check = valid_category();

        if (check == true)
        {
            string ins_type = "INSERT INTO categories ([id],name) values (" + TXT_CATEGORY_ID.Text + ",N'" + TXT_CATEGORY_Name.Text + "')";
            db.insert(ins_type);
            DRP_CATEGORY.Items.Clear();
            fill_category();
            clear_txt(TXT_CATEGORY_ID, TXT_CATEGORY_Name);
            txt_category_id();
        }

        else
        {
            Response.Write("<script>alert('please enter name or new name');</script>");
            return;
        }
    }

    protected void LINK_CATEGORY_CANCEL_Click(object sender, EventArgs e)
    {
        TXT_CATEGORY_Name.Visible = false;
        LINK_CATEGORY_SUBMIT.Visible = false;
        LINK_CATEGORY_CANCEL.Visible = false;
    }

    protected void LINK_CATEGORY_EDITE_DELETE_Click(object sender, EventArgs e)
    {
        Response.Redirect("categories.aspx");
    }

    protected void LINK_SIZE_EDITE_DELETE_Click(object sender, EventArgs e)
    {
        Response.Redirect("sizes.aspx");
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        

        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int prod_id = int.Parse(((Label)(GridView1.Rows[index].FindControl("LBL_PROD_ID_GRD"))).Text);
            string prod_name = ((Label)(GridView1.Rows[index].FindControl("LBL_PROD_NAME_GRD"))).Text;
            
            fill_grid_qty(prod_id);
            hide_product_cost_grid(false);
        }



    }
}