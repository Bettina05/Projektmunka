using Lingarix_Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lingarix_Database
{
    public interface IUserManager
    {
        void Add(Users user);
        IQueryable<Users> GetAll();
    }
}
