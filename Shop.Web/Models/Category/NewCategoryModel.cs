using System.ComponentModel.DataAnnotations;

namespace Shop.Web.Models.Category
{
    public class NewCategoryModel
    {
        [Required(ErrorMessage = "Please enter your name of the category")]
        [Display(Name = "Category name")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter url of the category image")]
        [Display(Name = "Image url")]
        public string ImageUrl { get; set; }

        [Display(Name = "Description (optional)")]
        public string Description { get; set; }
    }
}
