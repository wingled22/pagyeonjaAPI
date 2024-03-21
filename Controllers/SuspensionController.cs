using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pagyeonjaAPI.Entities;

namespace pagyeonjaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuspensionController : ControllerBase
    {
        private readonly HitchContext _context;
        public SuspensionController(HitchContext context)
        {
            _context = context;
        }

        // GET: api/Suspension
        [HttpGet("GetSuspensions")]
        public async Task<ActionResult<IEnumerable<Suspension>>> GetSuspensions()
        {
            if (_context.Suspensions == null)
            {
                return NotFound();
            }

            return await _context.Suspensions.OrderByDescending(a => a.SuspensionId).ToListAsync();
        }

        // GET: api/Suspension/5
        [HttpGet("GetSuspension")]
        public async Task<ActionResult<Suspension>> GetSuspension(Guid id)
        {
            if (_context.Suspensions == null)
            {
                return NotFound();
            }
            var Suspension = await _context.Suspensions.FindAsync(id);

            if (Suspension == null)
            {
                return NotFound();
            }

            return Suspension;
        }

        // PUT: api/Suspension/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdateSuspension")]
        public async Task<IActionResult> PutSuspension(Guid id, Suspension Suspension)
        {
            if (id != Suspension.SuspensionId)
            {
                return BadRequest();
            }

            _context.Entry(Suspension).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuspensionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Suspension
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("RegisterSuspension")]
        public async Task<ActionResult<Suspension>> PostSuspension(Suspension Suspension)
        {
            try
            {
                Suspension.SuspensionId = Guid.NewGuid();
                while (await _context.Suspensions.AnyAsync(r => r.SuspensionId == Suspension.SuspensionId))
                {
                    Suspension.SuspensionId = Guid.NewGuid();
                }

                _context.Suspensions.Add(Suspension);
                await _context.SaveChangesAsync();
                return CreatedAtAction("PostSuspension", new { id = Suspension.SuspensionId }, Suspension);

            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Unhandled Error occured: " + ex);
            }
        }

        // DELETE: api/Suspension/5
        [HttpDelete("DeleteSuspension")]
        public async Task<IActionResult> DeleteSuspension(Guid id)
        {
            if (_context.Suspensions == null)
            {
                return NotFound();
            }
            var Suspension = await _context.Suspensions.FindAsync(id);
            if (Suspension == null)
            {
                return NotFound();
            }

            _context.Suspensions.Remove(Suspension);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SuspensionExists(Guid id)
        {
            return (_context.Suspensions?.Any(e => e.SuspensionId == id)).GetValueOrDefault();
        }
    }
}