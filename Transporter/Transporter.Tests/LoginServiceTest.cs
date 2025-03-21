using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Moq;
using src.Controllers.DTOs;
using src.Infrastructure.Repositories.Interfaces.Manager;
using Transporter.Business.Services.Implementations;
using Transporter.Infrastructure.Domain;
using Transporter.Infrastructure.Repositories.Interfaces;
using Xunit;

namespace Transporter.Tests
{
    public class LoginServiceTest
    {
        private readonly Mock<ITransporterRepository> _transporterRepository;
        private readonly Mock<ITransporterManagerRepository> _transporterManagerRepo;
        private readonly Mock<IMapper> _mapper;
        private readonly TransporterService _service;


        public LoginServiceTest()
        {
            _transporterRepository = new Mock<ITransporterRepository>();
            _transporterManagerRepo = new Mock<ITransporterManagerRepository>();
            _mapper = new Mock<IMapper>();

            _service = new TransporterService(_transporterRepository.Object, _mapper.Object, _transporterManagerRepo.Object);
        }

        [Fact]
        public async Task TransporterLoginAsync_WithValidCredentials_ReturnsLoginDto()
        {
            //arrange
            var email = "test@example.com";
            var password = "password";
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var transporter = new TransporterCompany
            {
                Transporter_Id = 1,
                Email = email,
                Password = hashedPassword,
                Name = "name",
                CNPJ = "12345678901234"
            };

            var expectedLogin = new LoginDto
            {
                Email = email,
                Password = hashedPassword
            };

            _transporterManagerRepo.Setup(x => x.GetTransportByEmailAsync(email)).ReturnsAsync(transporter);
            _mapper.Setup(x => x.Map<LoginDto>(transporter)).Returns(expectedLogin);

            var result = await _service.LoginAsync(email, password);

            result.Should().NotBeNull();
            result.Email.Should().Be(expectedLogin.Email);
            result.Password.Should().Be(expectedLogin.Password);

            _transporterManagerRepo.Verify(x => x.GetTransportByEmailAsync(email), Times.Once);
        }

        [Fact]
        public async Task TransporterLoginAsync_WithInvalidPassword_ReturnsNull()
        {
            // Arrange
            var email = "test@example.com";
            var wrongPassword = "wrongpassword";

            var transporter = new TransporterCompany
            {
                Transporter_Id = 1,
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword("correctpassword"),
                Name = "name",
                CNPJ = "12345678901234"
            };

            _transporterManagerRepo.Setup(x => x.GetTransportByEmailAsync(email)).ReturnsAsync(transporter);

            // Act
            var result = await _service.LoginAsync(email, wrongPassword);

            result.Should().BeNull();

            _transporterManagerRepo.Verify(x => x.GetTransportByEmailAsync(email), Times.Once);
        }

        [Fact]
        public async Task TransporterLoginAsync_WithNonExistingEmail_ReturnsNull()
        {
            // Arrange
            var email = "nonexistent@example.com";
            var password = "password";

            _transporterManagerRepo.Setup(x => x.GetTransportByEmailAsync(email)).ReturnsAsync((TransporterCompany)null);

            // Act
            var result = await _service.LoginAsync(email, password);

            // Assert
            result.Should().BeNull();

            _transporterManagerRepo.Verify(x => x.GetTransportByEmailAsync(email), Times.Once);
        }



    }
}