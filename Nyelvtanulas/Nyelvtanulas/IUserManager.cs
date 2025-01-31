using Microsoft.AspNetCore.Identity;
using Nyelvtanulas.Models;
using System.Text;

namespace Nyelvtanulas
{
    public interface IUserManager
    {
        void Add(User user);
        IQueryable<User> GetAll();
    }
}
