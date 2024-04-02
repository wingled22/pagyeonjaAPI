using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pagyeonja.Entities.Entities;
using Pagyeonja.Repositories.Repositories;

namespace PagyeonjaServices.Services
{
    public class CommuterService : ICommuterService
    {
        private readonly ICommuterRepository _commuterRepository;

        public CommuterService(ICommuterRepository commuterRepository)
        {
            _commuterRepository = commuterRepository;
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

        public Task<Commuter> RegisterCommuter(Commuter commuter)
        {
            return _commuterRepository.RegisterCommuter(commuter);
        }

        public Task<bool> DeleteCommuter(Guid id)
        {
            return _commuterRepository.DeleteCommuter(id);
        }

        public async Task<bool> SuspensionExists(Guid id)
        {
            return await _commuterRepository.CommuterExists(id);
        }
    }
}