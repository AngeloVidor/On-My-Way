using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Cargo.Api.Infrastructure.Data;
using Cargo.Api.Infrastructure.Domain;
using Cargo.Api.Infrastructure.Repositories.Interfaces;

namespace Cargo.Api.Infrastructure.Repositories.Implementations
{
    public class CargoRepository : ICargoRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CargoRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CargoEntity> AddCargoAsync(CargoEntity cargo)
        {
            await _dbContext.Cargos.AddAsync(cargo);
            await _dbContext.SaveChangesAsync();
            return cargo;
        }
    }
}