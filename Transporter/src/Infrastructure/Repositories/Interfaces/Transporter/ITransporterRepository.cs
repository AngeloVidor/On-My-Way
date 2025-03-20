using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transporter.Infrastructure.Domain;

namespace Transporter.Infrastructure.Repositories.Interfaces
{
    public interface ITransporterRepository
    {
        Task<TransporterCompany> RegisterAsync(TransporterCompany transporter);
    }
}