using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pagyeonja.Entities.Entities;

namespace Pagyeonja.Services.Services
{
    public interface IRiderService
    {
        Task<IEnumerable<Rider>> GetRiders();
        Task<IEnumerable<Rider>> GetRidersApproved();
        Task<Rider> GetRider(Guid id);
        Task<bool> UpdateRider(Rider rider);
        Task<Rider> AddRider(Rider rider);
        Task<bool> DeleteRider(Guid id);
        Task<bool> RiderExists(Guid id);
    }
}