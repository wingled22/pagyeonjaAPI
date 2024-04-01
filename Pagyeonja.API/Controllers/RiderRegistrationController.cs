using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pagyeonja.Entities.Entities;
using Pagyeonja.Services.Services;

namespace pagyeonjaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RiderRegistrationController : ControllerBase
    {
        private readonly HitchContext _context;
        private readonly IRiderService _riderService;

        public RiderRegistrationController(HitchContext context, IRiderService riderService)
        {
            _context = context;
            _riderService = riderService;
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
        public async Task<IActionResult> GetRidersApproved()
        {
            try
            {
                var approvedRiders = await _riderService.GetRidersApproved();
                return Ok(approvedRiders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
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
        public async Task<IActionResult> UpdateRider(Guid id, Rider rider)
        {
            try
            {
                if (id != rider.RiderId)
                {
                    return BadRequest("ID mismatch");
                }

                var updatedRider = await _riderService.UpdateRider(rider);
                if (updatedRider == null)
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/Rider
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("RegisterRider")]
        public async Task<ActionResult<Rider>> PostRider([FromForm] Rider rider, [FromForm] List<IFormFile> images)
        {
            try
            {
                await _riderService.RegisterRider(rider, new List<string>());

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
                } 
                while (await _context.Riders.AnyAsync(r => r.RiderId == riderId));
                
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

        // Business logic controllers
        [HttpPut("RiderApprovalResponse")]
        public async Task<IActionResult> RiderApprovalResponse(Guid riderId, bool response)
        {
            try
            {
                var rider = await _context.Riders.FirstOrDefaultAsync(r => r.RiderId == riderId);
                var approval = await _context.Approvals.FirstOrDefaultAsync(a => a.UserId == riderId);
                if (approval != null && rider != null)
                {
                    rider.ApprovalStatus = response;
                    approval.ApprovalStatus = response;
                    approval.ApprovalDate = new DateTime();
                    await _context.SaveChangesAsync();
                    return new JsonResult("Rider approved");
                }
                return new BadRequestObjectResult("Rider not found!");
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        private bool RiderExists(Guid id)
        {
            return (_context.Riders?.Any(e => e.RiderId == id)).GetValueOrDefault();
        }


    }
}