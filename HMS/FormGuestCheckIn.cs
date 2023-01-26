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
    public partial class FormGuestCheckIn : Form
    {
        private readonly FormGuestInfo _parent;

        public string  _fName, _lName, _PID;
        public int _roomID, _id, _nightsNo;
        
        public FormGuestCheckIn(FormGuestInfo parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        public void UpdateGuestInfo()
        {
            lblHead.Text = "Guest Info Update";
            btnAdd.Text = "Update";
            fName.Text = _fName;
            lName.Text = _lName;
            PID.Text = _PID;
            roomNo.Text = _roomID.ToString();
            nightsNo.Text = _nightsNo.ToString();
        }

        private void roomNo_Enter(object sender, EventArgs e)
        {
            DisplayRooms();
        }

        public void clear()
        {
            fName.Text = lName.Text = PID.Text = nightsNo.Text = string.Empty;
            roomNo.Text = "Choose Room";
            roomNo.SelectedItem = null;
            
        }
        
        public void DisplayRooms()
        {
            DBRooms.DisplayRooms("SELECT id FROM rooms WHERE occupied = false;", roomNo);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
           if(btnAdd.Text == "Add")
            {
                
                if (fName.Text == string.Empty || lName.Text == string.Empty || PID.Text == string.Empty)
                {
                    MessageBox.Show("Missing information");
                }
                else
                {
                    Guests guest = new Guests(Convert.ToInt32(roomNo.Text), fName.Text.Trim(), lName.Text.Trim(), PID.Text.Trim(),Convert.ToInt32(nightsNo.Text));
                    DBGuests.AddGuest(guest);
                    clear();
                    Close();

                }

            }
            if (btnAdd.Text == "Update")
            {
                Guests guest = new Guests(Convert.ToInt32(roomNo.Text), fName.Text.Trim(), lName.Text.Trim(), PID.Text.Trim(), Convert.ToInt32(nightsNo.Text));
                DBGuests.UpdateGuest(guest,_id);
                clear();
                Close();
            }
            _parent.displayGuests();
            
        }

    }
}
