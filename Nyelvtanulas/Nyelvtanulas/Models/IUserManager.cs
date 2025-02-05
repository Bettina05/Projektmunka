using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Nyelvtanulas.Models
{
    public interface IUserManager
    {
        void Add(User user);
        IQueryable<User> GetAll();
    }
}
