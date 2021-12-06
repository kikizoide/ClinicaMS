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
    public partial class Patients : Form
    {
        public Patients()
        {
            InitializeComponent();
            DisplayPat();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\053\Documents\ClinicDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplayPat()
        {
            Con.Open();
            string Query = "select * from PatientTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            PatientsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void Clear()
        {
            PatNameTb.Text = "";
            PatGenCb.SelectedIndex = 0;
            PatCovidCb.SelectedIndex = 0;
            PatAddCb.Text = "";
            PatPhoneTb.Text = "";
            PatAlTb.Text = "";
        }

        private void PatGenCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (PatNameTb.Text == "" || PatAlTb.Text == "" || PatAddCb.Text == "" || PatPhoneTb.Text == "" || PatGenCb.SelectedIndex == -1 || PatCovidCb.SelectedIndex == -1)
            {
                MessageBox.Show("Informações inválidas.");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into PatientTbl(PatName,PatGen,PatDOB,PatAdd,PatPhone,PatCovid,PatAll)values(@PN,@PG,@PD,@PA,@PP,@PH,@PAl)", Con);
                    cmd.Parameters.AddWithValue("@PN", PatNameTb.Text);
                    cmd.Parameters.AddWithValue("@PG", PatGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PD", PatDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@PA", PatAddCb.Text);
                    cmd.Parameters.AddWithValue("@PP", PatPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@PH", PatCovidCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PAl", PatAlTb.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Paciente adicionado com sucesso.");
                    Con.Close();
                    DisplayPat();
                    Clear();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Homes obj = new Homes();
            obj.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Doctors obj = new Doctors();
            obj.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            LabTests obj = new LabTests();
            obj.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Receptionists obj = new Receptionists();
            obj.Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }
    }
}
