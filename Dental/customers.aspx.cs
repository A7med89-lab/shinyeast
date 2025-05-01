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



public partial class customers : System.Web.UI.Page
{
    public void filldrg()
    {
        //string select = "select customers.id, customers.name, regions.name as region, addresses.street, addresses.buid_number, phones.phone,customers.fixed, customers.dynamic from customers  inner join addresses  on customers.id = addresses.customer_id inner join phones on customers.id = phones.customer_id inner join regions on addresses.regoin_id = regions.id";
        string select = "select customers.id, customers.name,labs.name as lab ,regions.name as region, regions.id as region_id,addresses.street, addresses.buid_number, phones.phone,customers.fixed, customers.dynamic, customers.wh_from,customers.wh_to from customers  inner join addresses  on customers.id = addresses.customer_id inner join labs on customers.id = labs.customer_id inner join phones on customers.id = phones.customer_id inner join regions on addresses.regoin_id = regions.id ";
        //test //
        //string select = "select customers.id, customers.name,labs.name as lab , addresses.street, addresses.buid_number, phones.phone,customers.fixed, customers.dynamic, customers.wh_from,customers.wh_to from customers  inner join addresses  on customers.id = addresses.customer_id inner join labs on customers.id = labs.customer_id inner join phones on customers.id = phones.customer_id inner join regions on addresses.regoin_id = regions.id ";
        // test //
        db.select(select, GridView1);
        ViewState.Add("ds", db.ds1);


    }

    public void txt_id()
    {
        string id = "select max(id)+1 as id from customers";
        db.select_id(id, TXT_ID);
        if (TXT_ID.Text == "")
        {
            TXT_ID.Text = 1.ToString();
        }

    }

    database db = new database();



    public void clear_txt(params TextBox[] txts)
    {
        for (int i = 0; i < txts.Length; i++)
        {

            txts[i].Text = string.Empty;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {


        txt_id();
        if (IsPostBack == false)
        {
            filldrg();
            DRP_REGON.Items.Clear();
            string query = "select * from regions";
            db.select_combo(query, DRP_REGON);
            //DRP_REGON.SelectedItem.Text=" ";
        }


    }

    protected void TextBox6_TextChanged(object sender, EventArgs e)
    {


    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string ins_cst = "INSERT INTO customers ([id], [name], [address_id],[phone_id], [fixed], [dynamic], wh_from, wh_to) VALUES(" + TXT_ID.Text + ", '" + TXT_NAME.Text + "', " + TXT_ID.Text + "," + TXT_ID.Text + ",'" + CHK_FXD.Checked + "','" + CHK_DYNMIC.Checked + "','" + DRP_FROM.SelectedItem + "', '" + DRP_TO.SelectedItem + "')";
        db.insert(ins_cst);

        string ins_labs = "INSERT INTO labs ([id],name,customer_id) select  COALESCE (max(id),0)+1,'" + TXT_LAB_NAME.Text + "'," + TXT_ID.Text + " from labs";
        db.insert(ins_labs);
        //string ins_add = "INSERT INTO addresses ([id]) select  max(id) +1 from addresses";
        string ins_add = "INSERT INTO addresses ([id],customer_id,regoin_id,street,buid_number) select  COALESCE (max(id),0)+1," + TXT_ID.Text + "," + DRP_REGON.SelectedValue + ",'" + TXT_STRT.Text + "','" + TXT_BULD.Text + "' from addresses";
        db.insert(ins_add);
        //string update_add = "UPDATE addresses SET customer_id = '" + TXT_ID.Text + "' ,regoin_id = "+ DRP_REGON.SelectedValue +" WHERE id = (select  max(id) from addresses)";
        //db.update(update_add);
        //string ins_phone = "INSERT INTO addresses ([id]) select  max(id) +1 from phones";
        string ph = TXT_PHONE.Text;
        
        string[] phones = ph.Split('\r');
        for (int i=0 ; i < phones.Length;i++)
        {
            string ins_phone = "INSERT INTO phones ([id],customer_id,phone) select  COALESCE (max(id),0)+1," + TXT_ID.Text + ",'" + phones[i] + "' from phones";
            db.insert(ins_phone);
        }

        //string update_phone = "UPDATE addresses SET customer_id = '" + TXT_ID.Text + "' ,regoin_id = " + DRP_REGON.SelectedValue + " WHERE id = (select  max(id) from addresses)";
        //db.update(update_add);
        clear_txt(TXT_BULD, TXT_NAME, TXT_PHONE, TXT_STRT, TXT_LAB_NAME);
        CHK_DYNMIC.Checked = false;
        CHK_FXD.Checked = false;
        //string select = "select customers.id, customers.name, regions.name as region, addresses.street, addresses.buid_number, phones.phone,customers.fixed, customers.dynamic from customers  inner join addresses  on customers.id = addresses.customer_id inner join phones on customers.id = phones.customer_id inner join regions on addresses.regoin_id = regions.id";
        string select = "select customers.id, customers.name,labs.name as lab ,regions.name as region, addresses.street, addresses.buid_number, phones.phone,customers.fixed, customers.dynamic, customers.wh_from,customers.wh_to from customers  inner join addresses  on customers.id = addresses.customer_id inner join labs on customers.id = labs.customer_id inner join phones on customers.id = phones.customer_id inner join regions on addresses.regoin_id = regions.id ";
        db.select(select, GridView1);
        string id = "select max(id)+1 as id from customers";
        db.select_id(id, TXT_ID);
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("appointments.aspx");
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        string delete = "DELETE FROM emp_info WHERE id = '" + TXT_ID.Text + "'";
        db.delete(delete);
    }



    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        filldrg();

        //db.ds1 = (DataSet)ViewState["ds"];
        db.ds1.Tables[0].Rows[e.RowIndex].Delete();
        GridView1.DataSource = db.ds1.Tables[0];
        GridView1.DataBind();
        //ViewState["ds"] = db.ds1;
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();

        con.ConnectionString = ConfigurationManager.ConnectionStrings["dental"].ToString();
        if (con.State != ConnectionState.Open)
        {
            con.Open();
        }
        com.Connection = con;
        com.CommandText = "delete from customers where id=@id";
        SqlParameter param = new SqlParameter("@id", SqlDbType.Decimal, 4, "id");
        //param.Value = GridView1.Rows[e.RowIndex].Cells[e.RowIndex).FindControl(;
        com.Parameters.Add(param);
        SqlDataAdapter da = new SqlDataAdapter(com);
        da.DeleteCommand = com;
        da.Update(db.ds1);
        txt_id();

        filldrg();
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        db.ds1 = (DataSet)ViewState["ds"];
        GridView1.EditIndex = -1;  // transfer row to desgin mode
        GridView1.DataSource = db.ds1.Tables[0];
        GridView1.DataBind();

    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

        filldrg();

        db.ds1 = (DataSet)ViewState["ds"];
        GridView1.EditIndex = e.NewEditIndex; // transfer row to edite mode
        //db.ds1.Tables[0].Rows[e.NewEditIndex][1]= ((TextBox)(GridView1.Rows[e.NewEditIndex].Cells[1].FindControl("cst_name_edite"))).Text;
        GridView1.DataSource = db.ds1.Tables[0];
        GridView1.DataBind();
        ViewState["ds"] = db.ds1;


        /*
        
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["dental"].ToString();
        if (con.State != ConnectionState.Open)
        {
            con.Open();
        }
        com.Connection = con;
        com.CommandText = "UPDATE customers SET name = @name  WHERE fname = id = @id";
        SqlParameter param1 = new SqlParameter("@id", SqlDbType.Decimal, 4, "id");
        SqlParameter param2 = new SqlParameter("name", SqlDbType.Decimal, 4, "name");

        com.Parameters.Add(param1);
        com.Parameters.Add(param2);
        SqlDataAdapter da = new SqlDataAdapter(com);
        da.UpdateCommand = com;
        da.Update(db.ds1);

        txt_id();
        */

    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        //filldrg();

        db.ds1 = (DataSet)ViewState["ds"];
        GridView1.EditIndex = e.RowIndex; // transfer row to edite mode
        string x = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].FindControl("cst_name_edite"))).Text;
        db.ds1.Tables[0].Rows[e.RowIndex][1] = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].FindControl("cst_name_edite"))).Text;
        GridView1.DataSource = db.ds1.Tables[0];
        GridView1.DataBind();
        ViewState["ds"] = db.ds1;


        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["dental"].ToString();
        if (con.State != ConnectionState.Open)
        {
            con.Open();
        }
        com.Connection = con;
        com.CommandText = "UPDATE customers SET name = @name   WHERE id = @id";
        SqlParameter param1 = new SqlParameter("@id", SqlDbType.Decimal, 4, "id");
        SqlParameter param2 = new SqlParameter("name", SqlDbType.VarChar, 50, "name");

        com.Parameters.Add(param1);
        com.Parameters.Add(param2);
        SqlDataAdapter da = new SqlDataAdapter(com);
        da.UpdateCommand = com;
        da.Update(db.ds1);

        db.ds1 = (DataSet)ViewState["ds"];
        GridView1.EditIndex = -1;  // transfer row to desgin mode
        GridView1.DataSource = db.ds1.Tables[0];
        GridView1.DataBind();



        txt_id();
    }



    protected void TXT_LAB_NAME_TextChanged(object sender, EventArgs e)
    {

    }
}