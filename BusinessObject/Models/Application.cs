﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Application
{
    public int ApplicationId { get; set; }

    public DateTime? ApplicationDate { get; set; }

    public string Status { get; set; }

    public int? AccountId { get; set; }

    public int? JobId { get; set; }

    public virtual ICollection<InterviewProcess> InterviewProcesses { get; set; } = new List<InterviewProcess>();

    public virtual Job Job { get; set; }
}