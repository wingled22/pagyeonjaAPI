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

        public async Task<IEnumerable<RiderCommuterApprovalModel>> GetApprovals(string userType)
        {
            return userType.ToLower() == "rider" ? await (
                from a in _context.Approvals
                join r in _context.Riders on a.UserId equals r.RiderId
                where a.UserType == userType && (a.ApprovalStatus == null || a.ApprovalStatus == false)
                orderby r.DateApplied
                select new RiderCommuterApprovalModel
                {
                    Id = a.Id,
                    UserId = a.UserId,
                    FirstName = r.FirstName ?? "",
                    MiddleName = r.MiddleName ?? "",
                    LastName = r.LastName ?? "",
                    ApprovalStatus = a.ApprovalStatus,
                    ProfilePath = r.ProfilePath ?? ""
                }).ToListAsync() :
                await (
                from a in _context.Approvals
                join r in _context.Commuters on a.UserId equals r.CommuterId
                where a.UserType == userType && (a.ApprovalStatus == null || a.ApprovalStatus == false)
                orderby r.DateApplied
                select new RiderCommuterApprovalModel
                {
                    Id = a.Id,
                    UserId = a.UserId,
                    FirstName = r.FirstName ?? "",
                    MiddleName = r.MiddleName ?? "",
                    LastName = r.LastName ?? "",
                    ApprovalStatus = a.ApprovalStatus,
                    ProfilePath = r.ProfilePath ?? ""
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

        public async Task UserApprovalResponse(string usertype, Guid userId, bool response, string? rejectionmessage)
        {
            if (usertype.ToLower() == "rider")
            {
                var user = await _context.Riders.FirstOrDefaultAsync(r => r.RiderId == userId);
                var approval = await _context.Approvals.FirstOrDefaultAsync(a => a.UserId == userId);
                if (approval != null && user != null)
                {
                    user.ApprovalStatus = response;
                    approval.ApprovalStatus = response;
                    approval.RejectionMessage = rejectionmessage;
                    approval.ApprovalDate = new DateTime();
                }
            }
            else
            {
                var user = await _context.Commuters.FirstOrDefaultAsync(r => r.CommuterId == userId);
                var approval = await _context.Approvals.FirstOrDefaultAsync(a => a.UserId == userId);
                if (approval != null && user != null)
                {
                    user.ApprovalStatus = response;
                    approval.ApprovalStatus = response;
                    approval.RejectionMessage = rejectionmessage;
                    approval.ApprovalDate = new DateTime();
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}