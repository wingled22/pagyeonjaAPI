using System;
using System.Collections.Generic;

namespace pagyeonjaAPI.Entities;

public partial class Rider
{
    public int Id { get; set; }

    public Guid? RiderId { get; set; }

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

    public DateTime? DateRegistered { get; set; }

    public string? Address { get; set; }
}
