﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class InterviewProcess
{
    public int InterviewProcessId { get; set; }

    public int? RoundNumber { get; set; }

    public DateTime? InterviewDate { get; set; }

    public string Detail { get; set; }

    public string Result { get; set; }

    public DateTime? DateCreated { get; set; }

    public int? AccountId { get; set; }

    public int? ApplicationId { get; set; }

    public virtual Application Application { get; set; }
}