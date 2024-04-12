using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Pagyeonja.Entities.Entities;
using Pagyeonja.Repositories.Repositories;
using PagyeonjaServices.Services;


namespace PagyeonjaServices.Services
{
    public class CommuterService : ICommuterService
    {
        private readonly ICommuterRepository _commuterRepository;

        private readonly IDatabaseTransactionRepository _databaseTransactionRepo;
		private readonly IApprovalRepository _approvalRepository;


        public CommuterService(ICommuterRepository commuterRepository, IDatabaseTransactionRepository databaseTransactionRepository, IApprovalRepository approvalRepository)
        {
            _commuterRepository = commuterRepository;
            _databaseTransactionRepo = databaseTransactionRepository;
			_approvalRepository = approvalRepository;
        }

        public Task<IEnumerable<Commuter>> GetAllCommuters()
        {
            return _commuterRepository.GetAllCommuters();
        }

        public Task<IEnumerable<Commuter>> GetApprovedCommuters()
        {
            return _commuterRepository.GetApprovedCommuters();
        }

        public Task<Commuter> GetCommuterById(Guid id)
        {
            return _commuterRepository.GetCommuterById(id);
        }

        public Task<Commuter> UpdateCommuter(Commuter commuter)
        {
            return _commuterRepository.UpdateCommuter(commuter);
        }

        public async Task<Commuter> RegisterCommuter(Commuter commuter)
        {
            using var transaction = await _databaseTransactionRepo.StartTransaction();
			try
			{

				await _commuterRepository.RegisterCommuter(commuter);

				// Create rider approval
				var approval = new Approval()
				{
					UserId = commuter.CommuterId,
					UserType = "Commuter",
					ApprovalDate = null,
				};

				await _approvalRepository.AddApproval(approval);


				//commit changes if done
				await _databaseTransactionRepo.SaveTransaction(transaction);

				return commuter;
			}
			catch (Exception ex)
			{
				await _databaseTransactionRepo.RevertTransaction(transaction);
				throw;
			}
            // return _commuterRepository.RegisterCommuter(commuter);
        }

        public Task<bool> DeleteCommuter(Guid id)
        {
            return _commuterRepository.DeleteCommuter(id);
        }

        public async Task<bool> CommuterExists(Guid id)
        {
            return await _commuterRepository.CommuterExists(id);
        }
    }
}