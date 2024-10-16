using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModel
{
    public class AccountJobSkillView
    {
        public int AccountJobSkillId { get; set; }

        public string? JobSkillName { get; set; }

        public string? Experience { get; set; }
        public void ConvertAccountJobSkillIntoAccountJobSkillView(AccountJobSkill key, JobSkill key2)
        {
            AccountJobSkillId = key.AccountJobSkillId;
            JobSkillName = key2.Name;
            Experience = key.Experience;
        }
    }
}
