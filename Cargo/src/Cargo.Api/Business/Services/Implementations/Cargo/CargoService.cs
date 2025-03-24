using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cargo.Api.Business.Services.Interfaces;
using Cargo.Api.Controllers.DTOs;
using Cargo.Api.Infrastructure.Domain;
using Cargo.Api.Infrastructure.Repositories.Interfaces;

namespace Cargo.Api.Business.Services.Implementations
{
    public class CargoService : ICargoService
    {
        private readonly IMapper _mapper;
        private readonly ICargoRepository _cargoRepository;

        public CargoService(IMapper mapper, ICargoRepository cargoRepository)
        {
            _mapper = mapper;
            _cargoRepository = cargoRepository;
        }

        public async Task<CargoDto> CreateCargoAsync(CargoDto cargo)
        {
            var cargoEntity = _mapper.Map<CargoEntity>(cargo);
            var createdCargo = await _cargoRepository.AddCargoAsync(cargoEntity);
            return _mapper.Map<CargoDto>(createdCargo);
        }
    }
}