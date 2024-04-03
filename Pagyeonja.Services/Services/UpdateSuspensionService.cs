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
    public class UpdateSuspensionService : IUpdateSuspensionService
    {
        private readonly ISuspensionRepository _suspensionRepository;
        private readonly ICommuterRepository _commuterRepository;
        private readonly IRiderRepository _riderRepository;
        public UpdateSuspensionService(ISuspensionRepository suspensionRepository, ICommuterRepository commuterRepository, IRiderRepository riderRepository)
        {
            _suspensionRepository = suspensionRepository;
            _commuterRepository = commuterRepository;
            _riderRepository = riderRepository;
        }

        public async Task UpdateSuspensionDue(HitchContext context)
        {
            var suspendedData = await _suspensionRepository.GetExpiredSuspension();
            foreach(var sd in suspendedData)
            {
                if(sd.UserType.ToLower() == "commuter")
                {
                    var commuter = await _commuterRepository.GetCommuterSuspended(Guid.Parse(sd.UserId.ToString()));
                    if(commuter != null)
                    {
                        commuter.SuspensionStatus = false;
                        await _commuterRepository.UpdateCommuter(commuter);
                    }
                }
                else if(sd.UserType.ToLower() == "rider")
                {
                    var rider = await _riderRepository.GetRiderSuspended(Guid.Parse(sd.UserId.ToString()));
                    if(rider != null)
                    {
                        rider.SuspensionStatus = false;
                        await _riderRepository.UpdateRider(rider);
                    }
                }

                var suspension = await _suspensionRepository.GetSuspensionById(sd.SuspensionId);
                suspension.Status = false;

                await _suspensionRepository.UpdateSuspension(suspension);
            }

            return;
        }
    }
}