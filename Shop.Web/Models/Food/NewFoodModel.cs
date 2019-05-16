using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Models.Food
{
    public class NewFoodModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your name of the food")]
        [Display(Name = "Food name*")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter url of the food image")]
        [Display(Name = "Image url*")]
        public string ImageUrl { get; set; }

        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }


        [Display(Name = "Long Description")]
        public string LongDescription { get; set; }

        [Required(ErrorMessage = "Please enter price of the food")]
        [Range(0.1,double.MaxValue)]
        [Display(Name = "Price*")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Select if the food is prefered or not")]
        [Display(Name = "Is prefered?*")]
        public bool? IsPreferedFood { get; set; } = false;

        [Range(0, double.MaxValue)]
        [Required(ErrorMessage = "Please enter how many is left in stock")]
        [Display(Name = "In stock*")]
        public int? InStock { get; set; }

        [Required(ErrorMessage = "Please select category")]
        [Range(1,double.MaxValue)]
        public int? CategoryId { get; set; }
    }
}
