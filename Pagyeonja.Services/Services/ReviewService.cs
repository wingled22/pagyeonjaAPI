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
        private readonly IRideHistoryRepository _rideHistoryRepository;
        public ReviewService(IReviewRepository reviewRepository, IRideHistoryRepository rideHistoryRepository)
        {
            _reviewRepository = reviewRepository;
            _rideHistoryRepository = rideHistoryRepository;
        }

        public async Task<Review> AddReview(Review review)
        {
            return await _reviewRepository.AddReview(review);


            // var rideHistory = _rideHistoryRepository.get
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