using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using src.Business.Services.Interfaces.Manager;
using src.Business.Services.Interfaces.Tokens;
using src.Controllers.DTOs;

namespace src.Business.Services.Implementations.Tokens
{
    public class BearerTokenManagement : IBearerTokenManagement
    {
        private readonly IConfiguration _configuration;
        private readonly ITransporterManagerService _managerService;

        public BearerTokenManagement(IConfiguration configuration, ITransporterManagerService managerService)
        {
            _configuration = configuration;
            _managerService = managerService;
        }

        public async Task<string> GenerateTokenAsync(LoginDto user)
        {

            var identity = await _managerService.GetTransportByEmailAsync(user.Email);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, identity.Transporter_Id.ToString()),
                new Claim(ClaimTypes.Name, identity.Name),
            };

            var secretKey = _configuration["Jwt:Key"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                            issuer: _configuration["Jwt:Issuer"],
                            audience: _configuration["Jwt:Audience"],
                            claims: claims,
                            expires: DateTime.UtcNow.AddMinutes(
                                _configuration.GetValue<int>("Jwt:DurationInMinutes")
                            ),
                            signingCredentials: creds
                        );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}