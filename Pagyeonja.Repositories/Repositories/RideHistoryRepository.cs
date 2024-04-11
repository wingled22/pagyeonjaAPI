using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pagyeonja.Entities.Entities;
using Pagyeonja.Repositories.Repositories.Models;

namespace Pagyeonja.Repositories.Repositories
{
    public class RideHistoryRepository : IRideHistoryRepository
    {

        private readonly HitchContext _context;

        public RideHistoryRepository(HitchContext context)
        {
            _context = context;
        }

        public async Task<RideHistory> AddRideHistory(RideHistory rideHistory)
        {
            do
            {
                rideHistory.RideHistoryId = Guid.NewGuid();
            } while (await RideHistoryExists(rideHistory.RideHistoryId));

            await _context.RideHistories.AddAsync(rideHistory);
            await _context.SaveChangesAsync();
            return rideHistory;
        }

        public async Task<IEnumerable<RideHistory>> GetRideHistories()
        {
            return await _context.RideHistories.OrderByDescending(rh => rh.RideHistoryId).ToListAsync();
        }

        public async Task<RideHistory> GetRideHistoryByTransaction(Guid id)
        {
            return await _context.RideHistories.Where(rh => rh.TransactionId == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<RideHistoryModel>> GetUserRideHistory(Guid id, string usertype)
        {

            //get the id of the commuter or rider on the review
            // var res = usertype.ToLower() == "commuter" ?
            // await _context.Commuters.Where(c => c.CommuterId == id).FirstOrDefaultAsync() :
            // await _context.Riders.Where(r => r.RiderId == id).FirstOrDefaultAsync();

            //Please fix : If the ride histories.reviewID is null then catch it. because if it is not null it won't return a value

            return usertype.ToLower() == "commuter" ? await
            (
                from rh in _context.RideHistories
                join t in _context.Transactions on rh.TransactionId equals t.TransactionId
                join r in _context.Riders on rh.RiderId equals r.RiderId
                join c in _context.Commuters on rh.CommuterId equals c.CommuterId
                from re in _context.Reviews.Where(review => rh.ReviewId == review.ReviewId).DefaultIfEmpty()
                where c.CommuterId == id
                where c.CommuterId == id
                select new RideHistoryModel
                {
                    RideHistoryId = rh.RideHistoryId,
                    RiderId = r.RiderId,
                    CommuterId = c.CommuterId,
                    FirstName = r.FirstName ?? "", //name of the rider (since commuter is executed)
                    MiddleName = r.MiddleName ?? "",
                    LastName = r.LastName ?? "",
                    VehicleNumber = r.VehicleNumber ?? "",
                    Occupation = r.Occupation ?? "",
                    TransactionId = t.TransactionId,
                    Fare = t.Fare ?? 0.0,
                    StartingTime = t.StartingTime ?? null,
                    EndTime = t.EndTime ?? null,
                    StartingPoint = t.StartingPoint ?? "",
                    EndDestination = t.EndDestination ?? "",
                    ReviewId = re.ReviewId,
                    Rate = re.Rate,
                    Comment = re.Comment
                }).ToListAsync() : await
                (
                    from rh in _context.RideHistories
                    join t in _context.Transactions on rh.TransactionId equals t.TransactionId
                    join r in _context.Riders on rh.RiderId equals r.RiderId
                    join c in _context.Commuters on rh.CommuterId equals c.CommuterId
                    from re in _context.Reviews.Where(review => rh.ReviewId == review.ReviewId).DefaultIfEmpty()
                    where r.RiderId == id
                    select new RideHistoryModel
                    {
                        RideHistoryId = rh.RideHistoryId,
                        RiderId = r.RiderId,
                        CommuterId = c.CommuterId,
                        FirstName = c.FirstName ?? "", //name of the commuter (since rider is executed)
                        MiddleName = c.MiddleName ?? "",
                        LastName = c.LastName ?? "",
                        VehicleNumber = r.VehicleNumber ?? "",
                        Occupation = c.Occupation ?? "",
                        TransactionId = t.TransactionId,
                        Fare = t.Fare ?? 0.0,
                        StartingTime = t.StartingTime ?? null,
                        EndTime = t.EndTime ?? null,
                        StartingPoint = t.StartingPoint ?? "",
                        EndDestination = t.EndDestination ?? "",
                        ReviewId = re == null ? null : re.ReviewId,
                        Rate = re == null ? null : re.Rate,
                        Comment = re == null ? null : re.Comment
                    }).ToListAsync();
        }

        public async Task<bool> RideHistoryExists(Guid id)
        {
            return await _context.RideHistories.AnyAsync(rh => rh.RideHistoryId == id);
        }

        public async Task<RideHistory> UpdateRideHistory(RideHistory rideHistory)
        {
            _context.Entry(rideHistory).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return rideHistory;
        }
    }
}