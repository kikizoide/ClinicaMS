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
    public partial class LabTests : Form
    {
        public LabTests()
        {
            InitializeComponent();
            DisplayTest();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\053\Documents\ClinicDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplayTest()
        {
            Con.Open();
            string Query = "select * from TestTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            LabTestsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void Clear()
        {
            LabTestTb.Text = "";
            LabCostTb.Text = "";
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Doctors obj = new Doctors();
            obj.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (LabCostTb.Text == "" || LabTestTb.Text == "")
            {
                MessageBox.Show("Informações inválidas.");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into TestTbl(TestName,TestCost)values(@TN,@TC)", Con);
                    cmd.Parameters.AddWithValue("@TN", LabTestTb.Text);
                    cmd.Parameters.AddWithValue("@TC", LabCostTb.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Teste adicionado com sucesso.");
                    Con.Close();
                    DisplayTest();
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
            Patients obj = new Patients();
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
