using Lingarix_Database;
using Lingarix_Database.Entities;
using Microsoft.AspNetCore.Http;
using Moq;
using Nyelvtanulas.Models;
using System.Net.Http;
using System.Text;

namespace NyelvtanulasUnitTest
{
    public class Tests
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
        public void IsAuthenticated_ReturnsTrue_WhenUsernameInSession()
        {
            var mockSession = new Mock<ISession>();
            mockHttpContextAccessor.Setup(h => h.HttpContext.Session).Returns(mockSession.Object);
            mockSession.Setup(s => s.TryGetValue("username", out It.Ref<byte[]>.IsAny)).Returns(true);

            var result = authService.IsAuthenticated;

            Assert.IsTrue(result);
        }
        [Test]
        public void UserName_ReturnsCorrectUsername_WhenUsernameInSession()
        {
            var mockSession = new Mock<ISession>();
            mockHttpContextAccessor.Setup(h => h.HttpContext.Session).Returns(mockSession.Object);
            byte[] expectedUsername = Encoding.UTF8.GetBytes("tesztUser");
            mockSession.Setup(s => s.TryGetValue("username", out expectedUsername)).Returns(true);

            var result = authService.UserName;

            Assert.AreEqual("tesztUser", result);
        }
        [Test]
        public void LogOut_ClearsSession_WhenCalled()
        {
            var mockSession = new Mock<ISession>();
            mockHttpContextAccessor.Setup(h => h.HttpContext.Session).Returns(mockSession.Object);

            authService.LogOut();

            mockSession.Verify(s => s.Clear(), Times.Once);
        }

        [Test]
        public void TryLogIn_ReturnsTrue_AndSetsSession_WhenValidCredentials()
        {
            var mockSession = new Mock<ISession>();
            mockHttpContextAccessor.Setup(h => h.HttpContext.Session).Returns(mockSession.Object);

            var users = new Users { Username = "tesztUser", PasswordHash = "hashedPassword" };
            mockUserManager.Setup(u => u.GetAll()).Returns(new List<Users> { users }.AsQueryable());

            var hashedPassword = "hashedPassword";
            mockEncryptionService.Setup(e => e.HashPassword(It.IsAny<string>())).Returns(hashedPassword);

            var result = authService.TryLogIn("tesztUser", "password");

            Assert.IsTrue(result);
            mockSession.Verify(s => s.Set("username", It.IsAny<byte[]>()), Times.Once);
        }
        [Test]
        public void TryLogIn_ReturnsFalse_WhenUserNotFound()
        {
            var mockSession = new Mock<ISession>();
            mockHttpContextAccessor.Setup(h => h.HttpContext.Session).Returns(mockSession.Object);

            mockUserManager.Setup(u => u.GetAll()).Returns(new List<Users>().AsQueryable());

            var result = authService.TryLogIn("nonExistentUser", "password");

            Assert.IsFalse(result);
            mockSession.Verify(s => s.Set(It.IsAny<string>(), It.IsAny<byte[]>()), Times.Never);
        }

    }
}