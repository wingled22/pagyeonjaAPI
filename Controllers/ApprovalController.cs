using System;
using pagyeonjaAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace pagyeonjaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApprovalController : ControllerBase
    {
        public readonly HitchContext _context;
        public ApprovalController(HitchContext context)
        {
            _context = context;
        }

        [HttpPost("AddApproval")]
        public async Task<ActionResult<Approval>> PostApproval(Approval Approval)
        {
            try
            {
                Approval.Id = Guid.NewGuid();
                while (await _context.Approvals.AnyAsync(a => a.Id == Approval.Id))
                {
                    Approval.Id = Guid.NewGuid();
                }

                _context.Approvals.Add(Approval);
                await _context.SaveChangesAsync();
                return CreatedAtAction("PostApproval", new { id = Approval.Id }, Approval);

            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Unhandled Error occured: " + ex);
            }
        }

        [HttpGet("GetApprovals")]
        public async Task<IActionResult> GetAprovals(string usertype)
        {
            var approvals = await (
                from a in _context.Approvals
                join r in _context.Riders on a.UserId equals r.RiderId
                where a.UserType == usertype
                orderby a.Id descending
                select new { a.Id, r.FirstName, r.MiddleName, r.LastName, a.ApprovalStatus, r.ProfilePath }).ToListAsync();
            if (approvals == null)
            {
                return NotFound();
            }
            return new JsonResult(approvals);
        }

        [HttpGet("GetApproval")]
        public async Task<ActionResult<Approval>> GetApproval(Guid id)
        {
            if (_context.Approvals == null)
            {
                return NotFound();
            }
            var Approval = await _context.Approvals.FindAsync(id);

            if (Approval == null)
            {
                return NotFound();
            }

            return Approval;
        }

        [HttpPut("UpdateApproval")]
        public async Task<IActionResult> PutApproval(Guid id, Approval Approval)
        {
            if (id != Approval.Id)
            {
                return BadRequest();
            }

            _context.Entry(Approval).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApprovalExists(id))
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

        [HttpDelete("DeleteApproval")]
        public async Task<IActionResult> DeleteApproval(Guid id)
        {
            if (_context.Approvals == null)
            {
                return NotFound();
            }
            var Approval = await _context.Approvals.FindAsync(id);
            if (Approval == null)
            {
                return NotFound();
            }

            _context.Approvals.Remove(Approval);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool ApprovalExists(Guid id)
        {
            return (_context.Approvals?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}