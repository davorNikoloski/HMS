using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS
{
    class Rooms
    {
        public int id { get; set; }
        public bool occupied { get; set; }
        public string type { get; set; }

        public Rooms(int Id, bool Occupied, string Type)
        {
            Id = id;
            Occupied = occupied;
            Type = type;
        }
    }
}
