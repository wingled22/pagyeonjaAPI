using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pagyeonja.Entities.Entities;
using PagyeonjaServices.Services;

namespace pagyeonjaAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Roles = "Rider")]
    public class CommuterRegistrationController : ControllerBase
    {
        private readonly ICommuterService _commuterService;

        public CommuterRegistrationController(ICommuterService commuterService)
        {
            _commuterService = commuterService;
        }

        // GET: api/Commuters
        [HttpGet("GetCommuters")]
        public async Task<ActionResult<IEnumerable<Commuter>>> GetCommuters()
        {
            try
            {
                var commuters = await _commuterService.GetAllCommuters();
                return Ok(commuters);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet("GetCommutersApproved")]
        public async Task<ActionResult<IEnumerable<Commuter>>> GetCommutersApproved()
        {
            try
            {
                var approvedCommuters = await _commuterService.GetApprovedCommuters();
                return Ok(approvedCommuters);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        // GET: api/Commuter/5
        [HttpGet("GetCommuter")]
        public async Task<ActionResult<Commuter>> GetCommuter(Guid id)
        {
            try
            {
                var commuter = await _commuterService.GetCommuterById(id);
                if (commuter == null)
                {
                    return NotFound();
                }
                return Ok(commuter);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        // PUT: api/Commuter/5
        [HttpPut("UpdateCommuter")]
        public async Task<IActionResult> UpdateCommuter(Commuter commuter)
        {
            try
            {
                if (await _commuterService.CommuterExists(commuter.CommuterId))
                {
                    var updatedCommuter = await _commuterService.UpdateCommuter(commuter);
                    return Ok(updatedCommuter);
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

        // POST: api/Commuter
        [HttpPost("RegisterCommuter")]
        public async Task<ActionResult<Commuter>> RegisterCommuter([FromForm] Commuter commuter, [FromForm] List<IFormFile> images)
        {
            try
            {
                var registeredCommuter = await _commuterService.RegisterCommuter(commuter);
                Console.WriteLine("Registered Commuter: ", registeredCommuter);
                return CreatedAtAction("RegisterCommuter", new { id = registeredCommuter.CommuterId }, registeredCommuter);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        // DELETE: api/Commuter/5
        [HttpDelete("DeleteCommuter")]
        public async Task<IActionResult> DeleteCommuter(Guid id)
        {
            try
            {
                var result = await _commuterService.DeleteCommuter(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }


    // [Route("api/[controller]")]
    // [ApiController]
    // public class CommuterRegistrationController : ControllerBase
    // {
    //     private readonly HitchContext _context;
    //     public CommuterRegistrationController(HitchContext context)
    //     {
    //         _context = context;
    //     }

    //     // GET: api/Commuters
    //     [HttpGet("GetCommuters")]
    //     public async Task<ActionResult<IEnumerable<Commuter>>> GetCommuters()
    //     {
    //         if (_context.Commuters == null)
    //         {
    //             return NotFound();
    //         }
    //         return await _context.Commuters.OrderByDescending(a => a.CommuterId).ToListAsync();
    //     }

    //     [HttpGet("GetCommutersApproved")]
    //     public async Task<ActionResult<IEnumerable<Commuter>>> GetCommutersApproved()
    //     {
    //         if (_context.Commuters == null)
    //         {
    //             return NotFound();
    //         }

    //         return await _context.Commuters.Where(a => a.ApprovalStatus == true).OrderByDescending(a => a.CommuterId).ToListAsync();
    //     }

    //     // GET: api/Commuter/5
    //     [HttpGet("GetCommuter")]
    //     public async Task<ActionResult<Commuter>> GetCommuter(Guid id)
    //     {
    //         if (_context.Commuters == null)
    //         {
    //             return NotFound();
    //         }
    //         var Commuter = await _context.Commuters.FindAsync(id);

    //         if (Commuter == null)
    //         {
    //             return NotFound();
    //         }

    //         return Commuter;
    //     }

    //     // PUT: api/Commuter/5
    //     // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //     [HttpPut("UpdateCommuter")]
    //     public async Task<IActionResult> PutCommuter(Guid id, Commuter Commuter)
    //     {
    //         if (id != Commuter.CommuterId)
    //         {
    //             return BadRequest();
    //         }

    //         _context.Entry(Commuter).State = EntityState.Modified;

    //         try
    //         {
    //             await _context.SaveChangesAsync();
    //         }
    //         catch (DbUpdateConcurrencyException)
    //         {
    //             if (!CommuterExists(id))
    //             {
    //                 return NotFound();
    //             }
    //             else
    //             {
    //                 throw;
    //             }
    //         }

    //         return NoContent();
    //     }

    //     // POST: api/Commuter
    //     // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //     [HttpPost("RegisterCommuter")]
    //     public async Task<ActionResult<Commuter>> PostCommuter([FromForm] Commuter Commuter, [FromForm] List<IFormFile> images)
    //     {
    //         try
    //         {

    //             var fileNames = await SaveImages(images);
    //             Commuter.ProfilePath = string.Join(";", fileNames);

    //             Commuter.CommuterId = Guid.NewGuid();
    //             while (await _context.Commuters.AnyAsync(r => r.CommuterId == Commuter.CommuterId))
    //             {
    //                 Commuter.CommuterId = Guid.NewGuid();
    //             }

    //             _context.Commuters.Add(Commuter);
    //             await _context.SaveChangesAsync();
    //             return CreatedAtAction("PostCommuter", new { id = Commuter.CommuterId }, Commuter);

    //         }
    //         catch (Exception ex)
    //         {
    //             return new BadRequestObjectResult("Unhandled Error occured: " + ex);
    //         }
    //     }

    //       private static async Task<List<string>> SaveImages(List<IFormFile> images)
    //     {
    //         var filePaths = new List<string>();
    //         foreach (var image in images)
    //         {
    //             // Generate a unique filename
    //             var extension = Path.GetExtension(image.FileName);
    //             var uniqueFileName = $"{Guid.NewGuid()}{extension}";

    //             // Save the image to the Images folder
    //             var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", "commuter_profile", uniqueFileName);
    //             using var stream = new FileStream(path, FileMode.Create);
    //             await image.CopyToAsync(stream);
    //             filePaths.Add(uniqueFileName);
    //         }
    //         return filePaths;
    //     }

    //     // DELETE: api/Commuter/5
    //     [HttpDelete("DeleteCommuter")]
    //     public async Task<IActionResult> DeleteCommuter(Guid id)
    //     {
    //         if (_context.Commuters == null)
    //         {
    //             return NotFound();
    //         }
    //         var Commuter = await _context.Commuters.FindAsync(id);
    //         if (Commuter == null)
    //         {
    //             return NotFound();
    //         }

    //         _context.Commuters.Remove(Commuter);
    //         await _context.SaveChangesAsync();

    //         return NoContent();
    //     }

    //     private bool CommuterExists(Guid id)
    //     {
    //         return (_context.Commuters?.Any(e => e.CommuterId == id)).GetValueOrDefault();
    //     }
    // }
}