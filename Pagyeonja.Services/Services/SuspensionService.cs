using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pagyeonja.Entities.Entities;
using Pagyeonja.Repositories.Repositories;

namespace Pagyeonja.Services.Services
{
    public class SuspensionService : ISuspensionService
    {
        private readonly ISuspensionRepository _suspensionRepository;
        private readonly ICommuterRepository _commuterRepository;
        private readonly IRiderRepository _riderRepository;
        public SuspensionService(ISuspensionRepository suspensionRepository, ICommuterRepository commuterRepository, IRiderRepository riderRepository)
        {
            _suspensionRepository = suspensionRepository;
            _commuterRepository = commuterRepository;
            _riderRepository = riderRepository;
        }

        public Task<IEnumerable<Suspension>> GetSuspensions()
        {
            return _suspensionRepository.GetSuspensions();
        }
        public Task<Suspension> GetSuspension(Guid userid, string usertype)
        {
            return _suspensionRepository.GetSuspension(userid, usertype);
        }

        public Task<Suspension> UpdateSuspension(Suspension suspension)
        {
            return _suspensionRepository.UpdateSuspension(suspension);
        }

        public async Task<Suspension> InvokeSuspension(Suspension suspension)
        {
            await _suspensionRepository.InvokeSuspension(suspension);

            //Update the user based on the usertype and userid and set the suspension status to true
            if (suspension.UserType == "Commuter")
            {
                var User = await _commuterRepository.GetCommuterById(Guid.Parse(suspension.UserId.ToString()));
                if (User != null)
                {
                    User.SuspensionStatus = true;
                    await _commuterRepository.UpdateCommuter(User);
                }
            }
            else if (suspension.UserType == "Rider")
            {
                var User = await _riderRepository.GetRider(Guid.Parse(suspension.UserId.ToString()));
                if (User != null)
                {
                    User.SuspensionStatus = true;
                    await _riderRepository.UpdateRider(User);
                }
            }

            return suspension;
        }

        public async Task<Suspension> RevokeSuspension(Suspension suspension)
        {
            suspension.Status = false;
            await _suspensionRepository.UpdateSuspension(suspension);

            //Update the user based on the usertype and userid and set the suspension status to true
            if (suspension.UserType == "Commuter")
            {
                var User = await _commuterRepository.GetCommuterById(Guid.Parse(suspension.UserId.ToString()));
                if (User != null)
                {
                    User.SuspensionStatus = false;
                    await _commuterRepository.UpdateCommuter(User);
                }
            }
            else if (suspension.UserType == "Rider")
            {
                var User = await _riderRepository.GetRider(Guid.Parse(suspension.UserId.ToString()));
                if (User != null)
                {
                    User.SuspensionStatus = false;
                    await _riderRepository.UpdateRider(User);
                }
            }

            return suspension;
        }

        public async Task<bool> DeleteSuspension(Guid id)
        {
            return await _suspensionRepository.DeleteSuspension(id);
        }
    }
}