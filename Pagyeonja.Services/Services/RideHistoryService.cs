using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pagyeonja.Entities.Entities;
using Pagyeonja.Repositories.Repositories;

namespace Pagyeonja.Services.Services
{
    public class RideHistoryService
    {
        private readonly IRideHistoryRepository _rideHistoryRepository;
        public RideHistoryService(IRideHistoryRepository rideHistoryRepository)
        {
            _rideHistoryRepository = rideHistoryRepository;
        }
    }
}