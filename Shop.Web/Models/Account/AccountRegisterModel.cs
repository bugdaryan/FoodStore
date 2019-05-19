using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Models.Account
{
    public class AccountRegisterModel
    {
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$", 
            ErrorMessage = @"Password must be at least 4 characters, 
no more than 8 characters, and must include at least one upper case letter, 
one lower case letter, and one numeric digit")]
        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Passwords did not match")]
        [Display(Name = "Confirm password")]
        public string PasswordConfirmation { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage = "Enter valid email")]
        [DataType(DataType.EmailAddress)]
        [Display(Name ="Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(255, ErrorMessage = "First name must have from 2 to 255 characters", MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(255, ErrorMessage = "Last name must have from 2 to 255 characters", MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [Display(Name ="Profile image url")]
        public string ImageUrl { get; set; }

        [Display(Name = "Addres line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Addres line 2")]
        public string AddressLine2 { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City is required")]
        [StringLength(255, ErrorMessage = "City must have from 2 to 255 characters", MinimumLength = 2)]
        public string City { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country is required")]
        [StringLength(255, ErrorMessage = "Country must have from 2 to 255 characters", MinimumLength = 2)]
        public string Country { get; set; }

        [Display(Name ="Phone number")]
        public string PhoneNumber { get; set; }

        public string ReturnUrl { get; set; }
    }
}
