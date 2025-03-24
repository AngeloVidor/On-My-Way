using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cargo.Api.Business.Services.Interfaces;
using Cargo.Api.Controllers.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Cargo.Api.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class CargoController : ControllerBase
    {
        private readonly ICargoService _cargoService;

        public CargoController(ICargoService cargoService)
        {
            _cargoService = cargoService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCargoAsync([FromBody] CargoDto cargoDto)
        {
            var transporterId = long.Parse(HttpContext.Items["userId"].ToString());
            cargoDto.Transporter_ID = transporterId;

            try
            {
                var result = await _cargoService.CreateCargoAsync(cargoDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}