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
    public partial class FormGuestCheckOut : Form
    {
        private readonly FormGuestInfo _parent;
        public FormGuestCheckOut(FormGuestInfo parent)
        {
            InitializeComponent();
            _parent = parent;
        }
    }
}
//Display roomNo ime prezime i kolku mu e obvrska za plajcanje posle room.occupied=0 i e slobodna
//