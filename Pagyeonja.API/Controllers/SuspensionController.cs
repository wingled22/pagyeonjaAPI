using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pagyeonja.Entities.Entities;
using Pagyeonja.Services.Services;

namespace pagyeonjaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuspensionController : ControllerBase
    {
        private readonly ISuspensionService _suspensionService;
        private readonly HitchContext _context;
        public SuspensionController(ISuspensionService suspensionService)
        {
            _suspensionService = suspensionService;
        }

        // GET: api/Suspension
        [HttpGet("GetSuspensions")]
        public async Task<ActionResult<IEnumerable<Suspension>>> GetSuspensions()
        {
            try
            {
                var suspensions = await _suspensionService.GetSuspensions();
                return Ok(suspensions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        // GET: api/Suspension/5
        [HttpGet("GetSuspension")]
        public async Task<ActionResult<Suspension>> GetSuspension(Guid userid, string usertype)
        {
            try
            {
                var suspension = await _suspensionService.GetSuspension(userid, usertype);
                return Ok(suspension);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }


        // PUT: api/Suspension/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdateSuspension")]
        public async Task<IActionResult> PutSuspension(Guid id, Suspension suspension)
        {
            try
            {
                if (id != suspension.SuspensionId)
                {
                    return BadRequest("ID mismatch");
                }

                var updateSuspension = await _suspensionService.UpdateSuspension(suspension);
                return Ok(updateSuspension);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPut("RevokeSuspension")]
        public async Task<IActionResult> PutSuspension(Suspension Suspension)
        {
            try
            {
                if(await _suspensionService.SuspensionExists(Suspension.SuspensionId))
                {
                    var revokeSuspension = await _suspensionService.RevokeSuspension(Suspension);
                    return Ok(revokeSuspension);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        // POST: api/Suspension
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("RegisterSuspension")]
        public async Task<ActionResult<Suspension>> PostSuspension(Suspension Suspension)
        {
            try
            {
                var invokeSuspension = await _suspensionService.InvokeSuspension(Suspension);
                return CreatedAtAction("PostSuspension", new { id = invokeSuspension.SuspensionId }, invokeSuspension);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        // DELETE: api/Suspension/5
        [HttpDelete("DeleteSuspension")]
        public async Task<IActionResult> DeleteSuspension(Guid id)
        {
            try
            {
                if (await _suspensionService.SuspensionExists(id))
                {
                    var result = await _suspensionService.DeleteSuspension(id);
                    if (!result)
                    {
                        return NotFound();
                    }
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}