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

        // GET: api/RideHistory
        [HttpGet("GetRideHistories")]
        public async Task<ActionResult<IEnumerable<RideHistory>>> GetRideHistories()
        {
            try
            {
                var rideHistories = await _rideHistoryService.GetRideHistories();
                return Ok(rideHistories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        // GET: api/RideHistory
        [HttpGet("GetUserRideHistory")]
        public async Task<ActionResult<IEnumerable<RideHistory>>> GetUserRideHistory(Guid id, string usertype)
        {
            try
            {
                var userRideHistories = await _rideHistoryService.GetUserRideHistory(id, usertype);
                return Ok(userRideHistories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

         // GET: api/RideHistory/5
        [HttpGet("GetRideHistory")]
        public async Task<ActionResult<RideHistory>> GetRideHistory(Guid id)
        {
            try
            {
                var rideHistory = await _rideHistoryService.GetRideHistory(id);
                return Ok(rideHistory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPut("UpdateRideHistory")]
        public async Task<IActionResult> UpdateRideHistory(RideHistory rideHistory)
        {
            try
            {
                if (await _rideHistoryService.RideHistoryExists(rideHistory.RideHistoryId))
                {
                    var updateRideHistory = await _rideHistoryService.UpdateRideHistory(rideHistory);
                    return Ok(updateRideHistory);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}