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


        public TransporterServiceTests()
        {

            // Criando um mock (simulação) do repositório para evitar acessos reais ao banco de dados
            _mockRepo = new Mock<ITransporterRepository>();

            // Inicializando o serviço e injetando o mock do repositório como dependência
            _service = new TransporterService(_mockRepo.Object, new Mock<IMapper>().Object);
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
                CNPJ = "99.999.999/9999-99",
                Password = "password"
            };

            var transporterEntity = new TransporterCompany
            {
                Transporter_Id = 1,
                Name = "Transporter 1",
                Email = "email",
                CNPJ = "99.999.999/9999-99",
                Password = "password"
            };

            _mockRepo.Setup(repo => repo.RegisterAsync(It.IsAny<TransporterCompany>()))
            .ReturnsAsync(transporterEntity);

            //Act
            var result = await _service.RegisterAsync(transporterDto);

            //Assert
            result.Should().NotBeNull();
            result.Transporter_Id.Should().Be(transporterDto.Transporter_Id); 
            _mockRepo.Verify(repo => repo.RegisterAsync(It.IsAny<TransporterCompany>()), Times.Once);

        }
    }
}