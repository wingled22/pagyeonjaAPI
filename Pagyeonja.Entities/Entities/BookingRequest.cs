using System;
using System.Collections.Generic;

namespace Pagyeonja.Entities.Entities;

public partial class BookingRequest
{
    public Guid? BookingId { get; set; }

    public Guid? RiderId { get; set; }

    public Guid? CommuterId { get; set; }

    public string? PickupLocation { get; set; }

    public string? DropoffLocation { get; set; }

    public int? NumberOfPassengers { get; set; }

    public string? ContactNumber { get; set; }

    public bool? BookingStatus { get; set; }

    public double? PickupLng { get; set; }

    public double? PickupLat { get; set; }

    public double? DropoffLng { get; set; }

    public double? DropoffLat { get; set; }
}
