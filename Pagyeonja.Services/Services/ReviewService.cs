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
            await _reviewRepository.AddReview(review);

            var rideHistory = await _rideHistoryRepository.GetRideHistoryByTransaction(Guid.Parse(review.TransactionId.ToString()));
            if(rideHistory != null)
            {
                rideHistory.ReviewId = review.ReviewId;
                await _rideHistoryRepository.UpdateRideHistory(rideHistory);
            }

            return review;
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