using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lingarix_Database.Entities
{
    public class UserStatistics
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime Date {  get; set; }
        public int Score { get; set; }
        public string Exercises { get; set; }
        public double StudyTime { get; set; }
    }
}
