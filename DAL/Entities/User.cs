using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class User
{
    public DateOnly? DateOfBirth { get; set; }

    public long UserId { get; set; }

    public string? Gender { get; set; }

    public string? Phone { get; set; }

    public string Role { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? FullName { get; set; }

    public string? Address { get; set; }

    public string Password { get; set; } = null!;

    public string? Avatar { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<CollectedSample> CollectedSamples { get; set; } = new List<CollectedSample>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<LoginHistory> LoginHistories { get; set; } = new List<LoginHistory>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();
}
