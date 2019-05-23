using System.ComponentModel.DataAnnotations;

namespace Shop.Web.Models.Account
{
    public class AccountSettingsModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Please select role")]
        public string RoleId { get; set; }

        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$", ErrorMessage = @"Incorrect password")]
        [DataType(DataType.Password)]
        [Display(Name = "Old password")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$",
            ErrorMessage = @"Password must be at least 4 characters, 
no more than 8 characters, and must include at least one upper case letter, 
one lower case letter, and one numeric digit")]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords did not match")]
        [Display(Name = "Confirm new password")]
        public string NewPasswordConfirmation { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(255, ErrorMessage = "First name must have from 2 to 255 characters", MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(255, ErrorMessage = "Last name must have from 2 to 255 characters", MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Profile image url")]
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

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}
