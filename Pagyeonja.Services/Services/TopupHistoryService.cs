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
        public TopupHistoryService(ITopupHistoryRepository topupHistoryRepository)
        {
            _topupHistoryRepository = topupHistoryRepository;
        }

        public async Task<TopupHistory> AddTopupHistory(TopupHistory topupHistory)
        {
            return await _topupHistoryRepository.AddTopupHistory(topupHistory);
             
             


            
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