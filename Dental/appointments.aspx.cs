using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class appointments : System.Web.UI.Page
{

    public void filldrg()
    {

        string select = "select appointments.id,customers.name, labs.name as lab_name, customers.wh_from, customers.wh_to ,appointments.date, appointments.status from customers inner join appointments on customers.id = appointments.customer_id inner join labs on customers.id = labs.customer_id ";
        db.select(select, GridView1);
        ViewState.Add("ds", db.ds1);


    }

    public void txt_id()
    {
        string id = "select max(id)+1 as id from appointments";
        db.select_id(id, TXT_ID);
        if (TXT_ID.Text == "")
        {
            TXT_ID.Text = 1.ToString();
        }

    }

    public void filldrp()
    {
        string cst = "select id, name from customers";
        db.select_combo(cst, DRP_CST);
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
        if (IsPostBack == false)
        {
            txt_id();
            filldrg();
            filldrp();
        }

    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        filldrg();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        db.ds1 = (DataSet)ViewState["ds"];
        int count = db.ds1.Tables[0].Rows.Count;
        string[] cst_data_dataset = new string[count];
        string[] cst_data_grid = new string[count];
        for (int i = 0; i < cst_data_dataset.Length; i++)
            cst_data_dataset[i] = db.ds1.Tables[0].Rows[e.RowIndex][5].ToString();
        for (int i = 0; i < cst_data_grid.Length; i++)
        {
            cst_data_grid[i] = ((TextBox)GridView1.Rows[e.RowIndex].Cells[5].FindControl("date_edite")).Text;
            db.ds1.Tables[0].Rows[e.RowIndex][5] = cst_data_grid[i];
        }

        string update = "UPDATE appointments SET date = @date WHERE id = @id";
        SqlParameter param1 = new SqlParameter("@id", SqlDbType.Int, 4, "id");
        SqlParameter param2 = new SqlParameter("@date", SqlDbType.VarChar, 50, "date");
        db.Updating_grid(db.ds1, update, GridView1, cst_data_dataset, cst_data_grid, param1, param2);
        GridView1.EditIndex = -1;
        filldrg();

    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        db.ds1 = (DataSet)ViewState["ds"];
        int index = e.RowIndex;
        db.deleting_grid(db.ds1, "appointments", GridView1, index);
        txt_id();

    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

        GridView1.EditIndex = e.NewEditIndex;
        filldrg();


    }

    protected void Button1_Click(object sender, EventArgs e)

    {
        try
        {
            string cstname = DRP_CST.SelectedItem.Text;
            string select_name = "select id,name from customers where name='" + cstname + "'";
            string id = db.select_value(select_name, cstname, "id");
            int idd = int.Parse(id);
            string date = Calendar2.SelectedDate.ToString("dd/MM/yyyy");

            string insert = "INSERT INTO appointments ([id], [customer_id],  [date]) VALUES(" + TXT_ID.Text + ", " + idd + ", '" + date + "')";

            db.insert(insert);

            clear_txt(TXT_ID);
            filldrg();
            txt_id();
            filldrp();

        }
        catch
        {

        }

    }

    protected void Button4_Click(object sender, EventArgs e)
    {

    }

    protected void Button2_Click(object sender, EventArgs e)
    {

    }

    protected void Calendar2_SelectionChanged(object sender, EventArgs e)
    {
        string date = Calendar2.SelectedDate.ToString("dd/MM/yyyy");
        if (GridView1.EditIndex != -1)
        {
            //int indx = ((GridViewRow)(sender as Control).NamingContainer).RowIndex;

            // GridView1.Rows[0].Cells[5].FindControl("date_edite")= date;
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("customers.aspx");
    }



    protected void TXT_ID_TextChanged(object sender, EventArgs e)
    {

    }
}