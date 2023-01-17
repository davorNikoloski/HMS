using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HMS
{
    class DBRooms
    {
        public static MySqlConnection GetConnection()
        {
            string sql = "datasource=localhost; port=3306; username=root; password=admin11; database=hotelguest";

            MySqlConnection conn = new MySqlConnection(sql);
            try
            {
                conn.Open();
            }
            catch (MySqlException exc)
            {
                MessageBox.Show("Connection Error Type : \n" + exc.Message, " !! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return conn;
        }

        public static void DisplayRooms(string query, ComboBox cb)
        {
            string sql = query;

            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);

            DataTable dtb = new DataTable();
            adp.Fill(dtb);
            cb.DataSource = dtb;
            cb.DisplayMember = "id";
            cb.ValueMember = "id";
            conn.Close();
        }

        public static void read()
        {
            string sql = "SELECT * FROM rooms;";

            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            string[] stringArray = new string[10];
            while (reader.Read())
            {
                int i = 0;
                String res = reader.GetString(1);
                stringArray[i] = res;
                MessageBox.Show(stringArray[i]);
            }
        }
    }
}
