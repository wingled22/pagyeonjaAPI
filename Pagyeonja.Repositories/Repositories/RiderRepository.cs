using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pagyeonja.Entities.Entities;

namespace Pagyeonja.Repositories.Repositories
{

    public class RiderRepository : IRiderRepository
    {
        private readonly HitchContext _context;

        public RiderRepository(HitchContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rider>> GetRiders()
        {
            return await _context.Riders.OrderByDescending(a => a.DateApplied).ToListAsync();
        }

        public async Task<IEnumerable<Rider>> GetRidersApproved()
        {
            return await _context.Riders.Where(a => a.ApprovalStatus == true).OrderByDescending(a => a.DateApplied).ToListAsync();
        }

        public async Task<Rider> GetRider(Guid id)
        {
            return await _context.Riders.FindAsync(id);
        }

        public async Task<bool> UpdateRider(Rider rider)
        {
            _context.Entry(rider).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await RiderExists(rider.RiderId))
                    return false;
                else
                    throw;
            }
        }

        public async Task<Rider> RegisterRider(Rider rider)
        {
            // Implementation for registering a rider
            try
            {
                do
                {
                    rider.RiderId = Guid.NewGuid();
                } while (await RiderExists(rider.RiderId));

                await _context.Riders.AddAsync(rider);
                await _context.SaveChangesAsync();
                return rider;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public async Task<bool> DeleteRider(Guid id)
        {
            var rider = await _context.Riders.FindAsync(id);
            if (rider == null)
                return false;

            _context.Riders.Remove(rider);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RiderExists(Guid id)
        {
            return await _context.Riders.AnyAsync(e => e.RiderId == id);
        }

    }

}