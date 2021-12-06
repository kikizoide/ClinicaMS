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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\053\Documents\ClinicDb.mdf;Integrated Security=True;Connect Timeout=30");


        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if(RoleCb.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione um tipo de usuário.");
            }else if(RoleCb.SelectedIndex == 0)
            {
                if (UnameTb.Text == "" || PassTb.Text == "")
                {
                    MessageBox.Show("Usuário e senha inválidos (ADMIN).");

                }else if(UnameTb.Text == "Admin" && PassTb.Text == "Password")
                {
                    Homes obj = new Homes();
                    obj.Show();
                    this.Hide();
                }else
                {
                    MessageBox.Show("Usuário e senha inválidos (ADMIN).");
                }
               
            }else if(RoleCb.SelectedIndex == 1)
            {
                if (UnameTb.Text == "" || PassTb.Text == "")
                {
                    MessageBox.Show("Usuário e senha inválidos (MÉDICO).");

                }
                else
                {
                    Con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from DoctorTbl where DocName='" + UnameTb.Text + "' and DocPass='" + PassTb.Text + "'", Con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        Prescriptions obj = new Prescriptions();
                        obj.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Médico não encontrado");
                    }
                    Con.Close();
                }
                
            }
            else
            {
                if (UnameTb.Text == "" || PassTb.Text == "")
                {
                    MessageBox.Show("Usuário e senha inválidos (RECEPCIONISTA).");

                }
                else
                {
                    Con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from ReceptionistTbl where RecepName='" + UnameTb.Text + "' and RecepPass='" + PassTb.Text + "'", Con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        Receptionists obj = new Receptionists();
                        obj.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Recepcionista não encontrado");
                    }
                    Con.Close();
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            RoleCb.SelectedIndex = 0;
            UnameTb.Text = "";
            PassTb.Text = "";
        }
    }
}
