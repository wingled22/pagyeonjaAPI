using System;
using System.Collections.Generic;

namespace Pagyeonja.Entities.Entities;

public partial class Review
{
    public Guid ReviewId { get; set; }

    public Guid? TransactionId { get; set; }

    public Guid? CommuterId { get; set; }

    public Guid? RiderId { get; set; }

    public double? Rate { get; set; }

    public string? Comment { get; set; }
}
