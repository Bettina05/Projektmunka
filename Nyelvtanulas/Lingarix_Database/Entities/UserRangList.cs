﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lingarix_Database.Entities
{
    public class UserRangList
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public int Points { get; set; }
    }
}
