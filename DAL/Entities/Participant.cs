using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Participant
{
    public DateOnly? DateOfBirth { get; set; }

    public long ParticipantId { get; set; }

    public string? Email { get; set; }

    public string? FullName { get; set; }

    public string? Gender { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<CollectedSample> CollectedSamples { get; set; } = new List<CollectedSample>();
}
