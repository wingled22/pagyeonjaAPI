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
        public SuspensionService(ISuspensionRepository suspensionRepository)
        {
            _suspensionRepository = suspensionRepository;
        }
        
        public Task<IEnumerable<Suspension>> GetSuspensions()
        {
            return _suspensionRepository.GetSuspensions();
        }
        public Task<Suspension> GetSuspension(Guid userid, string usertype)
        {
            return _suspensionRepository.GetSuspension(userid, usertype);
        }
    }
}