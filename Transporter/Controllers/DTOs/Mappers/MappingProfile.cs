using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Transporter.Infrastructure.Domain;

namespace Transporter.Controllers.DTOs.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TransporterCompany, TransporterCompanyDto>().ReverseMap();
        }
    }
}