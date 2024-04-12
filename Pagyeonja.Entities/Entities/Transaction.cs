using System;
using System.Collections.Generic;

namespace Pagyeonja.Entities.Entities;

public partial class Transaction
{
    public Guid TransactionId { get; set; }

    public double? Fare { get; set; }

    public DateTime? StartingTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? StartingPoint { get; set; }

    public string? EndDestination { get; set; }

    public Guid? RiderId { get; set; }

    public Guid? CommuterId { get; set; }
}
