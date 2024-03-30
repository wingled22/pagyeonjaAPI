using System;
using System.Collections.Generic;

namespace pagyeonjaAPI.Entities;

public partial class Rider
{
    public Guid RiderId { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? VehicleNumber { get; set; }

    public string? Occupation { get; set; }

    public int? Age { get; set; }

    public string? ContactNumber { get; set; }

    public DateTime? Birthdate { get; set; }

    public string? EmailAddress { get; set; }

    public string? Sex { get; set; }

    public DateTime? DateApplied { get; set; }

    public string? Address { get; set; }

    public bool? ApprovalStatus { get; set; }

    public bool? SuspensionStatus { get; set; }

    public string? CivilStatus { get; set; }

    public string? ProfilePath { get; set; }
}
