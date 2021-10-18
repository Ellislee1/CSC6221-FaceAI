
using System;
using System.Data;
using FacialAI.Azure;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace FacialAI
{
    public partial class FaceAILogin : Form
    {
        DataBase dbs;
        frmHome parentHome;
        public FaceAILogin(frmHome home)
        {
            parentHome = home;
            InitializeComponent();
            dbs = new DataBase();

        }



        private void btnLoginClick(object sender, EventArgs e)
        {
            string login = "SELECT username FROM tblUsers WHERE username= '" + txtusername.Text + "' and password= '" + txtpassword.Text + "'";
            SqlDataReader reader = dbs.read(login);

            if (reader != null)
            {
               UserProfile profile = new UserProfile();
               this.Hide();
               profile.Show();


            }
            else
            {
                MessageBox.Show("Invalid Username and Password!", "Please Try Again", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtpassword.Text = "";
                txtpassword.Focus();
            }
        }

        private void btnClearClick(object sender, EventArgs e)
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

        private void btnCreateAccountClick(object sender, EventArgs e)
        {
            new frmHome().Show();
            Hide();
        }

        private void FaceAILogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            parentHome.Show();
        }
    }

}
