using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject.ViewModel
{
    public class JobSearch
    {
        public string name { get; set; }
        public List<int> listJobSkillIdInclude { get; set; }
        public List<int> listJobSkillIdExclude { get; set; }
        public int? jobCategoryId { get; set; }
        public int? jobLevelId { get; set; }
        public int? jobTypeId {  get; set; }
    }
}
