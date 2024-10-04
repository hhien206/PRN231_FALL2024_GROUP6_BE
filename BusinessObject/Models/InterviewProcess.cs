using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class InterviewProcess
{
    public Guid InterviewId { get; set; }

    public Guid? ApplicationId { get; set; }

    public DateOnly? InterviewDate { get; set; }

    public string? Interviewer { get; set; }

    public string? InterviewResult { get; set; }

    public virtual Application? Application { get; set; }
}
