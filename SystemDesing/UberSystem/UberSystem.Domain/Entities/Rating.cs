using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UberSystem.Domain.Entities;

public partial class Rating
{
    public long Id { get; set; }

    public long? CustomerId { get; set; }

    public long? DriverId { get; set; }

    public long? TripId { get; set; }

    public int? Rating1 { get; set; }

    public string? Feedback { get; set; }
    [JsonIgnore]
    public virtual Customer? Customer { get; set; }
    [JsonIgnore]
    public virtual Driver? Driver { get; set; }
    [JsonIgnore]
    public virtual Trip? Trip { get; set; }
}
