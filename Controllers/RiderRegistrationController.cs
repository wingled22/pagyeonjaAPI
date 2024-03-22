using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pagyeonjaAPI.Entities;

namespace pagyeonjaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RiderRegistrationController : ControllerBase
    {
        private readonly HitchContext _context;

        public RiderRegistrationController(HitchContext context)
        {
            _context = context;
        }

        // GET: api/Riders
        [HttpGet("GetRiders")]
        public async Task<ActionResult<IEnumerable<Rider>>> GetRiders()
        {
            if (_context.Riders == null)
            {
                return NotFound();
            }
            return await _context.Riders.OrderByDescending(a => a.DateApplied).ToListAsync();
        }

        [HttpGet("GetRidersApproved")]
        public async Task<ActionResult<IEnumerable<Rider>>> GetRidersApproved()
        {
            if (_context.Riders == null)
            {
                return NotFound();
            }

            return await _context.Riders.Where(a => a.ApprovalStatus == true).OrderByDescending(a => a.DateApplied).ToListAsync();
        }


        // GET: api/Rider/5
        [HttpGet("GetRider")]
        public async Task<ActionResult<Rider>> GetRider(int id)
        {
            if (_context.Riders == null)
            {
                return NotFound();
            }
            var Rider = await _context.Riders.FindAsync(id);

            if (Rider == null)
            {
                return NotFound();
            }

            return Rider;
        }

        // PUT: api/Rider/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdateRider")]
        public async Task<IActionResult> PutRider(Guid id, Rider Rider)
        {
            if (id != Rider.RiderId)
            {
                return BadRequest();
            }

            _context.Entry(Rider).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RiderExists(id))
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

        // POST: api/Rider
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("RegisterRider")]
        public async Task<ActionResult<Rider>> PostRider([FromForm] Rider rider, [FromForm] List<IFormFile> images)
        {
            try
            {
                if (images != null)
                {
                    var fileNames = await SaveImages(images);
                    rider.ProfilePath = string.Join(";", fileNames);
                }

                // generate riderid for rider
                var riderId = Guid.NewGuid();
                do
                {
                    rider.RiderId = riderId;
                } while (await _context.Riders.AnyAsync(r => r.RiderId == riderId));
                rider.DateApplied = new DateTime();

                // Create rider approval
                var approval = new Approval()
                {
                    UserId = rider.RiderId,
                    UserType = "Rider",
                    ApprovalDate = null,
                };
                // generate riderid for rider
                var approvalId = Guid.NewGuid();
                do
                {
                    approval.Id = approvalId;
                } while (await _context.Approvals.AnyAsync(a => a.Id == approvalId));

                await _context.Riders.AddAsync(rider);
                await _context.Approvals.AddAsync(approval);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    // Log the exception here
                    return StatusCode(500, "An error occurred while saving to the database.");
                }

                return CreatedAtAction("PostRider", new { id = rider.RiderId }, rider);
            }
            catch (Exception)
            {
                // Log the exception here
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        private static async Task<List<string>> SaveImages(List<IFormFile> images)
        {
            var filePaths = new List<string>();
            foreach (var image in images)
            {
                // Generate a unique filename
                var extension = Path.GetExtension(image.FileName);
                var uniqueFileName = $"{Guid.NewGuid()}{extension}";

                // Save the image to the Images folder
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", "rider_profile", uniqueFileName);
                using var stream = new FileStream(path, FileMode.Create);
                await image.CopyToAsync(stream);
                filePaths.Add(uniqueFileName);
            }
            return filePaths;
        }




        // DELETE: api/Rider/5
        [HttpDelete("DeleteRider")]
        public async Task<IActionResult> DeleteRider(int id)
        {
            if (_context.Riders == null)
            {
                return NotFound();
            }
            var Rider = await _context.Riders.FindAsync(id);
            if (Rider == null)
            {
                return NotFound();
            }

            _context.Riders.Remove(Rider);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RiderExists(Guid id)
        {
            return (_context.Riders?.Any(e => e.RiderId == id)).GetValueOrDefault();
        }
    }
}