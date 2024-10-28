using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModel
{
    public class EducateView
    {
        public int EducateId { get; set; }

        public string EducateName { get; set; }

        public string Description { get; set; }

        public DateOnly? Date { get; set; }

        public void ConvertEducateIntoEducateView(Educate key)
        {
            EducateId = key.EducateId;
            EducateName = key.EducateName;
            Description = key.Description;
            Date = key.Date;
        }
    }
}
