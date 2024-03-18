using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace kopiTumpah.webp
{
    static class NyambungDB
    {
        private static MySqlConnection _connection;
        public static MySqlConnection Connection
        {
            get
            {
                string StringKoneksi = "server=127.0.0.1;database=xiptsiimkii_twr;Uid=root;Pwd=;";
                //;"Data Source = MIFUNE-UEHR\\NIJIGASAKIHS; Initial Catalog =DBNyobaCRUD; Integrated Security = True
                _connection = new MySqlConnection(StringKoneksi);
                return _connection;
            }
        }
    }

    public class Operator
    {

        public string method;
  

        public void LoadDataToDataGridView(string dataTable, DataGridView datagrid)
        {
            try
            {
                using (MySqlConnection koneksi = NyambungDB.Connection)
                {
                    koneksi.Open();
                    method = "SELECT * FROM " + dataTable;

                    using (MySqlCommand command = new MySqlCommand(method, koneksi))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            datagrid.Rows.Clear();
                            while (reader.Read())
                            {
                                object[] row = new object[reader.FieldCount];
                                reader.GetValues(row);
                                datagrid.Rows.Add(row);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        int Id(string tabel)
        {
            int hasil = 1;
            using (MySqlConnection koneksi = NyambungDB.Connection)
            {
                koneksi.Open();
                string sql = "SELECT ID FROM " + tabel;
                MySqlCommand command = new MySqlCommand(sql, koneksi);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    List<int> IdList = new List<int>();
                    while (reader.Read())
                    {
                        IdList.Add(reader.GetInt32(0));
                    }
                    while (IdList.Contains(hasil))
                    {
                        hasil++;
                    }
                }
            }
            return hasil;

        }

        public void train_add(string nomor_kereta, string nama_layanan, string line, string leveltrain, bool cb_ekoChecked, bool cb_premChecked, bool cb_bisChecked, bool cb_ekseChecked, bool cb_pasarChecked)
        {
            using (MySqlConnection koneksi = NyambungDB.Connection)
            {
                koneksi.Open();
                string method = "INSERT INTO layanan (id, nomor_kereta, nama_layanan, line, level_layanan, kelas_layanan) VALUES (@idnew, @nomorkereta, @namalayanan, @line, @level_layanan, @kelas_layanan)";
                MySqlCommand command = new MySqlCommand(method, koneksi);

                command.Parameters.AddWithValue("@idnew", ("layanan")); // Assuming Id is a method that generates a new ID
                command.Parameters.AddWithValue("@nomorkereta", nomor_kereta);
                command.Parameters.AddWithValue("@namalayanan", nama_layanan);

                // Map the line value to the corresponding line name
                string lineValue = "";
                switch (line)
                {
                    case "PwtBjrs":
                        lineValue = "Purwokerto-Banjarsari";
                        break;
                    case "PwtPbg":
                        lineValue = "Purwokerto-Purbalingga";
                        break;
                    case "PwtKya":
                        lineValue = "Purwokerto-Kroya";
                        break;
                    default:
                        MessageBox.Show("Please select a line.");
                        return;
                }
                command.Parameters.AddWithValue("@line", lineValue);

                string levelValue = "";
                switch (leveltrain)
                {
                    case "FEED":
                        levelValue = "Feeder";
                        break;
                    case "LOC":
                        levelValue = "Lokal";
                        break;
                    case "LIMEX":
                        levelValue = "Patas";
                        break;
                    case "EXP":
                        levelValue = "Express";
                        break;
                    default:
                        MessageBox.Show("Please select a line.");
                        return;
                }
                command.Parameters.AddWithValue("@level_layanan", levelValue);

                // Concatenate selected checkboxes into a single string
                string kelasValue = "";
                if (cb_ekoChecked)
                    kelasValue += "Ekonomi, ";
                if (cb_premChecked)
                    kelasValue += "Premium, ";
                if (cb_bisChecked)
                    kelasValue += "Bisnis, ";
                if (cb_ekseChecked)
                    kelasValue += "Eksekutif, ";
                if (cb_pasarChecked)
                    kelasValue += "Pasar, ";

                // Remove the trailing comma and space if kelasValue is not empty
                if (!string.IsNullOrEmpty(kelasValue))
                    kelasValue = kelasValue.Remove(kelasValue.Length - 2);

                command.Parameters.AddWithValue("@kelas_layanan", kelasValue);

                int RowsAffected = command.ExecuteNonQuery();
            }
        }
        public void ResetAllControls(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Text = "";
                }
                else if (c is RadioButton)
                {
                    ((RadioButton)c).Checked = false;
                }
                else if (c is CheckBox)
                {
                    ((CheckBox)c).Checked = false;
                }
                else if (c.HasChildren)
                {
                    ResetAllControls(c);
                }
            }
        }


        public void train_update(int id, string nomor_kereta, string nama_layanan, string line, string leveltrain, bool cb_ekoChecked, bool cb_premChecked, bool cb_bisChecked, bool cb_ekseChecked, bool cb_pasarChecked)
        {
            using (MySqlConnection koneksi = NyambungDB.Connection)
            {
                koneksi.Open();
                string method = "UPDATE layanan SET nomor_kereta = @nomorkereta, nama_layanan = @namalayanan, line = @line, level_layanan = @level_layanan, kelas_layanan = @kelas_layanan WHERE ID = @id";
                MySqlCommand command = new MySqlCommand(method, koneksi);

                command.Parameters.AddWithValue("@id", id); // Assuming id is the primary key for the layanan table
                command.Parameters.AddWithValue("@nomorkereta", nomor_kereta);
                command.Parameters.AddWithValue("@namalayanan", nama_layanan);

                // Map the line value to the corresponding line name
                string lineValue = "";
                switch (line)
                {
                    case "PwtBjrs":
                        lineValue = "Purwokerto-Banjarsari";
                        break;
                    case "PwtPbg":
                        lineValue = "Purwokerto-Purbalingga";
                        break;
                    case "PwtKya":
                        lineValue = "Purwokerto-Kroya";
                        break;
                    default:
                        MessageBox.Show("Please select a line.");
                        return;
                }
                command.Parameters.AddWithValue("@line", lineValue);

                string levelValue = "";
                switch (leveltrain)
                {
                    case "FEED":
                        levelValue = "Feeder";
                        break;
                    case "LOC":
                        levelValue = "Lokal";
                        break;
                    case "LIMEX":
                        levelValue = "Patas";
                        break;
                    case "EXP":
                        levelValue = "Express";
                        break;
                    default:
                        MessageBox.Show("Please select a line.");
                        return;
                }
                command.Parameters.AddWithValue("@level_layanan", levelValue);

                // Concatenate selected checkboxes into a single string
                string kelasValue = "";
                if (cb_ekoChecked)
                    kelasValue += "Ekonomi, ";
                if (cb_premChecked)
                    kelasValue += "Premium, ";
                if (cb_bisChecked)
                    kelasValue += "Bisnis, ";
                if (cb_ekseChecked)
                    kelasValue += "Eksekutif, ";
                if (cb_pasarChecked)
                    kelasValue += "Pasar, ";

                // Remove the trailing comma and space if kelasValue is not empty
                if (!string.IsNullOrEmpty(kelasValue))
                    kelasValue = kelasValue.Remove(kelasValue.Length - 2);

                command.Parameters.AddWithValue("@kelas_layanan", kelasValue);

                int RowsAffected = command.ExecuteNonQuery();

            }
        }



        //public void train_edit(int id, TextBox txt_trainNumber, TextBox txt_namaLayanan, RadioButton Rb_PwtBjrs, RadioButton Rb_PwtPbg, RadioButton Rb_PwtKya, RadioButton RB_Feed, RadioButton RB_Lokal, RadioButton RB_Patas, RadioButton RB_Flag, CheckBox CB_Eko, CheckBox CB_Prem, CheckBox CB_Bis, CheckBox CB_Ekse, CheckBox CB_Pasar)
        //{
        //    try
        //    {
        //        // Check if train number and service name are filled
        //        if (string.IsNullOrEmpty(txt_trainNumber.Text) || string.IsNullOrEmpty(txt_namaLayanan.Text))
        //        {
        //            MessageBox.Show("Masukan semua data yang diperlukan!");
        //            return;
        //        }

        //        // Check if any radio button is checked
        //        if (!Rb_PwtBjrs.Checked && !Rb_PwtPbg.Checked && !Rb_PwtKya.Checked)
        //        {
        //            MessageBox.Show("Pilih line yang tersedia!");
        //            return;
        //        }

        //        // Determine the selected line
        //        string line = Rb_PwtBjrs.Checked ? "PwtBjrs" :
        //                      Rb_PwtPbg.Checked ? "PwtPbg" :
        //                      Rb_PwtKya.Checked ? "PwtKya" : "";

        //        // Check if train number is already used
        //        if (string.IsNullOrEmpty(line))
        //        {
        //            MessageBox.Show("Pilih line yang tersedia!");
        //            return;
        //        }

        //        // Check if any radio button is checked
        //        if (!RB_Feed.Checked && !RB_Lokal.Checked && !RB_Patas.Checked && !RB_Flag.Checked)
        //        {
        //            MessageBox.Show("Pilih salah satu level layanan yang tersedia!");
        //            return;
        //        }

        //        // Determine the selected line
        //        string leveltrain = RB_Feed.Checked ? "FEED" :
        //                            RB_Lokal.Checked ? "LOC" :
        //                            RB_Patas.Checked ? "LIMEX" :
        //                            RB_Flag.Checked ? "EXP" : "";

        //        // Check if train number is already used
        //        if (string.IsNullOrEmpty(leveltrain))
        //        {
        //            MessageBox.Show("Pilih salah satu level layanan yang tersedia!");
        //            return;
        //        }

        //        // Call train_add method passing in the checkboxes sta

        //        // Reset all controls after successfully adding the train
                
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error: {ex.Message}");
        //    }
        //}


        public bool IsTrainNumberAlreadyUsed(string trainNumber)
        {
            bool isUsed = false;

            // Query to check if the train number exists in the "layanan" table
            string query = $"SELECT COUNT(*) FROM layanan WHERE nomor_kereta = '{trainNumber}'";

            // Assuming NyambungDB is a class handling database connections
            MySqlConnection connection = NyambungDB.Connection;
            MySqlCommand command = new MySqlCommand(query, connection);

            try
            {
                connection.Open();
                int count = Convert.ToInt32(command.ExecuteScalar());

                // If count is greater than 0, it means the train number is already used
                if (count > 0)
                {
                    isUsed = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking train number: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

            return isUsed;
        }

        

        public void RetrieveDataToControls(DataGridViewRow row, TextBox txt_trainNumber, TextBox txt_namaLayanan, CheckBox CB_Eko, CheckBox CB_Prem, CheckBox CB_Bis, CheckBox CB_Ekse, CheckBox CB_Pasar, RadioButton Rb_PwtBjrs, RadioButton Rb_PwtPbg, RadioButton Rb_PwtKya, RadioButton RB_Feed, RadioButton RB_Lokal, RadioButton RB_Patas, RadioButton RB_Flag)
        {
            // Mengambil nilai dari sel DataGridView dan mengisinya ke TextBox dan CheckBox yang sesuai
            txt_trainNumber.Text = row.Cells["Col_noKereta"].Value.ToString();
            txt_namaLayanan.Text = row.Cells["Col_namaLayanan"].Value.ToString();

            // Checkbox
            string kelasLayanan = row.Cells["Col_kelas"].Value.ToString();
            CB_Eko.Checked = kelasLayanan.Contains("Ekonomi");
            CB_Prem.Checked = kelasLayanan.Contains("Premium");
            CB_Bis.Checked = kelasLayanan.Contains("Bisnis");
            CB_Ekse.Checked = kelasLayanan.Contains("Eksekutif");
            CB_Pasar.Checked = kelasLayanan.Contains("Pasar");

            // RadioButton
            switch (row.Cells["Col_line"].Value.ToString())
            {
                case "Purwokerto-Banjarsari":
                    
                    Rb_PwtBjrs.Checked = true;
                    break;
                case "Purwokerto-Purbalingga":
                    Rb_PwtPbg.Checked = true;
                    break;
                case "Purwokerto-Kroya":
                    Rb_PwtKya.Checked = true;
                    break;
                default:
                    // Handle case where line value is not recognized
                    break;
            }
            

            switch (row.Cells["Col_Level_layanan"].Value.ToString())
            {
                case "Feeder":
                    RB_Feed.Checked = true;
                    break;
                case "Lokal":
                    RB_Lokal.Checked = true;
                    break;
                case "Patas":
                    RB_Patas.Checked = true;
                    break;
                case "Express":
                    RB_Flag.Checked = true;
                    break;
                default:
                    // Handle case where level_layanan value is not recognized
                    break;
            }
        }

    }
}
