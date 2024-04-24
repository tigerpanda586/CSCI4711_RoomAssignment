using RoomScheduling.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoomScheduling
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usr = textBox1.Text;
            string pass = textBox2.Text;
            VerifyControl verifycontrol = new VerifyControl();
            bool verify = verifycontrol.login(usr, pass);
            if (verify)
            {
                this.Close();
                string role = DBConnector.GetUser(usr).getRole();
                if (role == "student")
                {
                    this.Close();
                    StudentForm studentform = new StudentForm(usr);
                    studentform.ShowDialog();
                }
                else
                {
                    this.Close();
                    AdminForm adminform = new AdminForm();
                    adminform.ShowDialog();
                }

            }
            else
            {
                MessageBox.Show("Invalid Username or Password");
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
