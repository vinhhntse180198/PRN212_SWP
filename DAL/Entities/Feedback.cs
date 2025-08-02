using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Feedback
{
    public int? Rating { get; set; }

    public DateTime? FeedbackDate { get; set; }

    public long FeedbackId { get; set; }

    public long? ServiceId { get; set; }

    public long? UserId { get; set; }

    public string Content { get; set; } = null!;

    public virtual Service? Service { get; set; }

    public virtual User? User { get; set; }
}
