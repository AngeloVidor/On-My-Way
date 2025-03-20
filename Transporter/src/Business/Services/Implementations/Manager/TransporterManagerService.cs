using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using src.Business.Services.Interfaces.Manager;
using src.Infrastructure.Repositories.Interfaces.Manager;
using Transporter.Controllers.DTOs;

namespace src.Business.Services.Implementations.Manager
{
    public class TransporterManagerService : ITransporterManagerService
    {
        private readonly ITransporterManagerRepository _managerRepository;
        private readonly IMapper _mapper;
        public TransporterManagerService(ITransporterManagerRepository managerRepository, IMapper mapper)
        {
            _managerRepository = managerRepository;
            _mapper = mapper;
        }

        public async Task<TransporterCompanyDto> GetTransportByEmailAsync(string email)
        {
            var transporter = await _managerRepository.GetTransportByEmailAsync(email);
            return _mapper.Map<TransporterCompanyDto>(transporter);
        }
    }
}