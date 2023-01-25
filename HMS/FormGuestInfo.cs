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
            if (e.ColumnIndex == 0)
            {
                
                formGuest.clear();
                formGuest._id = Convert.ToInt32(dataGridViewGuest.Rows[e.RowIndex].Cells[2].Value.ToString());
                formGuest._roomID = Convert.ToInt32(dataGridViewGuest.Rows[e.RowIndex].Cells[3].Value.ToString());
                formGuest._fName = dataGridViewGuest.Rows[e.RowIndex].Cells[4].Value.ToString();
                formGuest._lName = dataGridViewGuest.Rows[e.RowIndex].Cells[5].Value.ToString();
                formGuest._PID = dataGridViewGuest.Rows[e.RowIndex].Cells[6].Value.ToString();
                //MessageBox.Show(formGuest._roomID.ToString());
                formGuest.UpdateGuestInfo();
                formGuest.ShowDialog();
               
                return;
            }
            if (e.ColumnIndex == 1)
            {
                if (MessageBox.Show("Are you sure you want to check out this guest?", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //formChckOut.ShowDialog();
                    DBGuests.DeleteGuest(dataGridViewGuest.Rows[e.RowIndex].Cells[2].Value.ToString());

                    displayGuests();
                }
                return;
            }
        }

        
    }
}
// za delete odi na formChckOut.ShowDialog(); kaj so cheknuvame da go delete i ni se prikazuva dolg za uplata

//==========ONLINE SCHEDULE