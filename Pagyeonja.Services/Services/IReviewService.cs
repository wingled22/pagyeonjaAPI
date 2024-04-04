using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pagyeonja.Entities.Entities;

namespace Pagyeonja.Services.Services
{
    public interface IReviewService
    {
        Task<bool> ReviewExists(Guid id);
        Task<Review> AddReview(Review review);
        Task<IEnumerable<Review>> GetReviews();
    }
}