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

        public static List<string> showOccupied()
        {
            string sql = "SELECT * FROM rooms;";

            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<string> roomOcp = new List<string>();

            while (reader.Read())
            {
                //String res = reader.GetString(0);
                String rOcp = reader.GetString(1);

                roomOcp.Add(rOcp);
            }
            return roomOcp;
        }

        public static Dictionary<string,string> showRoomType()
        {
            string sql = "SELECT * FROM rooms;";

            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            Dictionary<string, string> kvpRnoRtyp = new Dictionary<string, string>();

            while (reader.Read())
            {
                string roomNum = reader.GetString(0);
                string roomTyp = reader.GetString(2);
                kvpRnoRtyp.Add(roomNum,roomTyp);
            }
            
            return kvpRnoRtyp;
        }
    }
}
