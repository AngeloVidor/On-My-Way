using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transporter.Infrastructure.Data;
using Transporter.Infrastructure.Domain;
using Transporter.Infrastructure.Repositories.Interfaces;

namespace Transporter.Infrastructure.Repositories.Implementations
{
    public class TransporterRepository : ITransporterRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TransporterRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TransporterCompany> RegisterAsync(TransporterCompany transporter)
        {
            await _dbContext.Transporters.AddAsync(transporter);
            await _dbContext.SaveChangesAsync();
            return transporter;
        }
    }
}