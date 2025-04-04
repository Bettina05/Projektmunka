﻿namespace Nyelvtanulas.Models
{
    public interface IAuthenticationService
    {
        // Kijelentkezés
        void LogOut();

        // Bejelentkezés
        bool TryLogIn(string username, string password);

        bool IsAuthenticated { get; }

        string UserName { get; }
    }
}
