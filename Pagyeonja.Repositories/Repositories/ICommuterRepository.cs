using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pagyeonja.Entities.Entities;

namespace Pagyeonja.Repositories.Repositories
{
    public interface ICommuterRepository
    {
        Task<IEnumerable<Commuter>> GetAllCommuters();
        Task<IEnumerable<Commuter>> GetApprovedCommuters();
        Task<Commuter> GetCommuterById(Guid id);
        Task<Commuter> UpdateCommuter(Commuter commuter);
        Task<Commuter> RegisterCommuter(Commuter commuter);
        Task<bool> DeleteCommuter(Guid id);
    }
}