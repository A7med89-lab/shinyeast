using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cash_trans : System.Web.UI.Page
{
    public void fill_grd()
    {

        //string select = "select c.id, c.name, ct.trans_date, ct.trans_type, ct.[in], ct.[out], ct.amount, ct.curr_amount from cashes c inner join cash_trans ct on c.id = ct.cash_id";
        string select = "select ct.id, c.id as cash_id, c.name, ct.trans_date, ct.trans_type, ct.[in], ct.[out], ct.sales_id, ct.purchase_id, sum(ct.amount) as amount, sum(ct.curr_amount) as current_amount from cashes c inner join cash_trans ct on c.id = ct.cash_id group by ct.id,c.id, c.name, ct.trans_date, ct.trans_type, ct.[in], ct.[out], ct.sales_id, ct.purchase_id, ct.amount, ct.curr_amount";
        db.select(select, GridView1);
        ViewState.Add("ds", db.ds1);
    }

    public void fill_grd(DropDownList DRP)
    {
        if(DRP.SelectedIndex == 0 || DRP.SelectedItem.Text == "Selected Cash")
        {
            string select = "select ct.id, c.id as cash_id, c.name, ct.trans_date, ct.trans_type, ct.[in], ct.[out], ct.sales_id, ct.purchase_id, sum(ct.amount) as amount, sum(ct.curr_amount) as current_amount from cashes c inner join cash_trans ct on c.id = ct.cash_id group by ct.id,c.id, c.name, ct.trans_date, ct.trans_type, ct.[in], ct.[out], ct.sales_id, ct.purchase_id, ct.amount, ct.curr_amount";
            db.select(select, GridView1);
            ViewState.Add("ds", db.ds1);
        }

        else
        {
            string select = "select ct.id, c.id as cash_id, c.name, ct.trans_date, ct.trans_type, ct.[in], ct.[out], ct.sales_id, ct.purchase_id, sum(ct.amount) as amount, sum(ct.curr_amount) as current_amount from cashes c inner join cash_trans ct on c.id = ct.cash_id group by ct.id,c.id, c.name, ct.trans_date, ct.trans_type, ct.[in], ct.[out], ct.sales_id, ct.purchase_id, ct.amount, ct.curr_amount having c.id=" + DRP.SelectedValue + "";
            db.select(select, GridView1);
            ViewState.Add("ds", db.ds1);
        }

        
    }

    public void fill_cash_type()
    {
        string select;
        if (DRP_TYPE.SelectedIndex==1)
        {
            select = "select c.id, c.name, ct.trans_date, ct.trans_type, ct.[in], ct.[out], ct.amount, ct.curr_amount from cashes c inner join cash_trans ct on c.id = ct.cash_id";
            db.select(select, GridView1);
            ViewState.Add("ds", db.ds1);
        }

        else if (DRP_TYPE.SelectedIndex == 2)
        {
            select = "select c.id, c.name, ct.trans_date, ct.trans_type, ct.[in], ct.[out], ct.amount, ct.curr_amount from cashes c inner join cash_trans ct on c.id = ct.cash_id";
            db.select(select, GridView1);
            ViewState.Add("ds", db.ds1);
        }

        else
        {
            select = "select c.id, c.name, ct.trans_date, ct.trans_type, ct.[in], ct.[out], ct.amount, ct.curr_amount from cashes c inner join cash_trans ct on c.id = ct.cash_id";
            db.select(select, GridView1);
            ViewState.Add("ds", db.ds1);
        }
        
    }

    public void fill_cash()
    {
        string product = "select id, name from cashes";
        db.select_combo(product, DRP_CASH);
        DRP_CASH.Items.Insert(0, "Selected Cash");

    }
    public void max_id()
    {
        string query = "select  max (id)+1 as id from cash_trans";
        db.select_id(query, TXT_ID);
        if (TXT_ID.Text == "")
        {
            TXT_ID.Text = 1.ToString();
        }
    }

    bool validation()
    {
        // validate deduplicate username
        // should enter amount
        // can't withdraw IFF amount >= Curr Amount
        int amount;
        string curr = db.select_value("select curr_amount from cash_trans where id = (select max (id) from cash_trans where cash_id=" + DRP_CASH.SelectedValue + ")", "curr_amount");
        if (curr == "")
        {
            amount = 0;
        }
        else
        {
            amount = int.Parse(curr);
        }    
        if (int.Parse(TXT_AMOUNT.Text) > amount)
        {
           return false;
        }
        else
        {
            return true;
        }
    }

    public void clear()
    {
        TXT_ID.Text = "";
    }
    database db = new database();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            fill_grd();
            fill_cash();
            max_id();
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            TXT_DATE.Text = date;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string insert;
        int amount;
        string curr = db.select_value("select curr_amount from cash_trans where id = (select max (id) from cash_trans where cash_id="+DRP_CASH.SelectedValue+")", "curr_amount");
        if (curr == "")
        {
            amount = 0;
        }
        else
        {
            amount = int.Parse(curr);
        }
        
        int curr_amount;

        if (DRP_TYPE.SelectedIndex == 1) //insert
        {
            curr_amount = (int.Parse(TXT_AMOUNT.Text)) + amount;
            insert = "insert into cash_trans (id, cash_id, trans_date ,trans_type ,[in],[out],amount,curr_amount  ) values (" + TXT_ID.Text + ", " + DRP_CASH.SelectedValue + ", '" + TXT_DATE.Text + "', '" + "insert" + "', '" + true + "', '" + false + "', "+TXT_AMOUNT.Text+","+curr_amount+")";
            db.insert(insert);
            insert = "Update cashes set id = " + DRP_CASH.SelectedValue + ", name = '" + DRP_CASH.SelectedItem.Text + "', amount = " + curr_amount + " where id = " + DRP_CASH.SelectedValue + " ";
            db.insert(insert);
            clear();
            fill_grd(DRP_CASH);
            max_id();
        }

        if (DRP_TYPE.SelectedIndex == 2) // withdraw
        {

            bool curr_amunt;
            curr_amunt = validation();
            if (curr_amunt == false)
            {
                Response.Write("<script>alert('You Withdraw amount greater than current Balance');</script>");
                return;
            }
            else
            {
                curr_amount = amount - (int.Parse(TXT_AMOUNT.Text));
                insert = "insert into cash_trans (id, cash_id, trans_date ,trans_type ,[in],[out],amount,curr_amount  ) values (" + TXT_ID.Text + ", " + DRP_CASH.SelectedValue + ", '" + TXT_DATE.Text + "', '" + "Withdraw" + "', '" + false + "', '" + true + "', " + TXT_AMOUNT.Text + "," + curr_amount + ")";
                db.insert(insert);
                insert = "Update cashes set id = " + DRP_CASH.SelectedValue + ", name = '" + DRP_CASH.SelectedItem.Text + "', amount = " + curr_amount + " where id = "+ DRP_CASH.SelectedValue+" ";
                db.insert(insert);
                clear();
                fill_grd(DRP_CASH);
                max_id();
            }
            
        }
        
    }

    protected void DRP_CASH_SelectedIndexChanged(object sender, EventArgs e)
    {
        fill_grd(DRP_CASH);
    }
}