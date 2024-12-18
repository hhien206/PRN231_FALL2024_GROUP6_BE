﻿using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IJobJobSkillRepository : IGenericRepository<JobJobSkill>
    {
        public Task<List<JobJobSkill>> GetAllJobJobSkillByJobId(int jobId);
    }
}
