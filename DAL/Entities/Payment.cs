using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Payment
{
    public double Amount { get; set; }

    public long? AppointmentId { get; set; }

    public DateTime? PaymentDate { get; set; }

    public long PaymentId { get; set; }

    public string? PaymentMethod { get; set; }

    public string? Status { get; set; }

    public virtual Appointment? Appointment { get; set; }
}
