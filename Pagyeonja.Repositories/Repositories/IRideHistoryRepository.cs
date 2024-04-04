using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pagyeonja.Entities.Entities;
using Pagyeonja.Repositories.Repositories.Models;

namespace Pagyeonja.Repositories.Repositories
{
    public interface IRideHistoryRepository
    {
        Task<RideHistory> AddRideHistory(RideHistory rideHistory);
        Task<bool> RideHistoryExists(Guid id);
        Task<IEnumerable<RideHistory>> GetRideHistories();
        Task<RideHistory> GetRideHistory(Guid id);
        Task<IEnumerable<RideHistoryModel>> GetUserRideHistory(Guid id, string usertype);
        Task<RideHistory> UpdateRideHistory(RideHistory rideHistory);

    }
}