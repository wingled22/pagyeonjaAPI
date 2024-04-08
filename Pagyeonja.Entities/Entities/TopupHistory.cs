using System;
using System.Collections.Generic;

namespace Pagyeonja.Entities.Entities;

public partial class TopupHistory
{
    public Guid TopupId { get; set; }

    public Guid? RiderId { get; set; }

    public double? TopupBefore { get; set; }

    public double? TopupAfter { get; set; }

    public double? TopupAmount { get; set; }

    public DateTime? TopupDate { get; set; }

    public string? Status { get; set; }
}
