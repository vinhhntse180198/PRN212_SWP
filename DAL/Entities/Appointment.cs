using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Appointment
{
    public DateOnly Dob { get; set; }

    public DateTime AppointmentDate { get; set; }

    public long AppointmentId { get; set; }

    public DateTime? CollectionSampleTime { get; set; }

    public long? ServiceId { get; set; }

    public long? UserId { get; set; }

    public string? CollectionLocation { get; set; }

    public string? District { get; set; }

    public string? Email { get; set; }

    public string? FingerprintFile { get; set; }

    public string? FullName { get; set; }

    public string? Gender { get; set; }

    public string? Phone { get; set; }

    public string? Province { get; set; }

    public string? ServiceType { get; set; }

    public string? Status { get; set; }

    public string? TestCategory { get; set; }

    public string? TestPurpose { get; set; }

    public long? GuestId { get; set; }

    public long? KitComponentId { get; set; }

    public string? FirstResultFile => Results?.FirstOrDefault()?.ResultFile;
    
    public string ResultStatus => Results?.Any() == true ? "Có kết quả" : "Chưa có kết quả";

    public virtual ICollection<CollectedSample> CollectedSamples { get; set; } = new List<CollectedSample>();

    public virtual Guest? Guest { get; set; }

    public virtual KitComponent? KitComponent { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    public virtual Service? Service { get; set; }

    public virtual User? User { get; set; }
}
