using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pagyeonja.Entities.Entities;
using Pagyeonja.Services.Services;

namespace Pagyeonja.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RideHistoryController : ControllerBase
    {
        private readonly IRideHistoryService _rideHistoryService;
        public RideHistoryController(IRideHistoryService rideHistoryService)
        {
            _rideHistoryService = rideHistoryService;
        }

        [HttpPost("AddRideHistory")]
        private async Task<ActionResult<RideHistory>> AddRideHistory(RideHistory rideHistory)
        {
            try
            {
                var createRideHistory = await _rideHistoryService.AddRideHistory(rideHistory);
                return CreatedAtAction("AddRideHistory", new { id = createRideHistory.RideHistoryId }, createRideHistory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}