using System.ComponentModel.DataAnnotations;
using MVC_Pustokkk.Models;

namespace MVC_Pustokkk.Areas.Admin.ViewModels
{
    public class CategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductCount { get; set; }
        public string Image { get; set; }
        //public string Photo { get; set; }
        public List<ProductVM> Products { get; set; } = new List<ProductVM>();

        [Display(Name = "Product Image")]
        public IFormFile Photo { get; set; }
    }
}
