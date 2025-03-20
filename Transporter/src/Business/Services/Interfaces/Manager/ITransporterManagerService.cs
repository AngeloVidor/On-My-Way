using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transporter.Controllers.DTOs;

namespace src.Business.Services.Interfaces.Manager
{
    public interface ITransporterManagerService
    {
        Task<TransporterCompanyDto> GetTransportByEmailAsync(string email);
    }
}