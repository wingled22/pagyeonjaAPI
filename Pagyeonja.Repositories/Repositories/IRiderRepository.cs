using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pagyeonja.Entities.Entities;

namespace Pagyeonja.Repositories.Repositories
{
    public interface IRiderRepository
    {
        Task<IEnumerable<Rider>> GetRiders();
        Task<IEnumerable<Rider>> GetRidersApproved();
        Task<Rider> GetRider(Guid id);
        Task<Rider> UpdateRider(Rider rider);
        Task<Rider> AddRider(Rider rider);
        Task<bool> DeleteRider(Guid id);
        Task<bool> RiderExists(Guid id);
        Task<bool> ImageExist(string filename, string usertype, string doctype);
        Task SaveImagePath(Guid id, string doctype, string usertype, string filename, string docview);
        Task<Rider> GetRiderSuspended(Guid id);
    }
}