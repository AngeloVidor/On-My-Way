using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using src.Controllers.DTOs;
using src.Infrastructure.Repositories.Interfaces.Manager;
using Transporter.Business.Services.Interfaces;
using Transporter.Controllers.DTOs;
using Transporter.Infrastructure.Domain;
using Transporter.Infrastructure.Repositories.Interfaces;

namespace Transporter.Business.Services.Implementations
{
    public class TransporterService : ITransporterService
    {
        private readonly ITransporterRepository _transporterRepository;
        private readonly ITransporterManagerRepository _transporterManagerRepo;
        private readonly IMapper _mapper;

        public TransporterService(ITransporterRepository transporterRepository, IMapper mapper, ITransporterManagerRepository transporterManagerRepo)
        {
            _transporterRepository = transporterRepository;
            _mapper = mapper;
            _transporterManagerRepo = transporterManagerRepo;
        }

        public async Task<LoginDto> LoginAsync(string email, string password)
        {
            var transporter = await _transporterManagerRepo.GetTransportByEmailAsync(email);
            if (transporter == null || !BCrypt.Net.BCrypt.Verify(password, transporter.Password))
            {
                return null;
            }
            return _mapper.Map<LoginDto>(transporter);
        }

        public async Task<TransporterCompanyDto> RegisterAsync(TransporterCompanyDto transporter)
        {
            transporter.Password = BCrypt.Net.BCrypt.HashPassword(transporter.Password);

            var transporterEntity = _mapper.Map<TransporterCompany>(transporter);
            var result = await _transporterRepository.RegisterAsync(transporterEntity);
            return _mapper.Map<TransporterCompanyDto>(result);
        }
    }
}