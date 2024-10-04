using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class JobCategory
{
    public Guid JobCategoryId { get; set; }

    public string? JobCategoryName { get; set; }

    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
}
