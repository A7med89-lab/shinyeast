using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;


public partial class suppliers : System.Web.UI.Page
{
    public void filldrg()
    {
        //string select = "select suppliers.id, suppliers.name,regions.name as region,addresses.street, addresses.buid_number, phones.phone, suppliers.wh_from, suppliers.wh_to from suppliers  inner join addresses  on suppliers.id = addresses.supplier_id inner join phones on suppliers.id = phones.supplier_id inner join regions on addresses.regoin_id = regions.id ";
        string select = "select suppliers.id, suppliers.name,regions.name as region,addresses.street, addresses.buid_number, phones.phone, suppliers.wh_from, suppliers.wh_to from suppliers inner join addresses  on suppliers.id = addresses.supplier_id left join phones on suppliers.id = phones.supplier_id left join regions on addresses.regoin_id = regions.id";
        db.select(select, GridView1);
        ViewState.Add("ds", db.ds1);
    }

    public void txt_id()
    {
        string id = "select max(id)+1 as id from suppliers";
        db.select_id(id, TXT_ID);
        if (TXT_ID.Text == "")
        {
            TXT_ID.Text = 1.ToString();
        }
    }

    public void fill_region()
    {
        DRP_REGON.Items.Clear();
        string query = "select * from regions";
        db.select_combo(query, DRP_REGON);
    }


    public void clear_txt(params TextBox[] txts)
    {
        for (int i = 0; i < txts.Length; i++)
        {

            txts[i].Text = string.Empty;
        }
    }

    database db = new database();
    protected void Page_Load(object sender, EventArgs e)
    {
        txt_id();
        fill_region();
        filldrg();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string ins_cst = "INSERT INTO suppliers ([id], [name], [email], wh_from, wh_to) VALUES(" + TXT_ID.Text + ", '" + TXT_NAME.Text + "','" + TXT_MAIL.Text + "','" + DRP_FROM.SelectedItem + "','" + DRP_TO.SelectedItem + "')";
        db.insert(ins_cst);

        string ins_add = "INSERT INTO addresses ([id],supplier_id,regoin_id,street,buid_number) select  COALESCE (max(id),0)+1," + TXT_ID.Text + "," + DRP_REGON.SelectedValue + ",'" + TXT_STRT.Text + "','" + TXT_BULD.Text + "' from addresses";
        db.insert(ins_add);

        string ins_phone = "INSERT INTO phones ([id],supplier_id,phone) select  COALESCE (max(id),0)+1," + TXT_ID.Text + ",'" + TXT_PHONE.Text + "' from phones";
        db.insert(ins_phone);

        clear_txt(TXT_ID, TXT_NAME, TXT_PHONE, TXT_BULD, TXT_MAIL, TXT_PHONE, TXT_STRT);

        txt_id();

        filldrg();

    }

    protected void Button4_Click(object sender, EventArgs e)
    {

    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        filldrg();
        DRP_REGON.Items.Clear();
        string query = "select * from regions";
        db.select_combo(query, DRP_REGON);

    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        db.ds1 = (DataSet)ViewState["ds"];
        int index = e.RowIndex;
        db.deleting_grid(db.ds1, "suppliers", GridView1, index);
        txt_id();
        filldrg();
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

        GridView1.EditIndex = -1;
        filldrg();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {

    }
}