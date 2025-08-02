using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public partial class SampleType
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public long KitComponentId { get; set; }

    [NotMapped]
    public bool? IsActive { get; set; } = true;

    public virtual ICollection<CollectedSample> CollectedSamples { get; set; } = new List<CollectedSample>();

    public virtual KitComponent KitComponent { get; set; } = null!;
}
