using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AirportDistanceApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirportDistanceApi.Controllers
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    public class AirportController : ControllerBase
    {
        private readonly IAirportService _airportService;

        public AirportController(IAirportService airportService)
        {
            _airportService = airportService;
        }

        [HttpGet("airport/getdistance")]
        [ProducesResponseType(typeof(double), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDistance([FromQuery] string fromIata, [FromQuery] string toIata)
        {
            if (!_airportService.ValidateIata(fromIata) || !_airportService.ValidateIata(toIata))
                return BadRequest();
            try
            {
                var distance = await _airportService.GetDistance(fromIata, toIata);

                return Ok(distance);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal Server Error. Message : {ex.Message}");
            }
        }

        
    }
}
