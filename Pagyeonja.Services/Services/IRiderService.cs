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
        Task<Rider> UpdateRider(Rider rider);
        Task<Rider> RegisterRider(Rider rider, List<string> imagePaths);
        Task<bool> DeleteRider(Guid id);
        Task<bool> RiderExists(Guid id);
    }
}