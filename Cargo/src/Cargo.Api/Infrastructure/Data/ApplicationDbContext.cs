using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cargo.Api.Infrastructure.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cargo.Api.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<CargoEntity> Cargos { get; set; }
    }
}