using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Controllers.DTOs;

namespace src.Business.Services.Interfaces.Tokens
{
    public interface IBearerTokenManagement
    {
        Task<string> GenerateTokenAsync(LoginDto user);
    }
}