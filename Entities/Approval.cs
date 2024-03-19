using System;
using System.Collections.Generic;

namespace pagyeonjaAPI.Entities;

public partial class Approval
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public bool? ApprovalStatus { get; set; }

    public DateTime? ApprovalDate { get; set; }

    public string? UserType { get; set; }
}
