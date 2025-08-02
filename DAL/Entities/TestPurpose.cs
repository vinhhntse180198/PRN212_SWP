using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public partial class TestPurpose
{
    public long TestPurposeId { get; set; }

    public string? TestPurposeDescription { get; set; }

    public string TestPurposeName { get; set; } = null!;

    [NotMapped]
    public bool? IsActive { get; set; } = true;

    public virtual ICollection<ServiceTestPurpose> ServiceTestPurposes { get; set; } = new List<ServiceTestPurpose>();
}
