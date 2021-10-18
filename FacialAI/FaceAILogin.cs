using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace FacialAI
{
    public partial class FaceAILogin : Form
    {
        public FaceAILogin()
        {
            InitializeComponent();
        }



        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\chgar\source\repos\CSC6221-FaceAI\FacialAI\bin\Debug\DatabaseFaceAI.accdb");
            string query = "SELECT * FROM tbl_users WHERE username= '" + txtusername.Text.Trim() + "' and password= '" + txtpassword.Text.Trim() + "'";
            OleDbDataAdapter dataA = new OleDbDataAdapter(query, con);
            DataTable dtbl = new DataTable();

            dataA.Fill(dtbl);
            if (dtbl.Rows.Count == 1)
            {
               UserProfile profile = new UserProfile();
               this.Hide();
               profile.Show();


            }
            else
            {
                MessageBox.Show("Invalid Username and Password1", "Please Try Again", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtpassword.Text = "";
                txtpassword.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtusername.Text = "";
            txtpassword.Text = "";
            txtusername.Focus();
        }

        private void checkbxShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbxShowPass.Checked)
            {
                txtpassword.PasswordChar = '\0';

            }
            else
            {
                txtpassword.PasswordChar = '•';

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new frm_home().Show();
            Hide();
        }

        private void FaceAILogin_Load(object sender, EventArgs e)
        {
          
        }

        private void FaceAILogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            new frm_home().Show();
            this.Hide();
        }
    }

}
