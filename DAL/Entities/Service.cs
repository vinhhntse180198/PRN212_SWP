using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Service
{
    public double Price { get; set; }

    public long ServiceId { get; set; }

    public string? Description { get; set; }

    public string? ServiceName { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<KitComponent> KitComponents { get; set; } = new List<KitComponent>();

    public virtual ICollection<ServiceTestPurpose> ServiceTestPurposes { get; set; } = new List<ServiceTestPurpose>();

    public virtual ICollection<TestCategory> TestCategories { get; set; } = new List<TestCategory>();
}
