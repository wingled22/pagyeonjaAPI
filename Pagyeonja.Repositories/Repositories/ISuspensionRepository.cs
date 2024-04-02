using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pagyeonja.Entities.Entities;

namespace Pagyeonja.Repositories.Repositories
{
    public interface ISuspensionRepository
    {
        Task<IEnumerable<Suspension>> GetSuspensions();
        Task<Suspension> GetSuspension(Guid userid, string usertype);
        Task<Suspension> UpdateSuspension(Suspension Suspension);
        Task<Suspension> InvokeSuspension(Suspension Suspension);
        Task<bool> DeleteSuspension(Guid id);
        Task<bool> SuspensionExists(Guid id);
    }
}