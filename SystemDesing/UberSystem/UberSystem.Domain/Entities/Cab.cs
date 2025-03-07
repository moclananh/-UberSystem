﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UberSystem.Domain.Entities;

public partial class Cab
{
    public long Id { get; set; }

    public long? DriverId { get; set; }

    public string? Type { get; set; }

    public string? RegNo { get; set; }
    [JsonIgnore]

    public virtual Driver? Driver { get; set; }

    public virtual ICollection<Driver> Drivers { get; } = new List<Driver>();
}
