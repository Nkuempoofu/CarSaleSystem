using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string uname = txtuname.Text; // assigning the text on the textbox to the uname
            string pass = txtpass.Text;

            if(uname.Equals("admin") && pass.Equals("1010"))
            {
                Main m = new Main(); // successful login takes you to the main form
                this.Hide();          // hides the current form and Show - the main form
                m.Show();
            }
            else
            {
                MessageBox.Show("Username or password do not match");
                txtuname.Clear();
                txtpass.Clear(); // clear the text fields after a failed log in
                txtuname.Focus();
            }
        }
    }
}
