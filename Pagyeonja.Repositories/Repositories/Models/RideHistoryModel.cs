using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pagyeonja.Repositories.Repositories.Models
{
    public class RideHistoryModel
    {
        public Guid RideHistoryId { get; set; }
        public Guid RiderId { get; set; }
        public Guid CommuterId {get; set;}
        public string? FirstName { get; set; } = "";
        public string? MiddleName { get; set; } = "";
        public string? LastName { get; set; } = "";
        public string? VehicleNumber { get; set; } = "";
        public string? Occupation { get; set; } = "";

        public Guid TransactionId { get; set; }
        public double? Fare { get; set; } = 0.0;
        public DateTime? StartingTime { get; set; } = null;
        public DateTime? EndTime { get; set; } = null;
        public string? StartingPoint { get; set; } = "";
        public string? EndDestination { get; set; } = "";

        public Guid? ReviewId { get; set; } = null;
        public double? Rate { get; set; } = 0.0;
        public string? Comment { get; set; } = "";
    }
}