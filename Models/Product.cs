using System.ComponentModel.DataAnnotations.Schema;
using MVC_Pustokkk.Models.Base;

namespace MVC_Pustokkk.Models
{
    public class Product:BaseEntity
    {  
        public int Id { get; set; }
        public string Author { get; set; }
        public string Details { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public decimal PriceOld { get; set; }
        public decimal PriceDiscount { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }

    }
}
