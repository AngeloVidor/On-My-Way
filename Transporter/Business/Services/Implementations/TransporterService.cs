using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Transporter.Business.Services.Interfaces;
using Transporter.Controllers.DTOs;
using Transporter.Infrastructure.Domain;
using Transporter.Infrastructure.Repositories.Interfaces;

namespace Transporter.Business.Services.Implementations
{
    public class TransporterService : ITransporterService
    {
        private readonly ITransporterRepository _transporterRepository;
        private readonly IMapper _mapper;

        public TransporterService(ITransporterRepository transporterRepository, IMapper mapper)
        {
            _transporterRepository = transporterRepository;
            _mapper = mapper;
        }

        public async Task<TransporterCompanyDto> RegisterAsync(TransporterCompanyDto transporter)
        {
            var transporterEntity = _mapper.Map<TransporterCompany>(transporter);
            var result = await _transporterRepository.RegisterAsync(transporterEntity);
            return _mapper.Map<TransporterCompanyDto>(result);
        }
    }
}