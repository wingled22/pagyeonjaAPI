using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using Pagyeonja.Entities.Entities;

namespace Pagyeonja.Repositories.Repositories
{
    public class CommuterRepository : ICommuterRepository
    {
        private readonly HitchContext _context;

        public CommuterRepository(HitchContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Commuter>> GetAllCommuters()
        {
            return await _context.Commuters.OrderByDescending(a => a.CommuterId).ToListAsync();
        }

        public async Task<IEnumerable<Commuter>> GetApprovedCommuters()
        {
            return await _context.Commuters.Where(a => a.ApprovalStatus == true).OrderByDescending(a => a.CommuterId).ToListAsync();
        }

        public async Task<Commuter> GetCommuterById(Guid id)
        {
            return await _context.Commuters.FindAsync(id);
        }

        public async Task<Commuter> UpdateCommuter(Commuter commuter)
        {
            _context.Entry(commuter).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return commuter;
        }

        public async Task<Commuter> RegisterCommuter(Commuter commuter)
        {
            // commuter.CommuterId = Guid.NewGuid();
            // _context.Commuters.Add(commuter);
            // await _context.SaveChangesAsync();
            // return commuter;

            try
            {
                do
                {
                commuter.CommuterId = Guid.NewGuid();
                } while (await CommuterExists(commuter.CommuterId));
                commuter.DateApplied = new DateTime();

                await _context.Commuters.AddAsync(commuter);
                await _context.SaveChangesAsync();
                return commuter;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteCommuter(Guid id)
        {
            var commuter = await _context.Commuters.FindAsync(id);
            if (commuter == null)
                return false;

            _context.Commuters.Remove(commuter);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CommuterExists(Guid id)
        {
            return await _context.Commuters.AnyAsync(e => e.CommuterId == id);
        }

        public Task<bool> ImageExist(string filename, string usertype, string doctype)
        {
            throw new NotImplementedException();
        }

        public async Task<Commuter> GetCommuterSuspended(Guid id)
        {
            return await _context.Commuters.Where(c => c.CommuterId == id && c.SuspensionStatus == true).FirstOrDefaultAsync();
        }
    }
}