using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Class1
/// </summary>
public class database
{
    public SqlCommand com = new SqlCommand();
    public DataSet ds1 = new DataSet();
    public SqlConnection conn = new SqlConnection();
    public DataTable dt = new DataTable();
    public void insert(string insert)
    {

        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["dental"].ToString();
        if (conn.State != ConnectionState.Open)
        {
            conn.Open();
        }
        try
        {
            SqlCommand com = new SqlCommand();
            com = conn.CreateCommand();
            com.CommandType = CommandType.Text;
            com.CommandText = insert;
            com.ExecuteNonQuery();
            Console.WriteLine("Done");
            conn.Close();
        }
        catch
        {
            Console.WriteLine("false");
        }
    }

    public DataSet fill_drop_grid (string query, DropDownList drp_casting, string first_item_str)
    {
        try
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["dental"].ToString();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            SqlCommand com = new SqlCommand();
            com = conn.CreateCommand();
            com.CommandType = CommandType.Text;
            com.CommandText = query;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            
            drp_casting.DataTextField = ds.Tables[0].Columns["name"].ToString(); // text field name of table dispalyed in dropdown       
            drp_casting.DataValueField = ds.Tables[0].Columns["id"].ToString();
            drp_casting.DataSource = ds.Tables[0];
            drp_casting.DataBind();
            //drp_casting.Items.Add(first_item_str);
            drp_casting.Items.Insert(0, first_item_str);
            conn.Close();
            return ds;
        }
        catch
        {
            Console.WriteLine("false");
            return null;
        }

    }
    public void select_combo(string query, DropDownList cmb)
    {
        try
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["dental"].ToString();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            SqlCommand com = new SqlCommand();
            com = conn.CreateCommand();
            com.CommandType = CommandType.Text;
            com.CommandText = query;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmb.DataTextField = ds.Tables[0].Columns["name"].ToString(); // text field name of table dispalyed in dropdown       
            cmb.DataValueField = ds.Tables[0].Columns["id"].ToString();
            cmb.DataSource = ds.Tables[0];
            cmb.DataBind();
            conn.Close();
        }
        catch
        {
            Console.WriteLine("false");
        }
    }



    // to get any single value from database
    public string select_value(string query, string where_db, string col_db)
    {
        try
        {
            string ret;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["dental"].ToString();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            SqlCommand com = new SqlCommand();
            //DataTable dt = new DataTable(); 
            //com = conn.CreateCommand(); // this command has the same effect of "com.Connection = conn"
            com.Connection = conn;
            com.CommandType = CommandType.Text;
            com.CommandText = query;
            SqlDataReader dr = com.ExecuteReader();
            dr.Read();
            ret = dr[col_db].ToString();
            conn.Close();

            return ret;

        }
        catch
        {
            string ret = "";
            Console.WriteLine("false");
            return ret;
        }
    }


    public string select_value(string query, string col_db)
    {
        try
        {
            string ret;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["dental"].ToString();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            SqlCommand com = new SqlCommand();
            //DataTable dt = new DataTable(); 
            //com = conn.CreateCommand(); // this command has the same effect of "com.Connection = conn"
            com.Connection = conn;
            com.CommandType = CommandType.Text;
            com.CommandText = query;
            SqlDataReader dr = com.ExecuteReader();
            dr.Read();
            ret = dr[col_db].ToString();
            conn.Close();

            return ret;

        }
        catch
        {
            string ret = "";
            Console.WriteLine("false");
            return ret;
        }
    }

    public void select_id(string query, TextBox txt)
    {
        try
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["dental"].ToString();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            SqlCommand com = new SqlCommand();
            //DataTable dt = new DataTable(); 
            //com = conn.CreateCommand(); // this command has the same effect of "com.Connection = conn"
            com.Connection = conn;
            com.CommandType = CommandType.Text;
            com.CommandText = query;
            SqlDataReader dr = com.ExecuteReader();
            dr.Read();
            txt.Text = dr["id"].ToString();
            conn.Close();

        }
        catch
        {
            Console.WriteLine("false");
        }
    }

    public void select(string query, GridView gr)
    {
        try
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["dental"].ToString();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            SqlCommand com = new SqlCommand();
            DataTable dt = new DataTable();
            ds1 = new DataSet();
            com = conn.CreateCommand();
            com.CommandType = CommandType.Text;
            com.CommandText = query;
            com.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(com);
            //da.Fill(dt);
            //gr.DataSource = dt;
            //gr.DataBind();
            da.Fill(ds1);
            gr.DataSource = ds1.Tables[0];
            gr.DataBind();
            conn.Close();
        }
        catch
        {
            Console.WriteLine("false");
        }
    }

    public void Updating_grid(DataSet ds, string update, GridView gr, string[] dataset_cell, string[] grid_cell, params SqlParameter[] pars)
    {
        try
        {

            // update dataset
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["dental"].ToString();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            SqlCommand com = new SqlCommand();
            com.Connection = conn;
            SqlDataAdapter da = new SqlDataAdapter(com);
            //ds.Tables[0].Rows[row_index_grid][0] = gr.Rows[0].Cells[row_index_grid];
            for (int i = 0; i < dataset_cell.Length; i++)
                dataset_cell[i] = grid_cell[i];

            gr.DataSource = ds.Tables[0];
            gr.DataBind();
            conn.Close();

            //update database
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            com = new SqlCommand();
            com.Connection = conn;
            com.CommandType = CommandType.Text;
            com.CommandText = update;
            //com.CommandText = "UPDATE appointments SET date = @date WHERE id = @";
            //SqlParameter param = new SqlParameter("@id", SqlDbType.Int, 4, "id");

            for (int i = 0; i < pars.Length; i++)
            {
                com.Parameters.Add(pars[i]);
            }

            da.UpdateCommand = com;
            da.Update(ds);
            conn.Close();
        }
        catch
        {
            Console.WriteLine("false");
        } 
    }



    public void deleting_grid(DataSet ds, string table_name_database, GridView gr, int row_index_grid)
    {
        try
        {
            
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["dental"].ToString();
            
            // delete from dataset
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlCommand com = new SqlCommand();
            com.Connection = conn;
            SqlDataAdapter da = new SqlDataAdapter(com);
            ds.Tables[0].Rows[row_index_grid].Delete();
            gr.DataSource = ds1.Tables[0];
            gr.DataBind();
            conn.Close();

            // detelet from database
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            com = new SqlCommand();

            com.Connection = conn;
            com.CommandType = CommandType.Text;
            com.CommandText = "DELETE FROM " + table_name_database + " WHERE id =@id";
            SqlParameter param = new SqlParameter("@id", SqlDbType.Int, 4, "id");
            com.Parameters.Add(param);
            da.DeleteCommand = com;
            da.Update(ds);
            conn.Close();
        }
        catch
        {
            Console.WriteLine("false");
        }
    }
    public void select(string query, params TextBox[] txt)
    {
        try
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["dental"].ToString();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            SqlCommand com = new SqlCommand();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            com = conn.CreateCommand();
            com.CommandType = CommandType.Text;
            com.CommandText = query;
            com.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(com);
            da.Fill(dt);
            conn.Close();
        }
        catch
        {
            Console.WriteLine("false");
        }
    }


    public void deleting_grid_where_plus(DataSet ds, string table_name_database, GridView gr, int row_index_grid, int [] datatype_size,SqlDbType[] sqldatatype , params string[] cond)
    {
        try
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["dental"].ToString();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            SqlCommand com = new SqlCommand();
            com.Connection = conn;
            SqlDataAdapter da = new SqlDataAdapter(com);
            ds.Tables[0].Rows[row_index_grid].Delete();
            gr.DataSource = ds1.Tables[0];
            gr.DataBind();
            conn.Close();


            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            com = new SqlCommand();

            com.Connection = conn;
            com.CommandType = CommandType.Text;
            SqlParameter param = new SqlParameter();
            for (int c=0 ; c > cond.Length;c++)
            {
                com.CommandText = "DELETE FROM " + table_name_database + " WHERE " + cond[c] + " = @ "+cond[c]+" ";
                param =new SqlParameter ("@"+cond[c]+"", sqldatatype[c], datatype_size[c], cond[c]);
                com.Parameters.Add(param);
            }
            
            
            
            da.DeleteCommand = com;
            da.Update(ds);
            conn.Close();
        }
        catch
        {
            Console.WriteLine("false");
        }
    }

    /*
    public void disp(string dis, GridView ob)
    {
        string con = "Data Source=.; Initial Catalog=Emp;Integrated Security=True;User ID=sa, password=P@$$w0rd";
        SqlConnection conn = new SqlConnection(con);
        if (conn.State != ConnectionState.Open)
        {
            conn.Open();
        }
        else
        {
            conn.Open();
        }


        try
        {
            SqlCommand com = new SqlCommand();
            DataTable dt = new DataTable();
            com = conn.CreateCommand();
            com.CommandType = CommandType.Text;
            com.CommandText = dis;
            com.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(com);
            da.Fill(dt);
            //Database_1 ds = new Database_1();
            ob.DataSource = dt;
            ob.DataBind();
        }
        catch
        {
            Console.WriteLine("false");
        }
    }

    */
    public void delete(string ins)
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["dental"].ToString();
        if (conn.State != ConnectionState.Open)
        {
            conn.Open();
        }
        try
        {
            SqlCommand com = new SqlCommand();
            com = conn.CreateCommand();
            com.CommandType = CommandType.Text;
            com.CommandText = ins;
            com.ExecuteNonQuery();
            conn.Close();

        }
        catch
        {
            Console.WriteLine("false");

        }


    }

    public void update(string update)
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["dental"].ToString();
        if (conn.State != ConnectionState.Open)
        {
            conn.Open();
        }
        try
        {
            SqlCommand com = new SqlCommand();
            com = conn.CreateCommand();
            com.CommandType = CommandType.Text;
            com.CommandText = update;
            com.ExecuteNonQuery();
            conn.Close();

        }
        catch (Exception x)
        {

            Console.WriteLine(x);

        }


    }
    public string con;

    public void Connection(string con)
    {
        SqlConnection conn = new SqlConnection(con);
        conn.Open();
    }
    public bool Conncheck(string con)
    {
        SqlConnection conn = new SqlConnection(con);
        conn.Open();
        if (conn.State == System.Data.ConnectionState.Open)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}