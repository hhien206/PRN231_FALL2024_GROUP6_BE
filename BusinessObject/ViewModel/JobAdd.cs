﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModel
{
    public class JobAdd
    {
        public string JobTitle { get; set; }

        public string Description { get; set; }

        public string Requirement { get; set; }

        public string Benefit { get; set; }

        public string JobTime { get; set; }

        public string SalaryRange { get; set; }

        public string Experience { get; set; }

        public DateTime? Deadline { get; set; }

        public List<int> listJobSkillId { get; set; }


    }
}
