using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace FacialAI
{
    public partial class UserProfile : Form
    {
        public UserProfile()
        {
            InitializeComponent();
        }

        private void UserProfile_Load(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\chgar\source\repos\CSC6221-FaceAI\FacialAI\bin\Debug\DatabaseFaceAI.accdb");
            OleDbDataAdapter dataA = new OleDbDataAdapter("SELECT * FROM tbl_users where username = '" + textBox1.Text + "'", con);
            DataTable dt = new DataTable();
            dataA.Fill(dt);
            textBox1.Text = dt.Rows[0][0].ToString();
            textBox2.Text = dt.Rows[0][1].ToString();
            textBox3.Text = dt.Rows[0][3].ToString();

        }

        private void UserProfile_FormClosing(object sender, FormClosingEventArgs e)
        {
            new frm_home().Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }


}
