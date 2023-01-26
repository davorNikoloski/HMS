using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS
{
    public partial class FormGuestInfo : Form
    {
        FormGuestCheckIn formGuest;
        FormRoomInfo formRoom;
        FormGuestCheckOut formChckOut;
        public FormGuestInfo()
        {
            InitializeComponent();
            formGuest = new FormGuestCheckIn(this);
            formRoom = new FormRoomInfo(this);
            formChckOut = new FormGuestCheckOut(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //rooms
            formRoom.ShowDialog();
        }

        public void displayGuests()
        {
            DBGuests.DisplayAndSearch("SELECT id, roomID, fName, lName, PID, checkInTime FROM guests;", dataGridViewGuest);
        }

        public int price(string rNo, int duration)
        {
            int cost = 1;
            Dictionary<string, string> type = DBRooms.showRoomType();

            if(type[rNo] == "Single")
            {
                cost = 100 * duration; 
            }
            else if(type[rNo] == "Deluxe")
            {
                cost = 140 * duration;
            }
            else if (type[rNo] == "Suite")
            {
                cost = 220 * duration;
            }
            else if (type[rNo] == "Penthouse")
            {
                cost = 280 * duration;
            }
            return cost;

        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            formGuest.clear();
            formGuest.ShowDialog();
        }

        private void FormGuestInfo_Shown(object sender, EventArgs e)
        {
            displayGuests();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DBGuests.DisplayAndSearch("SELECT id, roomID, fName, lName, PID, checkInTime FROM guests WHERE fName LIKE '%"+ txtSearch.Text +"%'", dataGridViewGuest);

        }

        private void dataGridViewGuest_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            List<string> duration = DBGuests.duration();
            int rowIndex = dataGridViewGuest.Rows[e.RowIndex].Index;
            if (e.ColumnIndex == 0)
            {
                
                formGuest.clear();
                formGuest._id = Convert.ToInt32(dataGridViewGuest.Rows[e.RowIndex].Cells[2].Value.ToString());
                formGuest._roomID = Convert.ToInt32(dataGridViewGuest.Rows[e.RowIndex].Cells[3].Value.ToString());
                formGuest._fName = dataGridViewGuest.Rows[e.RowIndex].Cells[4].Value.ToString();
                formGuest._lName = dataGridViewGuest.Rows[e.RowIndex].Cells[5].Value.ToString();
                formGuest._PID = dataGridViewGuest.Rows[e.RowIndex].Cells[6].Value.ToString();
                formGuest._nightsNo = Convert.ToInt32(duration[rowIndex]);
                //MessageBox.Show(duration[rowIndex]);
                formGuest.UpdateGuestInfo();
                formGuest.ShowDialog();
                
                return;
            }
            if (e.ColumnIndex == 1)
            {
                string roomNo = dataGridViewGuest.Rows[e.RowIndex].Cells[3].Value.ToString();
                int dur = Convert.ToInt32(duration[rowIndex]);
                int cost = price(roomNo, dur);


                if (MessageBox.Show("Are you sure you want to check out this guest?", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (MessageBox.Show("The guest stayed for: "+dur+" nights and the cost is: "+cost, "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        //formChckOut.ShowDialog();
                        DBGuests.DeleteGuest(dataGridViewGuest.Rows[e.RowIndex].Cells[2].Value.ToString());

                        displayGuests();
                    }
                }
                return;
            }
        }
    }
}

//==========ONLINE SCHEDULE