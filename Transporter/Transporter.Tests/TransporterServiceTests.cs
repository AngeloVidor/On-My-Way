using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Transporter.Business.Services.Implementations;
using Transporter.Controllers.DTOs;
using Transporter.Infrastructure.Domain;
using Transporter.Infrastructure.Repositories.Interfaces;
using FluentAssertions;
using Xunit;


namespace Transporter.Tests
{
    public class TransporterServiceTests
    {
        private readonly Mock<ITransporterRepository> _mockRepo;
        private readonly TransporterService _service;
        private readonly Mock<IMapper> _mapper;

        public TransporterServiceTests()
        {

            _mockRepo = new Mock<ITransporterRepository>();
            _mapper = new Mock<IMapper>();
            _service = new TransporterService(_mockRepo.Object, _mapper.Object);
        }

        [Fact]
        public async Task RegisterTransporterAsync_WithValidData_ReturnsTransporter()
        {
            // Arrange
            var transporterDto = new TransporterCompanyDto
            {
                Transporter_Id = 1,
                Name = "Transporter 1",
                Email = "email",
                Password = "password",
                CNPJ = "99.999.999/9999-99"
            };

            var transporterEntity = new TransporterCompany
            {
                Transporter_Id = 1,
                Name = "Transporter 1",
                Email = "email",
                Password = "password",
                CNPJ = "99.999.999/9999-99"
            };

            _mockRepo.Setup(repo => repo.RegisterAsync(It.IsAny<TransporterCompany>()))
            .ReturnsAsync(transporterEntity);

            _mapper.Setup(m => m.Map<TransporterCompanyDto>(It.IsAny<TransporterCompany>())).Returns(transporterDto);
            _mapper.Setup(m => m.Map<TransporterCompany>(It.IsAny<TransporterCompanyDto>())).Returns(transporterEntity);

            //Act
            var result = await _service.RegisterAsync(transporterDto);

            //Assert
            result.Should().NotBeNull();
            result.Email.Should().NotBeNullOrWhiteSpace();
            result.Name.Should().NotBeNullOrWhiteSpace();
            result.CNPJ.Should().NotBeNullOrWhiteSpace();
            result.Password.Should().NotBeNullOrWhiteSpace();
            result.Transporter_Id.Should().BeGreaterThan(0);
            result.Transporter_Id.Should().Be(transporterDto.Transporter_Id);

            transporterDto.Email.Should().NotBeNullOrWhiteSpace();
            transporterDto.Name.Should().NotBeNullOrWhiteSpace();
            transporterDto.Password.Should().NotBeNullOrWhiteSpace();
            transporterDto.CNPJ.Should().NotBeNullOrWhiteSpace();
            transporterDto.Transporter_Id.Should().BeGreaterThan(0);


            _mockRepo.Verify(repo => repo.RegisterAsync(It.IsAny<TransporterCompany>()), Times.Once);

        }
    }
}