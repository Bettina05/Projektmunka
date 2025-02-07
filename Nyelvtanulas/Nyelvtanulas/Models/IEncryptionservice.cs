namespace Nyelvtanulas.Models
{
    public interface IEncryptionService
    {
        string HashPassword(string password);
    }
}
