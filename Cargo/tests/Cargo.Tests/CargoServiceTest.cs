using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using FluentAssertions;
using Cargo.Api.Infrastructure.Domain;
using AutoMapper;
using Cargo.Api.Controllers.DTOs;
using Cargo.Api.Infrastructure.Repositories.Interfaces;
using Cargo.Api.Business.Services.Implementations;


namespace Cargo.Tests
{
    public class CargoServiceTest
    {
        private readonly Mock<ICargoRepository> _cargoRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CargoService _cargoService;

        public CargoServiceTest()
        {
            _cargoRepositoryMock = new Mock<ICargoRepository>();
            _mapperMock = new Mock<IMapper>();
            _cargoService = new CargoService(_mapperMock.Object, _cargoRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateCargo_ShouldAddCargo_WhenValidDataIsProvided()
        {
            //Arrange
            var transporter_ID = 1;
            var vehicle_ID = 1;
            var cargo_ID = 1;

            var cargoDto = new CargoDto
            {
                Transporter_ID = transporter_ID,
                Vehicle_ID = vehicle_ID,
                Cargo_ID = cargo_ID
            };

            var cargoEntity = new CargoEntity
            {
                Transporter_ID = transporter_ID,
                Vehicle_ID = vehicle_ID,
                Cargo_ID = cargo_ID
            };

            _mapperMock.Setup(x => x.Map<CargoEntity>(cargoDto)).Returns(cargoEntity);
            _mapperMock.Setup(x => x.Map<CargoDto>(cargoEntity)).Returns(cargoDto);
            _cargoRepositoryMock.Setup(x => x.AddCargoAsync(It.IsAny<CargoEntity>())).ReturnsAsync(cargoEntity);

            //Act
            var result = await _cargoService.CreateCargoAsync(cargoDto);
            result.Should().BeOfType<CargoDto>();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CargoDto>();
            result.Transporter_ID.Should().Be(transporter_ID);
            result.Cargo_ID.Should().Be(cargo_ID);
            result.Vehicle_ID.Should().Be(vehicle_ID);

            _cargoRepositoryMock.Verify(x => x.AddCargoAsync(It.IsAny<CargoEntity>()), Times.Once);
        }
    }
}