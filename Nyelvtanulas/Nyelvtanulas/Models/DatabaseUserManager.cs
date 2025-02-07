namespace Nyelvtanulas.Models
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
            return dbContext.Users.AsQueryable();
        }
    }
}
