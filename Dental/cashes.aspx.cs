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

public partial class cashes : System.Web.UI.Page
{
    public void fill_grd()
    {

        string select = "select id,name,amount from cashes";
        db.select(select, GridView1);
        ViewState.Add("ds", db.ds1);
    }

    public void fill_grd_details(int id)
    {

        //string select = "select c.id, c.name, ct.trans_date, ct.trans_type, ct.[in], ct.[out], ct.amount, ct.curr_amount from cashes c inner join cash_trans ct on c.id = ct.cash_id";
        string select = "select ct.id, c.id as cash_id, c.name, ct.trans_date, ct.trans_type, ct.[in], ct.[out], ct.sales_id, ct.purchase_id, sum(ct.amount) as amount, sum(ct.curr_amount) as current_amount from cashes c inner join cash_trans ct on c.id = ct.cash_id group by ct.id,c.id, c.name, ct.trans_date, ct.trans_type, ct.[in], ct.[out], ct.sales_id, ct.purchase_id, ct.amount, ct.curr_amount having c.id="+ id +"";
        db.select(select, GridView2);
        ViewState.Add("ds", db.ds1);
    }


    public void max_id()
    {
        string query = "select  max (id)+1 as id from cashes";
        db.select_id(query, TXT_ID);
        if (TXT_ID.Text == "")
        {
            TXT_ID.Text = 1.ToString();
        }
    }

    public void validation()
    {
        // validate deduplicate username
        // should username and password is entered
    }

    public void clear()
    {
        TXT_ID.Text = "";
        TXT_NAME.Text = "";
        
    }

    public void amount()
    {
        
        if (LBL_AMOUNT.Text=="")
        {
            LBL_AMOUNT.Text = "0";
        }

    }
    database db = new database();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            fill_grd();
            max_id();
            amount();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string insert = "insert into cashes (id, name) values (" + TXT_ID.Text + ", N'" + TXT_NAME.Text +"')";
        db.insert(insert);
        clear();
        fill_grd();
        max_id();

    }

    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        fill_grd();
        db.ds1 = (DataSet)ViewState["ds"];
        int id = int.Parse(((Label)(GridView1.Rows[e.NewSelectedIndex].Cells[0].FindControl("LBL_ID_GRD"))).Text);
        fill_grd_details(id);

    }
}