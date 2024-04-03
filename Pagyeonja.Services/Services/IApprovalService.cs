using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pagyeonja.Entities.Entities;
using Pagyeonja.Repositories;

namespace Pagyeonja.Services.Services
{
    public interface IApprovalService
    {
        Task<Approval> AddApproval(Approval approval);
        Task<IEnumerable<RiderCommuterApprovalModel>> GetApprovals(string userType);
        Task<Approval> GetApprovalById(Guid id);
        Task<Approval> UpdateApproval(Approval approval);
        Task<bool> DeleteApproval(Guid id);
        Task UserApprovalResponse(string usertype, Guid userId, bool response);
    }
}