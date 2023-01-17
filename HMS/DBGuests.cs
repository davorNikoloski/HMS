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
    class DBGuests
    {
        public static MySqlConnection GetConnection()
        {
            string sql = "datasource=localhost; port=3306; username=root; password=*******; database=hotelguest";

            MySqlConnection conn = new MySqlConnection(sql);
            try
            {
                conn.Open();
            }
            catch (MySqlException exc)
            {
                MessageBox.Show("Connection Error Type : \n" + exc.Message , " !! " , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return conn;
        }

        public static void AddGuest(Guests guest)
        {
            string sqlc = "INSERT INTO guests (roomID,fName, lName, PID) VALUES (@RoomNo,@GuestsfName,@GuestslName,@GuestsPID)";
            string sqlR = "UPDATE rooms INNER JOIN guests ON guests.roomID = rooms.ID SET rooms.occupied = 1;";
            string test = "INSERT INTO guests (roomID, fName, lName, PID) VALUES(" + guest.RoomNo + ",'" + guest.FName + "','" + guest.LName + "','" + guest.P_ID + "');";
            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sqlc, conn);
            MySqlCommand cmdR = new MySqlCommand(sqlR, conn);

            cmd.CommandType = CommandType.Text;
            //cmdR.CommandText = sqlR;
            
            cmd.Parameters.Add("@RoomNo", MySqlDbType.Int32).Value = guest.RoomNo;
            cmd.Parameters.Add("@GuestsfName", MySqlDbType.VarChar).Value = guest.FName;
            cmd.Parameters.Add("@GuestslName", MySqlDbType.VarChar).Value = guest.LName;
            cmd.Parameters.Add("@GuestsPID", MySqlDbType.VarChar).Value = guest.P_ID;
            
            try
            {
                cmd.ExecuteNonQuery();
                cmdR.ExecuteNonQuery();
                MessageBox.Show("Guest added Successfully : \n", "Info" , MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (MySqlException exc)
            {
                MessageBox.Show("Guest NOT added : \n" + exc.Message, " !! ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            conn.Close();
        }

        public static void UpdateGuest(Guests guest, int id)
        {
            string sqlc = "UPDATE guests SET roomID = @RoomNo, fName = @GuestsfName, lName = @GuestslName, PID = @GuestsPID WHERE id = @guestID;";
            string sqlR = "UPDATE rooms INNER JOIN guests ON guests.roomID = rooms.ID SET rooms.occupied = 1;";
            string sqlRu = "UPDATE rooms INNER JOIN guests ON guests.roomID = rooms.id SET occupied=0 WHERE guests.id = @guestID";

            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sqlc, conn);
            MySqlCommand cmdR = new MySqlCommand(sqlR, conn);
            MySqlCommand cmdRu = new MySqlCommand(sqlRu, conn);

            cmd.CommandType = CommandType.Text;
            cmdRu.CommandType = CommandType.Text;
            cmdRu.Parameters.Add("@guestID", MySqlDbType.VarChar).Value = id;
            //cmdR.CommandText = sqlR;

            cmd.Parameters.Add("@guestID", MySqlDbType.Int32).Value = id;
            cmd.Parameters.Add("@RoomNo", MySqlDbType.Int32).Value = guest.RoomNo;
            cmd.Parameters.Add("@GuestsfName", MySqlDbType.VarChar).Value = guest.FName;
            cmd.Parameters.Add("@GuestslName", MySqlDbType.VarChar).Value = guest.LName;
            cmd.Parameters.Add("@GuestsPID", MySqlDbType.VarChar).Value = guest.P_ID;
            try
            {
                cmdRu.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
                cmdR.ExecuteNonQuery();
                
                MessageBox.Show("Guest updated Successfully : \n", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (MySqlException exc)
            {
                MessageBox.Show("Guest NOT updated : \n" + exc.Message, " !! ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            conn.Close();
        }

        public static void DeleteGuest(string id)
        {
            string sqlc = "DELETE FROM guests WHERE id = @guestID";
            string sqlR = "UPDATE rooms INNER JOIN guests ON guests.roomID = rooms.id SET occupied=0 WHERE guests.id = @guestID";

            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sqlc, conn);
            MySqlCommand cmdR = new MySqlCommand(sqlR, conn);

            cmd.CommandType = CommandType.Text;
            cmdR.CommandType = CommandType.Text;
            cmd.Parameters.Add("@guestID", MySqlDbType.VarChar).Value = id;
            cmdR.Parameters.Add("@guestID", MySqlDbType.VarChar).Value = id;
            try
            {
                cmdR.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Guest Checked Out Successfully : \n", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (MySqlException exc)
            {
                MessageBox.Show("Guest NOT checked out : \n" + exc.Message, " !! ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            conn.Close();

        }

        public static void DisplayAndSearch(string query, DataGridView dgv)
        {
            string sql = query;

            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable dtb = new DataTable();
            adp.Fill(dtb);
            dgv.DataSource = dtb;
            conn.Close();
        }

    }
}
