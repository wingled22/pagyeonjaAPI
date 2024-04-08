using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pagyeonja.Repositories.Repositories.Models
{
    public class TopupHistoryModel
    {
        public Guid TopupId { get; set; }
        public Guid RiderId { get; set; }

        public string? FirstName { get; set; } = "";
        public string? MiddleName { get; set; } = "";
        public string? LastName { get; set; } = "";
        
        public double? TopupBefore { get; set; } = 0.0;
        public double? TopupAfter { get; set; } = 0.0;
        public double? TopupAmount { get; set; } = 0.0;
        public DateTime? TopupDate { get; set; } = null;
        public string? Status { get; set; } = "";
    }
}