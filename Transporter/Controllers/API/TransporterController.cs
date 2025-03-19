using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Transporter.Business.Services.Interfaces;
using Transporter.Controllers.DTOs;

namespace Transporter.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransporterController : ControllerBase
    {
        private readonly ITransporterService _transporterService;

        public TransporterController(ITransporterService transporterService)
        {
            _transporterService = transporterService;
        }


        [HttpPost]
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
    }
}