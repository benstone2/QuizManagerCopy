using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using QuizManager.Types.Account;
using System.Net;
using System.Threading.Tasks;

namespace QuizManager.Api.UnitTests.Controllers.Account
{
    public class WhenPostingSignIn : AccountTestBase
    {
        [SetUp]
        public void Arrange()
        {
            BaseArrange();

            _signInUserDto = _autoFixture.Create<SignInUserDto>();
        }

        [Ignore("Further setup of token retrieval required")]
        [Test]
        public async Task And_SignInIsSuccessful_Then_ReturnOk()
        {
            SetupUserSignInResponse(true);

            var result = await _controller.SignIn(_signInUserDto) as OkObjectResult;

            result.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        public async Task And_SignInIsUnsuccessfull_Then_ReturnUnauthorized()
        {
            SetupUserSignInResponse(false);

            var result = await _controller.SignIn(_signInUserDto) as UnauthorizedObjectResult;

            result.StatusCode.Should().Be((int)HttpStatusCode.Unauthorized);
        }

        private void SetupUserSignInResponse(bool signInSuccessful)
        {
            var signInResult = new MockSignInResult(signInSuccessful);

            _mockSignInManager
                .Setup(um => um.PasswordSignInAsync(_signInUserDto.EmailAddress, _signInUserDto.Password, false, false))
                .ReturnsAsync(signInResult);
        }
    }
}
