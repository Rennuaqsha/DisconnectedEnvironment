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
    public partial class Form3 : Form
    {
        private string stringConnection = "data source=DESKTOP-6I7FGSP\\NEMBOO;database=Activity6PABD;User ID=sa; Password=123";
        private SqlConnection koneksi;
        private string nim, nama, alamat, jk, prodi;
        private DateTime tgl;
        BindingSource costumersBindingSource = new BindingSource();
        public Form3()
        {
            InitializeComponent();
            koneksi = new SqlConnection();
            
        }

        private void FormDataMahasiswa_Load()
        {
            koneksi.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(new SqlCommand("Select m.nim, m.nama_mahasiswa," 
            + "m.alamat, m.jenis_kel, m.tgl_lahir, p.nama_prodi from dbo.Mahasiwa " +
            "join dbo.prodi p on m.id_prodi = p.id_prodi", koneksi));
            DataSet ds = new DataSet(); 
            dataAdapter.Fill(ds);

            this.costumersBindingSource.DataSource = ds.Tables[0];
            this.txtNIM.DataBindings.Add(
                new Binding("Text", this.costumersBindingSource, "Nim", true));
            this.txtNama.DataBindings.Add(
                new Binding("Text", this.costumersBindingSource, "Nama_Mahasiswa", true));
            this.txtAlamat.DataBindings.Add(
                new Binding("Text", this.costumersBindingSource, "Alamat", true));
            this.cbxJenisKelamin.DataBindings.Add(
                new Binding("Text", this.costumersBindingSource, "Jenis_kel", true));
            this.dtTanggalLahir.DataBindings.Add(
                new Binding("Text", this.costumersBindingSource, "tgl_lahir", true));
            this.cbxProdi.DataBindings.Add(
                new Binding("Text", this.costumersBindingSource, "nama_prodi", true));
            koneksi.Close();    
        }

        private void clearBinding()
        {
            this.txtNIM.DataBindings.Clear();
            this.txtNama.DataBindings.Clear();
            this.txtAlamat.DataBindings.Clear();
            this.cbxJenisKelamin.DataBindings.Clear();
            this.dtTanggalLahir.DataBindings.Clear();
            this.cbxProdi.DataBindings.Clear();
        }

        private void refreshform()
        {
            txtNIM.Enabled = false;
            txtNama.Enabled = false;
            cbxJenisKelamin.Enabled = false;
            txtAlamat.Enabled = false;
            dtTanggalLahir.Enabled = false;
            cbxProdi.Enabled = false;
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            button3.Enabled = false;
            clearBinding();
            FormDataMahasiswa_Load();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
