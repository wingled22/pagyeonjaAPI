using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pagyeonja.Entities.Entities;

namespace Pagyeonja.Services.Services
{
    public interface IApprovalService
    {
        Task<Approval> AddApproval(Approval approval);
        Task<object> GetApprovals(string userType);
        Task<Approval> GetApprovalById(Guid id);
        Task<Approval> UpdateApproval(Approval approval);
        Task<bool> DeleteApproval(Guid id);
    }
}