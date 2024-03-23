using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMinerV1.Database
{
    internal class Player
    {
        public string Name { get; set; } = null!;
        public int Ores { get; set; } = 0;
        public int ClickVal { get; set; } = 1;
        public int TotClicks { get; set; } = 0;
        public ICollection<Upgrade> Upgrades { get; set; }
    }
}
