using AutoFixture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using QuizManager.Api.Controllers;
using QuizManager.Core.User;
using QuizManager.Types.Account;

namespace QuizManager.Api.UnitTests.Controllers.Account
{
    public class AccountTestBase
    {
        protected Fixture _autoFixture;

        protected RegisterUserDto _registerDto;
        protected SignInUserDto _signInUserDto;

        protected Mock<SignInManager<QuizManagerUser>> _mockSignInManager;
        protected Mock<UserManager<QuizManagerUser>> _mockUserManager;

        protected AccountController _controller;

        public void BaseArrange()
        {
            _autoFixture = new Fixture();

            _mockUserManager = new Mock<UserManager<QuizManagerUser>>(Mock.Of<IUserStore<QuizManagerUser>>(),
                null, null, null, null, null, null, null, null);

            _mockSignInManager = new Mock<SignInManager<QuizManagerUser>>(_mockUserManager.Object,
                           Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<QuizManagerUser>>(), null, null, null, null);

            _controller = new AccountController(_mockSignInManager.Object, _mockUserManager.Object, Mock.Of<IConfiguration>());
        }
    }

    public class MockIdentityResult : IdentityResult
    {
        public MockIdentityResult(bool succeeded = false)
        {
            Succeeded = succeeded;
        }
    }

    public class MockSignInResult : Microsoft.AspNetCore.Identity.SignInResult
    {
        public MockSignInResult(bool succeeded = false)
        {
            Succeeded = succeeded;
        }
    }
}
