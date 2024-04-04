using System;
using System.Collections.Generic;

namespace Pagyeonja.Entities.Entities;

public partial class RideHistory
{
    public Guid RideHistoryId { get; set; }

    public Guid? TransactionId { get; set; }

    public Guid? RiderId { get; set; }

    public Guid? CommuterId { get; set; }

    public Guid? ReviewId { get; set; }
}
