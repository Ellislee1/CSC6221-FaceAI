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
using FacialAI.Azure;
using Microsoft.Data.SqlClient;

namespace FacialAI
{
    public partial class UserProfile : Form
    {
        private FaceAILogin login;
        DataBase dbs;
        String username;

        public UserProfile(FaceAILogin login, String username)
        {
            InitializeComponent();
            this.login = login;
            dbs = new DataBase();
            this.username = username;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Console.WriteLine("HERE1------------------------");
            string data = "SELECT username FROM tblUsers WHERE username= '" +username+"'";
            List<object> users = dbs.read(data);
            textBox1.Text = users[0].ToString();
        }

        private void UserProfile_FormClosing(object sender, FormClosingEventArgs e)
        {
            login.Show();
        }
    }


}
