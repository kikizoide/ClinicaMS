using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace ClinicaMS
{
    public partial class Receptionists : Form
    {
        public Receptionists()
        {
            InitializeComponent();
            DisplayRec();
        }
SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\053\Documents\ClinicDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void DisplayRec()
        {
            Con.Open();
            string Query = "select * from ReceptionistTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ReceptionistDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if(RNameTb.Text == "" || RPasswordTb.Text == "" || RPhoneTb.Text == "" || RAdressTb.Text == "")
            {
                MessageBox.Show("Informações inválidas.");
            }else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ReceptionistTbl(RecepName,RecepPhone,RecepAdd,RecepPass)values(@RN,@RP,@RA,@RPA)", Con);
                    cmd.Parameters.AddWithValue("@RN", RNameTb.Text);
                    cmd.Parameters.AddWithValue("@RP", RPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@RA", RAdressTb.Text);
                    cmd.Parameters.AddWithValue("@RPA", RPasswordTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Recepcionista adicionado com sucesso.");
                    Con.Close();
                    DisplayRec();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Informações inválidas.");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from ReceptionistTbl where RecepId=@RKey", Con);
                    cmd.Parameters.AddWithValue("@RKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Recepcionista excluído com sucesso.");
                    Con.Close();
                    DisplayRec();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (RNameTb.Text == "" || RPasswordTb.Text == "" || RPhoneTb.Text == "" || RAdressTb.Text == "")
            {
                MessageBox.Show("Informações inválidas.");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update ReceptionistTbl set RecepName=@RN, RecepPhone=@RP, RecepAdd=@RA, RecepPass=@RPA where RecepId=@RKey", Con);
                    cmd.Parameters.AddWithValue("@RN", RNameTb.Text);
                    cmd.Parameters.AddWithValue("@RP", RPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@RA", RAdressTb.Text);
                    cmd.Parameters.AddWithValue("@RPA", RPasswordTb.Text);
                    cmd.Parameters.AddWithValue("@RKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Recepcionista editado com sucesso.");
                    Con.Close();
                    DisplayRec();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        int Key = 0;
        private void ReceptionistDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RNameTb.Text = ReceptionistDGV.SelectedRows[0].Cells[1].Value.ToString();
            RPhoneTb.Text = ReceptionistDGV.SelectedRows[0].Cells[2].Value.ToString();
            RAdressTb.Text = ReceptionistDGV.SelectedRows[0].Cells[3].Value.ToString();
            RPasswordTb.Text = ReceptionistDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (RNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ReceptionistDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void RNameTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void RPhoneTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void RPasswordTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void RAdressTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
            Homes obj = new Homes();
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
