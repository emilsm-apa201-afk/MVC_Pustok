using MVC_Pustokkk.Migrations;

namespace MVC_Pustokkk.Areas.Admin.ViewModels
{
    public class FeaturedVM
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Details { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public decimal PriceOld { get; set; }
        public decimal PriceDiscount { get; set; }
        //public int CategoryId { get; set; }
        //public Category Category { get; set; }
    }
}
