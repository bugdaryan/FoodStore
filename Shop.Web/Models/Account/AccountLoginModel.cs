using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Models.Account
{
    public class AccountLoginModel
    {

        [Required(ErrorMessage = "Enter your email")]
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage = "Email is incorrect")]
        [DataType(DataType.EmailAddress)]
        [Display(Name ="Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$", 
            ErrorMessage = @"Password is incorrect")]
        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        public string Password { get; set; }

        public string IncorrectInput { get; set; }

        public string ReturnUrl { get; set; }
    }
}
