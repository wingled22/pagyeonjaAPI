using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pagyeonja.Entities.Entities;
using Pagyeonja.Repositories.Repositories.Models;

namespace Pagyeonja.Repositories.Repositories
{
    public interface ITopupHistoryRepository
    {
        Task<TopupHistory> AddTopupHistory(TopupHistory topupHistory);

        Task<bool> TopupHistoryExists(Guid id);
        Task<IEnumerable<TopupHistory>> GetTopupHistories();
        Task<IEnumerable<TopupHistoryModel>> GetRiderTopupHistory(Guid id);

        Task<TopupHistory> UpdateTopupHistory(TopupHistory topupHistory);
    }
}