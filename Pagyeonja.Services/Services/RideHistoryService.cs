using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pagyeonja.Entities.Entities;
using Pagyeonja.Repositories.Repositories;
using Pagyeonja.Repositories.Repositories.Models;

namespace Pagyeonja.Services.Services
{
    public class RideHistoryService : IRideHistoryService
    {
        private readonly IRideHistoryRepository _rideHistoryRepository;
        public RideHistoryService(IRideHistoryRepository rideHistoryRepository)
        {
            _rideHistoryRepository = rideHistoryRepository;
        }

        public async Task<RideHistory> AddRideHistory(RideHistory rideHistory)
        {
            return await _rideHistoryRepository.AddRideHistory(rideHistory);
        }

        public async Task<IEnumerable<RideHistory>> GetRideHistories()
        {
            return await _rideHistoryRepository.GetRideHistories();
        }

        public async Task<IEnumerable<RideHistoryModel>> GetUserRideHistory(Guid id, string usertype)
        {
            return await _rideHistoryRepository.GetUserRideHistory(id, usertype);
        }

        public async Task<bool> RideHistoryExists(Guid id)
        {
            return await _rideHistoryRepository.RideHistoryExists(id);
        }
    }
}