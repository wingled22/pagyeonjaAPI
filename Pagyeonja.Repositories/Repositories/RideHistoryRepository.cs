using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pagyeonja.Entities.Entities;

namespace Pagyeonja.Repositories.Repositories
{
    public class RideHistoryRepository : IRideHistoryRepository
    {

        private readonly HitchContext _context;

        public RideHistoryRepository(HitchContext context)
        {
            _context = context;
        }

        public async Task<RideHistory> AddRideHistory(RideHistory rideHistory)
        {
            do
            {
                rideHistory.RideHistoryId = Guid.NewGuid();
            } while (await RideHistoryExists(rideHistory.RideHistoryId));

            await _context.RideHistories.AddAsync(rideHistory);
            await _context.SaveChangesAsync();
            return rideHistory;
        }

        public async Task<IEnumerable<RideHistory>> GetRideHistories()
        {
            return await _context.RideHistories.OrderByDescending(rh => rh.RideHistoryId).ToListAsync();
        }

        public async Task<bool> RideHistoryExists(Guid id)
        {
            return await _context.RideHistories.AnyAsync(rh => rh.RideHistoryId == id);
        }
    }
}