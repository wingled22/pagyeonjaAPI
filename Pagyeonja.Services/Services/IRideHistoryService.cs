using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pagyeonja.Entities.Entities;

namespace Pagyeonja.Services.Services
{
    public interface IRideHistoryService
    {
        Task<RideHistory> AddRideHistory(RideHistory rideHistory);
        Task<bool> RideHistoryExists(Guid id);
        Task<IEnumerable<RideHistory>> GetRideHistories();
    }
}