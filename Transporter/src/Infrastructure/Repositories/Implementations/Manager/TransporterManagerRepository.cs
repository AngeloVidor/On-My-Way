using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Infrastructure.Repositories.Interfaces.Manager;
using Transporter.Infrastructure.Data;
using Transporter.Infrastructure.Domain;

namespace src.Infrastructure.Repositories.Implementations.Manager
{
    public class TransporterManagerRepository : ITransporterManagerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TransporterManagerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TransporterCompany> GetTransportByEmailAsync(string email)
        {
            return await _dbContext.Transporters.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}