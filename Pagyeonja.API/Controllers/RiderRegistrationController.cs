using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
			try
			{
				var getRiders = await _riderService.GetRiders();
				return Ok(getRiders);

			}
			catch (Exception ex)
			{
				return NotFound(ex);
			}
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
		public async Task<ActionResult<Rider>> GetRider(Guid id)
		{
			try
			{

				var getRider = await _riderService.GetRider(id);


				if (getRider == null)
				{
					return NotFound();
				}

				return Ok(getRider);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex}");
			}
		}

		// PUT: api/Rider/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754


		[HttpPut("UpdateRider")]
		public async Task<IActionResult> PutRider(Rider rider)
		{
			try
			{
				var updatedRider = await _riderService.UpdateRider(rider);
				if (updatedRider == null)
				{
					return NotFound();
				}

				return Ok(updatedRider);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}


		// POST: api/Rider
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost("RegisterRider")]
		public async Task<ActionResult<Rider>> PostRider([FromForm] Rider rider)
		{
			try
			{
				await _riderService.AddRider(rider);
				return CreatedAtAction("PostRider", new { id = rider.RiderId }, rider);
			}
			catch (Exception)
			{
				// Log the exception here
				return StatusCode(500, "An error occurred while processing your request.");
			}
		}

		[HttpPost("SaveImage")]
		public async Task SaveImages(Guid id, List<IFormFile> images, string doctype, string usertype, string docview)
		{
			try
			{
				await _riderService.SaveImage(id, images, doctype, usertype, docview);
			}
			catch (Exception ex)
			{
				NotFound(ex);
			}
		}



		[HttpDelete("DeleteRider")]
		public async Task<IActionResult> DeleteRider(Guid id)
		{
			try
			{
				var result = await _riderService.DeleteRider(id);
				if (!result)
				{
					return NotFound();
				}
				return NoContent();
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		private bool RiderExists(Guid id)
		{
			return (_context.Riders?.Any(e => e.RiderId == id)).GetValueOrDefault();
		}
	}
}