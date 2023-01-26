using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS
{
    class Guests
    {
        public int RoomNo { get; set; }
        public String FName { get; set; }
        public String LName { get; set; }
        public String P_ID { get; set; }
        public int NightsNo { get; set; }



        public Guests(int roomNo, String fName, String lName, String PID, int nightsNo)
        {
            RoomNo = roomNo;
            FName = fName;
            LName = lName;
            P_ID = PID;
            NightsNo = nightsNo;
        }

    }
}
