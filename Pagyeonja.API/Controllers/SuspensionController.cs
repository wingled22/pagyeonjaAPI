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
        // [HttpGet("GetSuspension")]
        // public async Task<ActionResult<Suspension>> GetSuspension(Guid userid, string usertype)
        // {
        //     if (_context.Suspensions == null)
        //     {
        //         return NotFound();
        //     }

        //     var Suspension = await _context.Suspensions
        //         .Where(s => s.UserId == userid && s.UserType == usertype && s.SuspensionDate >= DateTime.Now && s.Status == true)
        //         .OrderBy(s => s.InvokedSuspensionDate)
        //         .FirstOrDefaultAsync();

        //     return Suspension;
        // }


        // // PUT: api/Suspension/5
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("UpdateSuspension")]
        // public async Task<IActionResult> PutSuspension(Guid id, Suspension Suspension)
        // {
        //     if (id != Suspension.SuspensionId)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(Suspension).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!SuspensionExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        // [HttpPut("RevokeSuspension")]
        // public async Task<IActionResult> PutSuspension(Suspension Suspension)
        // {
        //     try
        //     {
        //         if(Suspension.UserType == "Commuter")
        //         {
        //             var User = _context.Commuters.Where(c => c.CommuterId == Suspension.UserId).FirstOrDefault();
        //             if(User == null)
        //             {
        //                 return BadRequest();
        //             }

        //             User.SuspensionStatus = false;
        //         }
        //         else if(Suspension.UserType == "Rider")
        //         {
        //             var User = _context.Riders.Where(r => r.RiderId == Suspension.UserId).FirstOrDefault();
        //             if(User == null)
        //             {
        //                 return BadRequest();
        //             }

        //             User.SuspensionStatus = false;
        //         }

        //         var suspensionData = _context.Suspensions.Where(s => s.SuspensionId == Suspension.SuspensionId).FirstOrDefault();
        //         suspensionData.Status = false;

        //         await _context.SaveChangesAsync();
        //     }
        //     catch(Exception ex)
        //     {
        //         return new BadRequestObjectResult("Unhandled Error occured: " + ex);
        //     }

        //     return NoContent();
        // }

        // // POST: api/Suspension
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPost("RegisterSuspension")]
        // public async Task<ActionResult<Suspension>> PostSuspension(Suspension Suspension)
        // {
        //     try
        //     {
        //         Suspension.SuspensionId = Guid.NewGuid();
        //         while (await _context.Suspensions.AnyAsync(r => r.SuspensionId == Suspension.SuspensionId))
        //         {
        //             Suspension.SuspensionId = Guid.NewGuid();
        //         }

        //         //add when did the suspension invoked
        //         Suspension.InvokedSuspensionDate = DateTime.Now;
        //         Suspension.Status = true;

        //         _context.Suspensions.Add(Suspension);

        //         //Update the user based on the usertype and userid and set the suspension status to true
        //         if(Suspension.UserType == "Commuter")
        //         {
        //             var User = _context.Commuters.Where(c => c.CommuterId == Suspension.UserId).FirstOrDefault();
        //             User.SuspensionStatus = true;
        //         }
        //         else if(Suspension.UserType == "Rider")
        //         {
        //             var User = _context.Riders.Where(r => r.RiderId == Suspension.UserId).FirstOrDefault();
        //             User.SuspensionStatus = true;
        //         }

        //         await _context.SaveChangesAsync();

        //         return CreatedAtAction("PostSuspension", new { id = Suspension.SuspensionId }, Suspension);

        //     }
        //     catch (Exception ex)
        //     {
        //         return new BadRequestObjectResult("Unhandled Error occured: " + ex);
        //     }
        // }

        // // DELETE: api/Suspension/5
        // [HttpDelete("DeleteSuspension")]
        // public async Task<IActionResult> DeleteSuspension(Guid id)
        // {
        //     if (_context.Suspensions == null)
        //     {
        //         return NotFound();
        //     }
        //     var Suspension = await _context.Suspensions.FindAsync(id);
        //     if (Suspension == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.Suspensions.Remove(Suspension);
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }

        // private bool SuspensionExists(Guid id)
        // {
        //     return (_context.Suspensions?.Any(e => e.SuspensionId == id)).GetValueOrDefault();
        // }
    }
}