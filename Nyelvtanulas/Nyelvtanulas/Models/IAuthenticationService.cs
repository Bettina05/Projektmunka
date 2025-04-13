namespace Nyelvtanulas.Models
{
    public interface IAuthenticationService
    {
        void LogOut();

        bool TryLogIn(string username, string password);

        bool IsAuthenticated { get; }

        string UserName { get; }
    }
}
