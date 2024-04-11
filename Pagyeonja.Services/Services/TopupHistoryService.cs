using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pagyeonja.Entities.Entities;
using Pagyeonja.Repositories.Repositories;
using Pagyeonja.Repositories.Repositories.Models;

namespace Pagyeonja.Services.Services
{
    public class TopupHistoryService : ITopupHistoryService
    {
        private readonly ITopupHistoryRepository _topupHistoryRepository;
        private readonly IRiderRepository _riderRepository;
        public TopupHistoryService(ITopupHistoryRepository topupHistoryRepository, IRiderRepository riderRepository)
        {
            _topupHistoryRepository = topupHistoryRepository;
            _riderRepository = riderRepository;
        }

        public async Task<TopupHistory> AddTopupHistory(TopupHistory topupHistory)
        {
            // Update topup after
            var rider = await _riderRepository.GetRider((Guid)topupHistory.RiderId);
            if (rider != null)
            {
                // Add topup amount to topup after
                rider.Balance += topupHistory.TopupAmount ?? 0;
                await _riderRepository.UpdateRider(rider);

                // Update the topupHistory with the new balance
                topupHistory.TopupAfter = rider.Balance;
            }

            // Now, topupHistory object has the updated TopupAfter value
            // Add it to the repository
            var addedTopupHistory = await _topupHistoryRepository.AddTopupHistory(topupHistory);
            return addedTopupHistory;
        }

        public async Task<IEnumerable<TopupHistoryModel>> GetRiderTopupHistory(Guid id)
        {
            return await _topupHistoryRepository.GetRiderTopupHistory(id);
        }

        public async Task<IEnumerable<TopupHistory>> GetTopupHistories()
        {
            return await _topupHistoryRepository.GetTopupHistories();
        }

        public async Task<bool> TopupHistoryExists(Guid id)
        {
            return await _topupHistoryRepository.TopupHistoryExists(id);
        }

        public async Task<TopupHistory> UpdateTopupHistory(TopupHistory topupHistory)
        {
            return await _topupHistoryRepository.UpdateTopupHistory(topupHistory);
        }
    }
}