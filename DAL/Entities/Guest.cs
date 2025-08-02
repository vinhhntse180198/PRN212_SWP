using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Guest
{
    public DateOnly? DateOfBirth { get; set; }

    public long GuestId { get; set; }

    public string? Email { get; set; }

    public string? FullName { get; set; }

    public string? Gender { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
