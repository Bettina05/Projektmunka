using Lingarix_Database.Entities;

namespace Nyelvtanulas.Models
{
    public interface IUserValidator
    {
        bool ValidateUser(Users user);
    }
}
