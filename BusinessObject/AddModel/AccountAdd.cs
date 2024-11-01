using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.AddModel
{
    public class AccountAdd
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "Password must be at least 6 characters long", MinimumLength = 6)]
        public string? password { get; set; }
        [Required(ErrorMessage = "Full name is required")]
        public string? FullName { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Phone number must contain only digits")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public string? gender { get; set; }

    }
}
