using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModel
{
    public class ResumeView
    {
        public int ResumeId {  get; set; }
        public string? UrlResume { get; set; }
        public void ConvertResumeIntoResumeView(Resume key)
        {
            ResumeId = key.ResumeId;
            UrlResume = key.UrlResume;
        }
    }   
}
