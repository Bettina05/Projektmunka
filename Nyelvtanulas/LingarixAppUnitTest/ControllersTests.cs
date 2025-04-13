using Lingarix_Database;
using Lingarix_Database.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Nyelvtanulas.Controllers;
using Nyelvtanulas.Models;
using NyelvtanuloMVC.Controllers;
using System.Net.Http;
using System.Text;

namespace NyelvtanulasUnitTest
{
    public class TestsForController
    {
        private Mock<IHttpContextAccessor> mockHttpContextAccessor;
        private Mock<IUserManager> mockUserManager;
        private Mock<IEncryptionService> mockEncryptionService;
        private AuthenticationServiceWithSession authService;
        
        [SetUp]
        public void Setup()
        {
            mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockUserManager = new Mock<IUserManager>();
            mockEncryptionService = new Mock<IEncryptionService>();

            authService = new AuthenticationServiceWithSession(
                mockHttpContextAccessor.Object,
                mockUserManager.Object,
                mockEncryptionService.Object);
        }
        [Test]
        public void RegisterUser_CreatesUserWithHashedPassword_WhenCalled()
        {
            var mockUserManager = new Mock<IUserManager>();
            var mockEncryptionService = new Mock<IEncryptionService>();
            var mockAuthenticationService = new Mock<IAuthenticationService>();
            var controller = new AccountController(mockUserManager.Object, mockEncryptionService.Object, mockAuthenticationService.Object);

            var user = new Users { Username = "testUser", PasswordHash = "plainPassword" };
            mockEncryptionService.Setup(e => e.HashPassword(It.IsAny<string>())).Returns("hashedPassword");

            var result = controller.RegisterUser(user);

            Assert.AreEqual("hashedPassword", user.PasswordHash);
            mockUserManager.Verify(u => u.Add(It.Is<Users>(u => u.Username == "testUser" && u.PasswordHash == "hashedPassword")), Times.Once); 
        }

        [Test]
        public void Login_ReturnsSuccessMessage_WhenCredentialsAreCorrect()
        {
            var mockUserManager = new Mock<IUserManager>();
            var mockEncryptionService = new Mock<IEncryptionService>();
            var mockAuthenticationService = new Mock<IAuthenticationService>();
            var controller = new AccountController(mockUserManager.Object, mockEncryptionService.Object, mockAuthenticationService.Object);

            mockAuthenticationService.Setup(a => a.TryLogIn("testUser", "correctPassword")).Returns(true);

            var result = controller.Login("testUser", "correctPassword", "captcha");

            Assert.AreEqual("Sikeres bejelentkezés!", controller.ViewBag.Message);
        }

        [Test]
        public void Login_ReturnsErrorMessage_WhenCredentialsAreIncorrect()
        {
            var mockUserManager = new Mock<IUserManager>();
            var mockEncryptionService = new Mock<IEncryptionService>();
            var mockAuthenticationService = new Mock<IAuthenticationService>();
            var controller = new AccountController(mockUserManager.Object, mockEncryptionService.Object, mockAuthenticationService.Object);

            mockAuthenticationService.Setup(a => a.TryLogIn("testUser", "wrongPassword")).Returns(false);

            var result = controller.Login("testUser", "wrongPassword", "captcha");

            Assert.AreEqual("Sikertelen bejelentkezés!", controller.ViewBag.Message);
        }

        [Test]
        public void Logout_CallsLogOutAndRedirectsToHome_WhenCalled()
        {
            var mockUserManager = new Mock<IUserManager>();
            var mockEncryptionService = new Mock<IEncryptionService>();
            var mockAuthenticationService = new Mock<IAuthenticationService>();
            var controller = new AccountController(mockUserManager.Object, mockEncryptionService.Object, mockAuthenticationService.Object);

            var result = controller.Logout();

            mockAuthenticationService.Verify(a => a.LogOut(), Times.Once);
            Assert.IsInstanceOf<RedirectToActionResult>(result); 
            var redirectResult = result as RedirectToActionResult;
            Assert.AreEqual("Logout", redirectResult?.ActionName); 
        }
        
    }
}
