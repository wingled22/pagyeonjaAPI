using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pagyeonja.Entities.Entities;
using Pagyeonja.Services.Services;

namespace Pagyeonja.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost("AddReview")]
        public async Task<ActionResult<Review>> AddReview(Review review)
        {
            try
            {
                var addReview = await _reviewService.AddReview(review);
                return CreatedAtAction("AddReview", new { id = addReview.ReviewId }, addReview);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}