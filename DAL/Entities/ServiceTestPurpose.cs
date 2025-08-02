using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public partial class ServiceTestPurpose
{
    public long Id { get; set; }

    public long ServiceId { get; set; }

    public long TestPurposeId { get; set; }

    [NotMapped]
    public bool? IsActive { get; set; } = true;

    public virtual Service Service { get; set; } = null!;

    public virtual TestPurpose TestPurpose { get; set; } = null!;
}
