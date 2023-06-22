﻿using System;
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
    public partial class DataMahasiswa : Form
    {
        private string stringConnection = "data source=DESKTOP-6I7FGSP\\NEMBOO;database=Activity6PABD;User ID=sa; Password=123";
        private SqlConnection koneksi;
        private string nim, nama, alamat, jk, prodi;
        private DateTime tgl;
        BindingSource costumersBindingSource = new BindingSource();
        public DataMahasiswa()
        {
            InitializeComponent();
            koneksi = new SqlConnection(stringConnection);
            this.bnMahasiswa.BindingSource = this.costumersBindingSource;
            refreshform();
            
        }

        private void FormDataMahasiswa_Load()
        {
            koneksi.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(new SqlCommand("Select m.nim, m.nama_mahasiswa, m.alamat, m.jenis_kelamin, m.tgl_lahir, p.nama_prodi\r\nFrom dbo.mahasiswa m \r\njoin dbo.prodi p \r\non m.id_prodi = p.id_prodi", koneksi));
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
                new Binding("Text", this.costumersBindingSource, "Jenis_kelamin", true));
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtNIM.Text = "";
            txtNama.Text = "";
            txtAlamat.Text = "";
            dtTanggalLahir.Value = DateTime.Now;
            txtNIM.Enabled = true;
            txtNama.Enabled = true;
            txtAlamat.Enabled = true;
            cbxJenisKelamin.Enabled = true;
            dtTanggalLahir.Enabled = true;
            cbxProdi.Enabled = true;
            prodicbx();
            btnSave.Enabled = true;
            btnClear.Enabled = true;
            btnAdd.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            nim = txtNIM.Text;
            nama = txtNama.Text;
            jk = cbxJenisKelamin.Text;
            alamat = txtAlamat.Text;
            tgl = dtTanggalLahir.Value;
            prodi = cbxProdi.Text;
            int hs = 0;
            koneksi.Open();
            string strs = "select id_prodi from dbo.prodi where nama_prodi = @dd";
            SqlCommand cm = new SqlCommand(strs, koneksi);
            cm.CommandType = CommandType.Text;
            cm.Parameters.Add(new SqlParameter("@dd", prodi));
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                hs = int.Parse(dr["id_prodi"].ToString());
            }
            dr.Close();
            string str = "insert into dbo.Mahasiswa (nim, nama_mahasiswa, jenis_kelamin, alamat, tgl_lahir, id_prodi)" +
                "values(@NIM, @nm, @jk, @Al, @Tgll, @Idp)";
            SqlCommand cmd = new SqlCommand(str, koneksi);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("NIM", nim));
            cmd.Parameters.Add(new SqlParameter("nm", nama));
            cmd.Parameters.Add(new SqlParameter("jk", jk));
            cmd.Parameters.Add(new SqlParameter("Al", alamat));
            cmd.Parameters.Add(new SqlParameter("Tgll", tgl));
            cmd.Parameters.Add(new SqlParameter("Idp", hs));
            cmd.ExecuteNonQuery();

            koneksi.Close();

            MessageBox.Show("Data Berhasil Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

            refreshform();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            refreshform();
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
            btnClear.Enabled = false;
            clearBinding();
            FormDataMahasiswa_Load();
        }

        private void prodicbx()
        {
            koneksi.Open();
            string str = "select nama_prodi from dbo.prodi";
            SqlCommand cmd = new SqlCommand(str, koneksi);
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmd.ExecuteReader();
            koneksi.Close();
            cbxProdi.DisplayMember = "nama_prodi";
            cbxProdi.ValueMember = "id_prodi";
            cbxProdi.DataSource = ds.Tables[0];
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
