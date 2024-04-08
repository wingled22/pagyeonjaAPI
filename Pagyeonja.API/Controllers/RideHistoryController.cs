using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pagyeonja.Entities.Entities;
using Pagyeonja.Services.Services;
using Pagyeonja.Repositories.Repositories.Models;

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

        //This is to get the Ride History of the User selected
        //MUST DO BEFORE : 
        // 1 . Populate the transaction (ride history will be created automatically)
        // 2 . Populate the review with the transaction id created (this will update the ride history review id automatically)

        // GET: api/RideHistory
        [HttpGet("GetUserRideHistory")]
        public async Task<ActionResult<IEnumerable<RideHistoryModel>>> GetUserRideHistory(Guid id, string usertype)
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
        [HttpGet("GetRideHistoryByTransaction")]
        public async Task<ActionResult<RideHistory>> GetRideHistoryByTransaction(Guid id)
        {
            try
            {
                var rideHistory = await _rideHistoryService.GetRideHistoryByTransaction(id);
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