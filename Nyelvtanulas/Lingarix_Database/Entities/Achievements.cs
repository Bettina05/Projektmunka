using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lingarix_Database.Entities
{
    public class Achievements
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string AchievementName { get; set; }

        public DateTime DateEarned { get; set; } = DateTime.Now;
    }
}
