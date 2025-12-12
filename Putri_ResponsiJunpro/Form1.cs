using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Putri_ResponsiJunpro
{
    public partial class Form1 : Form
    {
        private NpgsqlConnection conn;
        private string connString = "Host=localhost;Username=postgres;Password=informatika;Database=responsiJunpro";
        private string sql = "";
        private NpgsqlCommand cmd;
        private DataTable dt;
        public Form1()
        {
            InitializeComponent();
        }

        private void EstablishConn(string connstring)
        {
            try
            {
                this.connString = connString;
                conn = new NpgsqlConnection(connString);

                //Test Connection
                conn.Open();
                MessageBox.Show("Connection to PostgreSQL is successful!", "Connection Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to connect to PostgreSQL! Error: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EstablishConn()
        {
            MessageBox.Show("Connection to PostgreSQL is success", "Initializing", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            EstablishConn();
            EstablishConn(connString);
            LoadData();
        }

        private void LoadData()
        { 
            try
            {
                using(var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    sql = "SELECT * FORM dev_select()";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    using (var adapter = new NpgsqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;

                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
