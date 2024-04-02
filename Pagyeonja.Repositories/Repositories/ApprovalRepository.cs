using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pagyeonja.Entities.Entities;

namespace Pagyeonja.Repositories.Repositories
{
    public class ApprovalRepository : IApprovalRepository
    {
        private readonly HitchContext _context;

        public ApprovalRepository(HitchContext context)
        {
            _context = context;
        }

        public async Task<Approval> AddApproval(Approval approval)
        {
            approval.Id = Guid.NewGuid();
            while (await _context.Approvals.AnyAsync(a => a.Id == approval.Id))
            {
                approval.Id = Guid.NewGuid();
            }

            _context.Approvals.Add(approval);
            await _context.SaveChangesAsync();
            return approval;
        }

        public async Task<IEnumerable<RiderApprovalModel>> GetApprovals(string userType)
        {
            return await (
                from a in _context.Approvals
                join r in _context.Riders on a.UserId equals r.RiderId
                where a.UserType == userType && (a.ApprovalStatus == null || a.ApprovalStatus == false)
                orderby r.DateApplied
                select new RiderApprovalModel
                {
                    Id = a.Id,
                    UserId = (Guid)a.UserId,
                    FirstName = r.FirstName,
                    MiddleName = r.MiddleName,
                    LastName = r.LastName,
                    ApprovalStatus = (bool)a.ApprovalStatus,
                    ProfilePath = r.ProfilePath
                }).ToListAsync();
        }

        public async Task<Approval> GetApprovalById(Guid id)
        {
            return await _context.Approvals.FindAsync(id);
        }

        public async Task<Approval> UpdateApproval(Approval approval)
        {
            _context.Entry(approval).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return approval;
        }

        public async Task<bool> DeleteApproval(Guid id)
        {
            var approval = await _context.Approvals.FindAsync(id);
            if (approval == null)
                return false;

            _context.Approvals.Remove(approval);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}