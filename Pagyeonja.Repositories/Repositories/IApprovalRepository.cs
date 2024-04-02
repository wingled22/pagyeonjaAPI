using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pagyeonja.Entities.Entities;

namespace Pagyeonja.Repositories.Repositories
{
    public interface IApprovalRepository
    {
        Task<Approval> AddApproval(Approval approval);
        Task<IEnumerable<RiderApprovalModel>> GetApprovals(string userType);
        Task<Approval> GetApprovalById(Guid id);
        Task<Approval> UpdateApproval(Approval approval);
        Task<bool> DeleteApproval(Guid id);
    }
}