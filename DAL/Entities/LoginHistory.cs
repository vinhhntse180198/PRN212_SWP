using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class LoginHistory
{
    public long Id { get; set; }

    public DateTime LoginTime { get; set; }

    public long UserId { get; set; }

    public string? LoginType { get; set; }

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public virtual User User { get; set; } = null!;
}
