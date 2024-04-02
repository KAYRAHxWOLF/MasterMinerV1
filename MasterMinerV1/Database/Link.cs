using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MasterMinerV1.Database
{
    internal class Link
    {
        //links
        public int Id { get; set; }
        public Player player { get; set; } = null!;
        public int playerId { get; set; }
        public Upgrade upgrade { get; set; } = null!;
        public int upgradeId { get; set; }
        public string price { get; set; } = null!;
        public int owned { get; set; } = 0;
        public int increasePercent { get; set; }
        public int clickval { get; set; }
    }
}
