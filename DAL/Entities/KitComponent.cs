using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class KitComponent
{
    public int? Quantity { get; set; }

    public long KitComponentId { get; set; }

    public long? ServiceId { get; set; }

    public string? ComponentName { get; set; }

    public string? Introduction { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<CollectedSample> CollectedSamples { get; set; } = new List<CollectedSample>();

    public virtual ICollection<SampleType> SampleTypes { get; set; } = new List<SampleType>();

    public virtual Service? Service { get; set; }
}
