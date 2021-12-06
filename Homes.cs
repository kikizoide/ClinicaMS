using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaMS
{
    public partial class Homes : Form
    {
        public Homes()
        {
            InitializeComponent();
            CountPatients();
            CountDoctors();
            CountLabTests();
            CountCovid();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\053\Documents\ClinicDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void CountPatients()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select count(*) from PatientTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            PatNumLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountDoctors()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select count(*) from DoctorTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DocNumLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountLabTests()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select count(*) from TestTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            LabNumLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountCovid()
        {
            string Status = "Positivo";
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select count(*) from PatientTbl where PatCovid='"+Status+"'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CovidLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LogoutLbl_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void RecepLbl_Click(object sender, EventArgs e)
        {
            Receptionists obj = new Receptionists();
            obj.Show();
            this.Hide();
        }

        private void LabLbl_Click(object sender, EventArgs e)
        {
            LabTests obj = new LabTests();
            obj.Show();
            this.Hide();
        }

        private void DoctorLbl_Click(object sender, EventArgs e)
        {
            Doctors obj = new Doctors();
            obj.Show();
            this.Hide();
        }

        private void PatientLbl_Click(object sender, EventArgs e)
        {
            Patients obj = new Patients();
            obj.Show();
            this.Hide();
        }

        private void PatNumLbl_Click(object sender, EventArgs e)
        {

        }
    }
}
