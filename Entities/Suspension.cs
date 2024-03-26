using System;
using System.Collections.Generic;

namespace pagyeonjaAPI.Entities;

public partial class Suspension
{
    public Guid SuspensionId { get; set; }

    public Guid? UserId { get; set; }

    public string? UserType { get; set; }

    public string? Reason { get; set; }

    public DateTime? SuspensionDate { get; set; }

    public DateTime? InvokedSuspensionDate { get; set; }

    public bool? Status { get; set; }
}
