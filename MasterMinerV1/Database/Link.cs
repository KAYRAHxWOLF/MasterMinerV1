using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMinerV1.Database
{
    internal class Link
    {
        public int Id { get; set; }
        public Player player { get; set; } = null!;
        public int playerId { get; set; }
        public Upgrade upgrade { get; set; } = null!;
        public int upgradeId { get; set; }
    }
}
