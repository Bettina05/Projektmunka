using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lingarix_Database.Entities
{
    public class UserLevels
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
