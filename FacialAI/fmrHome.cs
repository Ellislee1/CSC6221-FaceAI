using AForge.Video;
using AForge.Video.DirectShow;
using FacialAI.Azure;
using System;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace FacialAI
{
    public partial class frmHome : Form
    {

        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice captureDevice;
        readonly OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DatabaseFaceAI.mdb");
        OleDbCommand cmd = new OleDbCommand();
        readonly OleDbDataAdapter da = new OleDbDataAdapter();

        Bitmap capturedImage;
        readonly FaceModels model;

        DataBase dbs;
        public frmHome()
        {
            InitializeComponent();

            model = new FaceModels();

            imageControl.SizeMode = PictureBoxSizeMode.StretchImage;
            pct_snapshot.SizeMode = PictureBoxSizeMode.StretchImage;
            dbs = new DataBase();

        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbxShowPass.Checked)
            {
                txtPassword.PasswordChar = '\0';
                txtComPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '•';
                txtComPassword.PasswordChar = '•';
            }
        }

        private void btnNewAccountClick(object sender, EventArgs e)
        {
            if (txtUsername.Text == "" && txtPassword.Text == "" && txtComPassword.Text == "")
            {
                MessageBox.Show("Username and Password fields are empty", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtPassword.Text == txtComPassword.Text)
            {
                con.Open();
                string query = "INSERT INTO tblUsers (username, password) VALUES ('" + txtUsername.Text + "','" + txtPassword.Text + "')";
                bool success = dbs.Insert(query);
                txtUsername.Text = "";
                txtPassword.Text = "";
                txtComPassword.Text = "";

                MessageBox.Show("Your Account has been Successfully Created", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Passwords does not match, Please Re-enter", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Text = "";
                txtComPassword.Text = "";
                txtPassword.Focus();
            }
        }

        private void btnClearClick(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtComPassword.Text = "";
            txtUsername.Focus();
        }

        private void btnLoginClick(object sender, EventArgs e)
        {
            new FaceAILogin(this).Show();
            Hide();
        }

        private void btnTakePicture_Click(object sender, EventArgs e)
        {
            capturedImage = (Bitmap)imageControl.Image.Clone();
            pct_snapshot.Image = capturedImage;
        }

        private void frmHomeLoad(object sender, EventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filter in filterInfoCollection)
            {
                cboCameras.Items.Add(filter.Name);
            }

            cboCameras.SelectedIndex = 0;
            captureDevice = new VideoCaptureDevice();

            if (captureDevice != null)
            {
                if (captureDevice.IsRunning == true)
                    captureDevice.Stop();
                captureDevice = new VideoCaptureDevice(filterInfoCollection[cboCameras.SelectedIndex].MonikerString);
                captureDevice.NewFrame += VideoCaptureDevice_NewFrame;
                captureDevice.Start();
            }
        }

        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            imageControl.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (captureDevice.IsRunning == true)
            {
                captureDevice.Stop();
            }
        }

        private void cboCameras_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (captureDevice != null)
            {
                if (captureDevice.IsRunning == true)
                    captureDevice.Stop();
                captureDevice = new VideoCaptureDevice(filterInfoCollection[cboCameras.SelectedIndex].MonikerString);
                captureDevice.NewFrame += VideoCaptureDevice_NewFrame;
                captureDevice.Start();
            }
        }

        private async void btnCompare_ClickAsync(object sender, EventArgs e)
        {
            bool to_save = chkSaveImage.Checked;
            bool val = await model.FindSimilar(capturedImage, to_save);
        }
    }
}
