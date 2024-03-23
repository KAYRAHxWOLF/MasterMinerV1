using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMinerV1.Database
{
    internal class Upgrade
    {
        public string Name { get; set; } = null!;
        public int Cost { get; set; } = 0!;
        public int ClickVal { get; set; } = 0!;
    }
}
