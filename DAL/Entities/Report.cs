using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Report
{
    public DateTime? CreatedAt { get; set; }

    public long ReportId { get; set; }

    public long? UserId { get; set; }

    public string? ReportContent { get; set; }

    public string? ReportTitle { get; set; }

    public virtual User? User { get; set; }
}
