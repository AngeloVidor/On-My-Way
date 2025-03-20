using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transporter.Infrastructure.Domain;

namespace src.Infrastructure.Repositories.Interfaces.Manager
{
    public interface ITransporterManagerRepository
    {
        Task<TransporterCompany> GetTransportByEmailAsync(string email);
    }
}