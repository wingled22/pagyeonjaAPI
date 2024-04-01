using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pagyeonja.Entities.Entities;
using Pagyeonja.Repositories.Repositories;

namespace Pagyeonja.Services.Services
{
    public class RiderService : IRiderService
    {
        private readonly IRiderRepository _riderRepository;
        private readonly IDatabaseTransactionRepository _databaseTransactionRepo;
        private readonly IApprovalRepository _approvalRepository;

        public RiderService(IRiderRepository riderRepository, IDatabaseTransactionRepository databaseTransactionRepository, IApprovalRepository approvalRepository)
        {
            _riderRepository = riderRepository;
            _databaseTransactionRepo = databaseTransactionRepository;
            _approvalRepository = approvalRepository;
        }

        public async Task<IEnumerable<Rider>> GetRiders()
        {
            return await _riderRepository.GetRiders();
        }

        public async Task<IEnumerable<Rider>> GetRidersApproved()
        {
            return await _riderRepository.GetRidersApproved();
        }

        public async Task<Rider> GetRider(Guid id)
        {
            return await _riderRepository.GetRider(id);
        }

        public async Task<bool> UpdateRider(Rider rider)
        {
            return await _riderRepository.UpdateRider(rider);
        }

        public async Task<Rider> RegisterRider(Rider rider, List<string> imagePaths)
        {
            using (var transaction  = await _databaseTransactionRepo.StartTransaction())
            {
                try
                {

                    await _riderRepository.AddRider(rider);

                    var app = new Approval();

                    await _approvalRepository.AddApproval(app);

                    
                    //commit changes if done
                    await _databaseTransactionRepo.SaveTransaction(transaction);

                    return rider;
                }
                catch (Exception ex)
                {
                    await _databaseTransactionRepo.RevertTransaction(transaction);
                    throw; 
                }
            }
            // return await _riderRepository.RegisterRider(rider);
        }

        public async Task<bool> DeleteRider(Guid id)
        {
            return await _riderRepository.DeleteRider(id);
        }

        public async Task<bool> RiderExists(Guid id)
        {
            return await _riderRepository.RiderExists(id);
        }
    }
}