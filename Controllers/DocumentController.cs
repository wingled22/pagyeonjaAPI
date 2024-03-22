using System;
using pagyeonjaAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace pagyeonjaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        public readonly HitchContext _context;
        public DocumentController(HitchContext context)
        {
            _context = context;
        }

        // [HttpGet("GetDocuments")]
        // public async Task<IActionResult> GetDocuments(string usertype)
        // {
        //     return new JsonResult(await _context.Documents.Where(d => d.UserType == usertype).ToListAsync());
        // }

        [HttpGet("GetDocuments")]
        public async Task<IActionResult> GetDocuments(Guid id, string usertype)
        {
            if (_context.Documents == null)
            {
                return NotFound();
            }

            var joinedDocs = usertype == "Rider" ? await (
                from r in _context.Riders
                where r.RiderId == id
                select new
                {
                    r.FirstName,
                    r.MiddleName,
                    r.LastName,
                    r.ProfilePath,
                    Documents = _context.Documents.Where(d => d.UserId == id && d.UserType == usertype).ToList()
                }).ToListAsync() :
                await (
                from c in _context.Commuters
                where c.CommuterId == id
                select new
                {
                    c.FirstName,
                    c.MiddleName,
                    c.LastName,
                    c.ProfilePath,
                    Documents = _context.Documents.Where(d => d.UserId == id && d.UserType == usertype).ToList()
                }).ToListAsync();

            if (joinedDocs == null || !joinedDocs.Any())
            {
                return NotFound();
            }

            return new JsonResult(joinedDocs);
        }


        [HttpPost("AddDocument")]
        public async Task<ActionResult<Document>> PostDocument([FromForm] Document Document, [FromForm] List<IFormFile> image)
        {
            try
            {
                if (image != null)
                {
                    var fileNames = await SaveImages(image);
                    Document.DocumentPath = string.Join(";", fileNames);
                }
                Document.Id = Guid.NewGuid();
                while (await _context.Approvals.AnyAsync(a => a.Id == Document.Id))
                {
                    Document.Id = Guid.NewGuid();
                }
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

        private static async Task<List<string>> SaveImages(List<IFormFile> images)
        {
            var filePaths = new List<string>();
            foreach (var image in images)
            {
                // Generate a unique filename
                var extension = Path.GetExtension(image.FileName);
                var uniqueFileName = $"{Guid.NewGuid()}{extension}";

                // Save the image to the Images folder
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", "documents", uniqueFileName);
                using var stream = new FileStream(path, FileMode.Create);
                await image.CopyToAsync(stream);
                filePaths.Add(uniqueFileName);
            }
            return filePaths;
        }
    }
}