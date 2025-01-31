using Nyelvtanulas.Models;

namespace Nyelvtanulas
{
    public class DatabaseUserManager : IUserManager
    {
        private UserDbContext dbContext;

        public DatabaseUserManager(UserDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(User user)
        {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }

        public IQueryable<User> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
