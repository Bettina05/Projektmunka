using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lingarix_Database.Entities
{
    public class UserBadge
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string BadgeName { get; set; }
        public DateTime DateEarned { get; set; }
    }
}
