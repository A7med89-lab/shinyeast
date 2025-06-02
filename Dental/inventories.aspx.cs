using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class inventories : System.Web.UI.Page
{
    public void filldrg()
    {
        //string select = "select suppliers.id, suppliers.name,regions.name as region,addresses.street, addresses.buid_number, phones.phone, suppliers.wh_from, suppliers.wh_to from suppliers  inner join addresses  on suppliers.id = addresses.supplier_id inner join phones on suppliers.id = phones.supplier_id inner join regions on addresses.regoin_id = regions.id ";
        //string select = "select suppliers.id, suppliers.name,regions.name as region,addresses.street, addresses.buid_number, phones.phone, suppliers.wh_from, suppliers.wh_to from suppliers inner join addresses  on suppliers.id = addresses.supplier_id left join phones on suppliers.id = phones.supplier_id left join regions on addresses.regoin_id = regions.id";
        string select = "select inventories.id, inventories.name , inventories.[desc], cities.name as city_name ,regions.name as region_name, addresses.street as street, addresses.buid_number as buid_number from inventories inner join addresses on inventories.address_id = addresses.id  left join regions on addresses.regoin_id = regions.id left join cities on addresses.city_id = cities.id";
        db.select(select, GridView1);
        ViewState.Add("ds", db.ds1);
    }

    public void txt_id()
    {
        string id = "select max(id)+1 as id from inventories";
        db.select_id(id, TXT_ID);
        if (TXT_ID.Text == "")
        {
            TXT_ID.Text = 1.ToString();
        }
    }

    public void fill_region()
    {
        DRP_CITY.Items.Clear();
        string query = "select * from regions";
        db.select_combo(query, DRP_REGON);
    }

    public void fill_city()
    {
        DRP_CITY.Items.Clear();
        string query = "select * from cities";
        db.select_combo(query, DRP_CITY);
    }

    public void clear()
    {
        TXT_NAME.Text = "";
        TXT_BUILD_NO.Text = "";
        TXT_NOTES.Text = "";
        TXT_STREET.Text = "";
        DRP_CITY.SelectedIndex = 0;
        DRP_REGON.SelectedIndex = 0;

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
        if (!IsPostBack)
        {
            txt_id();
            fill_region();
            fill_city();
            filldrg();
        }

    }

    protected void BTN_SAVE_Click(object sender, EventArgs e)
    {
       
        string ins_add = "INSERT INTO addresses ([id],city_id, regoin_id , street, buid_number) select  COALESCE (max(id),0)+1," + DRP_CITY.SelectedValue + "," + DRP_REGON.SelectedValue + ", N'" + TXT_STREET.Text + "', N'" + TXT_BUILD_NO.Text + "' from addresses";
        db.insert(ins_add);
        int address_id = int.Parse(db.select_value("select max(id) as id from addresses", "id"));
        string query = "insert into inventories (id, name,address_id,[desc]) values (" + TXT_ID.Text + ", N'" + TXT_NAME.Text + "'," + address_id + ",N'" + TXT_NOTES.Text + "')";
        db.insert(query);        
            
        fill_region();
        filldrg();
        txt_id();
        fill_city();
        clear_txt(TXT_NAME, TXT_BUILD_NO, TXT_NOTES, TXT_STREET);
       
        
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        filldrg();
        db.ds1 = (DataSet)ViewState["ds"];
        db.deleting_grid(db.ds1, "inventories", GridView1, e.RowIndex);
        filldrg();
        txt_id();
        clear();
    }
}