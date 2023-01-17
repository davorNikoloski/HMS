using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HMS
{
    public partial class FormRoomInfo : Form
    {
        private readonly FormGuestInfo _parent;
        public FormRoomInfo(FormGuestInfo parent)
        {
            InitializeComponent();
            _parent = parent;
        }
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

        public void ShowRooms()
        {
            DBRooms.read();
        }

        private void FormRoomInfo_Shown(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM rooms;";

            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            string[] roomsL = new string[] { "r1", "r2", "r3", "r4", "r5", "r11", "r12", "r13", "r14", "r15", };
            List<string> test = new List<string>();

            while (reader.Read())
            {
                //String res = reader.GetString(0);
                String res1 = reader.GetString(1);

                test.Add(res1);
            }


            for (int i = 0; i < 10; i++)
            {
                Panel p = this.Controls.Find(roomsL[i], true).FirstOrDefault() as Panel;
                if (test[i] == "True")
                {
                    // MessageBox.Show("True");
                    p.BackColor = Color.Red;
                }
                else
                {
                    //MessageBox.Show("False");
                    p.BackColor = Color.Green;

                }

            }

        }

    }
}
