using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pagyeonja.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace Pagyeonja.Services.Services
{
    public interface IRiderService
    {
        Task<IEnumerable<Rider>> GetRiders();
        Task<IEnumerable<Rider>> GetRidersApproved();
        Task<Rider> GetRider(Guid id);
        Task<Rider> UpdateRider(Rider rider);
        Task<Rider> AddRider(Rider rider);
        Task<bool> DeleteRider(Guid id);
        Task<bool> RiderExists(Guid id);
        Task<bool> ImageExist(string filename, string usertype, string doctype);
        Task SaveImage(Guid id, List<IFormFile> images, string doctype, string usertype);
        Task SaveImagePath(Guid id, string doctype, string usertype);
    }
}