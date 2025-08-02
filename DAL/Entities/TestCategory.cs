using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public partial class TestCategory
{
    public long TestCategoryId { get; set; }

    public string Name { get; set; } = null!;

    public long? ServiceId { get; set; }

    [NotMapped]
    public bool? IsActive { get; set; } = true;

    public virtual Service? Service { get; set; }
}
