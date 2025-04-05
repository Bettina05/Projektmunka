using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lingarix_Database.Entities
{
    public class UserBadge
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string BadgeName { get; set; }
        public DateTime DateEarned { get; set; }
    }
}
