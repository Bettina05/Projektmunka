using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LingarixApplication
{
    public class UserStatistics
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public int Score { get; set; }
        public DateTime Date { get; set; }
        public string Exercises { get; set; }
        public double StudyTime { get; set; }
    }
}
