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
    public class TopupHistoryController : ControllerBase
    {
        private readonly ITopupHistoryService _topupHistoryService;
        public TopupHistoryController(ITopupHistoryService topupHistoryService)
        {
            _topupHistoryService = topupHistoryService;
        }

        [HttpPost("AddTopupHistory")]
        public async Task<ActionResult<TopupHistory>> AddTopupHistory(TopupHistory topupHistory)
        {
            try
            {
                var addTopupHistory = await _topupHistoryService.AddTopupHistory(topupHistory);
                return CreatedAtAction("AddTopupHistory", new { id = addTopupHistory.TopupId }, addTopupHistory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

       
        [HttpGet("GetTopupHistories")]
        public async Task<ActionResult<IEnumerable<TopupHistory>>> GetTopupHistories()
        {
            try
            {
                var rideHistories = await _topupHistoryService.GetTopupHistories();
                return Ok(rideHistories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

       
        [HttpGet("GetRiderTopupHistory")]
        public async Task<ActionResult<IEnumerable<TopupHistoryModel>>> GetRiderTopupHistory(Guid id)
        {
            try
            {
                var riderTopupHistory = await _topupHistoryService.GetRiderTopupHistory(id);
                return Ok(riderTopupHistory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

       

        [HttpPut("UpdateTopupHistory")]
        public async Task<IActionResult> UpdateTopupHistory(TopupHistory topupHistory)
        {
            try
            {
                if (await _topupHistoryService.TopupHistoryExists(topupHistory.TopupId))
                {
                    var updateTopupHistory = await _topupHistoryService.UpdateTopupHistory(topupHistory);
                    return Ok(updateTopupHistory);
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