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
    public partial class Prescriptions : Form
    {
        public Prescriptions()
        {
            InitializeComponent();
            DisplayPrescription();
            GetDocId();
            GetPatId();
            GetTestId();
            Clear();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\053\Documents\ClinicDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplayPrescription()
        {
            Con.Open();
            string Query = "select * from PrescriptionTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            PrescriptionDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void Clear()
        {
            DocIdCb.SelectedIndex = 0;
            PatIdCb.SelectedIndex = 0;
            TestIdCb.SelectedIndex = 0;
            CostTb.Text = "";
            MedTb.Text = "";
            DocNameTb.Text = "";
            PatNameTb.Text = "";
            TestNameTb.Text = "";
        }

        private void GetDocName()
        {
            Con.Open();
            string Query = "Select * from DoctorTbl where DocId=" + DocIdCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                DocNameTb.Text = dr["DocName"].ToString();
            }
            Con.Close();
        }
        private void GetPatName()
        {
            Con.Open();
            string Query = "Select * from PatientTbl where PatId=" + PatIdCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                PatNameTb.Text = dr["PatName"].ToString();
            }
            Con.Close();
        }
        private void GetTest()
        {
            Con.Open();
            string Query = "Select * from TestTbl where TestNum=" + TestIdCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                TestNameTb.Text = dr["TestName"].ToString();
                CostTb.Text = dr["TestCost"].ToString();
            }
            Con.Close();
        }

        private void GetDocId()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select DocId from DoctorTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("DocId", typeof(int));
            dt.Load(rdr);
            DocIdCb.ValueMember = "DocId";
            DocIdCb.DataSource = dt;
            Con.Close();
        }

        private void GetPatId()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select PatId from PatientTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("PatId", typeof(int));
            dt.Load(rdr);
            PatIdCb.ValueMember = "PatId";
            PatIdCb.DataSource = dt;
            Con.Close();
        }

        private void GetTestId()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select TestNum from TestTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("TestNum", typeof(int));
            dt.Load(rdr);
            TestIdCb.ValueMember = "TestNum";
            TestIdCb.DataSource = dt;
            Con.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DocIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetDocName();
        }

        private void PatIdCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PatIdCb_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            GetPatName();
        }

        private void TestIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetTest();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (PatNameTb.Text == "" || DocNameTb.Text == "" || TestNameTb.Text == "")
            {
                MessageBox.Show("Informações inválidas.");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into PrescriptionTbl(DocId,DocName,PatId,PatName,LabTestId,LabTestName,Medicines,Cost)values(@DI,@DN,@PI,@PN,@TI,@TN,@Med,@Co)", Con);
                    cmd.Parameters.AddWithValue("@DI", DocIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@DN", DocNameTb.Text);
                    cmd.Parameters.AddWithValue("@PI", PatIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@PN", PatNameTb.Text);
                    cmd.Parameters.AddWithValue("@TI", TestIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@TN", TestNameTb.Text);
                    cmd.Parameters.AddWithValue("@Med", MedTb.Text);
                    cmd.Parameters.AddWithValue("@Co", CostTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Prescrição adicionada com sucesso.");
                    Con.Close();
                    DisplayPrescription();
                    Clear();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void PrescriptionDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PrescSumTxt.Text = "";
            PrescSumTxt.Text = "                                 Clínica Atos\n\n" + "                                  PRESCRIÇÃO                   " + "\n*******************************************************************" + "\n" + DateTime.Today.Date + "\n\n\n\n        Doutor: " + PrescriptionDGV.SelectedRows[0].Cells[2].Value.ToString() + "             Paciente: " + PrescriptionDGV.SelectedRows[0].Cells[4].Value.ToString() + "\n\n\n\n             Teste: " + PrescriptionDGV.SelectedRows[0].Cells[6].Value.ToString() + "             " + "       Medicamentos: " + PrescriptionDGV.SelectedRows[0].Cells[7].Value.ToString() + "\n\n\n\n              ";

        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            if(printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(PrescSumTxt.Text + "\n", new Font("Arial", 18, FontStyle.Regular), Brushes.Black, new Point(95, 80));
            e.Graphics.DrawString("\n\t" + "Obrigado", new Font("Arial", 18, FontStyle.Bold), Brushes.Red, new Point(200, 300));
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Patients obj = new Patients();
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

        private void label5_Click(object sender, EventArgs e)
        {
            Prescriptions obj = new Prescriptions();
            obj.Show();
            this.Hide();
        }
    }
}
