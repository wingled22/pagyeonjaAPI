using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pagyeonja.Entities.Entities;
using Pagyeonja.Repositories.Repositories;
using Pagyeonja.Services.Services;

namespace pagyeonjaAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RiderRegistrationController : ControllerBase
	{
		private readonly HitchContext _context;
		private readonly RiderService _riderService;

		public RiderRegistrationController(HitchContext context, RiderService riderService)
		{
			_context = context;
			_riderService = riderService;
		}

		// GET: api/Riders
		[HttpGet("GetRiders")]
		public async Task<ActionResult<IEnumerable<Rider>>> GetRiders()
		{
			try
			{
				return await _context.Riders.OrderByDescending(a => a.DateApplied).ToListAsync();
			}
			catch (Exception ex)
			{
				return NotFound(ex);
			}
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
		public async Task<ActionResult<Rider>> PostRider([FromForm] Rider rider)
		{
			try
			{
				await ((IRiderService)_riderService).AddRider(rider);
				return CreatedAtAction("PostRider", new { id = rider.RiderId }, rider);
			}
			catch (Exception)
			{
				// Log the exception here
				return StatusCode(500, "An error occurred while processing your request.");
			}
		}

		[HttpPost]
		public async Task SaveImages(Guid id, List<IFormFile> images, string doctype, string usertype)
		{
			try
			{
				await _riderService.SaveImage(id, images, doctype, usertype);
			}
			catch (Exception ex)
			{
				NotFound(ex);
			}
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