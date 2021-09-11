using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CarRentalSystem
{
    public partial class CarReg : Form
    {
        public CarReg()
        {
            InitializeComponent();
            Autono();
            load();
        }

        SqlConnection con = new SqlConnection("Data Source=.; Initial Catalog= carsale; User ID=sa; Password=123; ");
        SqlCommand cmd;
        SqlDataReader dr;
        string proid;
        string sql;
        bool Mode = true;
        string id;

        public void Autono()
        {
            sql = "select regno from carsales order by regno desc";
            cmd = new SqlCommand(sql,con);
            con.Open();
            dr = cmd.ExecuteReader();

            if(dr.Read())
            {
                int id = int.Parse(dr[0].ToString()) + 1;
                proid = id.ToString("00000");
            }
            else if(Convert.IsDBNull(dr))
            {
                proid = ("00000");
            }
            else
            {
                proid = ("00000");
            }


            txtregno.Text = proid.ToString();

            con.Close();
        }

        public void load()
        {
            sql = "select * from carsales";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();

            while(dr.Read())
            {
                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3]);
            }

            con.Close();
        }

        public void getid(String id)
        {
            sql = "select * from carsales where regno = '" + id + "' ";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();


            while(dr.Read())
            {
                txtregno.Text = dr[0].ToString();
                txtname.Text = dr[1].ToString();
                txtbrand.Text = dr[2].ToString();
                txtprice.Text = dr[3].ToString();
                txtyear.Text = dr[4].ToString();
                txtmodel.Text = dr[5].ToString();
                txtavl.Text = dr[6].ToString();
            }
            con.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string regno = txtregno.Text;
            string name = txtname.Text;
            string brand = txtbrand.Text;
            string price = txtprice.Text;
            string year = txtyear.Text;
            string model = txtmodel.Text;
            string available = txtavl.SelectedItem.ToString();

            //id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            if (Mode == true)
                {

                sql = "insert into carsales(regno,name,brand,price,year,model,available)values(@regno,@name,@brand,@price,@year,@model,@available)";
                con.Open();
                cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@regno", regno);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@brand", brand);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@year", year);
                cmd.Parameters.AddWithValue("@model", model);
                cmd.Parameters.AddWithValue("@available", available);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Successfully Added");

                txtname.Clear();
                txtbrand.Clear();
                txtprice.Clear();
                txtyear.Clear();
                txtmodel.Clear();
                txtname.Focus();
            }
            else
            {
                sql = "update carsales set brand= @brand,model=@model,available=@available where regno = @regno";
                con.Open();
                cmd = new SqlCommand(sql, con);
               
                cmd.Parameters.AddWithValue("@brand", brand);
                cmd.Parameters.AddWithValue("@model", model);
                cmd.Parameters.AddWithValue("@available", available);
                cmd.Parameters.AddWithValue("@regno", regno);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Successfully Updated");
                txtregno.Enabled = true;
                Mode = true;

                txtname.Clear();
                txtbrand.Clear();
                txtprice.Clear();
                txtyear.Clear();
                txtmodel.Clear();
                txtname.Focus();
            }

            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex==dataGridView1.Columns["update"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                txtregno.Enabled = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                getid(id);
            }
            else if (e.ColumnIndex == dataGridView1.Columns["delete"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                sql = "delete from carsales where regno = @id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Successfully Deleted");
                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            load();
            Autono();
            txtname.Clear();
            txtbrand.Clear();
            txtprice.Clear();
            txtyear.Clear();
            txtmodel.Clear();
            txtname.Focus();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
