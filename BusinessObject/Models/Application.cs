using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Application
{
    public Guid ApplicationId { get; set; }

    public Guid? JobId { get; set; }

    public Guid? AccountId { get; set; }

    public DateOnly? ApplicationDate { get; set; }

    public string? Status { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ICollection<InterviewProcess> InterviewProcesses { get; set; } = new List<InterviewProcess>();

    public virtual Job? Job { get; set; }
}
