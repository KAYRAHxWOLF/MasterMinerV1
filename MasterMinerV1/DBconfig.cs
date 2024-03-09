using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMinerV1
{
    public static class DBconfig
    {
        public static string ServerName = "localhost";
        public static string DatabaseName = "MasterMinerDB";
        public static string Username = "your_username";
        public static string Password = "your_password";
    }
}
