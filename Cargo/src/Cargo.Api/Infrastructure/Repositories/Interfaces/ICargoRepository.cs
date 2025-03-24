using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cargo.Api.Infrastructure.Domain;

namespace Cargo.Api.Infrastructure.Repositories.Interfaces
{
    public interface ICargoRepository
    {
        Task<CargoEntity> AddCargoAsync(CargoEntity cargo);
    }
}