﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DataAccessObject.Models;

public partial class Certificate
{
    public int CertificateId { get; set; }

    public string CertificateUrl { get; set; }

    public int? AccountId { get; set; }

    public virtual Account Account { get; set; }
}