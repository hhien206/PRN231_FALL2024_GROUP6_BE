﻿using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IJobSkillRepository : IGenericRepository<JobSkill>
    {
        public Task<List<JobSkill>?> ListJobSkillQuickSearch(string? nameQuickSerch);
    }
}
