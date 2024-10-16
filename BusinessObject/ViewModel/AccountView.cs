﻿using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModel
{
    public class AccountView
    {
        public int AccountId {  get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string UrlPicture { get; set; }
        public string Gender { get; set; }
        public DateTime? DateCreate { get; set; }
        public RoleView? Role { get; set; }
        public List<CertificateView>? Certificates { get; set; }
        public List<ResumeView>? Resumes { get; set; }
        public List<AccountJobSkillView>? AccountJobSkill { get; set; }
        public void ConvertAccountIntoAccountView(Account account, RoleView role, List<CertificateView> certificates, List<ResumeView> resumes, List<AccountJobSkillView> accountJobSkill)
        {
            AccountId = account.AccountId;
            Email = account.Email;
            PhoneNumber = account.PhoneNumber;
            Address = account.Address;
            DateOfBirth = account.DateOfBirth;
            UrlPicture = account.UrlPicture;
            Gender = account.Gender;
            DateCreate = account.DateCreate;
            Role = role;
            Certificates = certificates;
            Resumes = resumes;
            AccountJobSkill = accountJobSkill;
        }
    }
}
