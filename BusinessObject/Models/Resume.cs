using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Resume
{
    public Guid ResumeId { get; set; }

    public Guid? AccountId { get; set; }

    public string? ResumeUrl { get; set; }

    public virtual Account? Account { get; set; }
}
