using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class JobLevel
{
    public Guid JobLevelId { get; set; }

    public string? JobLevelName { get; set; }

    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
}
