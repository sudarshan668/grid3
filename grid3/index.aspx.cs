using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace grid3
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GridLoad();
        }


        public void GridLoad() 
        {
            using (SqlConnection con = new SqlConnection("Data Source=SID;Initial Catalog=testing;Integrated Security=True"))
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select * from Employee1",con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                con.Close();
            }
        
        }
       
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            Button2.Visible = true;
          //int a= Convert.ToInt32(GridView1.DataKeys[GridView1.SelectedIndex].Value.ToString());
            int a = Convert.ToInt32(GridView1.SelectedDataKey.Value.ToString());
            using (SqlConnection con = new SqlConnection("Data Source=SID;Initial Catalog=testing;Integrated Security=True"))
            {
                con.Open();
                SqlCommand sda = new SqlCommand("select * from Employee1 where id='"+a+"' ", con);
                SqlDataReader dr = sda.ExecuteReader();
                if (dr.Read())
                {
                    Name.Text = dr["Name"].ToString();
                    LastName.Text = dr["LastName"].ToString();
                }
                con.Close();

            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
                  int a = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            
            using (SqlConnection con = new SqlConnection("Data Source=SID;Initial Catalog=testing;Integrated Security=True"))
            {
                con.Open();
                SqlCommand sda = new SqlCommand("delete  Employee1 where id='" + a + "' ", con);
                sda.ExecuteNonQuery();

                con.Close();

            }
            GridLoad();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            int a = Convert.ToInt32( GridView1.SelectedDataKey.Value.ToString());
            using (SqlConnection con = new SqlConnection("Data Source=SID;Initial Catalog=testing;Integrated Security=True"))
            {
                con.Open();
                SqlCommand sda = new SqlCommand("update Employee1 set Name=@Name,LastName=@LastName where id='" + a + "' ", con);
                sda.Parameters.AddWithValue("@Name",Name.Text);
                sda.Parameters.AddWithValue("@LastName", LastName.Text);
                 sda.ExecuteNonQuery();
                 con.Close();

            }
            GridLoad();
            Button2.Visible = false;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Name.Text = "";
            LastName.Text = "";
            Button2.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Data Source=SID;Initial Catalog=testing;Integrated Security=True"))
            {
                con.Open();
                SqlCommand sda = new SqlCommand("insert into  Employee1 values(@Name,@LastName)", con);
                sda.Parameters.AddWithValue("@Name", Name.Text);
                sda.Parameters.AddWithValue("@LastName", LastName.Text);
                sda.ExecuteNonQuery();
                con.Close();

            }
            GridLoad();
             Name.Text = "";
            LastName.Text = "";
            Button2.Visible = false;
        }

      //you are doing great
    }
}