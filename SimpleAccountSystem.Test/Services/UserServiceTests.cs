using AutoFixture;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleAccountSystem.Domain.Service;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace SimpleAccountSystem.Test.Services
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class UserServiceTests
    {
        // real objects
        private Fixture? _fixture;
        private UserService? _userService;
        private UserManager<IdentityUser>? _userManager;

        //mock objects
        private Mock<IUserStore<IdentityUser>>? _userStoreMock;
        private Mock<UserManager<IdentityUser>>? _userManagerMock;

        [TestInitialize]
        public void Setup()
        {
            _fixture = new Fixture();
            _userStoreMock = new Mock<IUserStore<IdentityUser>>();
            _userManagerMock = new Mock<UserManager<IdentityUser>>(_userStoreMock?.Object, 
                null,
                null, 
                null, 
                null, 
                null,
                null, 
                null,
                null);

            _userService = new UserService(_userManagerMock.Object);
        }

        #region GetUserId Method Unit Test

        [TestMethod]
        public void Given_GetUserId_When_HasValid_ClaimsPrincipal_Then_ShouldReturn_UserId()
        {
            //Arrange
            var fakeUserId = _fixture.Create<string>();
            var fakeClaimsPrincipal = _fixture.Create<ClaimsPrincipal>();

            _userManagerMock?.Setup(i => i.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns(fakeUserId);

            //Act
            var result = _userService?.GetUserId(fakeClaimsPrincipal);

            //Assert
            Assert.IsNotNull(result);
            _userManagerMock?.Verify(i => i.GetUserId(It.IsAny<ClaimsPrincipal>()), Times.Once);

        }

        #endregion
    }
}
