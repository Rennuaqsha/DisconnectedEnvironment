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

namespace Disconnected_Environment
{
    public partial class Form2 : Form
    {
        private string stringConnection = "data source=DESKTOP-6I7FGSP\\NEMBOO;" +
            "database=Activity6PABD; User ID=sa;Password=123";
        private SqlConnection koneksi;
        public Form2()
        {
            InitializeComponent();
            koneksi = new SqlConnection(stringConnection);
            refreshfrom();
        }
        private void refreshfrom()
        {
            nmp.Text = "";
            nmp.Enabled = false;
            txtId.Text = "";
            txtId.Enabled = false;
            btnSave.Enabled = false;
            btnClear.Enabled = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void nmpText_TextChanged(object sender, EventArgs e)
        {

        }
        
        private void dataGridView()
        {
            koneksi.Open();
            string str = "Select nama_prodi, id_prodi from dbo.Prodi";
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            koneksi.Close();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            dataGridView();
            btnOpen.Enabled = false ;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            nmp.Enabled = true;
            txtId.Enabled = true;
            btnSave.Enabled = true;
            btnClear.Enabled = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            refreshfrom();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string nmProdi = nmp.Text;
            string idProdi = txtId.Text;

            if (nmProdi == "")
            {
                MessageBox.Show("Masukkan Nama Prodi", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } 
            else
            {
                koneksi.Open();
                string str = "insert into dbo.prodi (nama_prodi, id_prodi)" + "values(@nm,@id)";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("id", idProdi));
                cmd.Parameters.Add(new SqlParameter("nm", nmProdi));

                cmd.ExecuteNonQuery();

                koneksi.Close();
                MessageBox.Show("Data Berhasil Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView();
                refreshfrom();

            }
        }
        private void Form2_FormsClosed(object sender, FormClosedEventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

    }
}
