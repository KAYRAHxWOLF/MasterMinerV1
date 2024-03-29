using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMinerV1.Database
{
    internal class Player
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Ores { get; set; } = 0;
        public int ClickVal { get; set; } = 1;
        public int TotalClicks { get; set; } = 0;
        public ICollection<Link> Links { get; set; }
    }
}
