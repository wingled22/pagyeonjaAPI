using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pagyeonja.Entities.Entities;
using Pagyeonja.Services.Services;

namespace Pagyeonja.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly HitchContext _context;
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // GET: api/Transaction
        [HttpGet("GetTransactions")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            try
            {
                var transactions = await _transactionService.GetTransactions();
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        // GET: api/Transaction/5
        [HttpGet("GetTransaction")]
        public async Task<ActionResult<Transaction>> GetTransaction(Guid id)
        {
            try
            {
                var transaction = await _transactionService.GetTransaction(id);
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPost("AddTransaction")]
        public async Task<ActionResult<Transaction>> AddTransaction(Transaction transaction)
        {
            try
            {
                var addTransaction = await _transactionService.AddTransaction(transaction);
                return CreatedAtAction("AddTransaction", new { id = addTransaction.TransactionId }, addTransaction);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        // PUT: api/Transaction/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdateTransaction")]
        public async Task<IActionResult> UpdateTransaction(Transaction transaction)
        {
            try
            {
                if (await _transactionService.TransactionExists(transaction.TransactionId))
                {
                    var updateTransaction = await _transactionService.UpdateTransaction(transaction);
                    return Ok(updateTransaction);
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

        // DELETE: api/Suspension/5
        [HttpDelete("DeleteSuspension")]
        public async Task<IActionResult> DeleteSuspension(Guid id)
        {
            try
            {
                if (await _transactionService.TransactionExists(id))
                {
                    var result = await _transactionService.DeleteTransaction(id);
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