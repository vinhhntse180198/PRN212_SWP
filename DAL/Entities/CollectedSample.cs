using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class CollectedSample
{
    public DateOnly? CollectedDate { get; set; }

    public DateOnly? ReceivedDate { get; set; }

    public long? AppointmentId { get; set; }

    public long? KitComponentId { get; set; }

    public long? ParticipantId { get; set; }

    public long CollectedSampleId { get; set; }

    public long SampleTypeId { get; set; }

    public long? UserId { get; set; }

    public string? Status { get; set; }

    public long? SampleId { get; set; }

    public virtual Appointment? Appointment { get; set; }

    public virtual KitComponent? KitComponent { get; set; }

    public virtual Participant? Participant { get; set; }

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    public virtual Result? Sample { get; set; }

    public virtual SampleType SampleType { get; set; } = null!;

    public virtual User? User { get; set; }
}
