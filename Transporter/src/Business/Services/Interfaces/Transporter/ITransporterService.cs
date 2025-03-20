using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Controllers.DTOs;
using Transporter.Controllers.DTOs;

namespace Transporter.Business.Services.Interfaces
{
    public interface ITransporterService
    {
        Task<TransporterCompanyDto> RegisterAsync(TransporterCompanyDto transporter);
        Task<LoginDto> LoginAsync(string email, string password);
    }
}