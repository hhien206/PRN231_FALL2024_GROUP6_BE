using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModel
{
    public class CertificateView
    {
        public int CertificateId {  get; set; }
        public string? CertificateUrl {  get; set; }

        public void ConvertCertificateIntoCertificateView(Certificate key)
        {
            CertificateId = key.CertificateId;
            CertificateUrl = key.CertificateUrl;
        }
    }
}
