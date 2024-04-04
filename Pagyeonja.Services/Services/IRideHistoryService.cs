using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pagyeonja.Entities.Entities;
using Pagyeonja.Repositories;
using Pagyeonja.Repositories.Repositories.Models;

namespace Pagyeonja.Services.Services
{
    public interface IRideHistoryService
    {
        Task<RideHistory> AddRideHistory(RideHistory rideHistory);
        Task<bool> RideHistoryExists(Guid id);
        Task<IEnumerable<RideHistory>> GetRideHistories();
        Task<IEnumerable<RideHistoryModel>> GetUserRideHistory(Guid id, string usertype);
        Task<RideHistory> GetRideHistory(Guid id);
        Task<RideHistory> UpdateRideHistory(RideHistory rideHistory);
    }
}