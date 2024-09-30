using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModel
{
    public class JobSearch
    {
        public string name { get; set; }
        public List<int> listJobSkillIdInclude { get; set; }
        public List<int> listJobSkillIdExclude { get; set; }
    }
}
