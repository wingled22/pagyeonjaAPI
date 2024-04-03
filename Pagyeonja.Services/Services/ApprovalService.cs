using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pagyeonja.Entities.Entities;
using Pagyeonja.Repositories;
using Pagyeonja.Repositories.Repositories;

namespace Pagyeonja.Services.Services
{
    public class ApprovalService : IApprovalService
    {
        private readonly IApprovalRepository _approvalRepository;

        public ApprovalService(IApprovalRepository approvalRepository)
        {
            _approvalRepository = approvalRepository;
        }

        public ApprovalService()
        {
            _approvalRepository = new ApprovalRepository(new HitchContext());
        }

        public Task<Approval> AddApproval(Approval approval)
        {
            return _approvalRepository.AddApproval(approval);
        }

        public Task<IEnumerable<RiderCommuterApprovalModel>> GetApprovals(string userType)
        {
            return _approvalRepository.GetApprovals(userType);
        }

        public Task<Approval> GetApprovalById(Guid id)
        {
            return _approvalRepository.GetApprovalById(id);
        }

        public Task<Approval> UpdateApproval(Approval approval)
        {
            return _approvalRepository.UpdateApproval(approval);
        }

        public Task<bool> DeleteApproval(Guid id)
        {
            return _approvalRepository.DeleteApproval(id);
        }

        public async Task UserApprovalResponse(string usertype, Guid userId, bool response)
        {
            await _approvalRepository.UserApprovalResponse(usertype, userId, response);
        }
    }
}