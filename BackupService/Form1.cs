using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackupService
{
    public partial class Form1 : RexaForm
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "SQL Server Backup File|*.bak";
            if (sfd.ShowDialog() == DialogResult.OK)
                textBox4.Text = sfd.FileName;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string conn;
                switch (comboBox1.Text)
                {
                    case "Windows Authentication":
                        conn = $"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog={comboBox2.Text};Data Source={textBox1.Text}";
                        break;
                    case "SQL Server Authentication":
                        conn = $"Password={textBox3.Text};Persist Security Info=True;User ID={textBox2.Text};Initial Catalog={comboBox2.Text};Data Source={textBox1.Text}";
                        break;
                    default:
                        conn = string.Empty;
                        break;
                }
                SqlConnection con = new SqlConnection();
                con.ConnectionString = conn;
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = $"BACKUP DATABASE [{comboBox2.Text}] TO  DISK = N'{textBox4.Text}' WITH NOFORMAT, NOINIT, NAME = N'{comboBox2.Text}-Full Database Backup', SKIP, NOREWIND, NOUNLOAD, STATS = 1";
                com.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("There was something wrong");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("CMD:" + "\r\n" + "OSQL -L" + "\r\n" + "sqllocaldb i" + "\r\n" + "\r\n" + "You can create an UDL for connection test");
        }
    }
}
