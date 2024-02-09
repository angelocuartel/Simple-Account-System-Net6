using AutoFixture;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleAccountSystem.Domain.Service;
using SimpleAccountSystem.Dto.Request;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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

        //mock objects
        private Mock<IUserStore<IdentityUser>>? _userStoreMock;
        private Mock<IRoleStore<IdentityRole>>? _roleStoreMock;

        private Mock<UserManager<IdentityUser>>? _userManagerMock;
        private Mock<RoleManager<IdentityRole>>? _roleManagerMock;

        [TestInitialize]
        public void Setup()
        {
            _fixture = new Fixture();
            _userStoreMock = new Mock<IUserStore<IdentityUser>>();
            _roleStoreMock = new Mock<IRoleStore<IdentityRole>>();

            _userManagerMock = new Mock<UserManager<IdentityUser>>(_userStoreMock?.Object,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null);

            _roleManagerMock= new Mock<RoleManager<IdentityRole>>(_roleStoreMock.Object,
                null,
                null,
                null,
                null);

            _userService = new UserService(_userManagerMock.Object, _roleManagerMock.Object);
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

        #region GetUsers Method Unit Test

        [TestMethod]
        public void Given_GetUsers_When_HasValid_RecordCount_Then_ReturnUsers()
        {
            //Arrange
            var recordCount = 5;
            var fakeUsers = _fixture
                .CreateMany<IdentityUser>(10)
                .AsQueryable();

            _userManagerMock?.Setup(i => i.Users).Returns(fakeUsers);

            //Act
            var result = _userService?.GetUsers(recordCount);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(),recordCount);
            _userManagerMock?.Verify(i => i.Users, Times.Once);
        }

        [TestMethod]
        public void Given_GetUsers_When_HasValid_RecordCount_And_Filter_Then_ReturnUsers()
        {
            //Arrange
            var recordCount = 5;
            var fakeUsers = _fixture
                .CreateMany<IdentityUser>(10)
                .AsQueryable();

            var filter = fakeUsers.FirstOrDefault()?
                .UserName;

            _userManagerMock?.Setup(i => i.Users).Returns(fakeUsers);

            //Act
            var result = _userService?.GetUsers(recordCount, filter);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 1);
            _userManagerMock?.Verify(i => i.Users, Times.Once);
        }

        #endregion

        #region AddUserAsync Method Unit Test

        [TestMethod]
        public async Task Given_AddUserAsync_When_HasValid_IdentityUserRequestDto_Then_CreateUser()
        {
            //Arrange
            var fakeIdentityUserRequestDto = _fixture.Create<IdentityUserRequestDto>();
            var fakeIdentityResult = _fixture?
                .Build<IdentityResult>()
                .Create();

            _userManagerMock?.Setup(i => i.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(fakeIdentityResult);

            //Act
            var result = await _userService?.AddUserAsync(fakeIdentityUserRequestDto);
            

            //Assert
            Assert.IsNotNull(result);
            _userManagerMock?.Verify(i => i.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()),Times.Once);

        }

        #endregion

        #region GetRoles Method Unit Test

        [TestMethod]
        public void Given_GetRoles_When_HasNoFilter_Then_Return_Roles()
        {
            //Arrange
            var fakeRoles = _fixture
                .CreateMany<IdentityRole>(5)
                .AsQueryable();

            var fakeRolesCount = fakeRoles.Count();

            _roleManagerMock?.Setup(i => i.Roles).Returns(fakeRoles);

            //Act
            var result = _userService?.GetRoles(fakeRolesCount);

            //Assert
            Assert.AreEqual(fakeRolesCount, result?.Count());

            _roleManagerMock?.Verify(i => i.Roles, Times.Once);
        }

        [TestMethod]
        public void Given_GetRoles_When_HasFilter_Then_Return_Matched_Roles()
        {
            //Arrange
            var fakeRoles = _fixture
                .CreateMany<IdentityRole>(5)
                .AsQueryable();

            var fakeFilter = fakeRoles.FirstOrDefault()?.Name;

            var fakeRolesCount = fakeRoles.Count();

            _roleManagerMock?.Setup(i => i.Roles).Returns(fakeRoles);

            //Act
            var result = _userService?.GetRoles(fakeRolesCount, fakeFilter);

            //Assert
            Assert.AreEqual(1, result?.Count());
            Assert.AreEqual(fakeFilter, result?.FirstOrDefault()?.Name);

            _roleManagerMock?.Verify(i => i.Roles, Times.Once);
        }

        #endregion
    }
}
