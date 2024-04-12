using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pagyeonja.Entities.Entities;
using Pagyeonja.Repositories.Repositories.Models;

namespace Pagyeonja.Repositories.Repositories
{

    public class TopupHistoryRepository : ITopupHistoryRepository
    {
        private readonly HitchContext _context;

        public TopupHistoryRepository(HitchContext context)
        {
            _context = context;
        }

        public async Task<TopupHistory> AddTopupHistory(TopupHistory topupHistory)
        {
            do
            {
                topupHistory.TopupId = Guid.NewGuid();
            } while (await TopupHistoryExists(topupHistory.TopupId));

            await _context.TopupHistories.AddAsync(topupHistory);
            await _context.SaveChangesAsync();
            return topupHistory;
        }

        public async Task<IEnumerable<TopupHistory>> GetTopupHistories()
        {
            return await _context.TopupHistories.OrderByDescending(th => th.TopupId).ToListAsync();
        }

        public async Task<IEnumerable<TopupHistoryModel>> GetRiderTopupHistory(Guid id)
        {
            return await (
                 from th in _context.TopupHistories

                 join r in _context.Riders on th.RiderId equals r.RiderId
                 where r.RiderId == id
                 select new TopupHistoryModel
                 {
                     TopupId = th.TopupId,
                     RiderId = r.RiderId,
                     FirstName = r.FirstName ?? "",
                     MiddleName = r.MiddleName ?? "",
                     LastName = r.LastName ?? "",
                     TopupBefore = th.TopupBefore ?? 0.0,
                     TopupAfter = th.TopupAfter ?? 0.0,
                     TopupAmount = th.TopupAmount ?? 0.0,
                     TopupDate = th.TopupDate ?? null,
                     Status = th.Status ?? ""

                 }).ToListAsync();
        }

        public async Task<bool> TopupHistoryExists(Guid id)
        {
            return await _context.TopupHistories.AnyAsync(th => th.TopupId == id);
        }

        public async Task<TopupHistory> UpdateTopupHistory(TopupHistory topupHistory)
        {
            _context.Entry(topupHistory).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return topupHistory;
        }
    }
}