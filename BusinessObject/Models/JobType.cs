using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class JobType
{
    public Guid JobTypeId { get; set; }

    public string? JobTypeName { get; set; }

    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
}
