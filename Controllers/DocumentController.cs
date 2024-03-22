using System;
using pagyeonjaAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace pagyeonjaAPI.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        public readonly HitchContext _context;
        public DocumentController(HitchContext context)
        {
            _context = context;
        }

        [HttpGet("GetDocuments")]
        public async Task<IActionResult> GetDocuments(string usertype)
        {
            return new JsonResult(await _context.Documents.Where(d => d.UserType == usertype).ToListAsync());
        }

        [HttpGet("GetDocument")]
        public async Task<ActionResult<Document>> GetDocument(Guid id)
        {
            if (_context.Documents == null)
            {
                return NotFound();
            }
            var Document = await _context.Documents.FindAsync(id);

            if (Document == null)
            {
                return NotFound();
            }

            return Document;
        }

        [HttpPost("AddDocument")]
        public async Task<ActionResult<Document>> PostDocument(Guid userid, Document Document)
        {
            try
            {
                Document.Id = Guid.NewGuid();
                while (await _context.Approvals.AnyAsync(a => a.Id == Document.Id))
                {
                    Document.Id = Guid.NewGuid();
                }
                Document.UserId = userid;
                Document.UserType = "Rider";

                _context.Documents.Add(Document);
                await _context.SaveChangesAsync();
                return CreatedAtAction("PostDocument", new { id = Document.Id }, Document);

            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Unhandled Error occured: " + ex);
            }
        }
    }
}