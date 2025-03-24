using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cargo.Api.Controllers.DTOs;

namespace Cargo.Api.Business.Services.Interfaces
{
    public interface ICargoService
    {
        Task<CargoDto> CreateCargoAsync(CargoDto cargo);
    }
}