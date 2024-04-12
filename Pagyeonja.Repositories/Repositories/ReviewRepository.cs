using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pagyeonja.Entities.Entities;

namespace Pagyeonja.Repositories.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly HitchContext _context;
        public ReviewRepository(HitchContext context)
        {
            _context = context;
        }

        public async Task<Review> AddReview(Review review)
        {
            do
            {
                review.ReviewId = Guid.NewGuid();
            } while (await ReviewExists(review.ReviewId));

            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<IEnumerable<Review>> GetReviews()
        {
            return await _context.Reviews.OrderByDescending(re => re.ReviewId).ToListAsync();
        }

        public async Task<bool> ReviewExists(Guid id)
        {
            return await _context.Reviews.AnyAsync(re => re.ReviewId == id);
        }
    }
}