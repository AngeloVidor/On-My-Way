using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.Business.Services.Interfaces.Tokens;
using Transporter.Business.Services.Interfaces;
using Transporter.Controllers.DTOs;

namespace Transporter.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransporterController : ControllerBase
    {
        private readonly ITransporterService _transporterService;
        private readonly IBearerTokenManagement _bearerTokenManagement;

        public TransporterController(ITransporterService transporterService, IBearerTokenManagement bearerTokenManagement)
        {
            _transporterService = transporterService;
            _bearerTokenManagement = bearerTokenManagement;
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] TransporterCompanyDto transporter)
        {
            try
            {
                var result = await _transporterService.RegisterAsync(transporter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(string email, string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var loggedInUser = await _transporterService.LoginAsync(email, password);
                var token = await _bearerTokenManagement.GenerateTokenAsync(loggedInUser);
                return Ok(new { Token = token, User = loggedInUser });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }



}