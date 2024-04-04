using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pagyeonja.Entities.Entities;

namespace Pagyeonja.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly HitchContext _context;
        public TransactionController(HitchContext context)
        {
            _context = context;
        }

        // GET: api/Suspension
        [HttpGet("GetTransactions")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            try
            {
                // var suspensions = await _suspensionService.GetSuspensions();
                // return Ok(suspensions);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}