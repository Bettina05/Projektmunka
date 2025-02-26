using Lingarix_Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lingarix_Database
{
    public class DatabaseUserManager : IUserManager
    {
        private LingarixDbContext dbContext;

        public DatabaseUserManager(LingarixDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(Users user)
        {
            dbContext.users.Add(user);
            dbContext.SaveChanges();
        }

        public IQueryable<Users> GetAll()
        {
            return dbContext.users.AsQueryable();
        }
    }
}
