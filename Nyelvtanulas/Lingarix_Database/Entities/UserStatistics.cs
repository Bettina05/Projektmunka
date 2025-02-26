using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lingarix_Database.Entities
{
    public class UserStatistics
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime Date {  get; set; }
        public int Score { get; set; }
    }
}
