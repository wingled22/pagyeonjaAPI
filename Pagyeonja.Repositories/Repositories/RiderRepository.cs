using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using Pagyeonja.Entities.Entities;

namespace Pagyeonja.Repositories.Repositories
{

  public class RiderRepository : IRiderRepository
  {
    private readonly HitchContext _context;

    public RiderRepository(HitchContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Rider>> GetRiders()
    {
      return await _context.Riders.OrderByDescending(a => a.DateApplied).ToListAsync();

    }

    public async Task<IEnumerable<Rider>> GetRidersApproved()
    {
      return await _context.Riders.Where(a => a.ApprovalStatus == true).OrderByDescending(a => a.DateApplied).ToListAsync();
    }

    public async Task<Rider> GetRider(Guid id)
    {

      return await _context.Riders.FindAsync(id);
    }


    public async Task<Rider> UpdateRider(Rider rider)
    {
      _context.Entry(rider).State = EntityState.Modified;
      await _context.SaveChangesAsync();
      return rider;
    }

    public async Task<Rider> AddRider(Rider rider)
    {
      // Implementation for registering a rider
      try
      {
        do
        {
          rider.RiderId = Guid.NewGuid();
        } while (await RiderExists(rider.RiderId));
        rider.DateApplied = DateTime.Now;

        await _context.Riders.AddAsync(rider);
        await _context.SaveChangesAsync();
        return rider;
      }
      catch (System.Exception)
      {
        throw;
      }
    }

    public async Task<bool> DeleteRider(Guid id)
    {
      var rider = await _context.Riders.FindAsync(id);
      if (rider == null)
        return false;

      _context.Riders.Remove(rider);
      await _context.SaveChangesAsync();
      return true;
    }

    public async Task<bool> RiderExists(Guid id)
    {
      return await _context.Riders.AnyAsync(e => e.RiderId == id);
    }

    public async Task<bool> ImageExist(string filename, string usertype, string doctype)
    {
      return
        usertype.ToLower() == "rider" &&
        doctype.ToLower() == "profile" ?
        await _context.Riders.AnyAsync(r => r.ProfilePath == filename) :
        usertype.ToLower() == "commuter" &&
        doctype.ToLower() == "profile" ?
        await _context.Commuters.AnyAsync(c => c.ProfilePath == filename) :
        await _context.Documents.AnyAsync(d => d.DocumentPath == filename);
    }

    public async Task SaveImagePath(Guid id, string doctype, string usertype, string filename, string docview)
    {
      if (usertype.ToLower() == "rider" && doctype.ToLower() == "profile")
      {
        var rider = await _context.Riders.FindAsync(id);
        rider.ProfilePath = filename;
        _context.Riders.Update(rider);
      }
      else if (usertype.ToLower() == "commuter" && doctype.ToLower() == "profile")
      {
        var commuter = await _context.Commuters.FindAsync(id);
        commuter.ProfilePath = filename;
        _context.Commuters.Update(commuter);
      }
      else
      {
        var Document = new Document
        {
          Id = Guid.NewGuid(),
          UserId = id,
          UserType = usertype.ToLower(),
          DocumentName = doctype,
          DocumentPath = filename,
          DocumentView = docview
        };
        while (await _context.Documents.AnyAsync(d => d.Id == Document.Id))
        {
          Document.Id = Guid.NewGuid();
        }
        await _context.Documents.AddAsync(Document);
      }
      await _context.SaveChangesAsync();
    }

    public async Task<Rider> GetRiderSuspended(Guid id)
    {
      return await _context.Riders.Where(r => r.RiderId == id && r.SuspensionStatus == true).FirstOrDefaultAsync();
    }
  }

}