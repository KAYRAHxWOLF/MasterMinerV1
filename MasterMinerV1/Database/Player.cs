using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MasterMinerV1.Database
{
    internal class Player
    {
        public int Id { get; set; }
        public int GameSlot { get; set; }
        public string Ores { get; set; } = "0"; //Possible compability problems here
        public string ClickVal { get; set; } = "1"; //and here
        public long TotalClicks { get; set; } = 0;
        public ICollection<Link> Links { get; set; }
    }
}
