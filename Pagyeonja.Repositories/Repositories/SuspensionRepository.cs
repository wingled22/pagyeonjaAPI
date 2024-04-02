using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Pagyeonja.Entities.Entities;

namespace Pagyeonja.Repositories.Repositories
{
    public class SuspensionRepository : ISuspensionRepository
    {
        private readonly HitchContext _context;
        public SuspensionRepository(HitchContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Suspension>> GetSuspensions()
        {
            return await _context.Suspensions.OrderByDescending(a => a.SuspensionId).ToListAsync();
        }

        public async Task<Suspension> GetSuspension(Guid userid, string usertype)
        {
            return await _context.Suspensions
                .Where(s => s.UserId == userid && s.UserType == usertype && s.SuspensionDate >= DateTime.Now && s.Status == true)
                .OrderBy(s => s.InvokedSuspensionDate).FirstOrDefaultAsync();
        }

        public async Task<Suspension> UpdateSuspension(Suspension suspension)
        {
            _context.Entry(suspension).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return suspension;
        }

        public async Task<Suspension> InvokeSuspension(Suspension Suspension)
        {
            Suspension.SuspensionId = Guid.NewGuid();
            while (await _context.Suspensions.AnyAsync(r => r.SuspensionId == Suspension.SuspensionId))
            {
                Suspension.SuspensionId = Guid.NewGuid();
            }

            //add when did the suspension invoked
            Suspension.InvokedSuspensionDate = DateTime.Now;
            Suspension.Status = true;

            _context.Suspensions.Add(Suspension);

            await _context.SaveChangesAsync();
            return Suspension;
        }

        public async Task<bool> DeleteSuspension(Guid id)
        {
            var suspension = await _context.Suspensions.FindAsync(id);
            if(suspension == null)
                return false;
            
            _context.Suspensions.Remove(suspension);
            await _context.SaveChangesAsync();

            return true;
        }

        public Task<bool> SuspensionExists(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}