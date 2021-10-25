
using System;
using System.Data;
using FacialAI.Azure;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using System.Collections.Generic;

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
            string login = "SELECT username FROM tblUsers WHERE username= '" + txtusername.Text.Trim() + "' and password= '" + txtpassword.Text + "'";
            List<Object> reader = dbs.read(login);

            if (reader.Count > 0)
            {
                UserProfile profile = new UserProfile(this, txtusername.Text.Trim());
                this.Hide();
                parentHome.SetUsername(txtusername.Text.Trim());
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
            this.Close();
        }

        private void FaceAILogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            parentHome.Show();
        }
    }

}
