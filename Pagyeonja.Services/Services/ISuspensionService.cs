using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pagyeonja.Entities.Entities;

namespace Pagyeonja.Services.Services
{
    public interface ISuspensionService
    {
        Task<IEnumerable<Suspension>> GetSuspensions();
        Task<Suspension> GetSuspension(Guid userid, string usertype);
    }
}