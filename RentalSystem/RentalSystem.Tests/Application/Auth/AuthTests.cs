using Microsoft.AspNetCore.Mvc;
using Moq;
using RentalSystem.Api.Controllers;
using RentalSystem.Application.DTOs.Authentication;
using RentalSystem.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RentalSystem.Tests.Application.Commands.Authenticate
{
    public class AuthTests
    {
        [Theory]
        [InlineData("admin", "password", "Admin")]
        [InlineData("user", "password", "User")]
        public void Login_ShouldReturnToken_WhenCredentialsAreValid(string username, string password, string expectedRole)
        {
            // Arrange
            var token = "fake-jwt-token";
            var jwtServiceMock = new Mock<IJwtTokenService>();

            jwtServiceMock
                .Setup(s => s.GenerateToken(username, expectedRole))
                .Returns(token);

            var controller = new AuthController(jwtServiceMock.Object);

            var loginDto = new LoginDto(username, password);


            // Act
            var result = controller.Login(loginDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<AuthResponseDto>(okResult.Value);
            Assert.Equal(token, response.Token);
        }

        [Theory]
        [InlineData("admin", "wrong")]
        [InlineData("user", "badpass")]
        [InlineData("unknown", "password")]
        public void Login_ShouldReturnUnauthorized_WhenCredentialsAreInvalid(string username, string password)
        {
            // Arrange
            var jwtServiceMock = new Mock<IJwtTokenService>();
            var controller = new AuthController(jwtServiceMock.Object);

            var loginDto = new LoginDto(username, password);
               

            // Act
            var result = controller.Login(loginDto);

            // Assert
            var unauthorized = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Invalid credentials", unauthorized.Value);
        }
    }
}
