using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using QuizManager.Core.User;
using QuizManager.Types.Account;
using System.Net;
using System.Threading.Tasks;

namespace QuizManager.Api.UnitTests.Controllers.Account
{
    public class WhenPostingRegisterUser : AccountTestBase
    {
        [SetUp]
        public void Arrange()
        {
            BaseArrange();

            _registerDto = _autoFixture.Create<RegisterUserDto>();
        }

        [Test]
        public async Task And_RegistrationIsSuccessful_ReturnOk()
        {
            SetupUserRegistrationResponse(true);

            var result = await _controller.RegisterUser(_registerDto) as OkObjectResult;

            result.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        public async Task And_RegistrationIsUnsuccessful_ReturnBadRequest()
        {
            SetupUserRegistrationResponse(false);

            var result = await _controller.RegisterUser(_registerDto) as ObjectResult;

            result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        private void SetupUserRegistrationResponse(bool registrationSuccessful)
        {
            var result = new MockIdentityResult(registrationSuccessful);

            _mockUserManager
                .Setup(um => um.CreateAsync(It.Is<QuizManagerUser>(u => u.Email == _registerDto.EmailAddress), It.IsAny<string>()))
                .ReturnsAsync(result);
        }
    }
}
