using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pagyeonja.Entities.Entities;
using Pagyeonja.Repositories.Repositories;

namespace Pagyeonja.Services.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<Review> AddReview(Review review)
        {
            return await _reviewRepository.AddReview(review);
        }

        public async Task<IEnumerable<Review>> GetReviews()
        {
            return await _reviewRepository.GetReviews();
        }

        public async Task<bool> ReviewExists(Guid id)
        {
            return await _reviewRepository.ReviewExists(id);
        }
    }
}