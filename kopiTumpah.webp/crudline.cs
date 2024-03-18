using MySql.Data.MySqlClient;
using Mysqlx.Datatypes;
using Mysqlx.Expr;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Mysqlx.Notice.Warning.Types;

namespace kopiTumpah.webp
{
    public partial class crudline : Form
    {

        public Operator op;
        int id;
        

        public crudline()
        {
            InitializeComponent();
            op = new Operator();
        }

        public void layanan_view()
        {
            op.LoadDataToDataGridView("layanan", DTG1);
        }
        private void crudline_Load(object sender, EventArgs e)
        {
            layanan_view();
        }


        private void btn_connectionCheck_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = NyambungDB.Connection; // Get the MySqlConnection object from NyambungDB
            try
            {
                connection.Open(); // Attempt to open the connection
                MessageBox.Show("Connection successful!"); // Display a success message
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection failed: {ex.Message}"); // Display an error message if connection fails
            }
            finally
            {
                connection.Close(); // Make sure to close the connection when done
            }
        }

        private void btn_tambahKereta_Click(object sender, EventArgs e)
        {
            // Check if train number and service name are filled
            if (string.IsNullOrEmpty(txt_trainNumber.Text) || string.IsNullOrEmpty(txt_namaLayanan.Text))
            {
                MessageBox.Show("Masukan semua data yang diperlukan!");
                return;
            }

            // Check if any radio button is checked
            if (!Rb_PwtBjrs.Checked && !Rb_PwtPbg.Checked && !Rb_PwtKya.Checked)
            {
                MessageBox.Show("Pilih line yang tersedia!");
                return;
            }

            // Determine the selected line
            string line = Rb_PwtBjrs.Checked ? "PwtBjrs" :
                          Rb_PwtPbg.Checked ? "PwtPbg" :
                          Rb_PwtKya.Checked ? "PwtKya" : "";

            // Check if train number is already used
            if (string.IsNullOrEmpty(line))
            {
                MessageBox.Show("Pilih line yang tersedia!");
                return;
            }

            // Check if any radio button is checked
            if (!RB_Feed.Checked && !RB_Lokal.Checked && !RB_Patas.Checked && !RB_Flag.Checked)
            {
                MessageBox.Show("Pilih salah satu level layanan yang tersedia!");
                return;
            }

            // Determine the selected line
            string leveltrain = RB_Feed.Checked ? "FEED" :
                                RB_Lokal.Checked ? "LOC" :
                                RB_Patas.Checked ? "LIMEX" :
                                RB_Flag.Checked ? "EXP" : "";

            // Check if train number is already used
            if (string.IsNullOrEmpty(leveltrain))
            {
                MessageBox.Show("Pilih salah satu level layanan yang tersedia!");
                return;
            }

            // Call train_add method passing in the checkboxes state
            op.train_add(txt_trainNumber.Text, txt_namaLayanan.Text, line, leveltrain, CB_Eko.Checked, CB_Prem.Checked, CB_Bis.Checked, CB_Ekse.Checked, CB_Pasar.Checked);

            layanan_view();
        }






        private void btn_load_Click(object sender, EventArgs e)
        {
            op.LoadDataToDataGridView("layanan", DTG1);
        }

        private void DTG1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Rb_PwtBjrs_CheckedChanged(object sender, EventArgs e)
        {
            GB_PwtBjrs.Enabled = Rb_PwtBjrs.Checked;
        }

        private void Rb_PwtPbg_CheckedChanged(object sender, EventArgs e)
        {
            GB_PwtPbg.Enabled = Rb_PwtPbg.Checked;
            GB_PwtBjrs.Enabled = Rb_PwtPbg.Checked;
        }



        private void Rb_PwtKya_CheckedChanged(object sender, EventArgs e)
        {
            GB_PwtKya.Enabled = Rb_PwtKya.Checked;
        }

        private void Rb_PwtBa_CheckedChanged(object sender, EventArgs e)
        {
            GB_BjrsBa.Enabled = Rb_PwtBa.Checked;
            GB_PwtBjrs.Enabled = Rb_PwtBa.Checked;
        }

        private void CB_Ekse_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btn_editKereta_Click(object sender, EventArgs e)
        {
            // Check if train number and service name are filled
            if (string.IsNullOrEmpty(txt_trainNumber.Text) || string.IsNullOrEmpty(txt_namaLayanan.Text))
            {
                MessageBox.Show("Masukan semua data yang diperlukan!");
                return;
            }

            // Check if any radio button is checked
            if (!Rb_PwtBjrs.Checked && !Rb_PwtPbg.Checked && !Rb_PwtKya.Checked)
            {
                MessageBox.Show("Pilih line yang tersedia!");
                return;
            }

            // Determine the selected line
            string line = Rb_PwtBjrs.Checked ? "PwtBjrs" :
                            Rb_PwtPbg.Checked ? "PwtPbg" :
                            Rb_PwtKya.Checked ? "PwtKya" : "";

            // Check if train number is already used
            if (string.IsNullOrEmpty(line))
            {
                MessageBox.Show("Pilih line yang tersedia!");
                return;
            }

            // Check if any radio button is checked
            if (!RB_Feed.Checked && !RB_Lokal.Checked && !RB_Patas.Checked && !RB_Flag.Checked)
            {
                MessageBox.Show("Pilih salah satu level layanan yang tersedia!");
                return;
            }

            // Determine the selected line
            string leveltrain = RB_Feed.Checked ? "FEED" :
                                RB_Lokal.Checked ? "LOC" :
                                RB_Patas.Checked ? "LIMEX" :
                                RB_Flag.Checked ? "EXP" : "";

            // Check if train number is already used
            if (string.IsNullOrEmpty(leveltrain))
            {
                MessageBox.Show("Pilih salah satu level layanan yang tersedia!");
                return;
            }

            // Call train_add method passing in the checkboxes state
            op.train_update(id, txt_trainNumber.Text, txt_namaLayanan.Text, line, leveltrain, CB_Eko.Checked, CB_Prem.Checked, CB_Bis.Checked, CB_Ekse.Checked, CB_Pasar.Checked);
            //op.ResetAllControls(this);
            layanan_view();
        }


        private void DTG1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = DTG1.Rows[e.RowIndex];
                Operator dataRetriever = new Operator();
                dataRetriever.RetrieveDataToControls(row, txt_trainNumber, txt_namaLayanan, CB_Eko, CB_Prem, CB_Bis, CB_Ekse, CB_Pasar, Rb_PwtBjrs, Rb_PwtPbg, Rb_PwtKya, RB_Feed, RB_Lokal, RB_Patas, RB_Flag);

                // Set the id variable to the id of the selected row
                id = Convert.ToInt32(row.Cells["Col_id"].Value);
            }
        }


    }
}
