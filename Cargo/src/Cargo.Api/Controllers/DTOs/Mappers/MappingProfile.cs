using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cargo.Api.Infrastructure.Domain;

namespace Cargo.Api.Controllers.DTOs.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CargoEntity, CargoDto>().ReverseMap();
        }
    }
}