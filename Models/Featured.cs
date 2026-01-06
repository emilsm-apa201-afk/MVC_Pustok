using MVC_Pustokkk.Models.Base;

namespace MVC_Pustokkk.Models
{
    public class Featured:BaseEntity
    {  
        public string Author { get; set; }
        public string Details { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public decimal PriceOld { get; set; }
        public decimal PriceDiscount { get; set; }

    }
}
