using Microsoft.AspNetCore.Http;
using MVC_Pustokkk.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC_Pustokkk.Areas.Admin.ViewModels
{
    public class ProductVM
    {
        public int Id { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Details { get; set; }
        public string Image { get; set; }

        [Required]
        public decimal Price { get; set; }
        public decimal PriceOld { get; set; }
        public decimal PriceDiscount { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public List<Category> Categories { get; set; }

        [Display(Name = "Product Image")]
        public IFormFile Photo { get; set; }
    }
}
