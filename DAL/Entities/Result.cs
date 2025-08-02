using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public partial class Result
{
    public DateOnly? ResultDate { get; set; }

    public long? AppointmentId { get; set; }

    public long ResultId { get; set; }

    public long? SampleId { get; set; }

    public long UserId { get; set; }

    public string? Interpretation { get; set; }

    public string? ResultData { get; set; }

    public string? ResultFile { get; set; }

    public string? Status { get; set; }

    [NotMapped]
    public string? Note { get; set; }

    public virtual Appointment? Appointment { get; set; }

    public virtual ICollection<CollectedSample> CollectedSamples { get; set; } = new List<CollectedSample>();

    public virtual CollectedSample? Sample { get; set; }

    public virtual User User { get; set; } = null!;
}
